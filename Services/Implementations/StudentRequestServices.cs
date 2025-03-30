using Microsoft.EntityFrameworkCore;
using Context;
using DataBase;
using Services.Caching;
namespace Services;

// Класс StudentRequestServices
public class StudentRequestServices : IStudentRequestServices
{
    readonly IRequestRepository _RequestRepository;
    readonly ICachingServices<PublicRequest> _CachingServices;

    public StudentRequestServices(IRequestRepository requestRepository, ICachingServices<PublicRequest> cachingServices)
    {
        _RequestRepository = requestRepository;
        _CachingServices = cachingServices;
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
}
