using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMember.memberlog
{
    public class AdminLog : AppSettings<AdminLog>
    {
        public string client_chongi;
        public string client_commission;
        public string customer_chongi;
        public string customer_commission;
        public string labour;
        public string munshiana;
        public string product_id;
        public string product;
        public string weight_id;
        public string weight;
        public string _rent_per_product;
        public string _pack;
        public string marketfee;



        private string default_DIR = @"C:\";
        private string saleDir = @"\ArthiApp\";
        private string salesProccessedFolder = @"Sales\Processed\";//Not Updated
        private string salesInProccessedFolder = @"Sales\DailySales\";//Updates
        private string backupPath = @"Backup\";
        private string reportDir = @"Reports\";
       

        //private string DebugFolder=@"Debug\";
        //private string LiveFolder = @"Live\";
        //private string isLive()
        //{
        //    if (System.Diagnostics.Debugger.IsAttached)
        //    {
        //        return DebugFolder;
        //    }
        //    else
        //    {
        //        return LiveFolder;
        //    }
        //}
        public string getIsLiveOrTest()
        {
            DatabaseLog dbl = DatabaseLog.Load("database.json");

            string check = dbl.Status;
            string path = "";
            if (check == "Live")
            {
                path = default_DIR + saleDir + check + "\\";
            }
            else
            {
                path = default_DIR + saleDir + check + "\\";
            }
            if (!Directory.Exists(path) && path != "")
            {
                Directory.CreateDirectory(path);
            }

            return saleDir;
        }


        public string DefultDIR
        {
            get { return default_DIR; }
            set { this.default_DIR = value; }
        }
        public string SaleProcessedDir
        {
            get { return default_DIR + saleDir + /*isLive() +*/ salesProccessedFolder; }
        }
        public string SalesInProccessedFolder
        {
            get { return default_DIR + saleDir + /*isLive() +*/salesInProccessedFolder; }
        }
        public string BackupPath
        {
            get { return default_DIR + saleDir + /*isLive() +*/ backupPath; }
        }
        public string ReportPath
        {
            get { return default_DIR + saleDir + /*isLive() +*/ reportDir; }
        }


    }

}
