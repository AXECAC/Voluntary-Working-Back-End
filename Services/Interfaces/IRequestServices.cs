using DataBase;
namespace Services;

// Интерфейс IRequestServices
public interface IRequestServices
{
    // Получить все Requests
    Task<IBaseResponse<IEnumerable<Request>>> GetRequests();

    // Получить Request по id
    Task<IBaseResponse<Request>> GetRequest(int id);

    // Получить Request по NeededPeopleNumber
    Task<IBaseResponse<Request>> GetRequestByNeededPeopleNumber(int id);

    // Получить Request по PointNumber
    Task<IBaseResponse<Request>> GetRequestByPointNumber(int id);

    // Получить все Requests созданные и закрытые админом по его Id
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsByAdminId(int adminId);

    // Получить все Requests по адресу
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsWithAdress(string address);

    // Получить все Requests по DateTime начала
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsDTBegin(DateTime dateOfBegin);

    // Получить все Requests доступные на данный момент
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsDT(DateTime date);

    // Получить все Requests по DateTime дедлайна
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsDTDeadLine(DateTime dateOfDeadLine);

    // Получить все Requests по IsCompleted == true
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsCompleted();

    // Получить все Requests по IsFailed == true
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsFailed();
}
