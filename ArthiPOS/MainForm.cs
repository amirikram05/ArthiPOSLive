using System;
using System.Windows.Forms;
using ArthiPOS.controls.dashboard;
using ArthiPOS.utill;
using ArthiPOS.Reporting;
using DataMember;
using ArthiPOS.Controls.dashboard;
using ArthiPOS.callback;
using ArthiPOS.Properties;
using BAL;
using System.Data;
using ArthiPOS.Utill;
using System.IO;
using ArthiPOS.shop;
using System.Threading;
using System.Globalization;
using ArthiPOS.Controls.test;
using CommonUtilities;
using ArthiPOS.Controls;
using DataMember.memberlog;
using System.Diagnostics;

namespace ArthiPOS.controls
{
    public partial class MainForm : Form,UIUpdate
    {
       
        enum MainMenu
        {
            DASHBOARD,
            TRANSPORT,
            POS,
            INVOICING,
            BILLING,
            AUGRAI,
            CASHINOUT,
            PROFILE,
            REPORT,
            ADMIN,SETTING,BillPaidOut
        }
        enum LocalData
        {
            Default,
            Local
        }
        private MainMenu menu=MainMenu.DASHBOARD;
        private LocalData menuLocal = LocalData.Default;

        public MainForm()
        {
            InitializeComponent();
            SetUIChanges();


        }

        public MainForm(int v)
        {
            InitializeComponent();

            
        }

        UserControl userc=new Dashboard1();
        #region Navigation
        private void nav_menu_Click(object sender, EventArgs e)
        {
            //navigationFadeTransition1.HideAsyc(this, true);


            if (!nav_menu_left.Visible)
            {
                thide.ShowSync(nav_menu_left);
            }
            else
            {
                tshow.HideSync(nav_menu_left);
            }


        }

        #endregion


        #region Close and Help
        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            panel_info.Controls.Clear();
            /*try
            {
                new EncrypDecrypt().EncryptDatabase(RegistryAccess.GetStringRegistryValue(Const.REGKEY, ""));
            }
            catch (IOException ex)
            {

            }*/

            //CommonUtill.IsFileinUse();
            Environment.Exit(0);
            navigationFadeTransition1.HideAsyc(this, true);

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            controls.Help help = new controls.Help();
            help.ShowDialog();
        }
        #endregion


        #region Menu
        private void MainForm_Load(object sender, EventArgs e)
        {
            Admin.Date = today_date.Text;
            shop.ParentForm.body_panel = panel_info;
            shop.ParentForm.title = lbl_header_title;
            shop.ParentForm.database = lbl_dbname;

            string lang = RegistryAccess.GetStringRegistryValue("Language", "en-US");
            if (lang == "en-US")
            {
                combo_lang.SelectedIndex = 0;
            }
            else
            {
                combo_lang.SelectedIndex = 1;
            }

            if (LogUtill.LoginCount>0 || true)
            {
                Dashboard1 MainForm = new Dashboard1();
                lbl_header_title.Text = Resources.ResourceManager.GetString("a1014");
                panel_info.Controls.Add(MainForm);
            }else
            {

                panel_info.Controls.Clear();
                AdminProfile ledger = new AdminProfile(this);
                lbl_header_title.Text = Resources.ResourceManager.GetString("a1015");
                panel_info.Controls.Add(ledger);
            }
            string conn = new BLogic().getLiveDB();
            if (conn == "c")
            {
                lbl_dbname.Text = "Testing";
            }
            else
            if (conn == "liveConn")
            {
                lbl_dbname.Text = "Live";
            }
            else
            {
                lbl_dbname.Text = conn;
            }

        }
        private Thread cashinout=null,augrai=null,updoc=null,sales=null;
        private void vmenu_ledger_Click(object sender, EventArgs e)
        {
            //panel_info.Controls.Clear();
            //menu = MainMenu.CASHINOUT;
            //CashFlow ledger = new CashFlow();
            //userc = ledger;
            new BLogic().getLiveDB();
            lbl_header_title.Text = Resources.ResourceManager.GetString("a1005");
            //panel_info.Controls.Add(ledger);
            if (cashinout != null && cashinout.IsAlive)
                return;
            else
            {
                cashinout = new Thread(new ThreadStart(CashInoutm));
                cashinout.Name = "cashinout";
                cashinout.Start();
            }
        }
        private void CashInoutm()
        {
            CashInout ledger = new CashInout();
            ledger.ShowDialog();
        }
        private void UpDocument()
        {
            GDriveUploadDoc g = new GDriveUploadDoc();
            g.ShowDialog();
        }
        private void menu_account_profile_Click(object sender, EventArgs e)
        {
            panel_info.Controls.Clear();
            new BLogic().getLiveDB();
            menu = MainMenu.ADMIN;
            AdminProfile ledger = new AdminProfile(this);
            //userc = ledger;
            lbl_header_title.Text = Resources.ResourceManager.GetString("a1015");
            panel_info.Controls.Add(ledger);
        }
        private void vmenu_profile_Click(object sender, EventArgs e)
        {
            panel_info.Controls.Clear();
            menu = MainMenu.PROFILE;
            new BLogic().getLiveDB();
            Profiles pro = new Profiles();
            userc = pro;
            lbl_header_title.Text = Resources.ResourceManager.GetString("a1006");
            panel_info.Controls.Add(pro);
        }
        private void Vmenu_augrai_Click(object sender, EventArgs e)
        {
            //menu = MainMenu.AUGRAI;
            //panel_info.Controls.Clear();
            //menu = MainMenu.AUGRAI;
            //DataTable dt = new BLogic().p_customer_CRUD("Augrai", "1", Admin.Date);

            //RepAugrai rep = new RepAugrai(dt);
            //userc = rep;

            //lbl_header_title.Text = Resources.ResourceManager.GetString("a1004");
            new BLogic().getLiveDB();
            if (augrai != null && augrai.IsAlive)
                return;
            else
            {
                augrai = new Thread(new ThreadStart(Augrai));
                augrai.Name = "Augrai";
                augrai.Start();
            }
            //panel_info.Controls.Add(rep);
        }
        private void Augrai()
        {
            RepAugraiNewF rep = new RepAugraiNewF();
            rep.ShowDialog();
        }
        private void callSales()
        {
            SalesNew s = new SalesNew();
            s.ShowDialog();
        }


