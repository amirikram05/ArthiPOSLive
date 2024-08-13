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
    public partial class InvoicingPage : UserControl
    {
        private BLogic bal;
        List<Landlord> tLandlords;
        List<Customer> customers;
        List<DataMember.CustomerSales> custSales;
        string date;
        SaleParser saleParser;
        AdminLog adminlog;

        public InvoicingPage()
        {
            InitializeComponent();

        }
        public InvoicingPage(string date)
        {
            InitializeComponent();
            this.date = date;
            today_date.Text = date;
            this.dg_invoice.Focus();

        }



        bool localRecord = false;
        public void readClientDailySale(string sdate, string search)
        {
            if(rd_both.Checked || rd_live.Checked)
                tLandlords = bal.getLandlordsList(sdate, search);
            if (tLandlords == null) tLandlords = new List<Landlord>();

            if ( tLandlords.Count > 0)
            {
                localRecord = false;
            }
            else if (rd_both.Checked || rd_local.Checked)
            {
                localRecord = true;
                if (saleParser == null)
                {
                    saleParser = new SaleParser(date, Admin.SaveLog);
                }
                tLandlords = saleParser.LoadTodaySale();
                if (tLandlords == null)
                {
                    return;
                }
                if (search != "")
                {
                    int count = 0;
                    addControls(tLandlords, null);

                }
            }

            if (search == "" || !localRecord)
            {
                addControls(tLandlords, null);

            }
        }

        private void addControls(List<Landlord> tLandlords, List<DataMember.CustomerSales> custSales)
        {
            dg_invoice.Rows.Clear();
            dg_invoice.Refresh();

            int count = 0;
            if (tLandlords != null)
            {
                foreach (Landlord land in tLandlords)
                {
                    addRowingrid_Invoice(land);
                }
            }
            else if (custSales != null)
            {
                this.custSales = custSales;
                foreach (DataMember.CustomerSales sales in custSales)
                {
                    //invoicing = new InvoiceControl(land, null, null, land.date);
                    ////invoicing.panel_header_top.BackColor = CustomColors.getColor();
                    //invoicing.lbl_count.Text = "" + ++count;
                    //btn_print_all_bill.Controls.Add(invoicing);
                    addRowingrid_Invoice(sales);


                }
            }
        }
        private void addRowingrid_Invoice(Landlord land)
        {
            int count = this.dg_invoice.Rows.Count;

            this.dg_invoice.Rows.Add();
            this.dg_invoice.Rows[count - 1].Cells[0].Value = land.date;
            this.dg_invoice.Rows[count - 1].Cells[1].Value = land.land_person.pid;
            this.dg_invoice.Rows[count - 1].Cells[2].Value = land.land_person.pname;
            this.dg_invoice.Rows[count - 1].Cells[3].Value = land.land_person.pkey;
            this.dg_invoice.Rows[count - 1].Cells[4].Value = land.land_product.total_Quantity;
            this.dg_invoice.Rows[count - 1].Cells[5].Value = (int)land.total_sale;
            this.dg_invoice.Rows[count - 1].Cells[6].Value = 0;
            this.dg_invoice.Rows[count - 1].Cells[7].Value = (int)land.GetTotalService + (int)(land.GetCommission + land.GetChongi);
            this.dg_invoice.Rows[count - 1].Cells[8].Value = (int)land.GetGrandTotal;
        }




        private void addRowingrid_Invoice(DataMember.CustomerSales sales)
        {
            int count = this.dg_invoice.Rows.Count;
            this.dg_invoice.Rows.Add();
            this.dg_invoice.Rows[count - 1].Cells[0].Value = sales.date;
            this.dg_invoice.Rows[count - 1].Cells[1].Value = sales.person.pid;
            this.dg_invoice.Rows[count - 1].Cells[2].Value = sales.person.pname;
            this.dg_invoice.Rows[count - 1].Cells[3].Value = sales.person.pkey;
            this.dg_invoice.Rows[count - 1].Cells[4].Value = sales.total_quantity;
            this.dg_invoice.Rows[count - 1].Cells[5].Value = sales.GetGrandTotal;
            this.dg_invoice.Rows[count - 1].Cells[6].Value = sales.RemainingAmount;
            this.dg_invoice.Rows[count - 1].Cells[7].Value = Math.Ceiling(sales.Total_Chongi+sales.Total_Commission);
            this.dg_invoice.Rows[count - 1].Cells[8].Value = sales.GetGrandTotal+sales.RemainingAmount;
        }

        public void readCustomerDailySale(string date, string search)
        {
            //customers = bal.getCustomerBills(date, true);
            List<DataMember.CustomerSales> custSales = null;
            if(rd_both.Checked || rd_live.Checked)
                custSales = bal.getCustomerBills(date);
            if (custSales == null) custSales = new List<DataMember.CustomerSales>();

            if ( custSales.Count > 0)
            {
                localRecord = false;
            }
            else if(rd_both.Checked || rd_local.Checked)
            {
                localRecord = true;
                if (saleParser == null)
                {
                    saleParser = new SaleParser(date, Admin.SaveLog);
                }
                tLandlords = saleParser.LoadTodaySale();

                if (tLandlords != null)
                {
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

                        if (exists && search != "")
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
                addControls(null, custSales);

            }


        }

        private void Invoicing_Load(object sender, EventArgs e)
        {
            this.bal = new BLogic();
            date = today_date.Text;
            billtype_combo.SelectedIndex = 0;
            saleParser = new SaleParser(date, Admin.SaveLog);
            adminlog = LogUtill.getAdminInputLog();
            getUpdateSale();
            
            //readClientDailySale(date);

        }

        private void billtype_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dg_invoice.Focus();
            dg_invoice.ClearSelection();

            dg_invoice.Rows[currentrow].Selected = true;
            dg_invoice.Rows[currentrow].Cells[0].Selected = true;
            if (billtype_combo.SelectedIndex == 0 || billtype_combo.SelectedIndex == 2)
                dg_invoice.Columns[7].HeaderText = "Expense";
            else
                dg_invoice.Columns[7].HeaderText = "Commission+Chongi";

            callCustomerClientSales();



        }

        private void callCustomerClientSales()
        {
            saleParser = new SaleParser(date, Admin.SaveLog);
            string sdate = today_date.Text;
            string ldate = today_date.Text;
            string search = txt_search.Text;
            if (chk_date.Checked)
            {
                sdate = date_start.Text;
                ldate = date_last.Text;
            }
            if (billtype_combo.Text == "Client")
            {
                readClientDailySale(sdate, search);
            }
            else if (billtype_combo.Text == "Customer")
            {
                readCustomerDailySale(sdate, search);
            }
            else if (billtype_combo.Text == "Landlord")
            {
                //readLandlordClientSale(null);
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

        public void readLandlordClientSale(DataTable dt)
        {
            string id = txt_search.Text;
            string sdate = today_date.Text;
            string ldate = today_date.Text;
            if(chk_date.Checked)
            {
                sdate = date_start.Text;
                ldate = date_last.Text;
            }

            if(dt==null)
                dt = bal.readLandlordDailySale(sdate,ldate, id);
            AllReportsCC rp = new AllReportsCC();
            rp.ClientSaleDetail(dt);
            rp.ShowDialog();
        }

        private void today_date_ValueChanged(object sender, EventArgs e)
        {
            date = today_date.Text;
            dg_invoice.Rows.Clear();
            dg_invoice.Refresh();
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

        private void multiplePages(XtraReport report1, List<XtraReport> reports)
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
        private DataTable bipariDt;
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (billtype_combo.SelectedIndex == 2)
                readLandlordClientSale(bipariDt);

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
                    CustomDailog frm = new CustomDailog(tLandlords, billtype_combo.Text, "ALL Pages", "Local Invoice");
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
                addControls(tLandlords, null);
            }
            else
            if (billtype_combo.Text == "Customer")
            {
                readCustomerDailySale(date, "");
            }
        }



        private void txt_search_TextChanged(object sender, EventArgs e)
        {

            if (billtype_combo.Text == "Client")
            {

                readClientDailySale(date, txt_search.Text);
            }
            else
            if (billtype_combo.Text == "Customer")
            {
                readCustomerDailySale(date, txt_search.Text);
            }

        }

        #region Control Keys,Events
        int currentrow = 0, gridRow=0;
        private void selectUpRow(DataGridView grid)
        {
            DataGridView dgv = grid;

            int totalRows = dgv.Rows.Count;
            if (totalRows > 0)
            {
               
                int rowIndex = currentrow;
                if (rowIndex == 0)
                    return;
                //int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[0].Selected = true;
                grid.FirstDisplayedScrollingRowIndex = rowIndex - 1;
                currentrow--;
                if (currentrow < 0)
                {
                    currentrow = 0;
                }
                if (grid.Name == "dg_invoice")
                {
                    gridRow--;
                    if (gridRow < 0)
                    {
                        gridRow = 0;
                    }
                }

            }

        }
        private void selectDownRow(DataGridView grid)
        {
            DataGridView dgv = grid;
            int totalRows = dgv.Rows.Count;
            if (totalRows > 0)
            {
                //int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (currentrow == totalRows - 1)
                    return;
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[currentrow];
                dgv.ClearSelection();
                dgv.Rows[currentrow + 1].Cells[colIndex].Selected = true;
                grid.FirstDisplayedScrollingRowIndex = currentrow + 1;
                currentrow++;
                if (currentrow > totalRows)
                {
                    currentrow = totalRows - 1;
                }

                if (grid.Name == "dg_invoice")
                {
                    gridRow++;
                    if (gridRow > totalRows)
                    {
                        gridRow = totalRows - 1;
                    }
                }
            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Up:

                    selectUpRow(dg_invoice);
                    return true;
                case Keys.Down:
                    selectDownRow(dg_invoice);
                    return true;
                case Keys.F5:
                    loadRefresh();
                    return true;
                case Keys.Enter:

                    //dg_invoice_CellClick(this,new DataGridViewCellEventArgs(8,currentrow));

                    previewData(currentrow);
                    return true;
                case Keys.Control | Keys.P:
                    printReport(-1);
                    return true;



            }



            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void printReport(int row)
        {
            if (row == dg_invoice.Rows.Count)
                return;
            if(row==-1)
            {
                bunifuFlatButton1_Click(this,new EventArgs());
            } else
            if (billtype_combo.SelectedIndex == 0)
            {
                //Landlord land = tLandlords[row];
                string key = dg_invoice.Rows[row].Cells[3].Value.ToString();
                printReport(findLandlord(key), null);

            }
            else if (billtype_combo.SelectedIndex == 1)
            {
                string key = dg_invoice.Rows[row].Cells[3].Value.ToString();
                //DataMember.CustomerSales csale = this.custSales[row];
                printReport(null, findCustomer(key));

            }
        }

        private Landlord findLandlord(string key)
        {
            Landlord land = tLandlords.Find(x => x.land_person.pkey == key);
            return land;
        }
        private DataMember.CustomerSales findCustomer(string key)
        {
            DataMember.CustomerSales csale = this.custSales.Find(x => x.person.pkey == key);
            return csale;
        }


        private void printReport(Landlord landlord, DataMember.CustomerSales custSale)
        {
            bool isCustomer = false;
            if (landlord != null)
            {

                /*using (RCBilling rc = new RCBilling(landlord, landlord.date))
                {
                    rc.ShowDialog();
                }*/
                isCustomer = false;


                ReportPages rp = new ReportPages(isCustomer, landlord);
                rp.ShowDialog();

            }
            else if (custSale != null)
            {
                /*using (RCBilling rc = new RCBilling(customer, customer.date))
                {
                    rc.ShowDialog();
                }*/
                isCustomer = true;
                ReportPages rp = new ReportPages(isCustomer, custSale, null, custSale.date);
                rp.ShowDialog();
            }
        }


        private void previewData(int row)
        {
            if (billtype_combo.SelectedIndex == 0)
            {
                //Landlord land = tLandlords[row];
                string key = dg_invoice.Rows[row].Cells[3].Value.ToString();
                previewLandlordData(findLandlord(key), null, null);

            }
            else if (billtype_combo.SelectedIndex == 1)
            {
                string key = dg_invoice.Rows[row].Cells[3].Value.ToString();
               // DataMember.CustomerSales csale = this.custSales[row];
                previewLandlordData(null,findCustomer(key) , null);

            }
        }
        

        private void previewLandlordData(Landlord landlord, DataMember.CustomerSales custSale,Customer customer)
        {
            BLogic bal = new BLogic();
            SaleDetail sd;
            if (landlord != null)
            {
                sd = new SaleDetail(landlord);
                sd.ShowDialog();
            }
            else if (custSale != null)
            {
                if (localRecord)
                {
                    sd = new SaleDetail(localRecord, custSale);
                    sd.date = this.date;
                }
                else
                {
                    sd = new SaleDetail(custSale.person.pkey, this.date, customer);
                    sd.date = custSale.date;

                }
                sd.ShowDialog();

            }
        }

        public void selectCurrentRow(int row)
        {
            int colIndex = dg_invoice.SelectedCells[0].OwningColumn.Index;
            DataGridViewRow selectedRow = dg_invoice.Rows[row];
            dg_invoice.ClearSelection();
            dg_invoice.Rows[row].Cells[colIndex].Selected = true;
            dg_invoice.FirstDisplayedScrollingRowIndex = row;
        }

        private void dg_invoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            //user cannot delete after 2 days
            /*double days = CommonUtill.no_of_Days( sale_date.Value.Year, sale_date.Value.Month, sale_date.Value.Day);
            if (e.ColumnIndex == 0 && days > Const._Bill_Delete_After_Days)
            {
                MessageBox.Show(ConstMessages._TRANSPORT_TRYTODELETE_OLDBILL);
                return;
            }*/
            if (e.ColumnIndex == 9)
            {
                previewData(currentrow);
            }
            if (e.ColumnIndex == 10)
            {
                currentrow = index;
                PrintInvoice(index);
            }
            currentrow = e.RowIndex;
            //selectCurrentRow(currentrow);
        }
        private void PrintInvoice(int row)
        {
            bool isCustomer = false;
            if (billtype_combo.SelectedIndex == 0)
            {
                isCustomer = false ;
                //Landlord land = tLandlords[row];
                string key = dg_invoice.Rows[row].Cells[3].Value.ToString();
                ReportPages rp = new ReportPages(isCustomer, findLandlord(key));
                rp.ShowDialog();

            }
            else if (billtype_combo.SelectedIndex == 1)
            {
                isCustomer = true;
                string key = dg_invoice.Rows[row].Cells[3].Value.ToString();
                DataMember.CustomerSales css = findCustomer(key);
                ReportPages rp = new ReportPages(isCustomer, css, null, css.date);
                rp.ShowDialog();
            }
        }

        private void btn_frieght_detail_Click(object sender, EventArgs e)
        {
            FrightDetail fd = new FrightDetail(this.date);
            fd.ShowDialog();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            dg_invoice.Rows.Clear();
            dg_invoice.Refresh();

            string sdate = today_date.Text;
            string ldate = today_date.Text;
            string search = txt_search.Text;
            if (chk_date.Checked)
            {
                sdate = date_start.Text;
                ldate = date_last.Text;
            }

            string action = "";
            if (billtype_combo.SelectedIndex == 0)
                action = "Zamidar";
            else if(billtype_combo.SelectedIndex==1)
                action = "Customer";
            else if (billtype_combo.SelectedIndex == 2)
                action = "Bipari";

            DataTable dt = bal.salesDisplay(action, sdate, ldate,txt_search.Text,"");
            if (billtype_combo.SelectedIndex == 2)
                bipariDt = dt;

            foreach (DataRow dr in dt.Rows)
            {
                int count = this.dg_invoice.Rows.Count;
                this.dg_invoice.Rows.Add();
                this.dg_invoice.Rows[count - 1].Cells[0].Value = dr[1].ToString();
                this.dg_invoice.Rows[count - 1].Cells[1].Value = dr[2].ToString();
                this.dg_invoice.Rows[count - 1].Cells[2].Value = dr[3].ToString();
                this.dg_invoice.Rows[count - 1].Cells[3].Value = dr[4].ToString();
                this.dg_invoice.Rows[count - 1].Cells[4].Value = dr[5].ToString();
                this.dg_invoice.Rows[count - 1].Cells[5].Value = dr[6].ToString();
                this.dg_invoice.Rows[count - 1].Cells[6].Value = dr[7].ToString();
                this.dg_invoice.Rows[count - 1].Cells[7].Value = dr[8].ToString();
                this.dg_invoice.Rows[count - 1].Cells[8].Value = dr[9].ToString();
            }


        }

        private void chl_date_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = chk_date.Checked;
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            loadRefresh();
        }

        private void loadRefresh()
        {
            btn_submit_Click(this,new EventArgs());
        }
        #endregion

    }
}
