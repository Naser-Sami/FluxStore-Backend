using FluxStore.Application.Cart.Commands;
using FluxStore.Application.Cart.Interface;
using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Cart.Handlers
{
	public class RemoveFromCartHandler : IRequestHandler<RemoveFromCartCommand, Result>
	{
        private readonly ICartRepository _cartRepository;

		public RemoveFromCartHandler(ICartRepository cartRepository)
		{
            _cartRepository = cartRepository;
		}

        public async Task<Result> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByUserIdAsync(request.UserId);

            if (cart == null) return Result.Failure("Cart not found");

            cart.Items.RemoveAll(i => i.ProductId == request.ProductId);

            _cartRepository.Update(cart);
            await _cartRepository.SaveChangesAsync();

            return Result.Success("Item removed from cart");
        }
    }
}

