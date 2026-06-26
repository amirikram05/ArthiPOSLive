using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace DAL
{
    public class ProfilesDB
    {
        //Update Data
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter adapt;
        public static string Connection
        {
            get { return GeneralConst.ConnectionSTring; }
        }

        public bool p_product_CRUD(string action, string code, string uName, string eName,
            string freight, string labour, string bipcommi, string pcode,
            string pack, string cuscommi1, string location, string laga,
            string chongi, string munshiana, string marketfee, string shopcomm, string shoplabour = "")
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Connection))
                {
                    connection.Open();
                    cmd = new SqlCommand("p_product_CRUD", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@UName", uName);
                    cmd.Parameters.AddWithValue("@EName", eName);
                    cmd.Parameters.Add("@Freight", SqlDbType.Int).Value = freight;
                    cmd.Parameters.Add("@Labour", SqlDbType.Int).Value = labour;
                    cmd.Parameters.Add("@BipCommi", SqlDbType.Float).Value = bipcommi;
                    cmd.Parameters.AddWithValue("@PCode", pcode);
                    cmd.Parameters.AddWithValue("@Pack", pack);
                    cmd.Parameters.Add("@CUSCommi1", SqlDbType.Float).Value = cuscommi1;
                    cmd.Parameters.AddWithValue("@Location", location);
                    cmd.Parameters.Add("@ChongiLaga", SqlDbType.Int).Value = laga;
                    cmd.Parameters.Add("@ChongiCust", SqlDbType.Int).Value = chongi;
                    cmd.Parameters.Add("@Munshiana", SqlDbType.Int).Value = munshiana;
                    cmd.Parameters.Add("@Marketfee", SqlDbType.Int).Value = marketfee;
                    cmd.Parameters.Add("@ShopComm", SqlDbType.Int).Value = shopcomm;
                    cmd.Parameters.Add("@ShopLabour", SqlDbType.Int).Value = shoplabour;

                    int chk = cmd.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                    if (chk != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        public bool p_old_reacord(string action, string id, string name, string date, int amount, string address)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Connection))
                {
                    connection.Open();
                    cmd = new SqlCommand("p_old_record", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.Parameters.AddWithValue("@uname", name);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@address", address);

                    int chk = cmd.ExecuteNonQuery();

                    connection.Close();
                    if (chk != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        public bool p_weigt_CRUD(string action, string id, string uname, string ename)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Connection))
                {
                    connection.Open();
                    cmd = new SqlCommand("p_weigt_CRUD", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@uname", uname);
                    cmd.Parameters.AddWithValue("@ename", ename);
                    int chk = cmd.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                    if (chk != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        /*public bool delete_CC(string tbl,string id)
        {
            String sql = "delete tbl_customer where cust_id=@id";
            if (tbl == "tbl_client")
            {
                sql = "delete tbl_client where client_id=@id";
            }
            int ID = Convert.ToInt32(id);
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", ID);
                int chk = cmd.ExecuteNonQuery();
                connection.Close();
                if (chk != 0)
                {
                    return true;
                }
                return false;

            }
        }

        public bool insert_CC(string tbl, string text1)
        {
            String sql = "insert into tbl_customer_test(cust_name) values(@uname)";
            if (tbl== "tbl_client_test")
            {
                sql = "insert into tbl_client_test(client_name) values(@uname)";
            }
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                connection.Open();
                cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@uname", text1);
                int chk = cmd.ExecuteNonQuery();
                connection.Close();
                if (chk != 0)
                {
                    return true;
                }
                return false;

            }
        }

        public object getCC(string tbl)
        {
            String sql = "select  cust_id as ID,eng_cust_name as EngName ,cust_name as Name,cust_phone as Phone,cust_address as Address,remaining_amount as  RemainingAmount FROM tbl_customer";//tbl_customer_test
            if (tbl == "tbl_client")
            {
                sql = "select  client_id as ID,eng_client_name as EngName, client_name as Name,client_phone as Phone,client_address as Address, client_advance_amount as RemainingAmount FROM tbl_client";
            }
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                connection.Open();
                dt = new DataTable();
                adapt = new SqlDataAdapter(sql, connection);
                adapt.Fill(dt);
                return dt;

            }
        }

        public bool insert_CC(string tlb, string uname, string cname, string cphone, string caddress, string amount)
        {
            String sp = "insert into tbl_customer(eng_cust_name,cust_name, cust_phone, cust_address,remaining_amount) values(@cname ,@uname ,@cphone, @caddress,@remaining_amount)";
            if (tlb== "tbl_client")
            {
                sp = "insert into tbl_client(eng_client_name,client_name, client_phone, client_address,client_advance_amount) values(@cname,@uname, @cphone, @caddress,@client_advance_amount)";
            }
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                connection.Open();
                cmd = new SqlCommand(sp, connection);
                cmd.Parameters.AddWithValue("@uname", uname);
                cmd.Parameters.AddWithValue("@cname", cname);
                cmd.Parameters.AddWithValue("@cphone", cphone);
                cmd.Parameters.AddWithValue("@caddress", caddress);

                if (tlb == "tbl_client")
                {
                    cmd.Parameters.AddWithValue("@client_advance_amount", amount);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@remaining_amount", amount);
                }

                int chk=cmd.ExecuteNonQuery();
                connection.Close();
                if (chk!=0)
                {
                    return true;
                }
                return false;

            }
        }
        */

        public bool p_profile_CRUD(string action, string tlb, int id, string uname, string cname,
            string cphone, string caddress, int amount, string key,
            string date, string detail, string type)
        {
            // try
            // {
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                connection.Open();
                cmd = new SqlCommand("p_profile_CRUD", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = action;
                cmd.Parameters.Add("@table", SqlDbType.NVarChar).Value = tlb;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@cname", SqlDbType.NVarChar).Value = cname;
                cmd.Parameters.Add("@uname", SqlDbType.NVarChar).Value = uname;
                cmd.Parameters.Add("@cphone", SqlDbType.NVarChar).Value = cphone;
                cmd.Parameters.Add("@caddress", SqlDbType.NVarChar).Value = caddress;
                cmd.Parameters.Add("@amount", SqlDbType.Int).Value = amount;
                cmd.Parameters.Add("@clkey", SqlDbType.NVarChar).Value = key;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = detail;
                cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = type;
                int chk = cmd.ExecuteNonQuery();

                connection.Close();
                if (chk != 0)
                {
                    return true;
                }
                return false;
            }
            //}
            //catch(SqlException e)
            //{
            //    MessageBox.Show(e.ToString());
            //    return false;
            //}

        }
        public object p_profile_CRUD(string action, string tlb)
        {

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                connection.Open();
                cmd = new SqlCommand("p_profile_CRUD", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", action);
                cmd.Parameters.AddWithValue("@table", tlb);
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@cname", "");
                cmd.Parameters.AddWithValue("@uname", "");
                cmd.Parameters.AddWithValue("@cphone", "");
                cmd.Parameters.AddWithValue("@caddress", "");
                cmd.Parameters.AddWithValue("@amount", "");
                cmd.Parameters.AddWithValue("@clkey", "");
                cmd.Parameters.AddWithValue("@date", "");
                dt = new DataTable();
                adapt = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapt.Fill(dt);
                connection.Close();
                connection.Dispose();
                return dt;

            }
        }
        public bool addBalanceSheetExpense(string expensename, string total_amount,
            string date, string type, string key, string inout, string crud_action, string update)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(Connection))
            {

                try
                {

                    connection.Open();
                    int check = 0;
                    SqlCommand cmd = new SqlCommand("p_balnce_sheet_CRUD", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@bill_type", type);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.Parameters.AddWithValue("@desc", expensename);
                    cmd.Parameters.AddWithValue("@amount", total_amount);
                    cmd.Parameters.AddWithValue("@inout", inout);//debit or credit
                    cmd.Parameters.AddWithValue("@crud_action", crud_action); // insert or delete
                    cmd.Parameters.AddWithValue("@update", update);// in case of delete send update=1 it will update previous record as deleted
                    cmd.Parameters.Add("@id", SqlDbType.Int, 4);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                    check = cmd.ExecuteNonQuery();
                    string id = cmd.Parameters["@id"].Value.ToString();



                    connection.Close();
                    connection.Dispose();
                    if (check != 0)
                    {
                        return true;
                    }

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
        public bool p_investment(string @action, string key, string @cl_id, int @amount, string @amount_date,
            int @amount_receive, string @receive_date)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(Connection))
            {

                try
                {

                    connection.Open();
                    string procedure = "p_investment_CRUD";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.Parameters.AddWithValue("@cl_id", cl_id);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@amount_date", amount_date);
                    cmd.Parameters.AddWithValue("@amount_receive", amount_receive);
                    cmd.Parameters.AddWithValue("@receive_date", receive_date);
                    check = cmd.ExecuteNonQuery();



                    connection.Close();
                    connection.Dispose();
                    if (check != 0)
                    {
                        return true;
                    }

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
    }
}
