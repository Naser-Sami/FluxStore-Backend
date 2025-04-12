using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Commands.Products.Commands
{
	public class CreateProductCommand : IRequest<Result>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
    }
}

