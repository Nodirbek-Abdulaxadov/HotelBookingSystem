using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile? imageFile);
        Task RemoveImageAsync(string? imagePath);
    }
}
