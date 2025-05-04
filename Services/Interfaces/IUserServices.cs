using DataBase;
namespace Services;

// Интерфейс IUserServices
public interface IUserServices
{
    int GetMyId();

    Task<IBaseResponse> CheckIdsValid(List<int> ids);
}
