using FluxStore.Application.DTOs;

namespace FluxStore.Application.Interfaces
{
	public interface ICategoryService
	{
        Task AddCategoryAsync(CategoryDto categoryDto);
        Task DeleteAllCategoriesAsync();
        Task DeleteCategoryAsync(string id);
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(string id);
    }
}

