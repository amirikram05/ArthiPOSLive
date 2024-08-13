namespace ArthiPOS.controls
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txt_username = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.lbltitle = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.txt_password = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.btn_login = new Bunifu.Framework.UI.BunifuThinButton2();
            this.btn_forgotpassword = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.panel_login = new System.Windows.Forms.Panel();
            this.lbl_dbname = new System.Windows.Forms.Label();
            this.lbl_version = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel_login.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_username
            // 
            this.txt_username.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txt_username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_username.HintForeColor = System.Drawing.Color.Empty;
            this.txt_username.HintText = "User Name";
            this.txt_username.isPassword = false;
            this.txt_username.LineFocusedColor = System.Drawing.Color.Blue;
            this.txt_username.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_username.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.txt_username.LineThickness = 3;
            this.txt_username.Location = new System.Drawing.Point(30, 41);
            this.txt_username.Margin = new System.Windows.Forms.Padding(4);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(276, 33);
            this.txt_username.TabIndex = 1;
            this.txt_username.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.ForeColor = System.Drawing.Color.Black;
            this.lbltitle.Location = new System.Drawing.Point(134, 13);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(70, 25);
            this.lbltitle.TabIndex = 21;
            this.lbltitle.Text = "Login";
            // 
            // txt_password
            // 
            this.txt_password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txt_password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_password.HintForeColor = System.Drawing.Color.Empty;
            this.txt_password.HintText = "Password";
            this.txt_password.isPassword = true;
            this.txt_password.LineFocusedColor = System.Drawing.Color.Blue;
            this.txt_password.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_password.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.txt_password.LineThickness = 3;
            this.txt_password.Location = new System.Drawing.Point(31, 93);
            this.txt_password.Margin = new System.Windows.Forms.Padding(4);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(276, 32);
            this.txt_password.TabIndex = 22;
            this.txt_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_password.OnValueChanged += new System.EventHandler(this.password_OnValueChanged);
            // 
            // btn_login
            // 
            this.btn_login.ActiveBorderThickness = 1;
            this.btn_login.ActiveCornerRadius = 20;
            this.btn_login.ActiveFillColor = System.Drawing.Color.SeaGreen;
            this.btn_login.ActiveForecolor = System.Drawing.Color.White;
            this.btn_login.ActiveLineColor = System.Drawing.Color.SeaGreen;
            this.btn_login.BackColor = System.Drawing.Color.White;
            this.btn_login.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_login.BackgroundImage")));
            this.btn_login.ButtonText = "Login";
            this.btn_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login.ForeColor = System.Drawing.Color.SeaGreen;
            this.btn_login.IdleBorderThickness = 1;
            this.btn_login.IdleCornerRadius = 20;
            this.btn_login.IdleFillColor = System.Drawing.Color.White;
            this.btn_login.IdleForecolor = System.Drawing.Color.SeaGreen;
            this.btn_login.IdleLineColor = System.Drawing.Color.SeaGreen;
            this.btn_login.Location = new System.Drawing.Point(63, 157);
            this.btn_login.Margin = new System.Windows.Forms.Padding(5);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(204, 41);
            this.btn_login.TabIndex = 23;
            this.btn_login.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // btn_forgotpassword
            // 
            this.btn_forgotpassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_forgotpassword.Location = new System.Drawing.Point(33, 132);
            this.btn_forgotpassword.Name = "btn_forgotpassword";
            this.btn_forgotpassword.Size = new System.Drawing.Size(81, 13);
            this.btn_forgotpassword.TabIndex = 24;
            this.btn_forgotpassword.Text = "Forgot Password";
            this.btn_forgotpassword.Click += new System.EventHandler(this.btn_forgotpassword_Click);
            // 
            // panel_login
            // 
            this.panel_login.Controls.Add(this.lbltitle);
            this.panel_login.Controls.Add(this.lbl_dbname);
            this.panel_login.Controls.Add(this.lbl_version);
            this.panel_login.Controls.Add(this.lbl_status);
            this.panel_login.Controls.Add(this.txt_username);
            this.panel_login.Controls.Add(this.txt_password);
            this.panel_login.Controls.Add(this.btn_login);
            this.panel_login.Controls.Add(this.btn_forgotpassword);
            this.panel_login.Enabled = false;
            this.panel_login.Location = new System.Drawing.Point(4, 98);
            this.panel_login.Name = "panel_login";
            this.panel_login.Size = new System.Drawing.Size(344, 251);
            this.panel_login.TabIndex = 29;
            // 
            // lbl_dbname
            // 
            this.lbl_dbname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_dbname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbl_dbname.Location = new System.Drawing.Point(189, 129);
            this.lbl_dbname.Name = "lbl_dbname";
            this.lbl_dbname.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_dbname.Size = new System.Drawing.Size(121, 22);
            this.lbl_dbname.TabIndex = 30;
            this.lbl_dbname.Text = "Status";
            // 
            // lbl_version
            // 
            this.lbl_version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_version.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbl_version.Location = new System.Drawing.Point(203, 216);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_version.Size = new System.Drawing.Size(121, 13);
            this.lbl_version.TabIndex = 26;
            this.lbl_version.Text = "Status";
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbl_status.Location = new System.Drawing.Point(27, 216);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(37, 13);
            this.lbl_status.TabIndex = 25;
            this.lbl_status.Text = "Status";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox2.Location = new System.Drawing.Point(54, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(254, 90);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 30;
            this.pictureBox2.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel_login);
            this.Name = "Login";
            this.Size = new System.Drawing.Size(355, 352);
            this.Load += new System.EventHandler(this.Login_Load);
            this.panel_login.ResumeLayout(false);
            this.panel_login.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_username;
        private Bunifu.Framework.UI.BunifuCustomLabel lbltitle;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_password;
        private Bunifu.Framework.UI.BunifuThinButton2 btn_login;
        private DevExpress.XtraEditors.HyperlinkLabelControl btn_forgotpassword;
        private System.Windows.Forms.Panel panel_login;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.Label lbl_dbname;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