        private void btn_bill_account_Click(object sender, EventArgs e)
        {
            return;
            
        }

        private void btn_invoicing_Click(object sender, EventArgs e)
        {
            panel_info.Controls.Clear();
            menu = MainMenu.INVOICING;
            new BLogic().getLiveDB();
            dashboard.InvoicingPage invoice = new dashboard.InvoicingPage();
            userc = invoice;
            //sales.Left = flowLayoutPanel1.Left;
            lbl_header_title.Text = Resources.ResourceManager.GetString("a1016");
            panel_info.Controls.Add(invoice);
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            panel_info.Controls.Clear();
            menu = MainMenu.DASHBOARD;
            new BLogic().getLiveDB();
            //AlertMsg.Show("Hello WOrld", AlertMsg.AlertType.error);
            Dashboard1 MainForm = new Dashboard1();
            userc = MainForm;

            //sales.Left = flowLayoutPanel1.Left;
            lbl_header_title.Text = Resources.ResourceManager.GetString("a1014");
            panel_info.Controls.Add(MainForm);
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            menu = MainMenu.POS;
            lbl_header_title.Text = Resources.ResourceManager.GetString("a1001");
            //panel_info.Controls.Clear();
            //SalesNew sales = new SalesNew();
            //userc = sales;
            //panel_info.Controls.Add(sales);
            new BLogic().getLiveDB();
            if (sales != null && sales.IsAlive)
                return;
            else
            {
                sales = new Thread(new ThreadStart(callSales));
                sales.Name = lbl_header_title.Text;
                sales.Start();
            }
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            return;
        }
        #endregion

        #region Admin
        private void Menu_settings_Click(object sender, EventArgs e)
        {
            AddConfig cnf = new AddConfig(true);
            cnf.ShowDialog();
            DatabaseLog db = LogUtill.getDatabaseLog();
            lbl_dbname.Text = db.Status;
            RegistryAccess.SetStringRegistryValue("Database", db.Status);
            this.UpdateMainFormUI();
            UpdateMenu();
        }
        private void vmenu_reports_Click(object sender, EventArgs e)
        {
            panel_info.Controls.Clear();
            //ReportControl rep = new ReportControl();
            new BLogic().getLiveDB();
            menu = MainMenu.REPORT;
            Reports rep = new Reports();
            userc = rep;
            lbl_header_title.Text = Resources.ResourceManager.GetString("a1007");
            panel_info.Controls.Add(rep);
        }
        private void signout_bunifuFlat_Click(object sender, EventArgs e)
        {
            //LogUtill.loadLastUseInputs_AccountForm("", "", "", "", "", "", 0,"","","","");
            Authentication.Account = null;
            this.Hide();
            Authentication sistema = new Authentication();
            sistema.ShowDialog();
            this.Close();
        }
        #endregion


        #region Localization

