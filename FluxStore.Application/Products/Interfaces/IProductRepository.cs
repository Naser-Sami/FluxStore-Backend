using FluxStore.Domain.Entities;

namespace FluxStore.Application.Interfaces
{
	public interface IProductRepository
	{
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
        Task<Product?> GetWithCategoryByIdAsync(Guid id);
    }
}
