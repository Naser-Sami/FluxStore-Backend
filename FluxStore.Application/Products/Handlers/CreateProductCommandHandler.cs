using FluxStore.Application.Common;
using FluxStore.Application.Interfaces;
using FluxStore.Application.Products.Mappers;
using MediatR;

namespace FluxStore.Application.Products.Handlers
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
                AdditionalImages = request.AdditionalImages,
                AvailableColors = request.AvailableColors,
                AvailableSizes = request.AvailableSizes,
                CreatedAt = DateTime.UtcNow
            };

            await _productRepository.CreateAsync(product);

            return Result.Success(product.ToDto());
        }
    }
}

