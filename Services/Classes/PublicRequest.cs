using DataBase;
namespace Services;

// Class PublicRequest
public class PublicRequest
{
    public int Id { get; set; }
    public string Address { get; set; }
    public DateTime Date { get; set; }
    public DateTime DeadLine { get; set; }
    public int PointNumber { get; set; }
    public int NeededPeopleNumber { get; set; }
    public int RespondedPeople { get; set; }
    public string Description { get; set; }
    public bool IsComplited { get; set; }

    public PublicRequest(Request request)
    {
        this.Id = request.Id;
        this.Address = request.Address;
        this.Date = request.Date;
        this.DeadLine = request.DeadLine;
        this.PointNumber = request.PointNumber;
        this.NeededPeopleNumber = request.NeededPeopleNumber;
        this.RespondedPeople = request.RespondedPeople.Count();
        this.Description = request.Description;
        this.IsComplited = request.IsComplited;
    }
}
