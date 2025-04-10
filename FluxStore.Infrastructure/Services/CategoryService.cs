using System;
using FluxStore.Application.DTOs;
using FluxStore.Application.Interfaces;
using FluxStore.Domain.Entities;
using FluxStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FluxStore.Infrastructure.Services
{
	public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

		public CategoryService(ApplicationDbContext context)
		{
            _context = context;
		}

        public async Task AddCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                IconName = categoryDto.IconName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllCategoriesAsync()
        {
            var allCategories = await _context.Categories.ToListAsync();
            _context.Categories.RemoveRange(allCategories);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is not null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDto {
                    Id = c.Id,
                    Name = c.Name,
                    IconName = c.IconName
                }).ToListAsync();

            return categories;
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(string id)
        {
            var category = await _context.Categories.FindAsync(id);

            return category is null ? null : new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                IconName = category.IconName
            };
        }
    }
}

