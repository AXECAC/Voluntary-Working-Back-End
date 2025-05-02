using Microsoft.EntityFrameworkCore;
using Context;
using DataBase;
using Services.Caching;
namespace Services;

// Класс StudentRequestServices
public class StudentRequestServices : IStudentRequestServices
{
    private readonly IRequestRepository _RequestRepository;
    private readonly IRespondedPeopleRepository _RespondedPeopleRepository;
    private readonly ICachingServices<PublicRequest> _CachingServices;
    private readonly IUserServices _UserServices;

    public StudentRequestServices(IRequestRepository requestRepository, ICachingServices<PublicRequest> cachingServices,
            IUserServices userServices, IRespondedPeopleRepository respondedPeopleRepository)
    {
        _RequestRepository = requestRepository;
        _RespondedPeopleRepository = respondedPeopleRepository;
        _CachingServices = cachingServices;
        _UserServices = userServices;
    }

    // Получить все Requests
    public async Task<IBaseResponse<IEnumerable<PublicRequest>>> GetRequests()
    {
        BaseResponse<IEnumerable<PublicRequest>> response;

        var respondedPeople = await _RespondedPeopleRepository.GetAll();

        List<PublicRequest> requests;

        // Если есть откликнувшеся люди
        if (respondedPeople != null && respondedPeople.Count > 0)
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Select(request => new PublicRequest(request, respondedPeople))
                .ToListAsync();
        }
        else
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Select(request => new PublicRequest(request))
                .ToListAsync();
        }

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

        // Ищем откликнувшихся на запрос
        var respondedPeople = await _RespondedPeopleRepository
            .Where(rp => rp.RequestId == requestId)
            .ToListAsync();

        // Если запроса нет
        if (request == null)
        {
            // NotFound (404)
            response = BaseResponse.NotFound("Request not found");
            return response;
        }

        // Нашли запрос
        if (respondedPeople.Count < request.NeededPeopleNumber)
        {
            // Получаем Id откликнувшегося студента
            int myId = _UserServices.GetMyId();

            // Создаем новый RespondedPeople
            RespondedPeople newRP = new RespondedPeople();

            newRP.RequestId = requestId;
            newRP.UserId = myId;

            // Добавляем студента к RespondedPeople
            await _RespondedPeopleRepository.Create(newRP);
            // NoContent (204)
            response = BaseResponse.NoContent("Successed");
            return response;
        }

        // BadRequest (400)
        response = BaseResponse.BadRequest("No more places");
        return response;
    }
}
