namespace ArthiPOS.controls.dashboard
{
    partial class Invoicing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Invoicing));
            this.btn_print_all_bill = new System.Windows.Forms.FlowLayoutPanel();
            this.billtype_combo = new MetroFramework.Controls.MetroComboBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.today_date = new MetroFramework.Controls.MetroDateTime();
            this.nextdate = new MetroFramework.Controls.MetroButton();
            this.previousdate = new MetroFramework.Controls.MetroButton();
            this.btn_print_all_bills = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_update_sales = new Bunifu.Framework.UI.BunifuFlatButton();
            this.chk_status_localload = new System.Windows.Forms.CheckBox();
            this.txt_search = new ArthiPOS.Controls.UrduTextBox();
            this.panel13.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_print_all_bill
            // 
            this.btn_print_all_bill.AutoScroll = true;
            this.btn_print_all_bill.Location = new System.Drawing.Point(3, 65);
            this.btn_print_all_bill.Name = "btn_print_all_bill";
            this.btn_print_all_bill.Size = new System.Drawing.Size(1193, 588);
            this.btn_print_all_bill.TabIndex = 2;
            // 
            // billtype_combo
            // 
            this.billtype_combo.FormattingEnabled = true;
            this.billtype_combo.ItemHeight = 23;
            this.billtype_combo.Items.AddRange(new object[] {
            "Client",
            "Customer",
            "Landlord"});
            this.billtype_combo.Location = new System.Drawing.Point(568, 5);
            this.billtype_combo.Name = "billtype_combo";
            this.billtype_combo.PromptText = "Select";
            this.billtype_combo.Size = new System.Drawing.Size(115, 29);
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
            this.panel13.Location = new System.Drawing.Point(200, 0);
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
            this.today_date.ValueChanged += new System.EventHandler(this.today_date_ValueChanged);
            // 
            // nextdate
            // 
            this.nextdate.BackgroundImage = global::ArthiPOS.Properties.Resources.next;
            this.nextdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.nextdate.Location = new System.Drawing.Point(192, 6);
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
            this.btn_print_all_bills.Location = new System.Drawing.Point(428, 4);
            this.btn_print_all_bills.Name = "btn_print_all_bills";
            this.btn_print_all_bills.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_print_all_bills.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btn_print_all_bills.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_print_all_bills.selected = false;
            this.btn_print_all_bills.Size = new System.Drawing.Size(134, 30);
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
            this.btn_update_sales.Location = new System.Drawing.Point(890, 3);
            this.btn_update_sales.Name = "btn_update_sales";
            this.btn_update_sales.Normalcolor = System.Drawing.Color.White;
            this.btn_update_sales.OnHovercolor = System.Drawing.Color.Silver;
            this.btn_update_sales.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_update_sales.selected = false;
            this.btn_update_sales.Size = new System.Drawing.Size(306, 35);
            this.btn_update_sales.TabIndex = 199;
            this.btn_update_sales.Text = "Update Daily Sales";
            this.btn_update_sales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_update_sales.Textcolor = System.Drawing.Color.Gray;
            this.btn_update_sales.TextFont = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_update_sales.Click += new System.EventHandler(this.btn_update_sales_Click);
            // 
            // chk_status_localload
            // 
            this.chk_status_localload.Appearance = System.Windows.Forms.Appearance.Button;
            this.chk_status_localload.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.chk_status_localload.Location = new System.Drawing.Point(3, 0);
            this.chk_status_localload.Name = "chk_status_localload";
            this.chk_status_localload.Size = new System.Drawing.Size(191, 34);
            this.chk_status_localload.TabIndex = 200;
            this.chk_status_localload.Text = "Load From Local";
            this.chk_status_localload.UseVisualStyleBackColor = false;
            this.chk_status_localload.Click += new System.EventHandler(this.chk_status_localload_Click);
            // 
            // txt_search
            // 
            this.txt_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txt_search.IsNumeric = false;
            this.txt_search.LangEnglish = false;
            this.txt_search.Location = new System.Drawing.Point(689, 3);
            this.txt_search.Name = "txt_search";
            this.txt_search.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_search.Size = new System.Drawing.Size(195, 29);
            this.txt_search.TabIndex = 201;
            this.txt_search.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_search.WaterMarkText = "Search";
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // Invoicing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.chk_status_localload);
            this.Controls.Add(this.btn_update_sales);
            this.Controls.Add(this.btn_print_all_bills);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.billtype_combo);
            this.Controls.Add(this.btn_print_all_bill);
            this.Name = "Invoicing";
            this.Size = new System.Drawing.Size(1199, 656);
            this.Load += new System.EventHandler(this.Invoicing_Load);
            this.panel13.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel btn_print_all_bill;
        private MetroFramework.Controls.MetroComboBox billtype_combo;
        private System.Windows.Forms.Panel panel13;
        private MetroFramework.Controls.MetroDateTime today_date;
        private MetroFramework.Controls.MetroButton nextdate;
        private MetroFramework.Controls.MetroButton previousdate;
        private Bunifu.Framework.UI.BunifuFlatButton btn_print_all_bills;
        private Bunifu.Framework.UI.BunifuFlatButton btn_update_sales;
        private System.Windows.Forms.CheckBox chk_status_localload;
        private Controls.UrduTextBox txt_search;
    }
}
