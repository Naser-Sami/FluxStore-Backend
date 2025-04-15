using FluxStore.Domain.Common;

namespace FluxStore.Domain.Entities
{
	public class UserEntity : BaseEntity
	{
        public required string Username { get; set; }
        public required string Email { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public required string PasswordHash { get; set; }

        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }

        public string Role { get; set; } = "Customer";

        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
