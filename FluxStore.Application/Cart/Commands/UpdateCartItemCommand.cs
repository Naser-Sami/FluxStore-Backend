using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Cart.Commands
{
	public class UpdateCartItemCommand : IRequest<Result>
	{
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