        private void SetUIChanges()
       {
            vmenu_dashboard.Text = Resources.ResourceManager.GetString("a1014");
            vmenu_pos.Text = Resources.ResourceManager.GetString("a1001");
            vmenu_invoicing.Text = Resources.ResourceManager.GetString("a1016");
            vmenu_augrai.Text = Resources.ResourceManager.GetString("a1004");
            vmenu_ledger.Text = Resources.ResourceManager.GetString("a1005");
            vmenu_profile.Text = Resources.ResourceManager.GetString("a1006");
            vmenu_reports.Text = Resources.ResourceManager.GetString("a1007");
            menu_settings.Text = Resources.ResourceManager.GetString("a1008");
            menu_account_profile.Text = Resources.ResourceManager.GetString("a1015");
       }


        #endregion


        #region UpdateMainFormUI
        public void UpdateMainFormUI()
        {
            SetUIChanges();
        }
        public void UpdateMenu()
        {
            DatabaseLog db = LogUtill.getDatabaseLog();
            lbl_dbname.Text = db.Status;
            switch (menu)
                        {
                            case MainMenu.ADMIN: { menu_account_profile_Click(this, new EventArgs()); break; }
                            case MainMenu.AUGRAI: { Vmenu_augrai_Click(this, new EventArgs()); break; }
                            case MainMenu.BILLING: { btn_bill_account_Click(this, new EventArgs()); break; }
                            case MainMenu.CASHINOUT: { vmenu_ledger_Click(this, new EventArgs()); break; }
                            case MainMenu.DASHBOARD: { bunifuFlatButton2_Click(this, new EventArgs()); break; }
                            case MainMenu.INVOICING: { btn_invoicing_Click(this, new EventArgs()); break; }
                            case MainMenu.POS: { bunifuFlatButton3_Click(this, new EventArgs()); break; }
                            case MainMenu.PROFILE: { vmenu_profile_Click(this, new EventArgs()); break; }
                            case MainMenu.REPORT: { vmenu_reports_Click(this, new EventArgs()); break; }
                            case MainMenu.SETTING: { Menu_settings_Click(this, new EventArgs()); break; }
                            case MainMenu.TRANSPORT: { bunifuFlatButton6_Click(this, new EventArgs()); break; }
                        }
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            /*try
            {
                new EncrypDecrypt().EncryptDatabase(RegistryAccess.GetStringRegistryValue(Const.REGKEY, ""));
            }
            catch (IOException ex)
            {

            }*/
        }

        private void combo_lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeLanguage();
        }
        private void changeLanguage()
        {
            string lang = "en-US";


            if (combo_lang.SelectedIndex == 0)
            {
                lang = "en-US";
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");



                LogUtill.loadLastLanguage(lang);
                RegistryAccess.SetStringRegistryValue("Language", lang);
                this.UpdateMainFormUI();
                UpdateMenu();
            }
            else
            {
                lang = "ur-PK";
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ur-PK");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ur-PK");

                LogUtill.loadLastLanguage(lang);
                RegistryAccess.SetStringRegistryValue("Language", lang);
                this.UpdateMainFormUI();
                UpdateMenu();
            }

        }

        private void menu_settings_Click_1(object sender, EventArgs e)
        {
            AddConfig ad = new AddConfig(true);
            ad.ShowDialog();
            DatabaseLog db = LogUtill.getDatabaseLog();
            lbl_dbname.Text = db.Status;
            RegistryAccess.SetStringRegistryValue("Database", db.Status);
            UpdateMenu();


        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                
                case Keys.Control | Keys.D0: { menu_account_profile_Click(this,new EventArgs()); return true; }
                case Keys.Control | Keys.D1: { bunifuFlatButton2_Click(this,new EventArgs()); return true; }
                case Keys.Control | Keys.D2: { bunifuFlatButton3_Click(this,new EventArgs()); return true; }
                case Keys.Control | Keys.D3: { btn_invoicing_Click(this,new EventArgs()); return true; }
                case Keys.Control | Keys.D4: { Vmenu_augrai_Click(this,new EventArgs()); return true; }
                case Keys.Control | Keys.D5: { vmenu_ledger_Click(this,new EventArgs()); return true; }
                case Keys.Control | Keys.D6: { vmenu_profile_Click(this,new EventArgs()); return true; }
                case Keys.Control | Keys.D7: { vmenu_reports_Click(this,new EventArgs()); return true; }



            }



            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void today_date_ValueChanged(object sender, EventArgs e)
        {
            Admin.Date = today_date.Text;
        }

        private void google_btn_drive_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /*if (updoc != null && updoc.IsAlive)
                return;
            else
            {
                updoc = new Thread(new ThreadStart(UpDocument));
                updoc.Name = "UpDocument";
                updoc.Start();
            }*/
            UpDocument();
        }

        private void vmenu_billpaidout_Click(object sender, EventArgs e)
        {
            new BLogic().getLiveDB();
            BillPaidOut rep = new BillPaidOut();
            rep.ShowDialog();
        }
    }
}
