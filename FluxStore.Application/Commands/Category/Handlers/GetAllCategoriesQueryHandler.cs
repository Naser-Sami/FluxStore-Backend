using FluxStore.Application.Commands.Category.Queries;
using FluxStore.Application.Common;
using FluxStore.Application.DTOs.Category;
using FluxStore.Domain.Interfaces;
using MediatR;

namespace FluxStore.Application.Commands.Category.Handlers
{
	public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<CategoryDto>>>
    {
        private readonly ICategoryRepository _repository;

        public GetAllCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync();

            var result = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl
            }).ToList();

            return Result<List<CategoryDto>>.Success(result);
        }
    }
}

