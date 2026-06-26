using ArthiPOS.Properties;
using ArthiPOS.Reporting.ReportView;
using ArthiPOS.Reporting.ReportView.Bills;
using ArthiPOS.Reporting.ReportView.Header;
using ArthiPOS.Reporting.ReportView.NoHeader;
using ArthiPOS.Reporting.ReportView.report;
using BAL;
using CommonUtilities;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DataMember;
using DataMember.memberlog;
using MetroFramework.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static ArthiPOS.Controls.dashboard.ReportControl;
using Resources = ArthiPOS.Properties.Resources;

namespace ArthiPOS.Reporting
{
    public partial class AllReportsCC : Form
    {
        private List<Landlord> landlords;
        private List<Customer> customers;
        private string sdate;
        private string ldate;
        private string date;
        int check = 0;
        BLogic bal;
        private string bill_key;
        private DataTable dt = null;


        public AllReportsCC()
        {
            InitializeComponent();
        }
        public void ClientSaleDetail(DataTable dt)
        {
            ClientSaleDetail rb = new ClientSaleDetail();
            this.dt = dt;

            rb.Database.Tables["ClientDetailsRep"].SetDataSource(dt);

            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
            currentDoc = rb;

        }
        public void ExpenseReceiving(DataTable dte, string sdate, string ldate)
        {
            ReportExpRec rb = new ReportExpRec();
            rb.Database.Tables["CashExpense"].SetDataSource(dte);
            //DataTable wm = new DataTable();
            //wm.Columns.Add("waterpath", typeof(string));
            //string startupPath = Environment.CurrentDirectory;
            //wm.Rows.Add(@startupPath + "\\watermark.jpg");
            // rb.Database.Tables["Watermark"].SetDataSource(wm);
            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
            rb.SetParameterValue("title", Properties.Resources.ResourceManager.GetString("a2018", ci) ?? "");
            currentDoc = rb;

        }
        public void ExpenseRecSection(DataTable rec, DataTable exp, int balance, int receivings, int expenses, int currentBalance)
        {
            ReportExpRecNew rb = new ReportExpRecNew();
            //rb.Database.Tables["CashExpense"].SetDataSource(dt);
            string name = rb.Subreports[0].Name;
            string name1 = rb.Subreports[1].Name;
            rb.Subreports[0].Database.Tables["Expense"].SetDataSource(exp);
            rb.Subreports[1].Database.Tables["Receivings"].SetDataSource(rec);
            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
            rb.SetParameterValue("title", Resources.ResourceManager.GetString("a2018", ci) ?? "");
            rb.SetParameterValue("prBalance", balance);
            rb.SetParameterValue("receivings", receivings);
            rb.SetParameterValue("expense", expenses);
            rb.SetParameterValue("currentBalance", currentBalance);
            currentDoc = rb;

        }

        public void printLedger(int index, DataTable dt)
        {
            ReportView.ReportLedgerJ rb = new ReportView.ReportLedgerJ();
            if (dt == null)
                return;
            //rb.Database.Tables["CashExpense"].SetDataSource(dt);
            string name = "";
            int ind = 0;
            if (index == 0)
            {
                name = rb.Subreports[2].Name;
                ind = 1;
                rb.Subreports[2].Database.Tables["LAB"].SetDataSource(dt);

            }
            else if (index == 1)
            {
                ind = 2;

                name = rb.Subreports[3].Name;
                rb.Subreports[3].Database.Tables["Ledger"].SetDataSource(dt);

            }
            else if (index == 2)
            {
                ind = 1;
                name = rb.Subreports[1].Name;
                rb.Subreports[1].Database.Tables["CGJ"].SetDataSource(dt);

            }
            else if (index == 3)
            {
                ind = 1;
                name = rb.Subreports[0].Name;
                rb.Subreports[0].Database.Tables["Assets"].SetDataSource(dt);

            }
            else if (index == 4)
            {
                ind = 1;
                name = rb.Subreports[4].Name;
                rb.Subreports[4].Database.Tables["Trial"].SetDataSource(dt);

            }
            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
            rb.SetParameterValue("title", name);
            currentDoc = rb;

        }


        internal void ReportingData(DataTable dt, string header, string c1, string c2, string c3, string c4, string c5, string c6, string c7, string c8, string c9, string c10, string c11, string c12, string c13)
        {
            ReportView.ReportingData rb = new ReportView.ReportingData();
            rb.Database.Tables["SeasonDetail"].SetDataSource(dt);
            //DataTable wm = new DataTable();
            //wm.Columns.Add("waterpath", typeof(string));
            //string startupPath = Environment.CurrentDirectory;
            //wm.Rows.Add(@startupPath + "\\watermark.jpg");
            // rb.Database.Tables["Watermark"].SetDataSource(wm);

            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
            rb.SetParameterValue("title", header ?? "");
            rb.SetParameterValue("col1", c13 ?? "");
            rb.SetParameterValue("col2", c12 ?? "");
            rb.SetParameterValue("col3", c11 ?? "");
            rb.SetParameterValue("col4", c10 ?? "");
            rb.SetParameterValue("col5", c9 ?? "");
            rb.SetParameterValue("col6", c8 ?? "");
            rb.SetParameterValue("col7", c7 ?? "");
            rb.SetParameterValue("col8", c6 ?? "");
            rb.SetParameterValue("col9", c5 ?? "");
            rb.SetParameterValue("col10", c4 ?? "");
            rb.SetParameterValue("col11", c3 ?? "");
            rb.SetParameterValue("col12", c2 ?? "");
            rb.SetParameterValue("col13", c1 ?? "");

            currentDoc = rb;


        }

        public void Reportingdata(DataTable dt)
        {
            ReportRecEx rb = new ReportRecEx();
            rb.Database.Tables["RecEx"].SetDataSource(dt);
            //DataTable wm = new DataTable();
            //wm.Columns.Add("waterpath", typeof(string));
            //string startupPath = Environment.CurrentDirectory;
            //wm.Rows.Add(@startupPath + "\\watermark.jpg");
            // rb.Database.Tables["Watermark"].SetDataSource(wm);

            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
            currentDoc = rb;
        }

        public void AugraiDetailinfo(DataTable dts)
        {
            this.dt = dts;
            AugraiDetailInfo rb = new AugraiDetailInfo();
            rb.Database.Tables["AugDetail"].SetDataSource(this.dt);
            //DataTable wm = new DataTable();
            //wm.Columns.Add("waterpath", typeof(string));
            //string startupPath = Environment.CurrentDirectory;
            //wm.Rows.Add(@startupPath + "\\watermark.jpg");
            //rb.Database.Tables["Watermark"].SetDataSource(wm);
            currentDoc = rb;

            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
        }
        public void dailyAccounts(DataTable dt)
        {
            ReportDailyAccounts rb = new ReportDailyAccounts();
            DataRow dr = dt.Rows[0];
            int balance = int.Parse(dr[7].ToString() == "" ? "0" : dr[7].ToString());
            rb.Database.Tables["Accounts"].SetDataSource(dt);
            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
            rb.SetParameterValue("lastbalance", balance);
            currentDoc = rb;


        }
        public void BillandRecevings(DataTable cldt, DataTable dt, DataTable dtprd, string cid, string cname, string sdate, string ldate, string balance,int check)
        {
            //CustomReport rb = new CustomReport();
            if (dt != null || true)
            {
                this.dt = dt;
                ReportClass rb = null;
                if (check == 2 || check==1)
                {
                    rb = new ReportFardhisabBipZam();
                    rb.Database.Tables["BipZim"].SetDataSource(this.dt);

                }
                else
                {
                    rb = new ReportFardhisabClient();
                    rb.Database.Tables["CustBillRec"].SetDataSource(this.dt);
                }

                //rb.Subreports["ReportProductDetails"].SetDataSource(dtprd);
                rb.Subreports[0].Database.Tables["ProductTotal"].SetDataSource(dtprd);
                DataTable wm = new DataTable();
                wm.Columns.Add("waterpath", typeof(string));
                string startupPath = Environment.CurrentDirectory;
                wm.Rows.Add(@startupPath + "\\watermark.jpg");
                rb.Database.Tables["Watermark"].SetDataSource(wm);
                crystalReportViewer1.ReportSource = rb;
                crystalReportViewer1.Refresh();
                //rb.SetParameterValue("title", Resources.ResourceManager.GetString("a2018", ci) ?? "");
                rb.SetParameterValue("cid", cid);
                rb.SetParameterValue("cname", cname);
                rb.SetParameterValue("sdate", (sdate == null) ? "" : sdate);
                rb.SetParameterValue("ldate", (ldate == null) ? "" : ldate);
                rb.SetParameterValue("balance", balance);
                currentDoc = rb;

            }
            else
            {
                this.dt = cldt;
                ReportClientBillRec rb = new ReportClientBillRec();
                rb.Database.Tables["CustBillRec"].SetDataSource(this.dt);
                DataTable wm = new DataTable();
                wm.Columns.Add("waterpath", typeof(string));
                string startupPath = Environment.CurrentDirectory;
                wm.Rows.Add(@startupPath + "\\watermark.jpg");
                rb.Database.Tables["Watermark"].SetDataSource(wm);

                crystalReportViewer1.ReportSource = rb;
                crystalReportViewer1.Refresh();
                //rb.SetParameterValue("title", Resources.ResourceManager.GetString("a2018", ci) ?? "");
                rb.SetParameterValue("cid", cid);
                rb.SetParameterValue("cname", cname);
                rb.SetParameterValue("sdate", (sdate == null) ? "" : sdate);
                rb.SetParameterValue("ldate", (ldate == null) ? "" : ldate);
                rb.SetParameterValue("balance", balance);
                currentDoc = rb;

            }

        }

