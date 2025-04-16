using Microsoft.AspNetCore.Http;

namespace FluxStore.Application.Interfaces
{
	public interface IFileService
	{
        Task<string> UploadImageAsync(IFormFile file);
    }
}
