namespace ArthiPOS.Controls.dashboard
{
    partial class MoveKhata
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
            this.panel13 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.date_move = new MetroFramework.Controls.MetroDateTime();
            this.sale_date = new MetroFramework.Controls.MetroDateTime();
            this.dt_movesales = new System.Windows.Forms.DataGridView();
            this.btn_movekahta = new Bunifu.Framework.UI.BunifuFlatButton();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_movesales)).BeginInit();
            this.SuspendLayout();
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.White;
            this.panel13.Controls.Add(this.label2);
            this.panel13.Controls.Add(this.label1);
            this.panel13.Controls.Add(this.date_move);
            this.panel13.Controls.Add(this.sale_date);
            this.panel13.Location = new System.Drawing.Point(354, 41);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(459, 54);
            this.panel13.TabIndex = 98;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 117;
            this.label2.Text = "Sales Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 116;
            this.label1.Text = "Move To";
            // 
            // date_move
            // 
            this.date_move.CustomFormat = "yyyy-MM-dd";
            this.date_move.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_move.Location = new System.Drawing.Point(3, 19);
            this.date_move.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_move.Name = "date_move";
            this.date_move.Size = new System.Drawing.Size(200, 29);
            this.date_move.TabIndex = 115;
            // 
            // sale_date
            // 
            this.sale_date.CustomFormat = "yyyy-MM-dd";
            this.sale_date.Enabled = false;
            this.sale_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sale_date.Location = new System.Drawing.Point(256, 19);
            this.sale_date.MinimumSize = new System.Drawing.Size(0, 29);
            this.sale_date.Name = "sale_date";
            this.sale_date.Size = new System.Drawing.Size(200, 29);
            this.sale_date.TabIndex = 114;
            // 
            // dt_movesales
            // 
            this.dt_movesales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dt_movesales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dt_movesales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dt_movesales.DefaultCellStyle = dataGridViewCellStyle2;
            this.dt_movesales.Location = new System.Drawing.Point(4, 101);
            this.dt_movesales.Name = "dt_movesales";
            this.dt_movesales.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dt_movesales.Size = new System.Drawing.Size(831, 419);
            this.dt_movesales.TabIndex = 99;
            // 
            // btn_movekahta
            // 
            this.btn_movekahta.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_movekahta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_movekahta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_movekahta.BorderRadius = 0;
            this.btn_movekahta.ButtonText = "Move Khata To Other Date";
            this.btn_movekahta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_movekahta.DisabledColor = System.Drawing.Color.Gray;
            this.btn_movekahta.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_movekahta.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_movekahta.Iconimage = null;
            this.btn_movekahta.Iconimage_right = null;
            this.btn_movekahta.Iconimage_right_Selected = null;
            this.btn_movekahta.Iconimage_Selected = null;
            this.btn_movekahta.IconMarginLeft = 0;
            this.btn_movekahta.IconMarginRight = 0;
            this.btn_movekahta.IconRightVisible = true;
            this.btn_movekahta.IconRightZoom = 0D;
            this.btn_movekahta.IconVisible = true;
            this.btn_movekahta.IconZoom = 90D;
            this.btn_movekahta.IsTab = false;
            this.btn_movekahta.Location = new System.Drawing.Point(185, 35);
            this.btn_movekahta.Name = "btn_movekahta";
            this.btn_movekahta.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_movekahta.OnHovercolor = System.Drawing.Color.Gray;
            this.btn_movekahta.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_movekahta.selected = false;
            this.btn_movekahta.Size = new System.Drawing.Size(163, 60);
            this.btn_movekahta.TabIndex = 218;
            this.btn_movekahta.Text = "Move Khata To Other Date";
            this.btn_movekahta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_movekahta.Textcolor = System.Drawing.Color.White;
            this.btn_movekahta.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_movekahta.Click += new System.EventHandler(this.btn_movekahta_Click);
            // 
            // MoveKhata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 532);
            this.Controls.Add(this.btn_movekahta);
            this.Controls.Add(this.dt_movesales);
            this.Controls.Add(this.panel13);
            this.Name = "MoveKhata";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MoveKhata";
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_movesales)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel13;
        private MetroFramework.Controls.MetroDateTime sale_date;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dt_movesales;
        private Bunifu.Framework.UI.BunifuFlatButton btn_movekahta;
        private MetroFramework.Controls.MetroDateTime date_move;
    }
}