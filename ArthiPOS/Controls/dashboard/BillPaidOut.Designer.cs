namespace ArthiPOS.Controls.dashboard
{
    partial class BillPaidOut
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
            this._lbl_name = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this._lbl_bill_count = new System.Windows.Forms.Label();
            this._lbl_paid = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_printlist = new System.Windows.Forms.Button();
            this.chkpaid = new System.Windows.Forms.CheckBox();
            this.chk_unpaid = new System.Windows.Forms.CheckBox();
            this.rb_client = new System.Windows.Forms.RadioButton();
            this.rb_customer = new System.Windows.Forms.RadioButton();
            this.txt_search = new ArthiPOS.Controls.UrduTextBox();
            this.today_date = new MetroFramework.Controls.MetroDateTime();
            this.btn_print_all = new System.Windows.Forms.Button();
            this.lbl_id = new System.Windows.Forms.Label();
            this._lbl_id = new System.Windows.Forms.Label();
            this.p_date = new System.Windows.Forms.Panel();
            this.lbl_end = new System.Windows.Forms.Label();
            this.date_last = new MetroFramework.Controls.MetroDateTime();
            this.lbl_start = new System.Windows.Forms.Label();
            this.date_start = new MetroFramework.Controls.MetroDateTime();
            this.chk_date_enable = new System.Windows.Forms.CheckBox();
            this.chk_paid_unpaid = new System.Windows.Forms.CheckBox();
            this._lbl_unpaid = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.dg_bilpaid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1.SuspendLayout();
            this.p_date.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_bilpaid)).BeginInit();
            this.SuspendLayout();
            // 
            // _lbl_name
            // 
            this._lbl_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lbl_name.AutoSize = true;
            this._lbl_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lbl_name.Location = new System.Drawing.Point(977, 74);
            this._lbl_name.Name = "_lbl_name";
            this._lbl_name.Size = new System.Drawing.Size(51, 20);
            this._lbl_name.TabIndex = 0;
            this._lbl_name.Text = "Name";
            // 
            // lbl_name
            // 
            this.lbl_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_name.AutoSize = true;
            this.lbl_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.Location = new System.Drawing.Point(859, 74);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_name.Size = new System.Drawing.Size(65, 24);
            this.lbl_name.TabIndex = 1;
            this.lbl_name.Text = "Name";
            // 
            // _lbl_bill_count
            // 
            this._lbl_bill_count.AutoSize = true;
            this._lbl_bill_count.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lbl_bill_count.Location = new System.Drawing.Point(183, 33);
            this._lbl_bill_count.Name = "_lbl_bill_count";
            this._lbl_bill_count.Size = new System.Drawing.Size(75, 22);
            this._lbl_bill_count.TabIndex = 4;
            this._lbl_bill_count.Text = "Paid Bill";
            // 
            // _lbl_paid
            // 
            this._lbl_paid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lbl_paid.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lbl_paid.Location = new System.Drawing.Point(5, 33);
            this._lbl_paid.Name = "_lbl_paid";
            this._lbl_paid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lbl_paid.Size = new System.Drawing.Size(172, 28);
            this._lbl_paid.TabIndex = 5;
            this._lbl_paid.Text = "0";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btn_printlist);
            this.panel1.Controls.Add(this.chkpaid);
            this.panel1.Controls.Add(this.chk_unpaid);
            this.panel1.Controls.Add(this.rb_client);
            this.panel1.Controls.Add(this.rb_customer);
            this.panel1.Controls.Add(this.txt_search);
            this.panel1.Controls.Add(this.today_date);
            this.panel1.Controls.Add(this.btn_print_all);
            this.panel1.Controls.Add(this.lbl_id);
            this.panel1.Controls.Add(this._lbl_id);
            this.panel1.Controls.Add(this.p_date);
            this.panel1.Controls.Add(this.chk_date_enable);
            this.panel1.Controls.Add(this.chk_paid_unpaid);
            this.panel1.Controls.Add(this._lbl_unpaid);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.lbl_name);
            this.panel1.Controls.Add(this._lbl_paid);
            this.panel1.Controls.Add(this._lbl_name);
            this.panel1.Controls.Add(this._lbl_bill_count);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1035, 146);
            this.panel1.TabIndex = 6;
            // 
            // btn_printlist
            // 
            this.btn_printlist.Location = new System.Drawing.Point(115, 115);
            this.btn_printlist.Name = "btn_printlist";
            this.btn_printlist.Size = new System.Drawing.Size(106, 28);
            this.btn_printlist.TabIndex = 290;
            this.btn_printlist.Text = "Print List";
            this.btn_printlist.UseVisualStyleBackColor = true;
            this.btn_printlist.Click += new System.EventHandler(this.btn_printlist_Click);
            // 
            // chkpaid
            // 
            this.chkpaid.AutoSize = true;
            this.chkpaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkpaid.Location = new System.Drawing.Point(324, 88);
            this.chkpaid.Name = "chkpaid";
            this.chkpaid.Size = new System.Drawing.Size(59, 21);
            this.chkpaid.TabIndex = 289;
            this.chkpaid.Text = "Paid";
            this.chkpaid.UseVisualStyleBackColor = true;
            this.chkpaid.Click += new System.EventHandler(this.chkpaid_Click);
            // 
            // chk_unpaid
            // 
            this.chk_unpaid.AutoSize = true;
            this.chk_unpaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_unpaid.Location = new System.Drawing.Point(475, 88);
            this.chk_unpaid.Name = "chk_unpaid";
            this.chk_unpaid.Size = new System.Drawing.Size(79, 21);
            this.chk_unpaid.TabIndex = 288;
            this.chk_unpaid.Text = "UnPaid";
            this.chk_unpaid.UseVisualStyleBackColor = true;
            this.chk_unpaid.Click += new System.EventHandler(this.chk_unpaid_Click);
            // 
            // rb_client
            // 
            this.rb_client.AutoSize = true;
            this.rb_client.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_client.Location = new System.Drawing.Point(117, 4);
            this.rb_client.Name = "rb_client";
            this.rb_client.Size = new System.Drawing.Size(139, 22);
            this.rb_client.TabIndex = 285;
            this.rb_client.Text = "Landlord/Bipari";
            this.rb_client.UseVisualStyleBackColor = true;
            this.rb_client.CheckedChanged += new System.EventHandler(this.rb_client_CheckedChanged);
            // 
            // rb_customer
            // 
            this.rb_customer.AutoSize = true;
            this.rb_customer.Checked = true;
            this.rb_customer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_customer.Location = new System.Drawing.Point(11, 4);
            this.rb_customer.Name = "rb_customer";
            this.rb_customer.Size = new System.Drawing.Size(100, 22);
            this.rb_customer.TabIndex = 284;
            this.rb_customer.TabStop = true;
            this.rb_customer.Text = "Customer";
            this.rb_customer.UseVisualStyleBackColor = true;
            this.rb_customer.CheckedChanged += new System.EventHandler(this.rb_customer_CheckedChanged);
            // 
            // txt_search
            // 
            this.txt_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_search.IsNumeric = false;
            this.txt_search.LangEnglish = false;
            this.txt_search.Location = new System.Drawing.Point(321, 22);
            this.txt_search.Multiline = true;
            this.txt_search.Name = "txt_search";
            this.txt_search.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_search.Size = new System.Drawing.Size(341, 30);
            this.txt_search.TabIndex = 283;
            this.txt_search.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_search.WaterMarkText = "Search";
            // 
            // today_date
            // 
            this.today_date.CustomFormat = "yyyy-MM-dd";
            this.today_date.Enabled = false;
            this.today_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.today_date.Location = new System.Drawing.Point(895, 4);
            this.today_date.MinimumSize = new System.Drawing.Size(0, 29);
            this.today_date.Name = "today_date";
            this.today_date.Size = new System.Drawing.Size(137, 29);
            this.today_date.TabIndex = 130;
            // 
            // btn_print_all
            // 
            this.btn_print_all.Location = new System.Drawing.Point(3, 115);
            this.btn_print_all.Name = "btn_print_all";
            this.btn_print_all.Size = new System.Drawing.Size(106, 28);
            this.btn_print_all.TabIndex = 129;
            this.btn_print_all.Text = "Print All Invoice";
            this.btn_print_all.UseVisualStyleBackColor = true;
            this.btn_print_all.Click += new System.EventHandler(this.btn_print_all_Click);
            // 
            // lbl_id
            // 
            this.lbl_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_id.AutoSize = true;
            this.lbl_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_id.Location = new System.Drawing.Point(895, 43);
            this.lbl_id.Name = "lbl_id";
            this.lbl_id.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_id.Size = new System.Drawing.Size(29, 24);
            this.lbl_id.TabIndex = 128;
            this.lbl_id.Text = "ID";
            // 
            // _lbl_id
            // 
            this._lbl_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lbl_id.AutoSize = true;
            this._lbl_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lbl_id.Location = new System.Drawing.Point(999, 46);
            this._lbl_id.Name = "_lbl_id";
            this._lbl_id.Size = new System.Drawing.Size(26, 20);
            this._lbl_id.TabIndex = 127;
            this._lbl_id.Text = "ID";
            // 
            // p_date
            // 
            this.p_date.Controls.Add(this.lbl_end);
            this.p_date.Controls.Add(this.date_last);
            this.p_date.Controls.Add(this.lbl_start);
            this.p_date.Controls.Add(this.date_start);
            this.p_date.Enabled = false;
            this.p_date.Location = new System.Drawing.Point(321, 53);
            this.p_date.Name = "p_date";
            this.p_date.Size = new System.Drawing.Size(343, 35);
            this.p_date.TabIndex = 126;
            // 
            // lbl_end
            // 
            this.lbl_end.Location = new System.Drawing.Point(164, 0);
            this.lbl_end.Name = "lbl_end";
            this.lbl_end.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_end.Size = new System.Drawing.Size(68, 32);
            this.lbl_end.TabIndex = 117;
            this.lbl_end.Text = "Last Date";
            this.lbl_end.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // date_last
            // 
            this.date_last.CustomFormat = "yyyy-MM-dd";
            this.date_last.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_last.Location = new System.Drawing.Point(238, 3);
            this.date_last.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_last.Name = "date_last";
            this.date_last.Size = new System.Drawing.Size(103, 29);
            this.date_last.TabIndex = 116;
            // 
            // lbl_start
            // 
            this.lbl_start.Location = new System.Drawing.Point(-9, 0);
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
            this.date_start.Location = new System.Drawing.Point(55, 3);
            this.date_start.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_start.Name = "date_start";
            this.date_start.Size = new System.Drawing.Size(103, 29);
            this.date_start.TabIndex = 115;
            // 
            // chk_date_enable
            // 
            this.chk_date_enable.AutoSize = true;
            this.chk_date_enable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_date_enable.Location = new System.Drawing.Point(668, 56);
            this.chk_date_enable.Name = "chk_date_enable";
            this.chk_date_enable.Size = new System.Drawing.Size(15, 14);
            this.chk_date_enable.TabIndex = 125;
            this.chk_date_enable.UseVisualStyleBackColor = true;
            this.chk_date_enable.CheckedChanged += new System.EventHandler(this.chk_date_enable_CheckedChanged);
            // 
            // chk_paid_unpaid
            // 
            this.chk_paid_unpaid.AutoSize = true;
            this.chk_paid_unpaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_paid_unpaid.Location = new System.Drawing.Point(881, 122);
            this.chk_paid_unpaid.Name = "chk_paid_unpaid";
            this.chk_paid_unpaid.Size = new System.Drawing.Size(147, 21);
            this.chk_paid_unpaid.TabIndex = 124;
            this.chk_paid_unpaid.Text = "Paid/UnPaid List";
            this.chk_paid_unpaid.UseVisualStyleBackColor = true;
            this.chk_paid_unpaid.CheckedChanged += new System.EventHandler(this.chk_paid_unpaid_CheckedChanged);
            // 
            // _lbl_unpaid
            // 
            this._lbl_unpaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lbl_unpaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lbl_unpaid.Location = new System.Drawing.Point(5, 63);
            this._lbl_unpaid.Name = "_lbl_unpaid";
            this._lbl_unpaid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lbl_unpaid.Size = new System.Drawing.Size(172, 28);
            this._lbl_unpaid.TabIndex = 123;
            this._lbl_unpaid.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(183, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 22);
            this.label2.TabIndex = 122;
            this.label2.Text = "unPaid Bill";
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(322, 110);
            this.btn_search.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(345, 27);
            this.btn_search.TabIndex = 121;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // dg_bilpaid
            // 
            this.dg_bilpaid.AllowUserToOrderColumns = true;
            this.dg_bilpaid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_bilpaid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_bilpaid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_bilpaid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_bilpaid.DefaultCellStyle = dataGridViewCellStyle2;
            this.dg_bilpaid.Location = new System.Drawing.Point(6, 156);
            this.dg_bilpaid.Name = "dg_bilpaid";
            this.dg_bilpaid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_bilpaid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dg_bilpaid.Size = new System.Drawing.Size(1041, 479);
            this.dg_bilpaid.TabIndex = 7;
            this.dg_bilpaid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_bilpaid_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Print";
            this.Column1.Image = global::ArthiPOS.Properties.Resources.Print;
            this.Column1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column1.Name = "Column1";
            // 
            // BillPaidOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 635);
            this.Controls.Add(this.dg_bilpaid);
            this.Controls.Add(this.panel1);
            this.Name = "BillPaidOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BillPaidOut";
            this.Load += new System.EventHandler(this.BillPaidOut_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.p_date.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_bilpaid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _lbl_name;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label _lbl_bill_count;
        private System.Windows.Forms.Label _lbl_paid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dg_bilpaid;
        private System.Windows.Forms.CheckBox chk_paid_unpaid;
        private System.Windows.Forms.Label _lbl_unpaid;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroDateTime date_last;
        private System.Windows.Forms.Label lbl_end;
        private MetroFramework.Controls.MetroDateTime date_start;
        private System.Windows.Forms.Label lbl_start;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Panel p_date;
        private System.Windows.Forms.CheckBox chk_date_enable;
        private System.Windows.Forms.Label lbl_id;
        private System.Windows.Forms.Label _lbl_id;
        private System.Windows.Forms.Button btn_print_all;
        private MetroFramework.Controls.MetroDateTime today_date;
        private System.Windows.Forms.RadioButton rb_client;
        private System.Windows.Forms.RadioButton rb_customer;
        private UrduTextBox txt_search;
        private System.Windows.Forms.CheckBox chk_unpaid;
        private System.Windows.Forms.CheckBox chkpaid;
        private System.Windows.Forms.Button btn_printlist;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
    }
}