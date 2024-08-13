namespace ArthiPOS.Reporting
{
    partial class ReportBalanceSheet
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dg_balancest = new System.Windows.Forms.DataGridView();
            this.btn_print = new System.Windows.Forms.Button();
            this.date_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_start = new System.Windows.Forms.Label();
            this.date_start = new MetroFramework.Controls.MetroDateTime();
            this.lbl_end = new System.Windows.Forms.Label();
            this.date_last = new MetroFramework.Controls.MetroDateTime();
            this.btn_checkbs = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_correctbs = new System.Windows.Forms.CheckBox();
            this.btn_correctbs = new System.Windows.Forms.Button();
            this.lbl_ccash = new System.Windows.Forms.Label();
            this.lbl_corcash = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._lbl_corcash = new System.Windows.Forms.Label();
            this.btn_cashcorrection = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg_balancest)).BeginInit();
            this.date_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg_balancest
            // 
            this.dg_balancest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_balancest.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_balancest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("UrduLink", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_balancest.DefaultCellStyle = dataGridViewCellStyle2;
            this.dg_balancest.Location = new System.Drawing.Point(12, 7);
            this.dg_balancest.Name = "dg_balancest";
            this.dg_balancest.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_balancest.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dg_balancest.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dg_balancest.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("UrduLink", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_balancest.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dg_balancest.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_balancest.RowTemplate.Height = 35;
            this.dg_balancest.Size = new System.Drawing.Size(1059, 679);
            this.dg_balancest.TabIndex = 314;
            // 
            // btn_print
            // 
            this.btn_print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print.Location = new System.Drawing.Point(1092, 200);
            this.btn_print.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(190, 26);
            this.btn_print.TabIndex = 317;
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
            this.date_panel.Location = new System.Drawing.Point(1077, 85);
            this.date_panel.Name = "date_panel";
            this.date_panel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.date_panel.Size = new System.Drawing.Size(211, 72);
            this.date_panel.TabIndex = 316;
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
            // btn_checkbs
            // 
            this.btn_checkbs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_checkbs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_checkbs.Location = new System.Drawing.Point(1092, 164);
            this.btn_checkbs.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_checkbs.Name = "btn_checkbs";
            this.btn_checkbs.Size = new System.Drawing.Size(190, 26);
            this.btn_checkbs.TabIndex = 315;
            this.btn_checkbs.Text = "Check BS";
            this.btn_checkbs.UseVisualStyleBackColor = true;
            this.btn_checkbs.Click += new System.EventHandler(this.btn_checkbs_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1107, 12);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(155, 15);
            this.label1.TabIndex = 318;
            this.label1.Text = "Balance Sheet";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chk_correctbs
            // 
            this.chk_correctbs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_correctbs.AutoSize = true;
            this.chk_correctbs.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_correctbs.Location = new System.Drawing.Point(1092, 326);
            this.chk_correctbs.Name = "chk_correctbs";
            this.chk_correctbs.Size = new System.Drawing.Size(134, 22);
            this.chk_correctbs.TabIndex = 319;
            this.chk_correctbs.Text = "Correct Balance";
            this.chk_correctbs.UseVisualStyleBackColor = true;
            // 
            // btn_correctbs
            // 
            this.btn_correctbs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_correctbs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_correctbs.Location = new System.Drawing.Point(1092, 357);
            this.btn_correctbs.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_correctbs.Name = "btn_correctbs";
            this.btn_correctbs.Size = new System.Drawing.Size(190, 26);
            this.btn_correctbs.TabIndex = 320;
            this.btn_correctbs.Text = "Correct";
            this.btn_correctbs.UseVisualStyleBackColor = true;
            this.btn_correctbs.Click += new System.EventHandler(this.btn_correctbs_Click);
            // 
            // lbl_ccash
            // 
            this.lbl_ccash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ccash.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ccash.ForeColor = System.Drawing.Color.Red;
            this.lbl_ccash.Location = new System.Drawing.Point(1099, 260);
            this.lbl_ccash.Name = "lbl_ccash";
            this.lbl_ccash.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_ccash.Size = new System.Drawing.Size(155, 15);
            this.lbl_ccash.TabIndex = 321;
            this.lbl_ccash.Text = "Balance Sheet";
            this.lbl_ccash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_corcash
            // 
            this.lbl_corcash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_corcash.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_corcash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_corcash.Location = new System.Drawing.Point(1100, 299);
            this.lbl_corcash.Name = "lbl_corcash";
            this.lbl_corcash.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_corcash.Size = new System.Drawing.Size(155, 15);
            this.lbl_corcash.TabIndex = 322;
            this.lbl_corcash.Text = "Balance Sheet";
            this.lbl_corcash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1130, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 15);
            this.label4.TabIndex = 323;
            this.label4.Text = "Current Cash";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lbl_corcash
            // 
            this._lbl_corcash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lbl_corcash.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lbl_corcash.Location = new System.Drawing.Point(1130, 282);
            this._lbl_corcash.Name = "_lbl_corcash";
            this._lbl_corcash.Size = new System.Drawing.Size(155, 15);
            this._lbl_corcash.TabIndex = 324;
            this._lbl_corcash.Text = "Corrected Cash";
            this._lbl_corcash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_cashcorrection
            // 
            this.btn_cashcorrection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cashcorrection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_cashcorrection.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cashcorrection.Location = new System.Drawing.Point(1083, 42);
            this.btn_cashcorrection.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btn_cashcorrection.Name = "btn_cashcorrection";
            this.btn_cashcorrection.Size = new System.Drawing.Size(190, 37);
            this.btn_cashcorrection.TabIndex = 325;
            this.btn_cashcorrection.Text = "Balance Correction";
            this.btn_cashcorrection.UseVisualStyleBackColor = false;
            // 
            // ReportBalanceSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1294, 692);
            this.Controls.Add(this.btn_cashcorrection);
            this.Controls.Add(this._lbl_corcash);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_corcash);
            this.Controls.Add(this.lbl_ccash);
            this.Controls.Add(this.btn_correctbs);
            this.Controls.Add(this.chk_correctbs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.date_panel);
            this.Controls.Add(this.btn_checkbs);
            this.Controls.Add(this.dg_balancest);
            this.Name = "ReportBalanceSheet";
            this.Text = "ReportBalanceSheet";
            ((System.ComponentModel.ISupportInitialize)(this.dg_balancest)).EndInit();
            this.date_panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_balancest;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.FlowLayoutPanel date_panel;
        private System.Windows.Forms.Label lbl_start;
        private MetroFramework.Controls.MetroDateTime date_start;
        private System.Windows.Forms.Label lbl_end;
        private MetroFramework.Controls.MetroDateTime date_last;
        private System.Windows.Forms.Button btn_checkbs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_correctbs;
        private System.Windows.Forms.Button btn_correctbs;
        private System.Windows.Forms.Label lbl_ccash;
        private System.Windows.Forms.Label lbl_corcash;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label _lbl_corcash;
        private System.Windows.Forms.Button btn_cashcorrection;
    }
}