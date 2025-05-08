namespace FluxStore.Domain.Entities
{
    public class ProductReview
    {
        public Guid Id { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public string? ReviewerImage { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public double Rating { get; set; }
        public List<string>? Images { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;
    }
}