        public AllReportsCC(DataTable dt, ReportMenu rmenu, string sdate, string ldate)
        {
            InitializeComponent();
            bal = new BLogic();
            this.sdate = sdate;
            this.ldate = ldate;
            this.dt = dt;
            if (dt == null && ReportMenu.CustBillsandReceivings != rmenu)
                return;
            string col1 = ""
                , col2 = ""
                , col3 = ""
                , col4 = ""
                , col5 = ""
                , col6 = ""
                , col7 = ""
                , col8 = "";
            if (ReportMenu.CustBillsandReceivings != rmenu)
            {
                if (dt.Columns.Count > 1) col1 = this.dt.Columns[0].ColumnName;
                if (dt.Columns.Count > 2) col2 = this.dt.Columns[1].ColumnName;
                if (dt.Columns.Count > 3) col3 = this.dt.Columns[2].ColumnName;
                if (dt.Columns.Count > 4) col4 = this.dt.Columns[3].ColumnName;
                if (dt.Columns.Count > 5) col5 = this.dt.Columns[4].ColumnName;
                if (dt.Columns.Count > 6) col6 = this.dt.Columns[5].ColumnName;
                if (dt.Columns.Count > 7) col7 = this.dt.Columns[6].ColumnName;
                if (dt.Columns.Count > 8) col8 = this.dt.Columns[7].ColumnName;
            }
            CustomReport rb = new CustomReport();
            string op_ac = "0", end_acc = "0", aug = "0", exp = "0", rec = "0", tot_sale = "0";
            DataTable wm = new DataTable();
            wm.Columns.Add("waterpath", typeof(string));
            string startupPath = Environment.CurrentDirectory;
            wm.Rows.Add(@startupPath + "\\watermark.jpg");
            rb.Database.Tables["Watermark"].SetDataSource(wm);

            switch (rmenu)
            {
                case ReportMenu.CustBillsandReceivings:
                    {

                        break;
                    }
                case ReportMenu.BalanceSheetReport:
                    {

                        changeDatasetColumnName(0, 1, 2, 3, 4, 5, 6, 8);
                        rb.Database.Tables["CustomData"].SetDataSource(dt);
                        updatemenu2(rb);
                        crystalReportViewer1.ReportSource = rb;
                        crystalReportViewer1.Refresh();
                        rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1055", ci) ?? "");

                        break;
                    }
                case ReportMenu.AugraiReport:
                    {
                        changeDatasetColumnName(0, 1, 2, 3, 4, 5, 6, 7);
                        rb.Database.Tables["CustomData"].SetDataSource(dt);
                        updatemenu3(rb);
                        rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1056", ci) ?? "");
                        break;
                    }

                case ReportMenu.ProfitLoss://Menu 5
                    {
                        changeDatasetColumnName(0, 1, 2, 3, 4, 5, 6, 7);
                        rb.Database.Tables["CustomData"].SetDataSource(dt);
                        updatemenu5(rb);
                        rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1058", ci) ?? "");
                        break;
                    }
                case ReportMenu.ExpenseDetail://Menu 6
                    {
                        changeDatasetColumnName(0, 1, 2, 3, 4, 5, 6, 7);
                        rb.Database.Tables["CustomData"].SetDataSource(dt);
                        updatemenu6(rb);
                        rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1059", ci) ?? "");

                        break;
                    }
                case ReportMenu.CashReceived://Menu 7
                    {
                        changeDatasetColumnName(0, 1, 2, 3, 4, 5, 6, 7);
                        rb.Database.Tables["CustomData"].SetDataSource(dt);
                        updatemenu7(rb);
                        rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1060", ci) ?? "");
                        break;
                    }
                case ReportMenu.ExpenseCashReceive:// Menu 8
                    {
                        //CashRecExpense cre = new CashRecExpense();
                        //rb.Database.Tables["CashExpense"].SetDataSource(dt);
                        //crystalReportViewer1.ReportSource = cre;
                        //crystalReportViewer1.Refresh();
                        changeDatasetColumnName(0, 1, 2, 3, 4, 5, 6, 7);
                        rb.Database.Tables["CustomData"].SetDataSource(dt);
                        updatemenu8(rb);
                        rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1200", ci) ?? "");
                        DataTable dts = bal.p_today_totalDetails(sdate, ldate);
                        if (dts.Rows.Count > 0)
                        {

                            DataRow cr = dts.Rows[0];

                            aug = cr[0].ToString();
                            rec = cr[1].ToString();
                            exp = cr[2].ToString();
                            op_ac = cr[3].ToString();
                            end_acc = cr[4].ToString();
                            tot_sale = cr[5].ToString();
                        }

                        break;
                    }

                case ReportMenu.BipariSales://Menu 9
                    {
                        changeDatasetColumnName(0, 1, 2, 3, 4, 5, 6, 7);
                        rb.Database.Tables["CustomData"].SetDataSource(dt);
                        updatemenu9(rb);
                        rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1062", ci) ?? "");

                        break;
                    }
                case ReportMenu.CustomerSale://Menu 10
                    {
                        changeDatasetColumnName(0, 1, 2, 3, 4, 5, 6, 7);
                        rb.Database.Tables["CustomData"].SetDataSource(dt);
                        updatemenu10(rb);
                        rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1063", ci) ?? "");

                        break;
                    }
                case ReportMenu.BipariInvestment://Menu 11
                    {
                        changeDatasetColumnName(0, 1, 2, 3, 4, 5, 6, 7);
                        rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1064", ci) ?? "");
                        rb.Database.Tables["CustomData"].SetDataSource(dt);
                        updatemenu11(rb);
                        rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1047", ci) ?? "");
                        break;
                    }

                case ReportMenu.SERP://Menu 12
                    {

                        break;
                    }
                case ReportMenu.AugraiDiff:
                    {
                        changeDatasetColumnName(0, 1, 2, 3, 4, 5, 6, 7);
                        rb.Database.Tables["CustomData"].SetDataSource(dt);
                        updatemenu13(rb);
                        rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1056", ci) ?? "");
                        break;
                    }
            }
            //rb.SetParameterValue("title", rmenu.ToString() /*Resources.ResourceManager.GetString("a1200")*/ ?? "");


            rb.SetParameterValue("open_acc", op_ac ?? "");
            rb.SetParameterValue("end_acc", end_acc ?? "");
            rb.SetParameterValue("total_augrai", aug ?? "");
            rb.SetParameterValue("total_expense", exp ?? "");
            rb.SetParameterValue("cash_rec", rec ?? "");
            rb.SetParameterValue("total_sale", tot_sale ?? "");
            currentDoc = rb;

            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();

        }
        private DataTable cusSum = null, clSumm = null, todayDetail = null, exr = null, detReport = null;
        public void printTodayReport(string sdate, string ldate)
        {
            cusSum = new BLogic().p_report_CustomerClient("sCustomerSum", sdate, ldate);
            clSumm = new BLogic().p_report_CustomerClient("sClientSum", sdate, ldate);
            todayDetail = new BLogic().p_today_totalDetails(sdate, ldate);
            List<Object> obj = (List<object>)new BLReport().p_expenseCashReceive(sdate, ldate,
                1, 18);
            List<object> detailRep = (List<object>)new BLReport().p_DetailReport(sdate, ldate, "");


            TodayReport rb = new TodayReport();

            #region WaterMark
            DataTable wm = new DataTable();
            wm.Columns.Add("waterpath", typeof(string));
            string startupPath = Environment.CurrentDirectory;
            wm.Rows.Add(@startupPath + "\\watermark.jpg");

            #endregion

            #region ReportDetail
            if (detailRep == null)
            {
                return;
            }
            detReport = (DataTable)detailRep[1];

            DataRow accop = (detReport.Rows.Count > 0) ? detReport.Rows[0] : null;
            if (accop != null)
            {
                int acc_open = int.Parse(accop[3].ToString());
                string datec = sdate + " TO " + ldate;
                rb.Subreports[1].Database.Tables["DetailReport"].SetDataSource(detReport);
                rb.SetParameterValue("acc_open", acc_open);
                rb.SetParameterValue("sdate", datec);

            }
            #endregion
            #region CustomRep ExpenseandReceivings
            exr = (DataTable)obj[1];

            if (exr != null)
            {


                string op_ac = "0", end_acc = "0", aug = "0", exp = "0", rec = "0", tot_sale = "0";
                if (todayDetail.Rows.Count > 0)
                {

                    DataRow cr = todayDetail.Rows[0];

                    aug = cr[0].ToString();
                    rec = cr[1].ToString();
                    exp = cr[2].ToString();
                    op_ac = cr[3].ToString();
                    end_acc = cr[4].ToString();
                    tot_sale = cr[5].ToString();
                }
                if (this.exr.Columns.Count >= 1)
                    this.exr.Columns[0].ColumnName = "Col1";
                if (this.exr.Columns.Count >= 2)
                    this.exr.Columns[1].ColumnName = "Col2";
                if (this.exr.Columns.Count >= 3)
                    this.exr.Columns[2].ColumnName = "Col3";
                if (this.exr.Columns.Count >= 4)
                    this.exr.Columns[3].ColumnName = "Col4";
                if (this.exr.Columns.Count >= 5)
                    this.exr.Columns[4].ColumnName = "Col5";
                if (this.exr.Columns.Count >= 6)
                    this.exr.Columns[5].ColumnName = "Col6";
                if (this.exr.Columns.Count >= 7)
                    this.exr.Columns[6].ColumnName = "Col7";
                if (this.exr.Columns.Count >= 8)
                    this.exr.Columns[7].ColumnName = "Col8";
                rb.Subreports[0].Database.Tables["CustomData"].SetDataSource(exr);
                updatemenu8(rb);
                rb.SetParameterValue("col1", Resources.ResourceManager.GetString("a0009", ci) ?? "");
                rb.SetParameterValue("col2", Resources.ResourceManager.GetString("a0009", ci) ?? "");
                rb.SetParameterValue("col3", Resources.ResourceManager.GetString("a0205", ci) ?? "");
                rb.SetParameterValue("col4", Resources.ResourceManager.GetString("a1060", ci) ?? "");
                rb.SetParameterValue("col5", Resources.ResourceManager.GetString("a0006", ci) ?? "");
                rb.SetParameterValue("col6", Resources.ResourceManager.GetString("a0009", ci) ?? "");
                rb.SetParameterValue("col7", Resources.ResourceManager.GetString("a2009", ci) ?? "");
                rb.SetParameterValue("col8", Resources.ResourceManager.GetString("a2010", ci) ?? "");

                rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1200", ci) ?? "");
                currentDoc = rb;

            }

            #endregion




            rb.Database.Tables["Watermark"].SetDataSource(wm);
            rb.Database.Tables["SalesTotal"].SetDataSource(clSumm);
            rb.Database.Tables["SalesTotal_1"].SetDataSource(cusSum);

            rb.SetParameterValue("startdate", sdate);
            rb.SetParameterValue("lastdate", ldate);

            currentDoc = rb;
            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
        }
        internal void printAllCustDetailRep(bool isCustomer, string sdate,string ldate)
        {
            Account acc = Authentication.Account;

            if (isCustomer)
            {
                //SalesTodayCustAllDetail rb = new SalesTodayCustAllDetail();
                SalesCustAllDetail rb = new SalesCustAllDetail();
                dt = new BLogic().p_report_CustomerClient("sCustomer", sdate, ldate);
                //rb.Database.Tables["p_report_cc"].SetDataSource(dt);
                rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                DataTable dt1 = new BLogic().p_report_CustomerClient("sCustomerSum", sdate, ldate);
                rb.Database.Tables["SalesTotal"].SetDataSource(dt1);
                DataTable wm = new DataTable();
                wm.Columns.Add("waterpath", typeof(string));
                string startupPath = Environment.CurrentDirectory;
                wm.Rows.Add(@startupPath + "\\watermark.jpg");
                rb.Database.Tables["Watermark"].SetDataSource(wm);

                rb.SetParameterValue("Title", acc.shop_name);
                rb.SetParameterValue("Propriter", acc.propriters_name);
                rb.SetParameterValue("Name1", acc.name1 ?? "");
                rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                rb.SetParameterValue("Name2", acc.name2 ?? "");
                rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                rb.SetParameterValue("Address", acc.address ?? "");
                rb.SetParameterValue("Business", acc.business_type ?? "");
                crystalReportViewer1.ReportSource = rb;
                currentDoc = rb;


            }
            else
            {
                SalesTodayAllDetail rb = new SalesTodayAllDetail();
                dt = new BLogic().p_report_CustomerClient("sClient", sdate, ldate);
                DataTable dt1 = new BLogic().p_report_CustomerClient("sClientSum", sdate, ldate);

                rb.Database.Tables["Sales"].SetDataSource(dt);
                rb.Database.Tables["SalesTotal"].SetDataSource(dt1);
                DataTable wm = new DataTable();
                wm.Columns.Add("waterpath", typeof(string));
                string startupPath = Environment.CurrentDirectory;
                wm.Rows.Add(@startupPath + "\\watermark.jpg");
                rb.Database.Tables["Watermark"].SetDataSource(wm);

                rb.SetParameterValue("Title", acc.shop_name);
                rb.SetParameterValue("Propriter", acc.propriters_name);
                rb.SetParameterValue("Name1", acc.name1 ?? "");
                rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                rb.SetParameterValue("Name2", acc.name2 ?? "");
                rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                rb.SetParameterValue("Address", acc.address ?? "");
                rb.SetParameterValue("Business", acc.business_type ?? "");
                currentDoc = rb;

                crystalReportViewer1.ReportSource = rb;
            }
        }



