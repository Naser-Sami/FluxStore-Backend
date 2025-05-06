using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Product;
using FluxStore.Application.Products.Helper;
using MediatR;

namespace FluxStore.Application.Commands.Products.Queries
{
    public class GetAllProductsQuery : IRequest<Result<PaginatedList<ProductDto>>>
    {
        public Guid? CategoryId { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public List<string>? Colors { get; set; }
        public List<string>? Sizes { get; set; }
        public double? MinRating { get; set; }
        public bool? HasDiscount { get; set; }
        public string? Search { get; set; }

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Sorting
        public string? SortBy { get; set; } // e.g., "price", "name"
        public bool IsDescending { get; set; } = false;
    }
}

