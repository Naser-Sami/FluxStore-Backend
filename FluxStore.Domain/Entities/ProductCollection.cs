namespace FluxStore.Domain.Entities
{
    public class ProductCollection
    {
        public Guid ProductId { get; set; }
        public required Product Product { get; set; }

        public Guid CollectionId { get; set; }
        public required Collection Collection { get; set; }
    }
}

