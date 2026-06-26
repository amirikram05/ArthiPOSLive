namespace ArthiPOS.Controls.dashboard
{
    partial class Search
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
            this.grid_shop = new MetroFramework.Controls.MetroGrid();
            this.comb_select_searchtype = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_add_Item = new Bunifu.Framework.UI.BunifuImageButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_all_bip = new System.Windows.Forms.CheckBox();
            this.txt_bip_id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_amount = new System.Windows.Forms.RadioButton();
            this.chk_name = new System.Windows.Forms.RadioButton();
            this.chk_id = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rd_desc = new System.Windows.Forms.RadioButton();
            this.rd_asc = new System.Windows.Forms.RadioButton();
            this.txt_searach = new ArthiPOS.Controls.UrduTextBox();
            this.txt_address = new ArthiPOS.Controls.UrduTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grid_shop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_add_Item)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.grid_shop.Location = new System.Drawing.Point(8, 77);
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
            this.grid_shop.Size = new System.Drawing.Size(1170, 475);
            this.grid_shop.Style = MetroFramework.MetroColorStyle.Green;
            this.grid_shop.TabIndex = 1;
            this.grid_shop.Theme = MetroFramework.MetroThemeStyle.Light;
            this.grid_shop.UseStyleColors = true;
            this.grid_shop.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_shop_CellClick);
            this.grid_shop.SelectionChanged += new System.EventHandler(this.grid_shop_SelectionChanged);
            // 
            // comb_select_searchtype
            // 
            this.comb_select_searchtype.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comb_select_searchtype.FormattingEnabled = true;
            this.comb_select_searchtype.Items.AddRange(new object[] {
            "Client",
            "Customer",
            "Product",
            "Weight",
            "ExpenseType"});
            this.comb_select_searchtype.Location = new System.Drawing.Point(464, 5);
            this.comb_select_searchtype.Name = "comb_select_searchtype";
            this.comb_select_searchtype.Size = new System.Drawing.Size(119, 28);
            this.comb_select_searchtype.TabIndex = 2;
            this.comb_select_searchtype.Text = "Select";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(573, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 12);
            this.label2.TabIndex = 164;
            this.label2.Text = "CTRL + Enter";
            // 
            // btn_add_Item
            // 
            this.btn_add_Item.BackColor = System.Drawing.Color.Transparent;
            this.btn_add_Item.Image = global::ArthiPOS.Properties.Resources.add;
            this.btn_add_Item.ImageActive = null;
            this.btn_add_Item.Location = new System.Drawing.Point(589, 5);
            this.btn_add_Item.Name = "btn_add_Item";
            this.btn_add_Item.Size = new System.Drawing.Size(30, 30);
            this.btn_add_Item.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_add_Item.TabIndex = 163;
            this.btn_add_Item.TabStop = false;
            this.btn_add_Item.Zoom = 10;
            this.btn_add_Item.Click += new System.EventHandler(this.btn_add_Item_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chk_all_bip);
            this.groupBox1.Controls.Add(this.txt_bip_id);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chk_amount);
            this.groupBox1.Controls.Add(this.chk_name);
            this.groupBox1.Controls.Add(this.chk_id);
            this.groupBox1.Location = new System.Drawing.Point(608, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(571, 40);
            this.groupBox1.TabIndex = 168;
            this.groupBox1.TabStop = false;
            // 
            // chk_all_bip
            // 
            this.chk_all_bip.AutoSize = true;
            this.chk_all_bip.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_all_bip.Location = new System.Drawing.Point(145, 14);
            this.chk_all_bip.Name = "chk_all_bip";
            this.chk_all_bip.Size = new System.Drawing.Size(103, 22);
            this.chk_all_bip.TabIndex = 177;
            this.chk_all_bip.Text = "Search ALL";
            this.chk_all_bip.UseVisualStyleBackColor = true;
            this.chk_all_bip.CheckedChanged += new System.EventHandler(this.chk_all_bip_CheckedChanged);
            // 
            // txt_bip_id
            // 
            this.txt_bip_id.Enabled = false;
            this.txt_bip_id.Location = new System.Drawing.Point(54, 16);
            this.txt_bip_id.Name = "txt_bip_id";
            this.txt_bip_id.Size = new System.Drawing.Size(85, 20);
            this.txt_bip_id.TabIndex = 172;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(78, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 176;
            this.label1.Text = "Bipari ID";
            // 
            // chk_amount
            // 
            this.chk_amount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_amount.AutoSize = true;
            this.chk_amount.Location = new System.Drawing.Point(403, 12);
            this.chk_amount.Name = "chk_amount";
            this.chk_amount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_amount.Size = new System.Drawing.Size(61, 17);
            this.chk_amount.TabIndex = 170;
            this.chk_amount.Text = "Amount";
            this.chk_amount.UseVisualStyleBackColor = true;
            this.chk_amount.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chk_name
            // 
            this.chk_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_name.AutoSize = true;
            this.chk_name.Location = new System.Drawing.Point(470, 12);
            this.chk_name.Name = "chk_name";
            this.chk_name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_name.Size = new System.Drawing.Size(53, 17);
            this.chk_name.TabIndex = 171;
            this.chk_name.Text = "Name";
            this.chk_name.UseVisualStyleBackColor = true;
            this.chk_name.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chk_id
            // 
            this.chk_id.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_id.AutoSize = true;
            this.chk_id.Checked = true;
            this.chk_id.Location = new System.Drawing.Point(529, 12);
            this.chk_id.Name = "chk_id";
            this.chk_id.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_id.Size = new System.Drawing.Size(36, 17);
            this.chk_id.TabIndex = 169;
            this.chk_id.TabStop = true;
            this.chk_id.Text = "ID";
            this.chk_id.UseVisualStyleBackColor = true;
            this.chk_id.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rd_desc);
            this.groupBox2.Controls.Add(this.rd_asc);
            this.groupBox2.Location = new System.Drawing.Point(851, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(132, 40);
            this.groupBox2.TabIndex = 172;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sort";
            // 
            // rd_desc
            // 
            this.rd_desc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rd_desc.AutoSize = true;
            this.rd_desc.Location = new System.Drawing.Point(34, 16);
            this.rd_desc.Name = "rd_desc";
            this.rd_desc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rd_desc.Size = new System.Drawing.Size(50, 17);
            this.rd_desc.TabIndex = 171;
            this.rd_desc.Text = "Desc";
            this.rd_desc.UseVisualStyleBackColor = true;
            this.rd_desc.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // rd_asc
            // 
            this.rd_asc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rd_asc.AutoSize = true;
            this.rd_asc.Checked = true;
            this.rd_asc.Location = new System.Drawing.Point(83, 16);
            this.rd_asc.Name = "rd_asc";
            this.rd_asc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rd_asc.Size = new System.Drawing.Size(43, 17);
            this.rd_asc.TabIndex = 169;
            this.rd_asc.TabStop = true;
            this.rd_asc.Text = "Asc";
            this.rd_asc.UseVisualStyleBackColor = true;
            this.rd_asc.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txt_searach
            // 
            this.txt_searach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_searach.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txt_searach.IsNumeric = false;
            this.txt_searach.LangEnglish = false;
            this.txt_searach.Location = new System.Drawing.Point(790, 6);
            this.txt_searach.Name = "txt_searach";
            this.txt_searach.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_searach.Size = new System.Drawing.Size(388, 31);
            this.txt_searach.TabIndex = 0;
            this.txt_searach.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_searach.WaterMarkText = "Search";
            this.txt_searach.TextChanged += new System.EventHandler(this.txt_searach_TextChanged);
            // 
            // txt_address
            // 
            this.txt_address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_address.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txt_address.IsNumeric = false;
            this.txt_address.LangEnglish = false;
            this.txt_address.Location = new System.Drawing.Point(3, 3);
            this.txt_address.Name = "txt_address";
            this.txt_address.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_address.Size = new System.Drawing.Size(153, 31);
            this.txt_address.TabIndex = 173;
            this.txt_address.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_address.WaterMarkText = "Address";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_address);
            this.panel1.Location = new System.Drawing.Point(625, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(159, 37);
            this.panel1.TabIndex = 175;
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.txt_searach);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_add_Item);
            this.Controls.Add(this.comb_select_searchtype);
            this.Controls.Add(this.grid_shop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Search";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search";
            this.Load += new System.EventHandler(this.Search_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid_shop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_add_Item)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroGrid grid_shop;
        private System.Windows.Forms.ComboBox comb_select_searchtype;
        private System.Windows.Forms.Label label2;
        private Bunifu.Framework.UI.BunifuImageButton btn_add_Item;
        public UrduTextBox txt_searach;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton chk_id;
        private System.Windows.Forms.RadioButton chk_amount;
        private System.Windows.Forms.RadioButton chk_name;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rd_desc;
        private System.Windows.Forms.RadioButton rd_asc;
        public UrduTextBox txt_address;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_bip_id;
        private System.Windows.Forms.CheckBox chk_all_bip;
    }
}