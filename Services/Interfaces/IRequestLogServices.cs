namespace Services;

// Interface IRequestLogServices
public interface IRequestLogServices
{
    void AppendLogToFile(RequestLog log);
}
