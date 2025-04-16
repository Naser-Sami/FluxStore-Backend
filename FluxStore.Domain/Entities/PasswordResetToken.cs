namespace FluxStore.Domain.Entities
{
    public class PasswordResetToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Token { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime Expiry { get; set; }
    }
}

