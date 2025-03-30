using System.Data.Entity;
using Context;
using DataBase;
using Services.Caching;
namespace Services;

// Класс AdminRequestServices
public class AdminRequestServices : IAdminRequestServices
{
    readonly IRequestRepository _RequestRepository;
    readonly ICachingServices<Request> _CachingServices;

    public AdminRequestServices(IRequestRepository requestRepository, ICachingServices<Request> cachingServices)
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
        if (requests.Count == 0)
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
    public async Task<IBaseResponse<IEnumerable<Request>>> GetRequestsByNeededPeopleNumber(int neededPeopleNumber)
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

    // Получить Requests по PointNumber
    public async Task<IBaseResponse<IEnumerable<Request>>> GetRequestsByPointNumber(int pointNumber)
    {
        BaseResponse<IEnumerable<Request>> response;

        // Ищем в БД
        var requests = await _RequestRepository
            .Where(x => x.PointNumber == pointNumber)
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

    // Получить все Requests созданные и закрытые админом по его Id
    public async Task<IBaseResponse<IEnumerable<Request>>> GetRequestsByAdminId(int adminId)
    {
        BaseResponse<IEnumerable<Request>> response;

        // Ищем в БД
        var requests = await _RequestRepository
            .Where(x => x.AdminId == adminId)
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

    // Получить все Requests по адресу
    public async Task<IBaseResponse<IEnumerable<Request>>> GetRequestsWithAddress(string address)
    {
        BaseResponse<IEnumerable<Request>> response;

        // Ищем в БД
        var requests = await _RequestRepository
            .Where(x => x.Address == address)
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
        if (requests == null)
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
            .Where(x => x.Date >= date)
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
        if (requests == null)
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
        if (requests == null)
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
        if (requests == null)
        {
            response = BaseResponse<IEnumerable<Request>>.NotFound("Requests not found");
            return response;
        }

        // Ok (200)
        response = BaseResponse<IEnumerable<Request>>.Ok(requests);
        return response;
    }

    // Создать Request
    public async Task<IBaseResponse<bool>> CreateRequest(Request request)
    {
        // Обнуляем значения
        request.IsFailed = false;
        request.IsComplited = false;
        request.RespondedPeople = [];

        await _RequestRepository.Create(request);

        // Создаем Request
        var response = BaseResponse<bool>.Created("Request created");
        return response;
    }

    // Удалить Request по id
    public async Task<IBaseResponse<bool>> DeleteRequest(int id)
    {
        BaseResponse<bool> response;
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
            response = BaseResponse<bool>.NotFound("Request not found");
            return response;
        }

        await _RequestRepository.Delete(request);
        // NoContent (204)
        response = BaseResponse<bool>.NoContent();
        return response;
    }

    // Изменить Request
    public async Task<IBaseResponse<bool>> EditRequest(Request newRequest)
    {
        BaseResponse<bool> response;
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
            response = BaseResponse<bool>.NotFound("Request not found");
            return response;
        }

        request.Address = newRequest.Address;
        request.Date = newRequest.Date;
        request.DeadLine = newRequest.DeadLine;
        request.PointNumber = newRequest.PointNumber;
        request.RespondedPeople = newRequest.RespondedPeople;
        request.NeededPeopleNumber = newRequest.NeededPeopleNumber;
        request.Desctiption = newRequest.Desctiption;
        request.IsComplited = newRequest.IsComplited;
        request.IsFailed = newRequest.IsFailed;

        // Добавляем измененного Request
        _CachingServices.SetAsync(request, request.Id.ToString());
        await _RequestRepository.Update(request);
        // NoContent (201)
        response = BaseResponse<bool>.Created("Request edit");
        return response;
    }

}
