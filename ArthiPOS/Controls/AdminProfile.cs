using ArthiPOS.Controls.dashboard;
using ArthiPOS.Properties;
using ArthiPOS.Reporting;
using ArthiPOS.utill;
using ArthiPOS.Utill;
using BAL;
using CommonUtilities;
using DataMember;
using DataMember.memberlog;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ArthiPOS.controls
{
    public partial class AdminProfile : UserControl
    {

        BLogic bal;
        Account account;
        AdminLog adminLog;
        SaleParser saleParser;

        public AdminProfile(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            //txt_cash.Focus();

            Admin.Date = mainForm.today_date.Text;
            initProfile();
        }
        public AdminProfile()
        {
            InitializeComponent();
            Admin.Date = mainForm.today_date.Text;
            initProfile();
        }


        //private void btn_add_capital_cash_Click(object sender, EventArgs e)
        //{
        //    string cash = txt_cash.Text;
        //    string password = txt_password.Text;
        //    string desc = txt_desc.Text;
        //    string key= new BLogic().p_getInvoiceID("Other", "0", date_combo.Text);
        //    if (cash!="" && password!="" && account.api_key!="")
        //    {
        //        bal.addTodaySales(date_combo.Text);
        //        bal.p_insert_CapitalCash(date_combo.Text, password, cash,
        //            key, nameof(BillKey.EnumUser.Admin), nameof(BillKey.EnumUser.Admin));
        //        bal.update_today_sales(date_combo.Text);
        //        refresh(account.api_key);
        //    }

        //}

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                //case Keys.Up:
                //    selectUpRow(dg_backupdetails);
                //    return true;
                //case Keys.Down:
                //    selectDownRow(dg_backupdetails);
                //    return true;
                case Keys.Control | Keys.Enter:
                    //btn_add_capital_cash_Click(this, new EventArgs());
                    return true;
                case Keys.Control | Keys.Delete:
                    //grid_cash_CellContentClick(this, new DataGridViewCellEventArgs(0, currentrow));
                    return true;
                    //case Keys.Enter:
                    //    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        

        

        


        public void initProfile()
        {
            this.bal = new BLogic();
            account = Authentication.Account;
            adminLog = LogUtill.getAdminInputLog();

            initForm(account);



            string lang = RegistryAccess.GetStringRegistryValue("Language", "en-US");
            string db = RegistryAccess.GetStringRegistryValue("Database", "Live");
            lbl_link_path.Text = adminLog.SalesInProccessedFolder
                   + "\n" + adminLog.SaleProcessedDir
                   + "\n" + adminLog.BackupPath;

            if (lang == "en-US")
                combo_lang.SelectedIndex = 0;
            else
                combo_lang.SelectedIndex = 1;

            DatabaseLog dblog = LogUtill.getDatabaseLog();


            if (dblog.Status == "Live")
                chk_db.SelectedIndex = 0;
            else
                chk_db.SelectedIndex = 1;



            Account ac = new Account();
            ac.api_key = account.api_key;
            Account acc = bal.check_User(ac);
            lbl_name.Text = acc.username;
            lbl_email.Text = acc.email;
            lbl_address.Text = acc.address;
            lbl_license.Text = acc.license_no;
            lbl_license_exp_date.Text = acc.license_exp_date;
            lbl_registration_id.Text = acc.api_key;
            lbl_reg_expiry_date.Text = acc.api_key_exp_date;
            lbl_proprietors.Text = acc.propriters_name;
            lbl_phone.Text = acc.phone;
            lbl_shop_name.Text = acc.shop_name;
            lbl_business_type.Text = acc.business_type;
            lbl_trade_mark.Text = acc.trade_mark;
            if (acc.local == "1")
            {
                chk_savelocal.CheckState = CheckState.Checked;
            }
            else
            {
                chk_savelocal.CheckState = CheckState.Unchecked;
            }
            if (acc.accountclosing == "0")
            {
                chk_accClosing.Checked = false;
            }
            else
            {
                chk_accClosing.Checked = true;
            }
            lbl_path_default.Text = getPath();
            //chk_security.Checked = RegistryAccess.GetStringSecurityValue(Const.SECURITYKEY, false);


        }


        private void AdminProfile_Load(object sender, EventArgs e)
        {

        }
        private void initForm(Account a)
        {
            Account acc = bal.check_User(a);
            if (acc != null)
            {
                lbl_name.Text = account.username;
            }
        }
        

        private void btn_balcesheet_Click(object sender, EventArgs e)
        {
            CustomerAugrai cu = new CustomerAugrai();
            cu.xlbl_date.Text = Admin.Date;
            AugraiDetailReport myForm = new AugraiDetailReport(cu);
            myForm.TopLevel = true;
            myForm.ShowInTaskbar = false;
            myForm.ShowDialog();
        }
        //public string strResourcesPath = Application.StartupPath + "/Properties";
        private MainForm mainForm;




        private void Combo_lang_SelectedIndexChanged(object sender, EventArgs e)
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
                if (this.mainForm != null) this.mainForm.UpdateMainFormUI();
            }
            else
            {
                lang = "ur-PK";
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ur-PK");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ur-PK");

                LogUtill.loadLastLanguage(lang);
                RegistryAccess.SetStringRegistryValue("Language", lang);
                if (this.mainForm != null) this.mainForm.UpdateMainFormUI();
            }

        }

        private void changeDatabase()
        {
            string db = "Live";

            DatabaseLog dblog = LogUtill.getDatabaseLog();

            if (chk_db.SelectedIndex == 0)
            {
                db = "Live";
                dblog.DatabaseIs = dblog.LiveDB;
                LogUtill.loadDatabase(dblog);
                mainForm.lbl_dbname.Text = db;
                RegistryAccess.SetStringRegistryValue("Database", dblog.LiveDB);
                this.mainForm.UpdateMainFormUI();
            }
            else
            {
                db = "Testing";

                dblog.DatabaseIs = dblog.Testing_Database;
                LogUtill.loadDatabase(dblog);
                mainForm.lbl_dbname.Text = db;

                RegistryAccess.SetStringRegistryValue("Database", dblog.Testing_Database);
                this.mainForm.UpdateMainFormUI();
            }

        }

        private void btn_set_path_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            pathset();
        }
        private void pathset()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string default_path = dlg.SelectedPath;
                setPath(default_path);
                setPathLocal(default_path);
            }
        }

        private void setPathLocal(string default_path)
        {
            LogUtill.defaultDir(default_path);
            lbl_link_path.Enabled = true;
            adminLog = LogUtill.getAdminInputLog();
            lbl_link_path.Text = adminLog.SalesInProccessedFolder
                + "\n" + adminLog.SaleProcessedDir
                + "\n" + adminLog.BackupPath;
            lbl_path_default.Text = default_path;
            if (!Directory.Exists(adminLog.DefultDIR))
                Directory.CreateDirectory(adminLog.DefultDIR);
            if (!Directory.Exists(adminLog.SaleProcessedDir))
                Directory.CreateDirectory(adminLog.SaleProcessedDir);
            if (!Directory.Exists(adminLog.SalesInProccessedFolder))
                Directory.CreateDirectory(adminLog.SalesInProccessedFolder);
            if (!Directory.Exists(adminLog.BackupPath))
                Directory.CreateDirectory(adminLog.BackupPath);
        }

        private void setPath(string path)
        {
            new BLogic().setPathFiles(path);
        }
        private string DefaultPath = "C:\\ArthiApp";
        private string getPath() {
            DataTable dt = new BLogic().crudSettings("");
            if (dt == null)
            {
                setPath(DefaultPath);
                setPathLocal(DefaultPath);
                return DefaultPath;
            }
            DataRow dr = dt.Rows[0];
            string path = dr[1].ToString();
            return path;
        }
        private void btn_backup_Click(object sender, EventArgs e)
        {

            if (adminLog.BackupPath == string.Empty)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("a1082"));
                pathset();
            }
            else
            {
                try
                {
                    DatabaseLog db = LogUtill.getDatabaseLog();

                    if (bal.backupDb(adminLog.BackupPath, db.LocalCheck))
                    {
                        MessageBox.Show(Resources.ResourceManager.GetString("a1084"));
                    }

                    btn_backup.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btn_restore_Click(object sender, EventArgs e)
        {

            try
            {
                if (txt_restore_file_path.Text == string.Empty)
                {
                    MessageBox.Show(Resources.ResourceManager.GetString("a1082"));
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Please Take Previous Backup Before Restore.", "Backup Database", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //btn_backup_Click(this, new EventArgs());
                    if (bal.restoreDB(txt_restore_file_path.Text))
                    {
                        MessageBox.Show(Resources.ResourceManager.GetString("a1083"));
                    }
                }
                else
                if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Backup & Restore Cancel. Please Take Backup First.");

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL SERVER database backup files|*.bak";
            dlg.Title = "Database restore";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txt_restore_file_path.Text = dlg.FileName;
                btn_restore.Enabled = true;
            }
        }

        private void btn_password_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            PasswordForgot pf = new PasswordForgot();
            pf.ShowDialog();
        }

        private void chk_security_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_security.Checked)
            {
                //RegistryAccess.SetStringSecurityValue(Const.SECURITYKEY, true);
                //new EncrypDecrypt().EncryptDatabase(lbl_license.Text);

            }
            else
            {
                // RegistryAccess.SetStringSecurityValue(Const.SECURITYKEY, false);
                // new EncrypDecrypt().DecryptDatabase(lbl_license.Text);
            }
        }

        

        private void hyperLinkEdit3_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {

            ProfileEdit prof = new ProfileEdit(Authentication.Account);
            prof.ShowDialog();
            AdminProfile_Load(this, new EventArgs());
            prof.Close();
        }

        private void btn_rep_setting_Click(object sender, EventArgs e)
        {
            ReportSetting rs = new ReportSetting();
            rs.ShowDialog();
        }



        private void chk_savelocal_Click(object sender, EventArgs e)
        {
            bool chk = false;
            bool check = false;
            string password = Prompt.ShowDialog("User Check", "Enter Password");


            if (chk_savelocal.Checked)
            {
                Account acc = new Account();
                acc.local = "1";
                chk = bal.account_update_Local(acc);
                if (chk)
                {
                    check = true;
                    chk_savelocal.CheckState = CheckState.Checked;
                    Account account = this.bal.check_User(lbl_name.Text, password);
                    if (account != null)
                    {
                        Authentication.Account = account;
                    }
                    else
                    {
                        acc.local = "0";
                        chk = bal.account_update_Local(acc);
                    }
                }
            }
            else
            {
                Account acc = new Account();
                acc.local = "0";
                chk = bal.account_update_Local(acc);

                if (chk)
                {
                    check = false;
                    chk_savelocal.CheckState = CheckState.Unchecked;
                    Account account = this.bal.check_User(lbl_name.Text, password);
                    if (account != null)
                    {
                        Authentication.Account = account;
                    }
                    else
                    {
                        acc.local = "1";
                        chk = bal.account_update_Local(acc);
                    }
                }

            }
            if (check)
            {
                MessageBox.Show("Sales Will Be Saved in Local Machine..");
            }
            else
            {
                MessageBox.Show("Sales Will Not be Saved in Local Machine..");
            }
        }

        private void btn_billpagesetting_Click(object sender, EventArgs e)
        {
            PageSettings ps = new PageSettings();
            ps.ShowDialog();
        }

        private void chk_db_SelectedIndexChanged(object sender, EventArgs e)
        {
            //changeDatabase();
        }


        private string sdate = "", ldate = "", detail = "", passcode = "";

        private void chk_accClosing_Click(object sender, EventArgs e)
        {
            if (chk_accClosing.Checked)
            {
                Account acc = new Account();
                acc.accountclosing = "1";
                new BLogic().updateCashInoutAccount(acc);
            }
            else
            {
                Account acc = new Account();
                acc.accountclosing = "0";
                new BLogic().updateCashInoutAccount(acc);
            }
        }
    }
}
