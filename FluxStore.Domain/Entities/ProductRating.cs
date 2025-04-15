namespace FluxStore.Domain.Entities
{
	public class ProductRating
	{
        public Guid Id { get; set; }
        public int Rating { get; set; } // From 1 to 5
        public Guid ProductId { get; set; }

        public Product Product { get; set; } = default!;
    }
}

