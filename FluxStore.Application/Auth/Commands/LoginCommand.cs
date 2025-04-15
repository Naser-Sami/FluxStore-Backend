using FluxStore.Application.DTOs.Auth;
using MediatR;

namespace FluxStore.Application.Auth.Commands
{
    public record LoginCommand(LoginRequest Request) : IRequest<AuthResponse>;
}

