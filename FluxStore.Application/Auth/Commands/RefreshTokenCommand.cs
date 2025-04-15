using FluxStore.Application.Auth.DTOs;
using FluxStore.Application.DTOs.Auth;
using MediatR;

namespace FluxStore.Application.Auth.Commands
{
    public record RefreshTokenCommand(RefreshTokenRequest Request) : IRequest<AuthResponse>;
}

