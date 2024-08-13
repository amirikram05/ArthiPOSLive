using System;
using System.IO;
using System.Web.Script.Serialization;
using DataMember.memberlog;
using System.Configuration;

namespace CommonUtilities
{
    public class LogUtill
    {
        public static void loadLastUseInputs_TransportForm(string bipari_chongi, string client_commission, string customer_chongi, string customer_commission, string labour
            ,string munshiana, string product_id, string product
            , string weight_id, string weight,string _rent_per_product,string pack,string marketfee)
        {
            AdminLog log = AdminLog.Load();
            log.client_chongi = bipari_chongi;
            log.customer_chongi = customer_chongi;
            log.client_commission = client_commission;
            log.customer_commission = customer_commission;
            log.labour = labour;
            log.munshiana = munshiana;
            log.product_id = product_id;
            log.product = product;
            log.weight_id = weight_id;
            log.weight = weight;
            log._rent_per_product = _rent_per_product;
            log._pack = pack;
            log.marketfee = marketfee;
            log.Save();
        }
        public static void defaultDir(string dirPath)
        {
            AdminLog log = AdminLog.Load();
            log.DefultDIR = dirPath;
            log.Save();
        }
        
        /*public static void loadLastUseInputs_AccountForm(string shop_name,string username, string api_key,string address,string properiters,
            string phone,int capital_Cash,string name1,string phone1,string name2,string phone2)
        {
            AccountLog log = AccountLog.Load();
            log.shop_name = shop_name;
            log.username = username;
            log.api_key = api_key;
            log.address = address;
            log.propriters_name = properiters;
            log.phone = phone;
            log.capital_Cash = capital_Cash;
            log.Name1 = name1;
            log.Name2= name2;
            log.Phone1= phone1;
            log.Phone2 = phone2;
            log.Save("accounts.json");
        }*/
        /*public static void loadLastUseInputs_AccountForm(string shop_name, string username, string api_key, string address, string properiters,
            string phone, int capital_Cash, int loginCount)
        {
            AccountLog log = AccountLog.Load();
            log.shop_name = shop_name;
            log.username = username;
            log.api_key = api_key;
            log.address = address;
            log.propriters_name = properiters;
            log.phone = phone;
            log.capital_Cash = capital_Cash;
            log.loginCount = loginCount;
            log.Save("accounts.json");
        }*/
        public static int LoginCount
        {
            get {
                AccountLog log = AccountLog.Load("accounts.json");
                return log.loginCount;
            }
            set
            {
                AccountLog log = AccountLog.Load("accounts.json");
                log.loginCount = value;
            }
        }

        public static void setSorSearch(string id,string type)
        {
            AccountLog log = AccountLog.Load();
            log.sortmostBy = id;
            log.sortType = type;
            log.Save();
        }
        public static string[] getSorSearch()
        {
            AccountLog log = AccountLog.Load();
            return new string[] { log.sortmostBy,log.sortType};
        }



        public static AdminLog getAdminInputLog()
        {
            return AdminLog.Load();
        }
        /*public static AccountLog getAccountInputLog()
        {
            return AccountLog.Load("accounts.json");
        }*/

        public static void loadLastLanguage(string strCulture)
        {
            LanguageLog log = LanguageLog.Load();
            log.language = strCulture;
            log.Save("language.json");
        }
        public static LanguageLog getLanguageLog()
        {
            return LanguageLog.Load("language.json");
        }
        public static void loadDBConfig(string servername,string username,string password,string livedb,
            string backup,string connname,string testing_db,string currentDB,string localdb,int localCheck)
        {
            DatabaseLog db = DatabaseLog.Load();
            db.ServerName = servername;
            db.UserName = username;
            db.Password = password;

            db.LiveDB = livedb;
            db.Backupdb = backup;
            db.Testing_Database = testing_db;
            db.LocalDB = localdb;
            db.DatabaseIs = currentDB;
            db.LocalCheck = localCheck ;
            if(localCheck == 1)
            {
                db.connectionName = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            }
            else
            {
                db.connectionName = $"Data Source = {servername}; Initial Catalog = {currentDB}; Persist Security Info = True; User ID = {username}; Password = {password}";
            }
            if(localCheck==1)
                db.Status = currentDB == "db_pt" ? "Testing" : "Live_Local";
            else
                db.Status = currentDB == "db_pt" ? "Testing" : "Live";

            db.Save("database.json");
        }

