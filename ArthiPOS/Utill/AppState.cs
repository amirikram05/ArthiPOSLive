using System;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.Win32;

namespace ArthiPOS.Core.Application
{
    // User Session class
    public class UserSession
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }
        public string Token { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LastActivity { get; set; }
        public Dictionary<string, object> Permissions { get; set; }

        public UserSession()
        {
            Permissions = new Dictionary<string, object>();
            LoginTime = DateTime.Now;
        }
    }

    public enum UserRole
    {
        Admin,
        Manager,
        Cashier,
        Viewer
    }

    public enum AppTheme
    {
        Light,
        Dark,
        Blue,
        Green
    }

    // AppState Singleton
    public sealed class AppState : IDisposable
    {
        // Singleton instance with proper thread safety
        private static AppState _instance;
        private static readonly object _lockObject = new object();
        private bool _disposed = false;

        public static AppState Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new AppState();
                        }
                    }
                }
                return _instance;
            }
        }

        // Application State Properties
        public string CurrentDate { get; set; }
        public string DatabaseStatus { get; set; }
        public CultureInfo CurrentCulture { get; set; }
        public UserSession CurrentUser { get; set; }
        public bool IsOfflineMode { get; set; }
        public AppTheme CurrentTheme { get; set; }

        // Business State
        public decimal CashInHand { get; set; }
        public DateTime BusinessDate { get; set; }
        public bool IsShiftOpen { get; set; }

        // Settings
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string LicenseKey { get; set; }
        public DateTime LicenseExpiry { get; set; }

        // UI State
        public string CurrentModule { get; set; }
        public string LastErrorMessage { get; set; }
        public bool IsBusy { get; set; }

        // Events
        public event EventHandler<UserSessionChangedEventArgs> UserSessionChanged;
        public event EventHandler<CultureChangedEventArgs> CultureChanged;
        public event EventHandler<DatabaseStatusChangedEventArgs> DatabaseStatusChanged;
        public event EventHandler<ThemeChangedEventArgs> ThemeChanged;

        // Private constructor for singleton
        private AppState()
        {
            Initialize();
        }

        private void Initialize()
        {
            // Set defaults
            CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
            BusinessDate = DateTime.Today;
            CurrentCulture = CultureInfo.CurrentCulture;
            CurrentTheme = AppTheme.Light;
            IsOfflineMode = false;
            CashInHand = 0;
            IsShiftOpen = false;

            // Load settings from registry
            LoadSettings();
        }

        // Load settings from registry
        private void LoadSettings()
        {
            try
            {
                CompanyName = GetRegistryValue("CompanyName", "Arthi POS");
                BranchName = GetRegistryValue("BranchName", "Main Branch");
                LicenseKey = GetRegistryValue("LicenseKey", "");

                string expiry = GetRegistryValue("LicenseExpiry", "");
                if (DateTime.TryParse(expiry, out DateTime expiryDate))
                    LicenseExpiry = expiryDate;
                else
                    LicenseExpiry = DateTime.MaxValue;

                // Load theme
                string theme = GetRegistryValue("Theme", "Light");
                if (Enum.TryParse<AppTheme>(theme, out AppTheme appTheme))
                    CurrentTheme = appTheme;

                // Load culture
                string culture = GetRegistryValue("Culture", "en-US");
                CurrentCulture = CultureInfo.GetCultureInfo(culture);
            }
            catch (Exception ex)
            {
                LastErrorMessage = $"Failed to load settings: {ex.Message}";
                // Use defaults
            }
        }

        // Save settings to registry
        public void SaveSettings()
        {
            try
            {
                SetRegistryValue("CompanyName", CompanyName);
                SetRegistryValue("BranchName", BranchName);
                SetRegistryValue("LicenseKey", LicenseKey);
                SetRegistryValue("LicenseExpiry", LicenseExpiry.ToString("yyyy-MM-dd"));
                SetRegistryValue("Theme", CurrentTheme.ToString());
                SetRegistryValue("Culture", CurrentCulture.Name);

                // Save any other settings...
            }
            catch (Exception ex)
            {
                LastErrorMessage = $"Failed to save settings: {ex.Message}";
                throw;
            }
        }

        // User session management
        public void SetUserSession(UserSession session)
        {
            var oldUser = CurrentUser;
            CurrentUser = session;
            UserSessionChanged?.Invoke(this,
                new UserSessionChangedEventArgs(oldUser, session));

            // Update last activity
            if (session != null)
            {
                session.LastActivity = DateTime.Now;
            }
        }

        public void UpdateUserActivity()
        {
            if (CurrentUser != null)
            {
                CurrentUser.LastActivity = DateTime.Now;
            }
        }

        // Culture management
        public void SetCulture(CultureInfo culture)
        {
            var oldCulture = CurrentCulture;
            CurrentCulture = culture;
            CultureChanged?.Invoke(this,
                new CultureChangedEventArgs(oldCulture, culture));

            // Save to registry
            SetRegistryValue("Culture", culture.Name);
        }

        // Database status
        public void SetDatabaseStatus(string status)
        {
            var oldStatus = DatabaseStatus;
            DatabaseStatus = status;
            DatabaseStatusChanged?.Invoke(this,
                new DatabaseStatusChangedEventArgs(oldStatus, status));
        }

        // Theme management
        public void SetTheme(AppTheme theme)
        {
            var oldTheme = CurrentTheme;
            CurrentTheme = theme;
            ThemeChanged?.Invoke(this,
                new ThemeChangedEventArgs(oldTheme, theme));

            // Save to registry
            SetRegistryValue("Theme", theme.ToString());
        }

        // Business methods
        public bool IsLicenseValid()
        {
            if (string.IsNullOrEmpty(LicenseKey))
                return false;

            return LicenseExpiry > DateTime.Now;
        }

        public int DaysUntilLicenseExpiry()
        {
            if (LicenseExpiry == DateTime.MaxValue)
                return int.MaxValue;

            return (int)(LicenseExpiry - DateTime.Now).TotalDays;
        }

        public void OpenShift(decimal openingBalance)
        {
            CashInHand = openingBalance;
            IsShiftOpen = true;
        }

        public void CloseShift()
        {
            IsShiftOpen = false;
            // You might want to save shift data here
        }

        public void AddToCash(decimal amount)
        {
            CashInHand += amount;
        }

        public void SubtractFromCash(decimal amount)
        {
            CashInHand -= amount;
        }

        // Reset application state (for logout)
        public void Reset()
        {
            CurrentUser = null;
            CashInHand = 0;
            IsShiftOpen = false;
            CurrentModule = null;
            LastErrorMessage = null;
            IsBusy = false;
        }

        // Registry helper methods
        private string GetRegistryValue(string key, string defaultValue)
        {
            try
            {
                using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\ArthiPOS"))
                {
                    if (regKey != null)
                    {
                        object value = regKey.GetValue(key);
                        return value?.ToString() ?? defaultValue;
                    }
                }
            }
            catch
            {
                // Ignore registry errors
            }
            return defaultValue;
        }

        private void SetRegistryValue(string key, string value)
        {
            try
            {
                using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(@"Software\ArthiPOS"))
                {
                    if (regKey != null)
                    {
                        regKey.SetValue(key, value, RegistryValueKind.String);
                    }
                }
            }
            catch
            {
                // Ignore registry errors
            }
        }

        // Cleanup
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    SaveSettings();
                }
                _disposed = true;
            }
        }

        ~AppState()
        {
            Dispose(false);
        }
    }

    // Event Arguments Classes (already defined earlier, but including for completeness)
    public class UserSessionChangedEventArgs : EventArgs
    {
        public UserSession OldUser { get; set; }
        public UserSession NewUser { get; set; }

        public UserSessionChangedEventArgs(UserSession oldUser, UserSession newUser)
        {
            OldUser = oldUser;
            NewUser = newUser;
        }
    }

    public class CultureChangedEventArgs : EventArgs
    {
        public CultureInfo OldCulture { get; }
        public CultureInfo NewCulture { get; }

        public CultureChangedEventArgs(CultureInfo oldCulture, CultureInfo newCulture)
        {
            OldCulture = oldCulture;
            NewCulture = newCulture;
        }
    }

    public class DatabaseStatusChangedEventArgs : EventArgs
    {
        public string OldStatus { get; }
        public string NewStatus { get; }

        public DatabaseStatusChangedEventArgs(string oldStatus, string newStatus)
        {
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }
    }

    public class ThemeChangedEventArgs : EventArgs
    {
        public AppTheme OldTheme { get; }
        public AppTheme NewTheme { get; }

        public ThemeChangedEventArgs(AppTheme oldTheme, AppTheme newTheme)
        {
            OldTheme = oldTheme;
            NewTheme = newTheme;
        }
    }
}