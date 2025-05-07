using DataBase;
namespace Services;

// Интерфейс IUserServices
public interface IUserServices
{
    // Получить Id из jwt
    int GetMyId();

    // Получить Role из jwt
    string GetMyRole();

    Task<IBaseResponse> CheckIdsValid(List<int> ids);
}
