using ArthiPOS.Common.Utilities;
using ArthiPOS.Core.Application;
using ArthiPOS.Properties;
using CommonUtilities;
using DataMember;
using LogMaintain;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace ArthiPOS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        private static ILogger _logger;
        [STAThread]
        static void Main()
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string lang = LogUtill.getLanguageLog().language;
            if (lang != "" && lang != "Testing")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
            }
            try
            {
                string jsonFilePath = "database.json";
                string jsonContent = File.ReadAllText(jsonFilePath);
                DatabaseConfig config = JsonConvert.DeserializeObject<DatabaseConfig>(jsonContent);

                RegistryAccess.SetStringRegistryValue("DBStatus", config.Status);
                RegistryAccess.SetStringRegistryValue("DBString", config.connectionName);


            }
            catch (Exception e)
            {
                MessageBox.Show("Application Not Register.");
                Application.Run(new Authentication());
            }




            // Set UI thread culture
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;

            // Enable visual styles
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Setup exception handling
            SetupExceptionHandling();

            try
            {
                // Initialize application
                InitializeApplication();

                // Run main form
                Application.Run(new Authentication(1));

            }
            catch (Exception ex)
            {
                HandleFatalError(ex);
            }
            finally
            {
                Cleanup();
            }




        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            Debug.WriteLine(e.Exception.Message);
            ExceptionLogging.SendErrorToText(e.Exception);
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Log the exception, display it, etc

        }
        static ResourceManager resourceMan;
        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp =
                        new global::System.Resources.ResourceManager("ArthiPOS.Properties.Resources",
                        typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }



        /**********************************/
        private static void SetupExceptionHandling()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += OnThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        private static void InitializeApplication()
        {
            // Initialize logger
            _logger = new FileLogger();
            _logger.Info("Application starting...");

            // Create required directories
            CreateApplicationDirectories();

            // Check for single instance
            EnsureSingleInstance();

            // Load configuration
            LoadConfiguration();

            _logger.Info("Application initialized successfully");
        }

        private static void CreateApplicationDirectories()
        {
            try
            {
                System.IO.Directory.CreateDirectory(AppConfig.LogPath);
                System.IO.Directory.CreateDirectory(AppConfig.BackupPath);
                System.IO.Directory.CreateDirectory(AppConfig.TempPath);
            }
            catch (Exception ex)
            {
                _logger?.Error("Failed to create application directories", ex);
            }
        }

        private static void EnsureSingleInstance()
        {
            bool createdNew;
            var mutex = new System.Threading.Mutex(true, "ArthiPOS-SingleInstance", out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("Another instance of Arthi POS is already running.",
                    "Application Already Running", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }
        }

        private static void LoadConfiguration()
        {
            try
            {
                // Load settings from registry/config file
                // Initialize AppState
            }
            catch (Exception ex)
            {
                _logger?.Error("Failed to load configuration", ex);
            }
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            _logger?.Error("Unhandled thread exception", e.Exception);
            ShowErrorDialog(e.Exception);
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            _logger?.Fatal("Unhandled application exception", exception);
            ShowErrorDialog(exception);
        }

        private static void HandleFatalError(Exception ex)
        {
            _logger?.Fatal("Fatal application error", ex);
            ShowErrorDialog(ex, true);
        }

        private static void ShowErrorDialog(Exception ex, bool isFatal = false)
        {
            string message = isFatal
                ? $"A fatal error occurred and the application must close:\n{ex?.Message}"
                : $"An error occurred:\n{ex?.Message}";

            string caption = isFatal ? "Fatal Error" : "Error";

            MessageBox.Show(message, caption, MessageBoxButtons.OK,
                isFatal ? MessageBoxIcon.Error : MessageBoxIcon.Warning);
        }

        private static void Cleanup()
        {
            try
            {
                _logger?.Info("Application shutting down...");

                // Cleanup AppState
                AppState.Instance?.Dispose();

                // Dispose logger
                (_logger as IDisposable)?.Dispose();

                // Clean temp directory
                CleanTempDirectory();
            }
            catch (Exception ex)
            {
                // Last chance logging
                System.Diagnostics.Debug.WriteLine($"Cleanup error: {ex.Message}");
            }
        }

        private static void CleanTempDirectory()
        {
            try
            {
                if (System.IO.Directory.Exists(AppConfig.TempPath))
                {
                    var files = System.IO.Directory.GetFiles(AppConfig.TempPath);
                    foreach (var file in files)
                    {
                        try
                        {
                            System.IO.File.Delete(file);
                        }
                        catch
                        {
                            // Ignore deletion errors
                        }
                    }
                }
            }
            catch
            {
                // Ignore cleanup errors
            }
        }

    }
}
