using System;

namespace ArthiPOS.Controls.dashboard
{
    partial class ReportControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menu_admin = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_admin_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_admin_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_admin_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_admin_4 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_admin_5 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_admin_6 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_admin_8 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_admin_9 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_bipari = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_bipari_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_bipari_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_bipari_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_bipari_4 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_customer = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_customer_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_customer_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_customer_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_select_name = new System.Windows.Forms.Label();
            this.menu_panel = new System.Windows.Forms.Panel();
            this.check_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_date = new System.Windows.Forms.CheckBox();
            this.chk_id = new System.Windows.Forms.CheckBox();
            this.chk_name = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rd_none = new System.Windows.Forms.RadioButton();
            this.rd_product = new System.Windows.Forms.RadioButton();
            this.rd_city = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.date_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.date_last = new MetroFramework.Controls.MetroDateTime();
            this.lbl_end = new System.Windows.Forms.Label();
            this.date_start = new MetroFramework.Controls.MetroDateTime();
            this.lbl_start = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chk_print_all = new System.Windows.Forms.CheckBox();
            this.btn_load = new System.Windows.Forms.Button();
            this.lbl_page_size = new System.Windows.Forms.Label();
            this.pnlPager = new System.Windows.Forms.Panel();
            this.btn_print_report = new Bunifu.Framework.UI.BunifuFlatButton();
            this.grid_report = new System.Windows.Forms.DataGridView();
            this.txt_name = new ArthiPOS.Controls.UrduTextBox();
            this.txt_page_size = new ArthiPOS.Controls.UrduTextBox();
            this.toolStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.menu_panel.SuspendLayout();
            this.check_panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.date_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_report)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_admin,
            this.menu_bipari,
            this.menu_customer});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1176, 52);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // menu_admin
            // 
            this.menu_admin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.menu_admin_1,
            this.menu_admin_2,
            this.menu_admin_3,
            this.menu_admin_4,
            this.menu_admin_5,
            this.menu_admin_6,
            this.menu_admin_8,
            this.menu_admin_9});
            this.menu_admin.Image = ((System.Drawing.Image)(resources.GetObject("menu_admin.Image")));
            this.menu_admin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menu_admin.Name = "menu_admin";
            this.menu_admin.Size = new System.Drawing.Size(78, 49);
            this.menu_admin.Text = "Admin";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(207, 6);
            // 
            // menu_admin_1
            // 
            this.menu_admin_1.Name = "menu_admin_1";
            this.menu_admin_1.Size = new System.Drawing.Size(210, 24);
            this.menu_admin_1.Text = "Create Season Report";
            this.menu_admin_1.Click += new System.EventHandler(this.menu_admin_1_Click);
            // 
            // menu_admin_2
            // 
            this.menu_admin_2.Name = "menu_admin_2";
            this.menu_admin_2.Size = new System.Drawing.Size(210, 24);
            this.menu_admin_2.Text = "Bipari Investment";
            this.menu_admin_2.Click += new System.EventHandler(this.menu_admin_2_Click);
            // 
            // menu_admin_3
            // 
            this.menu_admin_3.Name = "menu_admin_3";
            this.menu_admin_3.Size = new System.Drawing.Size(210, 24);
            this.menu_admin_3.Text = "Cash Received";
            this.menu_admin_3.Click += new System.EventHandler(this.menu_admin_3_Click);
            // 
            // menu_admin_4
            // 
            this.menu_admin_4.Name = "menu_admin_4";
            this.menu_admin_4.Size = new System.Drawing.Size(210, 24);
            this.menu_admin_4.Text = "Expense Detail";
            this.menu_admin_4.Click += new System.EventHandler(this.menu_admin_4_Click);
            // 
            // menu_admin_5
            // 
            this.menu_admin_5.Name = "menu_admin_5";
            this.menu_admin_5.Size = new System.Drawing.Size(210, 24);
            this.menu_admin_5.Text = "Profit/Loss";
            this.menu_admin_5.Click += new System.EventHandler(this.menu_admin_5_Click);
            // 
            // menu_admin_6
            // 
            this.menu_admin_6.Name = "menu_admin_6";
            this.menu_admin_6.Size = new System.Drawing.Size(210, 24);
            this.menu_admin_6.Text = "Balance Sheet Report";
            this.menu_admin_6.Click += new System.EventHandler(this.menu_admin_6_Click);
            // 
            // menu_admin_8
            // 
            this.menu_admin_8.Name = "menu_admin_8";
            this.menu_admin_8.Size = new System.Drawing.Size(210, 24);
            this.menu_admin_8.Text = "Augrai Recovery";
            this.menu_admin_8.Click += new System.EventHandler(this.menu_admin_8_Click);
            // 
            // menu_admin_9
            // 
            this.menu_admin_9.Name = "menu_admin_9";
            this.menu_admin_9.Size = new System.Drawing.Size(210, 24);
            this.menu_admin_9.Text = "Investment Recovery";
            this.menu_admin_9.Click += new System.EventHandler(this.menu_admin_9_Click);
            // 
            // menu_bipari
            // 
            this.menu_bipari.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.menu_bipari_1,
            this.menu_bipari_2,
            this.menu_bipari_3,
            this.menu_bipari_4});
            this.menu_bipari.Image = ((System.Drawing.Image)(resources.GetObject("menu_bipari.Image")));
            this.menu_bipari.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menu_bipari.Name = "menu_bipari";
            this.menu_bipari.Size = new System.Drawing.Size(72, 49);
            this.menu_bipari.Text = "Bipari";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(199, 6);
            // 
            // menu_bipari_1
            // 
            this.menu_bipari_1.Name = "menu_bipari_1";
            this.menu_bipari_1.Size = new System.Drawing.Size(202, 24);
            this.menu_bipari_1.Text = "Show All Sales";
            this.menu_bipari_1.Click += new System.EventHandler(this.menu_bipari_1_Click);
            // 
            // menu_bipari_2
            // 
            this.menu_bipari_2.Name = "menu_bipari_2";
            this.menu_bipari_2.Size = new System.Drawing.Size(202, 24);
            this.menu_bipari_2.Text = "Investment";
            this.menu_bipari_2.Click += new System.EventHandler(this.menu_bipari_2_Click);
            // 
            // menu_bipari_3
            // 
            this.menu_bipari_3.Name = "menu_bipari_3";
            this.menu_bipari_3.Size = new System.Drawing.Size(202, 24);
            this.menu_bipari_3.Text = "List Of Bipari ";
            this.menu_bipari_3.Click += new System.EventHandler(this.menu_bipari_3_Click);
            // 
            // menu_bipari_4
            // 
            this.menu_bipari_4.Name = "menu_bipari_4";
            this.menu_bipari_4.Size = new System.Drawing.Size(202, 24);
            this.menu_bipari_4.Text = "Commisison/Chongi";
            this.menu_bipari_4.Click += new System.EventHandler(this.menu_bipari_4_Click);
            // 
            // menu_customer
            // 
            this.menu_customer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.menu_customer_1,
            this.menu_customer_2,
            this.menu_customer_3});
            this.menu_customer.Image = ((System.Drawing.Image)(resources.GetObject("menu_customer.Image")));
            this.menu_customer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menu_customer.Name = "menu_customer";
            this.menu_customer.Size = new System.Drawing.Size(98, 49);
            this.menu_customer.Text = "Customer";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
            // 
            // menu_customer_1
            // 
            this.menu_customer_1.Name = "menu_customer_1";
            this.menu_customer_1.Size = new System.Drawing.Size(202, 24);
            this.menu_customer_1.Text = "Show All Sales";
            this.menu_customer_1.Click += new System.EventHandler(this.menu_customer_1_Click);
            // 
            // menu_customer_2
            // 
            this.menu_customer_2.Name = "menu_customer_2";
            this.menu_customer_2.Size = new System.Drawing.Size(202, 24);
            this.menu_customer_2.Text = "List Of Customer";
            this.menu_customer_2.Click += new System.EventHandler(this.menu_customer_2_Click);
            // 
            // menu_customer_3
            // 
            this.menu_customer_3.Name = "menu_customer_3";
            this.menu_customer_3.Size = new System.Drawing.Size(202, 24);
            this.menu_customer_3.Text = "Commission/Chongi";
            this.menu_customer_3.Click += new System.EventHandler(this.menu_customer_3_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lbl_select_name);
            this.flowLayoutPanel1.Controls.Add(this.menu_panel);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 52);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1176, 138);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // lbl_select_name
            // 
            this.lbl_select_name.AutoSize = true;
            this.lbl_select_name.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_select_name.Location = new System.Drawing.Point(3, 0);
            this.lbl_select_name.Name = "lbl_select_name";
            this.lbl_select_name.Size = new System.Drawing.Size(50, 21);
            this.lbl_select_name.TabIndex = 260;
            this.lbl_select_name.Text = "Menu";
            // 
            // menu_panel
            // 
            this.menu_panel.Controls.Add(this.check_panel);
            this.menu_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu_panel.Location = new System.Drawing.Point(3, 24);
            this.menu_panel.Name = "menu_panel";
            this.menu_panel.Size = new System.Drawing.Size(1176, 56);
            this.menu_panel.TabIndex = 249;
            // 
            // check_panel
            // 
            this.check_panel.Controls.Add(this.label1);
            this.check_panel.Controls.Add(this.chk_date);
            this.check_panel.Controls.Add(this.chk_id);
            this.check_panel.Controls.Add(this.chk_name);
            this.check_panel.Controls.Add(this.groupBox1);
            this.check_panel.Controls.Add(this.flowLayoutPanel2);
            this.check_panel.Location = new System.Drawing.Point(6, 3);
            this.check_panel.Name = "check_panel";
            this.check_panel.Size = new System.Drawing.Size(1168, 45);
            this.check_panel.TabIndex = 121;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 25);
            this.label1.TabIndex = 122;
            this.label1.Text = "Search By";
            // 
            // chk_date
            // 
            this.chk_date.AutoSize = true;
            this.chk_date.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_date.Location = new System.Drawing.Point(106, 10);
            this.chk_date.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.chk_date.Name = "chk_date";
            this.chk_date.Size = new System.Drawing.Size(61, 25);
            this.chk_date.TabIndex = 120;
            this.chk_date.Text = "Date";
            this.chk_date.UseVisualStyleBackColor = true;
            this.chk_date.CheckedChanged += new System.EventHandler(this.chk_date_CheckedChanged);
            // 
            // chk_id
            // 
            this.chk_id.AutoSize = true;
            this.chk_id.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_id.Location = new System.Drawing.Point(167, 10);
            this.chk_id.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.chk_id.Name = "chk_id";
            this.chk_id.Size = new System.Drawing.Size(44, 25);
            this.chk_id.TabIndex = 121;
            this.chk_id.Text = "ID";
            this.chk_id.UseVisualStyleBackColor = true;
            this.chk_id.CheckedChanged += new System.EventHandler(this.chk_khataid_CheckedChanged);
            // 
            // chk_name
            // 
            this.chk_name.AutoSize = true;
            this.chk_name.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_name.Location = new System.Drawing.Point(211, 10);
            this.chk_name.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.chk_name.Name = "chk_name";
            this.chk_name.Size = new System.Drawing.Size(71, 25);
            this.chk_name.TabIndex = 122;
            this.chk_name.Text = "Name";
            this.chk_name.UseVisualStyleBackColor = true;
            this.chk_name.CheckedChanged += new System.EventHandler(this.chk_name_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rd_none);
            this.groupBox1.Controls.Add(this.rd_product);
            this.groupBox1.Controls.Add(this.rd_city);
            this.groupBox1.Location = new System.Drawing.Point(285, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 32);
            this.groupBox1.TabIndex = 261;
            this.groupBox1.TabStop = false;
            // 
            // rd_none
            // 
            this.rd_none.Checked = true;
            this.rd_none.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_none.Location = new System.Drawing.Point(6, 8);
            this.rd_none.Name = "rd_none";
            this.rd_none.Size = new System.Drawing.Size(62, 24);
            this.rd_none.TabIndex = 2;
            this.rd_none.TabStop = true;
            this.rd_none.Text = "None";
            this.rd_none.UseVisualStyleBackColor = true;
            // 
            // rd_product
            // 
            this.rd_product.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_product.Location = new System.Drawing.Point(129, 9);
            this.rd_product.Name = "rd_product";
            this.rd_product.Size = new System.Drawing.Size(78, 24);
            this.rd_product.TabIndex = 1;
            this.rd_product.Text = "Product";
            this.rd_product.UseVisualStyleBackColor = true;
            // 
            // rd_city
            // 
            this.rd_city.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_city.Location = new System.Drawing.Point(74, 9);
            this.rd_city.Name = "rd_city";
            this.rd_city.Size = new System.Drawing.Size(54, 24);
            this.rd_city.TabIndex = 0;
            this.rd_city.Text = "City";
            this.rd_city.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.txt_name);
            this.flowLayoutPanel2.Controls.Add(this.date_panel);
            this.flowLayoutPanel2.Controls.Add(this.btn_search);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(498, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(622, 42);
            this.flowLayoutPanel2.TabIndex = 8;
            // 
            // date_panel
            // 
            this.date_panel.Controls.Add(this.date_last);
            this.date_panel.Controls.Add(this.lbl_end);
            this.date_panel.Controls.Add(this.date_start);
            this.date_panel.Controls.Add(this.lbl_start);
            this.date_panel.Location = new System.Drawing.Point(170, 3);
            this.date_panel.Name = "date_panel";
            this.date_panel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.date_panel.Size = new System.Drawing.Size(363, 36);
            this.date_panel.TabIndex = 118;
            // 
            // date_last
            // 
            this.date_last.CustomFormat = "yyyy-MM-dd";
            this.date_last.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_last.Location = new System.Drawing.Point(257, 3);
            this.date_last.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_last.Name = "date_last";
            this.date_last.Size = new System.Drawing.Size(103, 29);
            this.date_last.TabIndex = 116;
            // 
            // lbl_end
            // 
            this.lbl_end.Location = new System.Drawing.Point(183, 0);
            this.lbl_end.Name = "lbl_end";
            this.lbl_end.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_end.Size = new System.Drawing.Size(68, 32);
            this.lbl_end.TabIndex = 117;
            this.lbl_end.Text = "Last Date";
            this.lbl_end.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // date_start
            // 
            this.date_start.CustomFormat = "yyyy-MM-dd";
            this.date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_start.Location = new System.Drawing.Point(74, 3);
            this.date_start.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_start.Name = "date_start";
            this.date_start.Size = new System.Drawing.Size(103, 29);
            this.date_start.TabIndex = 115;
            // 
            // lbl_start
            // 
            this.lbl_start.Location = new System.Drawing.Point(10, 0);
            this.lbl_start.Name = "lbl_start";
            this.lbl_start.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_start.Size = new System.Drawing.Size(58, 32);
            this.lbl_start.TabIndex = 118;
            this.lbl_start.Text = "First Date";
            this.lbl_start.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(539, 6);
            this.btn_search.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 27);
            this.btn_search.TabIndex = 119;
            this.btn_search.Text = "Submit";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chk_print_all);
            this.panel1.Controls.Add(this.btn_load);
            this.panel1.Controls.Add(this.txt_page_size);
            this.panel1.Controls.Add(this.lbl_page_size);
            this.panel1.Controls.Add(this.pnlPager);
            this.panel1.Controls.Add(this.btn_print_report);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1176, 48);
            this.panel1.TabIndex = 250;
            // 
            // chk_print_all
            // 
            this.chk_print_all.AutoSize = true;
            this.chk_print_all.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_print_all.Location = new System.Drawing.Point(325, 19);
            this.chk_print_all.Name = "chk_print_all";
            this.chk_print_all.Size = new System.Drawing.Size(75, 21);
            this.chk_print_all.TabIndex = 265;
            this.chk_print_all.Text = "Print All";
            this.chk_print_all.UseVisualStyleBackColor = true;
            this.chk_print_all.Visible = false;
            this.chk_print_all.CheckedChanged += new System.EventHandler(this.chk_print_all_CheckedChanged);
            // 
            // btn_load
            // 
            this.btn_load.BackColor = System.Drawing.Color.Transparent;
            this.btn_load.BackgroundImage = global::ArthiPOS.Properties.Resources.refresh1x;
            this.btn_load.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_load.Location = new System.Drawing.Point(281, 7);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(38, 33);
            this.btn_load.TabIndex = 264;
            this.btn_load.UseVisualStyleBackColor = false;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // lbl_page_size
            // 
            this.lbl_page_size.AutoSize = true;
            this.lbl_page_size.Location = new System.Drawing.Point(220, 5);
            this.lbl_page_size.Name = "lbl_page_size";
            this.lbl_page_size.Size = new System.Drawing.Size(55, 13);
            this.lbl_page_size.TabIndex = 262;
            this.lbl_page_size.Text = "Page Size";
            // 
            // pnlPager
            // 
            this.pnlPager.Location = new System.Drawing.Point(446, 6);
            this.pnlPager.Name = "pnlPager";
            this.pnlPager.Size = new System.Drawing.Size(284, 37);
            this.pnlPager.TabIndex = 260;
            // 
            // btn_print_report
            // 
            this.btn_print_report.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_print_report.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_print_report.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_print_report.BorderRadius = 0;
            this.btn_print_report.ButtonText = "Print Report";
            this.btn_print_report.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_print_report.DisabledColor = System.Drawing.Color.Gray;
            this.btn_print_report.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_print_report.Iconimage = null;
            this.btn_print_report.Iconimage_right = null;
            this.btn_print_report.Iconimage_right_Selected = null;
            this.btn_print_report.Iconimage_Selected = null;
            this.btn_print_report.IconMarginLeft = 0;
            this.btn_print_report.IconMarginRight = 0;
            this.btn_print_report.IconRightVisible = true;
            this.btn_print_report.IconRightZoom = 0D;
            this.btn_print_report.IconVisible = true;
            this.btn_print_report.IconZoom = 90D;
            this.btn_print_report.IsTab = false;
            this.btn_print_report.Location = new System.Drawing.Point(3, 7);
            this.btn_print_report.Name = "btn_print_report";
            this.btn_print_report.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_print_report.OnHovercolor = System.Drawing.Color.Gray;
            this.btn_print_report.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_print_report.selected = false;
            this.btn_print_report.Size = new System.Drawing.Size(215, 36);
            this.btn_print_report.TabIndex = 259;
            this.btn_print_report.Text = "Print Report";
            this.btn_print_report.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_print_report.Textcolor = System.Drawing.Color.White;
            this.btn_print_report.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print_report.Click += new System.EventHandler(this.btn_print_report_Click);
            // 
            // grid_report
            // 
            this.grid_report.AllowUserToAddRows = false;
            this.grid_report.AllowUserToDeleteRows = false;
            this.grid_report.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_report.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Jameel Noori Nastaleeq", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_report.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid_report.ColumnHeadersHeight = 30;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_report.DefaultCellStyle = dataGridViewCellStyle2;
            this.grid_report.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grid_report.Location = new System.Drawing.Point(9, 196);
            this.grid_report.Name = "grid_report";
            this.grid_report.ReadOnly = true;
            this.grid_report.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_report.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grid_report.RowTemplate.Height = 25;
            this.grid_report.Size = new System.Drawing.Size(1167, 488);
            this.grid_report.TabIndex = 261;
            // 
            // txt_name
            // 
            this.txt_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.txt_name.IsNumeric = false;
            this.txt_name.LangEnglish = false;
            this.txt_name.Location = new System.Drawing.Point(3, 6);
            this.txt_name.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.txt_name.Name = "txt_name";
            this.txt_name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_name.Size = new System.Drawing.Size(161, 27);
            this.txt_name.TabIndex = 3;
            this.txt_name.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_name.WaterMarkText = "Name";
            this.txt_name.TextChanged += new System.EventHandler(this.txt_name_TextChanged);
            // 
            // txt_page_size
            // 
            this.txt_page_size.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_page_size.IsNumeric = true;
            this.txt_page_size.LangEnglish = true;
            this.txt_page_size.Location = new System.Drawing.Point(221, 20);
            this.txt_page_size.Name = "txt_page_size";
            this.txt_page_size.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_page_size.Size = new System.Drawing.Size(54, 20);
            this.txt_page_size.TabIndex = 263;
            this.txt_page_size.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_page_size.WaterMarkText = "0";
            // 
            // ReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1176, 692);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.grid_report);
            this.Name = "ReportControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ReportControl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.menu_panel.ResumeLayout(false);
            this.check_panel.ResumeLayout(false);
            this.check_panel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.date_panel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_report)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton menu_admin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menu_admin_4;
        private System.Windows.Forms.ToolStripDropDownButton menu_bipari;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menu_bipari_1;
        private System.Windows.Forms.ToolStripMenuItem menu_bipari_2;
        private System.Windows.Forms.ToolStripMenuItem menu_bipari_3;
        private System.Windows.Forms.ToolStripDropDownButton menu_customer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menu_customer_1;
        private System.Windows.Forms.ToolStripMenuItem menu_customer_3;
        private System.Windows.Forms.ToolStripMenuItem menu_customer_2;
        private System.Windows.Forms.ToolStripMenuItem menu_bipari_4;
        private System.Windows.Forms.ToolStripMenuItem menu_admin_1;
        private System.Windows.Forms.ToolStripMenuItem menu_admin_2;
        private System.Windows.Forms.ToolStripMenuItem menu_admin_3;
        private System.Windows.Forms.ToolStripMenuItem menu_admin_5;
        private System.Windows.Forms.ToolStripMenuItem menu_admin_6;
        private System.Windows.Forms.ToolStripMenuItem menu_admin_8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel menu_panel;
        private System.Windows.Forms.FlowLayoutPanel check_panel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_date;
        private System.Windows.Forms.CheckBox chk_id;
        private System.Windows.Forms.CheckBox chk_name;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel date_panel;
        private System.Windows.Forms.Label lbl_start;
        private MetroFramework.Controls.MetroDateTime date_start;
        private System.Windows.Forms.Label lbl_end;
        private MetroFramework.Controls.MetroDateTime date_last;
        private ArthiPOS.Controls.UrduTextBox txt_name;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuFlatButton btn_print_report;
        private System.Windows.Forms.Panel pnlPager;
        private System.Windows.Forms.DataGridView grid_report;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rd_product;
        private System.Windows.Forms.RadioButton rd_city;
        private System.Windows.Forms.RadioButton rd_none;
        private System.Windows.Forms.ToolStripMenuItem menu_admin_9;
        public System.Windows.Forms.Label lbl_select_name;
        private System.Windows.Forms.Button btn_load;
        private UrduTextBox txt_page_size;
        private System.Windows.Forms.Label lbl_page_size;
        private System.Windows.Forms.CheckBox chk_print_all;
    }
}
