namespace FluxStore.Application.DTOs.User
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Gender { get; set; }
        public string? PhoneNubmer { get; set; }
        public string? ImageUrl { get; set; }
        public string? Address { get; set; }
    }
}

