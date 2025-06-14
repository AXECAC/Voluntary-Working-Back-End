using DataBase;
namespace Services;

// Интерфейс IAdminRequestServices
public interface IAdminRequestServices
{
    // Получить все Requests
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> Get();

    // Получить Request по id
    Task<IBaseResponse<PrivateRequest>> Get(int id);

    // Получить Requests по NeededPeopleNumber
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetByNeededPeopleNumber(int neededPeopleNumber);

    // Получить Requests по PointNumber
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetByPointNumber(int pointNumber);

    // Получить все Requests созданные и закрытые админом по его Id
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetByAdminId(int adminId);

    // Получить все Requests по адресу
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetByAddress(string address);

    // Получить все Requests по DateTime начала
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetDTBegin(DateTime dateOfBegin);

    // Получить все Requests доступные на момент date
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetDT(DateTime date);

    // Получить все Requests по DateTime дедлайна
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetDTDeadLine(DateTime dateOfDeadLine);

    // Получить все Requests по IsCompleted == true
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetCompleted();

    // Получить все Requests по IsFailed == true
    Task<IBaseResponse<IEnumerable<PrivateRequest>>> GetFailed();

    // Создать Request
    Task<IBaseResponse> Create(Request request);

    // Удалить Request по id
    Task<IBaseResponse> Delete(int id);

    // Изменить Request
    Task<IBaseResponse> Update(Request request);

    // Отметить Request как выполненый и зачислить баллы всем User из usersId
    Task<IBaseResponse> MarkAsCompleted(int requestId, List<int> usersId);
}
