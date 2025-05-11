using DataBase;
namespace Services;

// Интерфейс IUserServices
public interface IUserServices
{
    // Получить Id из jwt
    int GetMyId();

    // Получить Role из jwt
    string GetMyRole();

    // Проверить Id людей (есть ли люди с такими ids)
    Task<IBaseResponse> CheckIdsValid(List<int> ids);

    // Получить мои данные
    Task<IBaseResponse<User>> GetMyProfile();

    // Получить Requsts, на которые я записался
    Task<IBaseResponse<List<CurrentRequest>>> GetMyCurrentRequests();
}
