using FluxStore.Application.Cart.Commands;
using FluxStore.Application.Cart.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FluxStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
	{
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ✅ Add to Cart
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Message);
        }

        // ✅ Remove from Cart
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Message);
        }

        // ✅ Update Cart Item Quantity
        [HttpPut("update")]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateCartItemCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Message);
        }

        // ✅ Get User Cart
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetCart(Guid userId)
        {
            var result = await _mediator.Send(new GetCartQuery { UserId = userId });
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }
    }
}

