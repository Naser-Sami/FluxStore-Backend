using FluxStore.Application.Cart.Interface;
using FluxStore.Domain.Entities;
using FluxStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FluxStore.Infrastructure.Repositories
{
	public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

		public CartRepository(AppDbContext context)
		{
            _context = context;
		}

        public async Task<CartEntity?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Carts
               .Include(c => c.Items)
               .ThenInclude(i => i.Product)
               .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddAsync(CartEntity cart)
        {
            await _context.Carts.AddAsync(cart);
        }

        public async Task RemoveItemAsync(Guid userId, Guid productId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                cart.Items.Remove(item);
                _context.Carts.Update(cart);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(CartEntity cart)
        {
            _context.Carts.Update(cart);
        }
    }
}

