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
    public string TelegramUrl { get; set; }
    public string Description { get; set; }
    public bool IsComplited { get; set; }
    public bool IsFailed { get; set; }

    public PrivateRequest() { }

    public PrivateRequest(Request request)
    {
        this.Id = request.Id;
        this.AdminId = request.AdminId;
        this.Address = request.Address;
        this.Date = request.Date;
        this.DeadLine = request.DeadLine;
        this.PointNumber = request.PointNumber;
        this.NeededPeopleNumber = request.NeededPeopleNumber;
        this.RespondedPeople = new List<int>(); // Обнуляем количество откликнувших
        this.TelegramUrl = request.TelegramUrl;
        this.Description = request.Description;
        this.IsComplited = request.IsComplited;
        this.IsFailed = request.IsFailed;
    }

    public PrivateRequest(Request request, List<RespondedPeople> respondedPeople)
    {
        this.Id = request.Id;
        this.AdminId = request.AdminId;
        this.Address = request.Address;
        this.Date = request.Date;
        this.DeadLine = request.DeadLine;
        this.PointNumber = request.PointNumber;
        this.NeededPeopleNumber = request.NeededPeopleNumber;
        this.SetRespondedPeople(respondedPeople); // Устанавливаем UserId откликнувшихся User
        this.TelegramUrl = request.TelegramUrl;
        this.Description = request.Description;
        this.IsComplited = request.IsComplited;
        this.IsFailed = request.IsFailed;
    }

    // Конструктор копирования PrivateRequest в Request
    public Request ToRequest()
    {
        // Создаем Request
        Request req = new Request();

        // Передаем значения из PrivateRequest в Request
        req.Id = this.Id;
        req.AdminId = this.AdminId;
        req.Address = this.Address;
        req.Date = this.Date;
        req.DeadLine = this.DeadLine;
        req.PointNumber = this.PointNumber;
        req.NeededPeopleNumber = this.NeededPeopleNumber;
        req.Description = this.Description;
        req.IsComplited = this.IsComplited;
        req.IsFailed = this.IsFailed;

        return req;
    }

    // Устанавливаем UserId откликнувшихся User
    public void SetRespondedPeople(List<RespondedPeople> respondedPeople)
    {
        // Обнуляем
        this.RespondedPeople = new List<int>();
        // Считаем
        for (int i = 0; i < respondedPeople.Count; ++i)
        {
            // Если Id PublicRequest и RequestId совпадают
            if (this.Id == respondedPeople[i].RequestId)
            {
                // Добавляем UserId
                this.RespondedPeople.Add(respondedPeople[i].UserId);
            }
        }
    }
}
