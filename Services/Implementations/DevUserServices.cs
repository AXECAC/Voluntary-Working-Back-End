using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using DataBase;
using Context;
namespace Services;

// Класс UserServices
public class DevUserServices : IDevUserServices
{
    private readonly IUserRepository _UserRepository;
    private readonly IHttpContextAccessor _HttpContextAccessor;

    public DevUserServices(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _UserRepository = userRepository;
    }


    public async Task<IBaseResponse> PromoteToAdmin(string userEmail)
    {
        BaseResponse response;

        var user = await _UserRepository.FirstOrDefaultAsync(us => us.Email == userEmail);

        if (user == null)
        {
            response = BaseResponse.NotFound("User not found");
            return response;
        }

        if (user.Role != "Student")
        {
            response = BaseResponse.BadRequest("User is not Student");
            return response;
        }

        user.Role = "Admin";
        await _UserRepository.Update(user);
        response = BaseResponse.NoContent();
        return response;
    }
}
