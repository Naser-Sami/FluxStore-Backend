using FluxStore.Application.Commands.Category.Queries;
using FluxStore.Application.Common;
using FluxStore.Domain.Interfaces;
using MediatR;

namespace FluxStore.Application.Commands.Category.Handlers
{
	public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly ICategoryRepository _repository;

        public UpdateCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);
            if (category == null)
                return Result.Failure("Category not found");

            category.Name = request.Name;
            category.Description = request.Description;
            category.ImageUrl = request.ImageUrl;
            category.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(category);
            return Result.Success("Category updated successfully");
        }
    }
}

