using FluxStore.Application.Cart.DTOs;
using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Cart.Commands
{
    public class AddToCartCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? SelectedColor { get; set; }
        public string? SelectedSize { get; set; }
    }
}

