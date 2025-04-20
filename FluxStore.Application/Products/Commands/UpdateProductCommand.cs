using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Commands.Products.Commands
{
	public class UpdateProductCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }

        public List<string> AdditionalImages { get; set; } = new();
        public List<string> AvailableColors { get; set; } = new();
        public List<string> AvailableSizes { get; set; } = new();
    }
}