        public static void loadDatabase(DatabaseLog db)
        {
            DatabaseLog log = DatabaseLog.Load();
            db.connectionName = $"Data Source = {db.ServerName}; Initial Catalog = {db.DatabaseIs}; Persist Security Info = True; User ID = {db.UserName}; Password = {db.Password}";
            log = db;
            log.Save("database.json");
        }
        public static DatabaseLog getDatabaseLog()
        {
            return DatabaseLog.Load("database.json");
        }
    }
    //public class LanguageLog : AppSettings<LanguageLog>
    //{
    //    public string language= "en-US";
    //}


    //public class DatabaseLog : AppSettings<DatabaseLog>
    //{

    //    public string connectionName = "liveConn";

    //    public string ServerName;
    //    public string UserName;
    //    public string Password;
    //    public string LiveDB;
    //    public string Testing_Database;
    //    public string Backupdb;
    //    public string DatabaseIs = "";
        
    //}
    //public class AccountLog : AppSettings<AccountLog>
    //{
        
    //    public string Name1;
    //    public string Name2;
    //    public string Phone1;
    //    public string Phone2;
    //    public string username;
    //    public string shop_name;
    //    public string address;
    //    public string phone;
    //    public string propriters_name;
    //    public int capital_Cash;
    //    public int loginCount = 0;
    //    public string api_key;
    //    public string sortmostBy;
    //    public string sortType;
    //}
    //public class AdminLog : AppSettings<AdminLog>
    //{
    //    public string client_chongi;
    //    public string client_commission;
    //    public string customer_chongi;
    //    public string customer_commission;
    //    public string labour;
    //    public string munshiana;
    //    public string product_id;
    //    public string product;
    //    public string weight_id;
    //    public string weight;
    //    public string _rent_per_product;
    //    public string _pack;


    //    private string default_DIR= @"C:\";
    //    private string saleDir= @"\ArthiApp\";
    //    private string salesProccessedFolder = @"Sales\Processed\";//Not Updated
    //    private string salesInProccessedFolder= @"Sales\DailySales\";//Updates
    //    private string backupPath= @"Backup\";

    //    //private string DebugFolder=@"Debug\";
    //    //private string LiveFolder = @"Live\";
    //    //private string isLive()
    //    //{
    //    //    if (System.Diagnostics.Debugger.IsAttached)
    //    //    {
    //    //        return DebugFolder;
    //    //    }
    //    //    else
    //    //    {
    //    //        return LiveFolder;
    //    //    }
    //    //}


    //    public string DefultDIR
    //    {
    //        get { return default_DIR; }
    //        set { this.default_DIR = value; }
    //    }
    //    public string SaleProcessedDir
    //    {
    //        get { return default_DIR + saleDir+ /*isLive() +*/ salesProccessedFolder; }
    //    }
    //    public string SalesInProccessedFolder
    //    {
    //        get { return default_DIR + saleDir + /*isLive() +*/salesInProccessedFolder; }
    //    }
    //    public string BackupPath
    //    {
    //        get { return default_DIR+ saleDir + /*isLive() +*/ backupPath; }
    //    }


    //}
    //public class AppSettings<T> where T : new()
    //{
    //    private const string DEFAULT_FILENAME = "settings.json";

    //    public void Save(string fileName = DEFAULT_FILENAME)
    //    {
    //        File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
    //    }

    //    public static void Save(T pSettings, string fileName = DEFAULT_FILENAME)
    //    {
    //        File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(pSettings));
    //    }

    //    public static T Load(string fileName = DEFAULT_FILENAME)
    //    {
    //        T t = new T();
    //        if (File.Exists(fileName))
    //            t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));
    //        return t;
    //    }
    //}
}
