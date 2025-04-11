using FluxStore.Application.Common;
using FluxStore.Application.User.Command;
using FluxStore.Domain.Repositories;
using MediatR;

namespace FluxStore.Application.User.Handlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserProfileCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetCurrentUserAsync();

            if (user is null)
                return Result.Failure("User not found");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Gender = request.Gender;
            user.PhoneNumber = request.Phone;
            user.ImageUrl = request.ImageUrl;
            user.Address = request.Address;

            await _userRepository.UpdateAsync(user);

            return Result.Success("Profile updated successfully");
        }
    }
}

