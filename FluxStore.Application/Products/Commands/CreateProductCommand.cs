using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Product;
using MediatR;

namespace FluxStore.Application.Commands.Products.Commands
{
    public record CreateProductCommand(
        string Name,
        string Description,
        decimal Price,
        int Stock,
        string ImageUrl,
        Guid CategoryId
    ) : IRequest<Result<ProductDto>>;
}

