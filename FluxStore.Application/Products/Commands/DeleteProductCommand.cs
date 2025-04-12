using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Commands.Products.Commands
{
	public class DeleteProductCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}

