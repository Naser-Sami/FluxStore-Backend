using FluxStore.Application.Commands.Products.Commands;
using FluxStore.Application.Commands.Products.Queries;
using FluxStore.Application.Interfaces;
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
        private readonly IFileService _fileService;
        private readonly IProductRepository _productRepository;

        public ProductsController(IMediator mediator, IFileService fileService,
            IProductRepository productRepository)
		{
			_mediator = mediator;
			_fileService = fileService;
			_productRepository = productRepository;
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

		[HttpPost("update-product-image")]
		public async Task<IActionResult> ProductImage(IFormFile? image, Guid id)
		{
			try
			{
				if (image == null || image.Length == 0)
					return BadRequest("Product image is required.");

				var imageUrl = await _fileService.UploadImageAsync(image);

				var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
					return NotFound("Product not found.");

				if (product == null)
                    return NotFound("No data found.");

                product.ImageUrl = imageUrl.Replace("api/", "");
                await _productRepository.UpdateAsync(product);

                return Ok(new { imageUrl });
            }
			catch (Exception e)
			{
				return BadRequest(new { error = e.Message });
			}
		}

        [HttpPost("update-product-details-images")]
        public async Task<IActionResult> ProductDetailsImages(List<IFormFile>? images, Guid id)
        {
            try
            {
                List<string> imageUrl = new List<string>();

                if (images == null || images.Count == 0)
                    return BadRequest("Product image is required.");

                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                    return NotFound("Product not found.");

                if (product == null)
                    return NotFound("No data found.");

                product.AdditionalImages.Clear();

                foreach (var image in images)
                {
                    imageUrl.Add(await _fileService.UploadImageAsync(image));
                }

                foreach (var image in imageUrl)
                {
                    product.AdditionalImages.Add(image.Replace("api/", ""));
                }

                await _productRepository.UpdateAsync(product);

                return Ok("Products Images added succesfully.");
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
