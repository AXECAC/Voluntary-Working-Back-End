using Microsoft.AspNetCore.Http;
using DataBase;
using Context;
namespace Services;

// Класс UserServices
public class DevUserServices : IDevUserServices
{
    private readonly IUserRepository _UserRepository;

    public DevUserServices(IUserRepository userRepository)
    {
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

    public async Task<IBaseResponse> DemoteToStudent(string userEmail)
    {
        BaseResponse response;

        var user = await _UserRepository.FirstOrDefaultAsync(us => us.Email == userEmail);

        if (user == null)
        {
            response = BaseResponse.NotFound("User not found");
            return response;
        }

        if (user.Role == "Admin")
        {
            user.Role = "Student";
            await _UserRepository.Update(user);
            response = BaseResponse.NoContent();
            return response;
        }

        response = BaseResponse.BadRequest("User is not Admin");
        return response;
    }
}
