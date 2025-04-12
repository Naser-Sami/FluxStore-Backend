using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Commands.Category.Queries
{
    public class CreateCategoryCommand : IRequest<Result>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}