        public void printSRPSUM(string sdate, string ldate, int index, int pageSize)
        {


            CustomReport rb = new CustomReport();
            List<Object> obj = (List<object>)new BLReport().p_dailyProfitSalesExpense("SERPSUM", sdate, ldate,
                    index, pageSize);
            dt = null;
            dt = (DataTable)obj[1];
            if (dt == null)
                return;
            changeDatasetColumnName(0, 1, 2, 3, 4, 5, 6, 7);
            rb.Database.Tables["CustomData"].SetDataSource(dt);
            DataTable wm = new DataTable();
            wm.Columns.Add("waterpath", typeof(string));
            string startupPath = Environment.CurrentDirectory;
            wm.Rows.Add(@startupPath + "\\watermark.jpg");
            rb.Database.Tables["Watermark"].SetDataSource(wm);

            updatemenu12(rb);
            string tem = "0";
            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();

            rb.SetParameterValue("title", Resources.ResourceManager.GetString("a1088") ?? "");
            rb.SetParameterValue("open_acc", tem ?? "");
            rb.SetParameterValue("end_acc", tem ?? "");
            rb.SetParameterValue("total_augrai", tem ?? "");
            rb.SetParameterValue("total_expense", tem ?? "");
            rb.SetParameterValue("cash_rec", tem ?? "");
            rb.SetParameterValue("total_sale", tem ?? "");
            currentDoc = rb;

        }
        public void printBillList(DataTable dt, string id, string name, string date)
        {
            ReportBillListLand rb = new ReportBillListLand();
            rb.Database.Tables["BillList"].SetDataSource(dt);
            DataTable wm = new DataTable();
            wm.Columns.Add("bid", typeof(string));
            wm.Columns.Add("bname", typeof(string));
            wm.Columns.Add("sldate", typeof(string));
            DataRow dr = wm.NewRow();
            dr["bid"] = id;
            dr["bname"] = name;
            dr["sldate"] = date;
            wm.Rows.Add(dr);
            rb.Database.Tables["BillName"].SetDataSource(wm);
            currentDoc = rb;

            crystalReportViewer1.ReportSource = rb;
        }
        public void printA4FullPage(bool isCustomer, DataTable dt)
        {
            if (isCustomer)
            {
                ReportCustA7T_FullPage rb = new ReportCustA7T_FullPage();
                //rb.Database.Tables["p_report_cc"].SetDataSource(dt);
                rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                rb.Subreports["SaleDetail"].SetDataSource(dt);

                rb.SetParameterValue("Name1", "");
                rb.SetParameterValue("Phone1", "");
                currentDoc = rb;

                crystalReportViewer1.ReportSource = rb;
            }
            else
            {
                ReportA7 rb = new ReportA7();

                if (dt == null)
                    return;
                rb.Database.Tables["Sales"].SetDataSource(dt);
                rb.Subreports["SaleDetail"].SetDataSource(dt);
                rb.Subreports["SaleExpense"].SetDataSource(dt);
                currentDoc = rb;

                crystalReportViewer1.ReportSource = rb;
            }
        }
        public void printA7Report(bool isCustomer, DataTable dt)
        {
            if (isCustomer)
            {
                ReportCustA7T rb = new ReportCustA7T();
                //rb.Database.Tables["p_report_cc"].SetDataSource(dt);
                rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                rb.Subreports["SaleDetail"].SetDataSource(dt);

                rb.SetParameterValue("Name1", "");
                rb.SetParameterValue("Phone1", "");
                currentDoc = rb;

                crystalReportViewer1.ReportSource = rb;
            }
            else
            {
                ReportA7 rb = new ReportA7();

                if (dt == null)
                    return;
                rb.Database.Tables["Sales"].SetDataSource(dt);
                rb.Subreports["SaleDetail"].SetDataSource(dt);
                rb.Subreports["SaleExpense"].SetDataSource(dt);
                currentDoc = rb;

                crystalReportViewer1.ReportSource = rb;
            }
        }
        internal void printA4hReport(bool isCustomer, DataTable dt)
        {
            if (isCustomer)
            {


            }
            else
            {
                ReportA5 rb = new ReportA5();
                DataRow row = dt.Rows[0];
                if (row[31].ToString() == "B")
                {
                    ReportBipA5 rb1 = new ReportBipA5();

                    if (dt == null)
                        return;
                    rb1.Database.Tables["Sales"].SetDataSource(dt);
                    rb1.Subreports["SaleDetail"].SetDataSource(dt);
                    rb1.Subreports["SaleExpense"].SetDataSource(dt);
                    crystalReportViewer1.ReportSource = rb1;

                }
                else
                {
                    if (dt == null)
                        return;
                    rb.Database.Tables["Sales"].SetDataSource(dt);
                    rb.Subreports["SaleDetail"].SetDataSource(dt);
                    rb.Subreports["SaleExpense"].SetDataSource(dt);
                    currentDoc = rb;

                    crystalReportViewer1.ReportSource = rb;
                }
                
            }
        }

