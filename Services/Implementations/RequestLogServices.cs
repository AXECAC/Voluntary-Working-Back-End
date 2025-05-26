namespace Services;

// Class RequestLogServices
public class RequestLogServices : IRequestLogServices
{
    private readonly string LogsDir = "RequestLogs";
    private readonly string TemplateLogFileName = "request";

    public RequestLogServices()
    {
        EnsureDirectoryExist();
    }

    private void EnsureDirectoryExist()
    {
        // Создаем директорию, если ее нет
        if (!Directory.Exists(LogsDir))
        {
            Directory.CreateDirectory(LogsDir);
        }
    }

    private void EnsureFileExist(int requestId)
    {
        string fileName = TemplateLogFileName + requestId.ToString() + ".log";
        string pathToLog = Path.Combine(LogsDir, fileName);

        // Создаем файл, если его нет
        if (!File.Exists(pathToLog))
        {
            File.Create(pathToLog).Close(); // Закрываем поток после создания
        }
    }

    public void AppendLogToFile(RequestLog log)
    {
        EnsureFileExist(log.RequestId);

    }
}
