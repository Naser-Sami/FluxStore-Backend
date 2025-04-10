namespace FluxStore.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public required Category Category { get; set; }

        public ICollection<ProductCollection> ProductCollections { get; set; } = new List<ProductCollection>();
    }
}

