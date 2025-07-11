using Microsoft.EntityFrameworkCore;
using Context;
using DataBase;
namespace Services;

// Класс StudentRequestServices
public class StudentRequestServices : IStudentRequestServices
{
    private readonly IRequestRepository _RequestRepository;
    private readonly IRespondedPeopleRepository _RespondedPeopleRepository;
    private readonly IUserServices _UserServices;

    public StudentRequestServices(IRequestRepository requestRepository, IUserServices userServices,
            IRespondedPeopleRepository respondedPeopleRepository)
    {
        _RequestRepository = requestRepository;
        _RespondedPeopleRepository = respondedPeopleRepository;
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
    public async Task<IBaseResponse> AssignMe(int requestId)
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

        // Ищем откликнувшихся на запрос
        var respondedPeople = await _RespondedPeopleRepository
            .Where(rp => rp.RequestId == requestId)
            .ToListAsync();

        // Если остались места
        if (respondedPeople.Count < request.NeededPeopleNumber)
        {
            // Получаем Id откликнувшегося студента из токена
            int myId = _UserServices.GetMyId();

            // Если студент уже откликался
            if (respondedPeople.Where(rp => rp.UserId == myId).Any())
            {
                // BadRequest (400)
                response = BaseResponse.BadRequest("Already added");
                return response;
            }

            // Создаем новый RespondedPeople
            RespondedPeople newRP = new RespondedPeople();

            newRP.RequestId = requestId;
            newRP.UserId = myId;

            // Добавляем студента к откликнувшимся
            await _RespondedPeopleRepository.Create(newRP);
            // NoContent (204)
            response = BaseResponse.NoContent("Successed");
            return response;
        }

        // BadRequest (400)
        response = BaseResponse.BadRequest("No more places");
        return response;
    }

    public async Task<IBaseResponse> UnassignMe(int requestId)
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

        // Ищем откликнувшихся на запрос
        var respondedPeople = await _RespondedPeopleRepository
            .Where(rp => rp.RequestId == requestId)
            .ToListAsync();


        // Нашли запрос
        // Получаем Id откликнувшегося студента
        int myId = _UserServices.GetMyId();

        // Ищем отклик студента
        RespondedPeople deleteRP = respondedPeople
            .FirstOrDefault(rp => rp.UserId == myId && rp.RequestId == request.Id);

        // Если студент уже откликался
        if (deleteRP != null)
        {
            // Удаляем студента из RespondedPeople
            await _RespondedPeopleRepository.Delete(deleteRP);
            // NoContent (204)
            response = BaseResponse.NoContent("Successed");
            return response;
        }

        // BadRequest (400)
        response = BaseResponse.BadRequest("Was not assigned");
        return response;
    }
}