        private DataTable changeDatasetColumnName(int c1, int c2, int c3, int c4, int c5, int c6, int c7, int c8)
        {

            if (this.dt.Columns.Count >= 1)
                this.dt.Columns[c1].ColumnName = "Col1";
            if (this.dt.Columns.Count >= 2)
                this.dt.Columns[c2].ColumnName = "Col2";
            if (this.dt.Columns.Count >= 3)
                this.dt.Columns[c3].ColumnName = "Col3";
            if (this.dt.Columns.Count >= 4)
                this.dt.Columns[c4].ColumnName = "Col4";
            if (this.dt.Columns.Count >= 5)
                this.dt.Columns[c5].ColumnName = "Col5";
            if (this.dt.Columns.Count >= 6)
                this.dt.Columns[c6].ColumnName = "Col6";
            if (this.dt.Columns.Count >= 7)
                this.dt.Columns[c7].ColumnName = "Col7";
            if (this.dt.Columns.Count >= 8)
                this.dt.Columns[7].ColumnName = "Col8";


            return this.dt;

        }

        private CustomReport changeColumnHeader(CustomReport rb, string col1, string col2, string col3
            , string col4, string col5, string col6, string col7,
            string col8, string col9, string col10, string col11)
        {

            rb.SetParameterValue("col1", col1 ?? "");
            rb.SetParameterValue("col2", col2 ?? "");
            rb.SetParameterValue("col3", col3 ?? "");
            rb.SetParameterValue("col4", col4 ?? "");
            rb.SetParameterValue("col5", col5 ?? "");
            rb.SetParameterValue("col6", col6 ?? "");
            rb.SetParameterValue("col7", col7 ?? "");
            rb.SetParameterValue("col8", col8 ?? "");
            return rb;
        }
        private ReportCustBillRec changeColumnHeader(ReportCustBillRec rb, string col1, string col2, string col3
            , string col4, string col5, string col6, string col7,
            string col8, string col9, string col10, string col11)
        {

            rb.SetParameterValue("col1", col1 ?? "");
            rb.SetParameterValue("col2", col2 ?? "");
            rb.SetParameterValue("col3", col3 ?? "");
            rb.SetParameterValue("col4", col4 ?? "");
            rb.SetParameterValue("col5", col5 ?? "");
            rb.SetParameterValue("col6", col6 ?? "");
            rb.SetParameterValue("col7", col7 ?? "");
            rb.SetParameterValue("col8", col8 ?? "");
            return rb;
        }
        #region Localization
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ur-PK");


        public void updatemenu1(CustomReport rb)
        {
        }
        public void updatemenu2(ReportCustBillRec rb)
        {

            changeColumnHeader(rb,
                              Resources.ResourceManager.GetString("a0013", ci),
                               Resources.ResourceManager.GetString("a0205", ci),
                               Resources.ResourceManager.GetString("a2001", ci),
                               Resources.ResourceManager.GetString("a2002", ci),
                               Resources.ResourceManager.GetString("a2003", ci),
                               Resources.ResourceManager.GetString("a1076", ci),
                               Resources.ResourceManager.GetString("a0009", ci),
                               Resources.ResourceManager.GetString("a2005", ci),
                               Resources.ResourceManager.GetString("a2004", ci),
                               Resources.ResourceManager.GetString("a2006", ci),
                               Resources.ResourceManager.GetString("a1079", ci)
                           );
        }
        public void updatemenu2(CustomReport rb)
        {

            changeColumnHeader(rb,
                              Resources.ResourceManager.GetString("a0013", ci),
                               Resources.ResourceManager.GetString("a0205", ci),
                               Resources.ResourceManager.GetString("a2001", ci),
                               Resources.ResourceManager.GetString("a2002", ci),
                               Resources.ResourceManager.GetString("a2003", ci),
                               Resources.ResourceManager.GetString("a1076", ci),
                               Resources.ResourceManager.GetString("a0009", ci),
                               Resources.ResourceManager.GetString("a2005", ci),
                               Resources.ResourceManager.GetString("a2004", ci),
                               Resources.ResourceManager.GetString("a2006", ci),
                               Resources.ResourceManager.GetString("a1079", ci)
                           );
        }
        public void updatemenu3(CustomReport rb)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ur-PK");

