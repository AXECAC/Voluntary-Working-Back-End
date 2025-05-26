namespace Services;

// Класс RequestLogServices
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

    private void EnsureFileExist(string pathToLog)
    {
        // Создаем файл, если его нет
        if (!File.Exists(pathToLog))
        {
            File.Create(pathToLog).Close(); // Закрываем поток после создания
        }
    }

    private static string AddEscaping(string text)
    {
        return text?.Replace(",", "\\,");
    }

    private static string RemoveEscaping(string text)
    {
        return text?.Replace("\\,", ",");
    }



    public void AppendLogToFile(RequestLog log)
    {
        // Путь до файла
        string fileName = TemplateLogFileName + log.RequestId.ToString() + ".log";
        string pathToLog = Path.Combine(LogsDir, fileName);

        EnsureFileExist(pathToLog);

        string newLog = $"{log.Id}, {log.RequestId}, {AddEscaping(log.Action)}, {log.Date:O}";

        File.AppendAllText(pathToLog, newLog + Environment.NewLine);
    }
}
