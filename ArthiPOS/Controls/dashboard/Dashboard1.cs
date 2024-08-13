using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ArthiPOS.utill;
using BAL;
using ArthiPOS.Properties;
using DataMember;
using ArthiPOS.Utill;
using System.IO;
using CommonUtilities;
using DataMember.memberlog;

namespace ArthiPOS.Controls.dashboard
{
    public partial class Dashboard1 : UserControl
    {
        private Sub_DashboardDailySales newSub_DashboardDailySales;
        private Sub_DasboardControl newSub_DasboardControl;
        string date = "";
        BLogic bal;
        Account account;
        SaleParser saleParser;
        AdminLog adminlog;

        public Dashboard1()
        {
            InitializeComponent();
            date = sale_date.Text;
            saleParser = new SaleParser(date,Admin.SaveLog);
            Admin.Date= DateTime.Now.ToString("yyyy-MM-dd"); 
            bal = new BLogic();
            adminlog = LogUtill.getAdminInputLog();

            udateSection1();
            getUpdateSale();
        }


        #region Localization
        public void udateSection1()
        {
            lbl_customer_sales.Text = Resources.ResourceManager.GetString("a1063");//Customer Sales
            lbl_cust_total_sales.Text = Resources.ResourceManager.GetString("a0504");//Total Sales
            lbl_quantity.Text = Resources.ResourceManager.GetString("a0401");// Quantity
            lbl_cust_sales.Text = Resources.ResourceManager.GetString("a1062");//Sales
            lbl_commisison.Text = Resources.ResourceManager.GetString("a0302");// Commission
            lbl_chongi.Text = Resources.ResourceManager.GetString("a0301");//Chongi
            lbl_total_cust.Text = Resources.ResourceManager.GetString("a1053");//Total

            lbl_vendor_quantity.Text = Resources.ResourceManager.GetString("a0401");//Quantity
            lbl_vendor_sales.Text = Resources.ResourceManager.GetString("a1027");// Vendor Sales
            lbl_vendor_total_sales.Text = Resources.ResourceManager.GetString("a0504"); // Total Sales
            lbl_vendor_ritems.Text = Resources.ResourceManager.GetString("a1019");// Remaining Product
            lbl_advance.Text = Resources.ResourceManager.GetString("a0305");// Advance
            lbl_fright.Text = Resources.ResourceManager.GetString("a0304");//Fright
            lbl_labour.Text = Resources.ResourceManager.GetString("a0303");//Labour
            lbl_munshiana.Text = Resources.ResourceManager.GetString("a0307");//Munshiana
            lbl_expense.Text = Resources.ResourceManager.GetString("a0306");//Expense
            lbl_total_sales.Text = Resources.ResourceManager.GetString("a0504");//Total Sales
            lbl_total_vendor.Text = Resources.ResourceManager.GetString("a1053");//Total
            lbl_bipari_cc.Text = Resources.ResourceManager.GetString("a0302") + " " + Resources.ResourceManager.GetString("a0301"); ;



            lbl_vendor.Text = Resources.ResourceManager.GetString("a1002");//Total Vendor
            lbl_customers.Text = Resources.ResourceManager.GetString("a0202");//Total Customers
            lbl_admin.Text = Resources.ResourceManager.GetString("a1015");//Admin
            lbl_start_balance.Text = Resources.ResourceManager.GetString("a1071");//start balance
            lbl_shop_expense.Text = Resources.ResourceManager.GetString("a1022");// Shop Expense
            lbl_cash_sale.Text = Resources.ResourceManager.GetString("a1081");//Cash Sale
            lbl_receivings.Text = Resources.ResourceManager.GetString("a1021");// Recevings
            lbl_balance.Text = Resources.ResourceManager.GetString("a1073");// Balance

        }
        #endregion

        private void previousdate_Click(object sender, EventArgs e)
        {
            sale_date.Value = CommonUtill.ChangeDate(sale_date, -1);
            date = sale_date.Text;
        }

        private void nextdate_Click(object sender, EventArgs e)
        {
            sale_date.Value = CommonUtill.ChangeDate(sale_date, 1);
            date = sale_date.Text;
        }

        private void sale_date_ValueChanged(object sender, EventArgs e)
        {
            date = sale_date.Text;
            newSub_DasboardControl.date = date;
            newSub_DashboardDailySales.date=date;

            newSub_DasboardControl.init(date);//refresh
            init();
        }

