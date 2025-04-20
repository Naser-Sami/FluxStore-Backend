using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Products.Commands
{
    public class AddProductReviewCommand : IRequest<Result>
    {
        public Guid ProductId { get; set; }
        public int Rating { get; set; }  // 1-5
        public string Description { get; set; } = string.Empty;
        public List<string>? Images { get; set; }
    }
}

