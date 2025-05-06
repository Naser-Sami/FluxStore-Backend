using FluxStore.Application.Cart.DTOs;
using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Cart.Queries
{
	public class GetCartQuery : IRequest<Result<CartDto>>
	{
		public Guid UserId { get; set; }
	}
}

