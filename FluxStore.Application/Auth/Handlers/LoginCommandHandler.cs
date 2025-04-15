using FluxStore.Application.Auth.Commands;
using FluxStore.Application.DTOs.Auth;
using FluxStore.Application.Interfaces;
using MediatR;

namespace FluxStore.Application.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IAuthService _authService;
        public LoginCommandHandler(IAuthService authService) => _authService = authService;

        public Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            => _authService.LoginAsync(request.Request);
    }
}

