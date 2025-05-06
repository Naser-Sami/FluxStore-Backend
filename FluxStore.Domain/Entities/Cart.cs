namespace FluxStore.Domain.Entities
{
    public class CartEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartItem> Items { get; set; } = new();
        public double ShippingCost { get; set; } = 0;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

