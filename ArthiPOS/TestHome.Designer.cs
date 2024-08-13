namespace ArthiPOS
{
    partial class TestHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestHome));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.combo_lang = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.btnBackward = new System.Windows.Forms.ToolStripButton();
            this.btn_page1 = new System.Windows.Forms.ToolStripButton();
            this.btn_page2 = new System.Windows.Forms.ToolStripButton();
            this.btn_page3 = new System.Windows.Forms.ToolStripButton();
            this.btn_page4 = new System.Windows.Forms.ToolStripButton();
            this.btn_page5 = new System.Windows.Forms.ToolStripButton();
            this.btnForward = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.pnlPager = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // combo_lang
            // 
            this.combo_lang.FormattingEnabled = true;
            this.combo_lang.Items.AddRange(new object[] {
            resources.GetString("combo_lang.Items"),
            resources.GetString("combo_lang.Items1")});
            resources.ApplyResources(this.combo_lang, "combo_lang");
            this.combo_lang.Name = "combo_lang";
            this.combo_lang.SelectedIndexChanged += new System.EventHandler(this.Combo_lang_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFirst,
            this.btnBackward,
            this.btn_page1,
            this.btn_page2,
            this.btn_page3,
            this.btn_page4,
            this.btn_page5,
            this.btnForward,
            this.btnLast});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // btnFirst
            // 
            this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFirst, "btnFirst");
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnBackward, "btnBackward");
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btn_page1
            // 
            this.btn_page1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btn_page1, "btn_page1");
            this.btn_page1.Name = "btn_page1";
            this.btn_page1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btn_page2
            // 
            this.btn_page2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btn_page2, "btn_page2");
            this.btn_page2.Name = "btn_page2";
            this.btn_page2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // btn_page3
            // 
            this.btn_page3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btn_page3, "btn_page3");
            this.btn_page3.Name = "btn_page3";
            this.btn_page3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // btn_page4
            // 
            this.btn_page4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btn_page4, "btn_page4");
            this.btn_page4.Name = "btn_page4";
            this.btn_page4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // btn_page5
            // 
            this.btn_page5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btn_page5, "btn_page5");
            this.btn_page5.Name = "btn_page5";
            this.btn_page5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // btnForward
            // 
            this.btnForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnForward, "btnForward");
            this.btnForward.Name = "btnForward";
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnLast
            // 
            this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnLast, "btnLast");
            this.btnLast.Name = "btnLast";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // pnlPager
            // 
            resources.ApplyResources(this.pnlPager, "pnlPager");
            this.pnlPager.Name = "pnlPager";
            // 
            // TestHome
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlPager);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.combo_lang);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TestHome";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combo_lang;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnBackward;
        private System.Windows.Forms.ToolStripButton btn_page1;
        private System.Windows.Forms.ToolStripButton btn_page2;
        private System.Windows.Forms.ToolStripButton btn_page3;
        private System.Windows.Forms.ToolStripButton btn_page4;
        private System.Windows.Forms.ToolStripButton btn_page5;
        private System.Windows.Forms.ToolStripButton btnForward;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.Panel pnlPager;
    }
}