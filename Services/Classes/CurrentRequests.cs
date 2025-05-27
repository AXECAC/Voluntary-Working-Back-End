using DataBase;
namespace Services;

// Класс CurrentRequests
public class CurrentRequest
{
    public int Id { get; set; }
    public string Address { get; set; }
    public DateTime Date { get; set; }
    public DateTime DeadLine { get; set; }
    public int PointNumber { get; set; }
    public int NeededPeopleNumber { get; set; }
    public string TelegramUrl { get; set; }
    public string Description { get; set; }
    public bool IsComplited { get; set; }
    public bool IsFailed { get; set; }

    public CurrentRequest() { }

    public CurrentRequest(Request request)
    {
        this.Id = request.Id;
        this.Address = request.Address;
        this.Date = request.Date;
        this.DeadLine = request.DeadLine;
        this.PointNumber = request.PointNumber;
        this.NeededPeopleNumber = request.NeededPeopleNumber;
        this.TelegramUrl = request.TelegramUrl;
        this.Description = request.Description;
        this.IsComplited = request.IsComplited;
        this.IsFailed = request.IsFailed;
    }
}

