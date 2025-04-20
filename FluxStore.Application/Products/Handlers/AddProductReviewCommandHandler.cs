using FluxStore.Application.Common;
using FluxStore.Application.DTOs.User;
using FluxStore.Application.Interfaces;
using FluxStore.Application.Products.Commands;
using FluxStore.Domain.Entities;
using FluxStore.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FluxStore.Application.Products.Handlers
{
	public class AddProductReviewCommandHandler : IRequestHandler<AddProductReviewCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public AddProductReviewCommandHandler(IApplicationDbContext context,
            IUserRepository userRepository)
		{
            _context = context;
            _userRepository = userRepository;
		}

        public async Task<Result> Handle(AddProductReviewCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetCurrentUserAsync();


            if (user is null)
                return Result.Failure<UserProfileDto>("User not found");

            var product = await _context.Products
                .Include(p => p.Reviews)
                .Include(p => p.Ratings)
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
                return Result.Failure("Product not found");


            var review = new ProductReview
            {
                ProductId = product.Id,
                ReviewerName = $"{user.FirstName?.Trim()} {user.LastName?.Trim()}".Trim(),
                ReviewerImage = user.ImageUrl,
                Description = request.Description,
                Images = request.Images,
                Rating = request.Rating,
                Date = DateTime.UtcNow
            };

            var rating = new ProductRating
            {
                ProductId = product.Id,
                Rating = request.Rating
            };

            product.Reviews.Add(review);
            product.Ratings.Add(rating);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Review added");
        }
    }
}

