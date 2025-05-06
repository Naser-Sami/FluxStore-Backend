namespace FluxStore.Application.Cart.DTOs
{
    public class CartDto
    {
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public double SubTotal { get; set; }
        public double ShippingCost { get; set; }
        public double Total => SubTotal + ShippingCost;
    }
}

