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

    private static void EnsureFileExist(string pathToLog)
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

    // string => RequestLog
    private static RequestLog ParseLogLine(string logLine)
    {
        var parts = logLine.Split(",");
        return new RequestLog(
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                RemoveEscaping(parts[2]),
                DateTime.Parse(parts[3])
        );
    }

    private static RequestLog ReadLastLogFromFile(string pathToLog)
    {
        if (!File.Exists(pathToLog))
        {
            return new RequestLog(0, 0, "");
        }

        string lastLine = File.ReadLines(pathToLog).LastOrDefault();

        if (string.IsNullOrEmpty(lastLine))
        {
            return new RequestLog(0, 0, "");
        }

        return ParseLogLine(lastLine);
    }

    public void AppendLogToFile(RequestLog log)
    {
        // Путь до файла
        string fileName = TemplateLogFileName + log.RequestId.ToString() + ".log";
        string pathToLog = Path.Combine(LogsDir, fileName);

        // Находим последний log Id
        RequestLog lastLog = ReadLastLogFromFile(pathToLog);
        // Задаем новый log Id для новой записи
        log.Id = lastLog.Id + 1;

        EnsureFileExist(pathToLog);

        string newLog = $"{log.Id}, {log.RequestId}, {AddEscaping(log.Action)}, {log.Date:O}";

        File.AppendAllText(pathToLog, newLog + Environment.NewLine);
    }


    public List<RequestLog> ReadAllLogsFromFile(int requestId)
    {
        // Путь до файла
        string fileName = TemplateLogFileName + requestId.ToString() + ".log";
        string pathToLog = Path.Combine(LogsDir, fileName);

        if (!File.Exists(pathToLog))
        {
            return new List<RequestLog>();
        }

        return File.ReadAllLines(pathToLog)
            .Where(line => !string.IsNullOrEmpty(line))
            .Select(ParseLogLine)
            .ToList();
    }
}
