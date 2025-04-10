namespace FluxStore.Domain.Entities
{
    public class Collection
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string BannerImageUrl { get; set; } = string.Empty;

        public ICollection<ProductCollection> ProductCollections { get; set; } = new List<ProductCollection>();
    }
}

