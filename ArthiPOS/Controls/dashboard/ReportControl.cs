using ArthiPOS.Properties;
using ArthiPOS.Reporting;
using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class ReportControl : Form//UserControl
    {

        public enum ReportMenu
        {
            Default,
            CreateSeasonReport,
            //BipariInvesment,
            ExpenseCashReceive,
            CashReceived,
            ExpenseDetail,
            ProfitLoss,
            BalanceSheetReport,
            AugraiReport,
            InvestmentRecovery,
            BipariSales, BipariInvestment, BipariList, BipariProfit,
            CustomerSale, CustomerList, CustomerProfit, SERP,
            AugraiDiff, CustBillsandReceivings, DetailReport
        }
        public enum SearchCheck
        {
            Date, Khata, Name, City, Product
        }
        public ReportMenu eMenu = ReportMenu.Default;
        // SearchCheck eSearch;
        private BLReport bal;
        private string startdate = "", lastdate = "";
        private DataTable dt;
        public ReportControl()
        {
            InitializeComponent();
            bal = new BLReport();
            UIUpdate();
            txt_page_size.Text = "" + pageSize;
            chk_print_all.Visible = false;
        }
        #region Localization
        private void UIUpdate()
        {
            lbl_select_name.Text = Resources.ResourceManager.GetString("a1048");
            chk_date.Text = Resources.ResourceManager.GetString("a0009");
            chk_id.Text = Resources.ResourceManager.GetString("a0012");
            chk_name.Text = Resources.ResourceManager.GetString("a0205");
            rd_city.Text = Resources.ResourceManager.GetString("a1065");
            rd_product.Text = Resources.ResourceManager.GetString("a1066");
            rd_none.Text = Resources.ResourceManager.GetString("a1067");
            txt_name.WaterMarkText = Resources.ResourceManager.GetString("a1048");
            lbl_start.Text = Resources.ResourceManager.GetString("a1068");
            lbl_end.Text = Resources.ResourceManager.GetString("a1069");
            btn_print_report.Text = Resources.ResourceManager.GetString("a1096");
        }
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ur-PK");

        public void updatemenu1()
        {
        }
        public void updatemenu2()
        {
            if (grid_report.Rows.Count == 0)
            {
                return;
            }
            changeColumnNameLocal(
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

            //grid_report.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0013");//Khata
            //grid_report.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0205");//Name
            //grid_report.Columns[2].HeaderText = Resources.ResourceManager.GetString("a1071");//Balance
            //grid_report.Columns[3].HeaderText = Resources.ResourceManager.GetString("a1074");//Credit
            //grid_report.Columns[4].HeaderText = Resources.ResourceManager.GetString("a1075");//Debit
            //grid_report.Columns[5].HeaderText = Resources.ResourceManager.GetString("a1076");//Remaining Amount
            //grid_report.Columns[6].HeaderText = Resources.ResourceManager.GetString("a0009");//Date
        }
        public void updatemenu3()
        {
            changeColumnNameLocal(
                               Resources.ResourceManager.GetString("a1079", ci),
                               Resources.ResourceManager.GetString("a0205", ci),
                               Resources.ResourceManager.GetString("a2021", ci),
                               Resources.ResourceManager.GetString("a2023", ci),
                               Resources.ResourceManager.GetString("a2003", ci),
                               Resources.ResourceManager.GetString("a2022", ci),
                               "",
                                "", "", "", ""
                           );
            chk_print_all.Visible = true;

            //grid_report.Columns[0].HeaderText = Resources.ResourceManager.GetString("a1079");//#
            //grid_report.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0012");//ID
            //grid_report.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0205");//Name
            //grid_report.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0032");//Amount
            //grid_report.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0035");//Recevie Amount
            //grid_report.Columns[4].HeaderText = Resources.ResourceManager.GetString("a0009");//Date
        }
        public void updatemenu13()
        {
            changeColumnNameLocal(
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
        public void updatemenu14()
        {

        }
        public void updatemenu4()
        {

        }
        public void updatemenu5()
        {
            changeColumnNameLocal(
                               Resources.ResourceManager.GetString("a1079", ci),
                               Resources.ResourceManager.GetString("a0205", ci),
                               Resources.ResourceManager.GetString("a0032", ci)
                               , "", "", "", "", "", "", "", ""
                           );
            //grid_report.Columns[0].HeaderText = Resources.ResourceManager.GetString("a1079");//#
            //grid_report.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0205");//Name
            //grid_report.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0032");//Amount

        }
        public void updatemenu6()
        {
            changeColumnNameLocal(
                              Resources.ResourceManager.GetString("a1079", ci),
                              Resources.ResourceManager.GetString("a1077", ci),
                              Resources.ResourceManager.GetString("a0032", ci),
                              Resources.ResourceManager.GetString("a0013", ci),
                              Resources.ResourceManager.GetString("a0009", ci),
                              "", "", "", "", "", ""
                          );
            //grid_report.Columns[0].HeaderText = Resources.ResourceManager.GetString("a1079");//#
            //grid_report.Columns[1].HeaderText = Resources.ResourceManager.GetString("a1077");//Details
            //grid_report.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0032");//Amount
            //grid_report.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0013");//Khata
            //grid_report.Columns[4].HeaderText = Resources.ResourceManager.GetString("a0009");//Date
        }
        public void updatemenu7()
        {
            changeColumnNameLocal(
                              Resources.ResourceManager.GetString("a1079", ci),
                              Resources.ResourceManager.GetString("a0205", ci),
                              Resources.ResourceManager.GetString("a0032", ci),
                              Resources.ResourceManager.GetString("a1078", ci),
                              Resources.ResourceManager.GetString("a0012", ci),
                              Resources.ResourceManager.GetString("a0013", ci),
                              Resources.ResourceManager.GetString("a0009", ci),
                               "", "", "", ""
                          );
            //grid_report.Columns[0].HeaderText = Resources.ResourceManager.GetString("a1079");//#
            //grid_report.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0205");//Name
            //grid_report.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0032");//Amount
            //grid_report.Columns[3].HeaderText = Resources.ResourceManager.GetString("a1078");//Discount
            //grid_report.Columns[4].HeaderText = Resources.ResourceManager.GetString("a0012");//ID
            //grid_report.Columns[5].HeaderText = Resources.ResourceManager.GetString("a0013");//Khata
            //grid_report.Columns[6].HeaderText = Resources.ResourceManager.GetString("a0009");//Date
        }
        public void updatemenu8()
        {
            changeColumnNameLocal(
                               Resources.ResourceManager.GetString("a1079", ci),
                               Resources.ResourceManager.GetString("a0032", ci),
                               Resources.ResourceManager.GetString("a0205", ci),
                               Resources.ResourceManager.GetString("a1060", ci),
                               Resources.ResourceManager.GetString("a0006", ci),
                               Resources.ResourceManager.GetString("a2009", ci),
                               Resources.ResourceManager.GetString("a2010", ci),
                               Resources.ResourceManager.GetString("a2011", ci)
                               , "", "", ""
                           );
        }
        public void updatemenu9()
        {

            changeColumnNameLocal(
                               Resources.ResourceManager.GetString("a0009", ci),
                               Resources.ResourceManager.GetString("a0205", ci),
                               Resources.ResourceManager.GetString("a0401", ci),
                               Resources.ResourceManager.GetString("a1053", ci),
                               Resources.ResourceManager.GetString("a1026", ci),
                               Resources.ResourceManager.GetString("a0306", ci),
                               Resources.ResourceManager.GetString("a0012", ci),
                               Resources.ResourceManager.GetString("a1080", ci),
                               Resources.ResourceManager.GetString("a0013", ci),
                               Resources.ResourceManager.GetString("a0407", ci),
                               Resources.ResourceManager.GetString("a1079", ci)
                           );

        }

        public void updatemenu15()
        {
        }

        public void updatemenu10()
        {
            //grid_report.Columns[0].HeaderText = Resources.ResourceManager.GetString("a1079");//#
            //grid_report.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0205");//Name
            //grid_report.Columns[2].HeaderText = Resources.ResourceManager.GetString("a1089");//Chalan
            //grid_report.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0401");//Quantity
            //grid_report.Columns[4].HeaderText = Resources.ResourceManager.GetString("a0033");//Sale Amount
            //grid_report.Columns[5].HeaderText = Resources.ResourceManager.GetString("a1053");//Total
            //grid_report.Columns[6].HeaderText = Resources.ResourceManager.GetString("a0302");//Commission
            //grid_report.Columns[7].HeaderText = Resources.ResourceManager.GetString("a0301");//Chongi
            //grid_report.Columns[8].HeaderText = Resources.ResourceManager.GetString("a0012");//ID
            //grid_report.Columns[9].HeaderText = Resources.ResourceManager.GetString("a0013");//Khata
            //grid_report.Columns[10].HeaderText = Resources.ResourceManager.GetString("a0009");//Date
            changeColumnNameLocal(
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
        public void updatemenu11()
        {
            //grid_report.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0012");
            //grid_report.Columns[1].HeaderText = Resources.ResourceManager.GetString("a1043");
            //grid_report.Columns[2].HeaderText = Resources.ResourceManager.GetString("a1044");
            //grid_report.Columns[3].HeaderText = Resources.ResourceManager.GetString("a1042");
            //grid_report.Columns[4].HeaderText = Resources.ResourceManager.GetString("a1045");
            //grid_report.Columns[5].HeaderText = Resources.ResourceManager.GetString("a1046");
            changeColumnNameLocal(
                               Resources.ResourceManager.GetString("a0012", ci),
                               Resources.ResourceManager.GetString("a1043", ci),
                               Resources.ResourceManager.GetString("a1044", ci),
                               Resources.ResourceManager.GetString("a1042", ci),
                               Resources.ResourceManager.GetString("a1045", ci),
                               Resources.ResourceManager.GetString("a1046", ci), "", "", "", "", ""
                           );

        }
        public void updatemenu12()
        {
            changeColumnNameLocal(
                               Resources.ResourceManager.GetString("a1079", ci),
                               Resources.ResourceManager.GetString("a0009", ci),
                               Resources.ResourceManager.GetString("a0401", ci),
                               Resources.ResourceManager.GetString("a0504", ci),
                               Resources.ResourceManager.GetString("a1041", ci),
                               Resources.ResourceManager.GetString("a0304", ci),
                               Resources.ResourceManager.GetString("a0303", ci),
                               Resources.ResourceManager.GetString("a0307", ci),
                               Resources.ResourceManager.GetString("a1034", ci),
                               Resources.ResourceManager.GetString("a1035", ci),
                               Resources.ResourceManager.GetString("a1032", ci)
                           );
            //grid_report.Columns[0].HeaderText = Resources.ResourceManager.GetString("a1079");//#
            //grid_report.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0009");//Date
            //grid_report.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0401");//Quantity
            //grid_report.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0504");//Total Sale
            //grid_report.Columns[4].HeaderText = Resources.ResourceManager.GetString("a1041");//Total Sale
            //grid_report.Columns[5].HeaderText = Resources.ResourceManager.GetString("a0304");//Fright
            //grid_report.Columns[6].HeaderText = Resources.ResourceManager.GetString("a0303");//Labour
            //grid_report.Columns[7].HeaderText = Resources.ResourceManager.GetString("a0307");//Munshiana
            //grid_report.Columns[8].HeaderText = Resources.ResourceManager.GetString("a1034");//BipariCommission
            //grid_report.Columns[9].HeaderText = Resources.ResourceManager.GetString("a1035");//Bipari Chongi
            //grid_report.Columns[10].HeaderText = Resources.ResourceManager.GetString("a1032");//Customer Commission
            grid_report.Columns[11].HeaderText = Resources.ResourceManager.GetString("a1033", ci);//Customer Chongi
            grid_report.Columns[12].HeaderText = Resources.ResourceManager.GetString("a0038", ci);//Receving
            grid_report.Columns[13].HeaderText = Resources.ResourceManager.GetString("a1078", ci);//Discount
            grid_report.Columns[14].HeaderText = Resources.ResourceManager.GetString("a0306", ci);//Expense
            grid_report.Columns[15].HeaderText = Resources.ResourceManager.GetString("a1047", ci);//Augrai/Investment
            grid_report.Columns[16].HeaderText = Resources.ResourceManager.GetString("a2004", ci);//NetCash

        }

        #endregion
        private void ReportControl_Load(object sender, EventArgs e)
        {
            //showChanges(false, false, false, false, false, false, false, false, false, false);
        }


        public void showChanges(bool _menu_panel, bool _check_panel, bool _txt_id, bool _chk_id, bool _txt_name,
            bool _chk_name, bool _chk_city, bool _chk_product,
            bool _date_panel, bool _chk_date)
        {
            menu_panel.Visible = _menu_panel;
            check_panel.Visible = _check_panel;
            date_panel.Visible = _date_panel;

            chk_id.Visible = _chk_id;

            txt_name.Visible = _txt_name;

            chk_name.Visible = _chk_name;
            //chk_product.Visible = _chk_product;
            //chk_city.Visible = _chk_city;
            chk_date.Visible = _chk_date;
            groupBox1.Visible = false;
        }

        private void menu_admin_1_Click(object sender, EventArgs e)
        {
            lbl_select_name.Text = menu_admin_1.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(true, true, false, false, false, false, false, false, true, true);
            eMenu = ReportMenu.CreateSeasonReport;
        }

        private void menu_admin_2_Click(object sender, EventArgs e)
        {
            lbl_select_name.Text = menu_admin_2.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(false, false, false, false, false, false, false, false, true, false);
            eMenu = ReportMenu.BipariInvestment;
            loadGridData(1, "", "", "");


        }

        private void menu_admin_3_Click(object sender, EventArgs e)
        {
            lbl_select_name.Text = menu_admin_3.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(true, true, true, true, true, true, true, true, true, true);
            eMenu = ReportMenu.CashReceived;


            loadGridData(1, "", "", "");
        }

        private void menu_admin_4_Click(object sender, EventArgs e)
        {
            lbl_select_name.Text = menu_admin_4.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(true, true, false, false, false, false, false, false, true, true);
            eMenu = ReportMenu.ExpenseDetail;
            loadGridData(1, "", "", "");
        }

        private void menu_admin_5_Click(object sender, EventArgs e)
        {
            lbl_select_name.Text = menu_admin_5.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(true, true, false, false, false, false, false, false, true, true);
            eMenu = ReportMenu.ProfitLoss;
            loadGridData(1, "", "", "");
        }

        private void menu_admin_6_Click(object sender, EventArgs e)
        {
            lbl_select_name.Text = menu_admin_6.Text;
            showChanges(true, true, false, false, false, false, false, false, true, true);
            eMenu = ReportMenu.BalanceSheetReport;
            if (chk_date.Checked)
            {
                startdate = date_start.Text;
                lastdate = date_last.Text;
                loadGridData(1, startdate, lastdate, "");
            }
            else
                loadGridData(1, "", "", "");

        }

        private void menu_admin_8_Click(object sender, EventArgs e)
        {
            lbl_select_name.Text = menu_admin_8.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(true, true, false, false, false, false, false, false, true, true);
            eMenu = ReportMenu.AugraiReport;
            loadGridData(1, "", "", "");
        }

        private void menu_admin_9_Click(object sender, EventArgs e)
        {
            lbl_select_name.Text = menu_admin_9.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(true, true, false, false, false, false, false, false, true, true);
            eMenu = ReportMenu.InvestmentRecovery;
        }

        private void menu_bipari_1_Click(object sender, EventArgs e)
        {
            lbl_select_name.Text = menu_bipari_1.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(true, true, false, false, false, false, false, false, true, true);
            eMenu = ReportMenu.BipariSales;
            loadGridData(1, "", "", "");
        }

        private void menu_bipari_2_Click(object sender, EventArgs e)
        {
            eMenu = ReportMenu.BipariInvestment;
            lbl_select_name.Text = menu_bipari_2.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(false, false, false, false, false, false, false, false, false, false);
        }

        private void menu_bipari_3_Click(object sender, EventArgs e)
        {
            eMenu = ReportMenu.BipariList;
            lbl_select_name.Text = menu_bipari_3.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(false, false, false, false, false, false, false, false, false, false);
        }

        private void menu_bipari_4_Click(object sender, EventArgs e)
        {
            eMenu = ReportMenu.BipariProfit;
            lbl_select_name.Text = menu_bipari_4.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(true, true, false, false, false, false, false, false, true, true);
        }

        private void menu_customer_3_Click(object sender, EventArgs e)
        {
            eMenu = ReportMenu.BipariProfit;
            lbl_select_name.Text = menu_customer_3.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(true, true, false, false, false, false, false, false, true, true);
        }

        private void menu_customer_2_Click(object sender, EventArgs e)
        {
            eMenu = ReportMenu.CustomerList;
            lbl_select_name.Text = menu_customer_2.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(true, true, true, true, true, true, true, true, true, true);
            loadGridData(1, "", "", "");

        }

        private void menu_customer_1_Click(object sender, EventArgs e)
        {
            eMenu = ReportMenu.CustomerSale;
            lbl_select_name.Text = menu_customer_1.Text;
            showChanges(false, false, false, false, false, false, false, false, false, false);
            showChanges(true, true, true, true, true, true, true, true, true, true);
        }

        private void chk_name_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_name.Checked)
                chk_id.Checked = false;


        }




        #region Paging
        int pageindex = 1;
        int pageSize = 18;

        public void loadGridData(int index, string sdate, string ldate, string search)
        {
            try
            {
                string size = txt_page_size.Text;
                if (size != "")
                    pageindex = int.Parse(size);
                else
                    pageindex = index;


                switch (eMenu)
                {
                    case ReportMenu.BalanceSheetReport://Menu 2
                        {

                            List<Object> obj = (List<object>)bal.p_balance_sheet_read(sdate, ldate,
                                index, pageSize);
                            if (obj == null)
                            {
                                return;
                            }
                            dt = (DataTable)obj[1];

                            /*int count = dt.Rows.Count;
                            int credit = dt.Rows.Cast<DataRow>()
                                        .Sum(t => Convert.ToInt32(t[3].ToString()));

                            int debit = dt.Rows.Cast<DataRow>()
                                        .Sum(t => Convert.ToInt32(t[4].ToString()));
                            object[] o = { count + 1, Resources.ResourceManager.GetString("a1053"), 0 , credit,debit};
                            dt.Rows.Add(o);*/
                            grid_report.DataSource = dt;
                            updatemenu2();
                            this.PopulatePager((int)obj[0], index);
                            break;
                        }
                    case ReportMenu.AugraiReport://Menu 3
                        {
                            string print = "0";
                            if (chk_print_all.Checked)
                            {
                                print = "1";
                            }
                            dt = new BLogic().p_customer_CRUD("Augrai", print, date_start.Text);
                            grid_report.DataSource = dt;
                            updatemenu3();
                            break;
                        }
                    case ReportMenu.ProfitLoss://Menu 5
                        {
                            dt = bal.getProfiftLossDetails(sdate, ldate);
                            addRowingrid_bipari(dt);
                            updatemenu5();

                            //grid_report.DataSource = dt;
                            break;
                        }
                    case ReportMenu.ExpenseDetail://Menu 6
                        {
                            index = 1;
                            if (size != "")
                                pageindex = int.Parse(size);
                            else
                                pageindex = 18;

                            string filter = txt_name.Text;
                            List<Object> obj = (List<object>)bal.expenseDetails(sdate, ldate,
                                index, pageSize, filter);
                            dt = (DataTable)obj[1];
                            int count = dt.Rows.Count;
                            int total = dt.Rows.Cast<DataRow>()
                                        .Sum(t => Convert.ToInt32(t[2].ToString()));

                            object[] o = { count + 1, Resources.ResourceManager.GetString("a1053"), total };
                            dt.Rows.Add(o);
                            grid_report.DataSource = dt;
                            updatemenu6();
                            this.PopulatePager((int)obj[0], index);

                            break;
                        }
                    case ReportMenu.CashReceived://Menu 7
                        {
                            if (size != "")
                                pageindex = int.Parse(size);
                            else
                                pageindex = 23;
                            index = 1;
                            List<Object> obj = (List<object>)bal.cashReceving(sdate, ldate,
                                index, pageSize, search);
                            dt = (DataTable)obj[1];
                            int count = dt.Rows.Count;
                            int total = dt.Rows.Cast<DataRow>()
                                        .Sum(t => Convert.ToInt32(t[2].ToString()));

                            int discount = dt.Rows.Cast<DataRow>()
                                        .Sum(t => Convert.ToInt32(t[3].ToString()));
                            object[] o = { count + 1, "Total", total, discount };
                            dt.Rows.Add(o);
                            grid_report.DataSource = dt;
                            updatemenu7();
                            this.PopulatePager((int)obj[0], index);



                            break;
                        }


                    case ReportMenu.ExpenseCashReceive://Menu 8
                        {
                            // pageSize = 18;
                            List<Object> obj = (List<object>)bal.p_expenseCashReceive(sdate, ldate,
                                index, pageSize);
                            if (obj == null)
                            {
                                return;
                            }
                            dt = (DataTable)obj[1];

                            int count = dt.Rows.Count;
                            grid_report.DataSource = dt;
                            updatemenu8();
                            this.PopulatePager((int)obj[0], index);
                            break;
                        }
                    case ReportMenu.BipariSales://Menu 9
                        {
                            //pageSize = 18;
                            List<Object> obj = (List<object>)bal.getSalesClient(sdate, ldate, index, pageSize, search);
                            dt = (DataTable)obj[1];
                            int count = dt.Rows.Count;
                            //int credit = dt.Rows.Cast<DataRow>()
                            //            .Sum(t => Convert.ToInt32(t[3].ToString()));

                            //int debit = dt.Rows.Cast<DataRow>()
                            //            .Sum(t => Convert.ToInt32(t[4].ToString()));
                            //object[] o = { count + 1, Resources.ResourceManager.GetString("a1053"), 0, credit, debit };
                            //dt.Rows.Add(o);
                            grid_report.DataSource = dt;
                            updatemenu9();
                            this.PopulatePager((int)obj[0], index);
                            break;
                        }
                    case ReportMenu.CustomerSale://Menu 10
                        {
                            if (size != "")
                                pageindex = int.Parse(size);
                            else
                                pageindex = 23;
                            index = 1;
                            List<Object> obj = (List<object>)bal.customersales(sdate, ldate,
                                index, pageSize, search);
                            dt = (DataTable)obj[1];
                            int count = dt.Rows.Count;
                            int chalan = dt.Rows.Cast<DataRow>().Sum(t => Convert.ToInt32(t[2].ToString()));
                            int quantity = dt.Rows.Cast<DataRow>().Sum(t => Convert.ToInt32(t[3].ToString()));
                            int saleamount = dt.Rows.Cast<DataRow>().Sum(t => Convert.ToInt32(t[4].ToString()));
                            float total = dt.Rows.Cast<DataRow>().Sum(t => float.Parse(t[5].ToString()));
                            float commission = dt.Rows.Cast<DataRow>().Sum(t => float.Parse(t[6].ToString()));
                            int chongi = dt.Rows.Cast<DataRow>().Sum(t => Convert.ToInt32(t[7].ToString()));

                            object[] o = { count + 1, "Total", chalan, quantity, saleamount, total, commission, chongi };
                            dt.Rows.Add(o);
                            grid_report.DataSource = dt;
                            updatemenu10();
                            this.PopulatePager((int)obj[0], index);



                            break;
                        }
                    case ReportMenu.BipariInvestment://Menu 11
                        {
                            //pageSize = 19;
                            List<Object> obj = (List<object>)new BLogic().searchProfile("", "SClient", "", index, pageSize);
                            dt = (DataTable)obj[1];
                            int count = dt.Rows.Count;
                            int total = dt.Rows.Cast<DataRow>()
                                        .Sum(t => Convert.ToInt32(t[5].ToString()));


                            object[] o = { count + 1, string.Empty, string.Empty, string.Empty, Resources.ResourceManager.GetString("a1053"), total };
                            dt.Rows.Add(o);
                            grid_report.DataSource = dt;
                            updatemenu11();
                            this.PopulatePager((int)obj[0], index);
                            break;
                        }




                    case ReportMenu.SERP://Menu 12
                        {
                            //pageSize = 18;
                            List<Object> obj = (List<object>)bal.p_dailyProfitSalesExpense("SERP", sdate, ldate,
                                index, pageSize);
                            dt = (DataTable)obj[1];
                            int count = dt.Rows.Count;

                            grid_report.DataSource = dt;
                            updatemenu12();
                            this.PopulatePager((int)obj[0], index);
                            break;
                        }
                    case ReportMenu.AugraiDiff://Menu 13
                        {
                            //pageSize = 18;

                            List<Object> obj = (List<object>)bal.p_AugraiDateDetail(sdate, ldate, index, pageSize);
                            dt = (DataTable)obj[1];
                            int count = dt.Rows.Count;

                            grid_report.DataSource = dt;
                            //updatemenu3();
                            break;
                        }
                    case ReportMenu.CustBillsandReceivings:
                        {
                            if(chk_date.Checked && sdate=="" && ldate=="")
                            {
                                sdate = date_start.Text;
                                ldate = date_last.Text;
                            }
                            List<object> obj = (List<object>)new BLReport().p_CustBillsandReceivings(sdate, ldate, txt_name.Text);
                            if (obj == null)
                            {
                                return;
                            }
                            dt = (DataTable)obj[1];


                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("No Bill Receiving.");
                                return;
                            }

                            grid_report.DataSource = dt;
                            break;
                        }
                    case ReportMenu.DetailReport://Menu 3
                        {
                            if (!chk_date.Checked)
                                return;
                            List<object> obj = (List<object>)new BLReport().p_DetailReport(date_start.Text, date_last.Text, search);
                            if (obj == null)
                            {
                                return;
                            }
                            dt = (DataTable)obj[1];


                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("No Bill Receiving.");
                                return;
                            }

                            grid_report.DataSource = dt;
                            updatemenu15();
                            break;
                        }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void changeColumnNameLocal(string col1, string col2, string col3
            , string col4, string col5, string col6, string col7,
            string col8, string col9, string col10, string col11)
        {
            DataTable t = dt;
            if (grid_report.Columns.Count >= 1)
                grid_report.Columns[0].HeaderText = col1;
            if (grid_report.Columns.Count >= 2)
                grid_report.Columns[1].HeaderText = col2;
            if (grid_report.Columns.Count >= 3)
                grid_report.Columns[2].HeaderText = col3;
            if (grid_report.Columns.Count >= 4)
                grid_report.Columns[3].HeaderText = col4;
            if (grid_report.Columns.Count >= 5)
                grid_report.Columns[4].HeaderText = col5;
            if (grid_report.Columns.Count >= 6)
                grid_report.Columns[5].HeaderText = col6;
            if (grid_report.Columns.Count >= 7)
                grid_report.Columns[6].HeaderText = col7;
            if (grid_report.Columns.Count >= 8)
                grid_report.Columns[7].HeaderText = col8;
            if (grid_report.Columns.Count >= 9)
                grid_report.Columns[8].HeaderText = col9;
            if (grid_report.Columns.Count >= 10)
                grid_report.Columns[9].HeaderText = col10;
            if (grid_report.Columns.Count >= 11)
                grid_report.Columns[10].HeaderText = col11;

        }



        private void addRowingrid_bipari(DataTable dt1)
        {
            /*DataTable dt = new DataTable();
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Amount/Percentage", typeof(int));
            int count = 0;
            DataRow dr = dt1.Rows[0];
            foreach (DataColumn dc in dt1.Columns)
            {

                dt.Rows.Add(new object[] { count++, dt1.Columns[count].ColumnName, dr[count].ToString() });

            }*/

            DataTable table = new DataTable();
            table.Columns.Add("#");  // first column
            table.Columns.Add("Description");  // first column
            table.Columns.Add("Amount");  // first column
            int count = 0;
            DataRow dr = dt1.Rows[0];
            foreach (DataColumn col in dt1.Columns)
            {
                DataRow nrow = table.NewRow();
                object[] o = { count + 1, col.ColumnName, dr[count].ToString() };
                nrow.ItemArray = o;

                table.Rows.Add(nrow);
                count++;
            }


            grid_report.DataSource = table;
        }

        /*public void loadGridData(int index, string sdate, string ldate, string search)
        {
            try
            {
                pageindex = index;

                switch (eMenu)
                {
                    case ReportMenu.BalanceSheetReport:
                        {
                            break;
                        }
                    case ReportMenu.BipariInvestment:
                        {
                            pageSize = 23;
                            break;
                        }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }*/





        int totalPage = 0;
        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan = 3;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(pageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            totalPage = pageCount;
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 3;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<<", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = ">>>", Value = pageCount.ToString() });
            }

            //Clear existing Pager Buttons.
            pnlPager.Controls.Clear();

            //Loop and add Buttons for Pager.
            int count = 0;
            foreach (Page page in pages)
            {
                Button btnPage = new Button();
                btnPage.Location = new System.Drawing.Point(38 * count, 5);
                btnPage.Size = new System.Drawing.Size(35, 20);
                btnPage.Name = page.Value;
                btnPage.Text = page.Text;
                btnPage.Enabled = !page.Selected;
                btnPage.Click += new System.EventHandler(this.Page_Click);
                pnlPager.Controls.Add(btnPage);
                count++;
            }



        }

        private void Page_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);

            loadGridData(int.Parse(btnPager.Name), startdate, lastdate, "");
        }

        public class Page
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }
        }

        #endregion



        private void btn_search_Click(object sender, EventArgs e)
        {
            /*if (chk_date.Checked)
            {
                startdate = date_start.Text;
                lastdate = date_last.Text;
                loadGridData(1, startdate, lastdate, txt_name.Text);
            }
            else
            {
                loadGridData(1, "", "", "");
            }*/
            string sdate = "";
            string ldate = "";
            string search = "";

            if (chk_date.Checked)
            {
                sdate = date_start.Text;
                ldate = date_last.Text;
            }

            if (chk_id.Checked)
            {
                search = txt_name.Text;
            }
            else if (chk_name.Checked)
            {
                search = txt_name.Text;

            }
            if (eMenu == ReportMenu.CustBillsandReceivings)
            {
                loadGridData(1, sdate, ldate, search);
                return;
            }
            loadGridData(1, sdate, ldate, search);

        }

        #region Controls Change Arrow


        private void chk_date_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_date.Checked)
            {
                //showChanges(false, false, false, false, false, false, false, false, false, false);
                switch (eMenu)
                {
                    case ReportMenu.CustomerSale | ReportMenu.CashReceived:
                        {
                            showChanges(true, true, false, false, false, false, true, true, true, true);
                            break;
                        }
                    default:
                        {
                            //showChanges(true, true, true, true, true, true, true, true, true, true);
                            break;
                        }
                }
            }





        }


        private void txt_name_TextChanged(object sender, EventArgs e)
        {
            /*string sdate = "";
            string ldate = "";
            string search = "";

            if (chk_date.Checked)
            {
                sdate = date_start.Text;
                ldate = date_last.Text;
            }

            if (chk_id.Checked)
            {
                search = txt_name.Text;
            }
            else if (chk_name.Checked)
            {
                search = txt_name.Text;
                
            }
            if (eMenu==ReportMenu.CustBillsandReceivings)
            {
                
                return;
            }
            loadGridData(1,sdate,ldate,search);*/
        }
        private string name = "";
        public void searchDialog(string searchTxt)
        {
            int choice = 0;
            int searchType = 0;
            if (eMenu == ReportMenu.ExpenseDetail)
            {
                return;
            }
            else if (eMenu == ReportMenu.BipariSales)
            {
                choice = 1;
                searchType = 1;
            }
            else
            {
                choice = 2;
            }
            using (Search search = new Search(choice, searchTxt, searchType))
            {
                DialogResult res = search.ShowDialog();
                txt_name.Text = search.Id;
                name = search.Name;
                search.Close();

                return;
            }
        }
        private void chk_khataid_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_id.Checked)
                chk_name.Checked = false;
        }

        private void btn_print_report_Click(object sender, EventArgs e)
        {
            //GridPrintDocument doc = new GridPrintDocument(this.grid_report,this.grid_report.Font, true);
            //doc.DocumentName = "Preview";
            AllReportsCC rp = null;
            DataTable mydt = dt;
            switch (eMenu)
            {
                case ReportMenu.BalanceSheetReport://Menu 2
                    {
                        //doc.DocumentName = "Balance Sheet";
                        /*updatemenu2();
                        rp = new AllReportsCC( mydt, ReportMenu.BalanceSheetReport,startdate,lastdate);

                        rp.ShowDialog();
                        updatemenu2();*/
                        loadGridData(-1, startdate, lastdate, "");
                        rp = new AllReportsCC();
                        DataRow cr = dt.Rows[0];
                        int acc_open = int.Parse(cr[3].ToString());
                        string datec = "";
                        if (chk_date.Checked)
                        {
                            datec = date_start.Text + " To " + date_last.Text;
                        }
                        else
                        {
                            datec = "ALL";
                        }
                        rp.ReportBSeet(mydt, datec);
                        rp.ShowDialog();

                        return;
                    }
                case ReportMenu.AugraiReport://Menu 3
                    {
                        updatemenu3();
                        rp = new AllReportsCC(mydt, ReportMenu.AugraiReport, startdate, lastdate);
                        rp.ShowDialog();
                        updatemenu3();
                        return;
                    }
                case ReportMenu.ProfitLoss://Menu 5
                    {
                        //doc.DocumentName = "Profit & Loss";
                        updatemenu5();
                        rp = new AllReportsCC(mydt, ReportMenu.ProfitLoss, startdate, lastdate);
                        rp.ShowDialog();
                        updatemenu5();
                        return;
                    }
                case ReportMenu.ExpenseDetail://Menu 6
                    {
                        //doc.DocumentName = "Expense";
                        updatemenu6();
                        rp = new AllReportsCC(mydt, ReportMenu.ExpenseDetail, startdate, lastdate);
                        rp.ShowDialog();
                        updatemenu6();
                        return;
                    }
                case ReportMenu.CashReceived://Menu 7
                    {
                        //doc.DocumentName = "Cash Receive";
                        updatemenu7();
                        rp = new AllReportsCC(mydt, ReportMenu.CashReceived, startdate, lastdate);
                        rp.ShowDialog();
                        updatemenu7();
                        return;
                    }

                case ReportMenu.ExpenseCashReceive://Menu 8
                    {
                        string sdate = "";
                        string ldate = "";
                        if (chk_date.Checked)
                        {
                            sdate = date_start.Text;
                            ldate = date_last.Text;
                        }
                        //updatemenu8();
                        // rp = new AllReportsCC( dt, ReportMenu.ExpenseCashReceive, startdate, lastdate);
                        //rp = new AllReportsCC();
                        //rp.ExpenseReceiving(dt, startdate, lastdate);
                        List<Object> exp = (List<object>)new BLReport().p_reporting_CRUD("ExpenseCash", sdate, ldate, 1, 18, "");
                        List<Object> rec = (List<object>)new BLReport().p_reporting_CRUD("ReceivingCash", sdate, ldate, 1, 18, "");
                        if (exp == null)
                        {
                            return;
                        }
                        DataTable dtexp = (DataTable)exp[1];
                        if (rec == null)
                        {
                            return;
                        }
                        DataTable dtrec = (DataTable)rec[1];
                        //AllReportsCC rp = new AllReportsCC(dt, ReportMenu.ExpenseCashReceive, date, date);
                        rp = new AllReportsCC();
                        //rp.ExpenseReceiving(dt, date, date);
                        DataRow dr = new BLogic().getLastCash(sdate, ldate);
                        int balance = int.Parse(dr[0].ToString() == "" ? "0" : dr[0].ToString());
                        int receivings = int.Parse(dr[1].ToString() == "" ? "0" : dr[1].ToString());
                        int expense = int.Parse(dr[2].ToString() == "" ? "0" : dr[2].ToString());
                        int currentBalance = int.Parse(dr[2].ToString() == "" ? "0" : dr[3].ToString());

                        rp.ExpenseRecSection(dtrec, dtexp, balance, receivings, expense, currentBalance);
                        rp.ShowDialog();

                        //updatemenu8();
                        return;
                    }
                case ReportMenu.BipariSales://Menu 9
                    {
                        //doc.DocumentName = "Bipari Sale";
                        updatemenu9();
                        rp = new AllReportsCC(dt, ReportMenu.BipariSales, startdate, lastdate);
                        rp.ShowDialog();
                        updatemenu9();

                        return;
                    }
                case ReportMenu.CustomerSale://Menu 10
                    {
                        //doc.DocumentName = "Customer Sale";
                        updatemenu10();
                        rp = new AllReportsCC(dt, ReportMenu.CustomerSale, startdate, lastdate);
                        rp.ShowDialog();
                        updatemenu10();
                        return;
                    }
                case ReportMenu.BipariInvestment://Menu 11
                    {
                        updatemenu11();
                        rp = new AllReportsCC(dt, ReportMenu.BipariInvestment, startdate, lastdate);
                        rp.ShowDialog();
                        updatemenu11();
                        return;
                    }

                case ReportMenu.SERP://Menu 12
                    {
                        updatemenu12();
                        rp = new AllReportsCC();
                        string sdate = "";
                        string ldate = "";
                        if (chk_date.Checked)
                        {
                            sdate = date_start.Text;
                            ldate = date_last.Text;
                        }

                        rp.printSRPSUM(sdate, ldate, 1, pageSize);
                        rp.ShowDialog();
                        updatemenu12();

                        return;
                    }
                case ReportMenu.AugraiDiff:
                    {

                        rp = new AllReportsCC();
                        rp.AugraiDetailinfo(dt);
                        rp.ShowDialog();
                        return;
                    }
                case ReportMenu.CustBillsandReceivings:
                    {
                        rp = new AllReportsCC();
                        DataRow cr = (dt.Rows.Count == 0) ? null : dt.Rows[0];
                        if (cr == null)
                            return;
                        int balanceR = int.Parse(cr[5].ToString());
                        int bill = int.Parse(cr[3].ToString());
                        int receiving = int.Parse(cr[4].ToString());

                        int initialBalance = balanceR + receiving - bill;

                        rp.BillandRecevings(null, dt, null, txt_name.Text, name, startdate, lastdate, initialBalance + "",5);
                        rp.ShowDialog();
                        break;
                    }
                case ReportMenu.DetailReport:
                    {
                        rp = new AllReportsCC();
                        DataRow cr = dt.Rows[0];
                        int acc_open = int.Parse(cr[3].ToString());
                        string datec = "";
                        if (chk_date.Checked)
                        {
                            datec = date_start.Text + " To " + date_last.Text;
                            startdate = date_start.Text;
                            lastdate = date_last.Text;
                        }
                        else
                        {
                            datec = "ALL";
                            startdate = "";
                            lastdate = "";
                            return;
                        }
                        DataTable dtprd = new BLogic().readFardHisab("AllProduct", "", startdate, lastdate);
                        rp.DetailReport(dt, dtprd, startdate, lastdate, acc_open, datec);
                        rp.ShowDialog();
                        break;
                    }




            }
            // dataGridView1 is the DataGridView to print 
            /*doc.DefaultPageSettings.Landscape = true;
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.ClientSize = new Size(400, 300);
            printPreviewDialog.Location = new Point(29, 29);
            printPreviewDialog.Name = "Print Preview Dialog";
            printPreviewDialog.UseAntiAlias = true;
            printPreviewDialog.Document = doc;
            printPreviewDialog.ShowDialog();
            doc.Dispose();
            doc = null;*/
        }



        private void btn_load_Click(object sender, EventArgs e)
        {
            btn_search_Click(this, new EventArgs());
        }

        private bool ProcessKey(Message msg, Keys keyData)
        {
            bool retval = false;

            if ((keyData & Keys.Escape) == Keys.Escape)
            {
                Control control = Control.FromChildHandle(msg.HWnd);
                retval = control.Name == this.Name;

                if (!retval)
                {
                    Control parentControl = control.Parent;
                    while (parentControl != null)
                    {
                        if (parentControl.Name == this.Name)
                        {
                            retval = true;
                            break;
                        }
                        parentControl = parentControl.Parent;
                    }
                }
            }
            else if (keyData == Keys.Left)
            {
                if (chk_date.Checked)
                {
                    startdate = date_start.Text;
                    lastdate = date_last.Text;
                }
                else
                {
                    startdate = "";
                    lastdate = "";
                }

                if (pageindex > 1)
                {
                    --pageindex;

                }
                loadGridData(pageindex, startdate, lastdate, "");

            }
            else if (keyData == Keys.Right)
            {
                if (chk_date.Checked)
                {
                    startdate = date_start.Text;
                    lastdate = date_last.Text;
                }
                else
                {
                    startdate = "";
                    lastdate = "";
                }
                if (pageindex < totalPage)
                {
                    ++pageindex;
                }
                loadGridData(pageindex, startdate, lastdate, "");
            }
            else if (keyData == Keys.ControlKey | keyData == Keys.P)
            {
                btn_print_report_Click(this, new EventArgs());
            }
            return retval;
        }
        #endregion
        private void chk_print_all_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_print_all.Checked)
            {
                dt = new BLogic().p_customer_CRUD("Augrai", "1", date_start.Text);
            }
            else
            {
                dt = new BLogic().p_customer_CRUD("Augrai", "0", date_start.Text);
            }
            grid_report.DataSource = dt;
            updatemenu3();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.Control | Keys.P:
                    {

                        btn_print_report_Click(this, new EventArgs());
                        return true;
                    }
                case Keys.Enter:
                    {
                        searchDialog(txt_name.Text);
                        loadGridData(1, "", "", txt_name.Text);
                        return true;
                    }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
