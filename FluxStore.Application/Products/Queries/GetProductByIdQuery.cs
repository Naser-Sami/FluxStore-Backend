using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Product;
using MediatR;

namespace FluxStore.Application.Commands.Products.Queries
{
	public class GetProductByIdQuery : IRequest<Result<ProductDto>>
	{
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

