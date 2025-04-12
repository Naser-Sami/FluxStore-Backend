using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Commands.Category.Queries
{
    public class DeleteCategoryCommand : IRequest<Result>
    {
        public Guid Id { get; }

        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }
    }
}

