using FluxStore.Application.Cart.DTOs;
using FluxStore.Application.Cart.Interface;
using FluxStore.Application.Cart.Queries;
using FluxStore.Application.Common;
using FluxStore.Application.Interfaces;
using MediatR;

namespace FluxStore.Application.Cart.Handlers
{
	public class GetCartHandler : IRequestHandler<GetCartQuery, Result<CartDto>>
	{
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;

		public GetCartHandler(ICartRepository cartRepo, IProductRepository productRepo)
		{
            _cartRepo = cartRepo;
            _productRepo = productRepo;
		}

        public async Task<Result<CartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepo.GetByUserIdAsync(request.UserId);

            if (cart == null) return Result<CartDto>.Success(new CartDto { Items = new() });

            var items = new List<CartItemDto>();

            foreach (var item in cart.Items)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                if (product == null) continue;

                items.Add(new CartItemDto
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ImageUrl = product?.ImageUrl ?? "",
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                });
            }

            var subtotal = items.Sum(i => i.Total);

            var dto = new CartDto
            {
                Items = items,
                SubTotal = subtotal,
                ShippingCost = cart.ShippingCost
            };

            return Result<CartDto>.Success(dto);
        }
    }
}

