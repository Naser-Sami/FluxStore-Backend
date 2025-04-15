using FluxStore.Application.Auth.Commands;
using FluxStore.Application.DTOs.Auth;
using FluxStore.Application.Interfaces;
using MediatR;

namespace FluxStore.Application.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IAuthService _authService;
        public RegisterCommandHandler(IAuthService authService) => _authService = authService;

        public Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            => _authService.RegisterAsync(request.Request);
    }
}

