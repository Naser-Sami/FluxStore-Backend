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
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                throw new SecurityTokenException("Refresh token is required.");

            var user = await _context.Users.FirstOrDefaultAsync(
                u => u.RefreshToken == request.RefreshToken &&
                     u.RefreshTokenExpiryTime > DateTime.UtcNow,
                cancellationToken);

            if (user is null)
                throw new SecurityTokenException("Invalid refresh token");

            var newAccessToken = _tokenService.CreateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken(user);

            return new AuthResponse
            {
                UserId = user.Id,
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}