            changeColumnHeader(rb,
                               Resources.ResourceManager.GetString("a1079", ci),
                               Resources.ResourceManager.GetString("a0205", ci),
                               Resources.ResourceManager.GetString("a2021", ci),
                               Resources.ResourceManager.GetString("a2023", ci),
                               Resources.ResourceManager.GetString("a2003", ci),
                               Resources.ResourceManager.GetString("a2022", ci),
                               "",
                                "", "", "", ""
                           );
        }
        private void updatemenu13(CustomReport rb)
        {
            changeColumnHeader(rb,
                                Resources.ResourceManager.GetString("a0012", ci),
                                Resources.ResourceManager.GetString("a0205", ci),
                                Resources.ResourceManager.GetString("a1004", ci),
                                Resources.ResourceManager.GetString("a0038", ci),
                                Resources.ResourceManager.GetString("a2010", ci),
                                Resources.ResourceManager.GetString("a2012", ci),
                                Resources.ResourceManager.GetString("a2013", ci),
                                 "", "", "", ""
                            );
        }
        public void updatemenu4(CustomReport rb)
        {

        }
        public void updatemenu5(CustomReport rb)
        {
            changeColumnHeader(rb,
                              Resources.ResourceManager.GetString("a1079", ci),
                               Resources.ResourceManager.GetString("a0205", ci),
                               Resources.ResourceManager.GetString("a0032", ci)
                               , "", "", "", "", "", "", "", ""
                           );

        }
        public void updatemenu6(CustomReport rb)
        {
            changeColumnHeader(rb,
                              Resources.ResourceManager.GetString("a1079", ci),
                              Resources.ResourceManager.GetString("a1077", ci),
                              Resources.ResourceManager.GetString("a0032", ci),
                              Resources.ResourceManager.GetString("a0013", ci),
                              Resources.ResourceManager.GetString("a0009", ci),
                              "", "", "", "", "", ""
                          );

        }
        public void updatemenu7(CustomReport rb)
        {
            changeColumnHeader(rb,
                               Resources.ResourceManager.GetString("a1079", ci),
                              Resources.ResourceManager.GetString("a0205", ci),
                              Resources.ResourceManager.GetString("a0032", ci),
                              Resources.ResourceManager.GetString("a1078", ci),
                              Resources.ResourceManager.GetString("a0012", ci),
                              Resources.ResourceManager.GetString("a0013", ci),
                              Resources.ResourceManager.GetString("a0009", ci),
                               "", "", "", ""
                          );

        }
        public void updatemenu8(CustomReport rb)
        {
            changeColumnHeader(rb,
                              Resources.ResourceManager.GetString("a0009", ci),
                              Resources.ResourceManager.GetString("a0009", ci),
                              Resources.ResourceManager.GetString("a0205", ci),
                              Resources.ResourceManager.GetString("a1060", ci),
                              Resources.ResourceManager.GetString("a0006", ci),
                              Resources.ResourceManager.GetString("a0009", ci),
                              Resources.ResourceManager.GetString("a2010", ci),
                              Resources.ResourceManager.GetString("a2009", ci)
                              , "", "", ""
                          );

        }
        public void updatemenu8(TodayReport rb)
        {
            rb.SetParameterValue("col1", Resources.ResourceManager.GetString("a0009", ci) ?? "");
            rb.SetParameterValue("col2", Resources.ResourceManager.GetString("a0009", ci) ?? "");
            rb.SetParameterValue("col3", Resources.ResourceManager.GetString("a0205", ci) ?? "");
            rb.SetParameterValue("col4", Resources.ResourceManager.GetString("a1060", ci) ?? "");
            rb.SetParameterValue("col5", Resources.ResourceManager.GetString("a0006", ci) ?? "");
            rb.SetParameterValue("col6", Resources.ResourceManager.GetString("a0009", ci) ?? "");
            rb.SetParameterValue("col7", Resources.ResourceManager.GetString("a2009", ci) ?? "");
            rb.SetParameterValue("col8", Resources.ResourceManager.GetString("a2010", ci) ?? "");
        }
        public void updatemenu9(CustomReport rb)
        {

            changeColumnHeader(rb,
                              Resources.ResourceManager.GetString("a1079", ci),
                               Resources.ResourceManager.GetString("a0205", ci),
                               Resources.ResourceManager.GetString("a0401", ci),
                               Resources.ResourceManager.GetString("a1053", ci),
                               Resources.ResourceManager.GetString("a1026", ci),
                               Resources.ResourceManager.GetString("a0306", ci),
                               Resources.ResourceManager.GetString("a0012", ci),
                               Resources.ResourceManager.GetString("a1080", ci),
                               Resources.ResourceManager.GetString("a0009", ci),
                               Resources.ResourceManager.GetString("a0013", ci),
                               Resources.ResourceManager.GetString("a1079", ci)
                           );

        }
        public void updatemenu10(CustomReport rb)
        {

            changeColumnHeader(rb,
                               Resources.ResourceManager.GetString("a0009", ci),
                               Resources.ResourceManager.GetString("a0205", ci),
                               Resources.ResourceManager.GetString("a1089", ci),
                               Resources.ResourceManager.GetString("a0401", ci),
                               Resources.ResourceManager.GetString("a0033", ci),
                               Resources.ResourceManager.GetString("a1053", ci),
                               Resources.ResourceManager.GetString("a0302", ci),
                               Resources.ResourceManager.GetString("a0301", ci),
                               Resources.ResourceManager.GetString("a0012", ci),
                               Resources.ResourceManager.GetString("a0013", ci),
                               Resources.ResourceManager.GetString("a1079", ci)
                           );

        }
        public void updatemenu11(CustomReport rb)
        {

            changeColumnHeader(rb,
                               Resources.ResourceManager.GetString("a0012", ci),
                               Resources.ResourceManager.GetString("a1043", ci),
                               Resources.ResourceManager.GetString("a1044", ci),
                               Resources.ResourceManager.GetString("a1042", ci),
                               Resources.ResourceManager.GetString("a1045", ci),
                               Resources.ResourceManager.GetString("a1046", ci), "", "", "", "", ""
                           );

        }
        public void updatemenu12(CustomReport rb)
        {
            changeColumnHeader(rb,
                               Resources.ResourceManager.GetString("a0009", ci) + ", "
                               + Resources.ResourceManager.GetString("a1079", ci),//Date,ID
                               Resources.ResourceManager.GetString("a0401", ci) + "="
                               + Resources.ResourceManager.GetString("a1041", ci),//Qauntity=Sale
                               Resources.ResourceManager.GetString("a0503", ci),//Total Amount
                               Resources.ResourceManager.GetString("a0306", ci),//Expense
                               Resources.ResourceManager.GetString("a1026", ci),//Commission+Chongi
                               Resources.ResourceManager.GetString("a0006", ci),//Discount
                               Resources.ResourceManager.GetString("a1047", ci),//Investment
                               Resources.ResourceManager.GetString("a2004", ci),// Net Cash
                               Resources.ResourceManager.GetString("", ci),
                               Resources.ResourceManager.GetString("", ci),
                               Resources.ResourceManager.GetString("", ci)
                           );
        }

        #endregion

