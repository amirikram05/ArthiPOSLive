namespace ArthiPOS.Controls.dashboard
{
    partial class AddExtraAmount
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
            this.btn_save_extra_amount = new MetroFramework.Controls.MetroButton();
            this.txt_add_land_extra_amount = new ArthiPOS.Controls.UrduTextBox();
            this._lbl_add_extra_amount = new System.Windows.Forms.Label();
            this.lbl_s_name = new System.Windows.Forms.Label();
            this.lbl_s_id = new System.Windows.Forms.Label();
            this._lbl_khata = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_grand_total = new System.Windows.Forms.Label();
            this._lbl_total_amount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_list_sale = new System.Windows.Forms.Label();
            this.txt_customer_extra_amount = new ArthiPOS.Controls.UrduTextBox();
            this._lbl_customer_extra_amount = new System.Windows.Forms.Label();
            this.btn_customer_amount = new MetroFramework.Controls.MetroButton();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_save_extra_amount
            // 
            this.btn_save_extra_amount.BackColor = System.Drawing.Color.Transparent;
            this.btn_save_extra_amount.BackgroundImage = global::ArthiPOS.Properties.Resources.edit;
            this.btn_save_extra_amount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_save_extra_amount.Location = new System.Drawing.Point(15, 159);
            this.btn_save_extra_amount.Name = "btn_save_extra_amount";
            this.btn_save_extra_amount.Size = new System.Drawing.Size(30, 29);
            this.btn_save_extra_amount.TabIndex = 220;
            this.btn_save_extra_amount.UseSelectable = true;
            this.btn_save_extra_amount.Click += new System.EventHandler(this.btn_save_extra_amount_Click);
            // 
            // txt_add_land_extra_amount
            // 
            this.txt_add_land_extra_amount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_add_land_extra_amount.IsNumeric = true;
            this.txt_add_land_extra_amount.LangEnglish = true;
            this.txt_add_land_extra_amount.Location = new System.Drawing.Point(51, 159);
            this.txt_add_land_extra_amount.Name = "txt_add_land_extra_amount";
            this.txt_add_land_extra_amount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_add_land_extra_amount.Size = new System.Drawing.Size(130, 29);
            this.txt_add_land_extra_amount.TabIndex = 218;
            this.txt_add_land_extra_amount.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_add_land_extra_amount.WaterMarkText = "000000";
            // 
            // _lbl_add_extra_amount
            // 
            this._lbl_add_extra_amount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lbl_add_extra_amount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._lbl_add_extra_amount.Location = new System.Drawing.Point(187, 164);
            this._lbl_add_extra_amount.Name = "_lbl_add_extra_amount";
            this._lbl_add_extra_amount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lbl_add_extra_amount.Size = new System.Drawing.Size(141, 21);
            this._lbl_add_extra_amount.TabIndex = 219;
            this._lbl_add_extra_amount.Text = "Landlord Extra Amount";
            // 
            // lbl_s_name
            // 
            this.lbl_s_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_s_name.Location = new System.Drawing.Point(49, 31);
            this.lbl_s_name.Name = "lbl_s_name";
            this.lbl_s_name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_s_name.Size = new System.Drawing.Size(184, 26);
            this.lbl_s_name.TabIndex = 221;
            this.lbl_s_name.Text = "Admin";
            this.lbl_s_name.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_s_id
            // 
            this.lbl_s_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_s_id.Location = new System.Drawing.Point(78, 72);
            this.lbl_s_id.Name = "lbl_s_id";
            this.lbl_s_id.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_s_id.Size = new System.Drawing.Size(114, 18);
            this.lbl_s_id.TabIndex = 223;
            this.lbl_s_id.Text = "xxxxxxxxxxxxx";
            // 
            // _lbl_khata
            // 
            this._lbl_khata.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lbl_khata.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lbl_khata.Location = new System.Drawing.Point(205, 72);
            this._lbl_khata.Name = "_lbl_khata";
            this._lbl_khata.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lbl_khata.Size = new System.Drawing.Size(65, 18);
            this._lbl_khata.TabIndex = 222;
            this._lbl_khata.Text = "Khata #";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGreen;
            this.panel2.Controls.Add(this.lbl_grand_total);
            this.panel2.Controls.Add(this._lbl_total_amount);
            this.panel2.Location = new System.Drawing.Point(12, 106);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(263, 37);
            this.panel2.TabIndex = 224;
            // 
            // lbl_grand_total
            // 
            this.lbl_grand_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_grand_total.ForeColor = System.Drawing.Color.Red;
            this.lbl_grand_total.Location = new System.Drawing.Point(3, 8);
            this.lbl_grand_total.Name = "lbl_grand_total";
            this.lbl_grand_total.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_grand_total.Size = new System.Drawing.Size(143, 27);
            this.lbl_grand_total.TabIndex = 27;
            this.lbl_grand_total.Text = "0000000000";
            // 
            // _lbl_total_amount
            // 
            this._lbl_total_amount.AutoSize = true;
            this._lbl_total_amount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lbl_total_amount.Location = new System.Drawing.Point(152, 12);
            this._lbl_total_amount.Name = "_lbl_total_amount";
            this._lbl_total_amount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lbl_total_amount.Size = new System.Drawing.Size(108, 18);
            this._lbl_total_amount.TabIndex = 26;
            this._lbl_total_amount.Text = "Total Amount";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(131, 5);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(159, 21);
            this.label1.TabIndex = 225;
            this.label1.Text = "Bill-Client/Customer";
            // 
            // lbl_list_sale
            // 
            this.lbl_list_sale.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_list_sale.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_list_sale.Location = new System.Drawing.Point(12, 231);
            this.lbl_list_sale.Name = "lbl_list_sale";
            this.lbl_list_sale.Size = new System.Drawing.Size(316, 81);
            this.lbl_list_sale.TabIndex = 226;
            this.lbl_list_sale.Text = "000";
            // 
            // txt_customer_extra_amount
            // 
            this.txt_customer_extra_amount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_customer_extra_amount.IsNumeric = true;
            this.txt_customer_extra_amount.LangEnglish = true;
            this.txt_customer_extra_amount.Location = new System.Drawing.Point(51, 194);
            this.txt_customer_extra_amount.Name = "txt_customer_extra_amount";
            this.txt_customer_extra_amount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_customer_extra_amount.Size = new System.Drawing.Size(130, 29);
            this.txt_customer_extra_amount.TabIndex = 227;
            this.txt_customer_extra_amount.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_customer_extra_amount.WaterMarkText = "000000";
            // 
            // _lbl_customer_extra_amount
            // 
            this._lbl_customer_extra_amount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lbl_customer_extra_amount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._lbl_customer_extra_amount.Location = new System.Drawing.Point(187, 199);
            this._lbl_customer_extra_amount.Name = "_lbl_customer_extra_amount";
            this._lbl_customer_extra_amount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lbl_customer_extra_amount.Size = new System.Drawing.Size(141, 21);
            this._lbl_customer_extra_amount.TabIndex = 228;
            this._lbl_customer_extra_amount.Text = "Customer Extra Amount";
            // 
            // btn_customer_amount
            // 
            this.btn_customer_amount.BackColor = System.Drawing.Color.Transparent;
            this.btn_customer_amount.BackgroundImage = global::ArthiPOS.Properties.Resources.edit;
            this.btn_customer_amount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_customer_amount.Location = new System.Drawing.Point(15, 194);
            this.btn_customer_amount.Name = "btn_customer_amount";
            this.btn_customer_amount.Size = new System.Drawing.Size(30, 29);
            this.btn_customer_amount.TabIndex = 229;
            this.btn_customer_amount.UseSelectable = true;
            this.btn_customer_amount.Click += new System.EventHandler(this.btn_customer_amount_Click);
            // 
            // AddExtraAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 321);
            this.Controls.Add(this.btn_customer_amount);
            this.Controls.Add(this.txt_customer_extra_amount);
            this.Controls.Add(this._lbl_customer_extra_amount);
            this.Controls.Add(this.lbl_list_sale);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lbl_s_id);
            this.Controls.Add(this._lbl_khata);
            this.Controls.Add(this.lbl_s_name);
            this.Controls.Add(this.btn_save_extra_amount);
            this.Controls.Add(this.txt_add_land_extra_amount);
            this.Controls.Add(this._lbl_add_extra_amount);
            this.Name = "AddExtraAmount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddExtraAmount";
            this.Load += new System.EventHandler(this.AddExtraAmount_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btn_save_extra_amount;
        private ArthiPOS.Controls.UrduTextBox txt_add_land_extra_amount;
        private System.Windows.Forms.Label _lbl_add_extra_amount;
        public System.Windows.Forms.Label lbl_s_name;
        public System.Windows.Forms.Label lbl_s_id;
        public System.Windows.Forms.Label _lbl_khata;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label lbl_grand_total;
        public System.Windows.Forms.Label _lbl_total_amount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_list_sale;
        private ArthiPOS.Controls.UrduTextBox txt_customer_extra_amount;
        private System.Windows.Forms.Label _lbl_customer_extra_amount;
        private MetroFramework.Controls.MetroButton btn_customer_amount;
    }
}