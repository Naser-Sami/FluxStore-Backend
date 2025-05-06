using FluxStore.Application.Cart.Commands;
using FluxStore.Application.Cart.Interface;
using FluxStore.Application.Common;
using FluxStore.Application.Interfaces;
using FluxStore.Domain.Entities;
using MediatR;

namespace FluxStore.Application.Cart.Handlers
{
    public class AddToCartHandler : IRequestHandler<AddToCartCommand, Result>
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;

        public AddToCartHandler(ICartRepository cartRepo, IProductRepository productRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }

        public async Task<Result> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepo.GetByIdAsync(request.ProductId);
            if (product == null) return Result.Failure("Product not found");

            var cart = await _cartRepo.GetByUserIdAsync(request.UserId) ?? new CartEntity { UserId = request.UserId };

            var item = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (item != null)
            {
                item.Quantity += request.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    UnitPrice = product.Price,
                    SelectedColor = request.SelectedColor,
                    SelectedSize = request.SelectedSize,
                });
            }

            _cartRepo.Update(cart);
            await _cartRepo.SaveChangesAsync();

            return Result.Success("Item added to cart");
        }
    }
}

