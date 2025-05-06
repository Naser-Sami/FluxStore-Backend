using FluxStore.Application.Commands.Products.Commands;
using FluxStore.Application.Commands.Products.Queries;
using FluxStore.Application.Products.Commands;
using FluxStore.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluxStore.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
		{
			_mediator = mediator;
		}


        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsQuery query)
        {
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }


        [HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _mediator.Send(new GetProductByIdQuery(id));
			return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
		}

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
		{
            if (id != command.Id)
                return BadRequest("Product ID mismatch");

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _mediator.Send(new DeleteProductCommand(id));
			return result.IsSuccess ? Ok(result.Message) : NotFound(result.Message);
		}

        [HttpGet("{id:guid}/details")]
        public async Task<IActionResult> GetDetails(Guid id)
        {
			var result = await _mediator.Send(new GetProductDetailsQuery(id));
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpPost("add-review")]
        public async Task<IActionResult> AddReview([FromBody] AddProductReviewCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.Message });

            return Ok("Success");
        }
    }
}
