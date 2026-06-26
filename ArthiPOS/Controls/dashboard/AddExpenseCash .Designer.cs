namespace ArthiPOS.Controls.dashboard
{
    partial class AddExpenseCash
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
            this.lbl_add_cash = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_cash = new System.Windows.Forms.TextBox();
            this.lbl_title = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ledger_date = new MetroFramework.Controls.MetroDateTime();
            this.btnAddcash = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_bipari_search = new System.Windows.Forms.Button();
            this.lbl_id = new System.Windows.Forms.Label();
            this.txt_discount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_msg = new System.Windows.Forms.TextBox();
            this.lbl_augrai = new System.Windows.Forms.Label();
            this.lbl_cashtype = new System.Windows.Forms.Label();
            this.lbl_key = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_refreshkey = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_cate = new System.Windows.Forms.Button();
            this.lbl_cate_id = new System.Windows.Forms.Label();
            this.lbl_catedetail = new System.Windows.Forms.Label();
            this.lbl_cate_name = new System.Windows.Forms.Label();
            this.btn_pdate = new MetroFramework.Controls.MetroButton();
            this.btn_ndate = new MetroFramework.Controls.MetroButton();
            this.lbl_account_trans_name = new System.Windows.Forms.Label();
            this.lbl_expid = new System.Windows.Forms.Label();
            this.lbl_transaction_id = new System.Windows.Forms.Label();
            this.lbl_account_trans_id = new System.Windows.Forms.Label();
            this.lbl_transname = new System.Windows.Forms.Label();
            this.txt_cate = new ArthiPOS.Controls.UrduTextBox();
            this.txt_nameid = new ArthiPOS.Controls.UrduTextBox();
            this.txt_desc = new ArthiPOS.Controls.UrduTextBox();
            this.lbl_fright = new System.Windows.Forms.LinkLabel();
            this.lbl_fardhisab = new System.Windows.Forms.LinkLabel();
            this.lbl_remaining_amount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_details = new ArthiPOS.Controls.UrduTextBox();
            this.SuspendLayout();
            // 
            // lbl_add_cash
            // 
            this.lbl_add_cash.AutoSize = true;
            this.lbl_add_cash.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_add_cash.Location = new System.Drawing.Point(296, 392);
            this.lbl_add_cash.Name = "lbl_add_cash";
            this.lbl_add_cash.Size = new System.Drawing.Size(62, 17);
            this.lbl_add_cash.TabIndex = 292;
            this.lbl_add_cash.Text = "Amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 498);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 12);
            this.label2.TabIndex = 290;
            this.label2.Text = "CTRL + ENTER";
            // 
            // txt_cash
            // 
            this.txt_cash.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cash.Location = new System.Drawing.Point(117, 413);
            this.txt_cash.Name = "txt_cash";
            this.txt_cash.Size = new System.Drawing.Size(242, 29);
            this.txt_cash.TabIndex = 289;
            this.txt_cash.TextChanged += new System.EventHandler(this.txt_cash_TextChanged);
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.Location = new System.Drawing.Point(120, 9);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(152, 25);
            this.lbl_title.TabIndex = 293;
            this.lbl_title.Text = "Add Expenses";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(228, 146);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(127, 17);
            this.label3.TabIndex = 294;
            this.label3.Text = "Description";
            // 
            // ledger_date
            // 
            this.ledger_date.CustomFormat = "yyyy-MM-dd";
            this.ledger_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ledger_date.Location = new System.Drawing.Point(19, 104);
            this.ledger_date.MinimumSize = new System.Drawing.Size(0, 29);
            this.ledger_date.Name = "ledger_date";
            this.ledger_date.Size = new System.Drawing.Size(137, 29);
            this.ledger_date.TabIndex = 295;
            // 
            // btnAddcash
            // 
            this.btnAddcash.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnAddcash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnAddcash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddcash.BorderRadius = 0;
            this.btnAddcash.ButtonText = "Add";
            this.btnAddcash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddcash.DisabledColor = System.Drawing.Color.Gray;
            this.btnAddcash.Iconcolor = System.Drawing.Color.Transparent;
            this.btnAddcash.Iconimage = null;
            this.btnAddcash.Iconimage_right = null;
            this.btnAddcash.Iconimage_right_Selected = null;
            this.btnAddcash.Iconimage_Selected = null;
            this.btnAddcash.IconMarginLeft = 0;
            this.btnAddcash.IconMarginRight = 0;
            this.btnAddcash.IconRightVisible = true;
            this.btnAddcash.IconRightZoom = 0D;
            this.btnAddcash.IconVisible = true;
            this.btnAddcash.IconZoom = 90D;
            this.btnAddcash.IsTab = false;
            this.btnAddcash.Location = new System.Drawing.Point(31, 503);
            this.btnAddcash.Name = "btnAddcash";
            this.btnAddcash.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnAddcash.OnHovercolor = System.Drawing.Color.Gray;
            this.btnAddcash.OnHoverTextColor = System.Drawing.Color.White;
            this.btnAddcash.selected = false;
            this.btnAddcash.Size = new System.Drawing.Size(327, 38);
            this.btnAddcash.TabIndex = 296;
            this.btnAddcash.Text = "Add";
            this.btnAddcash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddcash.Textcolor = System.Drawing.Color.White;
            this.btnAddcash.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddcash.Click += new System.EventHandler(this.btnAddcash_Click);
            // 
            // btn_bipari_search
            // 
            this.btn_bipari_search.BackColor = System.Drawing.Color.Transparent;
            this.btn_bipari_search.BackgroundImage = global::ArthiPOS.Properties.Resources.search;
            this.btn_bipari_search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_bipari_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_bipari_search.Location = new System.Drawing.Point(34, 334);
            this.btn_bipari_search.Name = "btn_bipari_search";
            this.btn_bipari_search.Size = new System.Drawing.Size(20, 20);
            this.btn_bipari_search.TabIndex = 300;
            this.btn_bipari_search.UseVisualStyleBackColor = false;
            this.btn_bipari_search.Click += new System.EventHandler(this.btn_bipari_search_Click);
            // 
            // lbl_id
            // 
            this.lbl_id.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_id.Location = new System.Drawing.Point(200, 315);
            this.lbl_id.Name = "lbl_id";
            this.lbl_id.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_id.Size = new System.Drawing.Size(155, 15);
            this.lbl_id.TabIndex = 299;
            this.lbl_id.Text = "00000";
            // 
            // txt_discount
            // 
            this.txt_discount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_discount.Location = new System.Drawing.Point(31, 412);
            this.txt_discount.Name = "txt_discount";
            this.txt_discount.Size = new System.Drawing.Size(80, 29);
            this.txt_discount.TabIndex = 303;
            this.txt_discount.TextChanged += new System.EventHandler(this.txt_cash_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 17);
            this.label4.TabIndex = 304;
            this.label4.Text = "Discount";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 321);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 12);
            this.label5.TabIndex = 305;
            this.label5.Text = "CTRL + ENTER";
            // 
            // lbl_msg
            // 
            this.lbl_msg.Enabled = false;
            this.lbl_msg.ForeColor = System.Drawing.Color.Red;
            this.lbl_msg.Location = new System.Drawing.Point(31, 583);
            this.lbl_msg.Multiline = true;
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(327, 82);
            this.lbl_msg.TabIndex = 306;
            // 
            // lbl_augrai
            // 
            this.lbl_augrai.BackColor = System.Drawing.Color.White;
            this.lbl_augrai.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_augrai.ForeColor = System.Drawing.Color.Red;
            this.lbl_augrai.Location = new System.Drawing.Point(30, 367);
            this.lbl_augrai.Name = "lbl_augrai";
            this.lbl_augrai.Size = new System.Drawing.Size(161, 24);
            this.lbl_augrai.TabIndex = 307;
            this.lbl_augrai.Text = "00000";
            // 
            // lbl_cashtype
            // 
            this.lbl_cashtype.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cashtype.Location = new System.Drawing.Point(220, 367);
            this.lbl_cashtype.Name = "lbl_cashtype";
            this.lbl_cashtype.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_cashtype.Size = new System.Drawing.Size(141, 17);
            this.lbl_cashtype.TabIndex = 308;
            this.lbl_cashtype.Text = "Cash Type";
            // 
            // lbl_key
            // 
            this.lbl_key.AutoSize = true;
            this.lbl_key.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_key.Location = new System.Drawing.Point(50, 85);
            this.lbl_key.Name = "lbl_key";
            this.lbl_key.Size = new System.Drawing.Size(80, 18);
            this.lbl_key.TabIndex = 309;
            this.lbl_key.Text = "xxxxxxxxx";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(53, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 17);
            this.label7.TabIndex = 310;
            this.label7.Text = "Last Invoice ID";
            // 
            // btn_refreshkey
            // 
            this.btn_refreshkey.BackColor = System.Drawing.Color.White;
            this.btn_refreshkey.BackgroundImage = global::ArthiPOS.Properties.Resources.refresh1x;
            this.btn_refreshkey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_refreshkey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refreshkey.Location = new System.Drawing.Point(18, 72);
            this.btn_refreshkey.Name = "btn_refreshkey";
            this.btn_refreshkey.Size = new System.Drawing.Size(32, 29);
            this.btn_refreshkey.TabIndex = 311;
            this.btn_refreshkey.UseVisualStyleBackColor = false;
            this.btn_refreshkey.Click += new System.EventHandler(this.btn_refreshkey_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 12);
            this.label1.TabIndex = 317;
            this.label1.Text = "CTRL + ENTER";
            // 
            // btn_cate
            // 
            this.btn_cate.BackColor = System.Drawing.Color.Transparent;
            this.btn_cate.BackgroundImage = global::ArthiPOS.Properties.Resources.search;
            this.btn_cate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_cate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cate.Location = new System.Drawing.Point(30, 265);
            this.btn_cate.Name = "btn_cate";
            this.btn_cate.Size = new System.Drawing.Size(20, 20);
            this.btn_cate.TabIndex = 316;
            this.btn_cate.UseVisualStyleBackColor = false;
            this.btn_cate.Click += new System.EventHandler(this.btn_cate_Click);
            // 
            // lbl_cate_id
            // 
            this.lbl_cate_id.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cate_id.Location = new System.Drawing.Point(200, 247);
            this.lbl_cate_id.Name = "lbl_cate_id";
            this.lbl_cate_id.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_cate_id.Size = new System.Drawing.Size(147, 15);
            this.lbl_cate_id.TabIndex = 315;
            this.lbl_cate_id.Text = "00000";
            // 
            // lbl_catedetail
            // 
            this.lbl_catedetail.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_catedetail.ForeColor = System.Drawing.Color.LightGray;
            this.lbl_catedetail.Location = new System.Drawing.Point(60, 297);
            this.lbl_catedetail.Name = "lbl_catedetail";
            this.lbl_catedetail.Size = new System.Drawing.Size(291, 15);
            this.lbl_catedetail.TabIndex = 318;
            this.lbl_catedetail.Text = "Detail";
            // 
            // lbl_cate_name
            // 
            this.lbl_cate_name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cate_name.Location = new System.Drawing.Point(82, 242);
            this.lbl_cate_name.Name = "lbl_cate_name";
            this.lbl_cate_name.Size = new System.Drawing.Size(147, 20);
            this.lbl_cate_name.TabIndex = 319;
            this.lbl_cate_name.Text = "Category";
            // 
            // btn_pdate
            // 
            this.btn_pdate.BackColor = System.Drawing.Color.White;
            this.btn_pdate.BackgroundImage = global::ArthiPOS.Properties.Resources.previou;
            this.btn_pdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_pdate.Location = new System.Drawing.Point(162, 107);
            this.btn_pdate.Name = "btn_pdate";
            this.btn_pdate.Size = new System.Drawing.Size(25, 23);
            this.btn_pdate.TabIndex = 320;
            this.btn_pdate.UseSelectable = true;
            this.btn_pdate.Click += new System.EventHandler(this.btn_pdate_Click);
            // 
            // btn_ndate
            // 
            this.btn_ndate.BackColor = System.Drawing.Color.White;
            this.btn_ndate.BackgroundImage = global::ArthiPOS.Properties.Resources.next;
            this.btn_ndate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_ndate.Location = new System.Drawing.Point(193, 107);
            this.btn_ndate.Name = "btn_ndate";
            this.btn_ndate.Size = new System.Drawing.Size(25, 23);
            this.btn_ndate.TabIndex = 321;
            this.btn_ndate.UseSelectable = true;
            this.btn_ndate.Click += new System.EventHandler(this.btn_ndate_Click);
            // 
            // lbl_account_trans_name
            // 
            this.lbl_account_trans_name.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_account_trans_name.Location = new System.Drawing.Point(161, 222);
            this.lbl_account_trans_name.Name = "lbl_account_trans_name";
            this.lbl_account_trans_name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_account_trans_name.Size = new System.Drawing.Size(185, 20);
            this.lbl_account_trans_name.TabIndex = 325;
            this.lbl_account_trans_name.Text = "Account";
            // 
            // lbl_expid
            // 
            this.lbl_expid.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_expid.Location = new System.Drawing.Point(31, 146);
            this.lbl_expid.Name = "lbl_expid";
            this.lbl_expid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_expid.Size = new System.Drawing.Size(123, 19);
            this.lbl_expid.TabIndex = 324;
            this.lbl_expid.Text = "xxx";
            // 
            // lbl_transaction_id
            // 
            this.lbl_transaction_id.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_transaction_id.Location = new System.Drawing.Point(31, 204);
            this.lbl_transaction_id.Name = "lbl_transaction_id";
            this.lbl_transaction_id.Size = new System.Drawing.Size(137, 15);
            this.lbl_transaction_id.TabIndex = 326;
            this.lbl_transaction_id.Text = "xxx";
            // 
            // lbl_account_trans_id
            // 
            this.lbl_account_trans_id.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_account_trans_id.Location = new System.Drawing.Point(228, 204);
            this.lbl_account_trans_id.Name = "lbl_account_trans_id";
            this.lbl_account_trans_id.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_account_trans_id.Size = new System.Drawing.Size(119, 18);
            this.lbl_account_trans_id.TabIndex = 327;
            this.lbl_account_trans_id.Text = "xxx";
            // 
            // lbl_transname
            // 
            this.lbl_transname.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_transname.Location = new System.Drawing.Point(28, 222);
            this.lbl_transname.Name = "lbl_transname";
            this.lbl_transname.Size = new System.Drawing.Size(137, 20);
            this.lbl_transname.TabIndex = 328;
            this.lbl_transname.Text = "Transaction";
            // 
            // txt_cate
            // 
            this.txt_cate.Enabled = false;
            this.txt_cate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cate.IsNumeric = false;
            this.txt_cate.LangEnglish = false;
            this.txt_cate.Location = new System.Drawing.Point(60, 265);
            this.txt_cate.Name = "txt_cate";
            this.txt_cate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_cate.Size = new System.Drawing.Size(291, 29);
            this.txt_cate.TabIndex = 314;
            this.txt_cate.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_cate.WaterMarkText = "Search....";
            // 
            // txt_nameid
            // 
            this.txt_nameid.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nameid.IsNumeric = false;
            this.txt_nameid.LangEnglish = false;
            this.txt_nameid.Location = new System.Drawing.Point(60, 334);
            this.txt_nameid.Name = "txt_nameid";
            this.txt_nameid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_nameid.Size = new System.Drawing.Size(295, 29);
            this.txt_nameid.TabIndex = 298;
            this.txt_nameid.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_nameid.WaterMarkText = "Search....";
            // 
            // txt_desc
            // 
            this.txt_desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_desc.IsNumeric = false;
            this.txt_desc.LangEnglish = false;
            this.txt_desc.Location = new System.Drawing.Point(31, 166);
            this.txt_desc.Multiline = true;
            this.txt_desc.Name = "txt_desc";
            this.txt_desc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_desc.Size = new System.Drawing.Size(328, 35);
            this.txt_desc.TabIndex = 286;
            this.txt_desc.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_desc.WaterMarkText = "Description";
            // 
            // lbl_fright
            // 
            this.lbl_fright.AutoSize = true;
            this.lbl_fright.Font = new System.Drawing.Font("Jameel Noori Nastaleeq", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fright.Location = new System.Drawing.Point(19, 34);
            this.lbl_fright.Name = "lbl_fright";
            this.lbl_fright.Size = new System.Drawing.Size(56, 27);
            this.lbl_fright.TabIndex = 329;
            this.lbl_fright.TabStop = true;
            this.lbl_fright.Text = "Fright";
            this.lbl_fright.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_fright_LinkClicked);
            // 
            // lbl_fardhisab
            // 
            this.lbl_fardhisab.AutoSize = true;
            this.lbl_fardhisab.Font = new System.Drawing.Font("Jameel Noori Nastaleeq", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fardhisab.Location = new System.Drawing.Point(72, 34);
            this.lbl_fardhisab.Name = "lbl_fardhisab";
            this.lbl_fardhisab.Size = new System.Drawing.Size(92, 27);
            this.lbl_fardhisab.TabIndex = 330;
            this.lbl_fardhisab.TabStop = true;
            this.lbl_fardhisab.Text = "Fard Hisab";
            this.lbl_fardhisab.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_fardhisab_LinkClicked);
            // 
            // lbl_remaining_amount
            // 
            this.lbl_remaining_amount.BackColor = System.Drawing.Color.White;
            this.lbl_remaining_amount.Font = new System.Drawing.Font("Arial", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_remaining_amount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbl_remaining_amount.Location = new System.Drawing.Point(30, 544);
            this.lbl_remaining_amount.Name = "lbl_remaining_amount";
            this.lbl_remaining_amount.Size = new System.Drawing.Size(325, 36);
            this.lbl_remaining_amount.TabIndex = 331;
            this.lbl_remaining_amount.Text = "00000000000";
            this.lbl_remaining_amount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(302, 563);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 17);
            this.label8.TabIndex = 332;
            this.label8.Text = "بقایا ‌رقم";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(282, 129);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(71, 17);
            this.label6.TabIndex = 333;
            this.label6.Text = "Press F5";
            // 
            // txt_details
            // 
            this.txt_details.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_details.IsNumeric = false;
            this.txt_details.LangEnglish = false;
            this.txt_details.Location = new System.Drawing.Point(32, 449);
            this.txt_details.Multiline = true;
            this.txt_details.Name = "txt_details";
            this.txt_details.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_details.Size = new System.Drawing.Size(328, 46);
            this.txt_details.TabIndex = 334;
            this.txt_details.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_details.WaterMarkText = "تفصیل";
            // 
            // AddExpenseCash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 669);
            this.Controls.Add(this.txt_details);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbl_remaining_amount);
            this.Controls.Add(this.lbl_fardhisab);
            this.Controls.Add(this.lbl_fright);
            this.Controls.Add(this.lbl_transname);
            this.Controls.Add(this.lbl_account_trans_id);
            this.Controls.Add(this.lbl_transaction_id);
            this.Controls.Add(this.lbl_account_trans_name);
            this.Controls.Add(this.lbl_expid);
            this.Controls.Add(this.btn_pdate);
            this.Controls.Add(this.btn_ndate);
            this.Controls.Add(this.lbl_cate_name);
            this.Controls.Add(this.lbl_catedetail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_cate);
            this.Controls.Add(this.lbl_cate_id);
            this.Controls.Add(this.txt_cate);
            this.Controls.Add(this.btn_refreshkey);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbl_key);
            this.Controls.Add(this.lbl_cashtype);
            this.Controls.Add(this.lbl_augrai);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_discount);
            this.Controls.Add(this.btn_bipari_search);
            this.Controls.Add(this.lbl_id);
            this.Controls.Add(this.txt_nameid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddcash);
            this.Controls.Add(this.ledger_date);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.lbl_add_cash);
            this.Controls.Add(this.txt_cash);
            this.Controls.Add(this.txt_desc);
            this.Name = "AddExpenseCash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddAdminCash";
            this.Load += new System.EventHandler(this.AddExpenseCash_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UrduTextBox txt_desc;
        private System.Windows.Forms.Label lbl_add_cash;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_cash;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroDateTime ledger_date;
        private Bunifu.Framework.UI.BunifuFlatButton btnAddcash;
        private System.Windows.Forms.Button btn_bipari_search;
        private System.Windows.Forms.Label lbl_id;
        private UrduTextBox txt_nameid;
        private System.Windows.Forms.TextBox txt_discount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox lbl_msg;
        private System.Windows.Forms.Label lbl_augrai;
        private System.Windows.Forms.Label lbl_cashtype;
        private System.Windows.Forms.Label lbl_key;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_refreshkey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_cate;
        private System.Windows.Forms.Label lbl_cate_id;
        private UrduTextBox txt_cate;
        private System.Windows.Forms.Label lbl_catedetail;
        private System.Windows.Forms.Label lbl_cate_name;
        private MetroFramework.Controls.MetroButton btn_pdate;
        private MetroFramework.Controls.MetroButton btn_ndate;
        private System.Windows.Forms.Label lbl_account_trans_name;
        private System.Windows.Forms.Label lbl_expid;
        private System.Windows.Forms.Label lbl_transaction_id;
        private System.Windows.Forms.Label lbl_account_trans_id;
        private System.Windows.Forms.Label lbl_transname;
        private System.Windows.Forms.LinkLabel lbl_fright;
        private System.Windows.Forms.LinkLabel lbl_fardhisab;
        private System.Windows.Forms.Label lbl_remaining_amount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private UrduTextBox txt_details;
    }
}