namespace FluxStore.Application.DTOs.Auth
{
	public class AuthResponse
	{
        public string Token { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}