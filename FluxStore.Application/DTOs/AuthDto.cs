
namespace FluxStore.Application.DTOs
{
    public class RegisterDto
    {
        public string Username          { get; set; } = string.Empty;
        public string Email             { get; set; } = string.Empty;
        public string Password          { get; set; } = string.Empty;
        public string ConfirmPassword   { get; set; } = string.Empty;
        public UserRole Role            { get; set; } = UserRole.User;
    }

    public class LoginDto
    {
        public string Email         { get; set; } = string.Empty;
        public string Password      { get; set; } = string.Empty;
    }

    public class RefreshTokenDto
    {
        public string Token         { get; set; } = string.Empty;
        public string RefreshToken  { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public string Username      { get; set; } = string.Empty;
        public string Email         { get; set; } = string.Empty;
        public UserRole Role        { get; set; } = UserRole.User;
        public string Token         { get; set; } = string.Empty;
        public string RefreshToken  { get; set; } = string.Empty;
        public DateTime Expiration  { get; set; }
    }
}