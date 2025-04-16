using FluxStore.Application.DTOs.Auth;
using MediatR;

namespace FluxStore.Application.Auth.Commands
{
    public class RegisterCommand : IRequest<AuthResponse>
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}

