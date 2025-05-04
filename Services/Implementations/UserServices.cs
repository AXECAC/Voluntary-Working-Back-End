using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using DataBase;
using Context;
namespace Services;

// Класс UserServices
public class UserServices : IUserServices
{
    private readonly IUserRepository _UserRepository;
    private readonly IHttpContextAccessor _HttpContextAccessor;

    public UserServices(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _UserRepository = userRepository;
    }

    public int GetMyId()
    {
        return Convert.ToInt32(_HttpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);
    }


    public async Task<IBaseResponse> CheckIdsValid(List<int> ids)
    {
        BaseResponse response;

        User tryFind;
        for (int i = 0; i < ids.Count; i++)
        {
            tryFind = await _UserRepository.FirstOrDefaultAsync(x => x.Id == ids[i]);
            if (tryFind == null)
            {
                response = BaseResponse.NotFound("User id not found");
                return response;
            }
        }

        response = BaseResponse.NoContent();
        return response;
    }
}
