using DataBase;
namespace Services;

// Класс HashingServices
public class HashingServices : IHashingServices
{
    private static string HashFunc(string input)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(input);
        return hash;
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    public bool Verify(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, BCrypt.Net.BCrypt.GenerateSalt());
    }

    public User Hashing(User user)
    {
        user.Password = HashFunc(user.Password);
        return user;
    }
}
