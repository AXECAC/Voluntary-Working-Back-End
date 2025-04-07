using DataBase;
namespace Services;

// Интерфейс IStudentRequestServices
public interface IStudentRequestServices
{
    // Получить все Requests
    Task<IBaseResponse<IEnumerable<PublicRequest>>> GetRequests();

    // Добавить Id студента в откликнувшихся на запрос
    Task<IBaseResponse> AssigneeMe(int request);
}
