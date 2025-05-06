using FluxStore.Application.Cart.Commands;
using FluxStore.Application.Cart.Interface;
using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Cart.Handlers
{
    public class UpdateCartItemHandler : IRequestHandler<UpdateCartItemCommand, Result>
    {
        private readonly ICartRepository _cartRepo;

        public UpdateCartItemHandler(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task<Result> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepo.GetByUserIdAsync(request.UserId);
            if (cart == null) return Result.Failure("Cart not found");

            var item = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (item == null) return Result.Failure("Item not found");

            item.Quantity = request.Quantity;
            _cartRepo.Update(cart);
            await _cartRepo.SaveChangesAsync();

            return Result.Success("Item updated");
        }
    }
}

