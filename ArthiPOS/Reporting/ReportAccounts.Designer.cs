namespace ArthiPOS.Reporting
{
    partial class ReportAccounts
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_print = new System.Windows.Forms.Button();
            this.date_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_start = new System.Windows.Forms.Label();
            this.date_start = new MetroFramework.Controls.MetroDateTime();
            this.lbl_end = new System.Windows.Forms.Label();
            this.date_last = new MetroFramework.Controls.MetroDateTime();
            this.dg_balancest = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_submit = new System.Windows.Forms.Button();
            this.date_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_balancest)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_print
            // 
            this.btn_print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print.Location = new System.Drawing.Point(1089, 182);
            this.btn_print.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(190, 26);
            this.btn_print.TabIndex = 328;
            this.btn_print.Text = "Print";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // date_panel
            // 
            this.date_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.date_panel.Controls.Add(this.lbl_start);
            this.date_panel.Controls.Add(this.date_start);
            this.date_panel.Controls.Add(this.lbl_end);
            this.date_panel.Controls.Add(this.date_last);
            this.date_panel.Location = new System.Drawing.Point(1074, 64);
            this.date_panel.Name = "date_panel";
            this.date_panel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.date_panel.Size = new System.Drawing.Size(211, 72);
            this.date_panel.TabIndex = 327;
            // 
            // lbl_start
            // 
            this.lbl_start.Location = new System.Drawing.Point(150, 0);
            this.lbl_start.Name = "lbl_start";
            this.lbl_start.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_start.Size = new System.Drawing.Size(58, 32);
            this.lbl_start.TabIndex = 118;
            this.lbl_start.Text = "First Date";
            this.lbl_start.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // date_start
            // 
            this.date_start.CustomFormat = "yyyy-MM-dd";
            this.date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_start.Location = new System.Drawing.Point(6, 3);
            this.date_start.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_start.Name = "date_start";
            this.date_start.Size = new System.Drawing.Size(138, 29);
            this.date_start.TabIndex = 115;
            // 
            // lbl_end
            // 
            this.lbl_end.Location = new System.Drawing.Point(150, 35);
            this.lbl_end.Name = "lbl_end";
            this.lbl_end.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_end.Size = new System.Drawing.Size(58, 32);
            this.lbl_end.TabIndex = 117;
            this.lbl_end.Text = "Last Date";
            this.lbl_end.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // date_last
            // 
            this.date_last.CustomFormat = "yyyy-MM-dd";
            this.date_last.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_last.Location = new System.Drawing.Point(6, 38);
            this.date_last.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_last.Name = "date_last";
            this.date_last.Size = new System.Drawing.Size(138, 29);
            this.date_last.TabIndex = 116;
            // 
            // dg_balancest
            // 
            this.dg_balancest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_balancest.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dg_balancest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("UrduLink", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_balancest.DefaultCellStyle = dataGridViewCellStyle8;
            this.dg_balancest.Location = new System.Drawing.Point(9, 7);
            this.dg_balancest.Name = "dg_balancest";
            this.dg_balancest.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_balancest.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dg_balancest.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dg_balancest.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("UrduLink", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_balancest.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dg_balancest.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_balancest.RowTemplate.Height = 35;
            this.dg_balancest.Size = new System.Drawing.Size(1059, 679);
            this.dg_balancest.TabIndex = 325;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1104, 21);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(155, 26);
            this.label1.TabIndex = 329;
            this.label1.Text = "Accounts";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_submit
            // 
            this.btn_submit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_submit.Location = new System.Drawing.Point(1089, 146);
            this.btn_submit.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(190, 26);
            this.btn_submit.TabIndex = 326;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // ReportAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1294, 692);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.date_panel);
            this.Controls.Add(this.dg_balancest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_submit);
            this.Name = "ReportAccounts";
            this.Text = "ReportAccounts";
            this.date_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_balancest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.FlowLayoutPanel date_panel;
        private System.Windows.Forms.Label lbl_start;
        private MetroFramework.Controls.MetroDateTime date_start;
        private System.Windows.Forms.Label lbl_end;
        private MetroFramework.Controls.MetroDateTime date_last;
        private System.Windows.Forms.DataGridView dg_balancest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_submit;
    }
}