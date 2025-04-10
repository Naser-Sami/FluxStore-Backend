using FluxStore.Application.DTOs;
using FluxStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FluxStore.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CollectionsController : ControllerBase
	{
		private readonly ICollectionService _collectionService;

		public CollectionsController(ICollectionService collectionService)
		{
			_collectionService = collectionService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var collections = await _collectionService.GetAllAsync();
			return Ok(collections);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var collection = await _collectionService.GetByIdAsync(id);
            return collection is null ? NotFound() : Ok(collection);
        }

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CollectionDto dto)
		{
			await _collectionService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _collectionService.DeleteAsync(id);
			return NoContent();
		}
	}
}

