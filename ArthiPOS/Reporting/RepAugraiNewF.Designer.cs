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
            this.menuStrip1.SuspendLayout();
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
            this.chk_printall.Location = new System.Drawing.Point(210, 6);
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
            this.date_start.Location = new System.Drawing.Point(101, -2);
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
            this.chk_full_detail.Location = new System.Drawing.Point(450, 3);
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
            this.rb_client.Location = new System.Drawing.Point(383, 4);
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
            this.rb_customer.Location = new System.Drawing.Point(277, 4);
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
            this.chk_saleadvance.Location = new System.Drawing.Point(554, 5);
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
            this.pDFToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pDFToolStripMenuItem.Text = "PDF";
            this.pDFToolStripMenuItem.Click += new System.EventHandler(this.pDFToolStripMenuItem_Click);
            // 
            // hTMLToolStripMenuItem
            // 
            this.hTMLToolStripMenuItem.Name = "hTMLToolStripMenuItem";
            this.hTMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hTMLToolStripMenuItem.Text = "HTML";
            this.hTMLToolStripMenuItem.Click += new System.EventHandler(this.hTMLToolStripMenuItem_Click);
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.excelToolStripMenuItem.Text = "Excel";
            this.excelToolStripMenuItem.Click += new System.EventHandler(this.excelToolStripMenuItem_Click);
            // 
            // RepAugraiNewF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1187, 632);
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
    }
}
