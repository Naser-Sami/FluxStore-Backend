namespace FluxStore.Domain.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? ResetPasswordToken { get; set; } // Token for resetting password
        public DateTime? ResetPasswordExpiry { get; set; } // Expiry time for the token
    }
}