namespace ArthiPOS.Controls.test
{
    partial class ControlEXREC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dt_receive = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dt_expense = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbl_total = new System.Windows.Forms.Label();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.lbl_cash = new System.Windows.Forms.Label();
            this.lbl_receive = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_expense = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dt_receive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_expense)).BeginInit();
            this.SuspendLayout();
            // 
            // dt_receive
            // 
            this.dt_receive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dt_receive.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dt_receive.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dt_receive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dt_receive.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Jameel Noori Nastaleeq", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dt_receive.DefaultCellStyle = dataGridViewCellStyle2;
            this.dt_receive.Location = new System.Drawing.Point(609, 22);
            this.dt_receive.Name = "dt_receive";
            this.dt_receive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dt_receive.Size = new System.Drawing.Size(600, 460);
            this.dt_receive.TabIndex = 204;
            this.dt_receive.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dt_receive_CellClick);
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Delete";
            this.Column7.Image = global::ArthiPOS.Properties.Resources.del2;
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dt_expense
            // 
            this.dt_expense.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dt_expense.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dt_expense.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dt_expense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dt_expense.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Jameel Noori Nastaleeq", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dt_expense.DefaultCellStyle = dataGridViewCellStyle4;
            this.dt_expense.Location = new System.Drawing.Point(3, 22);
            this.dt_expense.Name = "dt_expense";
            this.dt_expense.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dt_expense.RowTemplate.Height = 25;
            this.dt_expense.Size = new System.Drawing.Size(600, 460);
            this.dt_expense.TabIndex = 203;
            this.dt_expense.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dt_expense_CellClick);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Delete";
            this.Column3.Image = global::ArthiPOS.Properties.Resources.del3;
            this.Column3.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(815, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 20);
            this.label4.TabIndex = 202;
            this.label4.Text = "Recevings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(218, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 201;
            this.label3.Text = "Expenses";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(222, 560);
            this.label14.Name = "label14";
            this.label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label14.Size = new System.Drawing.Size(100, 23);
            this.label14.TabIndex = 221;
            this.label14.Text = "Total";
            // 
            // lbl_total
            // 
            this.lbl_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total.Location = new System.Drawing.Point(26, 560);
            this.lbl_total.Name = "lbl_total";
            this.lbl_total.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_total.Size = new System.Drawing.Size(155, 18);
            this.lbl_total.TabIndex = 220;
            this.lbl_total.Text = "00000000";
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.DimGray;
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(8, 555);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(200, 2);
            this.materialDivider1.TabIndex = 219;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // lbl_cash
            // 
            this.lbl_cash.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cash.Location = new System.Drawing.Point(26, 485);
            this.lbl_cash.Name = "lbl_cash";
            this.lbl_cash.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_cash.Size = new System.Drawing.Size(155, 18);
            this.lbl_cash.TabIndex = 218;
            this.lbl_cash.Text = "00000000";
            // 
            // lbl_receive
            // 
            this.lbl_receive.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_receive.ForeColor = System.Drawing.Color.LimeGreen;
            this.lbl_receive.Location = new System.Drawing.Point(26, 509);
            this.lbl_receive.Name = "lbl_receive";
            this.lbl_receive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_receive.Size = new System.Drawing.Size(155, 18);
            this.lbl_receive.TabIndex = 217;
            this.lbl_receive.Text = "00000000";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(222, 509);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 216;
            this.label10.Text = "Recevings";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(207, 535);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(115, 23);
            this.label9.TabIndex = 215;
            this.label9.Text = "Expense/Billing";
            // 
            // lbl_expense
            // 
            this.lbl_expense.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_expense.ForeColor = System.Drawing.Color.Red;
            this.lbl_expense.Location = new System.Drawing.Point(26, 534);
            this.lbl_expense.Name = "lbl_expense";
            this.lbl_expense.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_expense.Size = new System.Drawing.Size(155, 18);
            this.lbl_expense.TabIndex = 214;
            this.lbl_expense.Text = "00000000";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(222, 485);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 213;
            this.label7.Text = "Total Cash";
            // 
            // ControlEXREC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lbl_total);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.lbl_cash);
            this.Controls.Add(this.lbl_receive);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lbl_expense);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dt_receive);
            this.Controls.Add(this.dt_expense);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "ControlEXREC";
            this.Size = new System.Drawing.Size(1239, 587);
            ((System.ComponentModel.ISupportInitialize)(this.dt_receive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_expense)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dt_receive;
        private System.Windows.Forms.DataGridViewImageColumn Column7;
        private System.Windows.Forms.DataGridView dt_expense;
        private System.Windows.Forms.DataGridViewImageColumn Column3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbl_total;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private System.Windows.Forms.Label lbl_cash;
        private System.Windows.Forms.Label lbl_receive;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_expense;
        private System.Windows.Forms.Label label7;
    }
}
