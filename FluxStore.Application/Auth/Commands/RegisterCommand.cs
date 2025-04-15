using FluxStore.Application.DTOs.Auth;
using MediatR;

namespace FluxStore.Application.Auth.Commands
{
    public record RegisterCommand(RegisterRequest Request) : IRequest<AuthResponse>;
}

