using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Category;
using MediatR;

namespace FluxStore.Application.Commands.Category.Queries
{
    public class GetCategoryByIdQuery : IRequest<Result<CategoryDto>>
    {
        public Guid Id { get; set; }

        public GetCategoryByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

