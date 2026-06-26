using ArthiPOS.Reporting.ReportView;
using BAL;
using DataMember;
using System;
using System.Data;
using System.Windows.Forms;

namespace ArthiPOS.Reporting
{
    public partial class RCBilling : Form
    {
        private Landlord landlord;
        private Customer customer;
        private string date;
        public RCBilling()
        {
            InitializeComponent();
        }
        public RCBilling(Landlord landlord, string date)
        {
            InitializeComponent();
            this.landlord = landlord;
            this.date = date;
        }
        public RCBilling(Customer customer, string date)
        {
            InitializeComponent();
            this.customer = customer;
            this.date = date;
        }

        private void RCBilling_Load(object sender, EventArgs e)
        {

            ReportBilling rb = new ReportBilling();
            string key = "",
                pid = "",
                pname = "",
                date = "";
            int advance = 0,
            rent = 0,
            labour = 0,
            munshiana = 0,
            service = 0,
            bill_amount = 0,
            sum_sales = 0,
            total_quantity = 0;
            float marketfee = 0;
            if (landlord != null)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(new BLogic().getClient_Sales("ByID", "", landlord.record_id));
                rb.Database.Tables["CustomerSales"].SetDataSource(ds.Tables[0]);
                key = landlord.land_person.pkey;
                pid = landlord.land_person.pid;
                pname = landlord.land_person.pname;
                date = landlord.date;
                advance = landlord.expense.total_advance_amount;
                rent = landlord.expense.total_rent;
                labour = landlord.expense.total_labour;
                munshiana = landlord.expense.total_munshiana;
                service = (int)landlord.GetTotalService + (int)Math.Ceiling(landlord.GetCommission) + (int)landlord.GetChongi;
                bill_amount = (int)landlord.GetGrandTotal;
                sum_sales = landlord.total_sale;
                total_quantity = landlord.land_product.total_Quantity;
                marketfee = landlord.GetChongi + landlord.GetCommission;

            }
            else if (customer != null)
            {
                BLogic bal = new BLogic();
                DataSet ds = new DataSet();
                DataTable dt = bal.getp_customer_sale_CRUD("SearchSalesByDate_Key", customer.customer_profile.pkey, "");
                DataMember.CustomerSales cs = bal.getCustomerSales(dt, this.date);
                ds.Tables.Add(dt);
                rb.Database.Tables["CustomerSales"].SetDataSource(ds.Tables[0]);


                DataRow cr = ds.Tables[0].Rows[0];
                key = cr[0].ToString();
                pid = cr[1].ToString();
                pname = cr[2].ToString();
                sum_sales = cs.total_sale;
                bill_amount = cs.GetGrandTotal;
                total_quantity = cs.total_quantity;






            }



            rb.SetParameterValue("bill_id", key);
            rb.SetParameterValue("khata_id", pid);
            rb.SetParameterValue("name", pname);
            rb.SetParameterValue("date", date);
            rb.SetParameterValue("advance", advance);
            rb.SetParameterValue("rent", rent);
            rb.SetParameterValue("labour", labour);
            rb.SetParameterValue("munshiana", munshiana);
            rb.SetParameterValue("total_service", service);
            rb.SetParameterValue("bill_amount", bill_amount);
            rb.SetParameterValue("sum_total_sale", sum_sales);
            rb.SetParameterValue("total_quantity", total_quantity);

            rb.SetParameterValue("market_fee", marketfee);
            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
        }


    }
}
