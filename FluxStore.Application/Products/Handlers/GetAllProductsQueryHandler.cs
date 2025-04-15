using FluxStore.Application.Commands.Products.Queries;
using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Product;
using FluxStore.Application.Interfaces;
using MediatR;

namespace FluxStore.Application.Products.Handlers
{
	public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<List<ProductDto>>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();


            var result = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description ?? "",
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl ?? "",
                CategoryId = p.CategoryId,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            return Result<List<ProductDto>>.Success(result);
        }
    }
}
