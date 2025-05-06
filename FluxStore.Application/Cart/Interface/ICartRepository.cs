using FluxStore.Domain.Entities;

namespace FluxStore.Application.Cart.Interface
{
    public interface ICartRepository
    {
        Task<CartEntity?> GetByUserIdAsync(Guid userId);
        Task AddAsync(CartEntity cart);
        void Update(CartEntity cart);
        Task RemoveItemAsync(Guid userId, Guid productId);
        Task SaveChangesAsync();
    }
}

