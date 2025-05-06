using FluxStore.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double Price { get; set; }
    public string? ImageUrl { get; set; }
    public int Stock { get; set; }
    public double? Discount { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // Admin-managed
    public List<string> AdditionalImages { get; set; } = new();
    public List<string> AvailableColors { get; set; } = new();
    public List<string> AvailableSizes { get; set; } = new();

    // User-managed
    public List<ProductRating> Ratings { get; set; } = new();
    public List<ProductReview> Reviews { get; set; } = new();
}