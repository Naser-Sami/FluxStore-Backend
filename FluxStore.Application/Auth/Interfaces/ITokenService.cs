using System.Security.Claims;
using FluxStore.Domain.Entities;

namespace FluxStore.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UserEntity user);
        string GenerateRefreshToken(UserEntity user);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
        bool ValidateRefreshToken(UserEntity user, string refreshToken);
        bool ValidatePasswordResetToken(UserEntity user, string token);
        void StorePasswordResetToken(string email, string token, TimeSpan validFor);
    }
}

