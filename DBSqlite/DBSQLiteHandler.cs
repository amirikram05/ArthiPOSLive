using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSqlite
{
    public class DBSQLiteHandler
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        public string ConnectionStringSQLITE
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConStrSqlite"].ConnectionString;

            }
        }
        private void SetConnection()
        {
            sql_con = new SQLiteConnection
                (ConnectionStringSQLITE);
        }
        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
        public DataTable LoadData(string query)
        {
            //string CommandText = "select id, desc from mains";
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            DB = new SQLiteDataAdapter(query, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            sql_con.Close();
            return DT;


        }
        public void Add(string txtSQLQuery)
        {
            //string txtSQLQuery = "insert into  mains (desc) values ('" + txtDesc.Text + "')";
            ExecuteQuery(txtSQLQuery);
        }
    }
}
