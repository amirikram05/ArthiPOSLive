using System;
using System.Configuration;
using System.IO;

namespace ArthiPOS.Core.Application
{
    public static class AppConfig
    {
        // Database
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
        public static string BackupConnectionString => ConfigurationManager.ConnectionStrings["BackupConnection"]?.ConnectionString;

        // Application
        public static string ApplicationName => "Arthi POS";
        public static Version Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        public static string CompanyName => "Arthi Technologies";

        // Paths
        public static string LogPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        public static string BackupPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
        public static string TempPath => Path.Combine(Path.GetTempPath(), "ArthiPOS");

        // Settings
        public static int SessionTimeout => GetInt("SessionTimeout", 30); // minutes
        public static int AutoSaveInterval => GetInt("AutoSaveInterval", 5); // minutes
        public static int MaxLoginAttempts => GetInt("MaxLoginAttempts", 3);

        // UI
        public static int AnimationDuration => GetInt("AnimationDuration", 300); // ms
        public static int TooltipDelay => GetInt("TooltipDelay", 500); // ms

        private static string GetString(string key, string defaultValue = "")
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }

        private static int GetInt(string key, int defaultValue)
        {
            return int.TryParse(ConfigurationManager.AppSettings[key], out int result) ? result : defaultValue;
        }

        private static bool GetBool(string key, bool defaultValue)
        {
            return bool.TryParse(ConfigurationManager.AppSettings[key], out bool result) ? result : defaultValue;
        }
    }
}