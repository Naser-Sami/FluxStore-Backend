using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Products.Queries
{
    public record GetProductDetailsQuery(Guid Id) : IRequest<Result<ProductDetailsDto>>;
}

