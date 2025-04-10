using FluxStore.Application.DTOs;
using FluxStore.Application.Interfaces;
using FluxStore.Domain.Entities;
using FluxStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FluxStore.Infrastructure.Services
{
	public class CollectionService : ICollectionService
    {
		private readonly ApplicationDbContext _context;

		public CollectionService(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task CreateAsync(CollectionDto dto)
        {
            var collection = new Collection
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                BannerImageUrl = dto.BannerImageUrl
            };

            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var collection = await _context.Collections.FindAsync(id);
            if (collection != null)
            {
                _context.Collections.Remove(collection);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CollectionDto>> GetAllAsync()
        {
            return await _context.Collections
            .Select(c => new CollectionDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                BannerImageUrl = c.BannerImageUrl
            })
            .ToListAsync();
        }

        public async Task<CollectionDto?> GetByIdAsync(Guid id)
        {
            var collection =  await _context.Collections.FindAsync(id);
            if (collection == null) return null;

            return new CollectionDto
            {
                Id = collection.Id,
                Title = collection.Title,
                Description = collection.Description,
                BannerImageUrl = collection.BannerImageUrl
            };
        }
    }
}

