using FluxStore.Application.Commands.Products.Commands;
using FluxStore.Application.Common;
using FluxStore.Application.Interfaces;
using FluxStore.Application.Products.Mappers;
using FluxStore.Domain.Entities;
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
                
            };

            await _productRepository.CreateAsync(product);

            var productDto = product.ToDto();
            return Result.Success(productDto);
        }
    }
}

