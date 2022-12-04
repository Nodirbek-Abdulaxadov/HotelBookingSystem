using BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public Task RemoveImageAsync(string? imagePath)
        {
            if (imagePath != null)
            {
                string uplodFolder = _webHostEnvironment.WebRootPath;
                string filePath = Path.Combine(uplodFolder, imagePath);
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
            }

            return Task.CompletedTask;
        }

        public Task<string> SaveImageAsync(IFormFile? imageFile)
        {
            string uniqueName = string.Empty;
            if (imageFile != null)
            {
                string uplodFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uplodFolder, uniqueName);
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                imageFile.CopyTo(fileStream);
                fileStream.Close();
            }

            return Task.FromResult("images/" + uniqueName);
        }
    }
}
