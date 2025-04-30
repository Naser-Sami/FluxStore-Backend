using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Product;
using FluxStore.Application.Interfaces;
using FluxStore.Application.Products.Mappers;
using MediatR;

namespace FluxStore.Application.Products.Handlers
{
	public class CreateProductCommandHandler :
        IRequestHandler<CreateProductCommand, Result<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;


        public CreateProductCommandHandler(IProductRepository productRepository
            ,IFileService fileService)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task<Result<ProductDto>> Handle(CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
                AvailableColors = request.AvailableColors,
                AvailableSizes = request.AvailableSizes,
                CreatedAt = DateTime.UtcNow
            };

            if (request.ImageUrl != null)
            {
                var mainImageUrl = await _fileService.UploadImageAsync(request.ImageUrl);
                product.ImageUrl = mainImageUrl.Replace("api/", "");
            }

            if (request.AdditionalImages != null && request.AdditionalImages.Any())
            {
                foreach (var image in request.AdditionalImages)
                {
                    var additionalImageUrl = await _fileService.UploadImageAsync(image);
                    product.AdditionalImages.Add(additionalImageUrl.Replace("api/", ""));
                }
            }

            await _productRepository.CreateAsync(product);

            return Result<ProductDto>.Success(product.ToDto());
        }
    }
}

