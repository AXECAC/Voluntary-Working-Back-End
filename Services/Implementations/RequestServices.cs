using Context;
using DataBase;
namespace Services;

// Класс RequestServices
// public class RequestServices : IRequestServices
public class RequestServices
{
    readonly IRequestRepository _RequestRepository;

    public RequestServices(IRequestRepository requestRepository)
    {
        _RequestRepository = requestRepository;
    }

    // Получить все Requests
    async Task<IBaseResponse<IEnumerable<Request>>> GetRequests()
    {
        BaseResponse<IEnumerable<Request>> response;

        var requests = await _RequestRepository.Select();

        // Если не найдено Request
        if (requests.Count != 0)
        {
            response = BaseResponse<IEnumerable<Request>>.NoContent("Find 0 requests");
            return response;
        }

        response = BaseResponse<IEnumerable<Request>>.Ok(requests);
        return response;
    }
}
