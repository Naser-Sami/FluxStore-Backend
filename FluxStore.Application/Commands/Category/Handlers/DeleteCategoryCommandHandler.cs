using FluxStore.Application.Commands.Category.Queries;
using FluxStore.Application.Common;
using FluxStore.Domain.Interfaces;
using MediatR;

namespace FluxStore.Application.Commands.Category.Handlers
{
	public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly ICategoryRepository _repository;

        public DeleteCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);
            if (category == null)
                return Result.Failure("Category not found");

            await _repository.DeleteAsync(category);
            return Result.Success("Category deleted successfully");
        }
    }
}

