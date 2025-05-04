using Microsoft.EntityFrameworkCore;
using Context;
using DataBase;
using Extentions;
using Services.Caching;
namespace Services;

// Класс AdminRequestServices
public class AdminRequestServices : IAdminRequestServices
{
    private readonly IRequestRepository _RequestRepository;
    private readonly IRespondedPeopleRepository _RespondedPeopleRepository;
    private readonly ICachingServices<Request> _CachingServices;

    public AdminRequestServices(IRequestRepository requestRepository, ICachingServices<Request> cachingServices,
            IRespondedPeopleRepository respondedPeopleRepository)
    {
        _RequestRepository = requestRepository;
        _RespondedPeopleRepository = respondedPeopleRepository;
        _CachingServices = cachingServices;
    }

    // Получить все Requests
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetRequests()
    {
        BaseResponse<IEnumerable<PrivateRequest>> response;

        var respondedPeople = await _RespondedPeopleRepository.GetAll();

        List<PrivateRequest> requests;

        // Если есть откликнувшеся люди
        if (respondedPeople != null && respondedPeople.Count > 0)
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Select(request => new PrivateRequest(request, respondedPeople))
                .ToListAsync();
        }
        else
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Select(request => new PrivateRequest(request))
                .ToListAsync();
        }

        // Если не найдено Request
        // NoContent (204)
        if (requests.Count == 0)
        {
            response = BaseResponse<IEnumerable<PrivateRequest>>.NoContent("Find 0 requests");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<PrivateRequest>>.Ok(requests);

        return response;
    }

    // Получить Request по id
    public async Task<IBaseResponse<PrivateRequest>> GetRequest(int id)
    {
        BaseResponse<PrivateRequest> response;

        // Ищем всех откликнувшихся на этот запрос
        var respondedPeople = await _RespondedPeopleRepository
            .GetQueryable()
            .Where(rp => rp.RequestId == id)
            .ToListAsync();

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
            response = BaseResponse<PrivateRequest>.NotFound("Request not found");
            return response;
        }

        // Нашли Request
        _CachingServices.SetAsync(request, request.Id.ToString());
        // Ok (200)
        response = BaseResponse<PrivateRequest>.Ok(new PrivateRequest(request, respondedPeople));
        return response;
    }

    // Получить Requests по NeededPeopleNumber
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetRequestsByNeededPeopleNumber(int neededPeopleNumber)
    {
        BaseResponse<IEnumerable<PrivateRequest>> response;

        // Берем всех откликнувшихся на запросы людей
        var respondedPeople = await _RespondedPeopleRepository.GetAll();

        List<PrivateRequest> requests;
        // Если есть откликнувшеся люди
        if (respondedPeople != null && respondedPeople.Count > 0)
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.NeededPeopleNumber == neededPeopleNumber)
                .Select(request => new PrivateRequest(request, respondedPeople))
                .ToListAsync();
        }
        else
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.NeededPeopleNumber == neededPeopleNumber)
                .Select(request => new PrivateRequest(request))
                .ToListAsync();
        }

        // Не нашли в БД
        // NotFound (404)
        if (requests == null || requests.Count() == 0)
        {
            response = BaseResponse<IEnumerable<PrivateRequest>>.NotFound("Requests not found");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<PrivateRequest>>.Ok(requests);
        return response;
    }

    // Получить Requests по PointNumber
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetRequestsByPointNumber(int pointNumber)
    {
        BaseResponse<IEnumerable<PrivateRequest>> response;

        // Берем всех откликнувшихся на запросы людей
        var respondedPeople = await _RespondedPeopleRepository.GetAll();

        List<PrivateRequest> requests;
        // Если есть откликнувшеся люди
        if (respondedPeople != null && respondedPeople.Count > 0)
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.PointNumber == pointNumber)
                .Select(request => new PrivateRequest(request, respondedPeople))
                .ToListAsync();
        }
        else
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.PointNumber == pointNumber)
                .Select(request => new PrivateRequest(request))
                .ToListAsync();
        }


        // Не нашли в БД
        // NotFound (404)
        if (requests == null)
        {
            response = BaseResponse<IEnumerable<PrivateRequest>>.NotFound("Requests not found");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<PrivateRequest>>.Ok(requests);
        return response;
    }

    // Получить все Requests созданные и закрытые админом по его Id
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetRequestsByAdminId(int adminId)
    {
        BaseResponse<IEnumerable<PrivateRequest>> response;

        // Берем всех откликнувшихся на запросы людей
        var respondedPeople = await _RespondedPeopleRepository.GetAll();

        List<PrivateRequest> requests;
        // Если есть откликнувшеся люди
        if (respondedPeople != null && respondedPeople.Count > 0)
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.AdminId == adminId)
                .Select(request => new PrivateRequest(request, respondedPeople))
                .ToListAsync();
        }
        else
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.AdminId == adminId)
                .Select(request => new PrivateRequest(request))
                .ToListAsync();
        }

        // Не нашли в БД
        // NotFound (404)
        if (requests == null || requests.Count() == 0)
        {
            response = BaseResponse<IEnumerable<PrivateRequest>>.NotFound("Requests not found");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<PrivateRequest>>.Ok(requests);
        return response;
    }

    // Получить все Requests по адресу
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetRequestsByAddress(string address)
    {
        BaseResponse<IEnumerable<PrivateRequest>> response;

        // Берем всех откликнувшихся на запросы людей
        var respondedPeople = await _RespondedPeopleRepository.GetAll();

        List<PrivateRequest> requests;
        // Если есть откликнувшеся люди
        if (respondedPeople != null && respondedPeople.Count > 0)
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.Address == address)
                .Select(request => new PrivateRequest(request, respondedPeople))
                .ToListAsync();
        }
        else
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.Address == address)
                .Select(request => new PrivateRequest(request))
                .ToListAsync();
        }

        // Не нашли в БД
        // NotFound (404)
        if (requests == null || requests.Count() == 0)
        {
            response = BaseResponse<IEnumerable<PrivateRequest>>.NotFound("Requests not found");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<PrivateRequest>>.Ok(requests);
        return response;
    }

    // Получить все Requests по DateTime начала
    public async Task<IBaseResponse<IEnumerable<Request>>> GetRequestsDTBegin(DateTime dateOfBegin)
    {
        BaseResponse<IEnumerable<Request>> response;

        // Ищем в БД
        var requests = await _RequestRepository
            .Where(x => x.Date == dateOfBegin)
            .ToListAsync();

        // Не нашли в БД
        // NotFound (404)
        if (requests == null || requests.Count() == 0)
        {
            response = BaseResponse<IEnumerable<Request>>.NotFound("Requests not found");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<Request>>.Ok(requests);
        return response;
    }

    // Получить все Requests доступные на момент date
    public async Task<IBaseResponse<IEnumerable<Request>>> GetRequestsDT(DateTime date)
    {
        BaseResponse<IEnumerable<Request>> response;

        // Ищем в БД
        var requests = await _RequestRepository
            .Where(x => x.Date <= date && date < x.DeadLine)
            .ToListAsync();

        // Не нашли в БД
        // NotFound (404)
        if (requests == null || requests.Count() == 0)
        {
            response = BaseResponse<IEnumerable<Request>>.NotFound("Requests not found");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<Request>>.Ok(requests);
        return response;
    }

    // Получить все Requests по DateTime дедлайна
    public async Task<IBaseResponse<IEnumerable<Request>>> GetRequestsDTDeadLine(DateTime dateOfDeadLine)
    {
        BaseResponse<IEnumerable<Request>> response;

        // Ищем в БД
        var requests = await _RequestRepository
            .Where(x => x.DeadLine == dateOfDeadLine)
            .ToListAsync();

        // Не нашли в БД
        // NotFound (404)
        if (requests == null || requests.Count() == 0)
        {
            response = BaseResponse<IEnumerable<Request>>.NotFound("Requests not found");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<Request>>.Ok(requests);
        return response;
    }

    // Получить все Requests по IsCompleted == true
    public async Task<IBaseResponse<IEnumerable<Request>>> GetRequestsCompleted()
    {
        BaseResponse<IEnumerable<Request>> response;

        // Ищем в БД
        var requests = await _RequestRepository
            .Where(x => x.IsComplited == true)
            .ToListAsync();

        // Не нашли в БД
        // NotFound (404)
        if (requests == null || requests.Count() == 0)
        {
            response = BaseResponse<IEnumerable<Request>>.NotFound("Requests not found");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<Request>>.Ok(requests);
        return response;
    }

    // Получить все Requests по IsFailed == true
    public async Task<IBaseResponse<IEnumerable<Request>>> GetRequestsFailed()
    {
        BaseResponse<IEnumerable<Request>> response;

        // Ищем в БД
        var requests = await _RequestRepository
            .Where(x => x.IsFailed == true)
            .ToListAsync();

        // Не нашли в БД
        // NotFound (404)
        if (requests == null || requests.Count() == 0)
        {
            response = BaseResponse<IEnumerable<Request>>.NotFound("Requests not found");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<Request>>.Ok(requests);
        return response;
    }

    // Создать Request
    public async Task<IBaseResponse> CreateRequest(Request request)
    {
        // Обнуляем значения
        request.Id = 0;
        request.IsFailed = false;
        request.IsComplited = false;

        await _RequestRepository.Create(request);

        // Создаем Request
        var response = BaseResponse.Created("Request created");
        return response;
    }

    // Удалить Request по id
    public async Task<IBaseResponse> DeleteRequest(int id)
    {
        BaseResponse response;
        // Ищем в кэше
        var request = await _CachingServices.GetAsync(id);

        // Нашли в кэше
        if (request != null)
        {
            // Удаляем из кэша
            _CachingServices.RemoveAsync(id.ToString());
        }

        request = await _RequestRepository.FirstOrDefaultAsync(x => x.Id == id);

        // Request не найден (404)
        if (request == null)
        {
            response = BaseResponse.NotFound("Request not found");
            return response;
        }

        await _RequestRepository.Delete(request);
        // NoContent (204)
        response = BaseResponse.NoContent();
        return response;
    }

    // Изменить Request
    public async Task<IBaseResponse> EditRequest(Request newRequest)
    {
        BaseResponse response;
        // Ищем в кэше
        var request = await _CachingServices.GetAsync(newRequest.Id);

        // Нашли в кэше
        if (request != null)
        {
            // Удаляем из кэша
            _CachingServices.RemoveAsync(newRequest.Id.ToString());
        }

        request = await _RequestRepository.FirstOrDefaultAsync(x => x.Id == newRequest.Id);

        // Request не найден (404)
        if (request == null)
        {
            response = BaseResponse.NotFound("Request not found");
            return response;
        }

        request.AdminId = request.AdminId;
        request.Address = newRequest.Address;
        request.Date = newRequest.Date;
        request.DeadLine = newRequest.DeadLine;
        request.PointNumber = newRequest.PointNumber;
        // request.RespondedPeople = newRequest.RespondedPeople;
        request.NeededPeopleNumber = newRequest.NeededPeopleNumber;
        request.Description = newRequest.Description;
        request.IsFailed = newRequest.IsFailed;

        // Добавляем измененного Request
        _CachingServices.SetAsync(request, request.Id.ToString());
        await _RequestRepository.Update(request);
        // NoContent (201)
        response = BaseResponse.Created("Request edit");
        return response;
    }
}
