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
        Task<bool> ValidatePasswordResetTokenAsync(UserEntity user, string token);
        Task StorePasswordResetTokenAsync(string email, string token, TimeSpan validFor);
    }
}

