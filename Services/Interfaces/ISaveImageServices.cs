using Microsoft.AspNetCore.Http;
namespace Services;

// Interface ISaveImageServices
public interface ISaveImageServices
{
    Task<string> SaveImage(IFormFile image, int requestId);
}
