namespace ArthiPOS.Controls
{
    partial class SecurityForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecurityForm));
            this.panel_login = new System.Windows.Forms.Panel();
            this.txt_username = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.txt_password = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.btn_login = new Bunifu.Framework.UI.BunifuThinButton2();
            this.lbltitle = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuMaterialTextbox1 = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.txt_registration_no = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.panel_login.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_login
            // 
            this.panel_login.Controls.Add(this.txt_registration_no);
            this.panel_login.Controls.Add(this.bunifuMaterialTextbox1);
            this.panel_login.Controls.Add(this.txt_username);
            this.panel_login.Controls.Add(this.txt_password);
            this.panel_login.Controls.Add(this.btn_login);
            this.panel_login.Controls.Add(this.lbltitle);
            this.panel_login.Enabled = false;
            this.panel_login.Location = new System.Drawing.Point(56, 12);
            this.panel_login.Name = "panel_login";
            this.panel_login.Size = new System.Drawing.Size(344, 248);
            this.panel_login.TabIndex = 30;
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
            this.txt_username.Location = new System.Drawing.Point(15, 32);
            this.txt_username.Margin = new System.Windows.Forms.Padding(4);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(303, 33);
            this.txt_username.TabIndex = 1;
            this.txt_username.Text = "Admin";
            this.txt_username.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.txt_password.Location = new System.Drawing.Point(16, 73);
            this.txt_password.Margin = new System.Windows.Forms.Padding(4);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(303, 33);
            this.txt_password.TabIndex = 22;
            this.txt_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btn_login
            // 
            this.btn_login.ActiveBorderThickness = 1;
            this.btn_login.ActiveCornerRadius = 20;
            this.btn_login.ActiveFillColor = System.Drawing.Color.SeaGreen;
            this.btn_login.ActiveForecolor = System.Drawing.Color.White;
            this.btn_login.ActiveLineColor = System.Drawing.Color.SeaGreen;
            this.btn_login.BackColor = System.Drawing.SystemColors.Control;
            this.btn_login.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_login.BackgroundImage")));
            this.btn_login.ButtonText = "Account Activation";
            this.btn_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login.ForeColor = System.Drawing.Color.SeaGreen;
            this.btn_login.IdleBorderThickness = 1;
            this.btn_login.IdleCornerRadius = 20;
            this.btn_login.IdleFillColor = System.Drawing.Color.White;
            this.btn_login.IdleForecolor = System.Drawing.Color.SeaGreen;
            this.btn_login.IdleLineColor = System.Drawing.Color.SeaGreen;
            this.btn_login.Location = new System.Drawing.Point(22, 198);
            this.btn_login.Margin = new System.Windows.Forms.Padding(5);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(298, 40);
            this.btn_login.TabIndex = 23;
            this.btn_login.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbltitle.Location = new System.Drawing.Point(131, 1);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(90, 25);
            this.lbltitle.TabIndex = 21;
            this.lbltitle.Text = "Security";
            // 
            // bunifuMaterialTextbox1
            // 
            this.bunifuMaterialTextbox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bunifuMaterialTextbox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.bunifuMaterialTextbox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuMaterialTextbox1.HintForeColor = System.Drawing.Color.Empty;
            this.bunifuMaterialTextbox1.HintText = "User Name";
            this.bunifuMaterialTextbox1.isPassword = false;
            this.bunifuMaterialTextbox1.LineFocusedColor = System.Drawing.Color.Blue;
            this.bunifuMaterialTextbox1.LineIdleColor = System.Drawing.Color.Gray;
            this.bunifuMaterialTextbox1.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.bunifuMaterialTextbox1.LineThickness = 3;
            this.bunifuMaterialTextbox1.Location = new System.Drawing.Point(21, 114);
            this.bunifuMaterialTextbox1.Margin = new System.Windows.Forms.Padding(4);
            this.bunifuMaterialTextbox1.Name = "bunifuMaterialTextbox1";
            this.bunifuMaterialTextbox1.Size = new System.Drawing.Size(297, 33);
            this.bunifuMaterialTextbox1.TabIndex = 24;
            this.bunifuMaterialTextbox1.Text = "Security Key";
            this.bunifuMaterialTextbox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lbl_msg);
            this.panel1.Location = new System.Drawing.Point(56, 266);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 195);
            this.panel1.TabIndex = 31;
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbl_msg.Location = new System.Drawing.Point(12, 9);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(50, 13);
            this.lbl_msg.TabIndex = 32;
            this.lbl_msg.Text = "Message";
            // 
            // txt_registration_no
            // 
            this.txt_registration_no.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_registration_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txt_registration_no.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_registration_no.HintForeColor = System.Drawing.Color.Empty;
            this.txt_registration_no.HintText = "Registration Key";
            this.txt_registration_no.isPassword = false;
            this.txt_registration_no.LineFocusedColor = System.Drawing.Color.Blue;
            this.txt_registration_no.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_registration_no.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.txt_registration_no.LineThickness = 3;
            this.txt_registration_no.Location = new System.Drawing.Point(21, 155);
            this.txt_registration_no.Margin = new System.Windows.Forms.Padding(4);
            this.txt_registration_no.Name = "txt_registration_no";
            this.txt_registration_no.Size = new System.Drawing.Size(297, 34);
            this.txt_registration_no.TabIndex = 27;
            this.txt_registration_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // SecurityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 473);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_login);
            this.Name = "SecurityForm";
            this.Text = "SecurityForm";
            this.panel_login.ResumeLayout(false);
            this.panel_login.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_login;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_username;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_password;
        private Bunifu.Framework.UI.BunifuThinButton2 btn_login;
        private Bunifu.Framework.UI.BunifuCustomLabel lbltitle;
        private Bunifu.Framework.UI.BunifuMaterialTextbox bunifuMaterialTextbox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_msg;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_registration_no;
    }
}