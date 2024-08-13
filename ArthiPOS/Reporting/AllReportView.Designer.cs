namespace ArthiPOS.Reporting
{
    partial class AllReportView
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
            this.cr_cashflow = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cr_cashflow
            // 
            this.cr_cashflow.ActiveViewIndex = -1;
            this.cr_cashflow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cr_cashflow.Cursor = System.Windows.Forms.Cursors.Default;
            this.cr_cashflow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cr_cashflow.Location = new System.Drawing.Point(0, 0);
            this.cr_cashflow.Name = "cr_cashflow";
            this.cr_cashflow.Size = new System.Drawing.Size(913, 546);
            this.cr_cashflow.TabIndex = 127;
            this.cr_cashflow.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // AllReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 546);
            this.Controls.Add(this.cr_cashflow);
            this.Name = "AllReportView";
            this.Text = "AllReportView";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer cr_cashflow;
    }
}