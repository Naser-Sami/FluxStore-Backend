using FluxStore.Domain.Entities;

namespace FluxStore.Application.Common.Interfaces
{
	public interface ITokenService
	{
        string GenerateToken(UserEntity user);
        string GeneratePasswordResetToken(UserEntity user);
        bool ValidatePasswordResetToken(UserEntity user, string token);
        void StorePasswordResetToken(string email, string token, TimeSpan validFor);
    }
}

