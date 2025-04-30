using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Product;
using MediatR;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

public class CreateProductCommand : IRequest<Result<ProductDto>>
{
    [SwaggerSchema("Product name")]
    public string Name { get; set; } = string.Empty;

    [SwaggerSchema("Description")]
    public string? Description { get; set; }

    [SwaggerSchema("Price")]
    public decimal Price { get; set; }

    [SwaggerSchema("Main image")]
    public IFormFile? ImageUrl { get; set; }

    [SwaggerSchema("Stock")]
    public int Stock { get; set; }

    [SwaggerSchema("Category ID")]
    public Guid CategoryId { get; set; }

    [SwaggerSchema("Additional images")]
    public List<IFormFile>? AdditionalImages { get; set; } = new();

    [SwaggerSchema("Available colors")]
    public List<string> AvailableColors { get; set; } = new();

    [SwaggerSchema("Available sizes")]
    public List<string> AvailableSizes { get; set; } = new();
}