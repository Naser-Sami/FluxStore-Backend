using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluxStore.Application.Common.Interfaces;
using FluxStore.Application.DTOs.Auth;
using FluxStore.Domain.Entities;
using FlxStore.Shared.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FluxStore.Infrastructure.Services
{
	public class TokenService : ITokenService
    {
        private readonly Dictionary<string, (string Email, DateTime Expiry)> _resetTokens = new();
        private readonly IOptions<JwtSettings> _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
		{
            _jwtSettings = jwtSettings;
		}

        public string GeneratePasswordResetToken(UserEntity user)
        {
            var token = Guid.NewGuid().ToString();
            _resetTokens[token] = (user.Email, DateTime.UtcNow.AddMinutes(10)); // token valid for 10 minutes
            return token;
        }

        public string GenerateToken(UserEntity user)
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

        public void StorePasswordResetToken(string email, string token, TimeSpan validFor)
        {
            var expiry = DateTime.UtcNow.Add(validFor);
            _resetTokens[token] = (email, expiry);
        }

        public bool ValidatePasswordResetToken(UserEntity user, string token)
        {
            if (_resetTokens.TryGetValue(token, out var entry))
            {
                if (entry.Email == user.Email && entry.Expiry > DateTime.UtcNow)
                {
                    _resetTokens.Remove(token); // one-time use
                    return true;
                }
            }

            return false;
        }
    }
}

