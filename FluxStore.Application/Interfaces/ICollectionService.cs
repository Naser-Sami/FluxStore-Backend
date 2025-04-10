using FluxStore.Application.DTOs;

namespace FluxStore.Application.Interfaces
{
	public interface ICollectionService
	{
        Task<List<CollectionDto>> GetAllAsync();
        Task<CollectionDto?> GetByIdAsync(Guid id);
        Task CreateAsync(CollectionDto dto);
        Task DeleteAsync(Guid id);
    }
}