        public AllReportsCC(List<Landlord> landlords, string date)
        {
            InitializeComponent();
            bal = new BLogic();
            this.landlords = landlords;
            this.date = date;
            //AllBill rb = new AllBill();
            Account acc = Authentication.Account;
            check = 0;
            {
                SalesTodayHA5 rb = new SalesTodayHA5();
                DataTable dt = new BLogic().p_report_CustomerClient("sClient", date, date);
                rb.Database.Tables["Sales"].SetDataSource(dt);
                rb.SetParameterValue("Title", acc.shop_name);
                rb.SetParameterValue("Propriter", acc.propriters_name);
                rb.SetParameterValue("Name1", acc.name1 ?? "");
                rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                rb.SetParameterValue("Name2", acc.name2 ?? "");
                rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                rb.SetParameterValue("Address", acc.address ?? "");
                rb.SetParameterValue("Business", acc.business_type ?? "");
                currentDoc = rb;

                crystalReportViewer1.ReportSource = rb;
            }
            crystalReportViewer1.Refresh();

        }
        public AllReportsCC(bool isCustomer, string cl_id, string date, int PagePrint, bool isHeader)
        {
            InitializeComponent();
            DataTable dt = null;
            Account acc = Authentication.Account;
            if (isHeader)
            {
                #region With Header
                if (isCustomer == false)
                {
                    if (PagePrint == 1)//A4
                    {

                    }
                    else if (PagePrint == 2)//A5
                    {
                        SalesTodayHA5 rb = new SalesTodayHA5();
                        dt = new BLogic().p_report_CustomerClient("sClientBill", cl_id, date, date);
                        rb.Database.Tables["Sales"].SetDataSource(dt);

                        if (dt == null)
                            return;
                        rb.SetParameterValue("Title", acc.shop_name);
                        rb.SetParameterValue("Propriter", acc.propriters_name);
                        rb.SetParameterValue("Name1", acc.name1 ?? "");
                        rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                        rb.SetParameterValue("Name2", acc.name2 ?? "");
                        rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                        rb.SetParameterValue("Address", acc.address ?? "");
                        rb.SetParameterValue("Business", acc.business_type ?? "");
                        currentDoc = rb;

                        crystalReportViewer1.ReportSource = rb;
                    }
                    else if (PagePrint == 3)//A6
                    {
                        SalesTodayHA6 rb = new SalesTodayHA6();
                        if (rb == null)
                        {
                            return;
                        }
                        dt = new BLogic().p_report_CustomerClient("sClientBill", cl_id, date, date);
                        rb.Database.Tables["Sales"].SetDataSource(dt);

                        if (dt == null)
                            return;
                        rb.SetParameterValue("Title", acc.shop_name);
                        rb.SetParameterValue("Propriter", acc.propriters_name);
                        rb.SetParameterValue("Name1", acc.name1 ?? "");
                        rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                        rb.SetParameterValue("Name2", acc.name2 ?? "");
                        rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                        rb.SetParameterValue("Address", acc.address ?? "");
                        rb.SetParameterValue("Business", acc.business_type ?? "");
                        currentDoc = rb;

                        crystalReportViewer1.ReportSource = rb;
                    }


                }

                else if (isCustomer)
                {
                    if (PagePrint == 1)//A4
                    {

                    }
                    else if (PagePrint == 2)//A5
                    {
                        SalesTodayCustHA5 rb = new SalesTodayCustHA5();

                        dt = new BLogic().p_report_CustomerClient("sCustomerBill", cl_id, date, date);
                        if (dt == null)
                            return;
                        rb.Database.Tables["CustomerSales"].SetDataSource(dt);

                        rb.SetParameterValue("Title", acc.shop_name);
                        rb.SetParameterValue("Propriter", acc.propriters_name);
                        rb.SetParameterValue("Name1", acc.name1 ?? "");
                        rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                        rb.SetParameterValue("Name2", acc.name2 ?? "");
                        rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                        rb.SetParameterValue("Address", acc.address ?? "");
                        rb.SetParameterValue("Business", acc.business_type ?? "");
                        currentDoc = rb;

                        crystalReportViewer1.ReportSource = rb;
                    }
                    else if (PagePrint == 3)//A6
                    {
                        SalesTodayCustHA6 rb = new SalesTodayCustHA6();

                        dt = new BLogic().p_report_CustomerClient("sCustomerBill", cl_id, date, date);
                        if (dt == null)
                            return;
                        rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                        rb.SetParameterValue("Title", acc.shop_name);
                        rb.SetParameterValue("Propriter", acc.propriters_name);
                        rb.SetParameterValue("Name1", acc.name1 ?? "");
                        rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                        rb.SetParameterValue("Name2", acc.name2 ?? "");
                        rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                        rb.SetParameterValue("Address", acc.address ?? "");
                        rb.SetParameterValue("Business", acc.business_type ?? "");
                        currentDoc = rb;

                        crystalReportViewer1.ReportSource = rb;
                    }


                }
            }
            #endregion
            #region Without Header
            else
            {
                if (!isCustomer)
                {
                    if (PagePrint == 1)//A4
                    {

                    }
                    else if (PagePrint == 2)//A5
                    {
                        /*SalesTodayHA5 rb = new SalesTodayHA5();
                        dt = new BLogic().p_report_CustomerClient("sClientBill", cl_id, date, date);
                        rb.Database.Tables["Sales"].SetDataSource(dt);

                        if (dt == null)
                            return;
                        rb.SetParameterValue("Title", acc.shop_name);
                        rb.SetParameterValue("Propriter", acc.propriters_name);
                        rb.SetParameterValue("Name1", acc.name1 ?? "");
                        rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                        rb.SetParameterValue("Name2", acc.name2 ?? "");
                        rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                        rb.SetParameterValue("Address", acc.address ?? "");
                        rb.SetParameterValue("Business", acc.business_type ?? "");*/
                        ReportA5 rb = new ReportA5();

                        dt = new BLogic().p_report_CustomerClient("sClientBill", cl_id, date, date);
                        if (dt == null)
                            return;
                        rb.Database.Tables["Sales"].SetDataSource(dt);
                        rb.Subreports["SaleDetail"].SetDataSource(dt);
                        rb.Subreports["SaleExpense"].SetDataSource(dt);
                        currentDoc = rb;

                        crystalReportViewer1.ReportSource = rb;
                    }
                    else if (PagePrint == 3)//A6
                    {
                        SalesTodayNHA6 rb = new SalesTodayNHA6();
                        if (rb == null)
                        {
                            return;
                        }
                        dt = new BLogic().p_report_CustomerClient("sClientBill", cl_id, date, date);
                        rb.Database.Tables["Sales"].SetDataSource(dt);

                        if (dt == null)
                            return;
                        rb.SetParameterValue("Name1", acc.name1 ?? "''");
                        rb.SetParameterValue("Phone1", acc.phone1 ?? "''");
                        currentDoc = rb;

                        crystalReportViewer1.ReportSource = rb;
                    }
                    else if (PagePrint == 4)//A7
                    {
                        //SalesTodayNHA7 rb = new SalesTodayNHA7();
                        ReportSalesNHA7 rb = new ReportSalesNHA7();
                        if (rb == null)
                        {
                            return;
                        }
                        dt = new BLogic().p_report_CustomerClient("sClientBill", cl_id, date, date);
                        rb.Database.Tables["Sales"].SetDataSource(dt);
                        rb.Subreports["SaleDetail"].SetDataSource(dt);
                        rb.Subreports["SaleExpense"].SetDataSource(dt);
                        if (dt == null)
                            return;
                        //rb.SetParameterValue("Name1", acc.name1 ?? "''");
                        //rb.SetParameterValue("Phone1", acc.phone1 ?? "''");
                        currentDoc = rb;

                        crystalReportViewer1.ReportSource = rb;
                    }

                }
                else if (isCustomer)
                {
                    if (PagePrint == 1)//A4
                    {

                    }
                    else if (PagePrint == 2)//A5
                    {
                        SalesTodayCustHA5 rb = new SalesTodayCustHA5();

                        dt = new BLogic().p_report_CustomerClient("sCustomerBill", cl_id, date, date);
                        if (dt == null)
                            return;
                        rb.Database.Tables["CustomerSales"].SetDataSource(dt);

                        rb.SetParameterValue("Title", acc.shop_name);
                        rb.SetParameterValue("Propriter", acc.propriters_name);
                        rb.SetParameterValue("Name1", acc.name1 ?? "");
                        rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                        rb.SetParameterValue("Name2", acc.name2 ?? "");
                        rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                        rb.SetParameterValue("Address", acc.address ?? "");
                        rb.SetParameterValue("Business", acc.business_type ?? "");
                        currentDoc = rb;

                        crystalReportViewer1.ReportSource = rb;
                    }
                    else if (PagePrint == 3)//A6
                    {
                        SalesTodayCustHA6 rb = new SalesTodayCustHA6();

                        dt = new BLogic().p_report_CustomerClient("sCustomerBill", cl_id, date, date);
                        if (dt == null)
                            return;
                        rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                        rb.SetParameterValue("Title", acc.shop_name);
                        rb.SetParameterValue("Propriter", acc.propriters_name);
                        rb.SetParameterValue("Name1", acc.name1 ?? "");
                        rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                        rb.SetParameterValue("Name2", acc.name2 ?? "");
                        rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                        rb.SetParameterValue("Address", acc.address ?? "");
                        rb.SetParameterValue("Business", acc.business_type ?? "");
                        currentDoc = rb;

                        crystalReportViewer1.ReportSource = rb;
                    }
                    else if (PagePrint == 4)//A7
                    {
                        SalesTodayCustNHA7 rb = new SalesTodayCustNHA7();
                        dt = new BLogic().p_report_CustomerClient("sCustomerBill", cl_id, date, date);
                        if (dt == null)
                            return;
                        rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                        rb.SetParameterValue("Name1", acc.name1 ?? "");
                        rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                        currentDoc = rb;

                        crystalReportViewer1.ReportSource = rb;
                    }
                    else if (PagePrint == 5)//All Report Detail
                    {
                        SalesTodayCustAllDetail rb = new SalesTodayCustAllDetail();
                        //SalesTodayCustNHA7 rb = new SalesTodayCustNHA7();
                        dt = new BLogic().p_report_CustomerClient("sCustomerBill", cl_id, date, date);
                        if (dt == null)
                            return;
                        rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                        rb.SetParameterValue("Name1", acc.name1 ?? "");
                        rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                        crystalReportViewer1.ReportSource = rb;
                        currentDoc = rb;

                    }


                }
            }
            #endregion
            crystalReportViewer1.Refresh();


        }
        public AllReportsCC(List<Customer> customers, string date)
        {
            InitializeComponent();
            bal = new BLogic();
            this.customers = customers;
            this.date = date;
            check = 1;
            //AllBill rb = new AllBill();
            int i = 0;
            //foreach (Landlord landlord in this.landlords)
            {
                //rb = new AllBill();

                //RepCustomerBill rb = new RepCustomerBill();
                SalesTodayCustHA5 rb = new SalesTodayCustHA5();
                DataTable dt = bal.p_report_CustomerClient("sCustomer", date, date);
                //rb.Database.Tables["p_report_cc"].SetDataSource(dt);
                rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                Account acc = Authentication.Account;
                rb.SetParameterValue("Title", acc.shop_name);
                rb.SetParameterValue("Propriter", acc.propriters_name);
                rb.SetParameterValue("Name1", acc.name1 ?? "");
                rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                rb.SetParameterValue("Name2", acc.name2 ?? "");
                rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                rb.SetParameterValue("Address", acc.address ?? "");
                rb.SetParameterValue("Business", acc.business_type ?? "");
                crystalReportViewer1.ReportSource = rb;
                currentDoc = rb;

            }
            crystalReportViewer1.Refresh();
        }
        public bool isLocal = false;
        public AllReportsCC(bool isCustomer, List<Landlord> landlords, List<Customer> customers, string date, int PagePrint)
        {
            InitializeComponent();
            bal = new BLogic();
            this.landlords = landlords;
            this.customers = customers;
            this.date = date;

            DataTable dt = null;
            Account acc = Authentication.Account;

            if (isCustomer == false)
            {
                if (PagePrint == 1)//A4
                {

                }
                else if (PagePrint == 2)//A5
                {
                    SalesTodayHA5 rb = new SalesTodayHA5();
                    dt = new BLogic().p_report_CustomerClient("sClient", date, date);
                    rb.Database.Tables["Sales"].SetDataSource(dt);
                    rb.SetParameterValue("Title", acc.shop_name);
                    rb.SetParameterValue("Propriter", acc.propriters_name);
                    rb.SetParameterValue("Name1", acc.name1 ?? "");
                    rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                    rb.SetParameterValue("Name2", acc.name2 ?? "");
                    rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                    rb.SetParameterValue("Address", acc.address ?? "");
                    rb.SetParameterValue("Business", acc.business_type ?? "");
                    crystalReportViewer1.ReportSource = rb;
                    currentDoc = rb;

                }
                else if (PagePrint == 3)//A6
                {
                    SalesTodayHA6 rb = new SalesTodayHA6();
                    if (rb == null)
                    {
                        return;
                    }
                    dt = new BLogic().p_report_CustomerClient("sClient", date, date);
                    rb.Database.Tables["Sales"].SetDataSource(dt);
                    rb.SetParameterValue("Title", acc.shop_name);
                    rb.SetParameterValue("Propriter", acc.propriters_name);
                    rb.SetParameterValue("Name1", acc.name1 ?? "");
                    rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                    rb.SetParameterValue("Name2", acc.name2 ?? "");
                    rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                    rb.SetParameterValue("Address", acc.address ?? "");
                    rb.SetParameterValue("Business", acc.business_type ?? "");
                    crystalReportViewer1.ReportSource = rb;
                    currentDoc = rb;

                }
                else if (PagePrint == 4)//A7
                {
                    //SalesTodayNHA7 rb = new SalesTodayNHA7();
                    //ReportSalesNHA7 rb = new ReportSalesNHA7();
                    ReportTest rb = new ReportTest();
                    if (rb == null)
                    {
                        return;
                    }
                    dt = new BLogic().p_report_CustomerClient("sClient", date, date);
                    rb.Database.Tables["Sales"].SetDataSource(dt);
                    rb.Subreports["SaleDetail"].SetDataSource(dt);
                    rb.Subreports["SaleExpense"].SetDataSource(dt);

                    //rb.SetParameterValue("Title", acc.shop_name);
                    //rb.SetParameterValue("Propriter", acc.propriters_name);
                    //rb.SetParameterValue("Name1", acc.name1 ?? "");
                    //rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                    //rb.SetParameterValue("Name2", acc.name2 ?? "");
                    //rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                    //rb.SetParameterValue("Address", acc.address ?? "");
                    //rb.SetParameterValue("Business", acc.business_type ?? "");
                    crystalReportViewer1.ReportSource = rb;
                    currentDoc = rb;


                }
                else if (PagePrint == 5)//All Report
                {
                    /* SalesTodayAllDetail rb = new SalesTodayAllDetail();
                     dt = bal.p_report_CustomerClient("sClient", date, date);
                     rb.Database.Tables["Sales"].SetDataSource(dt);
                     rb.SetParameterValue("Title", acc.shop_name);
                     rb.SetParameterValue("Propriter", acc.propriters_name);
                     rb.SetParameterValue("Name1", acc.name1 ?? "");
                     rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                     rb.SetParameterValue("Name2", acc.name2 ?? "");
                     rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                     rb.SetParameterValue("Address", acc.address ?? "");
                     rb.SetParameterValue("Business", acc.business_type ?? "");*/
                    ReportA5 rb = new ReportA5();
                    dt = new BLogic().p_report_CustomerClient("sClient", date, date);
                    if (dt == null)
                        return;
                    rb.Database.Tables["Sales"].SetDataSource(dt);
                    rb.Subreports["SaleDetail"].SetDataSource(dt);
                    rb.Subreports["SaleExpense"].SetDataSource(dt);
                    currentDoc = rb;

                    crystalReportViewer1.ReportSource = rb;
                }

            }
            else if (isCustomer)
            {
                if (PagePrint == 1)//A4
                {

                }
                else if (PagePrint == 2)//A5
                {
                    SalesTodayCustHA5 rb = new SalesTodayCustHA5();
                    dt = bal.p_report_CustomerClient("sCustomer", date, date);
                    //rb.Database.Tables["p_report_cc"].SetDataSource(dt);
                    rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                    rb.SetParameterValue("Title", acc.shop_name);
                    rb.SetParameterValue("Propriter", acc.propriters_name);
                    rb.SetParameterValue("Name1", acc.name1 ?? "");
                    rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                    rb.SetParameterValue("Name2", acc.name2 ?? "");
                    rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                    rb.SetParameterValue("Address", acc.address ?? "");
                    rb.SetParameterValue("Business", acc.business_type ?? "");
                    crystalReportViewer1.ReportSource = rb;
                    currentDoc = rb;

                }
                else if (PagePrint == 3)//A6
                {
                    SalesTodayCustHA6 rb = new SalesTodayCustHA6();
                    dt = bal.p_report_CustomerClient("sCustomer", date, date);
                    //rb.Database.Tables["p_report_cc"].SetDataSource(dt);
                    rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                    rb.SetParameterValue("Title", acc.shop_name);
                    rb.SetParameterValue("Propriter", acc.propriters_name);
                    rb.SetParameterValue("Name1", acc.name1 ?? "");
                    rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                    rb.SetParameterValue("Name2", acc.name2 ?? "");
                    rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                    rb.SetParameterValue("Address", acc.address ?? "");
                    rb.SetParameterValue("Business", acc.business_type ?? "");
                    crystalReportViewer1.ReportSource = rb;
                    currentDoc = rb;

                }
                else if (PagePrint == 4)//A7
                {
                    SalesTodayCustNHA7 rb = new SalesTodayCustNHA7();
                    dt = bal.p_report_CustomerClient("sCustomer", date, date);
                    //rb.Database.Tables["p_report_cc"].SetDataSource(dt);
                    rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                    rb.SetParameterValue("Name1", acc.name1 ?? "");
                    rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                    crystalReportViewer1.ReportSource = rb;
                    currentDoc = rb;

                }
                else if (PagePrint == 5)//All Report
                {
                    SalesTodayCustAllDetail rb = new SalesTodayCustAllDetail();
                    dt = bal.p_report_CustomerClient("sCustomer", date, date);
                    //rb.Database.Tables["p_report_cc"].SetDataSource(dt);
                    rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                    rb.SetParameterValue("Title", acc.shop_name);
                    rb.SetParameterValue("Propriter", acc.propriters_name);
                    rb.SetParameterValue("Name1", acc.name1 ?? "");
                    rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                    rb.SetParameterValue("Name2", acc.name2 ?? "");
                    rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                    rb.SetParameterValue("Address", acc.address ?? "");
                    rb.SetParameterValue("Business", acc.business_type ?? "");
                    currentDoc = rb;

                    crystalReportViewer1.ReportSource = rb;
                }



            }

            crystalReportViewer1.Refresh();




        }

