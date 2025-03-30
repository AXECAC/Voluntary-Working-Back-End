using DataBase;
namespace Services;

// Class PublicRequests
public class PublicRequests
{
    public int Id { get; set; }
    public string Address { get; set; }
    public DateTime Date { get; set; }
    public DateTime DeadLine { get; set; }
    public int PointNumber { get; set; }
    public int NeededPeopleNumber { get; set; }
    public int RespondedPeople { get; set; }
    public string Desctiption { get; set; }
    public bool IsComplited { get; set; }

    public PublicRequests(Request request)
    {
        this.Id = request.Id;
        this.Address = request.Address;
        this.Date = request.Date;
        this.DeadLine = request.DeadLine;
        this.PointNumber = request.PointNumber;
        this.NeededPeopleNumber = request.NeededPeopleNumber;
        this.RespondedPeople = request.RespondedPeople.Count();
        this.Desctiption = request.Desctiption;
        this.IsComplited = request.IsComplited;
    }
}
