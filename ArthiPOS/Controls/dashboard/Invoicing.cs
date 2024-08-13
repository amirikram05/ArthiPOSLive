using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using ArthiPOS.utill;
using BAL;
using DataMember;
using System.Drawing.Printing;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using ArthiPOS.Reporting;
using ArthiPOS.Utill;
using ArthiPOS.Controls.dashboard;
using System.IO;
using ArthiPOS.Properties;
using ArthiPOS.Reporting.ReportView.Header;
using ArthiPOS.Reporting.ReportView.NoHeader;
using ArthiPOS.Reporting.ReportDataSet;
using CommonUtilities;
using DataMember.memberlog;

namespace ArthiPOS.controls.dashboard
{
    public partial class Invoicing : UserControl
    {
        private BLogic bal;
        InvoiceControl invoicing;
        List<Landlord> tLandlords;
        List<Customer> customers;
        string date;
        SaleParser saleParser;
        AdminLog adminlog;

        public Invoicing()
        {
            InitializeComponent();
           
        }
        public Invoicing(string date)
        {
            InitializeComponent();
            this.date = date;
            today_date.Text=date;
        }

        public void addViews()
        {
            /*for (int i=0;i<12;i++)
            {
                // start the waiting animation
                circularProgress1.Visible = true;

                /*Thread t = new Thread(new ThreadStart(() => RUN(i)));
                t.Start();*/
                invoicing = new InvoiceControl(null,null, null,"");
                btn_print_all_bill.Controls.Add(invoicing);

                // re-enable things
            //}

        }

