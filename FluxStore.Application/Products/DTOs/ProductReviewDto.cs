namespace FluxStore.Application.Products.DTOs
{
    public class ProductReviewDto
    {
        public string ReviewerName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? ImageUrl { get; set; }
    }
}

