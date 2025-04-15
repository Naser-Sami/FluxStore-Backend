namespace FluxStore.Domain.Entities
{
	public class ProductReview
	{
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? ImageUrl { get; set; }

        public Product Product { get; set; } = default!;
    }
}

