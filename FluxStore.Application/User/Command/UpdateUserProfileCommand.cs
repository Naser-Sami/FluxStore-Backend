using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.User.Command
{
    public class UpdateUserProfileCommand : IRequest<Result>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string? Address { get; set; }
    }
}

