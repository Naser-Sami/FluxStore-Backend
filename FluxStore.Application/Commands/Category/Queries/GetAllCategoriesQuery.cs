using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Category;
using MediatR;

namespace FluxStore.Application.Commands.Category.Queries
{
    public class GetAllCategoriesQuery : IRequest<Result<List<CategoryDto>>> { }
}

