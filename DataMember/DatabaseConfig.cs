using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMember
{
    public class DatabaseConfig
    {
        public string connectionName { get; set; }
        public string ServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LiveDB { get; set; }
        public string Testing_Database { get; set; }
        public string Backupdb { get; set; }
        public string DatabaseIs { get; set; }
        public string Status { get; set; }
    }
}
