using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Commands.Category.Queries
{
    public class UpdateCategoryCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

