using DataBase;
namespace Services;

// Интерфейс IDevUserServices
public interface IDevUserServices
{
    // Выдать роль Admin
    Task<IBaseResponse> PromoteToAdmin(string userEmail);

}
