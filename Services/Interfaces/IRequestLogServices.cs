namespace Services;

// Interface IRequestLogServices
public interface IRequestLogServices
{
    // Добавить RequestLog последней строкой
    void AppendLogToFile(RequestLog log);

    // Получить все RequestLog данного Request
    List<RequestLog> ReadAllLogsFromFile(int requestId);

    // Удалить логи Request по его Id
    void DeleteLogFile(int requestId);
}
