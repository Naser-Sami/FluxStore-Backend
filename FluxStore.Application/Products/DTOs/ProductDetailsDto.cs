using FluxStore.Application.Products.DTOs;

public class ProductDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int Stock { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public List<string> AdditionalImages { get; set; } = new();
    public List<string> AvailableColors { get; set; } = new();
    public List<string> AvailableSizes { get; set; } = new();

    public double AverageRating { get; set; }
    public List<ProductReviewDto> Reviews { get; set; } = new();
}