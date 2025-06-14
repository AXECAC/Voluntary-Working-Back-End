using DataBase;
namespace Services;

// Интерфейс IAdminUserServices
public interface IAdminUserServices
{
    Task<IBaseResponse<IEnumerable<User>>> Get();

    Task<IBaseResponse<User>> Get(int id);

    Task<IBaseResponse> Create(User userEntity);

    Task<IBaseResponse> Delete(int id);

    Task<IBaseResponse<User>> GetByEmail(string email);

    Task<IBaseResponse> Update(string oldEmail, User userEntity);
}
