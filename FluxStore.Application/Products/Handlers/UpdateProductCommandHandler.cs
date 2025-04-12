using FluxStore.Application.Commands.Products.Commands;
using FluxStore.Application.Common;
using FluxStore.Application.Interfaces;
using FluxStore.Domain.Entities;
using MediatR;

namespace FluxStore.Application.Products.Handlers
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
	{
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                Stock = request.Stock,
                CategoryId = request.CategoryId
            };

            await _productRepository.UpdateAsync(product);
            return Result.Success("Product updated successfully");
        }
    }
}

