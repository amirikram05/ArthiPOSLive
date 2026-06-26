using DataMember;
using LogMaintain;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.XEvent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DAL
{
    public class DBHandler
    {


        public static string Connection
        {
            get { return GeneralConst.ConnectionSTring; }
        }

        public int oldAmount { get; private set; }

        public DataTable dt_client, dt_customer, dt_product, dt_weight, dt_sale, dt_augrai, dt_fright, dt_expense;
        public SqlCommand cmd;
        public DataTable p_chatha(string action,string sdate, string ldate)
        {

            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return null;
                }
                SqlCommand cmd = new SqlCommand("p_chatha", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;


                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                CloseConnection(conn);

                return data_tbl;
            }
        }

        public DataTable p_daily_temp_table_crud(
    string action,
    string date,
    int zamidar_id,
    int bipari_id,
    int total_quantity,
    float total_rent,
    int total_labour,
    int total_advance,
    float bipcommission,
    int biplaga,
    float cust_commission,
    int cust_chongi,
    int munshiana,
    int marketfee,
    int cust_id,
    int quantity,
    int rate,
    int total_sale_amount,
    float grand_total,
    float bipari_grand_total,
    int extra_cust,
    int extra_vendour,
    int product_id,
    string product_name,
    string product_marka,
    int beg_weight_id,
    string beg_weight_name,
    string bipkey,
    string zamidarkey,
    string custkey,
    int weight_id,
    string weight_name,
    string billtype,
    int bikri_quantity = 0,
    int bikri_rate = 0,
    string vehicle_no = null,
    string bag_type = null,
    int crud = 1,
    float c_commission = 0,
    float bz_commission = 0,
    float laga_per_item = 0,
    float chongi_per_item = 0,
    float freight_per_item = 0,
    float labour_per_item = 0)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return null;
                }


                SqlCommand cmd = new SqlCommand("p_daily_temp_table_crud", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@zamidar_id", SqlDbType.Float).Value = zamidar_id;
                cmd.Parameters.Add("@bipari_id", SqlDbType.Float).Value = bipari_id;
                cmd.Parameters.Add("@total_quantity", SqlDbType.Float).Value = total_quantity;
                cmd.Parameters.Add("@total_rent", SqlDbType.Float).Value = total_rent;
                cmd.Parameters.Add("@total_labour", SqlDbType.Float).Value = total_labour;
                cmd.Parameters.Add("@total_advance", SqlDbType.Float).Value = total_advance;
                cmd.Parameters.Add("@bipcommission", SqlDbType.Float).Value = bipcommission;
                cmd.Parameters.Add("@biplaga", SqlDbType.Float).Value = biplaga;
                cmd.Parameters.Add("@cust_commission", SqlDbType.Float).Value = cust_commission;
                cmd.Parameters.Add("@cust_chongi", SqlDbType.Float).Value = cust_chongi;
                cmd.Parameters.Add("@munshiana", SqlDbType.Float).Value = munshiana;
                cmd.Parameters.Add("@marketfee", SqlDbType.Float).Value = marketfee;
                cmd.Parameters.Add("@cust_id", SqlDbType.Float).Value = cust_id;
                cmd.Parameters.Add("@quantity", SqlDbType.Float).Value = quantity;
                cmd.Parameters.Add("@rate", SqlDbType.Float).Value = rate;
                cmd.Parameters.Add("@total_sale_amount", SqlDbType.Float).Value = total_sale_amount;
                cmd.Parameters.Add("@grand_total", SqlDbType.Float).Value = grand_total;
                cmd.Parameters.Add("@bipari_grand_total", SqlDbType.Float).Value = bipari_grand_total;
                cmd.Parameters.Add("@extra_cust", SqlDbType.Float).Value = extra_cust;
                cmd.Parameters.Add("@extra_vendour", SqlDbType.Float).Value = extra_vendour;
                cmd.Parameters.Add("@product_id", SqlDbType.Float).Value = product_id;
                cmd.Parameters.Add("@product_name", SqlDbType.NVarChar).Value = product_name;
                cmd.Parameters.Add("@product_marka", SqlDbType.NVarChar).Value = product_marka;
                cmd.Parameters.Add("@beg_weight_id", SqlDbType.Float).Value = beg_weight_id;
                cmd.Parameters.Add("@beg_weight_name", SqlDbType.NVarChar).Value = beg_weight_name;
                cmd.Parameters.Add("@bipkey", SqlDbType.NVarChar).Value = bipkey;
                cmd.Parameters.Add("@zamidarkey", SqlDbType.NVarChar).Value = zamidarkey;
                cmd.Parameters.Add("@custkey", SqlDbType.NVarChar).Value = custkey;
                cmd.Parameters.Add("@weight_id", SqlDbType.Float).Value = weight_id;
                cmd.Parameters.Add("@weight_name", SqlDbType.NVarChar).Value = weight_name;
                cmd.Parameters.Add("@billtype", SqlDbType.NVarChar).Value = billtype;
                cmd.Parameters.Add("@bikri_quantity", SqlDbType.Float).Value = bikri_quantity;
                cmd.Parameters.Add("@bikri_rate", SqlDbType.Float).Value = bikri_rate;
                cmd.Parameters.Add("@vehicle_no", SqlDbType.NVarChar).Value = vehicle_no;
                cmd.Parameters.Add("@bag_type", SqlDbType.NVarChar).Value = bag_type;
                cmd.Parameters.Add("@c_commission ", SqlDbType.NVarChar).Value = c_commission;
                cmd.Parameters.Add("@bz_commission ", SqlDbType.NVarChar).Value = bz_commission;
                cmd.Parameters.Add("@laga_per_item ", SqlDbType.NVarChar).Value = labour_per_item;
                cmd.Parameters.Add("@chongi_per_item  ", SqlDbType.NVarChar).Value = chongi_per_item;
                cmd.Parameters.Add("@freight_per_item ", SqlDbType.NVarChar).Value = freight_per_item;
                cmd.Parameters.Add("@labour_per_item", SqlDbType.NVarChar).Value = labour_per_item;


                if (crud != 4) // INSERT, UPDATE, DELETE
                {
                    int rowsAffected = executeQueryCommand(cmd);
                    CloseConnection(conn);

                    return rowsAffected > 0 ? new DataTable() : null;

                }
                else // SELECT
                {

                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    DataTable data_tbl = new DataTable();
                    adapt.Fill(data_tbl);
                    CloseConnection(conn);

                    return data_tbl;
                }
            }
        }



        public DataTable p_balancesheet_Read(string action, string sdate, string ldate)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return null;
                }
                SqlCommand cmd = new SqlCommand("p_balancesheet_Read", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;

                if (action == "UPbs")
                {
                    int rowsAffected = executeQueryCommand(cmd);
                    if (rowsAffected != 0)
                    {
                        CloseConnection(conn);
                        return new DataTable();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    adapt = new SqlDataAdapter(cmd);

                    DataTable data_tbl = new DataTable();
                    adapt.Fill(data_tbl);
                    CloseConnection(conn);
                    return data_tbl;
                }
            }
        }


        public DataTable p_acc_transcation_crud(string action, int crud, string name, string urduname, int transid = 0, int id = 0)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return null;
                }
                SqlCommand cmd = new SqlCommand("p_acc_transcation_crud", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@crud", SqlDbType.NVarChar).Value = crud;
                cmd.Parameters.Add("@engname", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@urduname", SqlDbType.NVarChar).Value = urduname;
                cmd.Parameters.Add("@transid", SqlDbType.NVarChar).Value = transid;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;


                if (crud != 4)
                {
                    int rowsAffected = executeQueryCommand(cmd);
                    if (rowsAffected != 0)
                    {
                        CloseConnection(conn);
                        return new DataTable();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    adapt = new SqlDataAdapter(cmd);

                    DataTable data_tbl = new DataTable();
                    adapt.Fill(data_tbl);
                    CloseConnection(conn);
                    return data_tbl;
                }
            }
        }

        public object createSeason(string action, string sdate, string ldate)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return false;
                }
                SqlCommand cmd = new SqlCommand("p_season_CRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;
                List<Object> obj = new List<object>();


                if (action == "Create" || action == "Delete")
                {
                    int rowsAffected = executeQueryCommand(cmd);


                    if (rowsAffected != 0)
                    {
                        obj.Add(rowsAffected);
                        obj.Add(null);
                    }
                    else
                    {
                        obj.Add(rowsAffected);
                        obj.Add(null);
                    }
                    CloseConnection(conn);

                    return obj;



                }
                else if (action == "Read")
                {
                    adapt = new SqlDataAdapter(cmd);

                    DataTable data_tbl = new DataTable();
                    adapt.Fill(data_tbl);
                    obj.Add(0);
                    obj.Add(data_tbl);
                    CloseConnection(conn);
                    return obj;
                }
                return null;

            }
        }

        public DataTable p_billingDetailPaid(string isCustomer, string idname, string sdate, string ldate, string status)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return null;
                }
                SqlCommand cmd = new SqlCommand("p_billingDetailPaid", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@isCustomer", SqlDbType.NVarChar).Value = isCustomer;
                cmd.Parameters.Add("@idname", SqlDbType.NVarChar).Value = idname;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = status;

                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                CloseConnection(conn);
                return data_tbl;
            }
        }

        public bool update_Bipariidprofile(string id, int bipari_id)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return false;
                }
                SqlCommand cmd = new SqlCommand("p_update_Bipariidprofile", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@zamidar_id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@bipari_id", SqlDbType.Int).Value = bipari_id;


                int rowsAffected = executeQueryCommand(cmd);
                if (rowsAffected != 0)
                {
                    CloseConnection(conn);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public DataTable getCashInout(string action, string date)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return null;
                }
                SqlCommand cmd = new SqlCommand("p_cashinout_exe", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;

                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                CloseConnection(conn);
                return data_tbl;
            }
        }
        public bool p_cashinout_Crud(string action, string keyid, string date, string catename, int cateid, int transactionid, int account_transaction_id
            , int typeid, string cash_type, int uid, string uname, string detialdesp,
            int amount, int discount, string entrytype, string category_id, string action_type)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return false;
                }
                SqlCommand cmd = new SqlCommand("p_cashinout_Crud", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@entrytype", SqlDbType.NVarChar).Value = entrytype;
                cmd.Parameters.Add("@discount", SqlDbType.Int).Value = discount;
                cmd.Parameters.Add("@amount", SqlDbType.Int).Value = amount;
                cmd.Parameters.Add("@detialdesp", SqlDbType.NVarChar).Value = detialdesp;
                cmd.Parameters.Add("@uid", SqlDbType.Int).Value = uid;
                cmd.Parameters.Add("@uname", SqlDbType.NVarChar).Value = uname;
                cmd.Parameters.Add("@cash_type", SqlDbType.NVarChar).Value = cash_type;
                cmd.Parameters.Add("@typeid", SqlDbType.Int).Value = typeid;
                cmd.Parameters.Add("@account_transaction_id", SqlDbType.Int).Value = account_transaction_id;
                cmd.Parameters.Add("@transactionid", SqlDbType.Int).Value = transactionid;
                cmd.Parameters.Add("@cateid", SqlDbType.Int).Value = cateid;
                cmd.Parameters.Add("@catename", SqlDbType.NVarChar).Value = catename;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@keyid", SqlDbType.NVarChar).Value = keyid;
                cmd.Parameters.Add("@category_id", SqlDbType.NVarChar).Value = category_id;
                cmd.Parameters.Add("@action_type", SqlDbType.NVarChar).Value = action_type;


                int rowsAffected = executeQueryCommand(cmd);
                if (rowsAffected != 0)
                {
                    CloseConnection(conn);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool p_fin_BalanceSheet_CRUD(string action, string date, string transactionid,
            string acctransid, int cash, string inout)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return false;
                }
                SqlCommand cmd = new SqlCommand("p_fin_BalanceSheet_CRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@transactionid", SqlDbType.Int).Value = transactionid;
                cmd.Parameters.Add("@account_id", SqlDbType.Int).Value = acctransid;
                cmd.Parameters.Add("@amount", SqlDbType.Int).Value = cash;
                cmd.Parameters.Add("@Inout", SqlDbType.NVarChar).Value = inout;


                int rowsAffected = executeQueryCommand(cmd);
                if (rowsAffected != 0)
                {

                    CloseConnection(conn);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool p_update_editcustomersales(string date, string saleid, string newCustid)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return false;
                }
                SqlCommand cmd = new SqlCommand("p_update_editcustomersales", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@saleid", SqlDbType.NVarChar).Value = saleid;
                cmd.Parameters.Add("@changeinto_custId", SqlDbType.NVarChar).Value = newCustid;
                cmd.Parameters.Add("@newkey", SqlDbType.NVarChar).Value = p_getInvoiceID("Other", newCustid, date);

                int rowsAffected = executeQueryCommand(cmd);
                if (rowsAffected != 0)
                {
                    CloseConnection(conn);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public DataTable p_getDates()
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_getDates", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }
        public bool p_insert_date(string date)
        {
            try
            {

                using (SqlConnection conn = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("p_insert_date", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0;

                    int rowsAffected = executeQueryCommand(cmd);

                    if (rowsAffected != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;

                }
            }
            catch (SqlException ex)
            {
                Admin.LogExecMang.LogException(ex,"InsertDate");

                return false;
            }
            catch (InvalidArgumentException e)
            {
                Admin.LogExecMang.LogException(e, "InsertDate");

                return false;
            }

        }

        public SqlDataAdapter adapt;

        #region SQL
        private SqlTransaction trans;

        public bool ConnectionTesting()
        {
            using (SqlConnection connx = GetConnection())
            {
                if (connx == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private SqlConnection conn;
        public SqlConnection GetConnection()
        {
            try
            {
                if (GeneralConst.ConnectionSTring == "")
                    return null;

                SqlConnection conn = new SqlConnection(GeneralConst.ConnectionSTring);
                conn.InfoMessage += Conn_InfoMessage;
                conn.FireInfoMessageEventOnUserErrors = true;
                conn.Open();
                return conn;
            }
            catch (SqlException ex)
            {

                Admin.LogExecMang.LogException(ex, "Connection Eror");

                Console.WriteLine("An error occurred while establishing a connection to the SQL Server:");
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception e)
            {
                Admin.LogExecMang.LogException(e, "Conection Error");

                Console.WriteLine("An error occurred:");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public DataTable p_BillingPayingDetail(string action, string client_id, string sdate, string ldate, string status, string desc)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return null;
                }
                SqlCommand cmd = new SqlCommand("p_BillingPayingDetail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@client_id", SqlDbType.NVarChar).Value = client_id;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = status;
                cmd.Parameters.Add("@desc", SqlDbType.NVarChar).Value = desc;

                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                CloseConnection(conn);
                return data_tbl;
            }
        }
        public bool p_BillingPayingDetail(string action, string client_id, string key, string date, int amount, string desc)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return false;
                }
                SqlCommand cmd = new SqlCommand("p_BillingPayingDetail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@client_id", SqlDbType.NVarChar).Value = client_id;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = key;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@status", SqlDbType.Int).Value = amount;
                cmd.Parameters.Add("@desc", SqlDbType.NVarChar).Value = desc;

                int rowsAffected = executeQueryCommand(cmd);
                if (rowsAffected != 0)
                {
                    CloseConnection(conn);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool p_updateALLIDS()
        {
            try
            {


                using (SqlConnection conn = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("p_updateALLIDS", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int rowsAffected = executeQueryCommand(cmd);
                    if (rowsAffected != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;

                }
            }
            catch (SqlException ex)
            {
                Admin.LogExecMang.LogException(ex, "Update AllIDS");

                return false;
            }
        }
        public DataTable p_cashflow_SP(string sdate, string ldate)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_cashflow_SP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }
        public DataTable readFardHisab(string type, string id, string sdate, string ldate)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_fardhisab", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = type;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;
                cmd.Parameters.Add("@idname", SqlDbType.NVarChar).Value = id;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }


        public DataTable p_ledger_Read(string action, string sdate, string ldate)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_ledger_Read", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
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



        public bool backupDB(string path, int localCheck)
        {
            bool isDebug = false;
#if DEBUG
            isDebug = false;
#else
               isDebug=true;
#endif
            using (SqlConnection conn = GetConnection())
            {
                Admin.LogExecMang.LogStart("DB Backup Start");

                string db = getLiveDB();
                string database = conn.Database.ToString();
                string name = "";

                if (db == "Testing")
                {
                    name = "Testing";
                    path = path + "Test\\";

                }
                else
                {
                    if (localCheck == 1)
                        name = "Local_";
                    name += "Live";
                    path = path + "Live\\";

                }
                if (!isDebug)
                {
                    path = path + "Dev\\";
                    name = name + "-Dev";
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string version = System.Windows.Forms.Application.ProductVersion;
                name = name + " " + version;
                string query = "BACKUP DATABASE [" + database + "] TO DISK='"
                            + path + "\\" + name
                            //+ "database" 
                            + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";
                SqlCommand cmd = new SqlCommand(query, conn);
                executeQueryCommand(cmd);
                CloseConnection(conn);
                Admin.LogExecMang.LogEnd("DB Backup "+ query);
                return true;
            }
        }

        public object p_Category_CRUD(string action, string name, string id, string key)
        {
            try
            {


                using (SqlConnection conn = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("p_Category_CRUD", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                    cmd.Parameters.Add("@cate_name", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                    List<Object> obj = new List<object>();

                    if (action == "Read" || action == "ReadSear" || action == "CateDetail")
                    {
                        adapt = new SqlDataAdapter(cmd);
                        DataTable data_tbl = new DataTable();
                        adapt.Fill(data_tbl);

                        obj.Add(0);
                        obj.Add(data_tbl);
                        CloseConnection(conn);
                        return obj;
                    }
                    else
                    if (action == "Update")
                    {
                        int rowsAffected = executeQueryCommand(cmd);
                        if (rowsAffected != 0)
                        {
                            CloseConnection(conn);
                            obj.Add(rowsAffected);
                            obj.Add(null);
                        }
                    }
                    else
                    {
                        int rowsAffected = executeQueryCommand(cmd);
                        if (rowsAffected != 0)
                        {
                            CloseConnection(conn);
                            obj.Add(rowsAffected);
                            obj.Add(null);
                        }
                    }


                }
            }
            catch (SqlException ex)
            {
                Admin.LogExecMang.LogException(ex, "CategoryCrud");

                return null;
            }
            return null;
        }

        public bool passwordChange(string key, string oldpass, string newpass)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("p_account_passwordChange", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                cmd.Parameters.Add("@oldpass", SqlDbType.NVarChar).Value = oldpass;
                cmd.Parameters.Add("@newpass", SqlDbType.NVarChar).Value = newpass;
                /*int rowsAffected = executeQueryCommand(cmd);
                if (rowsAffected != 0)
                {
                    CloseConnection(conn);
                    return true;
                }*/

                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                DataRow row = data_tbl.Rows[0];
                string check = row[0].ToString();
                CloseConnection(conn);

                if (check == "1")
                    return true;
                else
                    return false;

            }
        }

        public string getLiveDB()
        {
            return GeneralConst.ConName;
        }

        public string getBackupLiveDB()
        {
            return GeneralConst.ConName;
        }

        public bool restoreDB(string path)
        {

            using (SqlConnection conn = GetConnection())
            {
                string database = conn.Database.ToString();
                string sqlStmt2 = string.Format(string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", database));
                string sqlStmt3 = string.Format("USE MASTER RESTORE DATABASE [{0}] FROM DISK='{1}'WITH REPLACE;", database, path);
                string sqlStmt4 = string.Format(string.Format("ALTER DATABASE [{0}] SET MULTI_USER", database));

                SqlCommand bu2 = new SqlCommand(sqlStmt2, conn);
                bu2.ExecuteNonQuery();

                SqlCommand bu3 = new SqlCommand(sqlStmt3, conn);
                bu3.ExecuteNonQuery();

                SqlCommand bu4 = new SqlCommand(sqlStmt4, conn);
                bu4.ExecuteNonQuery();

                CloseConnection(conn);
            }




            return true;

        }



        public DataTable p_accountCrud(string action, Account acc)
        {

            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return null;
                }
                if (acc == null) return null;
                SqlCommand cmd = new SqlCommand("p_account_CRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = acc.username;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = acc.email;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = acc.password;
                cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = acc.phone;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = acc.address;
                cmd.Parameters.Add("@propriters", SqlDbType.NVarChar).Value = acc.propriters_name;
                cmd.Parameters.Add("@licensekey", SqlDbType.NVarChar).Value = acc.license_no;
                cmd.Parameters.Add("@license_exp", SqlDbType.NVarChar).Value = acc.license_exp_date;
                cmd.Parameters.Add("@api_key", SqlDbType.NVarChar).Value = acc.api_key;
                cmd.Parameters.Add("@registrationkey_exp", SqlDbType.NVarChar).Value = acc.api_key_exp_date;
                cmd.Parameters.Add("@shop_name", SqlDbType.NVarChar).Value = acc.shop_name;
                cmd.Parameters.Add("@name1", SqlDbType.NVarChar).Value = acc.name1 ?? "";
                cmd.Parameters.Add("@phone1", SqlDbType.NVarChar).Value = acc.name2 == null ? "" : acc.phone1;
                cmd.Parameters.Add("@name2", SqlDbType.NVarChar).Value = acc.name2 ?? "";
                cmd.Parameters.Add("@phone2", SqlDbType.NVarChar).Value = acc.phone2 == null ? "" : acc.phone2;
                cmd.Parameters.Add("@business_type", SqlDbType.NVarChar).Value = acc.business_type == null ? "" : acc.business_type;
                cmd.Parameters.Add("@local_data", SqlDbType.NVarChar).Value = acc.local;
                cmd.Parameters.Add("@isdb", SqlDbType.Int).Value = int.Parse(acc.isdb);
                cmd.Parameters.Add("@account_closing", SqlDbType.NVarChar).Value = acc.accountclosing;
                cmd.Parameters.Add("@trade_mark", SqlDbType.NVarChar).Value = acc.trade_mark;
                cmd.Parameters.Add("@web_id", SqlDbType.NVarChar).Value = acc.web_id;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                CloseConnection(conn);
                return data_tbl;
            }
        }

        public DataTable p_today_totalDetails(string sdate, string ldate)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_today_totalDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }

        public void addParameter()
        {
            List<SqlParameter> prm = new List<SqlParameter>()
             {
                 new SqlParameter("@variable1", SqlDbType.Int) {Value = ""},
                 new SqlParameter("@variable2", SqlDbType.NVarChar) {Value = ""},
                 new SqlParameter("@variable3", SqlDbType.DateTime) {Value = ""},
             };
        }
        public DataTable p_pagetSettingLoad(string action)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_pageSetting", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@labour", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@rent", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@munshiana", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@laga", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@bip_comm", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@cust_chongi", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@cust_comm", SqlDbType.Int).Value = 0;

                adapt = new SqlDataAdapter(cmd);
                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }

        public bool p_pagetSetting(string action, int labour, int rent, int munshiana, int bip_commission, int bip_laga, int cust_commission, int cust_chongi)
        {
            try
            {


                using (SqlConnection conn = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("p_pageSetting", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                    cmd.Parameters.Add("@labour", SqlDbType.NVarChar).Value = labour;
                    cmd.Parameters.Add("@rent", SqlDbType.NVarChar).Value = rent;
                    cmd.Parameters.Add("@munshiana", SqlDbType.NVarChar).Value = munshiana;
                    cmd.Parameters.Add("@laga", SqlDbType.NVarChar).Value = bip_laga;
                    cmd.Parameters.Add("@bip_comm", SqlDbType.NVarChar).Value = bip_commission;
                    cmd.Parameters.Add("@cust_chongi", SqlDbType.NVarChar).Value = cust_chongi;
                    cmd.Parameters.Add("@cust_comm", SqlDbType.NVarChar).Value = cust_commission;

                    int rowsAffected = executeQueryCommand(cmd);
                    if (rowsAffected != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;

                }
            }
            catch (SqlException ex)
            {
                Admin.LogExecMang.LogException(ex, "Execption");

                return false;
            }
        }

        public DataTable dataBackupMove(string action, string sdate, string ldate, string detail)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("BackupDatabase_db_pt", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@start_date", sdate);
                    cmd.Parameters.AddWithValue("@last_date", ldate);
                    cmd.Parameters.AddWithValue("@detail", detail);

                    adapt = new SqlDataAdapter(cmd);
                    DataTable data_tbl = new DataTable();
                    adapt.Fill(data_tbl);
                    return data_tbl;

                }
            }
            catch (SqlException ex)
            {
                Admin.LogExecMang.LogException(ex, "Execption");

                return null;
            }
        }

        public bool dataBackupMove_Create(string action, string sdate, string ldate, string detail)
        {
            try
            {


                using (SqlConnection conn = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("BackupDatabase_db_pt", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                    cmd.Parameters.Add("@start_date", SqlDbType.NVarChar).Value = sdate;
                    cmd.Parameters.Add("@last_date", SqlDbType.NVarChar).Value = ldate;
                    cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = detail;

                    int rowsAffected = executeQueryCommand(cmd);
                    if (rowsAffected != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;

                }
            }
            catch (SqlException ex)
            {
                Admin.LogExecMang.LogException(ex, "Execption");

                return false;
            }
        }

        public bool updateAccount(string action, Account acc)
        {
            try
            {


                using (SqlConnection conn = GetConnection())
                {


                    SqlCommand cmd = new SqlCommand("p_account_CRUD", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = acc.username;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = acc.email;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = acc.password;
                    cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = acc.phone;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = acc.address;
                    cmd.Parameters.Add("@propriters", SqlDbType.NVarChar).Value = acc.propriters_name;
                    cmd.Parameters.Add("@licensekey", SqlDbType.NVarChar).Value = acc.license_no;
                    cmd.Parameters.Add("@license_exp", SqlDbType.NVarChar).Value = acc.license_exp_date;
                    cmd.Parameters.Add("@api_key", SqlDbType.NVarChar).Value = acc.api_key;
                    cmd.Parameters.Add("@registrationkey_exp", SqlDbType.NVarChar).Value = acc.api_key_exp_date;
                    cmd.Parameters.Add("@shop_name", SqlDbType.NVarChar).Value = acc.shop_name;
                    cmd.Parameters.Add("@name1", SqlDbType.NVarChar).Value = acc.name1;
                    cmd.Parameters.Add("@phone1", SqlDbType.NVarChar).Value = acc.phone1;
                    cmd.Parameters.Add("@name2", SqlDbType.NVarChar).Value = acc.name2;
                    cmd.Parameters.Add("@phone2", SqlDbType.NVarChar).Value = acc.phone2;
                    cmd.Parameters.Add("@business_type", SqlDbType.NVarChar).Value = acc.business_type;
                    cmd.Parameters.Add("@local_data", SqlDbType.NVarChar).Value = acc.local;
                    cmd.Parameters.Add("@isdb", SqlDbType.Int).Value = int.Parse(acc.isdb);
                    cmd.Parameters.Add("@account_closing", SqlDbType.NVarChar).Value = acc.accountclosing;
                    cmd.Parameters.Add("@trade_mark", SqlDbType.NVarChar).Value = acc.trade_mark;
                    cmd.Parameters.Add("@web_id", SqlDbType.NVarChar).Value = acc.web_id;



                    int rowsAffected = executeQueryCommand(cmd);
                    if (rowsAffected != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;

                }
            }
            catch (SqlException ex)
            {
                Admin.LogExecMang.LogException(ex, "Execption");

                return false;
            }
        }


        public SqlTransaction createTransaction(SqlConnection connection)
        {
            using (trans = connection.BeginTransaction())
            {
                return trans;
            }
        }

        /*public bool creatCommandExecuteBool(List<SqlParameter> param,SqlConnection connection)
        {
             using (var cmd = connection.CreateCommand())
             {
                cmd.Transaction = trans;
                cmd.Parameters.AddRange(param.ToArray());
                
                int rowsAffected = executeQueryCommand(cmd);
                if (rowsAffected != 0)
                {
                    return true;
                }


                return false;
            }

        }
        public int creatCommandExecuteInt(List<SqlParameter> param, SqlConnection connection)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.Transaction = trans;
                cmd.Parameters.AddRange(param.ToArray());
               
                int rowsAffected = executeQueryCommand(cmd);
                
                return rowsAffected;
            }

        }*/
        public SqlCommand creatCommandExecute(string sp, List<SqlParameter> param, SqlConnection connection, SqlTransaction trans)
        {
            using (var cmd = new SqlCommand(sp, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                cmd.Parameters.AddRange(param.ToArray());
                /* setup command type, text */
                /* execute command */
                return cmd;
            }

        }



        public DataTable ExecuteAdapter(SqlCommand command)
        {
            using (adapt = new SqlDataAdapter(command))
            {
                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;
            }
        }
        public int ExecuteInt(SqlCommand command)
        {
            return executeQueryCommand(cmd);
        }
        public bool Executebool(SqlCommand command)
        {
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected != 0)
            {
                return true;
            }
            return false;
        }
        public int ExecuteQueryInt(List<SqlParameter> param, string sp)
        {
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                using (SqlCommand comd = creatCommandExecute(sp, param, GetConnection(), createTransaction(GetConnection())))
                {
                    try
                    {
                        trans.Commit();
                        return ExecuteInt(comd);

                    }
                    catch (Exception ex)
                    {
                        Admin.LogExecMang.LogException(ex, "Execption");

                        trans.Rollback();
                        /* log exception and the fact that rollback succeeded */
                        ExceptionLogging.SendErrorToText(ex);
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                /* log or whatever */
                Admin.LogExecMang.LogException(ex, "Execption");

                Console.WriteLine(ex.ToString());
                MessageBox.Show("DataBase Insert Not Success \n" + ex.StackTrace);
                ExceptionLogging.SendErrorToText(ex);
                return 0;
            }
        }
        public DataTable ExecuteQueryAdapter(List<SqlParameter> param, string sp)
        {
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                using (SqlCommand comd = creatCommandExecute(sp, param, GetConnection(), createTransaction(GetConnection())))
                {
                    try
                    {
                        trans.Commit();
                        return ExecuteAdapter(comd);

                    }
                    catch (Exception ex)
                    {
                        Admin.LogExecMang.LogException(ex, "Execption");

                        trans.Rollback();
                        /* log exception and the fact that rollback succeeded */
                        ExceptionLogging.SendErrorToText(ex);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                /* log or whatever */
                Admin.LogExecMang.LogException(ex, "Execption");

                Console.WriteLine(ex.ToString());
                MessageBox.Show("DataBase Insert Not Success \n" + ex.StackTrace);
                ExceptionLogging.SendErrorToText(ex);
                return null;
            }
        }

        public string checkCustSaleKeyExist(string date, string customerid)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_checkCustomerKey", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@custid", SqlDbType.NVarChar).Value = customerid;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                if (data_tbl.Rows.Count == 0)
                    return "";
                DataRow row = data_tbl.Rows[0];
                return row[0].ToString();

            }
        }

        public bool p_sale_delete(string action, string date, string billkey)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_sales_delete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@bipari_key", SqlDbType.NVarChar).Value = billkey;

                int rowsAffected = executeQueryCommand(cmd);
                if (rowsAffected != 0)
                {
                    CloseConnection(conn);
                    return true;
                }

                CloseConnection(conn);
                return false;

            }
        }
        public bool p_moveSaleDate(string action, string date, string move2date)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_moveSaleDate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@currentdate", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@moveto", SqlDbType.NVarChar).Value = move2date;

                int rowsAffected = executeQueryCommand(cmd);
                if (rowsAffected != 0)
                {
                    CloseConnection(conn);
                    return true;
                }

                CloseConnection(conn);
                return false;

            }
        }

        public int p_cashamount_CRUD(string action, string date, string adminid, string cash, string type, string desc, string key)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_cashamount_CRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                cmd.Parameters.Add("@amount", SqlDbType.NVarChar).Value = cash;
                cmd.Parameters.Add("@atype", SqlDbType.NVarChar).Value = type;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = desc;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = adminid;

                if (@action == "DeleteCashById")
                {
                    int rowsAffected = executeQueryCommand(cmd);
                    if (rowsAffected != 0)
                    {
                        CloseConnection(conn);
                        return 1;
                    }
                }
                else
                if (action == "CashInsert")
                {
                    Int32 newId = (Int32)cmd.ExecuteScalar();
                    if (newId != 0)
                    {
                        CloseConnection(conn);
                        return newId;
                    }
                }


                CloseConnection(conn);
                return 0;

            }
        }

        public bool ExecuteQueryBool(List<SqlParameter> param, string sp)
        {
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                using (SqlCommand comd = creatCommandExecute(sp, param, GetConnection(), createTransaction(GetConnection())))
                {
                    try
                    {
                        trans.Commit();
                        return Executebool(comd);

                    }
                    catch (Exception ex)
                    {
                        Admin.LogExecMang.LogException(ex, "Execption");

                        trans.Rollback();
                        /* log exception and the fact that rollback succeeded */
                        ExceptionLogging.SendErrorToText(ex);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                /* log or whatever */
                Admin.LogExecMang.LogException(ex, "Execption");

                Console.WriteLine(ex.ToString());
                MessageBox.Show("DataBase Insert Not Success \n" + ex.StackTrace);
                ExceptionLogging.SendErrorToText(ex);
                return false;
            }
        }

        public string p_extra_amount(string action)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "dbo.p_inert_daily_clients_product";


                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", action);
                    // cmd.Parameters.AddWithValue("@bill_id", extraAmount.Bill_ID);
                    //cmd.Parameters.AddWithValue("@quantity", "" + extraAmount.Quantity);
                    //cmd.Parameters.AddWithValue("@extra_amount", "" + extraAmount.Extra_Amount);
                    //cmd.Parameters.AddWithValue("@commission", "" + extraAmount.Commission);
                    cmd.Parameters.Add("@id", SqlDbType.Int, 4);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                    check = executeQueryCommand(cmd);
                    string id = cmd.Parameters["@id"].Value.ToString();

                    //int recordCount = Convert.ToInt32(cmd.Parameters["@id"].Value);






                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return id;
                    }


                    CloseConnection(conn);
                    return id;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return "";
                }
            }
        }

        public bool check_StatusDailySales(string billkey)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("checkBillStatus", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = billkey;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                if (data_tbl.Rows.Count == 0)
                    return false;
                DataRow row = data_tbl.Rows[0];
                string status = row[0].ToString();
                if (status == "0")
                    return false;
                else
                    return true;

            }
        }

        public bool p_update_bill_status(string key, string status, string status_date, string refrence)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_update_bill_status", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                cmd.Parameters.Add("@status_date", SqlDbType.NVarChar).Value = status_date;
                cmd.Parameters.Add("@text", SqlDbType.NVarChar).Value = status;
                cmd.Parameters.Add("@refrence", SqlDbType.NVarChar).Value = refrence;

                int rowsAffected = executeQueryCommand(cmd);

                if (rowsAffected != 0)
                {
                    CloseConnection(conn);
                    return true;
                }


                CloseConnection(conn);
                return false;

            }
        }


        #endregion




        public int p_insert_CapitalCash(string date, string password, string cash, string api_key)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_insert_CapitalCash", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                cmd.Parameters.Add("@api_key", SqlDbType.NVarChar).Value = api_key;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@cash", SqlDbType.Int).Value = cash;
                Int32 newId = (Int32)cmd.ExecuteScalar();
                if (newId != 0)
                {
                    CloseConnection(conn);
                    return newId;
                }


                CloseConnection(conn);
                return 0;

            }
        }

        public DataTable getCapitalCash(string api_key, string action, string sdate, string ldate)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_capital_cash_all", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@api_key", SqlDbType.NVarChar).Value = api_key;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;
            }
            return null;
        }

        public string p_getInvoiceID(string action, string id, string date)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("p_getInvoiceID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@_date", SqlDbType.NVarChar).Value = date;
                    adapt = new SqlDataAdapter(cmd);
                    DataTable data_tbl = new DataTable();
                    adapt.Fill(data_tbl);
                    string invoiceID = "";
                    DataRow dr = data_tbl.Rows[0];
                    invoiceID = dr[0].ToString();
                    return invoiceID;

                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Admin.LogExecMang.LogException(ex, "Execption");

                return "";
            }
            catch (FormatException ex)
            {
                Admin.LogExecMang.LogException(ex, "Execption");

                return "";
            }
            catch (SqlException ex)
            {
                Admin.LogExecMang.LogException(ex, "Execption");

                return "";
            }
        }

        /**
        param name="type" 
        type=Customer/Client/Invoice
        returns [0]=CU-000,[1]=CL-000,[2]=000
        **/
        public string[] p_getID(string type)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_InvoiceIncrment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = type;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);


                string[] invoiceID = new string[3];

                DataRow dr = data_tbl.Rows[0];
                invoiceID[0] = dr[0].ToString();
                invoiceID[1] = dr[1].ToString();
                invoiceID[2] = dr[2].ToString();
                return invoiceID;

            }
            return null;
        }

        public DataTable p_report_CustomerClient(string id, string @startdate, string @lastdate, string action)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_report_CustomerClient", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = startdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = lastdate;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }
        public DataTable p_dashboard(string @action, string @date, string @start_date, string @last_date)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_dashboardCVA", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@start_date", SqlDbType.NVarChar).Value = start_date;
                cmd.Parameters.Add("@last_date", SqlDbType.NVarChar).Value = last_date;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }
        public DataTable p_all_sale_profit_details(string action, string sdate, string ldate)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("p_all_sale_profit_details", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                    cmd.Parameters.Add("@start_date", SqlDbType.NVarChar).Value = sdate;
                    cmd.Parameters.Add("@last_date", SqlDbType.NVarChar).Value = ldate;
                    adapt = new SqlDataAdapter(cmd);

                    DataTable data_tbl = new DataTable();
                    adapt.Fill(data_tbl);
                    return data_tbl;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return null;
                }
            }
            return null;
        }

        public DataTable check_User(string username, string password)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_login", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@user", SqlDbType.NVarChar).Value = username;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                /*copy
                 Account acc = new Account();
                for (int k = 0; k < data_tbl.Rows.Count; k++)
                {
                    DataRow row = data_tbl.Rows[k];
                    acc.shop_name = row[0].ToString();
                    acc.address = row[1].ToString();
                    acc.phone = row[2].ToString();
                    acc.propriters_name = row[3].ToString();
                    acc.username = row[4].ToString();
                    acc.api_key = row[5].ToString();
                }*/

                return data_tbl;

            }
        }



        public bool p_weigt_CRUD(string action, string id, string name, string catid, string acc_catid)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.p_weigt_CRUD", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@uname", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.Add("@ename", SqlDbType.NVarChar).Value = "";
                    cmd.Parameters.Add("@catid", SqlDbType.NVarChar).Value = catid;
                    cmd.Parameters.Add("@acc_catid", SqlDbType.NVarChar).Value = acc_catid;

                    int rowsAffected = executeQueryCommand(cmd);
                    if (rowsAffected != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }
                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return false;
                }
            }
        }

        public object getTodayExpense(string date)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_todayExpense", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }

        public bool deleteSingleSale(string date, string landkey, string custid, string custkey, string rec_custid)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.p_deleteSinglecustomersale", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    cmd.Parameters.Add("@landkey", SqlDbType.NVarChar).Value = landkey;
                    cmd.Parameters.Add("@custkey", SqlDbType.NVarChar).Value = custkey;
                    cmd.Parameters.Add("@custid", SqlDbType.Int).Value = custid;
                    cmd.Parameters.Add("@recid", SqlDbType.Int).Value = rec_custid;

                    int rowsAffected = executeQueryCommand(cmd);
                    if (rowsAffected != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }
                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return false;
                }
            }
        }

        public object p_report_cash_expense(string startdate, string lastdate)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_report_cash_expense", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = startdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = lastdate;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }

        public DataRow getLastBalance(string sdate, string ldate)
        {
            string sp = "p_lastbalance";
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@sdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@ldate", SqlDbType.NVarChar).Value = ldate;
                adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                if (dt.Rows.Count == 0) return null;
                DataRow dr = dt.Rows[0];
                // string balance = cr[2].ToString();
                //string balance = (dr[0].ToString()==""? "0" : dr[0].ToString());
                return dr;
            }

            return null;

        }

        public object p_maalList(string date)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("dbo.p_maalList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                adapt = new SqlDataAdapter(cmd);
                DataTable search = new DataTable();
                adapt.Fill(search);
                return search;
            }
            return null;

        }

        public DataTable salesDisplay(string action, string sdate, string ldate, string search, string key)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("dbo.p_salesread", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;
                cmd.Parameters.Add("@search", SqlDbType.NVarChar).Value = search;
                cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                adapt = new SqlDataAdapter(cmd);
                dt_sale = new DataTable();
                adapt.Fill(dt_sale);
                return dt_sale;
            }
            return null;
        }

        public DataTable getRecivedCash(string action, string date, string id, string key)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("dbo.p_receviecash", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                adapt = new SqlDataAdapter(cmd);
                dt_sale = new DataTable();
                adapt.Fill(dt_sale);
                return dt_sale;
            }
            return null;
        }

        public DataTable searchBillHistory(string search, string searchBy, string startdate, string lastdate)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("dbo.p_searchBill", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@search", SqlDbType.NVarChar).Value = search;
                cmd.Parameters.Add("@searchBY", SqlDbType.NVarChar).Value = searchBy;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = startdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = lastdate;
                adapt = new SqlDataAdapter(cmd);
                dt_sale = new DataTable();
                adapt.Fill(dt_sale);
                return dt_sale;
            }
            return null;
        }

        public bool delete_DailyMaal(string _id, string _date, string type)
        {


            if (deleteRecord(_id, _date, type) == 0)
            {
                return true;
            }
            return false;
        }
        public bool p_ud_cust_sale_product(string bill_key, string id, string cust_id, string date, int quantity, int wana_Delete, string type)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.p_ud_cust_sale_product", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@bill_key", SqlDbType.NVarChar).Value = bill_key;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;

                    cmd.Parameters.Add("@cust_id", SqlDbType.Int).Value = cust_id;
                    cmd.Parameters.Add("@isDelete", SqlDbType.Int).Value = wana_Delete;
                    cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
                    int rowsAffected = executeQueryCommand(cmd);
                    if (rowsAffected != 0)
                    {
                        //p_expense_CRUD("DeleteCL", date, "", 0, bill_key, new Expense(),type);
                        //p_expensenew_CRUD("Delete", date, "", 0, bill_key);
                        CloseConnection(conn);
                        return true;
                    }
                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return false;
                }
            }
        }

        public bool p_changeLandlordName(string date, string landkey, string landid)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.p_changeLandlordName", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    cmd.Parameters.Add("@landkey", SqlDbType.NVarChar).Value = landkey;
                    cmd.Parameters.Add("@newLandid", SqlDbType.Int).Value = landid;
                    int rowsAffected = executeQueryCommand(cmd);
                    if (rowsAffected != 0)
                    {
                        //p_expense_CRUD("DeleteCL", date, "", 0, bill_key, new Expense(),type);
                        //p_expensenew_CRUD("Delete", date, "", 0, bill_key);
                        CloseConnection(conn);
                        return true;
                    }
                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return false;
                }
            }
        }

        public bool addSaleLandlord(string action, string date, string client_id,
            int billAmount, string key, string name, int discount)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("p_LandlordManage", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                    cmd.Parameters.Add("@client_id", SqlDbType.Int).Value = client_id;
                    cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.Add("@discount", SqlDbType.Int).Value = discount;
                    cmd.Parameters.Add("@gtotal", SqlDbType.Int).Value = billAmount;
                    int rowsAffected = executeQueryCommand(cmd);
                    if (rowsAffected != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }
                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return false;
                }
            }
        }

        /*public int deleteRecord_Cust(string id, string cust_id, string date)
        {


            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {
                string sql = "Delete from p_daily_sale where client_id=@id AND cust_id=@cust_id AND _date=@date;";


                try
                {

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    int check = -1;
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    cmd.Parameters.Add("@cust_id", SqlDbType.NVarChar).Value = cust_id;
                    check = executeQueryCommand(cmd);

                    CloseConnection(conn);
                    return check;

                }
                catch (SqlException ex)
                {
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return 0;
                }
            }
        }*/

        public int deleteRecord(string bill_key, string date, string type)
        {


            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {
                //string sql = "Delete from p_daily where client_id=@id AND t_date=@date;";


                try
                {
                    /*
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    int check = -1;
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    //cmd.Parameters.AddWithValue("@id", id);
                    //cmd.Parameters.AddWithValue("@date", date);
                    check = executeQueryCommand(cmd);

                    CloseConnection(conn);
                    return check;*/



                    SqlCommand cmd = new SqlCommand("dbo.p_daily_delete", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@billkey", SqlDbType.NVarChar).Value = bill_key;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = type;
                    int rowsAffected = executeQueryCommand(cmd);
                    CloseConnection(conn);
                    return rowsAffected;

                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return 0;
                }
            }
        }




        #region CustomerSales
        public object getClient_Sales(string action, string date, string bill_id)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_customer_sales", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = bill_id;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }
        public DataTable p_customer_sale_record(string action, string date, string key)
        {
            string sp = "p_customer_sale_record";
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                adapt = new SqlDataAdapter(cmd);

                dt_client = new DataTable();
                adapt.Fill(dt_client);
                return dt_client;
            }
        }

        public bool updateClientAugrai(string pid, int grandTotal)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_updateClientAugrai";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", pid);
                    cmd.Parameters.AddWithValue("@gtotal", grandTotal);

                    check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
        }

        public bool customer_sale_add(string date)
        {

            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_customer_sale_add";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", date);

                    check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
        }
        public bool p_customer_sale_CRUD(string @table_name, string @key, string @date, string @id)
        {
            if (@table_name == "Insert" || @table_name == "UpdateCust" || @table_name == "Update" || @table_name == "Delete")
            {
                using (SqlConnection conn = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("p_customer_sale_CRUD", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@table_name", SqlDbType.NVarChar).Value = @table_name;
                    cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    int check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }

                }
            }
            return false;

        }
        public DataRow getLastBlance_p_customer_sale_CRUD(string @table_name, string @key, string @date, string cid)
        {
            string sp = "p_customer_sale_CRUD";
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@table_name", SqlDbType.NVarChar).Value = @table_name;
                cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = cid;
                adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                if (dt.Rows.Count == 0) return null;
                DataRow cr = dt.Rows[0];
                // string balance = cr[2].ToString();
                //string balance = (dr[0].ToString()==""? "0" : dr[0].ToString());
                return cr;
            }

            return null;

        }

        public DataTable p_customer_sale_CRUD(string @table_name, string @key, string @date)
        {
            string sp = "p_customer_sale_CRUD";
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@table_name", SqlDbType.NVarChar).Value = @table_name;
                cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = "";
                adapt = new SqlDataAdapter(cmd);

                dt_client = new DataTable();
                adapt.Fill(dt_client);
                return dt_client;
            }

            return null;

        }
        public string[] p_customer_CRUD(string @table_name, string @key, string @date, int @bill_amount
            , int @cust_id, int @cash_rec_id, int @cashinout, int @discount, int @localdb, string @crtype, string @crdetail)
        {
            string[] str = new string[2];
            if (@table_name == "SaleIn" || @table_name == "CashIn" || @table_name == "Delete" || @table_name == "DeleteRec")
            {
                using (SqlConnection conn = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("p_customer_CRUD", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = table_name;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    cmd.Parameters.Add("@billid_key", SqlDbType.NVarChar).Value = key;
                    cmd.Parameters.Add("@bill_amount", SqlDbType.Int).Value = bill_amount;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = cust_id;
                    cmd.Parameters.Add("@cash_rec_id", SqlDbType.Int).Value = cash_rec_id;
                    cmd.Parameters.Add("@cashinout", SqlDbType.Int).Value = cashinout;
                    cmd.Parameters.Add("@discount", SqlDbType.Int).Value = discount;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = "";
                    cmd.Parameters.Add("@crtype", SqlDbType.NVarChar).Value = crtype;
                    cmd.Parameters.Add("@crdetail", SqlDbType.NVarChar).Value = crdetail;
                    cmd.Parameters.Add("@localdb", SqlDbType.NVarChar).Value = localdb;
                    cmd.Parameters.Add("@rid", SqlDbType.Int, 4);
                    cmd.Parameters["@rid"].Direction = ParameterDirection.Output;
                    int check = executeQueryCommand(cmd);
                    string id = cmd.Parameters["@rid"].Value.ToString();

                    if (check != 0)
                    {
                        str[0] = "true";
                        str[1] = id;
                        CloseConnection(conn);
                    }
                    else
                    {
                        str[0] = "false";
                        str[1] = "";
                        CloseConnection(conn);
                    }

                }
            }
            return str;


        }

        public bool updateCusomerAmountandBalanceShet(string date, string clKey, string custKey, string pid, int check, string custbillid)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_update_Cus_Amt_BAL", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@cl_key", SqlDbType.NVarChar).Value = clKey;
                cmd.Parameters.Add("@cust_key", SqlDbType.NVarChar).Value = custKey;
                cmd.Parameters.Add("@cust_id", SqlDbType.NVarChar).Value = pid;
                cmd.Parameters.Add("@check", SqlDbType.Int).Value = check;
                cmd.Parameters.Add("@custbillid", SqlDbType.NVarChar).Value = custbillid;
                int chk = executeQueryCommand(cmd);
                if (chk != 0)
                {
                    CloseConnection(conn);
                    return true;
                }

            }
            return false;
        }

        public bool p_cashreceiving_CRUD(string action, string date)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_cashreceiving_CRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;

                int check = executeQueryCommand(cmd);
                if (check != 0)
                {
                    CloseConnection(conn);
                    return true;
                }

            }
            return false;

        }
        public DataTable p_customer_CRUD(string action, string name, string date)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_customer_CRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@billid_key", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@bill_amount", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@cash_rec_id", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@cashinout", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@discount", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@crtype", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@crdetail", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@localdb", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@rid", SqlDbType.Int, 4);
                cmd.Parameters["@rid"].Direction = ParameterDirection.Output;
                adapt = new SqlDataAdapter(cmd);

                dt_client = new DataTable();
                adapt.Fill(dt_client);
                return dt_client;
            }

            return null;
        }
        public static void Conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            // EACH PRINT or RAISERROR(..., severity < 11) shows up here.
            List<string> sqlMessages = new List<string>();
            foreach (SqlError err in e.Errors)
            {
                sqlMessages.Add(err.Message);
            }
            Admin.LogExecMang.Log($"[SQL Message]{string.Join(", ", sqlMessages)}");
        }
        public DataTable p_augrai_read(string isprintall, string date,string def_year)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_augrai_read", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                //cmd.Parameters.Add("@def_year", SqlDbType.NVarChar).Value = def_year;

                cmd.Parameters.Add("@isPrintAll", SqlDbType.Int).Value = isprintall;
                adapt = new SqlDataAdapter(cmd);

                dt_client = new DataTable();
                adapt.Fill(dt_client);
                return dt_client;
            }

            return null;
        }

        public DataTable getExpenseLabels()
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("p_labelname", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                return dt;
            }
        }

        public object p_customersbills_augrai(string action, string customer_id, string key, int pageIndex, int PageSize)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_customersbills_augrai", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@cust_id", SqlDbType.Int).Value = int.Parse(customer_id);
                cmd.Parameters.Add("@last_bill_key", SqlDbType.NVarChar).Value = key;
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                adapt = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adapt.Fill(dt);
                int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                List<Object> obj = new List<object>();
                obj.Add(recordCount);
                obj.Add(dt);
                return obj;
            }

            return null;
        }



        #endregion

        public int getDailyID(string date, string landlord_id)
        {

            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_getid_daily", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@landlord_id", SqlDbType.NVarChar).Value = landlord_id;
                SqlDataReader dr = cmd.ExecuteReader();
                int id = int.Parse(dr[0].ToString());
                return id;

            }
            return 0;
        }


        public bool addTodaySales(string date)
        {

            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "dbo.p_today_sales";

                    /*@date
           ,@total_product
           ,@remaining_product
           ,@GetTotalSale
           ,@total_rent
           ,@total_labour
           ,@total_advance
           ,@total_munshiana
           ,@total_driver_expense
           ,@total_sale_oncash
           ,@total_bipari_commission
           ,@total_biparil_chongi
           ,@total_customer_commission
           ,@total_customer_chongi*/

                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", date);
                    /*cmd.Parameters.AddWithValue("@total_product", "");
                    cmd.Parameters.AddWithValue("@remaining_product", "" );
                    cmd.Parameters.AddWithValue("@GetTotalSale", "" );
                    cmd.Parameters.AddWithValue("@total_rent", "" );
                    cmd.Parameters.AddWithValue("@total_labour", "" );
                    cmd.Parameters.AddWithValue("@total_advance", "");
                    cmd.Parameters.AddWithValue("@total_munshiana", "");
                    cmd.Parameters.AddWithValue("@total_driver_expense", "");
                    cmd.Parameters.AddWithValue("@total_sale_oncash", "" );
                    cmd.Parameters.AddWithValue("@total_bipari_commission","");
                    cmd.Parameters.AddWithValue("@total_biparil_chongi", "" );
                    cmd.Parameters.AddWithValue("@total_customer_commission",);
                    cmd.Parameters.AddWithValue("@total_customer_chongi", "");*/
                    check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
        }



        public bool addClient_Landlord(int objectindex)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "dbo.p_inert_daily_clients_product";


                    int check = 0;
                    for (int i = objectindex; i < Admin.GetInstance.clients.Count(); i++)
                    {
                        Landlord land = Admin.GetInstance.clients[i];


                        SqlCommand cmd = new SqlCommand(procedure, conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p1", "" + land.client._person_cl.pid);
                        cmd.Parameters.AddWithValue("@p2", land.land_person.pid);
                        cmd.Parameters.AddWithValue("@p3", "" + land.land_product._product_id);
                        cmd.Parameters.AddWithValue("@p4", "" + land.land_product._weight_id);
                        cmd.Parameters.AddWithValue("@p5", "" + land.land_product.total_Quantity);
                        cmd.Parameters.AddWithValue("@p6", "" + land.expense.total_rent);
                        cmd.Parameters.AddWithValue("@p7", "" + land.expense.total_labour);
                        cmd.Parameters.AddWithValue("@p8", "" + land.expense.total_munshiana);
                        cmd.Parameters.AddWithValue("@p9", "" + land.land_person.advance);
                        cmd.Parameters.AddWithValue("@p10", "" + land.client._person_cl.expense);
                        cmd.Parameters.AddWithValue("@p11", land.date);
                        cmd.Parameters.AddWithValue("@p12", "" + land.client._person_cl.pkey);
                        cmd.Parameters.AddWithValue("@p13", land.land_person.pkey);
                        cmd.Parameters.AddWithValue("@p14", "" + land.land_product._type);
                        cmd.Parameters.AddWithValue("@p15", "" + land.client._vehicle_id);
                        cmd.Parameters.AddWithValue("@p16", "" + land.service.commission_customer_product);
                        cmd.Parameters.AddWithValue("@p17", "" + land.service.customer_chongi);
                        cmd.Parameters.AddWithValue("@p18", "" + land.service.labour_per_product);
                        cmd.Parameters.AddWithValue("@p19", "" + land.land_product.sale_remaining_product);
                        cmd.Parameters.AddWithValue("@p20", "" + land.service.client_chongi);
                        cmd.Parameters.AddWithValue("@p22", land.service.commission_client_product);
                        cmd.Parameters.AddWithValue("@p23", "" + land.land_product._product_name);
                        cmd.Parameters.AddWithValue("@p24", land.land_product._weight);
                        cmd.Parameters.AddWithValue("@p25", land.service.rent_per_product);
                        cmd.Parameters.AddWithValue("@total_bipari_commission", land.GetCommission);
                        cmd.Parameters.AddWithValue("@total_bipari_chongi", land.GetChongi);
                        cmd.Parameters.AddWithValue("@total_sale_amount", land.total_sale);
                        cmd.Parameters.AddWithValue("@status", land.status);
                        cmd.Parameters.AddWithValue("@product_marka", land.land_product.marka);
                        cmd.Parameters.AddWithValue("@marketfee", land.expense.total_marketfee);

                        cmd.Parameters.AddWithValue("@bill_type", land.bill_type);
                        cmd.Parameters.AddWithValue("@bikri_quantity", land.bikri_quantity);
                        cmd.Parameters.AddWithValue("@bikri_rate", land.bikri_rate);
                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                        check = executeQueryCommand(cmd);
                        string id = cmd.Parameters["@id"].Value.ToString();
                        Admin.GetInstance.clients[i].record_id = id;
                    }
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return false;
                }
            }
        }
        public string addClient_Landlord(Landlord land)
        {
            string sql = "";
            // sql = "select  client_id ,client_name ,client_phone ,client_address FROM tbl_client where client_id=" + bipariname;
            //sql = "INSERT INTO p_daily(client_id, landlord_id, product_id, weight_id, product_quantity, total_rent, total_labour, total_munshiana, total_advance, total_expense, t_date, t_client_key, t_landlord_key, t_type, vehicle_number) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15) ";

            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "dbo.p_inert_daily_clients_product";


                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p1", "" + land.client._person_cl.pid);
                    cmd.Parameters.AddWithValue("@p2", land.land_person.pid);
                    cmd.Parameters.AddWithValue("@p3", "" + land.land_product._product_id);
                    cmd.Parameters.AddWithValue("@p4", "" + land.land_product._weight_id);
                    cmd.Parameters.AddWithValue("@p5", "" + land.land_product.total_Quantity);
                    cmd.Parameters.AddWithValue("@p6", "" + land.expense.total_rent);
                    cmd.Parameters.AddWithValue("@p7", "" + land.expense.total_labour);
                    cmd.Parameters.AddWithValue("@p8", "" + land.expense.total_munshiana);
                    cmd.Parameters.AddWithValue("@p9", "" + land.land_person.advance);
                    cmd.Parameters.AddWithValue("@p10", "" + land.client._person_cl.expense);
                    cmd.Parameters.AddWithValue("@p11", land.date);
                    cmd.Parameters.AddWithValue("@p12", "" + land.client._person_cl.pkey);
                    cmd.Parameters.AddWithValue("@p13", land.land_person.pkey);
                    cmd.Parameters.AddWithValue("@p14", "" + land.land_product._type);
                    cmd.Parameters.AddWithValue("@p15", "" + land.client._vehicle_id);
                    cmd.Parameters.AddWithValue("@p16", "" + land.service.commission_customer_product);
                    cmd.Parameters.AddWithValue("@p17", "" + land.service.customer_chongi);
                    cmd.Parameters.AddWithValue("@p18", "" + land.service.labour_per_product);
                    cmd.Parameters.AddWithValue("@p19", "" + land.land_product.sale_remaining_product);
                    cmd.Parameters.AddWithValue("@p20", "" + land.service.client_chongi);
                    cmd.Parameters.AddWithValue("@p22", land.service.commission_client_product);
                    cmd.Parameters.AddWithValue("@p23", "" + land.land_product._product_name);
                    cmd.Parameters.AddWithValue("@p24", land.land_product._weight);
                    cmd.Parameters.AddWithValue("@p25", land.service.rent_per_product);
                    cmd.Parameters.AddWithValue("@total_bipari_commission", land.GetCommission);
                    cmd.Parameters.AddWithValue("@total_bipari_chongi", land.GetChongi);
                    cmd.Parameters.AddWithValue("@total_sale_amount", land.total_sale);
                    cmd.Parameters.AddWithValue("@status", land.status);
                    cmd.Parameters.AddWithValue("@product_marka", land.land_product.marka);
                    cmd.Parameters.AddWithValue("@marketfee", land.expense.total_marketfee);

                    cmd.Parameters.AddWithValue("@bill_type", land.bill_type);
                    cmd.Parameters.AddWithValue("@bikri_quantity", land.bikri_quantity);
                    cmd.Parameters.AddWithValue("@bikri_rate", land.bikri_rate);
                    cmd.Parameters.Add("@id", SqlDbType.Int, 4);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                    check = executeQueryCommand(cmd);
                    string id = cmd.Parameters["@id"].Value.ToString();

                    //int recordCount = Convert.ToInt32(cmd.Parameters["@id"].Value);






                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return id;
                    }


                    CloseConnection(conn);
                    return id;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "SQLExcption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return "key-dup";
                }
                catch (Exception ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");
                    return null;

                }
            }
        }
        public DataTable p_sales_customer_View(int clientid, int customer_id, string date)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_sales_customer_View", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@client_id", SqlDbType.Int).Value = clientid;
                cmd.Parameters.Add("@cust_id", SqlDbType.Int).Value = customer_id;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                adapt = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adapt.Fill(dt);
                return dt;
            }

            return null;
        }
        public string[] p_singleupdateadd_landlord_customer(
             int entity_id,
            string t_date ,
            int source_id ,
            string table ,
            string amount,
            string balance,
            int is_last,
            string type
            )
        {
            var result = new string[2];
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand("dbo.p_singleupdateadd_landlord_customer", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@entity_id", entity_id);
                    cmd.Parameters.AddWithValue("@t_date", t_date);
                    cmd.Parameters.AddWithValue("@source_id", source_id);
                    cmd.Parameters.AddWithValue("@table", table);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@balance", balance);
                    cmd.Parameters.AddWithValue("@is_last", is_last);
                    cmd.Parameters.AddWithValue("@type", type);

                    try
                    {
                        int c = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    result[0] = "OK";
                    result[1] = "1";

                }
            }
            catch (SqlException ex)
            {
                var sb = new StringBuilder();
                foreach (SqlError err in ex.Errors)
                {
                    sb.AppendLine($"⚠ Index #{err.Number}  Line {err.LineNumber}  Procedure {err.Procedure}");
                    sb.AppendLine(err.Message);
                }

                Admin.LogExecMang.LogException(ex, "SQL Exception");
                ExceptionLogging.SendErrorToText(ex);

                result[0] = "ERROR";
                result[1] = sb.ToString();
            }
            catch (Exception ex)
            {
                Admin.LogExecMang.LogException(ex, "General Exception");
                ExceptionLogging.SendErrorToText(ex);

                result[0] = "ERROR";
                result[1] = ex.Message;
            }

            return result;
        }
        public string[] p_singlesaleupdate_landlord_customer(string date,string landlordid,string customerid)
        {
            var result = new string[2];
            try
            {
                //string sql = "dbo.p_singlesaleupdate_landlord_customer";
                string sql = "dbo.p_singlesaleupdate_landlord_customer2";

                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@_date",date);
                    cmd.Parameters.AddWithValue("@client_id", landlordid);
                    cmd.Parameters.AddWithValue("@cust_id", customerid);
                    try
                    {
                        int c = cmd.ExecuteNonQuery();
                    }
                    catch(SqlException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    result[0] = "OK";
                    result[1] = "1";

                }
            }
            catch (SqlException ex)
            {
                var sb = new StringBuilder();
                foreach (SqlError err in ex.Errors)
                {
                    sb.AppendLine($"⚠ Index #{err.Number}  Line {err.LineNumber}  Procedure {err.Procedure}");
                    sb.AppendLine(err.Message);
                }

                Admin.LogExecMang.LogException(ex, "SQL Exception");
                ExceptionLogging.SendErrorToText(ex);

                result[0] = "ERROR";
                result[1] = sb.ToString();
            }
            catch (Exception ex)
            {
                Admin.LogExecMang.LogException(ex, "General Exception");
                ExceptionLogging.SendErrorToText(ex);

                result[0] = "ERROR";
                result[1] = ex.Message;
            }

            return result;
        }
        public string[] p_singlesaleadd(Customer customer, Services service, Product product,
                                Person person, string date, string recordId, string status,
            string billtype="",float bikri_quantity=0,float bikri_rate=0)
        {
            var result = new string[2];

            // computed totals
            float servicesAmt = customer.sale._sale_quantity * service.labour_per_product
                              + customer.sale._sale_quantity * service.rent_per_product
                              + person.advance + service.clerk_per_bill;

            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand("dbo.p_singlesaleadd", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bill_key", person.pkey);
                    cmd.Parameters.Add("@_date", SqlDbType.Date).Value = DateTime.Parse(date);
                    cmd.Parameters.AddWithValue("@p_daily_id", recordId);
                    cmd.Parameters.AddWithValue("@client_id", person.pid);
                    cmd.Parameters.AddWithValue("@cust_id", customer.customer_profile.pid);
                    cmd.Parameters.AddWithValue("@_quantity", customer.sale._sale_quantity);
                    cmd.Parameters.AddWithValue("@_amount", customer.sale._sale_amount);
                    cmd.Parameters.AddWithValue("@extra_cust", customer.sale.add_extra_amount_Customer);
                    cmd.Parameters.AddWithValue("@extra_landlord", customer.sale.add_extra_amount_Landlord);
                    cmd.Parameters.AddWithValue("@sale_amount", customer.sale._TotalSaleAmount);
                    cmd.Parameters.AddWithValue("@sale_amount_landlord", customer.sale._TotalExtraAmountLandlord);
                    cmd.Parameters.AddWithValue("@sale_amount_customer", customer.sale._TotalExtraAmountCustomer);
                    cmd.Parameters.AddWithValue("@commission", customer.Total_Commission);
                    cmd.Parameters.AddWithValue("@chongi", customer.Total_Chongi);
                    cmd.Parameters.AddWithValue("@grand_total", customer.getGrandTotalCustomer());
                    cmd.Parameters.AddWithValue("@bipari_grand_total",
                                                 customer.getGrandTotalLandlord() - servicesAmt);
                    cmd.Parameters.AddWithValue("@cust_bill_key", customer.customer_profile.pkey);
                    cmd.Parameters.AddWithValue("@product_id", product._product_id);
                    cmd.Parameters.AddWithValue("@product_name", product._product_name);
                    cmd.Parameters.AddWithValue("@beg_weight_id", product._weight_id);
                    cmd.Parameters.AddWithValue("@beg_weight_name", product._weight);
                    cmd.Parameters.AddWithValue("@product_marka", product.marka);
                    //@bill_type nvarchar(1),@bikri_quantity int, @bikri_rate float,
                    cmd.Parameters.AddWithValue("@bill_type", billtype);
                    cmd.Parameters.AddWithValue("@bikri_quantity", bikri_quantity);
                    cmd.Parameters.AddWithValue("@bikri_rate", bikri_rate);
                    // ✅ add the missing output parameter
                    var idParam = new SqlParameter("@id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idParam);

                    cmd.ExecuteNonQuery();

                    result[0] = "OK";
                    result[1] = idParam.Value != DBNull.Value ? idParam.Value.ToString() : "0";

                    p_cashReceivingAfterDelete(customer.customer_profile.pid,
                                               date,
                                               customer.getGrandTotalCustomer());
                }
            }
            catch (SqlException ex)
            {
                var sb = new StringBuilder();
                foreach (SqlError err in ex.Errors)
                {
                    sb.AppendLine($"⚠ Index #{err.Number}  Line {err.LineNumber}  Procedure {err.Procedure}");
                    sb.AppendLine(err.Message);
                }

                Admin.LogExecMang.LogException(ex, "SQL Exception");
                ExceptionLogging.SendErrorToText(ex);

                result[0] = "ERROR";
                result[1] = sb.ToString();
            }
            catch (Exception ex)
            {
                Admin.LogExecMang.LogException(ex, "General Exception");
                ExceptionLogging.SendErrorToText(ex);

                result[0] = "ERROR";
                result[1] = ex.Message;
            }

            return result;
        }
        private int p_cashReceivingAfterDelete(string custId, string date, int amount)
        {
            StringBuilder errorMessages = new StringBuilder();

            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand("dbo.p_cashReceivingAfterDelete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = date;
                    cmd.Parameters.Add("@cust_id", SqlDbType.NVarChar, 50).Value = custId;
                    cmd.Parameters.Add("@amount", SqlDbType.Int).Value = amount;


                    int rows = cmd.ExecuteNonQuery();
                    CloseConnection(conn);// do it directly; no helper needed
                    return rows;                         // connection auto‑closes at end of using
                }
            }
            catch (SqlException ex)
            {
                foreach (SqlError err in ex.Errors)
                {
                    errorMessages.AppendLine(
                        $"• {err.Message} (Line {err.LineNumber}, Procedure {err.Procedure})");
                }

                Console.WriteLine(errorMessages.ToString());
                Admin.LogExecMang.LogException(ex, "SQL Exception in p_cashReceivingAfterDelete");
                ExceptionLogging.SendErrorToText(ex);
                return 0;
            }
            catch (Exception ex)
            {
                Admin.LogExecMang.LogException(ex, "General Exception in p_cashReceivingAfterDelete");
                ExceptionLogging.SendErrorToText(ex);
                return 0;
            }
        }

        public string[] addCustomerSales(Customer customer, Services service, Product product, Person person, string date, string recordid)
        {
            StringBuilder errorMessages = new StringBuilder();
            string[] str = new string[2];
            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "dbo.p_insert_customer_sale";


                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    float services =
                        customer.sale._sale_quantity * service.labour_per_product +
                        customer.sale._sale_quantity * service.rent_per_product +
                        person.advance +
                        service.clerk_per_bill;

                    float total_chongi = customer.sale._sale_quantity * (int)service.client_chongi;
                    float total_commission = ((customer.sale.getTotalSale() + customer.sale.getTotalExtraAmountLandlord()) / 100) * service.commission_client_product;
                    float commis_chongi = total_chongi + total_commission;

                    cmd.Parameters.AddWithValue("@bill_key", person.pkey);
                    cmd.Parameters.AddWithValue("@_date", date);
                    cmd.Parameters.AddWithValue("@p_daily_id", 0);
                    cmd.Parameters.AddWithValue("@client_id", person.pid);
                    cmd.Parameters.AddWithValue("@cust_id", customer.customer_profile.pid);
                    cmd.Parameters.AddWithValue("@_quantity", customer.sale._sale_quantity);
                    cmd.Parameters.AddWithValue("@_amount", customer.sale._sale_amount);
                    cmd.Parameters.AddWithValue("@extra_cust", customer.sale.add_extra_amount_Customer);
                    cmd.Parameters.AddWithValue("@extra_landlord", customer.sale.add_extra_amount_Landlord);
                    cmd.Parameters.AddWithValue("@sale_amount", customer.sale._TotalSaleAmount);
                    cmd.Parameters.AddWithValue("@sale_amount_landlord", customer.sale._TotalExtraAmountLandlord);
                    cmd.Parameters.AddWithValue("@sale_amount_customer", customer.sale._TotalExtraAmountCustomer);
                    cmd.Parameters.AddWithValue("@commission", customer.Total_Commission);
                    cmd.Parameters.AddWithValue("@chongi", customer.Total_Chongi);
                    cmd.Parameters.AddWithValue("@grand_total", (customer.getGrandTotalCustomer()));
                    cmd.Parameters.AddWithValue("@bipari_grand_total", (customer.getGrandTotalLandlord() - services));
                    cmd.Parameters.AddWithValue("@cust_bill_key", customer.customer_profile.pkey);
                    cmd.Parameters.AddWithValue("@product_id", product._product_id);
                    cmd.Parameters.AddWithValue("@product_name", product._product_name);
                    cmd.Parameters.AddWithValue("@beg_weight_id", product._weight_id);
                    cmd.Parameters.AddWithValue("@beg_weight_name", product._weight);
                    cmd.Parameters.AddWithValue("@product_marka", product.marka);

                    //cmd.Parameters.AddWithValue("@id", "0");

                    cmd.Parameters.Add("@id", SqlDbType.Int, 4);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                    check = executeQueryCommand(cmd);
                    string id = cmd.Parameters["@id"].Value==null?"-1": cmd.Parameters["@id"].Value.ToString();
                    str[0] = "" + check;
                    str[1] = id;

                    //Customer Bill Make
                    p_customer_sale_CRUD("Insert", customer.customer_profile.pkey, date, customer.customer_profile.pid);
                    //insertCustomerifnotExist(customer.customer_profile.pkey, customer.customer_profile.pid, landlord.client.date);
                    //customerBill_Update(landlord.client.date);

                    CloseConnection(conn);

                    return str;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return null;
                }
            }
        }

        public bool p_addsaleclient(string action, string date, object clid, int gamount)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_addsaleclient";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@clid", clid);
                    cmd.Parameters.AddWithValue("@grand_total", gamount);
                    check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
        }
        public bool p_addCash(string action, string date, int id,
            string desc, int amount, int discount, int cashtype, string key, string acc_cat_id,
            string trid, string name, string datetime, string category_id, string expenseid)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_addCash";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@desc", SqlDbType.NVarChar).Value = desc;
                    cmd.Parameters.Add("@amount", SqlDbType.Int).Value = amount;
                    cmd.Parameters.Add("@discount", SqlDbType.Int).Value = discount;
                    cmd.Parameters.Add("@cashtype", SqlDbType.Int).Value = cashtype;
                    cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                    cmd.Parameters.Add("@acc_tranid", SqlDbType.NVarChar).Value = acc_cat_id;
                    cmd.Parameters.Add("@trid", SqlDbType.Int).Value = trid;
                    cmd.Parameters.Add("@cname", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.Add("@datetime", SqlDbType.NVarChar).Value = datetime;
                    cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = category_id;
                    cmd.Parameters.Add("@expenseid", SqlDbType.Int).Value = expenseid;



                    check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
            return false;
        }

        public bool p_ledger_CRUD(string action, string transaction_id, string acc_trans_id,
            string entry_type, int amount, int userid, string usertype, string date
            , string key, string expenseid, string entry_action, string category_id)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_ledger_CRUD";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    cmd.Parameters.Add("@transaction_id", SqlDbType.Int).Value = transaction_id;
                    cmd.Parameters.Add("@amount", SqlDbType.Int).Value = amount;
                    cmd.Parameters.Add("@account_trans_id", SqlDbType.Int).Value = acc_trans_id;
                    cmd.Parameters.Add("@entry_type", SqlDbType.NVarChar).Value = entry_type;
                    cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid;
                    cmd.Parameters.Add("@usertype", SqlDbType.NVarChar).Value = usertype;
                    cmd.Parameters.Add("@keyid", SqlDbType.NVarChar).Value = key;
                    cmd.Parameters.Add("@expenseid", SqlDbType.Int).Value = expenseid;
                    cmd.Parameters.Add("@entry_action", SqlDbType.NVarChar).Value = entry_action;
                    cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = category_id;


                    check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
            return false;
        }

        public bool p_daily_update_extraAmount(
           string @action,
        string @date,
        string @key,
        string @cust_id,
        int @quantity,
        int @extra_amount,
        int @total_extra_amount,
        int @total_amount,
        int @grand_total,
        int @total_commission)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_daily_update_extraAmount";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@cust_id", cust_id);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@extra_amount", extra_amount);
                    cmd.Parameters.AddWithValue("@total_extra_amount", total_extra_amount);
                    cmd.Parameters.AddWithValue("@total_amount", total_amount);
                    cmd.Parameters.AddWithValue("@grand_total", grand_total);
                    cmd.Parameters.AddWithValue("@total_commission", total_commission);
                    cmd.Parameters.AddWithValue("@key", @key);

                    check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
            return false;
        }





        public DataTable executeMyQuery(string sql, List<SqlParameter> parameters)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand(sql, conn);

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters.ToArray());

                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }




        /*    public bool todayExpense_add_update(Landlord templandlord)
            {

                StringBuilder errorMessages = new StringBuilder();
                using(SqlConnection conn=GetConnection())
                {

                    try
                    {
                        
                        string procedure = "p_todayExpense_add_update";
                        int check = 0;
                        SqlCommand cmd = new SqlCommand(procedure, conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@date", templandlord.date);

                        check = executeQueryCommand(cmd);
                        if (check != 0)
                        {
                            CloseConnection(conn);
                            return true;
                        }


                        CloseConnection(conn);
                        return false;
                    }
                    catch (SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            errorMessages.Append("Index #" + i + "\n" +
                                "Message: " + ex.Errors[i].Message + "\n" +
                                "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                                "Source: " + ex.Errors[i].Source + "\n" +
                                "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }
                        Console.WriteLine(errorMessages.ToString());
                        return false;
                    }
                }
            }
      */




        /**
         * Categories for Dispay Records
         * 1 : Total_RE =>=Display Total Rent and Expend
         * 2 : Cl_NOTWDLL => Client does not have landlords
         * 3 : Cl_WDLL=>Client have landlords
         * 4 : ProductSale=> Get All clients product for sale
         * Procedure Call p_showtoday_rent(@startdate,@lastdate,@category)
         * 
         * */
        #region p_showtoday_rent
        public void getSingleDayTransportRent(string date)
        {
            string category = "Total_RE";
            string procedure = "p_showtoday_rent";
            DataTable tbl = (DataTable)getClientProductRecord(date, "", category);

        }

        public bool updateCapitalCash(string api_key, Landlord landlord)
        {

            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_update_account_table";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", landlord.record_id);
                    cmd.Parameters.AddWithValue("@date", landlord.date);
                    cmd.Parameters.AddWithValue("@apikey", api_key);

                    check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
        }


        public bool update_daily_table_cash_flow(string key, Landlord templandlord)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_update_daily_table";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", templandlord.date);
                    cmd.Parameters.AddWithValue("@record_id", templandlord.record_id);
                    cmd.Parameters.AddWithValue("@RQuantity", templandlord.land_product.sale_remaining_product);
                    cmd.Parameters.AddWithValue("@Sale_amount", templandlord.total_sale);
                    cmd.Parameters.AddWithValue("@cl_Commission", templandlord.Total_Commission);
                    cmd.Parameters.AddWithValue("@cl_Chongi", templandlord.Total_Chongi);
                    cmd.Parameters.AddWithValue("@acc_key", templandlord.Total_Chongi);

                    check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
        }

        public void getMultipleDayTransportRent(string s_date, string l_date)
        {
            string category = "Total_RE";
            string procedure = "p_showtoday_rent";
            DataTable tbl = (DataTable)getClientProductRecord(s_date, l_date, category);
        }

        //Daily Maal Amad
        public List<Landlord> getTodayProductsForSale(string date, string khataid)
        {
            List<Landlord> client = new List<Landlord>();
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_todaysaleproduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@param", SqlDbType.NVarChar).Value = khataid;

                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                foreach (DataRow row in data_tbl.Rows)
                {

                    string _id = row[0].ToString();
                    string _clientnameid = row[1].ToString();
                    string _clientname = row[2].ToString();
                    string _product_id = row[3].ToString();
                    string _product_name = row[4].ToString();
                    string _weight_id = row[5].ToString();
                    string _weight = row[6].ToString();
                    string _total_quantity = row[7].ToString();
                    string _total_rent = row[8].ToString();
                    string _total_labour = row[9].ToString();
                    string _total_munshiana = row[10].ToString();
                    string _advance = row[11].ToString();
                    string _expense = row[12].ToString();
                    string _date = row[13].ToString();
                    string _key = row[15].ToString();
                    string _type = row[16].ToString();
                    string _vehicle_id = row[17].ToString();
                    string _commission = row[18].ToString();
                    string _chongi = row[19].ToString();
                    string labourpp = row[20].ToString();
                    string _remaining_item = row[21].ToString();
                    string _bipari_commission = row[22].ToString();
                    string _bipari_chongi = row[23].ToString();

                    Landlord temp = new Landlord();
                    temp.client._vehicle_id = _vehicle_id;
                    temp.client.record_id = _id;
                    temp.client.date = _date;

                    Services s = new Services();
                    s.commission_customer_product = float.Parse(_commission);
                    s.customer_chongi = float.Parse(_chongi);
                    s.commission_client_product = float.Parse(_bipari_commission);
                    s.client_chongi = float.Parse(_bipari_chongi);
                    s.labour_per_product = float.Parse(labourpp);

                    temp.client._services = s;


                    Product p = new Product();
                    p._product_id = _product_id;
                    p._product_name = _product_name;
                    p._weight_id = _weight_id;
                    p._weight = _weight;
                    p._type = _type;
                    p.total_Quantity = int.Parse(_total_quantity);
                    p.sale_remaining_product = int.Parse(_remaining_item);

                    temp.client._product = p;

                    temp.expense.total_rent = int.Parse(_total_rent);
                    temp.expense.total_labour = int.Parse(_total_labour);
                    temp.expense.total_munshiana = int.Parse(_total_munshiana);






                    Person cl_person = new Person(_clientnameid, _key, _clientname, "", int.Parse(_advance), int.Parse(_expense));
                    temp.land_person = cl_person;


                    client.Add(temp);

                }
                CloseConnection(conn);
                return client;
            }

            return null;
        }
        /*     public List<Landlord> getClientNot_HV_LandLords(string date)
             {
                 List<Landlord> clients = new List<Landlord>();
                 using(SqlConnection conn=GetConnection())
                 {

                     SqlCommand cmd = new SqlCommand(sql_cl_nothvLandlord, conn);
                     cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = date;
                     adapt = new SqlDataAdapter(cmd);

                     DataTable data_tbl = new DataTable();
                     adapt.Fill(data_tbl);
                     foreach (DataRow row in data_tbl.Rows)
                     {

                         string _id = row[0].ToString();
                         string _clientnameid = row[1].ToString();
                         string _ll_id = "-1";
                         string _landloardnameid = "";
                         string _product_id = row[2].ToString();
                         string _product_name = row[3].ToString();
                         string _weight_id = row[4].ToString();
                         string _weight = row[5].ToString();
                         string _total_quantity = row[6].ToString();
                         string _total_rent = row[7].ToString();
                         string _total_labour = row[8].ToString();
                         string _total_munshiana = row[9].ToString();
                         string _advance = row[10].ToString();
                         string _expense = row[11].ToString();
                         string _date = row[12].ToString();
                         string _key = row[13].ToString();
                         string _type = row[15].ToString();
                         string _vehicle_id = row[16].ToString();
                         string _commission = row[17].ToString();
                         string _chongi = row[18].ToString();
                         string labourpp = row[19].ToString();
                         string _remaining_item = row[20].ToString();
                         string _bipari_commission = row[21].ToString();
                         string _bipari_chongi = row[22].ToString();
                         Landlord temp = new Landlord();
                         temp.client._vehicle_id = _vehicle_id;
                         temp.client.date = _date;
                         Services s = new Services();
                         s.commission_customer_product = float.Parse(_commission);
                         s.customer_chongi = float.Parse(_chongi);
                         s.commission_client_product = float.Parse(_bipari_commission);
                         s.client_chongi = float.Parse(_bipari_chongi);
                         s.labour_per_product = float.Parse(labourpp);
                         Product p = new Product();
                         p._product_id = _product_id;
                         p._product_name = _product_name;
                         p._weight_id = _weight_id;
                         p._weight = _weight;
                         p._type = _type;
                         p.total_Quantity = int.Parse(_total_quantity);
                         p.sale_remaining_product = int.Parse(_remaining_item);

                         TotalSale sale = new TotalSale();




                         Person cl_person = new Person(_id, _key, _clientnameid, "", int.Parse(_advance), int.Parse(_expense));
                         temp.land_person= cl_person;

                         temp.client._person_cl = cl_person;
                         temp.service = s;
                         temp.land_product= p;
                         temp.expense.total_rent = int.Parse(_total_rent);
                         temp.expense.total_labour = int.Parse(_total_labour);
                         temp.expense.total_munshiana = int.Parse(_total_munshiana);

                         clients.Add(temp);

                     }
                     CloseConnection(conn);
                 }

                 return clients;
             }*/




        public object getTodayMaalAmad(string date)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_todayMaalAmadDetail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }


        #region CommonMethod Used
        public object searchRecords(string date, string tag, string text, int pageIndex, int PageSize)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("dbo.p_search_ccpw", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@param", SqlDbType.NVarChar).Value = text;
                cmd.Parameters.Add("@tbl_type", SqlDbType.NVarChar).Value = tag;
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                adapt = new SqlDataAdapter(cmd);

               if (tag.Equals("SRClient"))
                {
                    dt_client = new DataTable();
                    adapt.Fill(dt_client);
                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    List<Object> obj = new List<object>();
                    obj.Add(recordCount);
                    obj.Add(dt_client);
                    return obj;
                }
                else if (tag.Equals("SClient"))
                {
                    dt_client = new DataTable();
                    adapt.Fill(dt_client);
                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    List<Object> obj = new List<object>();
                    obj.Add(recordCount);
                    obj.Add(dt_client);
                    return obj;
                }
                else if (tag.Equals("SCustomer"))
                {
                    dt_customer = new DataTable();
                    adapt.Fill(dt_customer);
                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    List<Object> obj = new List<object>();
                    obj.Add(recordCount);
                    obj.Add(dt_customer);
                    return obj;
                }
                if (tag.Equals("ClBipari"))
                {
                    dt_client = new DataTable();
                    adapt.Fill(dt_client);
                    int recordCount =0;
                    List<Object> obj = new List<object>();
                    obj.Add(recordCount);
                    obj.Add(dt_client);
                    return obj;
                }
                else
               if (tag.Equals("Client"))
                {
                    dt_client = new DataTable();
                    adapt.Fill(dt_client);
                    return dt_client;
                }
                else if (tag.Equals("Customer"))
                {
                    dt_customer = new DataTable();
                    adapt.Fill(dt_customer);

                    return dt_customer;
                }
                else if (tag.Equals("Product"))
                {
                    dt_product = new DataTable();
                    adapt.Fill(dt_product);
                    return dt_product;
                }
                else if (tag.Equals("p_product"))
                {
                    dt_product = new DataTable();
                    adapt.Fill(dt_product);
                    int recordCount = dt_product.Rows.Count;
                    List<Object> obj = new List<object>();
                    obj.Add(recordCount);
                    obj.Add(dt_product);
                    return obj;
                }
                else if (tag.Equals("p_weight"))
                {
                    dt_weight = new DataTable();
                    adapt.Fill(dt_weight);
                    int recordCount = dt_weight.Rows.Count;
                    List<Object> obj = new List<object>();
                    obj.Add(recordCount);
                    obj.Add(dt_weight);
                    return obj;
                }
                else if (tag.Equals("Weight"))
                {
                    dt_weight = new DataTable();
                    adapt.Fill(dt_weight);
                    return dt_weight;
                }
                else if (tag.Equals("p_account_trans"))
                {
                    dt_sale = new DataTable();
                    adapt.Fill(dt_sale);
                    return dt_sale;
                }
                else if (tag.Equals("Sale"))
                {
                    dt_sale = new DataTable();
                    adapt.Fill(dt_sale);
                    return dt_sale;
                }
                else if (tag.Equals("Augrai"))
                {
                    dt_augrai = new DataTable();
                    adapt.Fill(dt_augrai);
                    return dt_augrai;
                }
                else if (tag.Equals("ExpenseType"))
                {
                    dt_expense = new DataTable();
                    adapt.Fill(dt_expense);
                    int recordCount = dt_expense.Rows.Count;
                    List<Object> obj = new List<object>();
                    obj.Add(recordCount);
                    obj.Add(dt_expense);
                    return obj;
                }
                else if (tag.Equals("Fright"))
                {
                    dt_fright = new DataTable();
                    adapt.Fill(dt_fright);
                    return dt_fright;
                }
                else if (tag.Equals("City"))
                {
                    dt_fright = new DataTable();
                    adapt.Fill(dt_fright);
                    return dt_fright;
                }
                else if(tag.Equals("ExpType"))
                {
                    dt_fright = new DataTable();
                    adapt.Fill(dt_fright);
                    return dt_fright;
                }

            }
            return null;

        }
        public System.Windows.Forms.AutoCompleteStringCollection suggestionCustClient(string tag)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("dbo.p_search_ccpw", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@param", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@tbl_type", SqlDbType.NVarChar).Value = tag;
                cmd.Parameters.AddWithValue("@PageIndex", 0);
                cmd.Parameters.AddWithValue("@PageSize", 0);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                SqlDataReader reader = cmd.ExecuteReader();
                System.Windows.Forms.AutoCompleteStringCollection MyCollection = new System.Windows.Forms.AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                conn.Close();
                return MyCollection;

            }
        }
        public object searchCustClient(string date, string tag, string text, int pageIndex, int PageSize)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("dbo.p_search_ccpw", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@param", SqlDbType.NVarChar).Value = text;
                cmd.Parameters.Add("@tbl_type", SqlDbType.NVarChar).Value = tag;
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                adapt = new SqlDataAdapter(cmd);

                if (tag.Equals("SClient"))
                {
                    dt_client = new DataTable();
                    adapt.Fill(dt_client);
                    return dt_client;
                }
                else if (tag.Equals("SCustomer"))
                {
                    dt_customer = new DataTable();
                    adapt.Fill(dt_customer);

                    return dt_customer;
                }
                if (tag.Equals("Client"))
                {
                    dt_client = new DataTable();
                    adapt.Fill(dt_client);
                    return dt_client;
                }
                else if (tag.Equals("Customer"))
                {
                    dt_customer = new DataTable();
                    adapt.Fill(dt_customer);

                    return dt_customer;
                }
                else if (tag.Equals("Product"))
                {
                    dt_product = new DataTable();
                    adapt.Fill(dt_product);
                    return dt_product;
                }
                else if (tag.Equals("Weight"))
                {
                    dt_weight = new DataTable();
                    adapt.Fill(dt_weight);
                    return dt_weight;
                }
                else if (tag.Equals("Sale"))
                {
                    dt_sale = new DataTable();
                    adapt.Fill(dt_sale);
                    return dt_sale;
                }
            }
            return null;

        }

        public int insertDataCPW(int v, string data, string address)
        {


            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {
                    string procedure = "p_insert_CCPW";

                    string tag = "Client";
                    if (v == 111)
                    {
                        tag = "ClBipari";
                        //procedure = "Insert into tbl_client(client_name) output INSERTED.client_id values (@data) ";
                    }
                    else
                    if (v == 1)
                    {
                        tag = "Client";
                        //procedure = "Insert into tbl_client(client_name) output INSERTED.client_id values (@data) ";
                    }
                    else if (v == 2)
                    {
                        tag = "Customer";
                        //procedure = "Insert into tbl_customer(cust_name) output INSERTED.cust_id values (@data) ";

                    }
                    else if (v == 3)
                    {
                        tag = "Product";
                        //procedure = "Insert into tbl_product(urdu_veg_name) output INSERTED.veg_id values (@data) ";

                    }
                    else if (v == 4)
                    {
                        tag = "Weight";
                        //procedure = "Insert into tbl_begcategory(urdu_beg_name) output INSERTED.begid values (@data) ";

                    }
                    else if (v == 5)
                    {
                        tag = "ExpenseType";
                        //procedure = "Insert into tbl_begcategory(urdu_beg_name) output INSERTED.begid values (@data) ";

                    }

                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", tag);
                    cmd.Parameters.AddWithValue("@data", data);
                    cmd.Parameters.AddWithValue("@address", address);
                    Int32 check = (Int32)cmd.ExecuteScalar();
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return check;
                    }

                    CloseConnection(conn);
                    return 0;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return 0;
                }
            }


            return 0;
        }

        #endregion


        #region SaleDB
        #region p_daily_CRUD

        public object p_daily_CRUD(string tbl_name, string date, string text)
        {
            using (SqlConnection conn = GetConnection())
            {


                SqlCommand cmd = new SqlCommand("p_daily_CRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tbl_name", SqlDbType.NVarChar).Value = tbl_name;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@text", SqlDbType.NVarChar).Value = text;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }
        #endregion


        public bool p_expensenew_CRUD(string @action, string @date, string @desc, int @amount, string @key)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_expensenew_CRUD";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@desc", desc);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    check = executeQueryCommand(cmd);
                    CloseConnection(conn);

                    if (check != 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
        }

        public bool p_expense_CRUD(string @action, string @date, string @name, int @amount, string @key, Expense @expense, string @type)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_expense_CRUD";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@extra", 0/*expense.total_expense*/);
                    cmd.Parameters.AddWithValue("@rent", expense.total_rent);
                    cmd.Parameters.AddWithValue("@labour", expense.total_labour);
                    cmd.Parameters.AddWithValue("@munshiana", 0/*expense.total_munshiana*/);
                    cmd.Parameters.AddWithValue("@advance", expense.total_advance_amount);
                    cmd.Parameters.AddWithValue("@type", type);
                    check = executeQueryCommand(cmd);
                    CloseConnection(conn);

                    if (check != 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
        }
        public bool addBalanceSheetExpense(string expensename, string total_amount,
            string date, string type, string key, string inout, string crud_action,
            string update, string account_transaction_id, string category_id)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {
                try
                {

                    string procedure = "p_balnce_sheet_CRUD";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@bill_type", type);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.Parameters.AddWithValue("@desc", expensename);
                    cmd.Parameters.AddWithValue("@amount", total_amount);
                    cmd.Parameters.AddWithValue("@inout", inout);//debit or credit
                    cmd.Parameters.AddWithValue("@crud_action", crud_action); // insert or delete
                    cmd.Parameters.AddWithValue("@update", update);// in case of delete send update=1 it will update previous record as deleted
                    cmd.Parameters.AddWithValue("@account_transaction_id", account_transaction_id);
                    cmd.Parameters.AddWithValue("@category_id", category_id);
                    cmd.Parameters.Add("@id", SqlDbType.Int, 4);

                    cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                    check = executeQueryCommand(cmd);
                    string id = cmd.Parameters["@id"].Value.ToString();




                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
        }



        public bool addBalanceSheet(string @inout, int @update, Landlord landlord,
            string bill_type, string action, string key, int oldAmount, string desc, string account_transaction_id)
        {
            this.oldAmount = oldAmount;
            return addBalanceSheet(@inout, @update, landlord, bill_type, action, key, desc, account_transaction_id);
        }

        public bool addAmountBalanceSheet(string @inout, int @update, string name, string bill_type, string action, string key, string desc, string date, int amount, string account_transaction_id)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {


                    string procedure = "p_balnce_sheet_CRUD";
                    int check = 0;

                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@inout", @inout);

                    int cash = 0;

                    if (bill_type == nameof(BillKey.EnumUser.Client) || bill_type == "DeleteOld")
                    {
                        if (bill_type == "DeleteOld")
                            cash = oldAmount;
                        else
                            cash = (int)amount;

                        cmd.Parameters.AddWithValue("@key", key);
                        desc = string.Format("{0}, {1}, {2}", name, bill_type + " " + action, cash);
                        cmd.Parameters.AddWithValue("@desc", desc);
                        cmd.Parameters.AddWithValue("@bill_type", nameof(BillKey.EnumUser.Client));

                    }
                    else if (bill_type == nameof(BillKey.EnumUser.Expense))
                    {

                        cmd.Parameters.AddWithValue("@bill_type", bill_type);

                        cmd.Parameters.AddWithValue("@key", key);

                        cash = amount;
                        desc = string.Format("{0}, {1}, {2}", name, bill_type + " " + action, cash);

                        cmd.Parameters.AddWithValue("@desc", desc);
                    }
                    else if (bill_type == nameof(BillKey.EnumUser.Shop))
                    {
                        cmd.Parameters.AddWithValue("@bill_type", bill_type);

                        cmd.Parameters.AddWithValue("@key", key);

                        desc = string.Format("{0}, {1}, {2}", name, bill_type + " " + action, cash);
                        cmd.Parameters.AddWithValue("@desc", desc);
                    }
                    else if (bill_type == nameof(BillKey.EnumUser.Customer))
                    {
                        cmd.Parameters.AddWithValue("@bill_type", bill_type);

                        cmd.Parameters.AddWithValue("@key", key);
                        desc = string.Format("{0}, {1}, {2}", name, bill_type + " " + action, cash);

                        cmd.Parameters.AddWithValue("@desc", desc);
                    }
                    else if (bill_type == nameof(BillKey.EnumUser.ClientInvest))
                    {
                        cmd.Parameters.AddWithValue("@bill_type", bill_type);
                        desc = string.Format("{0}, {1}, {2}", name, bill_type + " " + action, cash);

                        cmd.Parameters.AddWithValue("@key", key);
                        cmd.Parameters.AddWithValue("@desc", desc);
                    }
                    else if (bill_type == nameof(BillKey.EnumUser.Client))
                    {
                        cmd.Parameters.AddWithValue("@bill_type", bill_type);
                        desc = string.Format("{0}, {1}, {2}", name, bill_type + " " + action, cash);

                        cmd.Parameters.AddWithValue("@key", key);
                        cmd.Parameters.AddWithValue("@desc", desc);
                    }
                    cmd.Parameters.AddWithValue("@amount", cash);
                    cmd.Parameters.AddWithValue("@crud_action", action);
                    cmd.Parameters.AddWithValue("@update", @update);
                    cmd.Parameters.AddWithValue("@account_transaction_id", account_transaction_id);
                    cmd.Parameters.Add("@id", SqlDbType.Int, 4);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                    check = executeQueryCommand(cmd);
                    string id = cmd.Parameters["@id"].Value.ToString();



                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }

            }
        }


        public bool addBalanceSheet(string @inout, int @update, Landlord landlord,
            string bill_type, string action, string key, string desc, string account_transaction_id)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {


                    string procedure = "p_balnce_sheet_CRUD";
                    int check = 0;

                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", landlord.date);
                    cmd.Parameters.AddWithValue("@inout", @inout);

                    int cash = 0;

                    if (bill_type == nameof(BillKey.EnumUser.Client) || bill_type == "DeleteOld")
                    {
                        if (bill_type == "DeleteOld")
                            cash = oldAmount;
                        else
                            cash = (int)landlord.GetGrandTotal;

                        cmd.Parameters.AddWithValue("@key", landlord.land_person.pkey);
                        desc = string.Format("{0}, {1}, {2}", landlord.land_person.pname, bill_type + " " + action, cash);
                        cmd.Parameters.AddWithValue("@desc", desc);
                        cmd.Parameters.AddWithValue("@bill_type", nameof(BillKey.EnumUser.Client));

                    }
                    else if (bill_type == nameof(BillKey.EnumUser.Expense))
                    {
                        int naqdi = 0;
                        if (landlord.expense.total_expense > 0)
                        {
                            naqdi = landlord.expense.total_expense;
                        }
                        else if (landlord.client._person_cl.expense > 0)
                        {
                            naqdi = landlord.client._person_cl.expense;
                        }
                        cmd.Parameters.AddWithValue("@bill_type", bill_type);

                        cmd.Parameters.AddWithValue("@key", key);

                        cash = (int)landlord.GetTotalService - landlord.expense.total_munshiana /*+ naqdi*/;
                        desc = string.Format("{0}, {1}, {2}", landlord.land_person.pname, bill_type + " " + action, cash);

                        cmd.Parameters.AddWithValue("@desc", desc);
                    }
                    else if (bill_type == nameof(BillKey.EnumUser.Shop))
                    {
                        cmd.Parameters.AddWithValue("@bill_type", bill_type);

                        cmd.Parameters.AddWithValue("@key", key);

                        desc = string.Format("{0}, {1}, {2}", landlord.land_person.pname, bill_type + " " + action, cash);
                        cmd.Parameters.AddWithValue("@desc", desc);
                    }
                    else if (bill_type == nameof(BillKey.EnumUser.Customer))
                    {
                        cmd.Parameters.AddWithValue("@bill_type", bill_type);

                        cmd.Parameters.AddWithValue("@key", key);
                        desc = string.Format("{0}, {1}, {2}", landlord.land_person.pname, bill_type + " " + action, cash);

                        cmd.Parameters.AddWithValue("@desc", desc);
                    }
                    else if (bill_type == nameof(BillKey.EnumUser.ClientInvest))
                    {
                        cmd.Parameters.AddWithValue("@bill_type", bill_type);
                        desc = string.Format("{0}, {1}, {2}", landlord.land_person.pname, bill_type + " " + action, cash);

                        cmd.Parameters.AddWithValue("@key", key);
                        cmd.Parameters.AddWithValue("@desc", desc);
                    }
                    cmd.Parameters.AddWithValue("@amount", cash);
                    cmd.Parameters.AddWithValue("@crud_action", action);

                    cmd.Parameters.AddWithValue("@update", @update);
                    cmd.Parameters.AddWithValue("@account_transaction_id", account_transaction_id);

                    cmd.Parameters.Add("@id", SqlDbType.Int, 4);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                    check = executeQueryCommand(cmd);
                    string id = cmd.Parameters["@id"].Value.ToString();



                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }

            }
        }
        public string isuserNoExist(string id,string name,string table,string address)
        {
            int check = 0;
            try
            {


                using (SqlConnection conn = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("p_isusernotexist", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@uid", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.Add("@table", SqlDbType.NVarChar).Value = table;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = table;

                    cmd.Parameters.Add("@id", SqlDbType.Int, 4);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                    check = executeQueryCommand(cmd);
                    string uid = cmd.Parameters["@id"].Value.ToString();
                    if (check == -1)
                    {
                        Admin.LogExecMang.Log("User Inserted " + id + "->" + name + "->" + table + "->>" + uid);
                        CloseConnection(conn);
                        return uid;
                    }

                }
            }
            catch(Exception e)
            {
                Admin.LogExecMang.LogException(e, "UserInserttion");
                return "0";
            }
            return "0";
        }
        public bool insertCustomerSale(Landlord landlord, int custIndex)
        {
            StringBuilder errorMessages = new StringBuilder();
            int check = 0;
            string[] rs;

            for (int i = 0; i < landlord.customers.Count(); i++)
            {
                Customer customer = landlord.customers[i];
                //string id=isuserNoExist(customer.customer_profile.pid, customer.customer_profile.pname, "Customer","");
                //if (id == "0")
                //    return false;

                //customer.customer_profile.pid = id;

                rs = addCustomerSales(customer, landlord.service, landlord.land_product, landlord.land_person, landlord.date, landlord.record_id);
                if (rs != null)
                {
                    check = int.Parse(rs[0]);
                    landlord.customers[i].cust_bill_id = rs[1];

                    decimal gtotal = customer.getGrandTotalCustomer();
                    p_update_customerbill("UpdateAugrai", landlord.date, customer.customer_profile.pkey
                        , customer.customer_profile.pid, (int)gtotal, customer.cust_bill_id);
                }
            }

            p_update_customerbill("CustomerSale", landlord.date, "", "", 0, "");

            for (int i = custIndex; i < landlord.customers.Count(); i++)
            {
                Customer customer = landlord.customers[i];
                decimal Cgtotal = customer.getGrandTotalCustomer();
                p_update_customerbill("UpCustomersaleAugrai", landlord.date, customer.customer_profile.pkey
                        , customer.customer_profile.pid, Cgtotal, "");
            }
            

            return true;

        }

        public bool p_update_customerbill(string action, string date, string key, string id, decimal amount, string billid)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_update_customerbill", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                cmd.Parameters.Add("@cust_id", SqlDbType.NVarChar).Value = id;
                cmd.Parameters.Add("@amount", SqlDbType.NVarChar).Value = amount;
                cmd.Parameters.Add("@dsid", SqlDbType.NVarChar).Value = billid;
                int check = executeQueryCommand(cmd);
                if (check != 0)
                {
                    CloseConnection(conn);
                    return true;
                }

            }
            return false;
        }

        public bool update_today_sales(string date)
        {

            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_update_today_sales";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", date);

                    check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
        }

        public bool p_update_daily_table_product(Landlord templandlord)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_update_daily_table_product";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", templandlord.date);
                    cmd.Parameters.AddWithValue("@record_id", templandlord.record_id);
                    cmd.Parameters.AddWithValue("@RQuantity", templandlord.land_product.sale_remaining_product);
                    cmd.Parameters.AddWithValue("@Sale_amount", templandlord.total_sale);
                    cmd.Parameters.AddWithValue("@cl_Commission", templandlord.GetCommission);
                    cmd.Parameters.AddWithValue("@cl_Chongi", templandlord.GetChongi);
                    cmd.Parameters.AddWithValue("@status", templandlord.status);
                    cmd.Parameters.AddWithValue("@gtotal", templandlord.GetGrandTotal);

                    check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
        }
        public bool p_update_salesexpenses(Landlord templandlord)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_update_salesexpenses";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@landid", templandlord.land_person.pid);
                    cmd.Parameters.AddWithValue("@date", templandlord.date);
                    cmd.Parameters.AddWithValue("@key", templandlord.land_person.pkey);
                    cmd.Parameters.AddWithValue("@rent", templandlord.expense.total_rent);
                    cmd.Parameters.AddWithValue("@labour", templandlord.expense.total_labour);
                    cmd.Parameters.AddWithValue("@advance", templandlord.expense.total_advance_amount);
                    cmd.Parameters.AddWithValue("@munshiana", templandlord.expense.total_munshiana);
                    cmd.Parameters.AddWithValue("@marketfee", templandlord.expense.total_marketfee);

                    cmd.Parameters.AddWithValue("@commission", templandlord.Total_Commission);
                    cmd.Parameters.AddWithValue("@chongi", templandlord.Total_Chongi);


                    check = executeQueryCommand(cmd);
                    if (check != 0)
                    {
                        CloseConnection(conn);
                        return true;
                    }


                    CloseConnection(conn);
                    return false;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return false;
                }
            }
        }



        #endregion



        public object getClientProductRecord(string sdate, string ldate, string category)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("dbo.p_search_ccpw", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;
                cmd.Parameters.Add("@category", SqlDbType.NVarChar).Value = category;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                CloseConnection(conn);
                return data_tbl;
            }
            return null;
        }
        public DataTable getExpenseType(string name)
        {
            return (DataTable)searchRecords("", "ExpType", name, 1, 100);
        }
        public System.Windows.Forms.AutoCompleteStringCollection autoCompleteData()
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_expensetypes_CRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "Read";
                cmd.Parameters.Add("@expense_name", SqlDbType.NVarChar).Value = "";

                SqlDataReader rdr = cmd.ExecuteReader();
                //AutoCompleteStringCollection Contains a collection of strings to use for the auto-complete feature on certain Windows Forms controls.
                System.Windows.Forms.AutoCompleteStringCollection autoCompleteCollection = new System.Windows.Forms.AutoCompleteStringCollection();
                while (rdr.Read())
                {
                    autoCompleteCollection.Add(rdr.GetString(0));
                }
                return autoCompleteCollection;
            }
            return null;
        }
        public bool insertTodayExpense(string date, string expense, string amount, string refid, string expense_from,
            string type, string id, string accid, string detail, string trid, string cateid, string expenseId)
        {
            int check = 0;
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_insert_expense", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@param1", SqlDbType.NVarChar).Value = expense;
                cmd.Parameters.Add("@param2", SqlDbType.Int).Value = amount;
                cmd.Parameters.Add("@param3", SqlDbType.NVarChar).Value = refid;
                cmd.Parameters.Add("@param4", SqlDbType.NVarChar).Value = expense_from;
                cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = type;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                cmd.Parameters.Add("@accid", SqlDbType.Int).Value = accid;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = detail;
                cmd.Parameters.Add("@trid", SqlDbType.Int).Value = trid;
                cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = cateid;
                cmd.Parameters.Add("@expenseId", SqlDbType.Int).Value = expenseId;

                check = executeQueryCommand(cmd);
                if (check == -1 || check >0)
                {
                    CloseConnection(conn);
                    return true;
                }

            }
            return false;
        }

        public void addExpenseName(string txt)
        {
            using (SqlConnection conn = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("p_expensetypes_CRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@expense_name", SqlDbType.NVarChar).Value = txt;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "Insert";
                int check = executeQueryCommand(cmd);
                CloseConnection(conn);
            }
        }
        #endregion


        ///*********************P_Shop_Sales*****************************///
        /// DBHandler
        /// Methods call p_shop_sales_daily 
        /// Type OF Actions Perform 
        /// I=Insert,R=Read 2 months record,RALL=Read All Record
        /// D=Delete,U=Update,RU=Distinct user

        #region p_shop_sales_daily
        public object p_shop_sales_crud(string action, string sdate, string ldate
        , string name, string userid,
        string quantity, string rate, string size, string product
        , string t_date, string total_amount, string ispaid, int sort, int record_id, int labour, string productid,string details,int check)
        {
            using (SqlConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    return null;
                }

                SqlCommand cmd = new SqlCommand("p_shop_sales_crud", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@action", action);
                cmd.Parameters.AddWithValue("@startdate", sdate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@lastdate", ldate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@name", name ?? "");
                cmd.Parameters.AddWithValue("@quantity", quantity ?? "0");
                cmd.Parameters.AddWithValue("@product", product ?? "");
                cmd.Parameters.AddWithValue("@t_date", t_date ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@total_amount", string.IsNullOrEmpty(total_amount) ? 0 : Convert.ToInt32(total_amount));
                cmd.Parameters.AddWithValue("@userid", string.IsNullOrEmpty(userid) ? 0 : Convert.ToInt32(userid));
                cmd.Parameters.AddWithValue("@ispaid", ispaid);
                cmd.Parameters.AddWithValue("@orderby", sort);
                cmd.Parameters.AddWithValue("@rate", string.IsNullOrEmpty(rate) ? 0 : Convert.ToInt32(rate));
                cmd.Parameters.AddWithValue("@size", size ?? "");
                cmd.Parameters.AddWithValue("@record_id", record_id);
                cmd.Parameters.AddWithValue("@labour", labour);
                cmd.Parameters.AddWithValue("@product_id", productid);
                cmd.Parameters.AddWithValue("@remarks", details ?? "");

                // ❗❗❗ THIS WAS MISSING – VERY IMPORTANT ❗❗❗
                cmd.Parameters.AddWithValue("@check", check);

                List<object> obj = new List<object>();

                if (action == "I" || action == "D" || action == "U")
                {
                    int rowsAffected = executeQueryCommand(cmd);

                    obj.Add(rowsAffected);
                    obj.Add(null);

                    CloseConnection(conn);
                    return obj;
                }
                else if (action == "R")
                {
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    DataTable data_tbl = new DataTable();
                    adapt.Fill(data_tbl);

                    obj.Add(0);
                    obj.Add(data_tbl);

                    CloseConnection(conn);
                    return obj;
                }

                return null;
            }

        }
        //public DataTable shopSalesRead(string action, string sdate, string ldate
        //, string name, string userid,
        //string quantity, string rate, string size, string product
        //, string t_date, string total_amount, int ispaid, int sort, int record_id,string remarks,string check="")
        //{
        //    using (SqlConnection conn = GetConnection())
        //    {

        //        SqlCommand cmd = new SqlCommand("p_shop_sales_crud", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
        //        cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
        //        cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;
        //        cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
        //        cmd.Parameters.Add("@quantity", SqlDbType.NVarChar).Value = quantity;
        //        cmd.Parameters.Add("@product", SqlDbType.NVarChar).Value = product;
        //        cmd.Parameters.Add("@t_date", SqlDbType.NVarChar).Value = t_date;
        //        cmd.Parameters.Add("@total_amount", SqlDbType.NVarChar).Value = total_amount;
        //        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid == "" ? "0" : userid;
        //        cmd.Parameters.Add("@ispaid", SqlDbType.Int).Value = ispaid;
        //        cmd.Parameters.Add("@orderby", SqlDbType.Int).Value = sort;
        //        cmd.Parameters.Add("@rate", SqlDbType.Int).Value = sort;
        //        cmd.Parameters.Add("@size", SqlDbType.NVarChar).Value = sort;
        //        cmd.Parameters.Add("@record_id", SqlDbType.Int).Value = record_id;
        //        cmd.Parameters.Add("@labour", SqlDbType.Int).Value = 0;
        //        cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = "0";
        //        cmd.Parameters.Add("@remarks", SqlDbType.NVarChar).Value = remarks;
        //        cmd.Parameters.Add("@check", SqlDbType.NVarChar).Value = check;

        //        adapt = new SqlDataAdapter(cmd);

        //        DataTable data_tbl = new DataTable();
        //        adapt.Fill(data_tbl);
        //        CloseConnection(conn);
        //        return data_tbl;
        //    }
        //    return null;
        //}
        private int executeQueryCommand(SqlCommand cmd)
        {
            try
            {
                Admin.LogExecMang.Log(cmd.CommandText);

                int chk = cmd.ExecuteNonQuery();


                return chk;
            }
            catch (Exception e)
            {
                Admin.LogExecMang.LogException(e, e.Message);
                return 0;

            }
        }

        public object crudSettings(string action,string path)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "p_settings_default";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@crud", action);
                    cmd.Parameters.AddWithValue("@path", path);

                    List<Object> obj = new List<object>();


                    if (action == "U_Path")
                    {
                        int rowsAffected = executeQueryCommand(cmd);
                        //return null;

                        if (rowsAffected != 0)
                        {
                            obj.Add(rowsAffected);
                            obj.Add(null);
                        }
                        else
                        {
                            obj.Add(rowsAffected);
                            obj.Add(null);
                        }
                        CloseConnection(conn);

                        return obj;



                    }
                    else if (action == "R")
                    {
                        adapt = new SqlDataAdapter(cmd);

                        DataTable data_tbl = new DataTable();

                        adapt.Fill(data_tbl);
                        obj.Add(0);
                        obj.Add(data_tbl);
                        CloseConnection(conn);
                        //return null;

                        return obj;
                    }
                    return null;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// post_to_journal_sales
        /// </summary>
        /// <param name="type">S,P,S_ALL,P_ALL</param>
        /// <param name="key"> key should be unqiue</param>
        /// <param name="date">data should be posted to journal that date requeired</param>
        /// <returns></returns>
        public object post_to_journal_sales(string type, string key,string date)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "post_to_journal_sales";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.Parameters.AddWithValue("@date", date);
                    int rowsAffected = executeQueryCommand(cmd);
                    CloseConnection(conn);
                    
                    return rowsAffected;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return null;
                }
            }
        }
        public object post_to_journal(string source_table, string source_id, string action)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "post_to_journal";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_source_table", source_table);
                    cmd.Parameters.AddWithValue("@p_source_id", source_id);
                    cmd.Parameters.AddWithValue("@p_action", action);
                    cmd.Parameters.AddWithValue("@p_created_by", "admin");

                    int rowsAffected = executeQueryCommand(cmd);
                    CloseConnection(conn);

                    return rowsAffected;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return null;
                }
            }
        }

        public object CancelJournalEntry(string source_id)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = GetConnection())
            {

                try
                {

                    string procedure = "CancelJournalEntry";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SourceID", source_id);

                    int rowsAffected = executeQueryCommand(cmd);
                    CloseConnection(conn);

                    return rowsAffected;
                }
                catch (SqlException ex)
                {
                    Admin.LogExecMang.LogException(ex, "Execption");

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                    return null;
                }
            }
        }



        //    #region Call DB SP
        //    // Function to execute stored procedures that return a DataTable (for SELECTs)
        //    public DataTable ExecuteStoredProcedure(string storedProcedureName, SqlParameter[] parameters)
        //    {
        //        DataTable dt = new DataTable();

        //        using (SqlConnection conn = GetConnection())
        //        {
        //            try
        //            {
        //                using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;

        //                    if (parameters != null)
        //                    {
        //                        cmd.Parameters.AddRange(parameters);
        //                    }

        //                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //                    {
        //                        da.Fill(dt);  // Fill DataTable with the result set
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Admin.LogExecMang.LogException(ex, "Execption");
        //                // Handle exceptions
        //                Console.WriteLine("An error occurred: " + ex.Message);
        //            }
        //        }

        //        return dt;
        //    }

        //    // Function to execute stored procedures for non-query operations (like INSERT, UPDATE)
        //    public void ExecuteNonQueryStoredProcedure(string storedProcedureName, SqlParameter[] parameters)
        //    {
        //        using (SqlConnection conn = GetConnection())
        //        {
        //            try
        //            {
        //                using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    if (parameters != null)
        //                    {
        //                        cmd.Parameters.AddRange(parameters);
        //                    }
        //                    executeQueryCommand(cmd);  // Execute the query
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Admin.LogExecMang.LogException(ex, "Execption");
        //                // Handle exceptions
        //                Console.WriteLine("An error occurred: " + ex.Message);
        //            }
        //        }
        //    }
        //        #endregion

        #endregion
        ///*****************************************************************///





    }
}