        public void printLandlordBillDetail(string date, string id)
        {
            SalesTodayAllDetail rb = new SalesTodayAllDetail();
            dt = new BLogic().p_report_CustomerClient("sClient", date, date);
            if (dt == null)
                return;
            rb.Database.Tables["Sales"].SetDataSource(dt);
            DataTable wm = new DataTable();
            wm.Columns.Add("waterpath", typeof(string));
            string startupPath = Environment.CurrentDirectory;
            wm.Rows.Add(@startupPath + "\\watermark.jpg");
            rb.Database.Tables["Watermark"].SetDataSource(wm);
            currentDoc = rb;


            crystalReportViewer1.ReportSource = rb;
        }

        private void toolClientA4_Click(object sender, EventArgs e)
        {

        }

        #region Client Page Size

        private void toolClientA5_Click(object sender, EventArgs e)
        {
            SalesTodayHA5 rb = new SalesTodayHA5();
            Account acc = Authentication.Account;
            DataTable dt = new BLogic().p_report_CustomerClient("sClient", date, date);
            rb.Database.Tables["Sales"].SetDataSource(dt);
            rb.SetParameterValue("Title", acc.shop_name);
            rb.SetParameterValue("Propriter", acc.propriters_name);
            rb.SetParameterValue("Name1", acc.name1 ?? "");
            rb.SetParameterValue("Phone1", acc.phone1 ?? "");
            rb.SetParameterValue("Name2", acc.name2 ?? "");
            rb.SetParameterValue("Phone2", acc.phone2 ?? "");
            rb.SetParameterValue("Address", acc.address ?? "");
            rb.SetParameterValue("Business", acc.business_type ?? "");
            currentDoc = rb;

            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
        }
        private void toolClientA6_Click(object sender, EventArgs e)
        {
            if (check == 0)
            {

                Account acc = Authentication.Account;
                SalesTodayHA5 rb = new SalesTodayHA5();
                DataTable dt = bal.p_report_CustomerClient("sClient", date, date);
                rb.Database.Tables["Sales"].SetDataSource(dt);
                rb.SetParameterValue("Title", acc.shop_name);
                rb.SetParameterValue("Propriter", acc.propriters_name);
                rb.SetParameterValue("Name1", acc.name1 ?? "");
                rb.SetParameterValue("Phone1", acc.phone1 ?? "");
                rb.SetParameterValue("Name2", acc.name2 ?? "");
                rb.SetParameterValue("Phone2", acc.phone2 ?? "");
                rb.SetParameterValue("Address", acc.address ?? "");
                rb.SetParameterValue("Business", acc.business_type ?? "");
                currentDoc = rb;

                crystalReportViewer1.ReportSource = rb;
                crystalReportViewer1.Refresh();
            }
        }
        #endregion

