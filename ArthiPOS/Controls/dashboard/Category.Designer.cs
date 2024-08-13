namespace ArthiPOS.Controls.dashboard
{
    partial class Category
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
            this.button1 = new System.Windows.Forms.Button();
            this.dg_cate = new System.Windows.Forms.DataGridView();
            this.text_cat = new ArthiPOS.Controls.UrduTextBox();
            this.Column4 = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dg_cate)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(51, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dg_cate
            // 
            this.dg_cate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_cate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4});
            this.dg_cate.Location = new System.Drawing.Point(12, 54);
            this.dg_cate.Name = "dg_cate";
            this.dg_cate.Size = new System.Drawing.Size(603, 338);
            this.dg_cate.TabIndex = 2;
            this.dg_cate.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_cate_CellContentClick);
            // 
            // text_cat
            // 
            this.text_cat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.text_cat.IsNumeric = false;
            this.text_cat.LangEnglish = true;
            this.text_cat.Location = new System.Drawing.Point(166, 12);
            this.text_cat.Name = "text_cat";
            this.text_cat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.text_cat.Size = new System.Drawing.Size(411, 26);
            this.text_cat.TabIndex = 1;
            this.text_cat.WaterMarkColor = System.Drawing.Color.Gray;
            this.text_cat.WaterMarkText = "Add";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Update";
            this.Column4.Image = global::ArthiPOS.Properties.Resources.edit;
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Category
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 419);
            this.Controls.Add(this.dg_cate);
            this.Controls.Add(this.text_cat);
            this.Controls.Add(this.button1);
            this.Name = "Category";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Category";
            ((System.ComponentModel.ISupportInitialize)(this.dg_cate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private UrduTextBox text_cat;
        private System.Windows.Forms.DataGridView dg_cate;
        private System.Windows.Forms.DataGridViewImageColumn Column4;
    }
}