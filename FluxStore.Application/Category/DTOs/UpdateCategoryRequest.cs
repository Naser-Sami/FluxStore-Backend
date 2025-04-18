﻿namespace FluxStore.Application.DTOs.Category
{
	public class UpdateCategoryRequest
	{
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}

