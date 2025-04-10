namespace FluxStore.Application.DTOs
{
    public class CollectionDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string BannerImageUrl { get; set; } = string.Empty;
    }
}

