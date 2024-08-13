namespace ArthiPOS.Controls.dashboard
{
    partial class UrduDailog
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
            this.txt_urdu = new ArthiPOS.Controls.UrduTextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_urdu
            // 
            this.txt_urdu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_urdu.IsNumeric = false;
            this.txt_urdu.LangEnglish = false;
            this.txt_urdu.Location = new System.Drawing.Point(32, 25);
            this.txt_urdu.Multiline = true;
            this.txt_urdu.Name = "txt_urdu";
            this.txt_urdu.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_urdu.Size = new System.Drawing.Size(225, 121);
            this.txt_urdu.TabIndex = 0;
            this.txt_urdu.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_urdu.WaterMarkText = "Description";
            this.txt_urdu.TextChanged += new System.EventHandler(this.txt_urdu_TextChanged);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(103, 152);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // UrduDailog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 187);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.txt_urdu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UrduDailog";
            this.Text = "Description";
            this.Load += new System.EventHandler(this.UrduDailog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UrduTextBox txt_urdu;
        private System.Windows.Forms.Button btn_save;
    }
}