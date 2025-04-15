
using FluxStore.Application.Common;
using FluxStore.Application.Interfaces;
using FluxStore.Application.Products.DTOs;
using FluxStore.Application.Products.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FluxStore.Application.Products.Handlers
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, Result<ProductDetailsDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetProductDetailsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ProductDetailsDto>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Ratings)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
                return Result<ProductDetailsDto>.Failure("Product not found");

            var avgRating = product.Ratings.Any()
                ? product.Ratings.Average(r => r.Rating)
                : 0;

            var details = new ProductDetailsDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description ?? "",
                Price = product.Price,
                ImageUrl = product.ImageUrl ?? "",
                Stock = product.Stock,
                CategoryName = product.Category.Name,
                AdditionalImages = product.AdditionalImages,
                AvailableColors = product.AvailableColors,
                AvailableSizes = product.AvailableSizes,
                AverageRating = Math.Round(avgRating, 1),
                Reviews = product.Reviews.Select(r => new ProductReviewDto
                {
                    ReviewerName = r.ReviewerName,
                    Text = r.Text,
                    Date = r.Date,
                    ImageUrl = r.ImageUrl
                }).ToList()
            };

            return Result<ProductDetailsDto>.Success(details);
        }
    }
}

