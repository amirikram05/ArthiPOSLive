namespace ArthiPOS.Controls.dashboard
{
    partial class ProfileCCAdd
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_oldaugrai = new System.Windows.Forms.Label();
            this.lbl_augrai = new System.Windows.Forms.Label();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.txt_address = new ArthiPOS.Controls.UrduTextBox();
            this.lbl_type = new System.Windows.Forms.Label();
            this.lbl_id = new System.Windows.Forms.Label();
            this.today_date = new MetroFramework.Controls.MetroDateTime();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_quick_amount = new ArthiPOS.Controls.UrduTextBox();
            this.btn_cc_add = new Bunifu.Framework.UI.BunifuImageButton();
            this.lbl_add = new System.Windows.Forms.Label();
            this.cc_txt_name = new ArthiPOS.Controls.UrduTextBox();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_cc_add)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.lbl_oldaugrai);
            this.panel4.Controls.Add(this.lbl_augrai);
            this.panel4.Controls.Add(this.cb_type);
            this.panel4.Controls.Add(this.txt_address);
            this.panel4.Controls.Add(this.lbl_type);
            this.panel4.Controls.Add(this.lbl_id);
            this.panel4.Controls.Add(this.today_date);
            this.panel4.Controls.Add(this.lbl_msg);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.txt_quick_amount);
            this.panel4.Controls.Add(this.btn_cc_add);
            this.panel4.Controls.Add(this.lbl_add);
            this.panel4.Controls.Add(this.cc_txt_name);
            this.panel4.Location = new System.Drawing.Point(12, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(278, 354);
            this.panel4.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Jameel Noori Nastaleeq", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(195, 189);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(83, 23);
            this.label4.TabIndex = 258;
            this.label4.Text = "تازہ ‌‌اگراھی";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Jameel Noori Nastaleeq", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(183, 137);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(83, 23);
            this.label2.TabIndex = 257;
            this.label2.Text = "پرانی ‌اگراھی";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_oldaugrai
            // 
            this.lbl_oldaugrai.BackColor = System.Drawing.Color.White;
            this.lbl_oldaugrai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_oldaugrai.ForeColor = System.Drawing.Color.Green;
            this.lbl_oldaugrai.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_oldaugrai.Location = new System.Drawing.Point(16, 133);
            this.lbl_oldaugrai.Name = "lbl_oldaugrai";
            this.lbl_oldaugrai.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_oldaugrai.Size = new System.Drawing.Size(150, 31);
            this.lbl_oldaugrai.TabIndex = 256;
            this.lbl_oldaugrai.Text = "0";
            this.lbl_oldaugrai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_augrai
            // 
            this.lbl_augrai.BackColor = System.Drawing.Color.White;
            this.lbl_augrai.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lbl_augrai.ForeColor = System.Drawing.Color.Green;
            this.lbl_augrai.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_augrai.Location = new System.Drawing.Point(12, 176);
            this.lbl_augrai.Name = "lbl_augrai";
            this.lbl_augrai.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_augrai.Size = new System.Drawing.Size(185, 46);
            this.lbl_augrai.TabIndex = 255;
            this.lbl_augrai.Text = "0";
            this.lbl_augrai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_type
            // 
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Items.AddRange(new object[] {
            "Customer",
            "Bipari/Zamidar"});
            this.cb_type.Location = new System.Drawing.Point(11, 52);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(165, 21);
            this.cb_type.TabIndex = 254;
            this.cb_type.SelectedIndexChanged += new System.EventHandler(this.cb_type_SelectedIndexChanged);
            // 
            // txt_address
            // 
            this.txt_address.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_address.IsNumeric = false;
            this.txt_address.LangEnglish = false;
            this.txt_address.Location = new System.Drawing.Point(3, 104);
            this.txt_address.Name = "txt_address";
            this.txt_address.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_address.Size = new System.Drawing.Size(100, 26);
            this.txt_address.TabIndex = 253;
            this.txt_address.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_address.WaterMarkText = "Address";
            // 
            // lbl_type
            // 
            this.lbl_type.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_type.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_type.Location = new System.Drawing.Point(11, 84);
            this.lbl_type.Name = "lbl_type";
            this.lbl_type.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_type.Size = new System.Drawing.Size(106, 17);
            this.lbl_type.TabIndex = 252;
            this.lbl_type.Text = "Type";
            // 
            // lbl_id
            // 
            this.lbl_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_id.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_id.Location = new System.Drawing.Point(195, 78);
            this.lbl_id.Name = "lbl_id";
            this.lbl_id.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_id.Size = new System.Drawing.Size(79, 23);
            this.lbl_id.TabIndex = 251;
            this.lbl_id.Text = "0";
            // 
            // today_date
            // 
            this.today_date.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.today_date.CustomFormat = "yyyy-MM-dd";
            this.today_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.today_date.Location = new System.Drawing.Point(11, 16);
            this.today_date.MinimumSize = new System.Drawing.Size(0, 29);
            this.today_date.Name = "today_date";
            this.today_date.Size = new System.Drawing.Size(106, 29);
            this.today_date.TabIndex = 250;
            this.today_date.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // lbl_msg
            // 
            this.lbl_msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_msg.ForeColor = System.Drawing.Color.Red;
            this.lbl_msg.Location = new System.Drawing.Point(8, 270);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(263, 77);
            this.lbl_msg.TabIndex = 232;
            this.lbl_msg.Text = "Message";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 257);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 231;
            this.label3.Text = "CTRL + ENTER";
            // 
            // txt_quick_amount
            // 
            this.txt_quick_amount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txt_quick_amount.IsNumeric = true;
            this.txt_quick_amount.LangEnglish = true;
            this.txt_quick_amount.Location = new System.Drawing.Point(64, 225);
            this.txt_quick_amount.Name = "txt_quick_amount";
            this.txt_quick_amount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_quick_amount.Size = new System.Drawing.Size(211, 29);
            this.txt_quick_amount.TabIndex = 228;
            this.txt_quick_amount.Text = "0";
            this.txt_quick_amount.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_quick_amount.WaterMarkText = "0";
            // 
            // btn_cc_add
            // 
            this.btn_cc_add.BackColor = System.Drawing.Color.Transparent;
            this.btn_cc_add.Image = global::ArthiPOS.Properties.Resources.add;
            this.btn_cc_add.ImageActive = null;
            this.btn_cc_add.Location = new System.Drawing.Point(30, 225);
            this.btn_cc_add.Name = "btn_cc_add";
            this.btn_cc_add.Size = new System.Drawing.Size(28, 28);
            this.btn_cc_add.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_cc_add.TabIndex = 198;
            this.btn_cc_add.TabStop = false;
            this.btn_cc_add.Zoom = 10;
            this.btn_cc_add.Click += new System.EventHandler(this.btn_cc_add_Click);
            // 
            // lbl_add
            // 
            this.lbl_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_add.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_add.Location = new System.Drawing.Point(158, 24);
            this.lbl_add.Name = "lbl_add";
            this.lbl_add.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_add.Size = new System.Drawing.Size(106, 17);
            this.lbl_add.TabIndex = 35;
            this.lbl_add.Text = "Quick Add";
            // 
            // cc_txt_name
            // 
            this.cc_txt_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cc_txt_name.IsNumeric = false;
            this.cc_txt_name.LangEnglish = false;
            this.cc_txt_name.Location = new System.Drawing.Point(109, 104);
            this.cc_txt_name.Name = "cc_txt_name";
            this.cc_txt_name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cc_txt_name.Size = new System.Drawing.Size(166, 26);
            this.cc_txt_name.TabIndex = 4;
            this.cc_txt_name.WaterMarkColor = System.Drawing.Color.Gray;
            this.cc_txt_name.WaterMarkText = "Name";
            // 
            // ProfileCCAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 368);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ProfileCCAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProfileCCAdd";
            this.Load += new System.EventHandler(this.ProfileCCAdd_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_cc_add)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private UrduTextBox txt_quick_amount;
        private Bunifu.Framework.UI.BunifuImageButton btn_cc_add;
        private System.Windows.Forms.Label lbl_add;
        private UrduTextBox cc_txt_name;
        private System.Windows.Forms.Label lbl_msg;
        private MetroFramework.Controls.MetroDateTime today_date;
        private System.Windows.Forms.Label lbl_id;
        private System.Windows.Forms.Label lbl_type;
        private UrduTextBox txt_address;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.Label lbl_augrai;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_oldaugrai;
    }
}