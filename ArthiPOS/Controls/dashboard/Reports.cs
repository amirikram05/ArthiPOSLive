using ArthiPOS.Properties;
using ArthiPOS.Reporting;
using CommonUtilities;
using DataMember;
using DataMember.memberlog;
using System;
using System.Windows.Forms;
using static ArthiPOS.Controls.dashboard.ReportControl;

namespace ArthiPOS.Controls.dashboard
{
    public partial class Reports : UserControl
    {
        ReportControl rp;
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            UpdateLocalization();
            //string conn=new BLogic().getLiveDB();
            DatabaseLog db = LogUtill.getDatabaseLog();


            if (db.DatabaseIs == "db_pt" && db.LocalCheck == 0)
            {
                comboBox1.SelectedIndex = 0;
                comboBox1.Text = "Testing";
            }
            else
            if (db.DatabaseIs == "live_db_pt" && db.LocalCheck == 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            if (db.DatabaseIs == "Local" && db.LocalCheck == 1)

            {
                comboBox1.SelectedIndex = 1;
            }
        }
        private void UpdateLocalization()
        {
            btn_menu1.LabelText = Resources.ResourceManager.GetString("a1054");
            btn_menu2.LabelText = Resources.ResourceManager.GetString("a1055");
            btn_menu3.LabelText = Resources.ResourceManager.GetString("a1056");
            //btn_menu4.LabelText = Resources.ResourceManager.GetString("a1057");
            btn_menu5.LabelText = Resources.ResourceManager.GetString("a1058");
            btn_menu6.LabelText = Resources.ResourceManager.GetString("a1059");
            btn_menu7.LabelText = Resources.ResourceManager.GetString("a1060");
            btn_menu8.LabelText = Resources.ResourceManager.GetString("a1200");
            btn_menu9.LabelText = Resources.ResourceManager.GetString("a1062");
            btn_menu10.LabelText = Resources.ResourceManager.GetString("a1063");
            btn_menu11.LabelText = Resources.ResourceManager.GetString("a1064");
            btn_menu12.LabelText = Resources.ResourceManager.GetString("a1088");
            btn_menu13.LabelText = Resources.ResourceManager.GetString("a2019");
            btn_menu14.LabelText = Resources.ResourceManager.GetString("a2018");
        }
        private void menu_admin_1_Click(object sender, EventArgs e)
        {
            /*rp = new ReportControl();

            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(true, true, false, false, false, false, false, false, true, true);
            rp.eMenu = ReportMenu.CreateSeasonReport;
            rp.updatemenu1();
            rp.Text = btn_menu1.LabelText;
            rp.ShowDialog();*/

            SeasonList sl = new SeasonList();
            sl.ShowDialog();

        }

        private void menu_admin_2_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();

            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(false, false, false, false, false, false, false, false, true, false);
            rp.eMenu = ReportMenu.BipariInvestment;
            rp.loadGridData(1, Admin.Date, Admin.Date, "");
            rp.updatemenu11();
            rp.Text = btn_menu2.LabelText;
            rp.ShowDialog();
        }

