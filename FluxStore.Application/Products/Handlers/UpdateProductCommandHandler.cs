using FluxStore.Application.Commands.Products.Commands;
using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Product;
using FluxStore.Application.Interfaces;
using FluxStore.Application.Products.Mappers;
using MediatR;

namespace FluxStore.Application.Products.Handlers
{
	public class UpdateProductCommandHandler :
        IRequestHandler<UpdateProductCommand, Result<ProductDto>>
	{
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;

        public UpdateProductCommandHandler(IProductRepository productRepository
            ,IFileService fileService)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByIdAsync(request.Id);

            if (existingProduct == null)
            {
                return Result<ProductDto>.Failure("Product not found.");
            }

            // Update basic fields
            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;
            existingProduct.Stock = request.Stock;
            existingProduct.CategoryId = request.CategoryId;
            existingProduct.AvailableColors = request.AvailableColors;
            existingProduct.AvailableSizes = request.AvailableSizes;

            // Handle main image
            if (request.ImageUrl != null)
            {
                var mainImageUrl = await _fileService.UploadImageAsync(request.ImageUrl);
                existingProduct.ImageUrl = mainImageUrl.Replace("api/", "");
            }
            // else: keep the existingProduct.ImageUrl as-is

            // Handle additional images
            if (request.AdditionalImages != null && request.AdditionalImages.Any())
            {
                existingProduct.AdditionalImages.Clear(); // optionally clear existing if you want to replace
                foreach (var image in request.AdditionalImages)
                {
                    var additionalImageUrl = await _fileService.UploadImageAsync(image);
                    existingProduct.AdditionalImages.Add(additionalImageUrl.Replace("api/", ""));
                }
            }
            // else: keep the existingProduct.AdditionalImages as-is

            await _productRepository.UpdateAsync(existingProduct);
            return Result<ProductDto>.Success(existingProduct.ToDto());
        }
    }
}

