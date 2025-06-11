using DataBase;
namespace Services;

// Интерфейс IHashingServices
public interface IHashingServices
{
    bool Verify(string password, string hash);
    bool Verify(string password);
    User Hashing(User userEntity);
}
