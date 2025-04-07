using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace Services;

// Класс UserServices
public class UserServices : IUserServices
{
    private readonly IHttpContextAccessor _HttpContextAccessor;

    public UserServices(IHttpContextAccessor httpContextAccessor)
    {
        _HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public int GetMyId()
    {
        return Convert.ToInt32(_HttpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);
    }
}
