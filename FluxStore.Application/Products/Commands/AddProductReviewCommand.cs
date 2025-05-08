using FluxStore.Application.Common;
using FluxStore.Application.Products.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxStore.Application.Products.Commands
{
    public class AddProductReviewCommand : IRequest<Result<ProductReviewDto>>
    {
        [SwaggerSchema("Product id")]
        public Guid ProductId { get; set; }

        [SwaggerSchema("Rating")]
        public double Rating { get; set; }

        [SwaggerSchema("Description")]
        public string Description { get; set; } = string.Empty;

        [SwaggerSchema("Review images")]
        public List<IFormFile>? Images { get; set; }
    }
}

