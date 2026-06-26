namespace ArthiPOS.Controls.test
{
    partial class AddSaleExp
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
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_name = new ArthiPOS.Controls.UrduTextBox();
            this.urduTextBox1 = new ArthiPOS.Controls.UrduTextBox();
            this.urduTextBox2 = new ArthiPOS.Controls.UrduTextBox();
            this.SuspendLayout();
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(82, 121);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 301;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            // 
            // txt_name
            // 
            this.txt_name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_name.IsNumeric = false;
            this.txt_name.LangEnglish = false;
            this.txt_name.Location = new System.Drawing.Point(82, 25);
            this.txt_name.Name = "txt_name";
            this.txt_name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_name.Size = new System.Drawing.Size(161, 26);
            this.txt_name.TabIndex = 300;
            this.txt_name.WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_name.WaterMarkText = "";
            // 
            // urduTextBox1
            // 
            this.urduTextBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urduTextBox1.IsNumeric = true;
            this.urduTextBox1.LangEnglish = false;
            this.urduTextBox1.Location = new System.Drawing.Point(82, 57);
            this.urduTextBox1.Name = "urduTextBox1";
            this.urduTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.urduTextBox1.Size = new System.Drawing.Size(161, 26);
            this.urduTextBox1.TabIndex = 302;
            this.urduTextBox1.WaterMarkColor = System.Drawing.Color.Gray;
            this.urduTextBox1.WaterMarkText = "";
            // 
            // urduTextBox2
            // 
            this.urduTextBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urduTextBox2.IsNumeric = true;
            this.urduTextBox2.LangEnglish = false;
            this.urduTextBox2.Location = new System.Drawing.Point(82, 89);
            this.urduTextBox2.Name = "urduTextBox2";
            this.urduTextBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.urduTextBox2.Size = new System.Drawing.Size(161, 26);
            this.urduTextBox2.TabIndex = 303;
            this.urduTextBox2.WaterMarkColor = System.Drawing.Color.Gray;
            this.urduTextBox2.WaterMarkText = "";
            // 
            // AddSaleExp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 372);
            this.Controls.Add(this.urduTextBox2);
            this.Controls.Add(this.urduTextBox1);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.txt_name);
            this.Name = "AddSaleExp";
            this.Text = "AddSaleExp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_search;
        private UrduTextBox txt_name;
        private UrduTextBox urduTextBox1;
        private UrduTextBox urduTextBox2;
    }
}