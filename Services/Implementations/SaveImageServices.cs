using Microsoft.AspNetCore.Http;
namespace Services;

// Class SaveImageServices
public class SaveImageServices
{
    // Путь до директории с логами каждого Request
    private readonly string ImagesDir = "Images";
    private readonly string TemplateImageFileName = "image";

    public SaveImageServices()
    {
        EnsureDirectoryExist();
    }
    // Создаем директорию, если ее нет
    private void EnsureDirectoryExist()
    {
        if (!Directory.Exists(ImagesDir))
        {
            Directory.CreateDirectory(ImagesDir);
        }
    }

    private string CreatePathToImage(int requestId, string imageExtension)
    {
        // Путь до файла
        string fileName = TemplateImageFileName + requestId.ToString() + Guid.NewGuid().ToString() + imageExtension;
        string pathToImage = Path.Combine(ImagesDir, fileName);

        return pathToImage;
    }

    private string GetImageExtension(IFormFile image)
    {
        var imageExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

        if (!allowedExtensions.Contains(imageExtension))
        {
            return "BadExtension";
        }

        return imageExtension;
    }

    public async Task<string> SaveImage(IFormFile image, int requestId)
    {
        var imageExtension = GetImageExtension(image);

        if (imageExtension == "BadExtension")
        {
            return "Bad Extension Error";
        }

        var pathToImage = CreatePathToImage(requestId, imageExtension);

        using (var stream = new FileStream(pathToImage, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        return pathToImage;
    }
}
