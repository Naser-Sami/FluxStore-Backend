using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Profile;
using MediatR;

namespace FluxStore.Application.User.Queries;

public record GetProfileQuery(Guid UserId) : IRequest<Result<UserProfileDto>>;