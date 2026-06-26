namespace ArthiPOS.Reporting
{
    partial class ReportPages
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_bill_Report = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btn_print_A7 = new System.Windows.Forms.Button();
            this.btn_printA4 = new System.Windows.Forms.Button();
            this.btn_print_a4h = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_cust_rep = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Report Size";
            // 
            // btn_bill_Report
            // 
            this.btn_bill_Report.Font = new System.Drawing.Font("Jameel Noori Nastaleeq", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_bill_Report.Location = new System.Drawing.Point(7, 45);
            this.btn_bill_Report.Name = "btn_bill_Report";
            this.btn_bill_Report.Size = new System.Drawing.Size(142, 37);
            this.btn_bill_Report.TabIndex = 6;
            this.btn_bill_Report.Text = "زمیدار/بیوپاری ‌كچی ‌كاپی";
            this.btn_bill_Report.UseVisualStyleBackColor = true;
            this.btn_bill_Report.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btn_print_A7);
            this.tabPage3.Controls.Add(this.btn_printA4);
            this.tabPage3.Controls.Add(this.btn_print_a4h);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(261, 160);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "BILLS";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btn_print_A7
            // 
            this.btn_print_A7.BackColor = System.Drawing.Color.LightGray;
            this.btn_print_A7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print_A7.Location = new System.Drawing.Point(27, 87);
            this.btn_print_A7.Name = "btn_print_A7";
            this.btn_print_A7.Size = new System.Drawing.Size(207, 39);
            this.btn_print_A7.TabIndex = 7;
            this.btn_print_A7.Text = "A7";
            this.btn_print_A7.UseVisualStyleBackColor = false;
            this.btn_print_A7.Click += new System.EventHandler(this.btn_print_A7_Click);
            // 
            // btn_printA4
            // 
            this.btn_printA4.BackColor = System.Drawing.Color.LightGray;
            this.btn_printA4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_printA4.Location = new System.Drawing.Point(27, 6);
            this.btn_printA4.Name = "btn_printA4";
            this.btn_printA4.Size = new System.Drawing.Size(207, 39);
            this.btn_printA4.TabIndex = 5;
            this.btn_printA4.Text = "Print On Full Page";
            this.btn_printA4.UseVisualStyleBackColor = false;
            this.btn_printA4.Click += new System.EventHandler(this.btn_printA4_Click);
            // 
            // btn_print_a4h
            // 
            this.btn_print_a4h.BackColor = System.Drawing.Color.LightGray;
            this.btn_print_a4h.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print_a4h.Location = new System.Drawing.Point(27, 46);
            this.btn_print_a4h.Name = "btn_print_a4h";
            this.btn_print_a4h.Size = new System.Drawing.Size(207, 39);
            this.btn_print_a4h.TabIndex = 6;
            this.btn_print_a4h.Text = "A4 Half";
            this.btn_print_a4h.UseVisualStyleBackColor = false;
            this.btn_print_a4h.Click += new System.EventHandler(this.btn_print_a4h_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(3, 88);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(269, 186);
            this.tabControl1.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "CTRL+0";
            // 
            // btn_cust_rep
            // 
            this.btn_cust_rep.Font = new System.Drawing.Font("Jameel Noori Nastaleeq", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cust_rep.Location = new System.Drawing.Point(149, 45);
            this.btn_cust_rep.Name = "btn_cust_rep";
            this.btn_cust_rep.Size = new System.Drawing.Size(136, 37);
            this.btn_cust_rep.TabIndex = 12;
            this.btn_cust_rep.Text = " ‌گاہك كچی ‌كاپی";
            this.btn_cust_rep.UseVisualStyleBackColor = true;
            this.btn_cust_rep.Click += new System.EventHandler(this.btn_cust_rep_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(146, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "CTRL+1";
            // 
            // ReportPages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(297, 278);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_cust_rep);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_bill_Report);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ReportPages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ReportPages";
            this.Load += new System.EventHandler(this.ReportPages_Load);
            this.tabPage3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btn_bill_Report;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btn_print_A7;
        private System.Windows.Forms.Button btn_printA4;
        private System.Windows.Forms.Button btn_print_a4h;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button btn_cust_rep;
        private System.Windows.Forms.Label label2;
    }
}