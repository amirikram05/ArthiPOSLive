using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArthiPOS.shop.test;
using ArthiPOS.utill;
using System.Windows.Forms;

namespace ArthiPOS.shop
{
    public class DBHandler
    {
        public static string ConnectionSTring = ConfigurationManager.ConnectionStrings["c"].ConnectionString;
        public DataTable dt_client, dt_customer, dt_product, dt_weight,dt_sale;
        public SqlCommand cmd;
        public SqlDataAdapter adapt;

       
        public object searchRecords(string date,string tag,string text)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("dbo.p_search_ccpw", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@param", SqlDbType.NVarChar).Value = text;
                cmd.Parameters.Add("@tbl_type", SqlDbType.NVarChar).Value = tag;
                adapt = new SqlDataAdapter(cmd);

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

        internal bool p_insert_CapitalCash(string date,string password, string cash, string api_key)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_insert_CapitalCash", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                cmd.Parameters.Add("@api_key", SqlDbType.NVarChar).Value = api_key;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@cash", SqlDbType.Int).Value = cash;
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected != 0)
                {
                    connection.Close();
                    return true;
                }


                connection.Close();
                return false;

            }
            return false;
        }

        internal DataTable getCapitalCash(string api_key)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_capital_cash_all", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@api_key", SqlDbType.NVarChar).Value = api_key;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }

        internal Account check_User(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_login", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@user", SqlDbType.NVarChar).Value = username;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
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
                }

                return acc;

            }
        }

        internal object getTodayExpense(string date)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_todayExpense", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }

        public object p_maalList(string date)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("dbo.p_maalList", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                adapt = new SqlDataAdapter(cmd);
                DataTable search= new DataTable();
                    adapt.Fill(search);
                    return search;
            }
            return null;

        }




        internal bool delete_DailyMaal(string _id,string _date,string type)
        {
           

            if (deleteRecord(_id, _date, type) == 0)
            {
                return true;
            }
            return false;
        }
        public bool p_ud_cust_sale_product(string id, string cust_id, string date, int quantity,int wana_Delete)
        {


            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {

                try
                {
                    
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("dbo.p_ud_cust_sale_product", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    cmd.Parameters.Add("@cust_id", SqlDbType.Int).Value = cust_id;
                    cmd.Parameters.Add("@isDelete", SqlDbType.Int).Value = wana_Delete;
                    cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected != 0)
                    {
                        connection.Close();
                        return true;
                    }


                    connection.Close();
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
                    ExceptionLogging.SendErrorToText(ex);
                    return false;
                }
            }
        }

        public int deleteRecord_Cust(string id,string cust_id, string date)
        {


            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                string sql = "Delete from p_daily_sale where client_id=@id AND cust_id=@cust_id AND _date=@date;";


                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    int check = -1;
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    cmd.Parameters.Add("@cust_id", SqlDbType.NVarChar).Value = cust_id;
                    check = cmd.ExecuteNonQuery();

                    connection.Close();
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
        }

        public int deleteRecord( string id, string date,string type)
        {


            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                //string sql = "Delete from p_daily where client_id=@id AND t_date=@date;";


                try
                {
                    /*connection.Open();
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    int check = -1;
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    //cmd.Parameters.AddWithValue("@id", id);
                    //cmd.Parameters.AddWithValue("@date", date);
                    check = cmd.ExecuteNonQuery();

                    connection.Close();
                    return check;*/


                    connection.Open();
                    SqlCommand cmd = new SqlCommand("dbo.p_daily_delete", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                    cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = type;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected;

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
        }

        internal object getClient_Sales(string bill_id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_customer_sales", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = bill_id;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }

        internal int getDailyID(string date,string landlord_id)
        {
            
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_getid_daily", connection);
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

            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {

                try
                {
                    connection.Open();
                    string procedure = "dbo.p_today_sales";

                    /*@date
           ,@total_product
           ,@remaining_product
           ,@total_sale
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
                     SqlCommand cmd = new SqlCommand(procedure, connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@date", date);
                        /*cmd.Parameters.AddWithValue("@total_product", "");
                        cmd.Parameters.AddWithValue("@remaining_product", "" );
                        cmd.Parameters.AddWithValue("@total_sale", "" );
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
                        check = cmd.ExecuteNonQuery();
                    if (check != 0)
                    {
                        connection.Close();
                        return true;
                    }


                    connection.Close();
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
        public bool addClient_Landlord(int objectindex)
        {
            string sql = "";
            // sql = "select  client_id ,client_name ,client_phone ,client_address FROM tbl_client where client_id=" + bipariname;
            //sql = "INSERT INTO p_daily(client_id, landlord_id, product_id, weight_id, product_quantity, total_rent, total_labour, total_munshiana, total_advance, total_expense, t_date, t_client_key, t_landlord_key, t_type, vehicle_number) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15) ";

            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {

                try
                {
                    connection.Open();
                    string procedure = "dbo.p_inert_daily_clients_product";

                    
                        int check = 0;
                        for (int i= objectindex; i<Admin.GetInstance.clients.Count();i++)
                        {
                            Landlord land = Admin.GetInstance.clients[i];
                            SqlCommand cmd = new SqlCommand(procedure, connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@p1", "" + land.client._person_cl.pid);
                            cmd.Parameters.AddWithValue("@p2", land.land_person.pid);
                            cmd.Parameters.AddWithValue("@p3", "" + land.land_product._product_id);
                            cmd.Parameters.AddWithValue("@p4", "" + land.land_product._weight_id);
                            cmd.Parameters.AddWithValue("@p5", "" + land.land_product.total_Quantity);
                            cmd.Parameters.AddWithValue("@p6", "" + land.total_rent);
                            cmd.Parameters.AddWithValue("@p7", "" + land.total_labour);
                            cmd.Parameters.AddWithValue("@p8", "" + land.total_munshiana);
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
                            cmd.Parameters.AddWithValue("@id", "0");
                            check =cmd.ExecuteNonQuery();
                            string id = cmd.Parameters["@id"].Value.ToString();
                            Admin.GetInstance.clients[i].record_id = id;
                        }
                        if (check != 0)
                        {
                            connection.Close();
                            return true;
                        }
                    

                    connection.Close();
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
                    AlertMsg.Show(errorMessages.ToString(),AlertMsg.AlertType.error);
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return false;
                }
            }
        }
        internal bool insertCustomerSale(Landlord landlord)
        {
           StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {

                try
                {
                    connection.Open();
                    string procedure = "dbo.p_insert_customer_sale";


                    int check = 0;
                    for (int i = 0; i < landlord.customers.Count(); i++)
                    {

                        Customer customer= landlord.customers[i];

                            
                            SqlCommand cmd = new SqlCommand(procedure, connection);
                            cmd.CommandType = CommandType.StoredProcedure;

                            float services =
                                customer.sale._sale_quantity * landlord.service.labour_per_product +
                                customer.sale._sale_quantity * landlord.service.rent_per_product +
                                landlord.land_person.advance +
                                landlord.service.clerk_per_bill
                                ;
                            float total_chongi = customer.sale._sale_quantity * (int)landlord.service.client_chongi;
                            float total_commission = (customer.sale.total_Sale / 100) * landlord.service.commission_client_product;

                            float commis_chongi = total_chongi + total_commission;

                            cmd.Parameters.AddWithValue("@p1", landlord.client.date);
                            cmd.Parameters.AddWithValue("@p2", landlord.record_id);
                            cmd.Parameters.AddWithValue("@p3", landlord.land_person.pid);
                            cmd.Parameters.AddWithValue("@p4", customer.customer_profile.pid);
                            cmd.Parameters.AddWithValue("@p5", customer.sale._sale_quantity);
                            cmd.Parameters.AddWithValue("@p6", customer.sale._sale_amount);
                            cmd.Parameters.AddWithValue("@p7", customer.sale.total_Sale);
                            cmd.Parameters.AddWithValue("@p8", customer.total_commission);
                            cmd.Parameters.AddWithValue("@p9", customer.total_chongi);
                            cmd.Parameters.AddWithValue("@p10", (customer.sale.total_Sale + customer.total_commission + customer.total_chongi));
                            cmd.Parameters.AddWithValue("@p11", (customer.sale.total_Sale - services - commis_chongi));
                            cmd.Parameters.AddWithValue("@id", "0");


                            check = cmd.ExecuteNonQuery();
                            string id = cmd.Parameters["@id"].Value.ToString();
                            landlord.customers[i].cust_bill_id = id;
                        }
                        if (check != 0)
                        {
                            connection.Close();
                            return true;
                        }
                        connection.Close();


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
                    AlertMsg.Show(errorMessages.ToString(), AlertMsg.AlertType.error);
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return false;
                }
            }
        }

        


        internal int insertDataCPW(int v,string data)
        {
            

            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {

                try
                {
                    string procedure = "p_insert_CCPW";

                    string tag = "Client";
                    if (v == 1)
                    {
                        tag = "Client";
                        procedure = "Insert into tbl_client(client_name) output INSERTED.client_id values (@data) ";
                    }
                    else if (v == 2)
                    {
                        tag = "Customer";
                        procedure = "Insert into tbl_customer(cust_name) output INSERTED.cust_id values (@data) ";

                    }
                    else if (v == 3)
                    {
                        tag = "Product";
                        procedure = "Insert into tbl_product(urdu_veg_name) output INSERTED.veg_id values (@data) ";

                    }
                    else if (v == 4)
                    {
                        tag = "Weight";
                        procedure = "Insert into tbl_begcategory(urdu_beg_name) output INSERTED.begid values (@data) ";

                    }
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(procedure, connection);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@check", tag);
                    cmd.Parameters.AddWithValue("@data", data);
                    //cmd.Parameters.AddWithValue("@id", "0");
                    int check = Convert.ToInt32(cmd.ExecuteScalar()); ;
                    if (check != 0)
                    {
                        connection.Close();
                        return check;
                    }
                    
                    connection.Close();
                    return 0;
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
                    AlertMsg.Show(errorMessages.ToString(), AlertMsg.AlertType.error);
                    Console.WriteLine(errorMessages.ToString());
                    ExceptionLogging.SendErrorToText(ex);
                    return 0;
                }
            }


            return 0;
        }

        internal bool customer_sale_add(string date)
        {
            
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {

                try
                {
                    connection.Open();
                    string procedure = "p_customer_sale_add";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", date);

                    check = cmd.ExecuteNonQuery();
                    if (check != 0)
                    {
                        connection.Close();
                        return true;
                    }


                    connection.Close();
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

        internal bool update_today_sales(string date)
        {

            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {

                try
                {
                    connection.Open();
                    string procedure = "p_update_today_sales";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", date);

                    check = cmd.ExecuteNonQuery();
                    if (check != 0)
                    {
                        connection.Close();
                        return true;
                    }


                    connection.Close();
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

        internal bool p_expense_transport(string date)
        {
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {

                try
                {
                    connection.Open();
                    string procedure = "p_expense_transport";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", date);
                    check = cmd.ExecuteNonQuery();
                    if (check != 0)
                    {
                        connection.Close();
                        return true;
                    }


                    connection.Close();
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
        internal bool todayExpense_add_update(Landlord templandlord)
        {

            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {

                try
                {
                    connection.Open();
                    string procedure = "p_todayExpense_add_update";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", templandlord.date);
                     cmd.Parameters.AddWithValue("@record_id", templandlord.record_id);
                    cmd.Parameters.AddWithValue("@RQuantity", templandlord.land_product.sale_remaining_product);
                    cmd.Parameters.AddWithValue("@Sale_amount", templandlord.total_sale);
                    cmd.Parameters.AddWithValue("@cl_Commission", templandlord.total_commission);
                    cmd.Parameters.AddWithValue("@cl_Chongi", templandlord.total_chongi);

                    check = cmd.ExecuteNonQuery();
                    if (check != 0)
                    {
                        connection.Close();
                        return true;
                    }


                    connection.Close();
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
        internal List<Client> getDailySale(string date)
        {
            // Getting record who does not have landlords Product.
            //Query Get Single Date Record
            string sqlnotlandlord = "";
            return null;
        }




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
            DataTable tbl = (DataTable)getClientProductRecord(date,"", category);

        }

        internal bool updateCapitalCash(string api_key,Landlord landlord)
        {
            
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {

                try
                {
                    connection.Open();
                    string procedure = "p_update_account_table";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", landlord.record_id);
                    cmd.Parameters.AddWithValue("@date", landlord.date);
                    cmd.Parameters.AddWithValue("@apikey", api_key);

                    check = cmd.ExecuteNonQuery();
                    if (check != 0)
                    {
                        connection.Close();
                        return true;
                    }


                    connection.Close();
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

        internal bool addBalanceSheet(string @inout,int @update, Landlord landlord,string bill_type,int sign,string action)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {

                try
                {

                    connection.Open();
                    string procedure = "p_update_balnce_sheet";
                    int check = 0;

                    SqlCommand cmd = new SqlCommand(procedure, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", landlord.date);
                    cmd.Parameters.AddWithValue("@bill_type", bill_type);
                    cmd.Parameters.AddWithValue("@inout", @inout);
                    int cash = 0;

                    if (bill_type=="bipari")
                    {
                        cmd.Parameters.AddWithValue("@bill_id", landlord.record_id);
                        cmd.Parameters.AddWithValue("@desc", landlord.land_person.pname);
                        cash = (int)landlord.getGrandTotal();
                    }
                    else if (bill_type == "expense")
                    {
                        cmd.Parameters.AddWithValue("@bill_id", landlord.record_id);
                        cmd.Parameters.AddWithValue("@desc", landlord.land_person.pname);
                        cash = (int)landlord.getTotalService()+landlord.land_person.expense;
                    }
                    cmd.Parameters.AddWithValue("@amount", sign * cash);
                    cmd.Parameters.AddWithValue("@crud_action", action);
                    cmd.Parameters.AddWithValue("@update", @update);
                    check = cmd.ExecuteNonQuery();




                    if (check != 0)
                    {
                        connection.Close();
                        return true;
                    }


                    connection.Close();
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

        internal bool update_daily_table_cash_flow(string key,Landlord templandlord)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {

                try
                {
                    connection.Open();
                    string procedure = "p_update_daily_table";
                    int check = 0;
                    SqlCommand cmd = new SqlCommand(procedure, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", templandlord.date);
                    cmd.Parameters.AddWithValue("@record_id", templandlord.record_id);
                    cmd.Parameters.AddWithValue("@RQuantity", templandlord.land_product.sale_remaining_product);
                    cmd.Parameters.AddWithValue("@Sale_amount", templandlord.total_sale);
                    cmd.Parameters.AddWithValue("@cl_Commission", templandlord.total_commission);
                    cmd.Parameters.AddWithValue("@cl_Chongi", templandlord.total_chongi);
                    cmd.Parameters.AddWithValue("@acc_key", templandlord.total_chongi);

                    check = cmd.ExecuteNonQuery();
                    if (check != 0)
                    {
                        connection.Close();
                        return true;
                    }


                    connection.Close();
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

        public void getMultipleDayTransportRent(string s_date, string l_date)
        {
            string category = "Total_RE";
            string procedure = "p_showtoday_rent";
            DataTable tbl = (DataTable)getClientProductRecord(s_date, l_date, category);
        }

        //Daily Maal Amad
        public  List<Landlord> getTodayProductsForSale(string date,string khataid)
        {
            List<Landlord> client = new List<Landlord>();
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_todaysaleproduct", connection);
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
                    string _bipari_commission= row[22].ToString();
                    string _bipari_chongi = row[23].ToString();

                    Landlord temp = new Landlord();
                    temp.client._vehicle_id = _vehicle_id;
                    temp.client.record_id =_id;
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

                    temp.total_rent = int.Parse(_total_rent);
                    temp.total_labour = int.Parse(_total_labour);
                    temp.total_munshiana = int.Parse(_total_munshiana);






                    Person cl_person = new Person(_clientnameid, _key, _clientname, "",int.Parse(_advance), int.Parse(_expense));
                    temp.land_person = cl_person;


                    client.Add(temp);

                }
                connection.Close();
                return client;
            }

            return null;
        }
        public List<Landlord> getClientNot_HV_LandLords(string date)
        {
            List<Landlord> clients = new List<Landlord>();
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql_cl_nothvLandlord, connection);
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
                    temp.total_rent = int.Parse(_total_rent);
                    temp.total_labour = int.Parse(_total_labour);
                    temp.total_munshiana = int.Parse(_total_munshiana);

                    clients.Add(temp);

                }
                connection.Close();
            }
           
            return clients;
        }

        

        public object getClient_HV_LandLords(string date)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql_cl_hvLandlord, connection);
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = date;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }

        public object getTodayMaalAmad(string date)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_todayMaalAmadDetail", connection);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }




        public object getClient_TodayRent_Total(string date)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                
                connection.Open();
                SqlCommand cmd = new SqlCommand(todayRent_total, connection);
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl = new DataTable();
                adapt.Fill(data_tbl);
                return data_tbl;

            }
            return null;
        }

        public object getClientProductRecord(string sdate,string ldate,string category)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("dbo.p_search_ccpw", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = sdate;
                cmd.Parameters.Add("@lastdate", SqlDbType.NVarChar).Value = ldate;
                cmd.Parameters.Add("@category", SqlDbType.NVarChar).Value = category;
                adapt = new SqlDataAdapter(cmd);

                DataTable data_tbl= new DataTable();
                adapt.Fill(data_tbl);
                connection.Close();
                return data_tbl;
            }
            return null;
        }

        public AutoCompleteStringCollection autoCompleteData()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select expense_name_urdu from tbl_expensetypes;", connection);
               SqlDataReader rdr = cmd.ExecuteReader();
                //AutoCompleteStringCollection Contains a collection of strings to use for the auto-complete feature on certain Windows Forms controls.
                AutoCompleteStringCollection autoCompleteCollection = new AutoCompleteStringCollection();
                while (rdr.Read())
                {
                    autoCompleteCollection.Add(rdr.GetString(0));
                }
                return autoCompleteCollection;
            }
            return null;
        }
        internal bool insertTodayExpense(string date, string expense, string amount,string refid,string expense_from)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_insert_expense", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@param1", SqlDbType.NVarChar).Value =expense;
                cmd.Parameters.Add("@param2", SqlDbType.Int).Value = amount;
                cmd.Parameters.Add("@param3", SqlDbType.Int).Value = refid;
                cmd.Parameters.Add("@param4", SqlDbType.NVarChar).Value = expense_from;
                int check = cmd.ExecuteNonQuery();
                connection.Close();
                return true;

            }
            return false;
        }

        internal void addExpenseName(string text)
        {
            string sql = "insert into tbl_expensetypes(expense_name_urdu) values (@param)";
            using (SqlConnection connection = new SqlConnection(ConnectionSTring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param", SqlDbType.NVarChar).Value = text;
                int check = cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region SQL Queries
        string sql_cl_nothvLandlord = "Select c.client_id,client_name,d.product_id,p.urdu_veg_name,d.weight_id,b.urdu_beg_name"
     + ", d.product_quantity"
      + ", d.total_rent"
      + ", d.total_labour"
      + ", d.total_munshiana"
      + ", d.total_advance"
      + ", d.total_expense"
      + ", d.t_date"
      + ", d.t_client_key"
      + ", d.t_landlord_key"
      + ", d.t_type"
      + ", d.vehicle_number"
      + ", d.commission,d.chongi,d.labour,d.sale_remaining_product,d.bipari_commission,d.bipari_chongi "
+ "from tbl_client as c "
+ "inner join dbo.p_daily as d on c.client_id= d.client_id "
+ "inner join tbl_product as p on p.veg_id= d.product_id "
+ "inner join tbl_begcategory as b on b.beg_id= d.weight_id "
+ "where d.landlord_id= -1 AND d.t_date= @startdate;";

    string sql_cl_hvLandlord = "Select d.id,c.client_id,c.client_name,cl.client_id,cl.client_name,d.product_id,p.urdu_veg_name,d.weight_id,b.urdu_beg_name"
      + ", d.product_quantity"
      + ",d.total_rent"
      +",d.total_labour"
      +",d.total_munshiana"
      +",d.total_advance"
      +",d.total_expense"
      +",d.t_date"
      +",d.t_client_key"
      +",d.t_landlord_key"
      +",d.t_type"
     +" ,d.vehicle_number"
	+ ",d.commission,d.chongi,d.labour,d.sale_remaining_product,d.bipari_chongi,bipari_commission,rent_per_product,total_bipari_commission,total_bipari_chongi"
+ " from tbl_client as c "
+"inner join dbo.p_daily as d on c.client_id= d.client_id "
+"inner join tbl_product as p on p.veg_id= d.product_id "
+"inner join tbl_begcategory as b on b.beg_id= d.weight_id "
+"inner join tbl_client as cl on cl.client_id= d.landlord_id "
 +"where d.t_date= @startdate;";


        string sql_ProductSale= "Select d.id,c.client_id,c.client_name"+
            ",d.product_id,p.urdu_veg_name,d.weight_id,b.urdu_beg_name"
      + ", d.product_quantity"
      + ",d.total_rent"
      + ",d.total_labour"
      + ",d.total_munshiana"
      + ",d.total_advance"
      + ",d.total_expense"
      + ",d.t_date"
      + ",d.t_client_key"
      + ",d.t_landlord_key"
      + ",d.t_type"
     + " ,d.vehicle_number"
    + "  ,d.commission,d.chongi,d.labour,d.sale_remaining_product "
+ "from tbl_client as c "
+ "inner join dbo.p_daily as d on c.client_id= d.landlord_id "
+ "inner join tbl_product as p on p.veg_id= d.product_id "
+ "inner join tbl_begcategory as b on b.beg_id= d.weight_id "
 + "where d.t_date= @startdate;";

        string todayRent_total = " Select distinct SUM(d.product_quantity),SUM(d.total_rent),SUM(d.total_labour),SUM(d.total_munshiana),SUM(d.total_advance),SUM(d.total_expense),SUM(sale_remaining_product) from p_daily as d where t_date=@date;";



        string insert_driver = "INSERT INTO p_daily"
            +"(client_id, landlord_id, product_id, weight_id, product_quantity, total_rent,"
            +" total_labour,total_munshiana,total_advance, total_expense, t_date,"
            +" t_client_key,t_landlord_key, t_type, vehicle_number,commission,chongi,labour,"
            +"sale_remaining_product,check_client_commission,"
            +"check_customer_commission,customer_commission"
      + ", product_name,weight_name"
      + ")"
    + "VALUES output INSERTED.id(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10,"
            + "@p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18, @p19, @p20, @p21, @p22"
    + ", @p23, @p24)";


        #endregion
    }
}
