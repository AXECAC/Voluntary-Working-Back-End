using Microsoft.EntityFrameworkCore;
using Context;
using DataBase;
using Services.Caching;
namespace Services;

// Класс StudentRequestServices
public class StudentRequestServices : IStudentRequestServices
{
    private readonly IRequestRepository _RequestRepository;
    private readonly ICachingServices<PublicRequest> _CachingServices;
    private readonly IUserServices _UserServices;

    public StudentRequestServices(IRequestRepository requestRepository, ICachingServices<PublicRequest> cachingServices, IUserServices userServices)
    {
        _RequestRepository = requestRepository;
        _CachingServices = cachingServices;
        _UserServices = userServices;
    }

    // Получить все Requests
    public async Task<IBaseResponse<IEnumerable<PublicRequest>>> GetRequests()
    {
        BaseResponse<IEnumerable<PublicRequest>> response;

        // Ищем в БД
        var requests = await _RequestRepository
            .GetQueryable()
            .Select(request => new PublicRequest(request))
            .ToListAsync();

        // Если не найдено Request
        // NoContent (204)
        if (requests.Count == 0)
        {
            response = BaseResponse<IEnumerable<PublicRequest>>.NoContent("Find 0 requests");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<PublicRequest>>.Ok(requests);

        return response;
    }

    // Добавить Id студента в откликнувшихся на запрос
    public async Task<IBaseResponse> AssigneeMe(int requestId)
    {
        BaseResponse response;

        // Ищем запрос в БД
        var request = await _RequestRepository.FirstOrDefaultAsync(rq => rq.Id == requestId);

        // Если запроса нет
        if (request == null)
        {
            // NotFound (404)
            response = BaseResponse.NotFound("Request not found");
            return response;
        }

        // Нашли запрос
        if (request.RespondedPeople.Count() < request.NeededPeopleNumber)
        {
            // Добавляем студента к Request
            request.RespondedPeople.Append(
                    _UserServices.GetMyId() // Получаем Id откликнувшегося студента
                    );
            // NoContent (204)
            response = BaseResponse.NoContent("Successed");
            return response;
        }

        // BadRequest (400)
        response = BaseResponse.BadRequest("Successed");
        return response;

    }
}
