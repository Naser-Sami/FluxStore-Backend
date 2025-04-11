using FluxStore.Application.Common;
using FluxStore.Application.Common.Interfaces;
using FluxStore.Application.DTOs.Profile;
using FluxStore.Domain.Repositories;
using MediatR;

namespace FluxStore.Application.Profile.Queries;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Result<UserProfileDto>>
{
    private readonly IUserRepository _userRepository;

    public GetProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserProfileDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null) return Result.Failure<UserProfileDto>("User not found.");

        var dto = new UserProfileDto
        {
            Id = user.Id,
            FirstName = user.FirstName ?? "",
            LastName = user.LastName ?? "",
            Email = user.Email,
            Gender = user.Gender,
            PhoneNumber = user.PhoneNumber,
            ImageUrl = user.ImageUrl,
            Address = user.Address
        };

        return Result.Success(dto);
    }
}