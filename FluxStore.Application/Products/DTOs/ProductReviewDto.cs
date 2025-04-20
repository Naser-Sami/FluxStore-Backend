namespace FluxStore.Application.Products.DTOs
{
    public class ProductReviewDto
    {
        public string ReviewerName { get; set; } = string.Empty;
        public string? ReviewerImage { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public List<string>? Images { get; set; }
    }
}

