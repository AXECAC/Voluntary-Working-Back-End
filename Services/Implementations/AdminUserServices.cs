using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace Services;

// Class AdminUserServices
public class AdminUserServices : IAdminUserServices
{
    private readonly IHttpContextAccessor _HttpContextAccessor;

    public AdminUserServices(IHttpContextAccessor httpContextAccessor)
    {
        _HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public int GetAdminId()
    {
        return Convert.ToInt32(_HttpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);
    }
}
