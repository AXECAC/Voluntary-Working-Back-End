namespace DataBase;

// Entity Класс Request
public class Request
{
    public int Id { get; set; }
    public int AdminId { get; set; }
    public string Address { get; set; }
    public DateTime Date { get; set; }
    public DateTime DeadLine { get; set; }
    public int PointNumber { get; set; }
    public int NeededPeopleNumber { get; set; }
    public string TelegramUrl { get; set; }
    public string Description { get; set; }
    public bool IsComplited { get; set; }
    public bool IsFailed { get; set; }
}
