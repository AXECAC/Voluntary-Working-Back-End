namespace DataBase;

// Class Request
public class Request
{
    public int Id { get; set; }
    public int AdminId { get; set; }
    public string Address { get; set; }
    public DateTime Date { get; set; }
    public DateTime DeadLine { get; set; }
    public int PointNumber { get; set; }
    public int NeededPeopleNumber { get; set; }
    public string Desctiption { get; set; }
    public bool IsComplited { get; set; }
}
