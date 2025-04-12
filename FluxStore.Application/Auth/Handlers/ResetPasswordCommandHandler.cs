

using FluxStore.Application.Common;
using FluxStore.Application.Common.Interfaces;
using FluxStore.Domain.Entities;
using FluxStore.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FluxStore.Application.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<UserEntity> _passwordHasher;
        private readonly ITokenService _tokenService;

        public ResetPasswordCommandHandler(
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<UserEntity>();
            _tokenService = tokenService;
        }

        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.NewPassword != request.ConfirmPassword)
                return Result.Failure("Passwords do not match");

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return Result.Failure("User not found");

            var isValid = _tokenService.ValidatePasswordResetToken(user, request.Token); // we'll handle this
            if (!isValid)
                return Result.Failure("Invalid or expired reset token");

            user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);
            await _userRepository.UpdateAsync(user);

            return Result.Success("Success");
        }
    }
}

