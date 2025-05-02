using DataBase;
namespace Services;

// Class PrivateRequest
public class PrivateRequest
{
    public int Id { get; set; }
    public int AdminId { get; set; }
    public string Address { get; set; }
    public DateTime Date { get; set; }
    public DateTime DeadLine { get; set; }
    public int PointNumber { get; set; }
    public int NeededPeopleNumber { get; set; }
    public List<int> RespondedPeople { get; set; }
    public string Description { get; set; }
    public bool IsComplited { get; set; }
    public bool IsFailed { get; set; }

    public PrivateRequest(Request request)
    {
        this.Id = request.Id;
        this.Address = request.Address;
        this.Date = request.Date;
        this.DeadLine = request.DeadLine;
        this.PointNumber = request.PointNumber;
        this.NeededPeopleNumber = request.NeededPeopleNumber;
        this.RespondedPeople = new List<int>(); // Обнуляем количество откликнувших
        this.Description = request.Description;
        this.IsComplited = request.IsComplited;
    }
}