        private void RUN(int i)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate ()
                {
                    invoicing = new InvoiceControl(null,null, null, "");
                    btn_print_all_bill.Controls.Add(invoicing);
                });
            }
            else
            {
                invoicing = new InvoiceControl(null, null,null, "");
                btn_print_all_bill.Controls.Add(invoicing);
            }
        }
        bool localRecord=false;
        public void readClientDailySale(string date,string search)
        {
            tLandlords = bal.getLandlordsList(date, search);
            if (tLandlords.Count > 0)
            {
                localRecord = false;
            }
            else
            {
                localRecord = true;
                if (saleParser==null)
                {
                    saleParser = new SaleParser(date,Admin.SaveLog);
                }
                tLandlords = saleParser.LoadTodaySale();
                if (tLandlords==null)
                {
                    return;
                }
                if (search != "")
                {
                    btn_print_all_bill.Controls.Clear();
                    int count = 0;
                    foreach (Landlord land in tLandlords)
                    {
                        Person p = land.land_person;
                        if (p.pid.Contains(search) || p.pname.Contains(search))
                        {
                            invoicing = new InvoiceControl(land, null, null, land.date);
                            //invoicing.panel_header_top.BackColor = CustomColors.getColor();
                            invoicing.lbl_count.Text = "" + ++count;
                            btn_print_all_bill.Controls.Add(invoicing);
                        }
                    }
                }
            }

            if (search=="" || !localRecord)
            {
                btn_print_all_bill.Controls.Clear();
                addControls(tLandlords);

            }
        }

        private void addControls(List<Landlord> tLandlords)
        {

            int count = 0;
            foreach (Landlord land in tLandlords)
            {
                invoicing = new InvoiceControl(land, null, null, land.date);
                //invoicing.panel_header_top.BackColor = CustomColors.getColor();
                invoicing.lbl_count.Text = "" + ++count;
                btn_print_all_bill.Controls.Add(invoicing);
            }
        }

        public void readCustomerDailySale(string date,string search)
        {
            //customers = bal.getCustomerBills(date, true);
            List<DataMember.CustomerSales> custSales = bal.getCustomerBills(date);
            if (custSales.Count > 0)
            {
                localRecord = false;
            }
            else
            {
                localRecord = true;
                if (saleParser == null)
                {
                    saleParser = new SaleParser(date, Admin.SaveLog);
                }
                tLandlords = saleParser.LoadTodaySale();

                if (tLandlords != null)
                {
                    btn_print_all_bill.Controls.Clear();
                    int count = 0;
                    List<Customer> custs = new List<Customer>();
                    foreach (Landlord l in tLandlords)
                    {
                        foreach (Customer c in l.customers)
                        {
                            custs.Add(c);
                        }
                    }

                    if (custSales == null)
                    {
                        custSales = new List<DataMember.CustomerSales>();
                    }
                    int j = 0;


                    foreach (Customer c in custs)
                    {
                        bool exists = custs.Any(item => (item.customer_profile.pid.Contains(search) || item.customer_profile.pname.Contains(search)));

                        if (exists && search!="")
                        {
                            bool check = custSales.Any(item => item.person.pkey == c.customer_profile.pkey);
                            if (!check)
                            {
                                DataMember.CustomerSales cs = new DataMember.CustomerSales(date);
                                cs.person = c.customer_profile;
                                cs.RemainingAmount = c.RemainingAmount;
                                cs.expense = c.expense;
                                foreach (Customer ct in custs)
                                {
                                    if (c.customer_profile.pkey == ct.customer_profile.pkey)
                                    {
                                        cs.customers.Add(c);
                                    }

                                }
                                cs.getQuantity();
                                cs.getSaleTotal();
                                cs.getTotalChongi();
                                cs.getTotalCommission();
                                custSales.Add(cs);

                            }
                        }
                        else
                        {
                            bool check = custSales.Any(item => item.person.pkey == c.customer_profile.pkey);
                            if (!check)
                            {
                                DataMember.CustomerSales cs = new DataMember.CustomerSales(date);
                                cs.person = c.customer_profile;
                                cs.RemainingAmount = c.RemainingAmount;
                                cs.expense = c.expense;
                                foreach (Customer ct in custs)
                                {
                                    if (c.customer_profile.pkey == ct.customer_profile.pkey)
                                    {
                                        cs.customers.Add(c);
                                    }

                                }
                                cs.getQuantity();
                                cs.getSaleTotal();
                                cs.getTotalChongi();
                                cs.getTotalCommission();
                                custSales.Add(cs);

                            }
                        }
                    }
                }

            }



            if (custSales.Count > 0)
            {
                //localRecord = false;
                int count = 0;
                foreach (DataMember.CustomerSales cs in custSales)
                {
                    //cust.isCustomerBill = true;
                    invoicing = new InvoiceControl(null, cs,null, date);
                   // invoicing.isLocal = localRecord;
                    //invoicing.panel_header_top.BackColor = CustomColors.getColor();
                    if (localRecord)
                    {
                        invoicing.isLocal = localRecord;
                    }
                    invoicing.lbl_count.Text = "" + ++count;
                    btn_print_all_bill.Controls.Add(invoicing);
                }
            }
            

        }
        /*
        private List<Customer> loadCustomers(List<Landlord> landlords)
        {
            List<Customer> customersAll = new List<Customer>();
            foreach (Landlord landlord in landlords)
            {
                foreach (Customer customer in landlord.customers)
                {
                    customersAll.Add(customer);   
                }
            }

            List<Customer> customerSale = new List<Customer>();
            foreach (Customer tcust in customersAll)
            {
                Customer addCust = new Customer(tcust.landloard.service,true);
                addCust.customer_profile.pkey = tcust.customer_profile.pkey;
                addCust.customer_profile.pid = tcust.customer_profile.pid;
                addCust.customer_profile.pname = tcust.customer_profile.pname;
                addCust.landloard.date = tcust.landloard.date;
                addCust = tcust;
                foreach (Customer scust in customersAll)
                {
                    if (scust.customer_profile.pkey==tcust.customer_profile.pkey)
                    {
                        scust.isCustomerBill = true;
                        addCust.sale = scust.sale;
                        addCust.product = scust.product;

                        addCust.total_quantity +=scust.total_quantity ;
                        addCust.total_sale += (int)scust.GetTotalSaleCustomer;
                        addCust.GetGrandTotalCustomer += scust.GetGrandTotalCustomer;
                        addCust.Total_Commission =scust.Total_Commission;
                        addCust.Total_Chongi = scust.Total_Chongi;
                        addCust.total_chalan++;
                    }
                }
                customerSale.Add(addCust);
            }

            return customerSale;
        }
*/
        private void Invoicing_Load(object sender, EventArgs e)
        {
            this.bal = new BLogic();
            date = today_date.Text;
            billtype_combo.SelectedIndex=0;
            saleParser = new SaleParser(date, Admin.SaveLog);
            adminlog = LogUtill.getAdminInputLog();
            getUpdateSale();
            //readClientDailySale(date);

        }

        private void billtype_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            callCustomerClientSales();   
        }

        private void callCustomerClientSales()
        {
            saleParser = new SaleParser(date, Admin.SaveLog);
            btn_print_all_bill.Controls.Clear();
            if (billtype_combo.Text == "Client")
            {
                readClientDailySale(date,"");
            }
            else if (billtype_combo.Text == "Customer")
            {
                readCustomerDailySale(date,"");
            }
            else if (billtype_combo.Text == "Landlord")
            {
                readLandlordClientSale();
            }
            if (localRecord)
            {
                chk_status_localload.Checked = true;
                chk_status_localload.BackColor = Color.FromArgb(0xFF, 0x66, 0x33);
                chk_status_localload.Text = "Sales Not in DB.";
            }
            else
            {
                chk_status_localload.Checked = false;
                chk_status_localload.BackColor = Color.FromArgb(0x99, 0xFF, 0x00);
                chk_status_localload.Text = "Sales are Updated.Load Local File";

            }
        }
       
        public void readLandlordClientSale()
        {
            DataTable dt = bal.readLandlordDailySale(today_date.Text, today_date.Text, "");
            AllReportsCC rp = new AllReportsCC();
            rp.ClientSaleDetail(dt);
            rp.ShowDialog();
        }

        private void today_date_ValueChanged(object sender, EventArgs e)
        {
            date = today_date.Text;
            callCustomerClientSales();
        }

        private void nextdate_Click(object sender, EventArgs e)
        {
            today_date.Value = CommonUtill.ChangeDate(today_date, 1);
            date = today_date.Text;
        }

        private void previousdate_Click(object sender, EventArgs e)
        {
            today_date.Value = CommonUtill.ChangeDate(today_date, -1);
            date = today_date.Text;
        }




        /**
        * 
        * 
        * 
        * Multipages Print
        * 
        * */
        private PrinterSettings prnSettings;

        private void multiplePages(XtraReport report1,List<XtraReport> reports)
        {
            //XtraReport report1 = new XtraReport();
            //XtraReport[] reports = new XtraReport[] { new XtraReport(), new XtraReport() };

            ReportPrintTool pt1 = new ReportPrintTool(report1);
            pt1.PrintingSystem.StartPrint += new PrintDocumentEventHandler(PrintingSystem_StartPrint);

            foreach (XtraReport report in reports)
            {
                ReportPrintTool pts = new ReportPrintTool(report);
                pts.PrintingSystem.StartPrint +=
                    new PrintDocumentEventHandler(reportsStartPrintEventHandler);
            }

            pt1.PrintDialog();
            foreach (XtraReport report in reports)
             {
                 ReportPrintTool pts = new ReportPrintTool(report);
                 pts.Print();
             }
        }

        void PrintingSystem_StartPrint(object sender, PrintDocumentEventArgs e)
        {
            prnSettings = e.PrintDocument.PrinterSettings;
        }

        private void reportsStartPrintEventHandler(object sender, PrintDocumentEventArgs e)
        {
            int pageCount = e.PrintDocument.PrinterSettings.ToPage;
            e.PrintDocument.PrinterSettings = prnSettings;

            // The following line is required if the number of pages for each report varies,  
            // and you consistently need to print all pages. 
            e.PrintDocument.PrinterSettings.ToPage = pageCount;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            if (!localRecord)
            {
                if (billtype_combo.Text == "Client")
                {


                    //AllReportsCC rep = new AllReportsCC(tLandlords,date);
                    //rep.ShowDialog();
                    ReportPages rp = new ReportPages(true, false, tLandlords, customers, date);
                    rp.btn_bill_Report.Enabled = true;
                    rp.isLocal = localRecord;
                    rp.ShowDialog();

                }
                else if (billtype_combo.Text == "Customer")
                {
                    /*foreach (Customer customer in customers)
                    {
                        using (RCBilling rc = new RCBilling(customer,date))
                        {
                            rc.ShowDialog();
                        }
                    }*/
                    //AllReportsCC rep = new AllReportsCC(customers, date);
                    //rep.ShowDialog();
                    ReportPages rp = new ReportPages(true, true, tLandlords, customers, date);
                    rp.btn_bill_Report.Enabled = true;
                    rp.isLocal = localRecord;
                    rp.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Data Display From Local. Please Update your Database.");

                //SalesTodayNHA5 rb = new SalesTodayNHA5();
                /*if (billtype_combo.Text == "Client")
                {
                    //SalesTodayAllDetail rb = new SalesTodayAllDetail();
                    SalesTodayNHA7 rb = new SalesTodayNHA7();
                    DataTable dt = ReportData.createSaleDataset(tLandlords);
                    AllReportsCC all = new AllReportsCC();
                    rb.Database.Tables["Sales"].SetDataSource(dt);
                    if (dt == null)
                        return;
                    rb.SetParameterValue("Title", Authentication.Account.shop_name);
                    rb.SetParameterValue("Propriter", Authentication.Account.propriters_name);
                    rb.SetParameterValue("Name1", Authentication.Account.name1 ?? "");
                    rb.SetParameterValue("Phone1", Authentication.Account.phone1 ?? "");
                    rb.SetParameterValue("Name2", Authentication.Account.name2 ?? "");
                    rb.SetParameterValue("Phone2", Authentication.Account.phone2 ?? "");
                    rb.SetParameterValue("Address", Authentication.Account.address ?? "");
                    rb.SetParameterValue("Business", Authentication.Account.business_type ?? "");
                    all.crystalReportViewer1.ReportSource = rb;
                    all.ShowDialog();

                }*/
                //else if (billtype_combo.Text == "Customer")
                {
                    CustomDailog frm = new CustomDailog(tLandlords,billtype_combo.Text,"ALL Pages", "Local Invoice");
                    frm.ShowDialog();

                    /*SalesTodayCustAllDetail rb = new SalesTodayCustAllDetail();
                    DataTable dt = ReportDataCustomer.createSaleDataset(tLandlords);
                    AllReportsCC all = new AllReportsCC();
                    rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                    if (dt == null)
                        return;
                    rb.SetParameterValue("Title", Authentication.Account.shop_name);
                    rb.SetParameterValue("Propriter", Authentication.Account.propriters_name);
                    rb.SetParameterValue("Name1", Authentication.Account.name1 ?? "");
                    rb.SetParameterValue("Phone1", Authentication.Account.phone1 ?? "");
                    rb.SetParameterValue("Name2", Authentication.Account.name2 ?? "");
                    rb.SetParameterValue("Phone2", Authentication.Account.phone2 ?? "");
                    rb.SetParameterValue("Address", Authentication.Account.address ?? "");
                    rb.SetParameterValue("Business", Authentication.Account.business_type ?? "");
                    all.crystalReportViewer1.ReportSource = rb;
                    all.ShowDialog();*/
                }

            }


        }

     
        public void getUpdateSale()
        {
            FileInfo[] files = saleParser.getAllFiles(adminlog.SalesInProccessedFolder, true);

            if (files.Length > 0)
            {
                btn_update_sales.Textcolor = Color.Red;
                //btn_update_sales.Enabled = true;
                btn_update_sales.Text = string.Format("{0}\t\t({1})", Resources.ResourceManager.GetString("a1085"), files.Length);
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
            sales.initSalesUpdate(saleParser, adminlog.SalesInProccessedFolder, "Default");
            sales.ShowDialog();
            getUpdateSale();
        }

        private void chk_status_localload_Click(object sender, EventArgs e)
        {
            btn_print_all_bill.Controls.Clear();
            if (billtype_combo.Text == "Client")
            {
                if (saleParser == null)
                {
                    saleParser = new SaleParser(date, Admin.SaveLog);
                }
                tLandlords = saleParser.LoadProcessedTodaySale();
                if (tLandlords == null)
                {
                    return;
                }
                addControls(tLandlords);
            }
            else
            if (billtype_combo.Text == "Customer")
            {
                readCustomerDailySale(date,"");
            }
        }



        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            
            if (billtype_combo.Text == "Client")
            {

                readClientDailySale(date,txt_search.Text);
            }
            else
            if (billtype_combo.Text == "Customer")
            {
                readCustomerDailySale(date,txt_search.Text);
            }

        }

       
    }
}
