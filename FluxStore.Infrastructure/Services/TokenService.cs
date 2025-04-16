using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FluxStore.Application.Common.Interfaces;
using FluxStore.Domain.Entities;
using FluxStore.Infrastructure.Persistence;
using FlxStore.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FluxStore.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly Dictionary<string, (string Email, DateTime Expiry)> _resetTokens = new();

        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public TokenService(IOptions<JwtSettings> jwtSettings, IConfiguration config, AppDbContext context)
        {
            _jwtSettings = jwtSettings;
            _config = config;
            _context = context;
        }

        public string CreateToken(UserEntity user)
        {
            var jwt = _jwtSettings.Value;

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwt.DurationInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(UserEntity user)
        {
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _context.Users.Update(user);
            _context.SaveChanges();

            return refreshToken;
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!)),
                ValidateLifetime = false // allow expired token
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (validatedToken is JwtSecurityToken jwt && jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                {
                    return principal;
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        public async Task StorePasswordResetTokenAsync(string email, string token, TimeSpan validFor)
        {
            var expiry = DateTime.UtcNow.Add(validFor);
            var resetToken = new PasswordResetToken
            {
                Email = email,
                Token = token,
                Expiry = expiry
            };

            _context.PasswordResetTokens.Add(resetToken);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidatePasswordResetTokenAsync(UserEntity user, string token)
        {
            var resetToken = await _context.PasswordResetTokens
                .FirstOrDefaultAsync(t => t.Token == token);

            if (resetToken == null)
                return false;

            if (resetToken.Email != user.Email || resetToken.Expiry <= DateTime.UtcNow)
                return false;

            _context.PasswordResetTokens.Remove(resetToken); // Invalidate after use
            await _context.SaveChangesAsync();

            return true;
        }

        public bool ValidateRefreshToken(UserEntity user, string refreshToken)
        {
            return user.RefreshToken == refreshToken && user.RefreshTokenExpiryTime > DateTime.UtcNow;
        }
    }
}





