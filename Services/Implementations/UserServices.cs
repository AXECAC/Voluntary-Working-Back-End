using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using DataBase;
using Context;
namespace Services;

// Класс UserServices
public class UserServices : IUserServices
{
    private readonly IUserRepository _UserRepository;
    private readonly IRequestRepository _RequestRepository;
    private readonly IHashingServices _HashingServices;
    private readonly IRespondedPeopleRepository _RespondedPeopleRepository;
    private readonly IHttpContextAccessor _HttpContextAccessor;

    public UserServices(IHttpContextAccessor httpContextAccessor, IHashingServices hashingServices,
            IUserRepository userRepository, IRequestRepository requestRepository,
            IRespondedPeopleRepository respondedPeopleRepository)
    {
        _HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _HashingServices = hashingServices;
        _UserRepository = userRepository;
        _RequestRepository = requestRepository;
        _RespondedPeopleRepository = respondedPeopleRepository;
    }

    public int GetMyId()
    {
        return Convert.ToInt32(_HttpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);
    }

    public string GetMyRole()
    {
        return _HttpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.Role).Value;
    }

    public async Task<IBaseResponse> CheckIdsValid(List<int> ids)
    {
        BaseResponse response;

        User tryFind;
        for (int i = 0; i < ids.Count; i++)
        {
            tryFind = await _UserRepository.FirstOrDefaultAsync(x => x.Id == ids[i]);
            // Если не нашли хотя бы один Id из списка
            if (tryFind == null)
            {
                response = BaseResponse.NotFound("User id not found");
                return response;
            }
        }

        response = BaseResponse.NoContent();
        return response;
    }

    public async Task<IBaseResponse<User>> GetMyProfile()
    {
        BaseResponse<User> response;

        int myId = GetMyId();

        var user = await _UserRepository.FirstOrDefaultAsync(us => us.Id == myId);

        if (user == null)
        {
            response = BaseResponse<User>.NotFound("User not found");
            return response;
        }

        response = BaseResponse<User>.Ok(user);
        return response;
    }

    private async Task<List<CurrentRequest>> GetCurrentRequests(List<RespondedPeople> myResponse)
    {
        List<CurrentRequest> currentRequests = new List<CurrentRequest>();
        for (int i = 0; i < myResponse.Count; ++i)
        {
            Request request = await _RequestRepository.FirstOrDefaultAsync(req => req.Id == myResponse[i].RequestId);

            CurrentRequest currentRequest = new CurrentRequest(request);

            currentRequests.Add(currentRequest);
        }
        return currentRequests;
    }

    // Получить Requsts, на которые я записался
    public async Task<IBaseResponse<List<CurrentRequest>>> GetMyCurrentRequests()
    {
        BaseResponse<List<CurrentRequest>> response;

        int myId = GetMyId();

        // Ищем все запросы на которые откликнулся User
        var myResponse = await _RespondedPeopleRepository
            .GetQueryable()
            .Where(rp => rp.UserId == myId)
            .ToListAsync();

        List<CurrentRequest> currentRequests = new List<CurrentRequest>();
        if (myResponse != null && myResponse.Count > 0)
        {
            currentRequests = await GetCurrentRequests(myResponse);

            if (currentRequests == null || currentRequests.Count == 0)
            {
                response = BaseResponse<List<CurrentRequest>>.NotFound();
                return response;
            }
            else
            {
                response = BaseResponse<List<CurrentRequest>>.Ok(currentRequests);
                return response;
            }
        }
        response = BaseResponse<List<CurrentRequest>>.NotFound();
        return response;
    }

    // Поменять пароль
    public async Task<IBaseResponse> ChangeMyPassword(string newPassword)
    {
        BaseResponse response;

        int myId = GetMyId();

        var user = await _UserRepository.FirstOrDefaultAsync(us => us.Id == myId);

        if (user == null)
        {
            response = BaseResponse.NotFound("User not found");
            return response;
        }

        user.Password = newPassword;
        _HashingServices.Hashing(user);

        await _UserRepository.Update(user);

        response = BaseResponse.NoContent();
        return response;
    }

}
