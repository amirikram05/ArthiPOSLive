using ArthiPOS.controls;
using ArthiPOS.Properties;
using ArthiPOS.utill;
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
        [STAThread]
        static void Main()
        {
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string lang = LogUtill.getLanguageLog().language;
            if (lang!="" && lang != "Testing")
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
                Application.Run(new Authentication());


            }
            catch (Exception e)
            {
                MessageBox.Show("Application Not Register.");
                Application.Run(new Authentication());
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

    }
}
