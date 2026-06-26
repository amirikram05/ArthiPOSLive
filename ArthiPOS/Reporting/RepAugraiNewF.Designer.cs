namespace ArthiPOS.Reporting
{
    partial class RepAugraiNewF
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
            this.crystal_view_customer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.chk_printall = new System.Windows.Forms.CheckBox();
            this.date_start = new MetroFramework.Controls.MetroDateTime();
            this.chk_full_detail = new System.Windows.Forms.CheckBox();
            this.rb_client = new System.Windows.Forms.RadioButton();
            this.rb_customer = new System.Windows.Forms.RadioButton();
            this.chk_saleadvance = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroContextMenu1 = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cust_panel = new System.Windows.Forms.Panel();
            this.def_to = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.def_from = new System.Windows.Forms.TextBox();
            this.bt_browse_print = new System.Windows.Forms.Button();
            this.comb_groupby = new System.Windows.Forms.ComboBox();
            this.btn_pagesetup = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.cust_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // crystal_view_customer
            // 
            this.crystal_view_customer.ActiveViewIndex = -1;
            this.crystal_view_customer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystal_view_customer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystal_view_customer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystal_view_customer.Location = new System.Drawing.Point(0, 24);
            this.crystal_view_customer.Name = "crystal_view_customer";
            this.crystal_view_customer.Size = new System.Drawing.Size(1187, 608);
            this.crystal_view_customer.TabIndex = 0;
            this.crystal_view_customer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crystal_view_customer.Load += new System.EventHandler(this.crystal_view_customer_Load);
            // 
            // chk_printall
            // 
            this.chk_printall.AutoSize = true;
            this.chk_printall.Checked = true;
            this.chk_printall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_printall.Location = new System.Drawing.Point(292, 6);
            this.chk_printall.Name = "chk_printall";
            this.chk_printall.Size = new System.Drawing.Size(61, 17);
            this.chk_printall.TabIndex = 1;
            this.chk_printall.Text = "Print All";
            this.chk_printall.UseVisualStyleBackColor = true;
            this.chk_printall.CheckedChanged += new System.EventHandler(this.chk_printall_CheckedChanged);
            // 
            // date_start
            // 
            this.date_start.CustomFormat = "yyyy-MM-dd";
            this.date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_start.Location = new System.Drawing.Point(117, -2);
            this.date_start.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_start.Name = "date_start";
            this.date_start.Size = new System.Drawing.Size(103, 29);
            this.date_start.TabIndex = 116;
            this.date_start.CloseUp += new System.EventHandler(this.date_start_CloseUp);
            // 
            // chk_full_detail
            // 
            this.chk_full_detail.Appearance = System.Windows.Forms.Appearance.Button;
            this.chk_full_detail.Checked = true;
            this.chk_full_detail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_full_detail.Location = new System.Drawing.Point(505, 3);
            this.chk_full_detail.Name = "chk_full_detail";
            this.chk_full_detail.Size = new System.Drawing.Size(98, 20);
            this.chk_full_detail.TabIndex = 117;
            this.chk_full_detail.Text = "Print Full Detail";
            this.chk_full_detail.UseVisualStyleBackColor = true;
            this.chk_full_detail.CheckedChanged += new System.EventHandler(this.chk_full_detail_CheckedChanged);
            // 
            // rb_client
            // 
            this.rb_client.AutoSize = true;
            this.rb_client.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_client.Location = new System.Drawing.Point(442, 4);
            this.rb_client.Name = "rb_client";
            this.rb_client.Size = new System.Drawing.Size(61, 21);
            this.rb_client.TabIndex = 118;
            this.rb_client.TabStop = true;
            this.rb_client.Text = "Client";
            this.rb_client.UseVisualStyleBackColor = true;
            this.rb_client.Click += new System.EventHandler(this.rd_check_Click);
            // 
            // rb_customer
            // 
            this.rb_customer.AutoSize = true;
            this.rb_customer.Checked = true;
            this.rb_customer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_customer.Location = new System.Drawing.Point(355, 4);
            this.rb_customer.Name = "rb_customer";
            this.rb_customer.Size = new System.Drawing.Size(86, 21);
            this.rb_customer.TabIndex = 119;
            this.rb_customer.TabStop = true;
            this.rb_customer.Text = "Customer";
            this.rb_customer.UseVisualStyleBackColor = true;
            this.rb_customer.Click += new System.EventHandler(this.rd_check_Click);
            // 
            // chk_saleadvance
            // 
            this.chk_saleadvance.AutoSize = true;
            this.chk_saleadvance.Checked = true;
            this.chk_saleadvance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_saleadvance.Location = new System.Drawing.Point(608, 5);
            this.chk_saleadvance.Name = "chk_saleadvance";
            this.chk_saleadvance.Size = new System.Drawing.Size(95, 17);
            this.chk_saleadvance.TabIndex = 120;
            this.chk_saleadvance.Text = "Sale/Advance";
            this.chk_saleadvance.UseVisualStyleBackColor = true;
            this.chk_saleadvance.CheckedChanged += new System.EventHandler(this.chk_saleadvance_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1187, 24);
            this.menuStrip1.TabIndex = 121;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pDFToolStripMenuItem,
            this.hTMLToolStripMenuItem,
            this.excelToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.saveToolStripMenuItem.Text = "Save File";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // pDFToolStripMenuItem
            // 
            this.pDFToolStripMenuItem.Name = "pDFToolStripMenuItem";
            this.pDFToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.pDFToolStripMenuItem.Text = "PDF";
            this.pDFToolStripMenuItem.Click += new System.EventHandler(this.pDFToolStripMenuItem_Click);
            // 
            // hTMLToolStripMenuItem
            // 
            this.hTMLToolStripMenuItem.Name = "hTMLToolStripMenuItem";
            this.hTMLToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.hTMLToolStripMenuItem.Text = "HTML";
            this.hTMLToolStripMenuItem.Click += new System.EventHandler(this.hTMLToolStripMenuItem_Click);
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.excelToolStripMenuItem.Text = "Excel";
            this.excelToolStripMenuItem.Click += new System.EventHandler(this.excelToolStripMenuItem_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.BackgroundImage = global::ArthiPOS.Properties.Resources.previou;
            this.metroButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.metroButton2.Location = new System.Drawing.Point(230, 1);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(25, 23);
            this.metroButton2.TabIndex = 122;
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.previousdate_Click);
            // 
            // metroButton1
            // 
            this.metroButton1.BackgroundImage = global::ArthiPOS.Properties.Resources.next;
            this.metroButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.metroButton1.Location = new System.Drawing.Point(261, 1);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(25, 23);
            this.metroButton1.TabIndex = 123;
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.nextdate_Click);
            // 
            // metroContextMenu1
            // 
            this.metroContextMenu1.Name = "metroContextMenu1";
            this.metroContextMenu1.Size = new System.Drawing.Size(61, 4);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 126;
            this.label1.Text = "Defaulter";
            // 
            // cust_panel
            // 
            this.cust_panel.Controls.Add(this.def_to);
            this.cust_panel.Controls.Add(this.label2);
            this.cust_panel.Controls.Add(this.def_from);
            this.cust_panel.Controls.Add(this.label1);
            this.cust_panel.Location = new System.Drawing.Point(713, -1);
            this.cust_panel.Name = "cust_panel";
            this.cust_panel.Size = new System.Drawing.Size(194, 27);
            this.cust_panel.TabIndex = 130;
            // 
            // def_to
            // 
            this.def_to.Location = new System.Drawing.Point(137, 4);
            this.def_to.Name = "def_to";
            this.def_to.Size = new System.Drawing.Size(54, 20);
            this.def_to.TabIndex = 129;
            this.def_to.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(105, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 15);
            this.label2.TabIndex = 128;
            this.label2.Text = "TO";
            // 
            // def_from
            // 
            this.def_from.Location = new System.Drawing.Point(56, 3);
            this.def_from.Name = "def_from";
            this.def_from.Size = new System.Drawing.Size(42, 20);
            this.def_from.TabIndex = 127;
            this.def_from.Text = "7";
            // 
            // bt_browse_print
            // 
            this.bt_browse_print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_browse_print.Location = new System.Drawing.Point(1066, 3);
            this.bt_browse_print.Name = "bt_browse_print";
            this.bt_browse_print.Size = new System.Drawing.Size(109, 23);
            this.bt_browse_print.TabIndex = 131;
            this.bt_browse_print.Text = "Browser Print";
            this.bt_browse_print.UseVisualStyleBackColor = true;
            this.bt_browse_print.Click += new System.EventHandler(this.bt_browse_print_Click);
            // 
            // comb_groupby
            // 
            this.comb_groupby.FormattingEnabled = true;
            this.comb_groupby.Location = new System.Drawing.Point(909, 3);
            this.comb_groupby.Name = "comb_groupby";
            this.comb_groupby.Size = new System.Drawing.Size(151, 21);
            this.comb_groupby.TabIndex = 132;
            this.comb_groupby.Text = "GroupBy";
            this.comb_groupby.SelectedIndexChanged += new System.EventHandler(this.comb_groupby_SelectedIndexChanged);
            // 
            // btn_pagesetup
            // 
            this.btn_pagesetup.BackgroundImage = global::ArthiPOS.Properties.Resources.Print;
            this.btn_pagesetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_pagesetup.Location = new System.Drawing.Point(71, -2);
            this.btn_pagesetup.Name = "btn_pagesetup";
            this.btn_pagesetup.Size = new System.Drawing.Size(30, 29);
            this.btn_pagesetup.TabIndex = 133;
            this.btn_pagesetup.UseVisualStyleBackColor = true;
            this.btn_pagesetup.Click += new System.EventHandler(this.btn_pagesetup_Click);
            // 
            // RepAugraiNewF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1187, 632);
            this.Controls.Add(this.btn_pagesetup);
            this.Controls.Add(this.comb_groupby);
            this.Controls.Add(this.bt_browse_print);
            this.Controls.Add(this.cust_panel);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.chk_saleadvance);
            this.Controls.Add(this.rb_customer);
            this.Controls.Add(this.rb_client);
            this.Controls.Add(this.chk_full_detail);
            this.Controls.Add(this.date_start);
            this.Controls.Add(this.chk_printall);
            this.Controls.Add(this.crystal_view_customer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RepAugraiNewF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.RepAugrai_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cust_panel.ResumeLayout(false);
            this.cust_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystal_view_customer;
        private System.Windows.Forms.CheckBox chk_printall;
        private MetroFramework.Controls.MetroDateTime date_start;
        private System.Windows.Forms.CheckBox chk_full_detail;
        private System.Windows.Forms.RadioButton rb_client;
        private System.Windows.Forms.RadioButton rb_customer;
        private System.Windows.Forms.CheckBox chk_saleadvance;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroContextMenu metroContextMenu1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel cust_panel;
        private System.Windows.Forms.TextBox def_from;
        private System.Windows.Forms.TextBox def_to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_browse_print;
        private System.Windows.Forms.ComboBox comb_groupby;
        private System.Windows.Forms.Button btn_pagesetup;
    }
}
