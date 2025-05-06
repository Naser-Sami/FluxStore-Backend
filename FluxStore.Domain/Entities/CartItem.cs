namespace FluxStore.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public Guid CartId { get; set; }
        public CartEntity Cart { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public string? SelectedColor { get; set; }
        public string? SelectedSize { get; set; }
        public double UnitPrice { get; set; }
    }
}

