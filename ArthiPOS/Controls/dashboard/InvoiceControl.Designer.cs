namespace ArthiPOS.controls.dashboard
{
    partial class InvoiceControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceControl));
            this.panel_header_top = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this._lbl_khata_id = new MetroFramework.Controls.MetroLabel();
            this.lbl_invoice_no = new MetroFramework.Controls.MetroLabel();
            this.lbl_count = new MetroFramework.Controls.MetroTile();
            this.lbl_name = new MetroFramework.Controls.MetroLabel();
            this.lbl_total_amount = new MetroFramework.Controls.MetroTile();
            this._lbl_total_amount = new MetroFramework.Controls.MetroLabel();
            this.bunifuThinButton22 = new Bunifu.Framework.UI.BunifuThinButton2();
            this.btn_print = new Bunifu.Framework.UI.BunifuThinButton2();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_chongi = new MetroFramework.Controls.MetroLabel();
            this._lbl_chongi = new MetroFramework.Controls.MetroLabel();
            this.lbl_commission = new MetroFramework.Controls.MetroLabel();
            this._lbl_commission = new MetroFramework.Controls.MetroLabel();
            this.lbl_advance = new MetroFramework.Controls.MetroLabel();
            this._lbl_advance = new MetroFramework.Controls.MetroLabel();
            this.lbl_munshiana = new MetroFramework.Controls.MetroLabel();
            this._lbl_munshiana = new MetroFramework.Controls.MetroLabel();
            this.lbl_labour = new MetroFramework.Controls.MetroLabel();
            this._lbl_labour = new MetroFramework.Controls.MetroLabel();
            this.lbl_rent = new MetroFramework.Controls.MetroLabel();
            this._lbl_rent = new MetroFramework.Controls.MetroLabel();
            this.lbl_total_quantity = new MetroFramework.Controls.MetroLabel();
            this._lbl_total_quantity = new MetroFramework.Controls.MetroLabel();
            this.panel_header_top.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_header_top
            // 
            this.panel_header_top.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel_header_top.Controls.Add(this.panel3);
            this.panel_header_top.Controls.Add(this.lbl_count);
            this.panel_header_top.Controls.Add(this.lbl_name);
            this.panel_header_top.Location = new System.Drawing.Point(3, 3);
            this.panel_header_top.Name = "panel_header_top";
            this.panel_header_top.Size = new System.Drawing.Size(375, 66);
            this.panel_header_top.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this._lbl_khata_id);
            this.panel3.Controls.Add(this.lbl_invoice_no);
            this.panel3.Location = new System.Drawing.Point(199, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(173, 26);
            this.panel3.TabIndex = 5;
            // 
            // _lbl_khata_id
            // 
            this._lbl_khata_id.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this._lbl_khata_id.Location = new System.Drawing.Point(94, 3);
            this._lbl_khata_id.Name = "_lbl_khata_id";
            this._lbl_khata_id.Size = new System.Drawing.Size(76, 19);
            this._lbl_khata_id.Style = MetroFramework.MetroColorStyle.Black;
            this._lbl_khata_id.TabIndex = 12;
            this._lbl_khata_id.Text = "Invoice #";
            this._lbl_khata_id.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lbl_khata_id.UseStyleColors = true;
            // 
            // lbl_invoice_no
            // 
            this.lbl_invoice_no.AutoSize = true;
            this.lbl_invoice_no.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_invoice_no.Location = new System.Drawing.Point(3, 3);
            this.lbl_invoice_no.Name = "lbl_invoice_no";
            this.lbl_invoice_no.Size = new System.Drawing.Size(71, 19);
            this.lbl_invoice_no.TabIndex = 1;
            this.lbl_invoice_no.Text = "CL-11235";
            // 
            // lbl_count
            // 
            this.lbl_count.ActiveControl = null;
            this.lbl_count.Location = new System.Drawing.Point(0, 0);
            this.lbl_count.Name = "lbl_count";
            this.lbl_count.Size = new System.Drawing.Size(75, 63);
            this.lbl_count.TabIndex = 4;
            this.lbl_count.Text = "0";
            this.lbl_count.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_count.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.lbl_count.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.lbl_count.UseSelectable = true;
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_name.Location = new System.Drawing.Point(149, 36);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(63, 25);
            this.lbl_name.TabIndex = 3;
            this.lbl_name.Text = "Testing";
            this.lbl_name.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_total_amount
            // 
            this.lbl_total_amount.ActiveControl = null;
            this.lbl_total_amount.Location = new System.Drawing.Point(209, 101);
            this.lbl_total_amount.Name = "lbl_total_amount";
            this.lbl_total_amount.Size = new System.Drawing.Size(166, 80);
            this.lbl_total_amount.Style = MetroFramework.MetroColorStyle.Orange;
            this.lbl_total_amount.TabIndex = 10;
            this.lbl_total_amount.Text = "0000";
            this.lbl_total_amount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_total_amount.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_total_amount.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.lbl_total_amount.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.lbl_total_amount.UseSelectable = true;
            // 
            // _lbl_total_amount
            // 
            this._lbl_total_amount.AutoSize = true;
            this._lbl_total_amount.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this._lbl_total_amount.Location = new System.Drawing.Point(276, 77);
            this._lbl_total_amount.Name = "_lbl_total_amount";
            this._lbl_total_amount.Size = new System.Drawing.Size(99, 19);
            this._lbl_total_amount.Style = MetroFramework.MetroColorStyle.Green;
            this._lbl_total_amount.TabIndex = 11;
            this._lbl_total_amount.Text = "Total Amount";
            this._lbl_total_amount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lbl_total_amount.UseStyleColors = true;
            // 
            // bunifuThinButton22
            // 
            this.bunifuThinButton22.ActiveBorderThickness = 1;
            this.bunifuThinButton22.ActiveCornerRadius = 20;
            this.bunifuThinButton22.ActiveFillColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton22.ActiveForecolor = System.Drawing.Color.WhiteSmoke;
            this.bunifuThinButton22.ActiveLineColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton22.BackColor = System.Drawing.Color.White;
            this.bunifuThinButton22.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuThinButton22.BackgroundImage")));
            this.bunifuThinButton22.ButtonText = "Preview";
            this.bunifuThinButton22.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuThinButton22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuThinButton22.ForeColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton22.IdleBorderThickness = 1;
            this.bunifuThinButton22.IdleCornerRadius = 5;
            this.bunifuThinButton22.IdleFillColor = System.Drawing.Color.White;
            this.bunifuThinButton22.IdleForecolor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton22.IdleLineColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton22.Location = new System.Drawing.Point(209, 182);
            this.bunifuThinButton22.Margin = new System.Windows.Forms.Padding(5);
            this.bunifuThinButton22.Name = "bunifuThinButton22";
            this.bunifuThinButton22.Size = new System.Drawing.Size(166, 36);
            this.bunifuThinButton22.TabIndex = 1;
            this.bunifuThinButton22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuThinButton22.Click += new System.EventHandler(this.bunifuThinButton22_Click);
            // 
            // btn_print
            // 
            this.btn_print.ActiveBorderThickness = 1;
            this.btn_print.ActiveCornerRadius = 20;
            this.btn_print.ActiveFillColor = System.Drawing.Color.SeaGreen;
            this.btn_print.ActiveForecolor = System.Drawing.Color.WhiteSmoke;
            this.btn_print.ActiveLineColor = System.Drawing.Color.SeaGreen;
            this.btn_print.BackColor = System.Drawing.Color.White;
            this.btn_print.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_print.BackgroundImage")));
            this.btn_print.ButtonText = "Print";
            this.btn_print.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print.ForeColor = System.Drawing.Color.SeaGreen;
            this.btn_print.IdleBorderThickness = 1;
            this.btn_print.IdleCornerRadius = 5;
            this.btn_print.IdleFillColor = System.Drawing.Color.White;
            this.btn_print.IdleForecolor = System.Drawing.Color.SeaGreen;
            this.btn_print.IdleLineColor = System.Drawing.Color.SeaGreen;
            this.btn_print.Location = new System.Drawing.Point(211, 219);
            this.btn_print.Margin = new System.Windows.Forms.Padding(5);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(163, 36);
            this.btn_print.TabIndex = 0;
            this.btn_print.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.Gray;
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(0, 256);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(381, 3);
            this.materialDivider1.TabIndex = 32;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbl_chongi);
            this.panel2.Controls.Add(this._lbl_chongi);
            this.panel2.Controls.Add(this.lbl_commission);
            this.panel2.Controls.Add(this._lbl_commission);
            this.panel2.Controls.Add(this.lbl_advance);
            this.panel2.Controls.Add(this._lbl_advance);
            this.panel2.Controls.Add(this.lbl_munshiana);
            this.panel2.Controls.Add(this._lbl_munshiana);
            this.panel2.Controls.Add(this.lbl_labour);
            this.panel2.Controls.Add(this._lbl_labour);
            this.panel2.Controls.Add(this.lbl_rent);
            this.panel2.Controls.Add(this._lbl_rent);
            this.panel2.Controls.Add(this.lbl_total_quantity);
            this.panel2.Controls.Add(this._lbl_total_quantity);
            this.panel2.Location = new System.Drawing.Point(3, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 176);
            this.panel2.TabIndex = 33;
            // 
            // lbl_chongi
            // 
            this.lbl_chongi.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_chongi.Location = new System.Drawing.Point(9, 147);
            this.lbl_chongi.Name = "lbl_chongi";
            this.lbl_chongi.Size = new System.Drawing.Size(62, 15);
            this.lbl_chongi.TabIndex = 29;
            this.lbl_chongi.Text = "0000";
            this.lbl_chongi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lbl_chongi
            // 
            this._lbl_chongi.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this._lbl_chongi.Location = new System.Drawing.Point(77, 144);
            this._lbl_chongi.Name = "_lbl_chongi";
            this._lbl_chongi.Size = new System.Drawing.Size(104, 24);
            this._lbl_chongi.Style = MetroFramework.MetroColorStyle.Red;
            this._lbl_chongi.TabIndex = 28;
            this._lbl_chongi.Text = "Chongi";
            this._lbl_chongi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lbl_chongi.UseStyleColors = true;
            // 
            // lbl_commission
            // 
            this.lbl_commission.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_commission.Location = new System.Drawing.Point(9, 123);
            this.lbl_commission.Name = "lbl_commission";
            this.lbl_commission.Size = new System.Drawing.Size(62, 15);
            this.lbl_commission.TabIndex = 27;
            this.lbl_commission.Text = "0000";
            this.lbl_commission.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lbl_commission
            // 
            this._lbl_commission.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this._lbl_commission.Location = new System.Drawing.Point(77, 120);
            this._lbl_commission.Name = "_lbl_commission";
            this._lbl_commission.Size = new System.Drawing.Size(104, 24);
            this._lbl_commission.Style = MetroFramework.MetroColorStyle.Red;
            this._lbl_commission.TabIndex = 26;
            this._lbl_commission.Text = "Commission";
            this._lbl_commission.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lbl_commission.UseStyleColors = true;
            // 
            // lbl_advance
            // 
            this.lbl_advance.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_advance.Location = new System.Drawing.Point(9, 29);
            this.lbl_advance.Name = "lbl_advance";
            this.lbl_advance.Size = new System.Drawing.Size(62, 15);
            this.lbl_advance.TabIndex = 25;
            this.lbl_advance.Text = "0000";
            this.lbl_advance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lbl_advance
            // 
            this._lbl_advance.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this._lbl_advance.Location = new System.Drawing.Point(77, 24);
            this._lbl_advance.Name = "_lbl_advance";
            this._lbl_advance.Size = new System.Drawing.Size(104, 24);
            this._lbl_advance.Style = MetroFramework.MetroColorStyle.Red;
            this._lbl_advance.TabIndex = 24;
            this._lbl_advance.Text = "Advance";
            this._lbl_advance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lbl_advance.UseStyleColors = true;
            // 
            // lbl_munshiana
            // 
            this.lbl_munshiana.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_munshiana.Location = new System.Drawing.Point(9, 102);
            this.lbl_munshiana.Name = "lbl_munshiana";
            this.lbl_munshiana.Size = new System.Drawing.Size(62, 15);
            this.lbl_munshiana.TabIndex = 23;
            this.lbl_munshiana.Text = "0000";
            this.lbl_munshiana.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lbl_munshiana
            // 
            this._lbl_munshiana.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this._lbl_munshiana.Location = new System.Drawing.Point(77, 96);
            this._lbl_munshiana.Name = "_lbl_munshiana";
            this._lbl_munshiana.Size = new System.Drawing.Size(104, 24);
            this._lbl_munshiana.Style = MetroFramework.MetroColorStyle.Red;
            this._lbl_munshiana.TabIndex = 22;
            this._lbl_munshiana.Text = "Munshiana";
            this._lbl_munshiana.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lbl_munshiana.UseStyleColors = true;
            // 
            // lbl_labour
            // 
            this.lbl_labour.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_labour.Location = new System.Drawing.Point(9, 77);
            this.lbl_labour.Name = "lbl_labour";
            this.lbl_labour.Size = new System.Drawing.Size(62, 15);
            this.lbl_labour.TabIndex = 21;
            this.lbl_labour.Text = "0000";
            this.lbl_labour.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lbl_labour
            // 
            this._lbl_labour.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this._lbl_labour.Location = new System.Drawing.Point(77, 72);
            this._lbl_labour.Name = "_lbl_labour";
            this._lbl_labour.Size = new System.Drawing.Size(104, 24);
            this._lbl_labour.Style = MetroFramework.MetroColorStyle.Red;
            this._lbl_labour.TabIndex = 20;
            this._lbl_labour.Text = "Labour";
            this._lbl_labour.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lbl_labour.UseStyleColors = true;
            // 
            // lbl_rent
            // 
            this.lbl_rent.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_rent.Location = new System.Drawing.Point(9, 54);
            this.lbl_rent.Name = "lbl_rent";
            this.lbl_rent.Size = new System.Drawing.Size(62, 15);
            this.lbl_rent.TabIndex = 19;
            this.lbl_rent.Text = "0000";
            this.lbl_rent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lbl_rent
            // 
            this._lbl_rent.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this._lbl_rent.Location = new System.Drawing.Point(77, 48);
            this._lbl_rent.Name = "_lbl_rent";
            this._lbl_rent.Size = new System.Drawing.Size(104, 24);
            this._lbl_rent.Style = MetroFramework.MetroColorStyle.Red;
            this._lbl_rent.TabIndex = 18;
            this._lbl_rent.Text = "Transport Rent";
            this._lbl_rent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lbl_rent.UseStyleColors = true;
            // 
            // lbl_total_quantity
            // 
            this.lbl_total_quantity.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_total_quantity.Location = new System.Drawing.Point(9, 5);
            this.lbl_total_quantity.Name = "lbl_total_quantity";
            this.lbl_total_quantity.Size = new System.Drawing.Size(62, 15);
            this.lbl_total_quantity.TabIndex = 17;
            this.lbl_total_quantity.Text = "0000";
            this.lbl_total_quantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lbl_total_quantity
            // 
            this._lbl_total_quantity.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this._lbl_total_quantity.Location = new System.Drawing.Point(77, 0);
            this._lbl_total_quantity.Name = "_lbl_total_quantity";
            this._lbl_total_quantity.Size = new System.Drawing.Size(104, 24);
            this._lbl_total_quantity.Style = MetroFramework.MetroColorStyle.Red;
            this._lbl_total_quantity.TabIndex = 16;
            this._lbl_total_quantity.Text = "Total Product";
            this._lbl_total_quantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lbl_total_quantity.UseStyleColors = true;
            // 
            // InvoiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this._lbl_total_amount);
            this.Controls.Add(this.lbl_total_amount);
            this.Controls.Add(this.bunifuThinButton22);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.panel_header_top);
            this.Name = "InvoiceControl";
            this.Size = new System.Drawing.Size(381, 262);
            this.panel_header_top.ResumeLayout(false);
            this.panel_header_top.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel lbl_name;
        private Bunifu.Framework.UI.BunifuThinButton2 btn_print;
        private Bunifu.Framework.UI.BunifuThinButton2 bunifuThinButton22;
        private MetroFramework.Controls.MetroLabel lbl_invoice_no;
        private MetroFramework.Controls.MetroTile lbl_total_amount;
        private MetroFramework.Controls.MetroLabel _lbl_total_amount;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroLabel lbl_advance;
        private MetroFramework.Controls.MetroLabel _lbl_advance;
        private MetroFramework.Controls.MetroLabel lbl_munshiana;
        private MetroFramework.Controls.MetroLabel _lbl_munshiana;
        private MetroFramework.Controls.MetroLabel lbl_labour;
        private MetroFramework.Controls.MetroLabel _lbl_labour;
        private MetroFramework.Controls.MetroLabel lbl_rent;
        private MetroFramework.Controls.MetroLabel _lbl_rent;
        private MetroFramework.Controls.MetroLabel lbl_total_quantity;
        private MetroFramework.Controls.MetroLabel _lbl_total_quantity;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Panel panel_header_top;
        private MetroFramework.Controls.MetroLabel lbl_chongi;
        private MetroFramework.Controls.MetroLabel _lbl_chongi;
        private MetroFramework.Controls.MetroLabel lbl_commission;
        private MetroFramework.Controls.MetroLabel _lbl_commission;
        private MetroFramework.Controls.MetroLabel _lbl_khata_id;
        public MetroFramework.Controls.MetroTile lbl_count;
    }
}