        private void init()
        {
            
            DataTable sales = bal.getDashBoardSales(date);
            DataTable custsale = bal.getDashBoardCustSales(date);
            account = Authentication.Account;

            string sbalance=bal.getCapitalPreviousDayCash(account.api_key,date, date);
            string balance = bal.getCapitalCash(account.api_key);
            DataRow dr_Sale = sales.Rows[0];
            DataRow dr_custsale = custsale.Rows[0];
            
            updateadmin(dr_custsale[0].ToString(), dr_Sale[0].ToString(), 
                sbalance,
                dr_Sale[1].ToString(),
                dr_custsale[2].ToString(),
                 dr_custsale[1].ToString(),
                 balance);
            updatevendor(dr_Sale[2].ToString(),
                dr_Sale[0].ToString(),
                dr_Sale[9].ToString(),
                dr_Sale[3].ToString(),
                dr_Sale[4].ToString(),
                dr_Sale[5].ToString(),
                dr_Sale[6].ToString(),
                dr_Sale[7].ToString(),
                dr_Sale[8].ToString(),
                dr_Sale[9].ToString(),
                dr_Sale[10].ToString(),
                dr_Sale[11].ToString()
                );

            updateCustomerSales(dr_custsale[0].ToString(),
                dr_custsale[3].ToString(),
                dr_custsale[4].ToString(),
                dr_custsale[5].ToString(), 
                dr_custsale[6].ToString(), 
                dr_custsale[7].ToString(),
                dr_custsale[8].ToString());



        }
        private void updateadmin(string vendortotalcount,string customertotalcount,
           string sbalance,string shopexpense,string cashsale,string receivings,
           string balance)
        {
            if (int.Parse(sbalance)==0)
            {
                _lbl_start_balance.Text = balance;
                _lbl_balance.Text = balance;

            }
            else
            {
                _lbl_start_balance.Text = sbalance;
                _lbl_balance.Text = sbalance;

            }
            _lbl_vendor.Text = vendortotalcount;
            _lbl_shop_expense.Text = shopexpense;
            _lbl_cash_sale.Text = cashsale;
            _lbl_receivings.Text = receivings;
            //_lbl_balance.Text = balance;
            _lbl_customers.Text = customertotalcount;
        }
        private void updateCustomerSales(string count, string totalsale,
            string quantity,string sale,string commission,string chongi,
            string total)
        {
            _lbl_quantity.Text = quantity;
            lbl_customer_total_count.Text = count;
            _lbl_cust_total_sales.Text = ""+totalsale;
            _lbl_cust_sales.Text = sale;
            _lbl_commission.Text = commission;
            _lbl_chongi.Text = chongi;
            _lbl_total_cust.Text = ""+total;
        }
        private void updatevendor(string quantity,string count,
            string gtotalsale,
            string ritem,string advance,string fright,string labour,
            string munshiana,string totalExpense,string totalsale,
            string total,string chcomiss)
        {

            _lbl_vendor_quantity.Text = quantity;
            lbl_vendor_count.Text = count;
            _lbl_vendor_total_sales.Text = gtotalsale;
            _lbl_vendor_ritems.Text = ritem;
            _lbl_advance.Text = advance;
            _lbl_fright.Text = fright;
            _lbl_labour.Text = labour;
            _lbl_munshiana.Text = munshiana;
            _lbl_expense.Text = totalExpense;
            _lbl_total_sales.Text = totalsale;
            _lbl_total_vendor.Text = total;
            _lbl_bipari_cc.Text = chcomiss;
        }

        private void Dsahboard1_Load(object sender, EventArgs e)
        {
            date = sale_date.Text;
            newSub_DashboardDailySales = new Sub_DashboardDailySales();
            newSub_DasboardControl = new Sub_DasboardControl(date);
            this.flowLayoutPanel1.Controls.Add(newSub_DasboardControl);
            this.flowLayoutPanel1.Controls.Add(newSub_DashboardDailySales);

            newSub_DasboardControl.init(date);//refresh
            init();


        }

        public void getUpdateSale()
        {
            FileInfo[] files = saleParser.getAllFiles(adminlog.SalesInProccessedFolder,true);

            if (files.Length > 0)
            {
                btn_update_sales.Textcolor = Color.Red;
                //btn_update_sales.Enabled = true;
                btn_update_sales.Text = Resources.ResourceManager.GetString("a1085")+ "\t\t(" + files.Length+")";
            }
            else
            {
                btn_update_sales.Textcolor = Color.DimGray;
                //btn_update_sales.Enabled = false;
                btn_update_sales.Text = Resources.ResourceManager.GetString("a1085") + "\t\t(0)";
            }
        }
        private void btn_update_sales_Click(object sender, EventArgs e)
        {
            SalesUpdate sales = new SalesUpdate();
            sales.initSalesUpdate(saleParser, adminlog.SalesInProccessedFolder,"Default");
            sales.ShowDialog();
        }
    }
}
