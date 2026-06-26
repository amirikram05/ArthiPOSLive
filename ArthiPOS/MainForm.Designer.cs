using System.Drawing;
using System.Windows.Forms;

namespace ArthiPOS.controls
{
    partial class MainForm
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            BunifuAnimatorNS.Animation animation1 = new BunifuAnimatorNS.Animation();
            BunifuAnimatorNS.Animation animation2 = new BunifuAnimatorNS.Animation();
            this.nav_menu_left = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.today_date = new MetroFramework.Controls.MetroDateTime();
            this.panel2 = new System.Windows.Forms.Panel();
            this.vmenu_rent = new Bunifu.Framework.UI.BunifuFlatButton();
            this.lbl_aboutinfo = new System.Windows.Forms.Label();
            this.menu_signout = new Bunifu.Framework.UI.BunifuFlatButton();
            this.vmenu_dashboard = new Bunifu.Framework.UI.BunifuFlatButton();
            this.vmenu_reports = new Bunifu.Framework.UI.BunifuFlatButton();
            this.menu_account_profile = new Bunifu.Framework.UI.BunifuFlatButton();
            this.menu_settings = new Bunifu.Framework.UI.BunifuFlatButton();
            this.vmenu_profile = new Bunifu.Framework.UI.BunifuFlatButton();
            this.vmenu_pos = new Bunifu.Framework.UI.BunifuFlatButton();
            this.vmenu_augrai = new Bunifu.Framework.UI.BunifuFlatButton();
            this.vmenu_ledger = new Bunifu.Framework.UI.BunifuFlatButton();
            this.vmenu_invoicing = new Bunifu.Framework.UI.BunifuFlatButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardCTRL1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pOSCTRL3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invoicingCTRL4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.augraiCTRL6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cashINOUTCTRL7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profilesCTRL8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportCTRL9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cTRL0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.calcEdit1 = new DevExpress.XtraEditors.CalcEdit();
            this.lbl_dbname = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuImageButton5 = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuImageButton2 = new Bunifu.Framework.UI.BunifuImageButton();
            this.lbl_header_title = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuImageButton4 = new Bunifu.Framework.UI.BunifuImageButton();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.urduToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gdriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shopIncomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hELPToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lOGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cONTRACTSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kEYBOARDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_info = new System.Windows.Forms.Panel();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.navigationFadeTransition1 = new Bunifu.Framework.UI.BunifuFormFadeTransition(this.components);
            this.tshow = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.miniToolStrip = new System.Windows.Forms.MenuStrip();
            this.dropdownMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemReports = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemShopDaily = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNotifications = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.thide = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.nav_menu_left.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton4)).BeginInit();
            this.menuStrip2.SuspendLayout();
            this.panel_info.SuspendLayout();
            this.dropdownMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // nav_menu_left
            // 
            resources.ApplyResources(this.nav_menu_left, "nav_menu_left");
            this.nav_menu_left.BackColor = System.Drawing.Color.White;
            this.nav_menu_left.Controls.Add(this.panel3);
            this.nav_menu_left.Controls.Add(this.today_date);
            this.nav_menu_left.Controls.Add(this.panel2);
            this.tshow.SetDecoration(this.nav_menu_left, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.nav_menu_left, BunifuAnimatorNS.DecorationType.None);
            this.nav_menu_left.Name = "nav_menu_left";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox2);
            this.thide.SetDecoration(this.panel3, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.panel3, BunifuAnimatorNS.DecorationType.None);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.tshow.SetDecoration(this.pictureBox2, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.pictureBox2, BunifuAnimatorNS.DecorationType.None);
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // today_date
            // 
            resources.ApplyResources(this.today_date, "today_date");
            this.thide.SetDecoration(this.today_date, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.today_date, BunifuAnimatorNS.DecorationType.None);
            this.today_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.today_date.Name = "today_date";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.vmenu_rent);
            this.panel2.Controls.Add(this.lbl_aboutinfo);
            this.panel2.Controls.Add(this.menu_signout);
            this.panel2.Controls.Add(this.vmenu_dashboard);
            this.panel2.Controls.Add(this.vmenu_reports);
            this.panel2.Controls.Add(this.menu_account_profile);
            this.panel2.Controls.Add(this.menu_settings);
            this.panel2.Controls.Add(this.vmenu_profile);
            this.panel2.Controls.Add(this.vmenu_pos);
            this.panel2.Controls.Add(this.vmenu_augrai);
            this.panel2.Controls.Add(this.vmenu_ledger);
            this.panel2.Controls.Add(this.vmenu_invoicing);
            this.panel2.Controls.Add(this.menuStrip1);
            this.thide.SetDecoration(this.panel2, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.panel2, BunifuAnimatorNS.DecorationType.None);
            this.panel2.Name = "panel2";
            // 
            // vmenu_rent
            // 
            this.vmenu_rent.Activecolor = System.Drawing.Color.WhiteSmoke;
            this.vmenu_rent.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.vmenu_rent, "vmenu_rent");
            this.vmenu_rent.BorderRadius = 7;
            this.vmenu_rent.ButtonText = "Rent/Shop";
            this.vmenu_rent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tshow.SetDecoration(this.vmenu_rent, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.vmenu_rent, BunifuAnimatorNS.DecorationType.None);
            this.vmenu_rent.DisabledColor = System.Drawing.Color.Gray;
            this.vmenu_rent.Iconcolor = System.Drawing.Color.Transparent;
            this.vmenu_rent.Iconimage = null;
            this.vmenu_rent.Iconimage_right = null;
            this.vmenu_rent.Iconimage_right_Selected = null;
            this.vmenu_rent.Iconimage_Selected = null;
            this.vmenu_rent.IconMarginLeft = 0;
            this.vmenu_rent.IconMarginRight = 0;
            this.vmenu_rent.IconRightVisible = false;
            this.vmenu_rent.IconRightZoom = 0D;
            this.vmenu_rent.IconVisible = false;
            this.vmenu_rent.IconZoom = 90D;
            this.vmenu_rent.IsTab = true;
            this.vmenu_rent.Name = "vmenu_rent";
            this.vmenu_rent.Normalcolor = System.Drawing.Color.White;
            this.vmenu_rent.OnHovercolor = System.Drawing.Color.White;
            this.vmenu_rent.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(51)))));
            this.vmenu_rent.selected = false;
            this.vmenu_rent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vmenu_rent.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.vmenu_rent.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.vmenu_rent.Click += new System.EventHandler(this.vmenu_rent_Click);
            // 
            // lbl_aboutinfo
            // 
            resources.ApplyResources(this.lbl_aboutinfo, "lbl_aboutinfo");
            this.lbl_aboutinfo.BackColor = System.Drawing.Color.Transparent;
            this.tshow.SetDecoration(this.lbl_aboutinfo, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.lbl_aboutinfo, BunifuAnimatorNS.DecorationType.None);
            this.lbl_aboutinfo.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_aboutinfo.Name = "lbl_aboutinfo";
            this.lbl_aboutinfo.Click += new System.EventHandler(this.lbl_aboutinfo_Click);
            // 
            // menu_signout
            // 
            this.menu_signout.Activecolor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.menu_signout, "menu_signout");
            this.menu_signout.BackColor = System.Drawing.Color.Transparent;
            this.menu_signout.BorderRadius = 7;
            this.menu_signout.ButtonText = "Logout";
            this.menu_signout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tshow.SetDecoration(this.menu_signout, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.menu_signout, BunifuAnimatorNS.DecorationType.None);
            this.menu_signout.DisabledColor = System.Drawing.Color.Transparent;
            this.menu_signout.Iconcolor = System.Drawing.Color.Transparent;
            this.menu_signout.Iconimage = null;
            this.menu_signout.Iconimage_right = null;
            this.menu_signout.Iconimage_right_Selected = null;
            this.menu_signout.Iconimage_Selected = null;
            this.menu_signout.IconMarginLeft = 0;
            this.menu_signout.IconMarginRight = 0;
            this.menu_signout.IconRightVisible = false;
            this.menu_signout.IconRightZoom = 0D;
            this.menu_signout.IconVisible = false;
            this.menu_signout.IconZoom = 90D;
            this.menu_signout.IsTab = true;
            this.menu_signout.Name = "menu_signout";
            this.menu_signout.Normalcolor = System.Drawing.Color.Transparent;
            this.menu_signout.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(111)))));
            this.menu_signout.OnHoverTextColor = System.Drawing.Color.CornflowerBlue;
            this.menu_signout.selected = false;
            this.menu_signout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.menu_signout.Textcolor = System.Drawing.Color.DodgerBlue;
            this.menu_signout.TextFont = new System.Drawing.Font("Segoe UI", 11F);
            this.menu_signout.Click += new System.EventHandler(this.signout_bunifuFlat_Click);
            // 
            // vmenu_dashboard
            // 
            this.vmenu_dashboard.Activecolor = System.Drawing.Color.WhiteSmoke;
            this.vmenu_dashboard.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.vmenu_dashboard, "vmenu_dashboard");
            this.vmenu_dashboard.BorderRadius = 7;
            this.vmenu_dashboard.ButtonText = "Dashboard";
            this.vmenu_dashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tshow.SetDecoration(this.vmenu_dashboard, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.vmenu_dashboard, BunifuAnimatorNS.DecorationType.None);
            this.vmenu_dashboard.DisabledColor = System.Drawing.Color.Gray;
            this.vmenu_dashboard.Iconcolor = System.Drawing.Color.Transparent;
            this.vmenu_dashboard.Iconimage = null;
            this.vmenu_dashboard.Iconimage_right = null;
            this.vmenu_dashboard.Iconimage_right_Selected = null;
            this.vmenu_dashboard.Iconimage_Selected = null;
            this.vmenu_dashboard.IconMarginLeft = 0;
            this.vmenu_dashboard.IconMarginRight = 0;
            this.vmenu_dashboard.IconRightVisible = false;
            this.vmenu_dashboard.IconRightZoom = 120D;
            this.vmenu_dashboard.IconVisible = true;
            this.vmenu_dashboard.IconZoom = 100D;
            this.vmenu_dashboard.IsTab = true;
            this.vmenu_dashboard.Name = "vmenu_dashboard";
            this.vmenu_dashboard.Normalcolor = System.Drawing.Color.White;
            this.vmenu_dashboard.OnHovercolor = System.Drawing.Color.White;
            this.vmenu_dashboard.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(51)))));
            this.vmenu_dashboard.selected = true;
            this.vmenu_dashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vmenu_dashboard.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.vmenu_dashboard.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.vmenu_dashboard.Click += new System.EventHandler(this.bunifuFlatButton2_Click);
            // 
            // vmenu_reports
            // 
            this.vmenu_reports.Activecolor = System.Drawing.Color.WhiteSmoke;
            this.vmenu_reports.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.vmenu_reports, "vmenu_reports");
            this.vmenu_reports.BorderRadius = 7;
            this.vmenu_reports.ButtonText = "Reports";
            this.vmenu_reports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tshow.SetDecoration(this.vmenu_reports, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.vmenu_reports, BunifuAnimatorNS.DecorationType.None);
            this.vmenu_reports.DisabledColor = System.Drawing.Color.Gray;
            this.vmenu_reports.Iconcolor = System.Drawing.Color.Transparent;
            this.vmenu_reports.Iconimage = null;
            this.vmenu_reports.Iconimage_right = null;
            this.vmenu_reports.Iconimage_right_Selected = null;
            this.vmenu_reports.Iconimage_Selected = null;
            this.vmenu_reports.IconMarginLeft = 0;
            this.vmenu_reports.IconMarginRight = 0;
            this.vmenu_reports.IconRightVisible = false;
            this.vmenu_reports.IconRightZoom = 0D;
            this.vmenu_reports.IconVisible = false;
            this.vmenu_reports.IconZoom = 90D;
            this.vmenu_reports.IsTab = true;
            this.vmenu_reports.Name = "vmenu_reports";
            this.vmenu_reports.Normalcolor = System.Drawing.Color.White;
            this.vmenu_reports.OnHovercolor = System.Drawing.Color.White;
            this.vmenu_reports.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(51)))));
            this.vmenu_reports.selected = false;
            this.vmenu_reports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vmenu_reports.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.vmenu_reports.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.vmenu_reports.Click += new System.EventHandler(this.vmenu_reports_Click);
            // 
            // menu_account_profile
            // 
            this.menu_account_profile.Activecolor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.menu_account_profile, "menu_account_profile");
            this.menu_account_profile.BackColor = System.Drawing.Color.Transparent;
            this.menu_account_profile.BorderRadius = 7;
            this.menu_account_profile.ButtonText = "Admin";
            this.menu_account_profile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tshow.SetDecoration(this.menu_account_profile, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.menu_account_profile, BunifuAnimatorNS.DecorationType.None);
            this.menu_account_profile.DisabledColor = System.Drawing.Color.Gray;
            this.menu_account_profile.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menu_account_profile.Iconcolor = System.Drawing.Color.Transparent;
            this.menu_account_profile.Iconimage = null;
            this.menu_account_profile.Iconimage_right = null;
            this.menu_account_profile.Iconimage_right_Selected = null;
            this.menu_account_profile.Iconimage_Selected = null;
            this.menu_account_profile.IconMarginLeft = 0;
            this.menu_account_profile.IconMarginRight = 0;
            this.menu_account_profile.IconRightVisible = true;
            this.menu_account_profile.IconRightZoom = 0D;
            this.menu_account_profile.IconVisible = false;
            this.menu_account_profile.IconZoom = 20D;
            this.menu_account_profile.IsTab = true;
            this.menu_account_profile.Name = "menu_account_profile";
            this.menu_account_profile.Normalcolor = System.Drawing.Color.Transparent;
            this.menu_account_profile.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menu_account_profile.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(51)))));
            this.menu_account_profile.selected = false;
            this.menu_account_profile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_account_profile.Textcolor = System.Drawing.Color.DimGray;
            this.menu_account_profile.TextFont = new System.Drawing.Font("Segoe UI", 11F);
            this.menu_account_profile.Click += new System.EventHandler(this.menu_account_profile_Click);
            // 
            // menu_settings
            // 
            this.menu_settings.Activecolor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.menu_settings, "menu_settings");
            this.menu_settings.BackColor = System.Drawing.Color.Transparent;
            this.menu_settings.BorderRadius = 7;
            this.menu_settings.ButtonText = "Configuration";
            this.menu_settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tshow.SetDecoration(this.menu_settings, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.menu_settings, BunifuAnimatorNS.DecorationType.None);
            this.menu_settings.DisabledColor = System.Drawing.Color.Gray;
            this.menu_settings.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menu_settings.Iconcolor = System.Drawing.Color.Transparent;
            this.menu_settings.Iconimage = null;
            this.menu_settings.Iconimage_right = null;
            this.menu_settings.Iconimage_right_Selected = null;
            this.menu_settings.Iconimage_Selected = null;
            this.menu_settings.IconMarginLeft = 0;
            this.menu_settings.IconMarginRight = 0;
            this.menu_settings.IconRightVisible = false;
            this.menu_settings.IconRightZoom = 0D;
            this.menu_settings.IconVisible = false;
            this.menu_settings.IconZoom = 90D;
            this.menu_settings.IsTab = true;
            this.menu_settings.Name = "menu_settings";
            this.menu_settings.Normalcolor = System.Drawing.Color.Transparent;
            this.menu_settings.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menu_settings.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(51)))));
            this.menu_settings.selected = false;
            this.menu_settings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_settings.Textcolor = System.Drawing.Color.DimGray;
            this.menu_settings.TextFont = new System.Drawing.Font("Segoe UI", 11F);
            this.menu_settings.Click += new System.EventHandler(this.menu_settings_Click_1);
            // 
            // vmenu_profile
            // 
            this.vmenu_profile.Activecolor = System.Drawing.Color.WhiteSmoke;
            this.vmenu_profile.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.vmenu_profile, "vmenu_profile");
            this.vmenu_profile.BorderRadius = 7;
            this.vmenu_profile.ButtonText = "Profiles";
            this.vmenu_profile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tshow.SetDecoration(this.vmenu_profile, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.vmenu_profile, BunifuAnimatorNS.DecorationType.None);
            this.vmenu_profile.DisabledColor = System.Drawing.Color.Gray;
            this.vmenu_profile.Iconcolor = System.Drawing.Color.Transparent;
            this.vmenu_profile.Iconimage = null;
            this.vmenu_profile.Iconimage_right = null;
            this.vmenu_profile.Iconimage_right_Selected = null;
            this.vmenu_profile.Iconimage_Selected = null;
            this.vmenu_profile.IconMarginLeft = 0;
            this.vmenu_profile.IconMarginRight = 0;
            this.vmenu_profile.IconRightVisible = false;
            this.vmenu_profile.IconRightZoom = 0D;
            this.vmenu_profile.IconVisible = false;
            this.vmenu_profile.IconZoom = 90D;
            this.vmenu_profile.IsTab = true;
            this.vmenu_profile.Name = "vmenu_profile";
            this.vmenu_profile.Normalcolor = System.Drawing.Color.White;
            this.vmenu_profile.OnHovercolor = System.Drawing.Color.White;
            this.vmenu_profile.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(51)))));
            this.vmenu_profile.selected = false;
            this.vmenu_profile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vmenu_profile.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.vmenu_profile.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.vmenu_profile.Click += new System.EventHandler(this.vmenu_profile_Click);
            // 
            // vmenu_pos
            // 
            this.vmenu_pos.Activecolor = System.Drawing.Color.WhiteSmoke;
            this.vmenu_pos.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.vmenu_pos, "vmenu_pos");
            this.vmenu_pos.BorderRadius = 7;
            this.vmenu_pos.ButtonText = "Point of Sale";
            this.vmenu_pos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tshow.SetDecoration(this.vmenu_pos, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.vmenu_pos, BunifuAnimatorNS.DecorationType.None);
            this.vmenu_pos.DisabledColor = System.Drawing.Color.Gray;
            this.vmenu_pos.Iconcolor = System.Drawing.Color.Transparent;
            this.vmenu_pos.Iconimage = null;
            this.vmenu_pos.Iconimage_right = global::ArthiPOS.Properties.Resources.right;
            this.vmenu_pos.Iconimage_right_Selected = null;
            this.vmenu_pos.Iconimage_Selected = null;
            this.vmenu_pos.IconMarginLeft = 0;
            this.vmenu_pos.IconMarginRight = 0;
            this.vmenu_pos.IconRightVisible = false;
            this.vmenu_pos.IconRightZoom = 0D;
            this.vmenu_pos.IconVisible = false;
            this.vmenu_pos.IconZoom = 90D;
            this.vmenu_pos.IsTab = true;
            this.vmenu_pos.Name = "vmenu_pos";
            this.vmenu_pos.Normalcolor = System.Drawing.Color.White;
            this.vmenu_pos.OnHovercolor = System.Drawing.Color.White;
            this.vmenu_pos.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(51)))));
            this.vmenu_pos.selected = false;
            this.vmenu_pos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vmenu_pos.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.vmenu_pos.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.vmenu_pos.Click += new System.EventHandler(this.bunifuFlatButton3_Click);
            // 
            // vmenu_augrai
            // 
            this.vmenu_augrai.Activecolor = System.Drawing.Color.WhiteSmoke;
            this.vmenu_augrai.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.vmenu_augrai, "vmenu_augrai");
            this.vmenu_augrai.BorderRadius = 7;
            this.vmenu_augrai.ButtonText = "Augrai";
            this.vmenu_augrai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tshow.SetDecoration(this.vmenu_augrai, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.vmenu_augrai, BunifuAnimatorNS.DecorationType.None);
            this.vmenu_augrai.DisabledColor = System.Drawing.Color.Gray;
            this.vmenu_augrai.Iconcolor = System.Drawing.Color.Transparent;
            this.vmenu_augrai.Iconimage = null;
            this.vmenu_augrai.Iconimage_right = null;
            this.vmenu_augrai.Iconimage_right_Selected = null;
            this.vmenu_augrai.Iconimage_Selected = null;
            this.vmenu_augrai.IconMarginLeft = 0;
            this.vmenu_augrai.IconMarginRight = 0;
            this.vmenu_augrai.IconRightVisible = false;
            this.vmenu_augrai.IconRightZoom = 0D;
            this.vmenu_augrai.IconVisible = false;
            this.vmenu_augrai.IconZoom = 90D;
            this.vmenu_augrai.IsTab = true;
            this.vmenu_augrai.Name = "vmenu_augrai";
            this.vmenu_augrai.Normalcolor = System.Drawing.Color.White;
            this.vmenu_augrai.OnHovercolor = System.Drawing.Color.White;
            this.vmenu_augrai.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(51)))));
            this.vmenu_augrai.selected = false;
            this.vmenu_augrai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vmenu_augrai.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.vmenu_augrai.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.vmenu_augrai.Click += new System.EventHandler(this.Vmenu_augrai_Click);
            // 
            // vmenu_ledger
            // 
            this.vmenu_ledger.Activecolor = System.Drawing.Color.WhiteSmoke;
            this.vmenu_ledger.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.vmenu_ledger, "vmenu_ledger");
            this.vmenu_ledger.BorderRadius = 7;
            this.vmenu_ledger.ButtonText = "Cash INOUT";
            this.vmenu_ledger.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tshow.SetDecoration(this.vmenu_ledger, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.vmenu_ledger, BunifuAnimatorNS.DecorationType.None);
            this.vmenu_ledger.DisabledColor = System.Drawing.Color.Gray;
            this.vmenu_ledger.Iconcolor = System.Drawing.Color.Transparent;
            this.vmenu_ledger.Iconimage = null;
            this.vmenu_ledger.Iconimage_right = null;
            this.vmenu_ledger.Iconimage_right_Selected = null;
            this.vmenu_ledger.Iconimage_Selected = null;
            this.vmenu_ledger.IconMarginLeft = 0;
            this.vmenu_ledger.IconMarginRight = 0;
            this.vmenu_ledger.IconRightVisible = false;
            this.vmenu_ledger.IconRightZoom = 0D;
            this.vmenu_ledger.IconVisible = false;
            this.vmenu_ledger.IconZoom = 90D;
            this.vmenu_ledger.IsTab = true;
            this.vmenu_ledger.Name = "vmenu_ledger";
            this.vmenu_ledger.Normalcolor = System.Drawing.Color.White;
            this.vmenu_ledger.OnHovercolor = System.Drawing.Color.White;
            this.vmenu_ledger.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(51)))));
            this.vmenu_ledger.selected = false;
            this.vmenu_ledger.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vmenu_ledger.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.vmenu_ledger.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.vmenu_ledger.Click += new System.EventHandler(this.vmenu_ledger_Click);
            // 
            // vmenu_invoicing
            // 
            this.vmenu_invoicing.Activecolor = System.Drawing.Color.WhiteSmoke;
            this.vmenu_invoicing.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.vmenu_invoicing, "vmenu_invoicing");
            this.vmenu_invoicing.BorderRadius = 7;
            this.vmenu_invoicing.ButtonText = "Invoicing";
            this.vmenu_invoicing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tshow.SetDecoration(this.vmenu_invoicing, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.vmenu_invoicing, BunifuAnimatorNS.DecorationType.None);
            this.vmenu_invoicing.DisabledColor = System.Drawing.Color.Gray;
            this.vmenu_invoicing.Iconcolor = System.Drawing.Color.Transparent;
            this.vmenu_invoicing.Iconimage = null;
            this.vmenu_invoicing.Iconimage_right = null;
            this.vmenu_invoicing.Iconimage_right_Selected = null;
            this.vmenu_invoicing.Iconimage_Selected = null;
            this.vmenu_invoicing.IconMarginLeft = 0;
            this.vmenu_invoicing.IconMarginRight = 0;
            this.vmenu_invoicing.IconRightVisible = false;
            this.vmenu_invoicing.IconRightZoom = 0D;
            this.vmenu_invoicing.IconVisible = false;
            this.vmenu_invoicing.IconZoom = 90D;
            this.vmenu_invoicing.IsTab = true;
            this.vmenu_invoicing.Name = "vmenu_invoicing";
            this.vmenu_invoicing.Normalcolor = System.Drawing.Color.White;
            this.vmenu_invoicing.OnHovercolor = System.Drawing.Color.White;
            this.vmenu_invoicing.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(51)))));
            this.vmenu_invoicing.selected = false;
            this.vmenu_invoicing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vmenu_invoicing.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.vmenu_invoicing.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.vmenu_invoicing.Click += new System.EventHandler(this.btn_invoicing_Click);
            // 
            // menuStrip1
            // 
            this.thide.SetDecoration(this.menuStrip1, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.menuStrip1, BunifuAnimatorNS.DecorationType.None);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dashboardCTRL1ToolStripMenuItem,
            this.pOSCTRL3ToolStripMenuItem,
            this.invoicingCTRL4ToolStripMenuItem,
            this.augraiCTRL6ToolStripMenuItem,
            this.cashINOUTCTRL7ToolStripMenuItem,
            this.profilesCTRL8ToolStripMenuItem,
            this.reportCTRL9ToolStripMenuItem,
            this.cTRL0ToolStripMenuItem});
            resources.ApplyResources(this.menuToolStripMenuItem, "menuToolStripMenuItem");
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            // 
            // dashboardCTRL1ToolStripMenuItem
            // 
            this.dashboardCTRL1ToolStripMenuItem.Name = "dashboardCTRL1ToolStripMenuItem";
            resources.ApplyResources(this.dashboardCTRL1ToolStripMenuItem, "dashboardCTRL1ToolStripMenuItem");
            // 
            // pOSCTRL3ToolStripMenuItem
            // 
            this.pOSCTRL3ToolStripMenuItem.Name = "pOSCTRL3ToolStripMenuItem";
            resources.ApplyResources(this.pOSCTRL3ToolStripMenuItem, "pOSCTRL3ToolStripMenuItem");
            // 
            // invoicingCTRL4ToolStripMenuItem
            // 
            this.invoicingCTRL4ToolStripMenuItem.Name = "invoicingCTRL4ToolStripMenuItem";
            resources.ApplyResources(this.invoicingCTRL4ToolStripMenuItem, "invoicingCTRL4ToolStripMenuItem");
            // 
            // augraiCTRL6ToolStripMenuItem
            // 
            this.augraiCTRL6ToolStripMenuItem.Name = "augraiCTRL6ToolStripMenuItem";
            resources.ApplyResources(this.augraiCTRL6ToolStripMenuItem, "augraiCTRL6ToolStripMenuItem");
            // 
            // cashINOUTCTRL7ToolStripMenuItem
            // 
            this.cashINOUTCTRL7ToolStripMenuItem.Name = "cashINOUTCTRL7ToolStripMenuItem";
            resources.ApplyResources(this.cashINOUTCTRL7ToolStripMenuItem, "cashINOUTCTRL7ToolStripMenuItem");
            // 
            // profilesCTRL8ToolStripMenuItem
            // 
            this.profilesCTRL8ToolStripMenuItem.Name = "profilesCTRL8ToolStripMenuItem";
            resources.ApplyResources(this.profilesCTRL8ToolStripMenuItem, "profilesCTRL8ToolStripMenuItem");
            // 
            // reportCTRL9ToolStripMenuItem
            // 
            this.reportCTRL9ToolStripMenuItem.Name = "reportCTRL9ToolStripMenuItem";
            resources.ApplyResources(this.reportCTRL9ToolStripMenuItem, "reportCTRL9ToolStripMenuItem");
            // 
            // cTRL0ToolStripMenuItem
            // 
            this.cTRL0ToolStripMenuItem.Name = "cTRL0ToolStripMenuItem";
            resources.ApplyResources(this.cTRL0ToolStripMenuItem, "cTRL0ToolStripMenuItem");
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.calcEdit1);
            this.panel1.Controls.Add(this.lbl_dbname);
            this.panel1.Controls.Add(this.bunifuImageButton5);
            this.panel1.Controls.Add(this.bunifuImageButton2);
            this.panel1.Controls.Add(this.lbl_header_title);
            this.panel1.Controls.Add(this.bunifuImageButton1);
            this.panel1.Controls.Add(this.bunifuImageButton4);
            this.panel1.Controls.Add(this.menuStrip2);
            this.thide.SetDecoration(this.panel1, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.panel1, BunifuAnimatorNS.DecorationType.None);
            this.panel1.Name = "panel1";
            // 
            // calcEdit1
            // 
            resources.ApplyResources(this.calcEdit1, "calcEdit1");
            this.thide.SetDecoration(this.calcEdit1, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.calcEdit1, BunifuAnimatorNS.DecorationType.None);
            this.calcEdit1.Name = "calcEdit1";
            this.calcEdit1.Properties.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("calcEdit1.Properties.Appearance.BackColor")));
            this.calcEdit1.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("calcEdit1.Properties.Appearance.Font")));
            this.calcEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.calcEdit1.Properties.Appearance.Options.UseFont = true;
            this.calcEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("calcEdit1.Properties.Buttons"))))});
            this.calcEdit1.Properties.ShowCloseButton = true;
            // 
            // lbl_dbname
            // 
            resources.ApplyResources(this.lbl_dbname, "lbl_dbname");
            this.tshow.SetDecoration(this.lbl_dbname, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.lbl_dbname, BunifuAnimatorNS.DecorationType.None);
            this.lbl_dbname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbl_dbname.Name = "lbl_dbname";
            // 
            // bunifuImageButton5
            // 
            resources.ApplyResources(this.bunifuImageButton5, "bunifuImageButton5");
            this.bunifuImageButton5.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thide.SetDecoration(this.bunifuImageButton5, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.bunifuImageButton5, BunifuAnimatorNS.DecorationType.None);
            this.bunifuImageButton5.ImageActive = null;
            this.bunifuImageButton5.Name = "bunifuImageButton5";
            this.bunifuImageButton5.TabStop = false;
            this.bunifuImageButton5.Zoom = 10;
            this.bunifuImageButton5.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // bunifuImageButton2
            // 
            resources.ApplyResources(this.bunifuImageButton2, "bunifuImageButton2");
            this.bunifuImageButton2.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thide.SetDecoration(this.bunifuImageButton2, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.bunifuImageButton2, BunifuAnimatorNS.DecorationType.None);
            this.bunifuImageButton2.ImageActive = null;
            this.bunifuImageButton2.Name = "bunifuImageButton2";
            this.bunifuImageButton2.TabStop = false;
            this.bunifuImageButton2.Zoom = 10;
            this.bunifuImageButton2.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // lbl_header_title
            // 
            resources.ApplyResources(this.lbl_header_title, "lbl_header_title");
            this.tshow.SetDecoration(this.lbl_header_title, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.lbl_header_title, BunifuAnimatorNS.DecorationType.None);
            this.lbl_header_title.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_header_title.Name = "lbl_header_title";
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thide.SetDecoration(this.bunifuImageButton1, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.bunifuImageButton1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuImageButton1.Image = global::ArthiPOS.Properties.Resources.menuxxx;
            this.bunifuImageButton1.ImageActive = null;
            resources.ApplyResources(this.bunifuImageButton1, "bunifuImageButton1");
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            this.bunifuImageButton1.Click += new System.EventHandler(this.nav_menu_Click);
            // 
            // bunifuImageButton4
            // 
            resources.ApplyResources(this.bunifuImageButton4, "bunifuImageButton4");
            this.bunifuImageButton4.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thide.SetDecoration(this.bunifuImageButton4, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.bunifuImageButton4, BunifuAnimatorNS.DecorationType.None);
            this.bunifuImageButton4.ImageActive = null;
            this.bunifuImageButton4.Name = "bunifuImageButton4";
            this.bunifuImageButton4.TabStop = false;
            this.bunifuImageButton4.Zoom = 10;
            this.bunifuImageButton4.Click += new System.EventHandler(this.bunifuImageButton4_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.Color.White;
            this.thide.SetDecoration(this.menuStrip2, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.menuStrip2, BunifuAnimatorNS.DecorationType.None);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gdriveToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.shopIncomeToolStripMenuItem,
            this.hELPToolStripMenuItem1});
            resources.ApplyResources(this.menuStrip2, "menuStrip2");
            this.menuStrip2.Name = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.urduToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // urduToolStripMenuItem
            // 
            this.urduToolStripMenuItem.Name = "urduToolStripMenuItem";
            resources.ApplyResources(this.urduToolStripMenuItem, "urduToolStripMenuItem");
            this.urduToolStripMenuItem.Click += new System.EventHandler(this.urduToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            resources.ApplyResources(this.logoutToolStripMenuItem, "logoutToolStripMenuItem");
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.signout_bunifuFlat_Click);
            // 
            // gdriveToolStripMenuItem
            // 
            this.gdriveToolStripMenuItem.Name = "gdriveToolStripMenuItem";
            resources.ApplyResources(this.gdriveToolStripMenuItem, "gdriveToolStripMenuItem");
            this.gdriveToolStripMenuItem.Click += new System.EventHandler(this.google_btn_drive_LinkClicked);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.link_shopdaily_Click);
            // 
            // shopIncomeToolStripMenuItem
            // 
            this.shopIncomeToolStripMenuItem.Name = "shopIncomeToolStripMenuItem";
            resources.ApplyResources(this.shopIncomeToolStripMenuItem, "shopIncomeToolStripMenuItem");
            this.shopIncomeToolStripMenuItem.Click += new System.EventHandler(this.link_report_Click);
            // 
            // hELPToolStripMenuItem1
            // 
            this.hELPToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lOGToolStripMenuItem,
            this.cONTRACTSToolStripMenuItem,
            this.kEYBOARDToolStripMenuItem,
            this.notificationToolStripMenuItem});
            this.hELPToolStripMenuItem1.Name = "hELPToolStripMenuItem1";
            resources.ApplyResources(this.hELPToolStripMenuItem1, "hELPToolStripMenuItem1");
            // 
            // lOGToolStripMenuItem
            // 
            this.lOGToolStripMenuItem.Name = "lOGToolStripMenuItem";
            resources.ApplyResources(this.lOGToolStripMenuItem, "lOGToolStripMenuItem");
            this.lOGToolStripMenuItem.Click += new System.EventHandler(this.btn_log_Click);
            // 
            // cONTRACTSToolStripMenuItem
            // 
            this.cONTRACTSToolStripMenuItem.Name = "cONTRACTSToolStripMenuItem";
            resources.ApplyResources(this.cONTRACTSToolStripMenuItem, "cONTRACTSToolStripMenuItem");
            this.cONTRACTSToolStripMenuItem.Click += new System.EventHandler(this.btn_editor_Click);
            // 
            // kEYBOARDToolStripMenuItem
            // 
            this.kEYBOARDToolStripMenuItem.Name = "kEYBOARDToolStripMenuItem";
            resources.ApplyResources(this.kEYBOARDToolStripMenuItem, "kEYBOARDToolStripMenuItem");
            this.kEYBOARDToolStripMenuItem.Click += new System.EventHandler(this.bunifuImageButton3_Click);
            // 
            // notificationToolStripMenuItem
            // 
            this.notificationToolStripMenuItem.Name = "notificationToolStripMenuItem";
            resources.ApplyResources(this.notificationToolStripMenuItem, "notificationToolStripMenuItem");
            this.notificationToolStripMenuItem.Click += new System.EventHandler(this.btnNotifications_Click);
            // 
            // panel_info
            // 
            this.panel_info.BackColor = System.Drawing.Color.White;
            this.panel_info.Controls.Add(this.lblUserInfo);
            this.thide.SetDecoration(this.panel_info, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.panel_info, BunifuAnimatorNS.DecorationType.None);
            resources.ApplyResources(this.panel_info, "panel_info");
            this.panel_info.Name = "panel_info";
            // 
            // lblUserInfo
            // 
            resources.ApplyResources(this.lblUserInfo, "lblUserInfo");
            this.tshow.SetDecoration(this.lblUserInfo, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.lblUserInfo, BunifuAnimatorNS.DecorationType.None);
            this.lblUserInfo.ForeColor = System.Drawing.Color.White;
            this.lblUserInfo.Name = "lblUserInfo";
            // 
            // navigationFadeTransition1
            // 
            this.navigationFadeTransition1.Delay = 2;
            // 
            // tshow
            // 
            this.tshow.AnimationType = BunifuAnimatorNS.AnimationType.HorizSlide;
            this.tshow.Cursor = null;
            animation1.AnimateOnlyDifferences = true;
            animation1.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.BlindCoeff")));
            animation1.LeafCoeff = 0F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicCoeff")));
            animation1.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicShift")));
            animation1.MosaicSize = 0;
            animation1.Padding = new System.Windows.Forms.Padding(0);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.ScaleCoeff")));
            animation1.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.SlideCoeff")));
            animation1.TimeCoeff = 0F;
            animation1.TransparencyCoeff = 0F;
            this.tshow.DefaultAnimation = animation1;
            // 
            // miniToolStrip
            // 
            this.thide.SetDecoration(this.miniToolStrip, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.miniToolStrip, BunifuAnimatorNS.DecorationType.None);
            resources.ApplyResources(this.miniToolStrip, "miniToolStrip");
            this.miniToolStrip.Name = "miniToolStrip";
            // 
            // dropdownMenu
            // 
            this.thide.SetDecoration(this.dropdownMenu, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.dropdownMenu, BunifuAnimatorNS.DecorationType.None);
            this.dropdownMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemReports,
            this.menuItemShopDaily,
            this.menuItemNotifications,
            this.menuItemLog,
            this.menuItemEditor,
            this.menuItemHelp});
            this.dropdownMenu.Name = "dropdownMenu";
            this.dropdownMenu.ShowImageMargin = false;
            resources.ApplyResources(this.dropdownMenu, "dropdownMenu");
            // 
            // menuItemReports
            // 
            this.menuItemReports.Name = "menuItemReports";
            resources.ApplyResources(this.menuItemReports, "menuItemReports");
            this.menuItemReports.Click += new System.EventHandler(this.link_report_Click);
            // 
            // menuItemShopDaily
            // 
            this.menuItemShopDaily.Name = "menuItemShopDaily";
            resources.ApplyResources(this.menuItemShopDaily, "menuItemShopDaily");
            this.menuItemShopDaily.Click += new System.EventHandler(this.link_shopdaily_Click);
            // 
            // menuItemNotifications
            // 
            this.menuItemNotifications.Name = "menuItemNotifications";
            resources.ApplyResources(this.menuItemNotifications, "menuItemNotifications");
            this.menuItemNotifications.Click += new System.EventHandler(this.btnNotifications_Click);
            // 
            // menuItemLog
            // 
            this.menuItemLog.Name = "menuItemLog";
            resources.ApplyResources(this.menuItemLog, "menuItemLog");
            this.menuItemLog.Click += new System.EventHandler(this.btn_log_Click);
            // 
            // menuItemEditor
            // 
            this.menuItemEditor.Name = "menuItemEditor";
            resources.ApplyResources(this.menuItemEditor, "menuItemEditor");
            this.menuItemEditor.Click += new System.EventHandler(this.btn_editor_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Name = "menuItemHelp";
            resources.ApplyResources(this.menuItemHelp, "menuItemHelp");
            this.menuItemHelp.Click += new System.EventHandler(this.bunifuImageButton3_Click);
            // 
            // thide
            // 
            this.thide.AnimationType = BunifuAnimatorNS.AnimationType.HorizSlide;
            this.thide.Cursor = null;
            animation2.AnimateOnlyDifferences = true;
            animation2.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.BlindCoeff")));
            animation2.LeafCoeff = 0F;
            animation2.MaxTime = 1F;
            animation2.MinTime = 0F;
            animation2.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicCoeff")));
            animation2.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicShift")));
            animation2.MosaicSize = 0;
            animation2.Padding = new System.Windows.Forms.Padding(0);
            animation2.RotateCoeff = 0F;
            animation2.RotateLimit = 0F;
            animation2.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.ScaleCoeff")));
            animation2.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.SlideCoeff")));
            animation2.TimeCoeff = 0F;
            animation2.TransparencyCoeff = 0F;
            this.thide.DefaultAnimation = animation2;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ControlBox = false;
            this.Controls.Add(this.panel_info);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.nav_menu_left);
            this.thide.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.miniToolStrip;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.nav_menu_left.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton4)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panel_info.ResumeLayout(false);
            this.panel_info.PerformLayout();
            this.dropdownMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.FlowLayoutPanel nav_menu_left;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton4;
        private System.Windows.Forms.Panel panel_info;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_header_title;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private DevExpress.XtraEditors.CalcEdit calcEdit1;
        private System.Windows.Forms.Label lbl_aboutinfo;
        private BunifuAnimatorNS.BunifuTransition tshow;
        private BunifuAnimatorNS.BunifuTransition thide;
        private Bunifu.Framework.UI.BunifuFormFadeTransition navigationFadeTransition1;
        public MetroFramework.Controls.MetroDateTime today_date;
        public Bunifu.Framework.UI.BunifuCustomLabel lbl_dbname;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuFlatButton menu_signout;
        private Bunifu.Framework.UI.BunifuFlatButton vmenu_dashboard;
        private Bunifu.Framework.UI.BunifuFlatButton vmenu_reports;
        private Bunifu.Framework.UI.BunifuFlatButton menu_account_profile;
        private Bunifu.Framework.UI.BunifuFlatButton menu_settings;
        private Bunifu.Framework.UI.BunifuFlatButton vmenu_profile;
        private Bunifu.Framework.UI.BunifuFlatButton vmenu_pos;
        private Bunifu.Framework.UI.BunifuFlatButton vmenu_augrai;
        private Bunifu.Framework.UI.BunifuFlatButton vmenu_ledger;
        private Bunifu.Framework.UI.BunifuFlatButton vmenu_invoicing;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dashboardCTRL1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pOSCTRL3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invoicingCTRL4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem augraiCTRL6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cashINOUTCTRL7ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profilesCTRL8ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportCTRL9ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cTRL0ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip miniToolStrip;
        private Bunifu.Framework.UI.BunifuFlatButton vmenu_rent;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton5;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton2;
        private System.Windows.Forms.Label lblUserInfo;
        private Panel panel3;
        private System.Windows.Forms.ContextMenuStrip dropdownMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemReports;
        private System.Windows.Forms.ToolStripMenuItem menuItemShopDaily;
        private System.Windows.Forms.ToolStripMenuItem menuItemNotifications;
        private System.Windows.Forms.ToolStripMenuItem menuItemLog;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditor;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem languageToolStripMenuItem;
        private ToolStripMenuItem englishToolStripMenuItem;
        private ToolStripMenuItem urduToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private ToolStripMenuItem gdriveToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem shopIncomeToolStripMenuItem;
        private ToolStripMenuItem hELPToolStripMenuItem1;
        private ToolStripMenuItem lOGToolStripMenuItem;
        private ToolStripMenuItem cONTRACTSToolStripMenuItem;
        private ToolStripMenuItem kEYBOARDToolStripMenuItem;
        private ToolStripMenuItem notificationToolStripMenuItem;
    }
}