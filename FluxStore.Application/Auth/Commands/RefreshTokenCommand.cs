using FluxStore.Application.DTOs.Auth;
using MediatR;

namespace FluxStore.Application.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<AuthResponse>
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}