        #region Customer Page Size
        private void toolCustomerA4_Click(object sender, EventArgs e)
        {

        }
        private void toolCustomerA5_Click(object sender, EventArgs e)
        {
            SalesTodayCustHA5 rb = new SalesTodayCustHA5();
            DataTable dt = bal.p_report_CustomerClient("sCustomer", date, date);
            rb.Database.Tables["CustomerSales"].SetDataSource(dt);
            Account acc = Authentication.Account;
            rb.SetParameterValue("Title", acc.shop_name);
            rb.SetParameterValue("Propriter", acc.propriters_name);
            rb.SetParameterValue("Name1", acc.name1 ?? "");
            rb.SetParameterValue("Phone1", acc.phone1 ?? "");
            rb.SetParameterValue("Name2", acc.name2 ?? "");
            rb.SetParameterValue("Phone2", acc.phone2 ?? "");
            rb.SetParameterValue("Address", acc.address ?? "");
            rb.SetParameterValue("Business", acc.business_type ?? "");
            currentDoc = rb;

            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
        }

        private void toolCustomerA6_Click(object sender, EventArgs e)
        {
            SalesTodayCustHA6 rb = new SalesTodayCustHA6();
            DataTable dt = bal.p_report_CustomerClient("sCustomer", date, date);
            //rb.Database.Tables["p_report_cc"].SetDataSource(dt);
            rb.Database.Tables["CustomerSales"].SetDataSource(dt);
            Account acc = Authentication.Account;
            rb.SetParameterValue("Title", acc.shop_name);
            rb.SetParameterValue("Propriter", acc.propriters_name);
            rb.SetParameterValue("Name1", acc.name1 ?? "");
            rb.SetParameterValue("Phone1", acc.phone1 ?? "");
            rb.SetParameterValue("Name2", acc.name2 ?? "");
            rb.SetParameterValue("Phone2", acc.phone2 ?? "");
            rb.SetParameterValue("Address", acc.address ?? "");
            rb.SetParameterValue("Business", acc.business_type ?? "");
            currentDoc = rb;

            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
        }

        private void btn_convertpdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.ShowDialog();
        }

        public void ReportBSeet(DataTable dt, string datec)
        {
            this.dt = dt;
            ReportBalSheet rb = new ReportBalSheet();
            rb.Database.Tables["DTBalanceSheet"].SetDataSource(this.dt);


            DataTable wm = new DataTable();
            wm.Columns.Add("waterpath", typeof(string));
            string startupPath = Environment.CurrentDirectory;
            wm.Rows.Add(@startupPath + "\\watermark.jpg");
            rb.Database.Tables["Watermark"].SetDataSource(wm);

            rb.SetParameterValue("sdate", datec);
            currentDoc = rb;

            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
        }



        #endregion

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Account acc = Authentication.Account;
            SalesTodayHA5 rb = new SalesTodayHA5();
            DataTable dt = new BLogic().p_report_CustomerClient("sClient", date, date);
            rb.Database.Tables["Sales"].SetDataSource(dt);
            rb.SetParameterValue("Title", "");
            rb.SetParameterValue("Propriter", "");
            rb.SetParameterValue("Name1", "");
            rb.SetParameterValue("Phone1", "");
            rb.SetParameterValue("Name2", "");
            rb.SetParameterValue("Phone2", "");
            rb.SetParameterValue("Address", "");
            rb.SetParameterValue("Business", "");
            currentDoc = rb;

            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
        }
        ReportDocument currentDoc;
        public void savetoPDF(ReportDocument cr, string filetype)
        {
            AdminLog adminLog = LogUtill.getAdminInputLog();

            string path = adminLog.ReportPath;
            if (!Directory.Exists(path) && path != "")
            {
                Directory.CreateDirectory(path);
            }
            path = path + cr.GetClassName().ToString() + "-" + date + filetype;
            //PageMargins margins = new PageMargins { topMargin = 100, leftMargin = 250, bottomMargin = 100, rightMargin = 100 };
            //cr.PrintOptions.ApplyPageMargins(margins);
            //Stream str = cr.ExportToStream(ExportFormatType.PortableDocFormat);
            //int length = Convert.ToInt32(str.Length);
            //byte[] bytes = new byte[length];
            //str.Read(bytes, 0, length);
            //str.Close();
            //File.WriteAllBytes(@path, bytes);
            //Process.Start("C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe", "file:///"+path);

            // Export the report to HTML format
            if (filetype == ".html")
            {
                ExportOptions exportOptions = new ExportOptions();
                exportOptions.ExportFormatType = ExportFormatType.HTML32; // or HTML40 if needed
                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                DiskFileDestinationOptions diskOptions = new DiskFileDestinationOptions();
                diskOptions.DiskFileName = path;
                exportOptions.DestinationOptions = diskOptions;

                // Set HTML export options to include page breaks
                HTMLFormatOptions htmlOptions = new HTMLFormatOptions();
                htmlOptions.HTMLBaseFolderName = Path.GetDirectoryName(path);
                htmlOptions.HTMLFileName = Path.GetFileNameWithoutExtension(path);
                htmlOptions.HTMLEnableSeparatedPages = true; // Enable page breaks
                exportOptions.FormatOptions = htmlOptions;

                cr.Export(exportOptions);
            }
            else if (filetype == ".pdf")
                cr.ExportToDisk(ExportFormatType.PortableDocFormat, path);
            else if (filetype == ".xlsx")
                cr.ExportToDisk(ExportFormatType.Excel, path);



            // Check if the HTML file exists
            if (File.Exists(path))
            {
                // Open the HTML file in the default web browser
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savetoPDF(currentDoc, ".pdf");
        }

        private void hTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {

            savetoPDF(currentDoc, ".html");

        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savetoPDF(currentDoc, ".xlsx");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;

            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void DetailReport(DataTable dt, DataTable dtPro, string startdate, string lastdate, int acc_open, string datec)
        {
            this.dt = dt;
            ReportDetail rb = new ReportDetail();
            DataTable wm = new DataTable();

            wm.Columns.Add("waterpath", typeof(string));
            string startupPath = Environment.CurrentDirectory;
            wm.Rows.Add(@startupPath + "\\watermark.jpg");
            rb.Database.Tables["Watermark"].SetDataSource(wm);
            rb.Database.Tables["DetailReport"].SetDataSource(this.dt);
            rb.Subreports[0].Database.Tables["ProductTotal"].SetDataSource(dtPro);

            rb.SetParameterValue("acc_open", acc_open);
            rb.SetParameterValue("sdate", datec);
            currentDoc = rb;

            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
        }
        public void printSeason(DataTable dt)
        {
            this.dt = dt;
            Season rb = new Season();
            //DataTable wm = new DataTable();
            //wm.Columns.Add("waterpath", typeof(string));
            //string startupPath = Environment.CurrentDirectory;
            //wm.Rows.Add(@startupPath + "\\watermark.jpg");
            //rb.Database.Tables["Watermark"].SetDataSource(wm);
            rb.Database.Tables["Season"].SetDataSource(this.dt);
            currentDoc = rb;

            crystalReportViewer1.ReportSource = rb;
            crystalReportViewer1.Refresh();
        }


    }
}
