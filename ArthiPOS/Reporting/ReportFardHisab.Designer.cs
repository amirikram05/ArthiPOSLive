namespace ArthiPOS.Reporting
{
    partial class ReportFardHisab
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dg_invoice = new System.Windows.Forms.DataGridView();
            this.rb_client = new System.Windows.Forms.RadioButton();
            this.rb_customer = new System.Windows.Forms.RadioButton();
            this.rb_admin = new System.Windows.Forms.RadioButton();
            this.date_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_start = new System.Windows.Forms.Label();
            this.date_start = new MetroFramework.Controls.MetroDateTime();
            this.lbl_end = new System.Windows.Forms.Label();
            this.date_last = new MetroFramework.Controls.MetroDateTime();
            this.btn_search = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_bipari_search = new System.Windows.Forms.Button();
            this.lbl_id = new System.Windows.Forms.Label();
            this.txt_nameid = new ArthiPOS.Controls.UrduTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_print = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rb_advance = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dg_invoice)).BeginInit();
            this.date_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg_invoice
            // 
            this.dg_invoice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_invoice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dg_invoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("UrduLink", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_invoice.DefaultCellStyle = dataGridViewCellStyle5;
            this.dg_invoice.Location = new System.Drawing.Point(12, 1);
            this.dg_invoice.Name = "dg_invoice";
            this.dg_invoice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_invoice.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dg_invoice.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dg_invoice.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("UrduLink", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_invoice.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dg_invoice.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_invoice.RowTemplate.Height = 35;
            this.dg_invoice.Size = new System.Drawing.Size(1059, 679);
            this.dg_invoice.TabIndex = 203;
            // 
            // rb_client
            // 
            this.rb_client.AutoSize = true;
            this.rb_client.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_client.Location = new System.Drawing.Point(56, 73);
            this.rb_client.Name = "rb_client";
            this.rb_client.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_client.Size = new System.Drawing.Size(148, 24);
            this.rb_client.TabIndex = 204;
            this.rb_client.TabStop = true;
            this.rb_client.Text = "Client/Landlord";
            this.rb_client.UseVisualStyleBackColor = true;
            this.rb_client.Click += new System.EventHandler(this.rb_client_Click);
            // 
            // rb_customer
            // 
            this.rb_customer.AutoSize = true;
            this.rb_customer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_customer.Location = new System.Drawing.Point(100, 43);
            this.rb_customer.Name = "rb_customer";
            this.rb_customer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_customer.Size = new System.Drawing.Size(104, 24);
            this.rb_customer.TabIndex = 205;
            this.rb_customer.TabStop = true;
            this.rb_customer.Text = "Customer";
            this.rb_customer.UseVisualStyleBackColor = true;
            this.rb_customer.Click += new System.EventHandler(this.rb_customer_Click);
            // 
            // rb_admin
            // 
            this.rb_admin.AutoSize = true;
            this.rb_admin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_admin.Location = new System.Drawing.Point(127, 133);
            this.rb_admin.Name = "rb_admin";
            this.rb_admin.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_admin.Size = new System.Drawing.Size(77, 24);
            this.rb_admin.TabIndex = 206;
            this.rb_admin.TabStop = true;
            this.rb_admin.Text = "Admin";
            this.rb_admin.UseVisualStyleBackColor = true;
            this.rb_admin.Click += new System.EventHandler(this.rb_admin_Click);
            // 
            // date_panel
            // 
            this.date_panel.Controls.Add(this.lbl_start);
            this.date_panel.Controls.Add(this.date_start);
            this.date_panel.Controls.Add(this.lbl_end);
            this.date_panel.Controls.Add(this.date_last);
            this.date_panel.Location = new System.Drawing.Point(0, 226);
            this.date_panel.Name = "date_panel";
            this.date_panel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.date_panel.Size = new System.Drawing.Size(211, 73);
            this.date_panel.TabIndex = 223;
            // 
            // lbl_start
            // 
            this.lbl_start.Location = new System.Drawing.Point(150, 0);
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
            this.date_start.Location = new System.Drawing.Point(6, 3);
            this.date_start.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_start.Name = "date_start";
            this.date_start.Size = new System.Drawing.Size(138, 29);
            this.date_start.TabIndex = 115;
            // 
            // lbl_end
            // 
            this.lbl_end.Location = new System.Drawing.Point(150, 35);
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
            this.date_last.Location = new System.Drawing.Point(6, 38);
            this.date_last.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_last.Name = "date_last";
            this.date_last.Size = new System.Drawing.Size(138, 29);
            this.date_last.TabIndex = 116;
            // 
            // btn_search
            // 
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.Location = new System.Drawing.Point(15, 308);
            this.btn_search.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(190, 27);
            this.btn_search.TabIndex = 120;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(-6, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 12);
            this.label5.TabIndex = 309;
            this.label5.Text = "CTRL + ENTER";
            // 
            // btn_bipari_search
            // 
            this.btn_bipari_search.BackColor = System.Drawing.Color.Transparent;
            this.btn_bipari_search.BackgroundImage = global::ArthiPOS.Properties.Resources.search;
            this.btn_bipari_search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_bipari_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_bipari_search.Location = new System.Drawing.Point(0, 192);
            this.btn_bipari_search.Name = "btn_bipari_search";
            this.btn_bipari_search.Size = new System.Drawing.Size(32, 25);
            this.btn_bipari_search.TabIndex = 308;
            this.btn_bipari_search.UseVisualStyleBackColor = false;
            // 
            // lbl_id
            // 
            this.lbl_id.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_id.Location = new System.Drawing.Point(63, 173);
            this.lbl_id.Name = "lbl_id";
            this.lbl_id.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_id.Size = new System.Drawing.Size(155, 15);
            this.lbl_id.TabIndex = 307;
            this.lbl_id.Text = "00000";
            // 
            // txt_nameid
            // 
            this.txt_nameid.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nameid.IsNumeric = false;
            this.txt_nameid.LangEnglish = false;
            this.txt_nameid.Location = new System.Drawing.Point(38, 191);
            this.txt_nameid.Name = "txt_nameid";
            this.txt_nameid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_nameid.Size = new System.Drawing.Size(176, 29);
            this.txt_nameid.TabIndex = 306;
            this.txt_nameid.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_nameid.WaterMarkText = "Search....";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 8);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(155, 15);
            this.label1.TabIndex = 310;
            this.label1.Text = "Fard Hisab";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_print
            // 
            this.btn_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print.Location = new System.Drawing.Point(15, 344);
            this.btn_print.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(190, 27);
            this.btn_print.TabIndex = 311;
            this.btn_print.Text = "Print";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rb_advance);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_print);
            this.panel1.Controls.Add(this.rb_client);
            this.panel1.Controls.Add(this.rb_customer);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.rb_admin);
            this.panel1.Controls.Add(this.btn_bipari_search);
            this.panel1.Controls.Add(this.date_panel);
            this.panel1.Controls.Add(this.lbl_id);
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.txt_nameid);
            this.panel1.Location = new System.Drawing.Point(1074, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 679);
            this.panel1.TabIndex = 312;
            // 
            // rb_advance
            // 
            this.rb_advance.AutoSize = true;
            this.rb_advance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_advance.Location = new System.Drawing.Point(109, 103);
            this.rb_advance.Name = "rb_advance";
            this.rb_advance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_advance.Size = new System.Drawing.Size(96, 24);
            this.rb_advance.TabIndex = 312;
            this.rb_advance.TabStop = true;
            this.rb_advance.Text = "Advance";
            this.rb_advance.UseVisualStyleBackColor = true;
            // 
            // ReportFardHisab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1294, 692);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dg_invoice);
            this.Name = "ReportFardHisab";
            this.Text = "ReportFardHisab";
            ((System.ComponentModel.ISupportInitialize)(this.dg_invoice)).EndInit();
            this.date_panel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_invoice;
        private System.Windows.Forms.RadioButton rb_client;
        private System.Windows.Forms.RadioButton rb_customer;
        private System.Windows.Forms.RadioButton rb_admin;
        private System.Windows.Forms.FlowLayoutPanel date_panel;
        private System.Windows.Forms.Label lbl_start;
        private MetroFramework.Controls.MetroDateTime date_start;
        private System.Windows.Forms.Label lbl_end;
        private MetroFramework.Controls.MetroDateTime date_last;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_bipari_search;
        private System.Windows.Forms.Label lbl_id;
        private Controls.UrduTextBox txt_nameid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rb_advance;
    }
}