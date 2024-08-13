namespace ArthiPOS.Reporting
{
    partial class ReportAugraiChk
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_print = new System.Windows.Forms.Button();
            this.date_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_start = new System.Windows.Forms.Label();
            this.date_start = new MetroFramework.Controls.MetroDateTime();
            this.lbl_end = new System.Windows.Forms.Label();
            this.date_last = new MetroFramework.Controls.MetroDateTime();
            this.btn_search = new System.Windows.Forms.Button();
            this.grid_shop = new MetroFramework.Controls.MetroGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.ID = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.Name = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.Address = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.LastAugrai = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.FreshAugrai = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.Purchase = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.Receivings = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.SeasonAugrai = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.TQuantity = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.TCommission = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.TChongi = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.Column1 = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.date_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_shop)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_print
            // 
            this.btn_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print.Location = new System.Drawing.Point(432, 6);
            this.btn_print.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(83, 26);
            this.btn_print.TabIndex = 314;
            this.btn_print.Text = "Print";
            this.btn_print.UseVisualStyleBackColor = true;
            // 
            // date_panel
            // 
            this.date_panel.Controls.Add(this.lbl_start);
            this.date_panel.Controls.Add(this.date_start);
            this.date_panel.Controls.Add(this.lbl_end);
            this.date_panel.Controls.Add(this.date_last);
            this.date_panel.Controls.Add(this.btn_search);
            this.date_panel.Controls.Add(this.btn_print);
            this.date_panel.Controls.Add(this.grid_shop);
            this.date_panel.Location = new System.Drawing.Point(12, 12);
            this.date_panel.Name = "date_panel";
            this.date_panel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.date_panel.Size = new System.Drawing.Size(1029, 40);
            this.date_panel.TabIndex = 313;
            // 
            // lbl_start
            // 
            this.lbl_start.Location = new System.Drawing.Point(968, 0);
            this.lbl_start.Name = "lbl_start";
            this.lbl_start.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_start.Size = new System.Drawing.Size(58, 32);
            this.lbl_start.TabIndex = 118;
            this.lbl_start.Text = "First Date";
            this.lbl_start.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // date_start
            // 
            this.date_start.CustomFormat = "yyyy-MM-dd";
            this.date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_start.Location = new System.Drawing.Point(824, 3);
            this.date_start.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_start.Name = "date_start";
            this.date_start.Size = new System.Drawing.Size(138, 29);
            this.date_start.TabIndex = 115;
            // 
            // lbl_end
            // 
            this.lbl_end.Location = new System.Drawing.Point(760, 0);
            this.lbl_end.Name = "lbl_end";
            this.lbl_end.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_end.Size = new System.Drawing.Size(58, 32);
            this.lbl_end.TabIndex = 117;
            this.lbl_end.Text = "Last Date";
            this.lbl_end.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // date_last
            // 
            this.date_last.CustomFormat = "yyyy-MM-dd";
            this.date_last.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_last.Location = new System.Drawing.Point(616, 3);
            this.date_last.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_last.Name = "date_last";
            this.date_last.Size = new System.Drawing.Size(138, 29);
            this.date_last.TabIndex = 116;
            // 
            // btn_search
            // 
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.Location = new System.Drawing.Point(521, 6);
            this.btn_search.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(89, 26);
            this.btn_search.TabIndex = 312;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            // 
            // grid_shop
            // 
            this.grid_shop.AllowUserToAddRows = false;
            this.grid_shop.AllowUserToDeleteRows = false;
            this.grid_shop.AllowUserToOrderColumns = true;
            this.grid_shop.AllowUserToResizeRows = false;
            this.grid_shop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_shop.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid_shop.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grid_shop.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grid_shop.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid_shop.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grid_shop.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_shop.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid_shop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_shop.DefaultCellStyle = dataGridViewCellStyle2;
            this.grid_shop.EnableHeadersVisualStyles = false;
            this.grid_shop.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid_shop.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grid_shop.Location = new System.Drawing.Point(-20, 38);
            this.grid_shop.Name = "grid_shop";
            this.grid_shop.ReadOnly = true;
            this.grid_shop.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grid_shop.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_shop.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grid_shop.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Jameel Noori Nastaleeq", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.grid_shop.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.grid_shop.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.grid_shop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid_shop.Size = new System.Drawing.Size(1046, 586);
            this.grid_shop.Style = MetroFramework.MetroColorStyle.Green;
            this.grid_shop.TabIndex = 315;
            this.grid_shop.Theme = MetroFramework.MetroThemeStyle.Light;
            this.grid_shop.UseStyleColors = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.metroGrid1);
            this.panel1.Location = new System.Drawing.Point(12, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1029, 578);
            this.panel1.TabIndex = 314;
            // 
            // metroGrid1
            // 
            this.metroGrid1.AllowUserToAddRows = false;
            this.metroGrid1.AllowUserToDeleteRows = false;
            this.metroGrid1.AllowUserToOrderColumns = true;
            this.metroGrid1.AllowUserToResizeRows = false;
            this.metroGrid1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.metroGrid1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.metroGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGrid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGrid1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.metroGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.metroGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Name,
            this.Address,
            this.LastAugrai,
            this.FreshAugrai,
            this.Purchase,
            this.Receivings,
            this.SeasonAugrai,
            this.TQuantity,
            this.TCommission,
            this.TChongi,
            this.Column1,
            this.Column2});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGrid1.DefaultCellStyle = dataGridViewCellStyle6;
            this.metroGrid1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroGrid1.EnableHeadersVisualStyles = false;
            this.metroGrid1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGrid1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.Location = new System.Drawing.Point(0, 6);
            this.metroGrid1.Name = "metroGrid1";
            this.metroGrid1.ReadOnly = true;
            this.metroGrid1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.metroGrid1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Jameel Noori Nastaleeq", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            this.metroGrid1.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.metroGrid1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGrid1.Size = new System.Drawing.Size(1029, 572);
            this.metroGrid1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroGrid1.TabIndex = 315;
            this.metroGrid1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroGrid1.UseStyleColors = true;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 27;
            // 
            // Name
            // 
            this.Name.HeaderText = "Name";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            this.Name.Width = 53;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 67;
            // 
            // LastAugrai
            // 
            this.LastAugrai.HeaderText = "LastAugrai";
            this.LastAugrai.Name = "LastAugrai";
            this.LastAugrai.ReadOnly = true;
            this.LastAugrai.Width = 84;
            // 
            // FreshAugrai
            // 
            this.FreshAugrai.HeaderText = "FreshAugrai";
            this.FreshAugrai.Name = "FreshAugrai";
            this.FreshAugrai.ReadOnly = true;
            this.FreshAugrai.Width = 93;
            // 
            // Purchase
            // 
            this.Purchase.HeaderText = "Purchase";
            this.Purchase.Name = "Purchase";
            this.Purchase.ReadOnly = true;
            this.Purchase.Width = 73;
            // 
            // Receivings
            // 
            this.Receivings.HeaderText = "Receivings";
            this.Receivings.Name = "Receivings";
            this.Receivings.ReadOnly = true;
            this.Receivings.Width = 84;
            // 
            // SeasonAugrai
            // 
            this.SeasonAugrai.HeaderText = "Purchase - Receivings";
            this.SeasonAugrai.Name = "SeasonAugrai";
            this.SeasonAugrai.ReadOnly = true;
            this.SeasonAugrai.Width = 142;
            // 
            // TQuantity
            // 
            this.TQuantity.HeaderText = "Remaining Augrai";
            this.TQuantity.Name = "TQuantity";
            this.TQuantity.ReadOnly = true;
            this.TQuantity.Width = 120;
            // 
            // TCommission
            // 
            this.TCommission.HeaderText = "TQuantity";
            this.TCommission.Name = "TCommission";
            this.TCommission.ReadOnly = true;
            this.TCommission.Width = 77;
            // 
            // TChongi
            // 
            this.TChongi.HeaderText = "Commission";
            this.TChongi.Name = "TChongi";
            this.TChongi.ReadOnly = true;
            this.TChongi.Width = 94;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Chongi";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Update Augrai";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // ReportAugraiChk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1053, 638);
            this.Controls.Add(this.date_panel);
            this.Controls.Add(this.panel1);
            this.Name = "ReportAugraiChk";
            this.Text = "ReportAugraiChk";
            this.date_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid_shop)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.FlowLayoutPanel date_panel;
        private System.Windows.Forms.Label lbl_start;
        private MetroFramework.Controls.MetroDateTime date_start;
        private System.Windows.Forms.Label lbl_end;
        private MetroFramework.Controls.MetroDateTime date_last;
        private System.Windows.Forms.Button btn_search;
        private MetroFramework.Controls.MetroGrid grid_shop;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroGrid metroGrid1;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn ID;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn Name;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn Address;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn LastAugrai;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn FreshAugrai;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn Purchase;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn Receivings;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn SeasonAugrai;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn TQuantity;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn TCommission;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn TChongi;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn Column2;
    }
}