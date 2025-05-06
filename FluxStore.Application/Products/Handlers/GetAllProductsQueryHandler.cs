using FluxStore.Application.Commands.Products.Queries;
using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Product;
using FluxStore.Application.Interfaces;
using FluxStore.Application.Products.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FluxStore.Application.Products.Handlers
{
	public class GetAllProductsQueryHandler
        : IRequestHandler<GetAllProductsQuery, Result<PaginatedList<ProductDto>>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PaginatedList<ProductDto>>> Handle(GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.Products.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(request.Search))
                query = query.Where(p =>
                    p.Name.Contains(request.Search)
                );
            

            if (!string.IsNullOrEmpty(request.CategoryId.ToString()))
                query = query.Where(p => p.CategoryId == request.CategoryId);

            if (request.MinPrice.HasValue)
                query = query.Where(p => p.Price >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= request.MaxPrice.Value);

            if (request.Colors?.Any() == true)
                query = query.Where(p => p.AvailableColors.Any(
                    c => request.Colors.Contains(c)));

            if (request.Sizes?.Any() == true)
                query = query.Where(p => p.AvailableSizes.Any(
                    s => request.Sizes.Contains(s)));

            if (request.HasDiscount == true)
                query = query.Where(p => p.Discount > 0);

            if (request.MinRating.HasValue)
                query = query.Where(
                    p => p.Ratings.Any() && p.Ratings.Average(
                        r => r.Rating) >= request.MinRating.Value);

            // Sorting
            query = request.SortBy?.ToLower() switch
            {
                "price" => request.IsDescending ? query.OrderByDescending(
                    p => p.Price) : query.OrderBy(p => p.Price),
                "name" => request.IsDescending ? query.OrderByDescending(
                    p => p.Name) : query.OrderBy(p => p.Name),
                "created" => request.IsDescending ? query.OrderByDescending(
                    p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt),
                _ => query.OrderByDescending(p => p.CreatedAt) // Default sort
            };

            // Pagination
            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var result = items.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description ?? "",
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl ?? "",
                CategoryId = p.CategoryId,
                CreatedAt = p.CreatedAt
            }).ToList();

            return Result<PaginatedList<ProductDto>>.Success(new PaginatedList<ProductDto>(result, totalCount));
        }
    }
}
