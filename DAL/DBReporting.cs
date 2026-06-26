using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DBReporting
    {
        public static string Connection
        {
            get { return GeneralConst.ConnectionSTring; }
        }
        private SqlDataAdapter adapt;
        private DataTable dt;

        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(GeneralConst.ConnectionSTring);
                conn.Open();
                return conn;
            }
            catch (SqlException ex)
            {
                return null;
            }

        }
        public bool CloseConnection(SqlConnection conn)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                // do something
                // ...
                conn.Close();
                conn.Dispose();
                return true;
            }
            return false;
        }
        public DataTable GetShopExpenseReport(string stardate, string lastdate, int section)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetShopExpenseReport", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@start_date", SqlDbType.NVarChar).Value = stardate;
                cmd.Parameters.Add("@end_date", SqlDbType.NVarChar).Value = lastdate;
                cmd.Parameters.Add("@section", SqlDbType.Int).Value = section;


                adapt = new SqlDataAdapter(cmd);

                dt = new DataTable();
                adapt.Fill(dt);
                if (dt == null && dt.Rows.Count == 0)
                {
                    CloseConnection(conn);
                    return null;
                }

                return dt;
            }
            return null;
        }
        public DataTable p_report_data_all(string action, string reportno, string search, string stardate, string lastdate, string filter)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("p_report_data_all", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date_start", SqlDbType.NVarChar).Value = stardate;
                cmd.Parameters.Add("@date_last", SqlDbType.NVarChar).Value = lastdate;
                cmd.Parameters.AddWithValue("@report_no", reportno);
                cmd.Parameters.AddWithValue("@search_name", @search);
                cmd.Parameters.AddWithValue("@filter", filter);

                adapt = new SqlDataAdapter(cmd);

                dt = new DataTable();
                adapt.Fill(dt);
                if (dt == null && dt.Rows.Count == 0)
                {
                    CloseConnection(conn);
                    return null;
                }

                return dt;
            }
            return null;
        }
        #region Reporting
        public object p_reporting_CRUD(string @action, string @stardate, string @lastdate, int pageIndex, int PageSize, string @search)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("dbo.p_reporting_CRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = stardate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = lastdate;
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.AddWithValue("@search", @search);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                adapt = new SqlDataAdapter(cmd);

                dt = new DataTable();
                adapt.Fill(dt);
                int recordCount = 0;//Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                if (dt == null && dt.Rows.Count == 0)
                {
                    CloseConnection(conn);
                    return null;
                }

                List<Object> obj = new List<object>();
                obj.Add(recordCount);
                obj.Add(dt);
                CloseConnection(conn);
                return obj;
            }
            return null;
        }

        public DataTable p_all_sale_profit_details(string action, string sdate, string ldate)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("dbo.p_all_sale_profit_details", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@start_date", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@last_date", SqlDbType.NVarChar).Value = ldate;
                adapt = new SqlDataAdapter(cmd);

                dt = new DataTable();
                adapt.Fill(dt);
                CloseConnection(conn);
                return dt;

            }
            return null;
        }

        public List<object> p_dailyProfitSalesExpense(string action, string @stardate, string @lastdate, int pageIndex, int PageSize)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("dbo.p_dailyProfitSalesExpense", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = stardate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = lastdate;
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                adapt = new SqlDataAdapter(cmd);

                dt = new DataTable();
                adapt.Fill(dt);
                int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                List<Object> obj = new List<object>();
                obj.Add(recordCount);
                obj.Add(dt);
                CloseConnection(conn);
                return obj;

            }
            return null;

        }
        #endregion
    }
}
