using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMember.memberlog
{
    public class DatabaseLog : AppSettings<DatabaseLog>
    {

        public string connectionName = "liveConn";

        public string ServerName;
        public string UserName;
        public string Password;
        public string LiveDB;
        public string Testing_Database;
        public string Backupdb;
        public string DatabaseIs = "";
        public string Status = "";
        public string LocalDB = "";
        public int LocalCheck = 0;
    }

}
