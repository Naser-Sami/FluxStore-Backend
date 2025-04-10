using FluxStore.Application.DTOs;
using FluxStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FluxStore.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var categories = await _categoryService.GetAllCategoriesAsync();
			return Ok(categories);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var category = await _categoryService.GetCategoryByIdAsync(id);
			return category is null ? NotFound() : Ok(category);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CategoryDto dto)
		{
			await _categoryService.AddCategoryAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			await _categoryService.DeleteCategoryAsync(id);
			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAll()
		{
			await _categoryService.DeleteAllCategoriesAsync();
			return NoContent();
		}
	}
}

