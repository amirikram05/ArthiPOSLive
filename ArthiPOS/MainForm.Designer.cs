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
            BunifuAnimatorNS.Animation animation3 = new BunifuAnimatorNS.Animation();
            BunifuAnimatorNS.Animation animation4 = new BunifuAnimatorNS.Animation();
            this.nav_menu_left = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.panel2 = new System.Windows.Forms.Panel();
            this.vmenu_billpaidout = new Bunifu.Framework.UI.BunifuFlatButton();
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
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.google_btn_drive = new System.Windows.Forms.LinkLabel();
            this.lbl_dbname = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.today_date = new MetroFramework.Controls.MetroDateTime();
            this.combo_lang = new System.Windows.Forms.ComboBox();
            this.calcEdit1 = new DevExpress.XtraEditors.CalcEdit();
            this.lbl_header_title = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuImageButton3 = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuImageButton4 = new Bunifu.Framework.UI.BunifuImageButton();
            this.panel_info = new System.Windows.Forms.Panel();
            this.navigationFadeTransition1 = new Bunifu.Framework.UI.BunifuFormFadeTransition(this.components);
            this.tshow = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.miniToolStrip = new System.Windows.Forms.MenuStrip();
            this.thide = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.nav_menu_left.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton4)).BeginInit();
            this.SuspendLayout();
            // 
            // nav_menu_left
            // 
            resources.ApplyResources(this.nav_menu_left, "nav_menu_left");
            this.nav_menu_left.BackColor = System.Drawing.Color.White;
            this.nav_menu_left.Controls.Add(this.pictureBox2);
            this.nav_menu_left.Controls.Add(this.materialDivider1);
            this.nav_menu_left.Controls.Add(this.panel2);
            this.nav_menu_left.Controls.Add(this.materialDivider2);
            this.nav_menu_left.Controls.Add(this.label1);
            this.tshow.SetDecoration(this.nav_menu_left, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.nav_menu_left, BunifuAnimatorNS.DecorationType.None);
            this.nav_menu_left.Name = "nav_menu_left";
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
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.Gray;
            this.thide.SetDecoration(this.materialDivider1, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.materialDivider1, BunifuAnimatorNS.DecorationType.None);
            this.materialDivider1.Depth = 0;
            resources.ApplyResources(this.materialDivider1, "materialDivider1");
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.vmenu_billpaidout);
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
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // vmenu_billpaidout
            // 
            this.vmenu_billpaidout.Activecolor = System.Drawing.Color.WhiteSmoke;
            this.vmenu_billpaidout.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.vmenu_billpaidout, "vmenu_billpaidout");
            this.vmenu_billpaidout.BorderRadius = 7;
            this.vmenu_billpaidout.ButtonText = "Bill Paidout";
            this.vmenu_billpaidout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tshow.SetDecoration(this.vmenu_billpaidout, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.vmenu_billpaidout, BunifuAnimatorNS.DecorationType.None);
            this.vmenu_billpaidout.DisabledColor = System.Drawing.Color.Gray;
            this.vmenu_billpaidout.Iconcolor = System.Drawing.Color.Transparent;
            this.vmenu_billpaidout.Iconimage = null;
            this.vmenu_billpaidout.Iconimage_right = null;
            this.vmenu_billpaidout.Iconimage_right_Selected = null;
            this.vmenu_billpaidout.Iconimage_Selected = null;
            this.vmenu_billpaidout.IconMarginLeft = 0;
            this.vmenu_billpaidout.IconMarginRight = 0;
            this.vmenu_billpaidout.IconRightVisible = false;
            this.vmenu_billpaidout.IconRightZoom = 0D;
            this.vmenu_billpaidout.IconVisible = false;
            this.vmenu_billpaidout.IconZoom = 90D;
            this.vmenu_billpaidout.IsTab = true;
            this.vmenu_billpaidout.Name = "vmenu_billpaidout";
            this.vmenu_billpaidout.Normalcolor = System.Drawing.Color.White;
            this.vmenu_billpaidout.OnHovercolor = System.Drawing.Color.White;
            this.vmenu_billpaidout.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(51)))));
            this.vmenu_billpaidout.selected = false;
            this.vmenu_billpaidout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vmenu_billpaidout.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.vmenu_billpaidout.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.vmenu_billpaidout.Click += new System.EventHandler(this.vmenu_billpaidout_Click);
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
            // materialDivider2
            // 
            this.materialDivider2.BackColor = System.Drawing.Color.Gray;
            this.thide.SetDecoration(this.materialDivider2, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.materialDivider2, BunifuAnimatorNS.DecorationType.None);
            this.materialDivider2.Depth = 0;
            resources.ApplyResources(this.materialDivider2, "materialDivider2");
            this.materialDivider2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider2.Name = "materialDivider2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.tshow.SetDecoration(this.label1, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.label1, BunifuAnimatorNS.DecorationType.None);
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.google_btn_drive);
            this.panel1.Controls.Add(this.lbl_dbname);
            this.panel1.Controls.Add(this.today_date);
            this.panel1.Controls.Add(this.combo_lang);
            this.panel1.Controls.Add(this.calcEdit1);
            this.panel1.Controls.Add(this.lbl_header_title);
            this.panel1.Controls.Add(this.bunifuImageButton1);
            this.panel1.Controls.Add(this.bunifuImageButton3);
            this.panel1.Controls.Add(this.bunifuImageButton4);
            this.thide.SetDecoration(this.panel1, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.panel1, BunifuAnimatorNS.DecorationType.None);
            this.panel1.Name = "panel1";
            // 
            // google_btn_drive
            // 
            this.tshow.SetDecoration(this.google_btn_drive, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.google_btn_drive, BunifuAnimatorNS.DecorationType.None);
            resources.ApplyResources(this.google_btn_drive, "google_btn_drive");
            this.google_btn_drive.Name = "google_btn_drive";
            this.google_btn_drive.TabStop = true;
            this.google_btn_drive.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.google_btn_drive_LinkClicked);
            // 
            // lbl_dbname
            // 
            resources.ApplyResources(this.lbl_dbname, "lbl_dbname");
            this.tshow.SetDecoration(this.lbl_dbname, BunifuAnimatorNS.DecorationType.None);
            this.thide.SetDecoration(this.lbl_dbname, BunifuAnimatorNS.DecorationType.None);
            this.lbl_dbname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbl_dbname.Name = "lbl_dbname";
            // 
            // today_date
            // 
            resources.ApplyResources(this.today_date, "today_date");
            this.thide.SetDecoration(this.today_date, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.today_date, BunifuAnimatorNS.DecorationType.None);
            this.today_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.today_date.Name = "today_date";
            this.today_date.ValueChanged += new System.EventHandler(this.today_date_ValueChanged);
            // 
            // combo_lang
            // 
            resources.ApplyResources(this.combo_lang, "combo_lang");
            this.thide.SetDecoration(this.combo_lang, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.combo_lang, BunifuAnimatorNS.DecorationType.None);
            this.combo_lang.FormattingEnabled = true;
            this.combo_lang.Items.AddRange(new object[] {
            resources.GetString("combo_lang.Items"),
            resources.GetString("combo_lang.Items1")});
            this.combo_lang.Name = "combo_lang";
            this.combo_lang.SelectedIndexChanged += new System.EventHandler(this.combo_lang_SelectedIndexChanged);
            // 
            // calcEdit1
            // 
            resources.ApplyResources(this.calcEdit1, "calcEdit1");
            this.thide.SetDecoration(this.calcEdit1, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.calcEdit1, BunifuAnimatorNS.DecorationType.None);
            this.calcEdit1.Name = "calcEdit1";
            this.calcEdit1.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("calcEdit1.Properties.Appearance.Font")));
            this.calcEdit1.Properties.Appearance.Options.UseFont = true;
            this.calcEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("calcEdit1.Properties.Buttons"))))});
            this.calcEdit1.Properties.ShowCloseButton = true;
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
            // bunifuImageButton3
            // 
            resources.ApplyResources(this.bunifuImageButton3, "bunifuImageButton3");
            this.bunifuImageButton3.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thide.SetDecoration(this.bunifuImageButton3, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.bunifuImageButton3, BunifuAnimatorNS.DecorationType.None);
            this.bunifuImageButton3.Image = global::ArthiPOS.Properties.Resources.help1;
            this.bunifuImageButton3.ImageActive = null;
            this.bunifuImageButton3.Name = "bunifuImageButton3";
            this.bunifuImageButton3.TabStop = false;
            this.bunifuImageButton3.Zoom = 10;
            this.bunifuImageButton3.Click += new System.EventHandler(this.bunifuImageButton3_Click);
            // 
            // bunifuImageButton4
            // 
            resources.ApplyResources(this.bunifuImageButton4, "bunifuImageButton4");
            this.bunifuImageButton4.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thide.SetDecoration(this.bunifuImageButton4, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.bunifuImageButton4, BunifuAnimatorNS.DecorationType.None);
            this.bunifuImageButton4.Image = global::ArthiPOS.Properties.Resources.closeb;
            this.bunifuImageButton4.ImageActive = null;
            this.bunifuImageButton4.Name = "bunifuImageButton4";
            this.bunifuImageButton4.TabStop = false;
            this.bunifuImageButton4.Zoom = 10;
            this.bunifuImageButton4.Click += new System.EventHandler(this.bunifuImageButton4_Click);
            // 
            // panel_info
            // 
            this.panel_info.BackColor = System.Drawing.Color.White;
            this.thide.SetDecoration(this.panel_info, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.panel_info, BunifuAnimatorNS.DecorationType.None);
            resources.ApplyResources(this.panel_info, "panel_info");
            this.panel_info.Name = "panel_info";
            // 
            // navigationFadeTransition1
            // 
            this.navigationFadeTransition1.Delay = 2;
            // 
            // tshow
            // 
            this.tshow.AnimationType = BunifuAnimatorNS.AnimationType.HorizSlide;
            this.tshow.Cursor = null;
            animation3.AnimateOnlyDifferences = true;
            animation3.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.BlindCoeff")));
            animation3.LeafCoeff = 0F;
            animation3.MaxTime = 1F;
            animation3.MinTime = 0F;
            animation3.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.MosaicCoeff")));
            animation3.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation3.MosaicShift")));
            animation3.MosaicSize = 0;
            animation3.Padding = new System.Windows.Forms.Padding(0);
            animation3.RotateCoeff = 0F;
            animation3.RotateLimit = 0F;
            animation3.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.ScaleCoeff")));
            animation3.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.SlideCoeff")));
            animation3.TimeCoeff = 0F;
            animation3.TransparencyCoeff = 0F;
            this.tshow.DefaultAnimation = animation3;
            // 
            // miniToolStrip
            // 
            this.thide.SetDecoration(this.miniToolStrip, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this.miniToolStrip, BunifuAnimatorNS.DecorationType.None);
            resources.ApplyResources(this.miniToolStrip, "miniToolStrip");
            this.miniToolStrip.Name = "miniToolStrip";
            // 
            // thide
            // 
            this.thide.AnimationType = BunifuAnimatorNS.AnimationType.HorizSlide;
            this.thide.Cursor = null;
            animation4.AnimateOnlyDifferences = true;
            animation4.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation4.BlindCoeff")));
            animation4.LeafCoeff = 0F;
            animation4.MaxTime = 1F;
            animation4.MinTime = 0F;
            animation4.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation4.MosaicCoeff")));
            animation4.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation4.MosaicShift")));
            animation4.MosaicSize = 0;
            animation4.Padding = new System.Windows.Forms.Padding(0);
            animation4.RotateCoeff = 0F;
            animation4.RotateLimit = 0F;
            animation4.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation4.ScaleCoeff")));
            animation4.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation4.SlideCoeff")));
            animation4.TimeCoeff = 0F;
            animation4.TransparencyCoeff = 0F;
            this.thide.DefaultAnimation = animation4;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ControlBox = false;
            this.Controls.Add(this.panel_info);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.nav_menu_left);
            this.thide.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.tshow.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.miniToolStrip;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.nav_menu_left.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.FlowLayoutPanel nav_menu_left;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton3;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton4;
        private System.Windows.Forms.Panel panel_info;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_header_title;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private DevExpress.XtraEditors.CalcEdit calcEdit1;
        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private BunifuAnimatorNS.BunifuTransition tshow;
        private BunifuAnimatorNS.BunifuTransition thide;
        private Bunifu.Framework.UI.BunifuFormFadeTransition navigationFadeTransition1;
        private System.Windows.Forms.ComboBox combo_lang;
        public MetroFramework.Controls.MetroDateTime today_date;
        public Bunifu.Framework.UI.BunifuCustomLabel lbl_dbname;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuFlatButton vmenu_billpaidout;
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
        private System.Windows.Forms.LinkLabel google_btn_drive;
    }
}