        private void menu_admin_3_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();

            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(true, true, true, true, true, true, true, true, true, true);
            rp.eMenu = ReportMenu.CashReceived;
            rp.loadGridData(1, Admin.Date, Admin.Date, "");
            rp.updatemenu7();
            rp.Text = btn_menu3.LabelText;
            rp.ShowDialog();
        }

        private void menu_admin_4_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(true, true, false, false, true, false, false, false, true, true);
            rp.eMenu = ReportMenu.ExpenseDetail;
            rp.loadGridData(1, Admin.Date, Admin.Date, "");
            rp.updatemenu6();
            rp.Text = btn_menu4.LabelText;
            rp.ShowDialog();
        }

        private void menu_admin_5_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(true, true, false, false, false, false, false, false, true, true);
            rp.eMenu = ReportMenu.ProfitLoss;
            rp.loadGridData(1, Admin.Date, Admin.Date, "");
            rp.updatemenu5();
            rp.Text = btn_menu5.LabelText;
            rp.ShowDialog();
        }

        private void menu_admin_6_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();
            rp.showChanges(true, true, false, false, false, false, false, false, true, true);
            rp.eMenu = ReportMenu.BalanceSheetReport;
            rp.loadGridData(1, Admin.Date, Admin.Date, "");
            rp.updatemenu2();
            rp.Text = btn_menu6.LabelText;
            rp.ShowDialog();
        }

        private void menu_admin_8_Click(object sender, EventArgs e)
        {
            //rp = new ReportControl();
            //rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            //rp.showChanges(true, true, false, false, false, false, false, false, true, true);
            //rp.eMenu = ReportMenu.AugraiReport;
            //rp.loadGridData(1, Admin.Date, Admin.Date,"");
            //rp.updatemenu3();
            //rp.Text = btn_menu8.LabelText;
            //rp.ShowDialog();
            RepAugraiNewF rep = new RepAugraiNewF();
            rep.ShowDialog();
        }

        private void menu_admin_9_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(true, true, false, false, false, false, false, false, true, true);
            //rp.eMenu = ReportMenu.InvestmentRecovery;
            rp.updatemenu4();
            // rp.Text = btn_menu9.LabelText;
            ReportForAll a = new ReportForAll();
            a.ShowDialog();
        }

        private void menu_bipari_1_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            //rp.showChanges(true, true, false, false, false, false, false, false, true, true);
            rp.showChanges(true, true, true, true, true, true, true, true, true, true);
            rp.eMenu = ReportMenu.BipariSales;
            rp.loadGridData(1, Admin.Date, Admin.Date, "");
            rp.updatemenu9();
            rp.Text = btn_menu10.LabelText;
            rp.ShowDialog();
        }

        private void menu_bipari_2_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();
            rp.eMenu = ReportMenu.BipariInvestment;
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
        }

        private void menu_bipari_3_Click(object sender, EventArgs e)
        {
            rp.eMenu = ReportMenu.BipariList;
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
        }

        private void menu_bipari_4_Click(object sender, EventArgs e)
        {
            rp.eMenu = ReportMenu.BipariProfit;
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(true, true, false, false, false, false, false, false, true, true);
        }

        private void menu_customer_3_Click(object sender, EventArgs e)
        {
            rp.eMenu = ReportMenu.BipariProfit;
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(true, true, false, false, false, false, false, false, true, true);
        }

        private void menu_customer_2_Click(object sender, EventArgs e)
        {
            rp.eMenu = ReportMenu.CustomerList;
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(true, true, true, true, true, true, true, true, true, true);
        }

        private void menu_customer_1_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();
            rp.eMenu = ReportMenu.CustomerSale;
            rp.showChanges(true, true, true, true, true, true, true, true, true, true);
            rp.loadGridData(1, Admin.Date, Admin.Date, "");
            rp.updatemenu10();
            rp.Text = btn_menu10.LabelText;
            rp.ShowDialog();
        }

        private void btn_menu8_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();
            rp.showChanges(true, true, false, false, false, false, false, false, true, true);
            rp.eMenu = ReportMenu.ExpenseCashReceive;
            rp.loadGridData(1, Admin.Date, Admin.Date, "");
            rp.updatemenu8();
            rp.Text = btn_menu8.LabelText;
            rp.ShowDialog();
        }

        private void btn_menu12_Click(object sender, EventArgs e)
        {
            //rp = new ReportControl();
            //rp.showChanges(true, true, false, false, false, false, false, false, true, true);
            //rp.eMenu = ReportMenu.SERP;
            //rp.loadGridData(1, Admin.Date, Admin.Date,"");
            //rp.updatemenu12();
            //rp.Text = btn_menu12.LabelText;
            //rp.ShowDialog();
            rp = new ReportControl();
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(true, true, false, false, false, false, false, false, true, true);
            rp.eMenu = ReportMenu.DetailReport;
            rp.loadGridData(1, Admin.Date, Admin.Date, "");
            rp.updatemenu15();
            rp.Text = btn_menu16.LabelText;
            rp.ShowDialog();
        }

        private void btn_menu13_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();
            rp.showChanges(true, true, false, false, false, false, false, false, true, true);
            rp.eMenu = ReportMenu.AugraiDiff;
            rp.loadGridData(1, Admin.Date, Admin.Date, "");
            //rp.updatemenu13();
            rp.Text = btn_menu8.LabelText;
            rp.ShowDialog();
        }

        private void btn_cust_recBill_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(true, true, true, true, true, true, true, true, true, true);
            rp.eMenu = ReportMenu.CustBillsandReceivings;
            rp.loadGridData(1, "", "", "1");
            rp.updatemenu14();
            rp.Text = btn_menu8.LabelText;
            rp.ShowDialog();
        }

        private void btn_menu15_Click(object sender, EventArgs e)
        {
            rp = new ReportControl();
            rp.showChanges(false, false, false, false, false, false, false, false, false, false);
            rp.showChanges(true, true, false, false, false, false, false, false, true, true);
            rp.eMenu = ReportMenu.DetailReport;
            rp.loadGridData(1, Admin.Date, Admin.Date, "");
            rp.updatemenu15();
            rp.Text = btn_menu16.LabelText;
            rp.ShowDialog();
        }

        private void btn_menu16_Click(object sender, EventArgs e)
        {
            ReportingData rp = new ReportingData();
            rp.ShowDialog();
        }

        private void btn_menu17_Click(object sender, EventArgs e)
        {
            ReportFardHisab fh = new ReportFardHisab();
            fh.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseLog db = LogUtill.getDatabaseLog();
            string conName = db.connectionName;
            string servername = db.ServerName;
            string uname = db.UserName;
            string password = db.Password;
            string livedb = db.LiveDB;
            string backup = db.Backupdb;
            string test_db = db.Testing_Database;
            string dbname = livedb;
            string localdb = db.LocalDB;
            string currentDB = RegistryAccess.GetStringRegistryValue("DBStatus", "");
            if (currentDB == "Testing")
            {
                dbname = test_db;
            }
            else
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    if (db.DatabaseIs == "Local" || db.LocalCheck == 1)
                    {
                        dbname = "Local";
                    }
                    else
                    {
                        dbname = livedb;
                    }
                }
                else
                {
                    dbname = backup;
                }
            }


            LogUtill.loadDBConfig(servername, uname, password, livedb, backup, conName, test_db, dbname, localdb, db.LocalCheck);
            DatabaseLog dbx = LogUtill.getDatabaseLog();
            RegistryAccess.SetStringRegistryValue("DBStatus", dbx.Status);
            RegistryAccess.SetStringRegistryValue("DBString", dbx.connectionName);



        }

        private void btn_menu18_Click(object sender, EventArgs e)
        {
            ReportLedgerForm rl = new ReportLedgerForm();
            rl.ShowDialog();
        }

        private void btn_menu19_Click(object sender, EventArgs e)
        {
            ReportBalanceSheet b = new ReportBalanceSheet();
            b.ShowDialog();
        }

        private void btn_menu20_Click(object sender, EventArgs e)
        {
            ReportAccounts r = new ReportAccounts();
            r.ShowDialog();
        }
    }
}
