using FluxStore.Application.Auth.Commands;
using FluxStore.Application.Common.Interfaces;
using FluxStore.Application.DTOs.Auth;
using FluxStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace FluxStore.Application.Auth.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly IApplicationDbContext _context;

        public RefreshTokenCommandHandler(ITokenService tokenService, IApplicationDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
                throw new SecurityTokenException("Access token is required.");

            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                throw new SecurityTokenException("Refresh access token is required.");

            var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token);
            var email = principal?.FindFirstValue(ClaimTypes.Email);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            if (user == null || !_tokenService.ValidateRefreshToken(user, request.RefreshToken))
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            var newToken = _tokenService.CreateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken(user); ;

            return new AuthResponse
            {
                Token = newToken,
                RefreshToken = newRefreshToken,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}

