namespace ArthiPOS.Controls.dashboard
{
    partial class TransactionEntryUpdate
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
            this.dgv_acc_trac = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_tr_ac_engname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_act_del = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_atr_update = new System.Windows.Forms.Button();
            this.btn_atr_add = new System.Windows.Forms.Button();
            this.txt_tr_ac_id = new System.Windows.Forms.TextBox();
            this.lbl_tranc_id = new System.Windows.Forms.Label();
            this.txt_trac_name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_tr_ac_urduname = new ArthiPOS.Controls.UrduTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_trans_del = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_tr_update = new System.Windows.Forms.Button();
            this.btn_tr_add = new System.Windows.Forms.Button();
            this.txt_trid = new System.Windows.Forms.TextBox();
            this.txt_tr_urduname = new ArthiPOS.Controls.UrduTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_tr_engname = new System.Windows.Forms.TextBox();
            this.dgv_trac = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_acc_trac)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_trac)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_acc_trac
            // 
            this.dgv_acc_trac.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_acc_trac.Location = new System.Drawing.Point(12, 2);
            this.dgv_acc_trac.Name = "dgv_acc_trac";
            this.dgv_acc_trac.Size = new System.Drawing.Size(730, 271);
            this.dgv_acc_trac.TabIndex = 0;
            this.dgv_acc_trac.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_acc_trac_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID";
            // 
            // txt_tr_ac_engname
            // 
            this.txt_tr_ac_engname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tr_ac_engname.Location = new System.Drawing.Point(13, 63);
            this.txt_tr_ac_engname.Name = "txt_tr_ac_engname";
            this.txt_tr_ac_engname.Size = new System.Drawing.Size(163, 23);
            this.txt_tr_ac_engname.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Urdu Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "English Name";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(748, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(256, 540);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.btn_act_del);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btn_atr_update);
            this.panel1.Controls.Add(this.btn_atr_add);
            this.panel1.Controls.Add(this.txt_tr_ac_id);
            this.panel1.Controls.Add(this.lbl_tranc_id);
            this.panel1.Controls.Add(this.txt_trac_name);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_tr_ac_urduname);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_tr_ac_engname);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 289);
            this.panel1.TabIndex = 0;
            // 
            // btn_act_del
            // 
            this.btn_act_del.Location = new System.Drawing.Point(132, 177);
            this.btn_act_del.Name = "btn_act_del";
            this.btn_act_del.Size = new System.Drawing.Size(58, 23);
            this.btn_act_del.TabIndex = 15;
            this.btn_act_del.Text = "Delete";
            this.btn_act_del.UseVisualStyleBackColor = true;
            this.btn_act_del.Click += new System.EventHandler(this.btn_act_del_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(31, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Account Transaction";
            // 
            // btn_atr_update
            // 
            this.btn_atr_update.Location = new System.Drawing.Point(73, 177);
            this.btn_atr_update.Name = "btn_atr_update";
            this.btn_atr_update.Size = new System.Drawing.Size(58, 23);
            this.btn_atr_update.TabIndex = 12;
            this.btn_atr_update.Text = "Update";
            this.btn_atr_update.UseVisualStyleBackColor = true;
            this.btn_atr_update.Click += new System.EventHandler(this.btn_atr_update_Click);
            // 
            // btn_atr_add
            // 
            this.btn_atr_add.Location = new System.Drawing.Point(13, 177);
            this.btn_atr_add.Name = "btn_atr_add";
            this.btn_atr_add.Size = new System.Drawing.Size(58, 23);
            this.btn_atr_add.TabIndex = 11;
            this.btn_atr_add.Text = "Add";
            this.btn_atr_add.UseVisualStyleBackColor = true;
            this.btn_atr_add.Click += new System.EventHandler(this.btn_atr_add_Click);
            // 
            // txt_tr_ac_id
            // 
            this.txt_tr_ac_id.Enabled = false;
            this.txt_tr_ac_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tr_ac_id.Location = new System.Drawing.Point(34, 25);
            this.txt_tr_ac_id.Name = "txt_tr_ac_id";
            this.txt_tr_ac_id.Size = new System.Drawing.Size(54, 23);
            this.txt_tr_ac_id.TabIndex = 10;
            // 
            // lbl_tranc_id
            // 
            this.lbl_tranc_id.AutoSize = true;
            this.lbl_tranc_id.Location = new System.Drawing.Point(158, 135);
            this.lbl_tranc_id.Name = "lbl_tranc_id";
            this.lbl_tranc_id.Size = new System.Drawing.Size(18, 13);
            this.lbl_tranc_id.TabIndex = 9;
            this.lbl_tranc_id.Text = "ID";
            // 
            // txt_trac_name
            // 
            this.txt_trac_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_trac_name.Location = new System.Drawing.Point(13, 151);
            this.txt_trac_name.Name = "txt_trac_name";
            this.txt_trac_name.Size = new System.Drawing.Size(163, 23);
            this.txt_trac_name.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Transaction ID";
            // 
            // txt_tr_ac_urduname
            // 
            this.txt_tr_ac_urduname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txt_tr_ac_urduname.IsNumeric = false;
            this.txt_tr_ac_urduname.LangEnglish = false;
            this.txt_tr_ac_urduname.Location = new System.Drawing.Point(13, 102);
            this.txt_tr_ac_urduname.Name = "txt_tr_ac_urduname";
            this.txt_tr_ac_urduname.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_tr_ac_urduname.Size = new System.Drawing.Size(163, 23);
            this.txt_tr_ac_urduname.TabIndex = 3;
            this.txt_tr_ac_urduname.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_tr_ac_urduname.WaterMarkText = "";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.btn_trans_del);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btn_tr_update);
            this.panel2.Controls.Add(this.btn_tr_add);
            this.panel2.Controls.Add(this.txt_trid);
            this.panel2.Controls.Add(this.txt_tr_urduname);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.txt_tr_engname);
            this.panel2.Location = new System.Drawing.Point(3, 298);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(253, 203);
            this.panel2.TabIndex = 13;
            // 
            // btn_trans_del
            // 
            this.btn_trans_del.Location = new System.Drawing.Point(132, 132);
            this.btn_trans_del.Name = "btn_trans_del";
            this.btn_trans_del.Size = new System.Drawing.Size(58, 23);
            this.btn_trans_del.TabIndex = 16;
            this.btn_trans_del.Text = "Delete";
            this.btn_trans_del.UseVisualStyleBackColor = true;
            this.btn_trans_del.Click += new System.EventHandler(this.btn_trans_del_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(61, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Transaction";
            // 
            // btn_tr_update
            // 
            this.btn_tr_update.Location = new System.Drawing.Point(73, 132);
            this.btn_tr_update.Name = "btn_tr_update";
            this.btn_tr_update.Size = new System.Drawing.Size(58, 23);
            this.btn_tr_update.TabIndex = 12;
            this.btn_tr_update.Text = "Update";
            this.btn_tr_update.UseVisualStyleBackColor = true;
            this.btn_tr_update.Click += new System.EventHandler(this.btn_tr_update_Click);
            // 
            // btn_tr_add
            // 
            this.btn_tr_add.Enabled = false;
            this.btn_tr_add.Location = new System.Drawing.Point(13, 132);
            this.btn_tr_add.Name = "btn_tr_add";
            this.btn_tr_add.Size = new System.Drawing.Size(58, 23);
            this.btn_tr_add.TabIndex = 11;
            this.btn_tr_add.Text = "Add";
            this.btn_tr_add.UseVisualStyleBackColor = true;
            this.btn_tr_add.Click += new System.EventHandler(this.btn_tr_add_Click);
            // 
            // txt_trid
            // 
            this.txt_trid.Enabled = false;
            this.txt_trid.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_trid.Location = new System.Drawing.Point(34, 22);
            this.txt_trid.Name = "txt_trid";
            this.txt_trid.Size = new System.Drawing.Size(54, 23);
            this.txt_trid.TabIndex = 10;
            // 
            // txt_tr_urduname
            // 
            this.txt_tr_urduname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txt_tr_urduname.IsNumeric = false;
            this.txt_tr_urduname.LangEnglish = false;
            this.txt_tr_urduname.Location = new System.Drawing.Point(13, 106);
            this.txt_tr_urduname.Name = "txt_tr_urduname";
            this.txt_tr_urduname.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_tr_urduname.Size = new System.Drawing.Size(163, 23);
            this.txt_tr_urduname.TabIndex = 3;
            this.txt_tr_urduname.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_tr_urduname.WaterMarkText = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "English Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "ID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Urdu Name";
            // 
            // txt_tr_engname
            // 
            this.txt_tr_engname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tr_engname.Location = new System.Drawing.Point(13, 65);
            this.txt_tr_engname.Name = "txt_tr_engname";
            this.txt_tr_engname.Size = new System.Drawing.Size(163, 23);
            this.txt_tr_engname.TabIndex = 2;
            // 
            // dgv_trac
            // 
            this.dgv_trac.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_trac.Location = new System.Drawing.Point(12, 279);
            this.dgv_trac.Name = "dgv_trac";
            this.dgv_trac.Size = new System.Drawing.Size(730, 271);
            this.dgv_trac.TabIndex = 8;
            this.dgv_trac.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_trac_CellClick);
            // 
            // TransactionEntryUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 540);
            this.Controls.Add(this.dgv_trac);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.dgv_acc_trac);
            this.Name = "TransactionEntryUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TransactionEntryUpdate";
            this.Load += new System.EventHandler(this.TransactionEntryUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_acc_trac)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_trac)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_acc_trac;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_tr_ac_engname;
        private UrduTextBox txt_tr_ac_urduname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_atr_update;
        private System.Windows.Forms.Button btn_atr_add;
        private System.Windows.Forms.TextBox txt_tr_ac_id;
        private System.Windows.Forms.Label lbl_tranc_id;
        private System.Windows.Forms.TextBox txt_trac_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_tr_update;
        private System.Windows.Forms.Button btn_tr_add;
        private System.Windows.Forms.TextBox txt_trid;
        private UrduTextBox txt_tr_urduname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_tr_engname;
        private System.Windows.Forms.DataGridView dgv_trac;
        private System.Windows.Forms.Button btn_act_del;
        private System.Windows.Forms.Button btn_trans_del;
    }
}