namespace ArthiPOS.Reporting
{
    partial class RepAugrai
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
            this.rd_check = new DevExpress.XtraEditors.RadioGroup();
            this.date_start = new MetroFramework.Controls.MetroDateTime();
            this.chk_full_detail = new System.Windows.Forms.CheckBox();
            this.chk_saleadvance = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.rd_check.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // crystal_view_customer
            // 
            this.crystal_view_customer.ActiveViewIndex = -1;
            this.crystal_view_customer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystal_view_customer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystal_view_customer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystal_view_customer.Location = new System.Drawing.Point(0, 0);
            this.crystal_view_customer.Name = "crystal_view_customer";
            this.crystal_view_customer.Size = new System.Drawing.Size(1203, 671);
            this.crystal_view_customer.TabIndex = 0;
            this.crystal_view_customer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crystal_view_customer.Load += new System.EventHandler(this.crystal_view_customer_Load);
            // 
            // chk_printall
            // 
            this.chk_printall.AutoSize = true;
            this.chk_printall.Checked = true;
            this.chk_printall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_printall.Location = new System.Drawing.Point(586, 6);
            this.chk_printall.Name = "chk_printall";
            this.chk_printall.Size = new System.Drawing.Size(61, 17);
            this.chk_printall.TabIndex = 1;
            this.chk_printall.Text = "Print All";
            this.chk_printall.UseVisualStyleBackColor = true;
            this.chk_printall.CheckedChanged += new System.EventHandler(this.chk_printall_CheckedChanged);
            // 
            // rd_check
            // 
            this.rd_check.Location = new System.Drawing.Point(654, 0);
            this.rd_check.Name = "rd_check";
            this.rd_check.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Customer"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Client")});
            this.rd_check.Size = new System.Drawing.Size(159, 23);
            this.rd_check.TabIndex = 3;
            this.rd_check.SelectedIndexChanged += new System.EventHandler(this.rd_check_SelectedIndexChanged);
            // 
            // date_start
            // 
            this.date_start.CustomFormat = "yyyy-MM-dd";
            this.date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_start.Location = new System.Drawing.Point(477, 1);
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
            this.chk_full_detail.Location = new System.Drawing.Point(819, 3);
            this.chk_full_detail.Name = "chk_full_detail";
            this.chk_full_detail.Size = new System.Drawing.Size(98, 20);
            this.chk_full_detail.TabIndex = 117;
            this.chk_full_detail.Text = "Print Full Detail";
            this.chk_full_detail.UseVisualStyleBackColor = true;
            this.chk_full_detail.CheckedChanged += new System.EventHandler(this.chk_full_detail_CheckedChanged);
            // 
            // chk_saleadvance
            // 
            this.chk_saleadvance.AutoSize = true;
            this.chk_saleadvance.Checked = true;
            this.chk_saleadvance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_saleadvance.Location = new System.Drawing.Point(931, 4);
            this.chk_saleadvance.Name = "chk_saleadvance";
            this.chk_saleadvance.Size = new System.Drawing.Size(95, 17);
            this.chk_saleadvance.TabIndex = 118;
            this.chk_saleadvance.Text = "Sale/Advance";
            this.chk_saleadvance.UseVisualStyleBackColor = true;
            this.chk_saleadvance.CheckedChanged += new System.EventHandler(this.chk_saleadvance_CheckedChanged);
            // 
            // RepAugrai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chk_saleadvance);
            this.Controls.Add(this.chk_full_detail);
            this.Controls.Add(this.date_start);
            this.Controls.Add(this.rd_check);
            this.Controls.Add(this.chk_printall);
            this.Controls.Add(this.crystal_view_customer);
            this.Name = "RepAugrai";
            this.Size = new System.Drawing.Size(1203, 671);
            this.Load += new System.EventHandler(this.RepAugrai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rd_check.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystal_view_customer;
        private System.Windows.Forms.CheckBox chk_printall;
        private DevExpress.XtraEditors.RadioGroup rd_check;
        private MetroFramework.Controls.MetroDateTime date_start;
        private System.Windows.Forms.CheckBox chk_full_detail;
        private System.Windows.Forms.CheckBox chk_saleadvance;
    }
}
