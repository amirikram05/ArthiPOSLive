using System;
using System.IO;
using System.Text;

namespace ArthiPOS.Common.Utilities
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    public interface ILogger
    {
        void Log(LogLevel level, string message, Exception exception = null);
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message, Exception exception = null);
        void Fatal(string message, Exception exception = null);
    }

    public class FileLogger : ILogger, IDisposable
    {
        private readonly string _logDirectory;
        private readonly string _logFile;
        private readonly object _lockObject = new object();
        private bool _disposed;

        public FileLogger(string logDirectory = null)
        {
            _logDirectory = logDirectory ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(_logDirectory);

            _logFile = Path.Combine(_logDirectory, $"ArthiPOS_{DateTime.Now:yyyyMMdd}.log");
        }

        public void Log(LogLevel level, string message, Exception exception = null)
        {
            lock (_lockObject)
            {
                try
                {
                    var logEntry = FormatLogEntry(level, message, exception);
                    File.AppendAllText(_logFile, logEntry, Encoding.UTF8);

                    // Also write to console for debugging
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debug.WriteLine(logEntry);
                    }
                }
                catch
                {
                    // If logging fails, we don't want to crash the application
                }
            }
        }

        public void Debug(string message) => Log(LogLevel.Debug, message);
        public void Info(string message) => Log(LogLevel.Info, message);
        public void Warning(string message) => Log(LogLevel.Warning, message);
        public void Error(string message, Exception exception = null) => Log(LogLevel.Error, message, exception);
        public void Fatal(string message, Exception exception = null) => Log(LogLevel.Fatal, message, exception);

        private string FormatLogEntry(LogLevel level, string message, Exception exception)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{level}] {message}");

            if (exception != null)
            {
                sb.AppendLine($"Exception: {exception.GetType().Name}");
                sb.AppendLine($"Message: {exception.Message}");
                sb.AppendLine($"Stack Trace: {exception.StackTrace}");

                if (exception.InnerException != null)
                {
                    sb.AppendLine($"Inner Exception: {exception.InnerException}");
                }
            }

            sb.AppendLine(new string('-', 80));
            return sb.ToString();
        }

        public void ClearOldLogs(int daysToKeep = 7)
        {
            try
            {
                var cutoffDate = DateTime.Now.AddDays(-daysToKeep);
                var logFiles = Directory.GetFiles(_logDirectory, "ArthiPOS_*.log");

                foreach (var file in logFiles)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.CreationTime < cutoffDate)
                    {
                        fileInfo.Delete();
                    }
                }
            }
            catch
            {
                // Ignore cleanup errors
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                ClearOldLogs();
                _disposed = true;
            }
        }
    }
}