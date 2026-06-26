
using Microsoft.Win32;
using System;

namespace DAL
{
    public class GeneralConst
    {
        /*public static string connectingDBString()
        {
            string jsonFilePath = "database.json";
            string jsonContent = File.ReadAllText(jsonFilePath);
            DatabaseConfig config = JsonConvert.DeserializeObject<DatabaseConfig>(jsonContent);
            ConName = config.Status;
            return config.connectionName;
        }*/

        //public static string ConName = "liveConn";//LiveDB
        public static string ConName = "C"; //Testing DB

        // public static string ConnectionSTring = ConfigurationManager.ConnectionStrings["c"].ConnectionString;
        //LocalDb
        public static string ConnectionSTring
        {
            get
            {
                try
                {

                    //#if DEBUG//SqlSB
                    //return ConfigurationManager.ConnectionStrings["c"].ConnectionString;
                    //#else
                    //ServerDB
                    //return ConfigurationManager.ConnectionStrings[ConName].ConnectionString;
                    ConName = GetStringRegistryValue("DBStatus", "");
                    string conn = GetStringRegistryValue("DBString", "");
                    return conn;
                }
                catch (NullReferenceException e)
                {
                    return "";
                }
                //LocalFileDB
                //return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; //LocalDb
                //#endif

            }

        }
        static public string GetStringRegistryValue(string key, string defaultValue)
        {
            RegistryKey rkCompany;
            RegistryKey rkApplication;

            rkCompany = Registry.CurrentUser.OpenSubKey("SOFTWARE", false).OpenSubKey("Awrika", false);
            if (rkCompany != null)
            {
                rkApplication = rkCompany.OpenSubKey("Arthi-App", true);
                if (rkApplication != null)
                {
                    foreach (string sKey in rkApplication.GetValueNames())
                    {
                        if (sKey == key)
                        {
                            return (string)rkApplication.GetValue(sKey);
                        }
                    }
                }
            }
            return defaultValue;
        }

    }
}
