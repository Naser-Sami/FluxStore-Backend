using FluxStore.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FluxStore.Infrastructure.Services
{
	public class FileService : IFileService
	{
		public FileService()
		{
		}

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine("wwwroot", "uploads", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"uploads/{fileName}";
        }
    }
}

