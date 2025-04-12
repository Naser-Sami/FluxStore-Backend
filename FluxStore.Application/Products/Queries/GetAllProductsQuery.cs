using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Category;
using FluxStore.Application.DTOs.Product;
using MediatR;

namespace FluxStore.Application.Commands.Products.Queries
{
    public class GetAllProductsQuery : IRequest<Result<List<ProductDto>>> { }
}

