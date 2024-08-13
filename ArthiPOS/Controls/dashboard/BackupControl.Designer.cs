namespace ArthiPOS.Controls.dashboard
{
    partial class BackupControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btn_restore = new System.Windows.Forms.Button();
            this.btn_browse_restore = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_location = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_browse_backup = new System.Windows.Forms.Button();
            this.btn_backup = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Restore Database";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Backup Database";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.btn_restore);
            this.panel2.Controls.Add(this.btn_browse_restore);
            this.panel2.Location = new System.Drawing.Point(92, 243);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(440, 117);
            this.panel2.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Location";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(79, 39);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(270, 20);
            this.textBox2.TabIndex = 5;
            // 
            // btn_restore
            // 
            this.btn_restore.Location = new System.Drawing.Point(358, 68);
            this.btn_restore.Name = "btn_restore";
            this.btn_restore.Size = new System.Drawing.Size(75, 23);
            this.btn_restore.TabIndex = 3;
            this.btn_restore.Text = "Restore";
            this.btn_restore.UseVisualStyleBackColor = true;
            // 
            // btn_browse_restore
            // 
            this.btn_browse_restore.Location = new System.Drawing.Point(358, 36);
            this.btn_browse_restore.Name = "btn_browse_restore";
            this.btn_browse_restore.Size = new System.Drawing.Size(75, 23);
            this.btn_browse_restore.TabIndex = 2;
            this.btn_browse_restore.Text = "Browse";
            this.btn_browse_restore.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_location);
            this.panel1.Controls.Add(this.btn_backup);
            this.panel1.Location = new System.Drawing.Point(92, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 117);
            this.panel1.TabIndex = 14;
            // 
            // lbl_location
            // 
            this.lbl_location.AutoSize = true;
            this.lbl_location.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_location.Location = new System.Drawing.Point(3, 39);
            this.lbl_location.Name = "lbl_location";
            this.lbl_location.Size = new System.Drawing.Size(70, 20);
            this.lbl_location.TabIndex = 6;
            this.lbl_location.Text = "Location";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(148, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(270, 20);
            this.textBox1.TabIndex = 5;
            // 
            // btn_browse_backup
            // 
            this.btn_browse_backup.Location = new System.Drawing.Point(424, 48);
            this.btn_browse_backup.Name = "btn_browse_backup";
            this.btn_browse_backup.Size = new System.Drawing.Size(98, 23);
            this.btn_browse_backup.TabIndex = 0;
            this.btn_browse_backup.Text = "Set Backup Path";
            this.btn_browse_backup.UseVisualStyleBackColor = true;
            // 
            // btn_backup
            // 
            this.btn_backup.Location = new System.Drawing.Point(355, 66);
            this.btn_backup.Name = "btn_backup";
            this.btn_backup.Size = new System.Drawing.Size(75, 23);
            this.btn_backup.TabIndex = 1;
            this.btn_backup.Text = "Backup";
            this.btn_backup.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(243, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 22);
            this.label1.TabIndex = 25;
            this.label1.Text = "Backup/Restore";
            // 
            // BackupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 413);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_browse_backup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "BackupControl";
            this.Text = "BackupControl";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btn_restore;
        private System.Windows.Forms.Button btn_browse_restore;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_location;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_browse_backup;
        private System.Windows.Forms.Button btn_backup;
        private System.Windows.Forms.Label label1;
    }
}