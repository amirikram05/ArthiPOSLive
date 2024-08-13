namespace ArthiPOS.Controls.dashboard
{
    partial class VendorForm
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
            this.panel_vendor = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel_vendor
            // 
            this.panel_vendor.BackColor = System.Drawing.Color.White;
            this.panel_vendor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_vendor.Location = new System.Drawing.Point(0, 0);
            this.panel_vendor.Name = "panel_vendor";
            this.panel_vendor.Size = new System.Drawing.Size(1187, 661);
            this.panel_vendor.TabIndex = 0;
            // 
            // VendorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1187, 661);
            this.Controls.Add(this.panel_vendor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "VendorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stock Mangement";
            this.Load += new System.EventHandler(this.VendorForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_vendor;
    }
}