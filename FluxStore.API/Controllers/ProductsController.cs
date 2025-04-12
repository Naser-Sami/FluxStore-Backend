using FluxStore.Application.Commands.Products.Commands;
using FluxStore.Application.Commands.Products.Queries;
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _mediator.Send(new GetProductByIdQuery(id));
			return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProductCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message); 
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
		{
            if (id != command.Id)
                return BadRequest("Product ID mismatch");

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
        }

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _mediator.Send(new DeleteProductCommand(id));
			return result.IsSuccess ? Ok(result.Message) : NotFound(result.Message);
		}
    }
}

