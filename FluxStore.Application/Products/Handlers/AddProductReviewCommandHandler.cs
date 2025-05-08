using FluxStore.Application.Common;
using FluxStore.Application.Interfaces;
using FluxStore.Application.Products.Commands;
using FluxStore.Application.Products.DTOs;
using FluxStore.Domain.Entities;
using FluxStore.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FluxStore.Application.Products.Handlers
{
    public class AddProductReviewCommandHandler : IRequestHandler<AddProductReviewCommand, Result<ProductReviewDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;

        public AddProductReviewCommandHandler(IApplicationDbContext context,
            IUserRepository userRepository, IFileService fileService)
        {
            _context = context;
            _userRepository = userRepository;
            _fileService = fileService;
        }

        public async Task<Result<ProductReviewDto>> Handle(AddProductReviewCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetCurrentUserAsync();
            if (user is null) return Result<ProductReviewDto>.Failure("User not found");

            var product = await _context.Products
                .Include(p => p.Reviews)
                .Include(p => p.Ratings)
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product is null) return Result<ProductReviewDto>.Failure("Product not found");

            var review = new ProductReview
            {
                ProductId = product.Id,
                ReviewerName = $"{user.FirstName?.Trim()} {user.LastName?.Trim()}".Trim(),
                ReviewerImage = user.ImageUrl,
                Description = request.Description,
                Rating = request.Rating,
                Date = DateTime.UtcNow,
                Images = new List<string>()
            };

            if (request.Images != null && request.Images.Any())
            {
                foreach (var image in request.Images)
                {
                    var uploadedPath = await _fileService.UploadImageAsync(image);
                    review.Images.Add(uploadedPath.Replace("api/", ""));
                }
            }

            var rating = new ProductRating
            {
                ProductId = product.Id,
                Rating = request.Rating
            };

            product.Reviews.Add(review);
            product.Ratings.Add(rating);

            await _context.SaveChangesAsync(cancellationToken);

            var reviewDto = new ProductReviewDto
            {
                ReviewerName = review.ReviewerName,
                ReviewerImage = review.ReviewerImage ?? "",
                ProductId = review.ProductId,
                Rating = review.Rating,
                Description = review.Description,
                Images = review.Images,
                Date = review.Date
            };

            return Result<ProductReviewDto>.Success(reviewDto);
        }
    }
}