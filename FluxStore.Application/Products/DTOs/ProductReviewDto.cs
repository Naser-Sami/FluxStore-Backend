namespace FluxStore.Application.Products.DTOs
{
    public class ProductReviewDto
    {
        public string ReviewerName { get; set; } = string.Empty;
        public string ReviewerImage { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new();
        public DateTime Date { get; set; }
    }
}

