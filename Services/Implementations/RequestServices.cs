using System.Data.Entity;
using Context;
using DataBase;
using Services.Caching;
namespace Services;

// Класс RequestServices
// public class RequestServices : IRequestServices
public class RequestServices
{
    readonly IRequestRepository _RequestRepository;
    readonly ICachingServices<Request> _CachingServices;

    public RequestServices(IRequestRepository requestRepository, ICachingServices<Request> cachingServices)
    {
        _RequestRepository = requestRepository;
        _CachingServices = cachingServices;
    }

    // Получить все Requests
    public async Task<IBaseResponse<IEnumerable<Request>>> GetRequests()
    {
        BaseResponse<IEnumerable<Request>> response;

        // Ищем в БД
        var requests = await _RequestRepository.Select();

        // Если не найдено Request
        // NoContent (204)
        if (requests.Count != 0)
        {
            response = BaseResponse<IEnumerable<Request>>.NoContent("Find 0 requests");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<Request>>.Ok(requests);

        return response;
    }

    // Получить Request по id
    public async Task<IBaseResponse<Request>> GetRequest(int id)
    {
        BaseResponse<Request> response;

        // Ищем в кэше
        var request = await _CachingServices.GetAsync(id);

        // Не нашли в кэше
        if (request == null)
        {
            // Ищем в БД
            request = await _RequestRepository.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Не нашли в БД
        // NotFound (404)
        if (request == null)
        {
            response = BaseResponse<Request>.NotFound("Request not found");
            return response;
        }

        // Нашли Request
        _CachingServices.SetAsync(request, request.Id.ToString());
        // Ok (200)
        response = BaseResponse<Request>.Ok(request);
        return response;
    }

    // Получить Requests по NeededPeopleNumber
    public async Task<IBaseResponse<IEnumerable<Request>>> GetRequestByNeededPeopleNumber(int neededPeopleNumber)
    {
        BaseResponse<IEnumerable<Request>> response;

        // Ищем в БД
        var requests = await _RequestRepository
            .Where(x => x.NeededPeopleNumber == neededPeopleNumber)
            .ToListAsync();

        // Не нашли в БД
        // NotFound (404)
        if (requests == null)
        {
            response = BaseResponse<IEnumerable<Request>>.NotFound("Requests not found");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<Request>>.Ok(requests);
        return response;
    }
}
