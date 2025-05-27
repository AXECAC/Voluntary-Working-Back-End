namespace DataBase;

// Класс User
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; } // ФИО через пробел
    public string TelegramUrl { get; set; }
    public string Role { get; set; }
    public string Group { get; set; }
    public int Points { get; set; }
    public int FinishedRequests { get; set; }
}
