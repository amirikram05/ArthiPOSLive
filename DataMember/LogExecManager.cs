using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DataMember
{
    public class LogExecManager
    {
        private readonly string logDirectory;
        private readonly int retentionDays;

        public LogExecManager(string logDirectory, int retentionDays = 10)
        {
            this.logDirectory = logDirectory;
            this.retentionDays = retentionDays;

            // Ensure the log directory exists; if not, create it
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
                Console.WriteLine($"Log directory created: {logDirectory}");
            }

            // Clean up old logs on initialization
            CleanUpOldLogs();
        }

        // Log a normal message to the log file
        public void Log(string message)
        {
            string logFilePath = GetLogFilePath();

            using (StreamWriter sw = new StreamWriter(logFilePath, true))
            {
                string logMessage = $"{DateTime.Now}: {message}";
                sw.WriteLine(logMessage);
            }

            // Clean up old logs each time a new log is written
            CleanUpOldLogs();
        }

        // Log an exception with detailed information
        public void LogException(Exception ex, string additionalInfo = "")
        {
            string logFilePath = GetLogFilePath();

            using (StreamWriter sw = new StreamWriter(logFilePath, true))
            {
                string logMessage = $"{DateTime.Now}: [ERROR] {ex.Message} | {additionalInfo} | StackTrace: {ex.StackTrace}";
                sw.WriteLine(logMessage);
            }

            // Clean up old logs each time a new log is written
            CleanUpOldLogs();
        }

        // Get the log file path for today's log
        private string GetLogFilePath()
        {
            // Generate a file name based on the current date
            string logFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            return Path.Combine(logDirectory, logFileName);
        }

        // Delete logs older than the retention period
        private void CleanUpOldLogs()
        {
            var logFiles = Directory.GetFiles(logDirectory, "*.log");

            foreach (var logFile in logFiles)
            {
                // Extract the date from the file name
                string fileName = Path.GetFileNameWithoutExtension(logFile);
                if (DateTime.TryParse(fileName, out DateTime logDate))
                {
                    // Check if the log file is older than the retention period
                    if ((DateTime.Now - logDate).TotalDays > retentionDays)
                    {
                        File.Delete(logFile);
                        Console.WriteLine($"Deleted old log: {logFile}");
                    }
                }
            }
        }

        // Utility method to log the start of a block
        public void LogStart(string blockName)
        {
            Log($"[START] Execution started for: {blockName}");
        }

        // Utility method to log the end of a block
        public void LogEnd(string blockName)
        {
            Log($"[END] Execution finished for: {blockName}");
        }
    }
}
