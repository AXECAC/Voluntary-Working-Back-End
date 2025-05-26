namespace Services;

// Класс RequestLog
public class RequestLog
{
    public int Id { get; set; }
    public int RequestId { get; set; }
    public string Action { get; set; }
    public DateTime Date { get; set; }

    public RequestLog() { }

    public RequestLog(int id, int requestId, string action)
    {
        Id = id;
        RequestId = requestId;
        Action = action;
        Date = DateTime.Now;
    }

    public RequestLog(int id, int requestId, string action, DateTime date)
    {
        Id = id;
        RequestId = requestId;
        Action = action;
        Date = date;
    }
}
