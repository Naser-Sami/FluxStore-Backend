using FluxStore.Application.Common;
using FluxStore.Application.Common.Interfaces;
using FluxStore.Domain.Repositories;
using MediatR;

namespace FluxStore.Application.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public ForgotPasswordCommandHandler(
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return Result.Failure<string>("User not found");

            var token = Guid.NewGuid().ToString(); // Your reset token
            await _tokenService.StorePasswordResetTokenAsync(user.Email, token, TimeSpan.FromHours(1)); // Store for 1 hour

            // TODO: Send email to user.Email with the token

            return Result.Success(token);
        }
    }
}

