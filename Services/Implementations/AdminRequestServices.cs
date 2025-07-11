using Microsoft.EntityFrameworkCore;
using Context;
using DataBase;
using Services.Caching;
namespace Services;

// Класс AdminRequestServices
public class AdminRequestServices : IAdminRequestServices
{
    private readonly IRequestRepository _RequestRepository;
    private readonly IUserRepository _UserRepository;
    private readonly IRespondedPeopleRepository _RespondedPeopleRepository;
    private readonly ICachingServices<Request> _CachingServices;
    private readonly IRequestLogServices _RequestLogServices;

    public AdminRequestServices(IRequestRepository requestRepository, IUserRepository userRepository,
            ICachingServices<Request> cachingServices, IRespondedPeopleRepository respondedPeopleRepository,
            IRequestLogServices requestLogServices)
    {
        _RequestRepository = requestRepository;
        _UserRepository = userRepository;
        _RespondedPeopleRepository = respondedPeopleRepository;
        _CachingServices = cachingServices;
        _RequestLogServices = requestLogServices;
    }

    // Получить все Requests
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> Get()
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
    public async Task<IBaseResponse<PrivateRequest>> Get(int id)
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
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetByNeededPeopleNumber(int neededPeopleNumber)
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
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetByPointNumber(int pointNumber)
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
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetByAdminId(int adminId)
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
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetByAddress(string address)
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
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetDTBegin(DateTime dateOfBegin)
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
                .Where(x => x.Date == dateOfBegin)
                .Select(request => new PrivateRequest(request, respondedPeople))
                .ToListAsync();
        }
        else
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.Date == dateOfBegin)
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

    // Получить все Requests доступные на момент date
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetDT(DateTime date)
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
                .Where(x => x.Date <= date && date < x.DeadLine)
                .Select(request => new PrivateRequest(request, respondedPeople))
                .ToListAsync();
        }
        else
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.Date <= date && date < x.DeadLine)
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

    // Получить все Requests по DateTime дедлайна
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetDTDeadLine(DateTime dateOfDeadLine)
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
                .Where(x => x.DeadLine == dateOfDeadLine)
                .Select(request => new PrivateRequest(request, respondedPeople))
                .ToListAsync();
        }
        else
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.DeadLine == dateOfDeadLine)
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

    // Получить все Requests по IsCompleted == true
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetCompleted()
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
                .Where(x => x.IsComplited == true)
                .Select(request => new PrivateRequest(request, respondedPeople))
                .ToListAsync();
        }
        else
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.IsComplited == true)
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

    // Получить все Requests по IsFailed == true
    public async Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetFailed()
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
                .Where(x => x.IsFailed == true)
                .Select(request => new PrivateRequest(request, respondedPeople))
                .ToListAsync();
        }
        else
        {
            // Ищем в БД
            requests = await _RequestRepository
                .GetQueryable()
                .Where(x => x.IsFailed == true)
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

    // Создать Request
    public async Task<IBaseResponse> Create(Request request)
    {
        // Обнуляем значения
        request.Id = 0;
        request.TelegramUrl = "Временно не доступен";
        request.IsFailed = false;
        request.IsComplited = false;

        await _RequestRepository.Create(request);

        int requestId = await _RequestRepository
            .GetQueryable()
            .Where(req => req.AdminId == request.AdminId)
            .OrderBy(req => req.Id)
            .Select(req => req.Id)
            .LastOrDefaultAsync();

        
        RequestLog log = new RequestLog(0, request.AdminId, requestId, $"Request created");

        _RequestLogServices.AppendLogToFile(log);
        // Создаем Request
        var response = BaseResponse.Created($"Request{requestId} created");
        return response;
    }

    // Удалить Request по id
    public async Task<IBaseResponse> Delete(int id)
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

        RequestLog log = new RequestLog(0, request.AdminId, request.Id, $"Request deleted");

        _RequestLogServices.AppendLogToFile(log);
        // NoContent (204)
        response = BaseResponse.NoContent();
        return response;
    }

    // Изменить Request
    public async Task<IBaseResponse> Update(Request newRequest)
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

        request.AdminId = newRequest.AdminId;
        request.Address = newRequest.Address;
        request.Date = newRequest.Date;
        request.DeadLine = newRequest.DeadLine;
        request.PointNumber = newRequest.PointNumber;
        request.NeededPeopleNumber = newRequest.NeededPeopleNumber;
        request.Description = newRequest.Description;

        // Добавляем измененного Request
        _CachingServices.SetAsync(request, request.Id.ToString());
        await _RequestRepository.Update(request);


        RequestLog log = new RequestLog(0, request.AdminId, request.Id, $"Request updated: Adress to {request.Address}; Date to {request.Date}; DeadLine to {request.DeadLine}; PointNumber to {request.PointNumber}; NeededPeopleNumber to {request.NeededPeopleNumber}; Description to {request.Description}");

        _RequestLogServices.AppendLogToFile(log);
        // NoContent (201)
        response = BaseResponse.Created("Request Update");
        return response;
    }

    private async Task<IBaseResponse> PointsPerRequest(int points, List<int> usersId)
    {
        BaseResponse response;

        List<User> users = new List<User>();

        User user;
        for (int i = 0; i < usersId.Count; i++)
        {
            user = await _UserRepository.FirstOrDefaultAsync(us => us.Id == usersId[i]);

            if (user == null)
            {
                response = BaseResponse.NotFound("User is not found");
                return response;
            }

            user.Points += points;
            user.FinishedRequests += 1;
        }

        for (int i = 0; i < users.Count; i++)
        {
            await _UserRepository.Update(users[i]);
        }
        response = BaseResponse.NoContent();
        return response;
    }


    // Отметить Request как выполненый и зачислить баллы всем User из usersId
    public async Task<IBaseResponse> MarkAsCompleted(int requestId, List<int> usersId)
    {
        IBaseResponse response;

        var request = await _RequestRepository.FirstOrDefaultAsync(rq => rq.Id == requestId);

        if (request == null)
        {
            response = BaseResponse.NotFound("Request not found");
            return response;
        }

        if (usersId.Count > request.NeededPeopleNumber)
        {
            response = BaseResponse.UnprocessableContent("Number of Ids is more than necessary");
            return response;
        }

        if (request.IsComplited == true)
        {
            response = BaseResponse.BadRequest("Request is already Completed");
            return response;
        }

        if (request.IsFailed == true)
        {
            response = BaseResponse.BadRequest("Request is already Failed");
            return response;
        }

        response = await PointsPerRequest(request.PointNumber, usersId);

        if (response.StatusCode == DataBase.StatusCodes.NotFound)
        {
            return response;
        }

        request.IsComplited = true;

        await _RequestRepository.Update(request);

        RequestLog log = new RequestLog(0, request.AdminId, request.Id, $"Request MarkAsCompleted");

        _RequestLogServices.AppendLogToFile(log);
        return response;
    }
}
