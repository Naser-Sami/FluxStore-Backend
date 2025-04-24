using FluxStore.Application.DTOs.Product;
using FluxStore.Domain.Entities;

namespace FluxStore.Application.Products.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
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
        }

        public static Product ToEntity(this ProductDto dto)
        {
            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description ?? "",
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = dto.ImageUrl ?? "",
                CategoryId = dto.CategoryId,
                CreatedAt = dto.CreatedAt
            };
        }
    }
}