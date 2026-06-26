using System.Windows.Forms;

namespace ArthiPOS.controls.dashboard
{
    partial class InvoicingPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoicingPage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.billtype_combo = new MetroFramework.Controls.MetroComboBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.today_date = new MetroFramework.Controls.MetroDateTime();
            this.nextdate = new MetroFramework.Controls.MetroButton();
            this.previousdate = new MetroFramework.Controls.MetroButton();
            this.btn_print_all_bills = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_update_sales = new Bunifu.Framework.UI.BunifuFlatButton();
            this.chk_status_localload = new System.Windows.Forms.CheckBox();
            this.dg_invoice = new System.Windows.Forms.DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewImageColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_frieght_detail = new Bunifu.Framework.UI.BunifuTileButton();
            this.rd_live = new System.Windows.Forms.RadioButton();
            this.rd_both = new System.Windows.Forms.RadioButton();
            this.rd_local = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_submit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.date_last = new MetroFramework.Controls.MetroDateTime();
            this.date_start = new MetroFramework.Controls.MetroDateTime();
            this.label4 = new System.Windows.Forms.Label();
            this.chk_date = new System.Windows.Forms.CheckBox();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_search = new ArthiPOS.Controls.UrduTextBox();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_invoice)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // billtype_combo
            // 
            this.billtype_combo.FormattingEnabled = true;
            this.billtype_combo.ItemHeight = 23;
            this.billtype_combo.Items.AddRange(new object[] {
            "Client",
            "Customer",
            "Landlord"});
            this.billtype_combo.Location = new System.Drawing.Point(618, 49);
            this.billtype_combo.Name = "billtype_combo";
            this.billtype_combo.PromptText = "Select";
            this.billtype_combo.Size = new System.Drawing.Size(152, 29);
            this.billtype_combo.Style = MetroFramework.MetroColorStyle.Green;
            this.billtype_combo.TabIndex = 115;
            this.billtype_combo.Theme = MetroFramework.MetroThemeStyle.Light;
            this.billtype_combo.UseSelectable = true;
            this.billtype_combo.SelectedIndexChanged += new System.EventHandler(this.billtype_combo_SelectedIndexChanged);
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.White;
            this.panel13.Controls.Add(this.today_date);
            this.panel13.Controls.Add(this.nextdate);
            this.panel13.Controls.Add(this.previousdate);
            this.panel13.Location = new System.Drawing.Point(140, 8);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(222, 35);
            this.panel13.TabIndex = 116;
            // 
            // today_date
            // 
            this.today_date.CustomFormat = "yyyy-MM-dd";
            this.today_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.today_date.Location = new System.Drawing.Point(3, 3);
            this.today_date.MinimumSize = new System.Drawing.Size(0, 29);
            this.today_date.Name = "today_date";
            this.today_date.Size = new System.Drawing.Size(152, 29);
            this.today_date.TabIndex = 116;
            this.today_date.CloseUp += new System.EventHandler(this.today_date_CloseUp);
            // 
            // nextdate
            // 
            this.nextdate.BackgroundImage = global::ArthiPOS.Properties.Resources.next;
            this.nextdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.nextdate.Location = new System.Drawing.Point(192, 8);
            this.nextdate.Name = "nextdate";
            this.nextdate.Size = new System.Drawing.Size(25, 23);
            this.nextdate.TabIndex = 115;
            this.nextdate.UseSelectable = true;
            this.nextdate.Click += new System.EventHandler(this.nextdate_Click);
            // 
            // previousdate
            // 
            this.previousdate.BackgroundImage = global::ArthiPOS.Properties.Resources.previou;
            this.previousdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.previousdate.Location = new System.Drawing.Point(161, 6);
            this.previousdate.Name = "previousdate";
            this.previousdate.Size = new System.Drawing.Size(25, 23);
            this.previousdate.TabIndex = 114;
            this.previousdate.UseSelectable = true;
            this.previousdate.Click += new System.EventHandler(this.previousdate_Click);
            // 
            // btn_print_all_bills
            // 
            this.btn_print_all_bills.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_print_all_bills.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_print_all_bills.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_print_all_bills.BorderRadius = 0;
            this.btn_print_all_bills.ButtonText = "Print All Bill";
            this.btn_print_all_bills.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_print_all_bills.DisabledColor = System.Drawing.Color.Gray;
            this.btn_print_all_bills.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_print_all_bills.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_print_all_bills.Iconimage")));
            this.btn_print_all_bills.Iconimage_right = null;
            this.btn_print_all_bills.Iconimage_right_Selected = null;
            this.btn_print_all_bills.Iconimage_Selected = null;
            this.btn_print_all_bills.IconMarginLeft = 0;
            this.btn_print_all_bills.IconMarginRight = 0;
            this.btn_print_all_bills.IconRightVisible = true;
            this.btn_print_all_bills.IconRightZoom = 0D;
            this.btn_print_all_bills.IconVisible = true;
            this.btn_print_all_bills.IconZoom = 90D;
            this.btn_print_all_bills.IsTab = false;
            this.btn_print_all_bills.Location = new System.Drawing.Point(582, -2);
            this.btn_print_all_bills.Name = "btn_print_all_bills";
            this.btn_print_all_bills.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_print_all_bills.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btn_print_all_bills.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_print_all_bills.selected = false;
            this.btn_print_all_bills.Size = new System.Drawing.Size(134, 46);
            this.btn_print_all_bills.TabIndex = 117;
            this.btn_print_all_bills.Text = "Print All Bill";
            this.btn_print_all_bills.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_print_all_bills.Textcolor = System.Drawing.Color.White;
            this.btn_print_all_bills.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print_all_bills.Click += new System.EventHandler(this.bunifuFlatButton1_Click);
            // 
            // btn_update_sales
            // 
            this.btn_update_sales.Activecolor = System.Drawing.Color.White;
            this.btn_update_sales.BackColor = System.Drawing.Color.White;
            this.btn_update_sales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_update_sales.BorderRadius = 0;
            this.btn_update_sales.ButtonText = "Update Daily Sales";
            this.btn_update_sales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_update_sales.DisabledColor = System.Drawing.Color.Gray;
            this.btn_update_sales.Enabled = false;
            this.btn_update_sales.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_update_sales.Iconimage = global::ArthiPOS.Properties.Resources.add;
            this.btn_update_sales.Iconimage_right = null;
            this.btn_update_sales.Iconimage_right_Selected = null;
            this.btn_update_sales.Iconimage_Selected = null;
            this.btn_update_sales.IconMarginLeft = 10;
            this.btn_update_sales.IconMarginRight = 0;
            this.btn_update_sales.IconRightVisible = true;
            this.btn_update_sales.IconRightZoom = 0D;
            this.btn_update_sales.IconVisible = true;
            this.btn_update_sales.IconZoom = 80D;
            this.btn_update_sales.IsTab = false;
            this.btn_update_sales.Location = new System.Drawing.Point(746, 3);
            this.btn_update_sales.Name = "btn_update_sales";
            this.btn_update_sales.Normalcolor = System.Drawing.Color.White;
            this.btn_update_sales.OnHovercolor = System.Drawing.Color.Silver;
            this.btn_update_sales.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_update_sales.selected = false;
            this.btn_update_sales.Size = new System.Drawing.Size(217, 41);
            this.btn_update_sales.TabIndex = 199;
            this.btn_update_sales.Text = "Update Daily Sales";
            this.btn_update_sales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_update_sales.Textcolor = System.Drawing.Color.Gray;
            this.btn_update_sales.TextFont = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_update_sales.Visible = false;
            this.btn_update_sales.Click += new System.EventHandler(this.btn_update_sales_Click);
            // 
            // chk_status_localload
            // 
            this.chk_status_localload.Appearance = System.Windows.Forms.Appearance.Button;
            this.chk_status_localload.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.chk_status_localload.Location = new System.Drawing.Point(3, 10);
            this.chk_status_localload.Name = "chk_status_localload";
            this.chk_status_localload.Size = new System.Drawing.Size(131, 34);
            this.chk_status_localload.TabIndex = 200;
            this.chk_status_localload.Text = "Load From Local";
            this.chk_status_localload.UseVisualStyleBackColor = false;
            this.chk_status_localload.Click += new System.EventHandler(this.chk_status_localload_Click);
            // 
            // dg_invoice
            // 
            this.dg_invoice.AllowUserToOrderColumns = true;
            this.dg_invoice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_invoice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_invoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_invoice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column8,
            this.Column7,
            this.Column12,
            this.Column13});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_invoice.DefaultCellStyle = dataGridViewCellStyle6;
            this.dg_invoice.Location = new System.Drawing.Point(6, 84);
            this.dg_invoice.Name = "dg_invoice";
            this.dg_invoice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_invoice.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dg_invoice.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dg_invoice.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_invoice.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dg_invoice.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_invoice.RowTemplate.Height = 35;
            this.dg_invoice.Size = new System.Drawing.Size(1187, 573);
            this.dg_invoice.TabIndex = 202;
            this.dg_invoice.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_invoice_CellClick);
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Date";
            this.Column9.Name = "Column9";
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Name";
            this.Column2.Name = "Column2";
            this.Column2.Width = 130;
            // 
            // Column3
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column3.HeaderText = "Invoice";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Quantity";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column5.HeaderText = "Total Bill";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column6.HeaderText = "Previous Amount";
            this.Column6.Name = "Column6";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Expense/CommissionChongi";
            this.Column8.Name = "Column8";
            this.Column8.Width = 150;
            // 
            // Column7
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column7.HeaderText = "Total";
            this.Column7.Name = "Column7";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Preview";
            this.Column12.Image = global::ArthiPOS.Properties.Resources.vewitem;
            this.Column12.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column12.Name = "Column12";
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Print";
            this.Column13.Image = global::ArthiPOS.Properties.Resources.Print;
            this.Column13.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column13.Name = "Column13";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 203;
            this.label1.Text = "CTRL + P";
            // 
            // btn_frieght_detail
            // 
            this.btn_frieght_detail.BackColor = System.Drawing.Color.OrangeRed;
            this.btn_frieght_detail.color = System.Drawing.Color.OrangeRed;
            this.btn_frieght_detail.colorActive = System.Drawing.Color.MediumSeaGreen;
            this.btn_frieght_detail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_frieght_detail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btn_frieght_detail.ForeColor = System.Drawing.Color.White;
            this.btn_frieght_detail.Image = global::ArthiPOS.Properties.Resources.add;
            this.btn_frieght_detail.ImagePosition = 10;
            this.btn_frieght_detail.ImageZoom = 50;
            this.btn_frieght_detail.LabelPosition = 33;
            this.btn_frieght_detail.LabelText = "Today Fright";
            this.btn_frieght_detail.Location = new System.Drawing.Point(426, 0);
            this.btn_frieght_detail.Margin = new System.Windows.Forms.Padding(6);
            this.btn_frieght_detail.Name = "btn_frieght_detail";
            this.btn_frieght_detail.Size = new System.Drawing.Size(147, 44);
            this.btn_frieght_detail.TabIndex = 205;
            this.btn_frieght_detail.Click += new System.EventHandler(this.btn_frieght_detail_Click);
            // 
            // rd_live
            // 
            this.rd_live.AutoSize = true;
            this.rd_live.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_live.Location = new System.Drawing.Point(66, 8);
            this.rd_live.Name = "rd_live";
            this.rd_live.Size = new System.Drawing.Size(52, 21);
            this.rd_live.TabIndex = 203;
            this.rd_live.TabStop = true;
            this.rd_live.Text = "Live";
            this.rd_live.UseVisualStyleBackColor = true;
            // 
            // rd_both
            // 
            this.rd_both.AutoSize = true;
            this.rd_both.Checked = true;
            this.rd_both.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_both.Location = new System.Drawing.Point(5, 8);
            this.rd_both.Name = "rd_both";
            this.rd_both.Size = new System.Drawing.Size(55, 21);
            this.rd_both.TabIndex = 202;
            this.rd_both.TabStop = true;
            this.rd_both.Text = "Both";
            this.rd_both.UseVisualStyleBackColor = true;
            // 
            // rd_local
            // 
            this.rd_local.AutoSize = true;
            this.rd_local.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_local.Location = new System.Drawing.Point(124, 8);
            this.rd_local.Name = "rd_local";
            this.rd_local.Size = new System.Drawing.Size(60, 21);
            this.rd_local.TabIndex = 204;
            this.rd_local.TabStop = true;
            this.rd_local.Text = "Local";
            this.rd_local.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.rd_local);
            this.groupBox1.Controls.Add(this.rd_both);
            this.groupBox1.Controls.Add(this.rd_live);
            this.groupBox1.Location = new System.Drawing.Point(985, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 37);
            this.groupBox1.TabIndex = 206;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(113, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 207;
            this.label2.Text = "Enter";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btn_submit);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.date_last);
            this.panel1.Controls.Add(this.date_start);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(6, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 35);
            this.panel1.TabIndex = 117;
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(347, 1);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(51, 34);
            this.btn_submit.TabIndex = 209;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(159, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 208;
            this.label3.Text = "TO";
            // 
            // date_last
            // 
            this.date_last.CustomFormat = "yyyy-MM-dd";
            this.date_last.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_last.Location = new System.Drawing.Point(189, 3);
            this.date_last.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_last.Name = "date_last";
            this.date_last.Size = new System.Drawing.Size(152, 29);
            this.date_last.TabIndex = 117;
            // 
            // date_start
            // 
            this.date_start.CustomFormat = "yyyy-MM-dd";
            this.date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_start.Location = new System.Drawing.Point(3, 3);
            this.date_start.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_start.Name = "date_start";
            this.date_start.Size = new System.Drawing.Size(152, 29);
            this.date_start.TabIndex = 116;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(686, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 208;
            this.label4.Text = "CTRL + P";
            // 
            // chk_date
            // 
            this.chk_date.AutoSize = true;
            this.chk_date.Location = new System.Drawing.Point(404, 45);
            this.chk_date.Name = "chk_date";
            this.chk_date.Size = new System.Drawing.Size(15, 14);
            this.chk_date.TabIndex = 209;
            this.chk_date.UseVisualStyleBackColor = true;
            this.chk_date.CheckedChanged += new System.EventHandler(this.chl_date_CheckedChanged);
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackColor = System.Drawing.Color.White;
            this.btn_refresh.BackgroundImage = global::ArthiPOS.Properties.Resources.refresh1x;
            this.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refresh.Location = new System.Drawing.Point(369, 1);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(46, 42);
            this.btn_refresh.TabIndex = 210;
            this.btn_refresh.UseVisualStyleBackColor = false;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(401, -2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 211;
            this.label5.Text = "F5";
            // 
            // txt_search
            // 
            this.txt_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txt_search.IsNumeric = false;
            this.txt_search.LangEnglish = false;
            this.txt_search.Location = new System.Drawing.Point(776, 49);
            this.txt_search.Name = "txt_search";
            this.txt_search.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_search.Size = new System.Drawing.Size(417, 29);
            this.txt_search.TabIndex = 201;
            this.txt_search.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_search.WaterMarkText = "Search";
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // InvoicingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.chk_date);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_frieght_detail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dg_invoice);
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.chk_status_localload);
            this.Controls.Add(this.btn_update_sales);
            this.Controls.Add(this.btn_print_all_bills);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.billtype_combo);
            this.Name = "InvoicingPage";
            this.Size = new System.Drawing.Size(1199, 656);
            this.Load += new System.EventHandler(this.Invoicing_Load);
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_invoice)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroComboBox billtype_combo;
        private System.Windows.Forms.Panel panel13;
        private MetroFramework.Controls.MetroDateTime today_date;
        private MetroFramework.Controls.MetroButton nextdate;
        private MetroFramework.Controls.MetroButton previousdate;
        private Bunifu.Framework.UI.BunifuFlatButton btn_print_all_bills;
        private Bunifu.Framework.UI.BunifuFlatButton btn_update_sales;
        private System.Windows.Forms.CheckBox chk_status_localload;
        private Controls.UrduTextBox txt_search;
        private System.Windows.Forms.DataGridView dg_invoice;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuTileButton btn_frieght_detail;
        private System.Windows.Forms.RadioButton rd_live;
        private System.Windows.Forms.RadioButton rd_both;
        private System.Windows.Forms.RadioButton rd_local;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroDateTime date_last;
        private MetroFramework.Controls.MetroDateTime date_start;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chk_date;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewImageColumn Column12;
        private System.Windows.Forms.DataGridViewImageColumn Column13;
    }
}
