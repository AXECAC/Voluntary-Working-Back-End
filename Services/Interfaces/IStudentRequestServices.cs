using DataBase;
namespace Services;

// Интерфейс IStudentRequestServices
public interface IStudentRequestServices
{
    // Получить все Requests
    Task<IBaseResponse<IEnumerable<PublicRequest>>> GetRequests();
}
