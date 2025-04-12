using FluxStore.Domain.Entities;

namespace FluxStore.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryEntity>> GetAllAsync();
        Task<CategoryEntity?> GetByIdAsync(Guid id);
        Task AddAsync(CategoryEntity category);
        Task UpdateAsync(CategoryEntity category);
        Task DeleteAsync(CategoryEntity category);
    }
}

