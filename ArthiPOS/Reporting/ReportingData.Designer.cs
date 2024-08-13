namespace ArthiPOS.Reporting
{
    partial class ReportingData
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
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_name = new ArthiPOS.Controls.UrduTextBox();
            this.date_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.date_last = new MetroFramework.Controls.MetroDateTime();
            this.lbl_end = new System.Windows.Forms.Label();
            this.date_start = new MetroFramework.Controls.MetroDateTime();
            this.lbl_start = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lbl_print = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.dg_data = new System.Windows.Forms.DataGridView();
            this.chk_sort = new System.Windows.Forms.CheckBox();
            this.btn_recalSale = new System.Windows.Forms.Button();
            this.flowLayoutPanel2.SuspendLayout();
            this.date_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_data)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.btn_search);
            this.flowLayoutPanel2.Controls.Add(this.txt_name);
            this.flowLayoutPanel2.Controls.Add(this.date_panel);
            this.flowLayoutPanel2.Controls.Add(this.comboBox1);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(143, 5);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(812, 42);
            this.flowLayoutPanel2.TabIndex = 9;
            // 
            // btn_search
            // 
            this.btn_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_search.Location = new System.Drawing.Point(734, 6);
            this.btn_search.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 27);
            this.btn_search.TabIndex = 119;
            this.btn_search.Text = "Submit";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_name
            // 
            this.txt_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.txt_name.IsNumeric = false;
            this.txt_name.LangEnglish = false;
            this.txt_name.Location = new System.Drawing.Point(567, 6);
            this.txt_name.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.txt_name.Name = "txt_name";
            this.txt_name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_name.Size = new System.Drawing.Size(161, 27);
            this.txt_name.TabIndex = 3;
            this.txt_name.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_name.WaterMarkText = "Name";
            // 
            // date_panel
            // 
            this.date_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.date_panel.Controls.Add(this.date_last);
            this.date_panel.Controls.Add(this.lbl_end);
            this.date_panel.Controls.Add(this.date_start);
            this.date_panel.Controls.Add(this.lbl_start);
            this.date_panel.Location = new System.Drawing.Point(198, 3);
            this.date_panel.Name = "date_panel";
            this.date_panel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.date_panel.Size = new System.Drawing.Size(363, 36);
            this.date_panel.TabIndex = 118;
            // 
            // date_last
            // 
            this.date_last.CustomFormat = "yyyy-MM-dd";
            this.date_last.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_last.Location = new System.Drawing.Point(257, 3);
            this.date_last.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_last.Name = "date_last";
            this.date_last.Size = new System.Drawing.Size(103, 29);
            this.date_last.TabIndex = 116;
            // 
            // lbl_end
            // 
            this.lbl_end.Location = new System.Drawing.Point(183, 0);
            this.lbl_end.Name = "lbl_end";
            this.lbl_end.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_end.Size = new System.Drawing.Size(68, 32);
            this.lbl_end.TabIndex = 117;
            this.lbl_end.Text = "Last Date";
            this.lbl_end.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // date_start
            // 
            this.date_start.CustomFormat = "yyyy-MM-dd";
            this.date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_start.Location = new System.Drawing.Point(74, 3);
            this.date_start.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_start.Name = "date_start";
            this.date_start.Size = new System.Drawing.Size(103, 29);
            this.date_start.TabIndex = 115;
            // 
            // lbl_start
            // 
            this.lbl_start.Location = new System.Drawing.Point(10, 0);
            this.lbl_start.Name = "lbl_start";
            this.lbl_start.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_start.Size = new System.Drawing.Size(58, 32);
            this.lbl_start.TabIndex = 118;
            this.lbl_start.Text = "First Date";
            this.lbl_start.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Season Report",
            "Expense Report",
            "Bipari Report",
            "Customer Purchases Report",
            "Augrai Difference",
            "RemaingFreshNotZero",
            "AllAugrai",
            "Receiving Report"});
            this.comboBox1.Location = new System.Drawing.Point(33, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(159, 24);
            this.comboBox1.TabIndex = 163;
            this.comboBox1.Text = "Select";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lbl_print
            // 
            this.lbl_print.BackColor = System.Drawing.Color.White;
            this.lbl_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_print.Location = new System.Drawing.Point(12, 28);
            this.lbl_print.Name = "lbl_print";
            this.lbl_print.Size = new System.Drawing.Size(100, 42);
            this.lbl_print.TabIndex = 120;
            this.lbl_print.TabStop = true;
            this.lbl_print.Text = "Print";
            this.lbl_print.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_print.Click += new System.EventHandler(this.lbl_print_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 12);
            this.label1.TabIndex = 162;
            this.label1.Text = "CTRL + P";
            // 
            // dg_data
            // 
            this.dg_data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_data.Location = new System.Drawing.Point(12, 73);
            this.dg_data.Name = "dg_data";
            this.dg_data.Size = new System.Drawing.Size(993, 426);
            this.dg_data.TabIndex = 10;
            // 
            // chk_sort
            // 
            this.chk_sort.AutoSize = true;
            this.chk_sort.Location = new System.Drawing.Point(145, 50);
            this.chk_sort.Name = "chk_sort";
            this.chk_sort.Size = new System.Drawing.Size(131, 17);
            this.chk_sort.TabIndex = 163;
            this.chk_sort.Text = "ID=False/Augrai=True";
            this.chk_sort.UseVisualStyleBackColor = true;
            this.chk_sort.CheckedChanged += new System.EventHandler(this.chk_sort_CheckedChanged);
            // 
            // btn_recalSale
            // 
            this.btn_recalSale.Location = new System.Drawing.Point(282, 50);
            this.btn_recalSale.Name = "btn_recalSale";
            this.btn_recalSale.Size = new System.Drawing.Size(151, 23);
            this.btn_recalSale.TabIndex = 164;
            this.btn_recalSale.Text = "Recalulate";
            this.btn_recalSale.UseVisualStyleBackColor = true;
            this.btn_recalSale.Click += new System.EventHandler(this.btn_recalSale_Click);
            // 
            // ReportingData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1017, 511);
            this.Controls.Add(this.btn_recalSale);
            this.Controls.Add(this.chk_sort);
            this.Controls.Add(this.dg_data);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_print);
            this.Name = "ReportingData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ReportingData";
            this.Load += new System.EventHandler(this.ReportingData_Load);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.date_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Controls.UrduTextBox txt_name;
        private System.Windows.Forms.FlowLayoutPanel date_panel;
        private MetroFramework.Controls.MetroDateTime date_last;
        private System.Windows.Forms.Label lbl_end;
        private MetroFramework.Controls.MetroDateTime date_start;
        private System.Windows.Forms.Label lbl_start;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.DataGridView dg_data;
        private System.Windows.Forms.LinkLabel lbl_print;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chk_sort;
        private System.Windows.Forms.Button btn_recalSale;
    }
}