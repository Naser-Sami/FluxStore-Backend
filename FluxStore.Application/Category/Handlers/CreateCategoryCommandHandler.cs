using FluxStore.Application.Commands.Category.Queries;
using FluxStore.Application.Common;
using FluxStore.Domain.Interfaces;
using MediatR;

namespace FluxStore.Application.Commands.Category.Handlers
{
	public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<string>>
    {
        private readonly ICategoryRepository _repository;

        public CreateCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Domain.Entities.Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                CreatedAt = DateTime.UtcNow
        };

            await _repository.AddAsync(category);
            return Result.Success("Category created successfully");
        }
    }
}

