using FluxStore.Application.DTOs.Auth;
using MediatR;

namespace FluxStore.Application.Auth.Commands
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}

