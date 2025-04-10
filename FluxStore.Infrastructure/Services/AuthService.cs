using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FluxStore.Application.DTOs;
using FluxStore.Application.Exceptions;
using FluxStore.Application.Interfaces;
using FluxStore.Domain.Entities;
using FluxStore.Infrastructure.Auth;
using FluxStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FluxStore.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthService> _logger;
        private readonly IEmailService _emailService;

        public AuthService(ApplicationDbContext context, JwtSettings jwtSettings,
            ILogger<AuthService> logger, IEmailService emailService)
        {
            _context = context;
            _jwtSettings = jwtSettings;
            _logger = logger;
            _emailService = emailService;
        }


        public async Task<AuthResponse> RegisterAsync(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new AuthException("Email already exists", 409); // 409 Conflict

            try
            {
                var hashedPassword = HashPassword(dto.Password);
                var user = new User
                {
                    UserName = dto.Username,
                    Email = dto.Email,
                    PasswordHash = hashedPassword,
                    Role = dto.Role,
                };

                var response = GenerateJwtToken(user);
                user.RefreshToken = response.RefreshToken;
                user.RefreshTokenExpiryTime = response.Expiration;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                response = GenerateJwtToken(user);
                return response;
            }
            catch (AuthException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during registration");
                throw new AuthException("An error occurred while registering the user", 500);
            }
        }

        public async Task<AuthResponse> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                throw new AuthException("Invalid email or password", 401);

            try
            {
                var response = GenerateJwtToken(user);
                user.RefreshToken = response.RefreshToken;
                user.RefreshTokenExpiryTime = response.Expiration;
                await _context.SaveChangesAsync();

                return response;
            }
            catch (AuthException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login");
                throw new AuthException("An error occurred while logging in", 500);
            }
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                throw new AuthException("Email not found", 404);

            // Generate a secure token
            user.ResetPasswordToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            user.ResetPasswordExpiry = DateTime.UtcNow.AddHours(1);

            await _context.SaveChangesAsync();

            // Send the token via email
            await _emailService.SendEmailAsync(
                user.Email, "Password Reset", EmailBody(user));

            return "Password reset email sent.";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ResetPasswordToken == dto.Token);

            if (user == null || user.ResetPasswordExpiry < DateTime.UtcNow)
                throw new AuthException("Invalid or expired reset token", 400);

            if (dto.NewPassword != dto.ConfirmPassword)
                throw new AuthException("Passwords do not match", 400);

            // Hash the new password
            user.PasswordHash = HashPassword(dto.NewPassword);
            user.ResetPasswordToken = null;
            user.ResetPasswordExpiry = null;

            await _context.SaveChangesAsync();

            return "Password reset successfully.";
        }

        public static bool IsValidPassword(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => "!@#$%^&*()".Contains(ch));
        }

        private AuthResponse GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role.ToString(), user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponse
            {
                Username = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = GenerateRefreshToken(),
                Expiration = token.ValidTo
            };
        }

        private string GenerateRefreshToken()
            => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }

        private static string EmailBody(User? user)
        {
            //https://localhost:7016
            return $@"
<html>
<head>
    <style>
        .container {{
            font-family: Arial, sans-serif;
            text-align: center;
            padding: 20px;
        }}
        .button {{
            background-color: #007bff;
            color: white;
            padding: 10px 20px;
            text-decoration: none;
            border-radius: 5px;
            display: inline-block;
            margin-top: 20px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h2>Password Reset Request</h2>
        <p>Hello {user?.UserName},</p>
        <p>We received a request to reset your password. Click the button below to proceed:</p>
        <a href='http://localhost:5232/api/auth/reset-password?token={user?.ResetPasswordToken}' class='button'>Reset Password</a>
        <p>If you did not request this, please ignore this email.</p>
        <p>Thank you, <br/> FluxStore Team</p>
    </div>
</body>
</html>
";
        }
    }
}