using FluxStore.Application.DTOs.Profile;
using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Profile.Queries;

public record GetProfileQuery(Guid UserId) : IRequest<Result<UserProfileDto>>;