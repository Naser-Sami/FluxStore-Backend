using FluxStore.Application.DTOs.Auth;

namespace FluxStore.Application.Interfaces
{
	public interface IAuthService
	{
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}

