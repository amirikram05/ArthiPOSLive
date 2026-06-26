using ArthiPOS.callback;
using ArthiPOS.controls.dashboard;
using ArthiPOS.Controls;
using ArthiPOS.Controls.dashboard;
using ArthiPOS.Controls.test;
using ArthiPOS.newmenu;
using ArthiPOS.Properties;
using ArthiPOS.Rent;
using ArthiPOS.Reporting;
using ArthiPOS.Utill;
using BAL;
using CommonUtilities;
using DataMember;
using DataMember.memberlog;
using MetroFramework.Properties;
using ShopRentManagementSystem;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Resources = ArthiPOS.Properties.Resources;

namespace ArthiPOS.controls
{
    public partial class MainForm : Form, UIUpdate
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
            ADMIN, SETTING, BillPaidOut, RentMangement
        }
        enum LocalData
        {
            Default,
            Local
        }
        private MainMenu menu = MainMenu.DASHBOARD;
        private LocalData menuLocal = LocalData.Default;
        public NotificationPanel notificationPanel=null;
        // Add this with your other button declarations
        public MainForm()
        {
            InitializeComponent();
            SetUIChanges();
           

        }

        public MainForm(int v)
        {
            InitializeComponent();


        }

        UserControl userc = new Dashboard1();
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
            var result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Save any pending data
                backupdb();
                Environment.Exit(0);
            }

        }
        private void backupdb()
        {
            try
            {

                DialogResult dialogResult = MessageBox.Show("Do you Want To Take Backup?(Y/N)", "Backup Database", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    AdminLog adminLog = LogUtill.getAdminInputLog();
                    DatabaseLog db = LogUtill.getDatabaseLog();

                    if (new BLogic().backupDb(adminLog.BackupPath, db.LocalCheck))
                    {
                        MessageBox.Show(Resources.ResourceManager.GetString("a1084"));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
            }
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
            

            if (LogUtill.LoginCount > 0 || true)
            {
                Dashboard1 MainForm = new Dashboard1();
                lbl_header_title.Text = Resources.ResourceManager.GetString("a1014");
                panel_info.Controls.Add(MainForm);
            }
            else
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
        private void btnDropdownMenu_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            dropdownMenu.Show(btn, new System.Drawing.Point(0, btn.Height));
        }
        private Thread cashinout = null, augrai = null, updoc = null, sales = null, reportdata = null;
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
            UpdateControlLanguage(ledger);
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
            pro.Dock = DockStyle.Fill;
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
            //Augrai();
        }
        private void ReportData()
        {
            ReportAllData rp = new ReportAllData();
            UpdateControlLanguage(rp);
            rp.ShowDialog();
        }
        private void Augrai()
        {
            RepAugraiNewF rep = new RepAugraiNewF();
            UpdateControlLanguage(rep);
            rep.ShowDialog();
        }
        private void callSales()
        {
            SalesNew s = new SalesNew();
            UpdateControlLanguage(s);
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
            invoice.Dock = DockStyle.Fill;
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
            MainForm.Dock = DockStyle.Fill;

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
        private void UpdateControlLanguage(Control parentControl)
        {
            foreach (Control ctrl in parentControl.Controls)
            {
                if (ctrl is Label)
                {
                    // Assuming the control's name corresponds to a key in your resource file
                    string resourceKey = ctrl.Name;
                    string localizedText = Resources.ResourceManager.GetString(resourceKey);

                    if (!string.IsNullOrEmpty(localizedText))
                        ctrl.Text = localizedText;
                }
                else if (ctrl.Controls.Count > 0)
                {
                    // Recursively update nested controls
                    UpdateControlLanguage(ctrl);
                }
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
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Authentication.Account = null;
                this.Hide();

                Authentication authForm = new Authentication();
                authForm.ShowDialog();

                this.Close();
            }
        }
        #endregion
        private void SetActiveMenu(Bunifu.Framework.UI.BunifuFlatButton activeButton)
        {
            // Reset all buttons
            foreach (Control control in panel2.Controls)
            {
                if (control is Bunifu.Framework.UI.BunifuFlatButton button)
                {
                    button.Normalcolor = Color.Transparent;
                    button.OnHovercolor = Color.FromArgb(60, 60, 60);
                    button.Textcolor = Color.White;
                }
            }

            // Set active button
            if (activeButton != null)
            {
                activeButton.Normalcolor = Color.FromArgb(0, 122, 204);
                activeButton.OnHovercolor = Color.FromArgb(0, 122, 204);
                activeButton.Textcolor = Color.White;
            }
        }

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

                case Keys.Control | Keys.D0: { menu_account_profile_Click(this, new EventArgs()); return true; }
                case Keys.Control | Keys.D1: { bunifuFlatButton2_Click(this, new EventArgs()); return true; }
                case Keys.Control | Keys.D2: { bunifuFlatButton3_Click(this, new EventArgs()); return true; }
                case Keys.Control | Keys.D3: { btn_invoicing_Click(this, new EventArgs()); return true; }
                case Keys.Control | Keys.D4: { Vmenu_augrai_Click(this, new EventArgs()); return true; }
                case Keys.Control | Keys.D5: { vmenu_ledger_Click(this, new EventArgs()); return true; }
                case Keys.Control | Keys.D6: { vmenu_profile_Click(this, new EventArgs()); return true; }
                case Keys.Control | Keys.D7: { vmenu_reports_Click(this, new EventArgs()); return true; }



            }



            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void lbl_aboutinfo_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {

        }



        private void link_shopdaily_Click(object sender, EventArgs e)
        {
            DailySales d = new DailySales(Admin.Date);
            d.ShowDialog();
        }

        private void btn_log_Click(object sender, EventArgs e)
        {
            LogExecMangeForm log = new LogExecMangeForm();
            log.ShowDialog();
        }

        private void btn_editor_Click(object sender, EventArgs e)
        {
            FrmUrduDocumentEditor f = new FrmUrduDocumentEditor();
            f.ShowDialog();
        }

        private void vmenu_rent_Click(object sender, EventArgs e)
        {
            panel_info.Controls.Clear();
            menu = MainMenu.RentMangement;
            new BLogic().getLiveDB();
            FrmMain f = new FrmMain();
            //TestFormMain f = new TestFormMain();
            f.ShowDialog();
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            ShowNotifications();
        }
        private void ShowNotifications()
        {
            // Show notifications panel
            if(notificationPanel==null)
                notificationPanel = new NotificationPanel();
            notificationPanel.Show();
        }

        private void google_btn_drive_LinkClicked(object sender, EventArgs e)
        {
            UpDocument();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string lang = "en-US";
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");



                LogUtill.loadLastLanguage(lang);
                RegistryAccess.SetStringRegistryValue("Language", lang);
                this.UpdateMainFormUI();
                UpdateMenu();
        }

        private void urduToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string lang = "ur-PK";
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ur-PK");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ur-PK");

            LogUtill.loadLastLanguage(lang);
            RegistryAccess.SetStringRegistryValue("Language", lang);
            this.UpdateMainFormUI();
            UpdateMenu();
        }

        private void link_report_Click(object sender, EventArgs e)
        {

            new BLogic().getLiveDB();
            if (reportdata != null && reportdata.IsAlive)
                return;
            else
            {
                reportdata = new Thread(new ThreadStart(ReportData));
                reportdata.Name = "Reporting";
                reportdata.Start();
            }
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