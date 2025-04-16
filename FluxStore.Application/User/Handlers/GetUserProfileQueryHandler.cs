using FluxStore.Application.Common;
using FluxStore.Application.DTOs.User;
using FluxStore.Domain.Repositories;
using MediatR;

namespace FluxStore.Application.User.Handlers
{
    public record GetUserProfileQuery : IRequest<Result<UserProfileDto>>;

    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, Result<UserProfileDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserProfileQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetCurrentUserAsync(); // We’ll define this shortly

            if (user is null)
                return Result.Failure<UserProfileDto>("User not found");

            return Result.Success(new UserProfileDto
            {
                Id = user.Id,
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                Email = user.Email,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber ?? "",
                ImageUrl = user.ImageUrl,
                Address = user.Address
            });
        }
    }
}
