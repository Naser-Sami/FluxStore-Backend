namespace FluxStore.Application.Cart.DTOs
{
    public class CartItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Total => UnitPrice * Quantity;

        public string? SelectedColor { get; set; }
        public string? SelectedSize { get; set; }
    }
}

