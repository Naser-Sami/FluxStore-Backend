using FluxStore.Application.Commands.Products.Queries;
using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Product;
using FluxStore.Application.Interfaces;
using FluxStore.Domain.Entities;
using MediatR;

namespace FluxStore.Application.Products.Handlers
{
	public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
	{
		private readonly IProductRepository _productRepository;
		public GetProductByIdQueryHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
                return Result<ProductDto>.Failure("Product not found");


            var dto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description ?? "",
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl ?? "",
                CategoryId = product.CategoryId,
                CreatedAt = product.CreatedAt
            };

            return Result<ProductDto>.Success(dto);
        }
    }
}

