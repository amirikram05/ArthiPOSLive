using System.Collections.Generic;
using System.Data;

namespace DataMember
{
    public class ReportData
    {

        public string bikri_type;
        public string t_landlord_key;
        public string t_date;
        public string landlord_name;
        public string client_name;
        public string client_id;
        public string product_name;
        public int product_quantity;
        public int _quantity;
        public int _amount;
        public int sale_amount;
        public int sale_amount_landlord;
        public int _extra_landlord;
        public int total_sale_amount;
        public int grand_total;
        public int total_bipari_chongi;
        public float total_bipari_commission; 
        public int total_advance;
        public int total_rent;
        public int total_labour;
        public int total_munshiana;
        public string t_client_key;
        public float expense;
        public float comm_chongi;
        public int remaining_amount;
        public string customer_name;
       



        public static DataTable createSaleDataset(List<Landlord> tclients)
        {
            if (tclients == null)
                return null;
            List<ReportData> rds = new List<ReportData>();
            foreach (Landlord land in tclients)
            {
                foreach (Customer c in land.customers)
                {
                    ReportData r = new ReportData();
                    r.bikri_type = land.bikri_type;
                    r.t_landlord_key = land.land_person.pkey;
                    r.landlord_name = land.land_person.pname;
                    r.client_name = land.client._person_cl.pname;
                    r.t_client_key = land.client._person_cl.pkey;
                    r.client_id = land.client._person_cl.pid;
                    r.product_name = land.land_product._product_name;
                    r.product_quantity = land.total_quantity;
                    r.total_advance = land.expense.total_advance_amount;
                    r.total_bipari_chongi = (int)land.Total_Chongi;
                    r.total_bipari_commission = land.Total_Commission;
                    r.total_labour = land.expense.total_labour;
                    r.total_munshiana = land.expense.total_munshiana+land.expense.total_marketfee;
                    r.total_rent = land.expense.total_rent;
                    r.grand_total = (int)land.GetGrandTotal;
                    r.total_sale_amount = land.total_sale;
                    r.t_date = land.date;
                    r._quantity = c.sale._sale_quantity;
                    r._amount = (int)c.sale._sale_amount;
                    r.sale_amount = c.sale._TotalSaleAmount;
                    r.expense = (land.expense.total_rent + land.expense.total_labour +
                        land.expense.total_advance_amount + land.expense.total_munshiana+land.expense.total_marketfee+
                        ((int)land.Total_Commission + (int)land.Total_Chongi));
                    r.comm_chongi = ((int)land.Total_Commission + (int)land.Total_Chongi);
                    r._extra_landlord = c.sale._TotalExtraAmountLandlord;
                    r.sale_amount_landlord = c.sale.getTotalExtraAmountLandlord();
                    r.customer_name = c.customer_profile.pname;
                    r.remaining_amount = System.Math.Abs(c.RemainingAmount - (int)land.GetGrandTotal);
                    rds.Add(r);
                }

            }
            return dataintoDataTable(rds);
        }
        private static DataTable dataintoDataTable(List<ReportData> rds)
        {
            DataSet mydsTest = new DataSet();
            DataTable objDataTable = new DataTable("Sales");
            objDataTable.Columns.Add(new DataColumn("t_landlord_key"));
            objDataTable.Columns.Add(new DataColumn("landlord_name"));
            objDataTable.Columns.Add(new DataColumn("client_id"));
            objDataTable.Columns.Add(new DataColumn("client_name"));
            objDataTable.Columns.Add(new DataColumn("t_client_key"));
            objDataTable.Columns.Add(new DataColumn("product_name"));
            objDataTable.Columns.Add(new DataColumn("product_quantity", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("total_advance", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("total_bipari_chongi", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("total_bipari_commission", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("total_labour", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("total_munshiana", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("total_rent", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("grand_total"));
            objDataTable.Columns.Add(new DataColumn("total_sale_amount", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("t_date"));
            objDataTable.Columns.Add(new DataColumn("_quantity", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("_amount", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("sale_amount", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("expense", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("comm_chongi", typeof(float)));
            objDataTable.Columns.Add(new DataColumn("_extra_landlord", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("sale_amount_landlord", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("remaining_amount", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("customer_name"));
            objDataTable.Columns.Add(new DataColumn("bikri_type"));



            foreach (ReportData rd in rds)
            {
                objDataTable.Rows.Add(
                    rd.t_landlord_key, rd.landlord_name
                    , rd.client_id, rd.client_name, rd.t_client_key,
                    rd.product_name, rd.product_quantity, rd.total_advance,
                    rd.total_bipari_chongi, rd.total_bipari_commission,
                    rd.total_labour, rd.total_munshiana,
                    rd.total_rent, rd.grand_total, rd.total_sale_amount,
                    rd.t_date, rd._quantity, rd._amount, rd.sale_amount, rd.expense,
                    rd.comm_chongi, rd._extra_landlord,
                    rd.sale_amount_landlord,
                    rd.remaining_amount,
                    rd.customer_name,rd.bikri_type

                    );
            }
            //mydsTest.Tables.Add(objDataTable);
            return objDataTable;//mydsTest;
        }


    }
    public class ReportDataCustomer
    {
        /*t_date,
        ID
        Name,
        KhataNO ,
        TotalQuantity,
        TotalSale,
        ProductName,
        Quantity,
        Rate,
        SaleAmount,
        CommChongi,
        grand_total*/
        public string t_date;
        public string ID;
        public string Name;
        public string KhataNO;
        public int TotalQuantity;
        public int TotalSale;
        public string ProductName;
        public int Quantity;
        public int Rate;
        public int SaleAmount;
        public int CommChongi;
        public int grand_total;
        public int remaining_amount = 0;




        public static DataTable createSaleDataset(List<Landlord> tclients)
        {
            if (tclients == null)
                return null;
            List<ReportDataCustomer> rds = new List<ReportDataCustomer>();
            foreach (Landlord land in tclients)
            {
                foreach (Customer c in land.customers)
                {
                    ReportDataCustomer r = new ReportDataCustomer();
                    r.t_date = c.date;
                    r.ID = c.customer_profile.pid;
                    r.Name = c.customer_profile.pname;
                    r.KhataNO = c.customer_profile.pkey;
                    r.TotalQuantity = c.product.total_Quantity;
                    r.TotalSale = c.sale._TotalSaleAmount + c.sale._TotalExtraAmountCustomer;
                    c.sale.getTotalExtraAmountCustomer();
                    r.ProductName = c.product._product_name;
                    r.Quantity = c.sale._sale_quantity;
                    r.Rate = (int)c.sale._sale_amount;
                    r.SaleAmount = c.sale.getTotalSale() + c.sale.getTotalExtraAmountCustomer() + (int)(c.Total_Commission + c.Total_Chongi);
                    r.CommChongi = (int)(c.Total_Commission + c.Total_Chongi);
                    r.grand_total = c.getGrandTotalCustomer();
                    r.remaining_amount = c.RemainingAmount;
                    rds.Add(r);
                }
            }

            return dataintoDataTable(rds);
        }
        private static DataTable dataintoDataTable(List<ReportDataCustomer> rds)
        {
            DataSet mydsTest = new DataSet();
            DataTable objDataTable = new DataTable("CustomerSales");
            objDataTable.Columns.Add(new DataColumn("t_date"));
            objDataTable.Columns.Add(new DataColumn("ID"));
            objDataTable.Columns.Add(new DataColumn("Name"));
            objDataTable.Columns.Add(new DataColumn("KhataNo"));
            objDataTable.Columns.Add(new DataColumn("TotalQuantity", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("TotalSale", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("ProductName"));
            objDataTable.Columns.Add(new DataColumn("Quantity", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("Rate", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("SaleAmount", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("CommChongi", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("grand_total", typeof(int)));
            objDataTable.Columns.Add(new DataColumn("remaining_amount", typeof(int)));
            /*for (int i = 0; i < rds.Count; i++)
            {
                ReportDataCustomer r = rds[i];
                string id = r.ID;
                int totalQuantity = 0, grandtotal = 0, totalsale = 0;
                foreach (ReportDataCustomer rd in rds)
                {
                    if (rd.ID==r.ID)
                    {

                        totalQuantity += rd.Quantity;
                        grandtotal += rd.grand_total;
                        totalsale = rd.TotalSale;
                    }
                }


                rds[i].TotalQuantity = totalQuantity;
                rds[i].grand_total = grandtotal;
                rds[i].TotalSale = totalsale;
            }*/



            foreach (ReportDataCustomer rd in rds)
            {
                objDataTable.Rows.Add(
                    rd.t_date, rd.ID, rd.Name, rd.KhataNO, rd.TotalQuantity, rd.TotalSale, rd.ProductName, rd.Quantity,
                    rd.Rate, rd.SaleAmount, rd.CommChongi, rd.grand_total);
            }
            //mydsTest.Tables.Add(objDataTable);
            return objDataTable;//mydsTest;
        }
    }

}
