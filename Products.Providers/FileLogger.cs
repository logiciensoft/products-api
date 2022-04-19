using Microsoft.Extensions.Logging;

namespace Products.Providers;

public class FileLogger: ILogger
{
    private readonly string _filePath;
    private static readonly object Lock = new();
    
    public FileLogger(string path)
    {
        _filePath = path;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null!;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (formatter != null!)
        {
            lock (Lock)
            {
                var exc = "";
                var n = Environment.NewLine;
                var fullFilePath = Path.Combine(_filePath, DateTime.Now.ToString("yyyy_MM_dd") + ".log");
                
                if (exception != null) 
                    exc = n + exception.GetType() + ": " + exception.Message + n + exception.StackTrace + n;

                if (!Directory.Exists(_filePath))
                    Directory.CreateDirectory(_filePath);

                File.AppendAllText(fullFilePath, logLevel + ": " + DateTime.Now + " " + formatter(state, exception) + n + exc);
            }
        }
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    
}