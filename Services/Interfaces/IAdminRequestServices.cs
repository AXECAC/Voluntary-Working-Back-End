using DataBase;
namespace Services;

// Интерфейс IAdminRequestServices
public interface IAdminRequestServices
{
    // Получить все Requests
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetRequests();

    // Получить Request по id
    Task<IBaseResponse<PrivateRequest>> GetRequest(int id);

    // Получить Requests по NeededPeopleNumber
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetRequestsByNeededPeopleNumber(int neededPeopleNumber);

    // Получить Requests по PointNumber
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetRequestsByPointNumber(int pointNumber);

    // Получить все Requests созданные и закрытые админом по его Id
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsByAdminId(int adminId);

    // Получить все Requests по адресу
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsByAddress(string address);

    // Получить все Requests по DateTime начала
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsDTBegin(DateTime dateOfBegin);

    // Получить все Requests доступные на момент date
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsDT(DateTime date);

    // Получить все Requests по DateTime дедлайна
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsDTDeadLine(DateTime dateOfDeadLine);

    // Получить все Requests по IsCompleted == true
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsCompleted();

    // Получить все Requests по IsFailed == true
    Task<IBaseResponse<IEnumerable<Request>>> GetRequestsFailed();

    // Создать Request
    Task<IBaseResponse> CreateRequest(Request request);

    // Удалить Request по id
    Task<IBaseResponse> DeleteRequest(int id);

    // Изменить Request
    Task<IBaseResponse> EditRequest(Request request);
}
