using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.User.Command
{
    public record UpdateUserProfileCommand(
        string FirstName,
        string LastName,
        string? Gender,
        string? Phone,
        string? ImageUrl,
        string? Address
    ) : IRequest<Result>;
}

