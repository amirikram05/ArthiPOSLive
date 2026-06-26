namespace ArthiPOS.Controls
{
    partial class FrmUrduDocumentEditor
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUrduDocumentEditor));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.cmbFont = new System.Windows.Forms.ComboBox();
            this.cmbFontSize = new System.Windows.Forms.ComboBox();
            this.cmbLineSpacing = new System.Windows.Forms.ComboBox();
            this.btnBold = new System.Windows.Forms.Button();
            this.btnItalic = new System.Windows.Forms.Button();
            this.btnUnderline = new System.Windows.Forms.Button();
            this.btnAlignRight = new System.Windows.Forms.Button();
            this.btnAlignCenter = new System.Windows.Forms.Button();
            this.btnA = new System.Windows.Forms.Button();
            this.btnB2 = new System.Windows.Forms.Button();
            this.btnC = new System.Windows.Forms.Button();
            this.btnD = new System.Windows.Forms.Button();
            this.rtbEditor = new ArthiPOS.Controls.UrduRichTextBox();
            this.txtWatermark = new System.Windows.Forms.TextBox();
            this.btnApplyWatermark = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.panelToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.BackColor = System.Drawing.Color.Gainsboro;
            this.panelToolbar.Controls.Add(this.cmbFont);
            this.panelToolbar.Controls.Add(this.cmbFontSize);
            this.panelToolbar.Controls.Add(this.cmbLineSpacing);
            this.panelToolbar.Controls.Add(this.btnBold);
            this.panelToolbar.Controls.Add(this.btnItalic);
            this.panelToolbar.Controls.Add(this.btnUnderline);
            this.panelToolbar.Controls.Add(this.btnAlignRight);
            this.panelToolbar.Controls.Add(this.btnAlignCenter);
            this.panelToolbar.Controls.Add(this.btnA);
            this.panelToolbar.Controls.Add(this.btnB2);
            this.panelToolbar.Controls.Add(this.btnC);
            this.panelToolbar.Controls.Add(this.btnD);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Location = new System.Drawing.Point(0, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(710, 41);
            this.panelToolbar.TabIndex = 1;
            // 
            // cmbFont
            // 
            this.cmbFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFont.Location = new System.Drawing.Point(10, 8);
            this.cmbFont.Name = "cmbFont";
            this.cmbFont.Size = new System.Drawing.Size(180, 21);
            this.cmbFont.TabIndex = 0;
            this.cmbFont.SelectedIndexChanged += new System.EventHandler(this.cmbFont_SelectedIndexChanged);
            // 
            // cmbFontSize
            // 
            this.cmbFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFontSize.Location = new System.Drawing.Point(200, 8);
            this.cmbFontSize.Name = "cmbFontSize";
            this.cmbFontSize.Size = new System.Drawing.Size(60, 21);
            this.cmbFontSize.TabIndex = 1;
            this.cmbFontSize.SelectedIndexChanged += new System.EventHandler(this.cmbFontSize_SelectedIndexChanged);
            // 
            // cmbLineSpacing
            // 
            this.cmbLineSpacing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineSpacing.Location = new System.Drawing.Point(270, 8);
            this.cmbLineSpacing.Name = "cmbLineSpacing";
            this.cmbLineSpacing.Size = new System.Drawing.Size(80, 21);
            this.cmbLineSpacing.TabIndex = 2;
            this.cmbLineSpacing.SelectedIndexChanged += new System.EventHandler(this.cmbLineSpacing_SelectedIndexChanged);
            // 
            // btnBold
            // 
            this.btnBold.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBold.Location = new System.Drawing.Point(360, 6);
            this.btnBold.Name = "btnBold";
            this.btnBold.Size = new System.Drawing.Size(30, 26);
            this.btnBold.TabIndex = 3;
            this.btnBold.Text = "B";
            this.btnBold.Click += new System.EventHandler(this.btnBold_Click);
            // 
            // btnItalic
            // 
            this.btnItalic.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.btnItalic.Location = new System.Drawing.Point(395, 6);
            this.btnItalic.Name = "btnItalic";
            this.btnItalic.Size = new System.Drawing.Size(30, 26);
            this.btnItalic.TabIndex = 4;
            this.btnItalic.Text = "I";
            this.btnItalic.Click += new System.EventHandler(this.btnItalic_Click);
            // 
            // btnUnderline
            // 
            this.btnUnderline.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.btnUnderline.Location = new System.Drawing.Point(430, 6);
            this.btnUnderline.Name = "btnUnderline";
            this.btnUnderline.Size = new System.Drawing.Size(30, 26);
            this.btnUnderline.TabIndex = 5;
            this.btnUnderline.Text = "U";
            this.btnUnderline.Click += new System.EventHandler(this.btnUnderline_Click);
            // 
            // btnAlignRight
            // 
            this.btnAlignRight.Location = new System.Drawing.Point(470, 6);
            this.btnAlignRight.Name = "btnAlignRight";
            this.btnAlignRight.Size = new System.Drawing.Size(40, 26);
            this.btnAlignRight.TabIndex = 6;
            this.btnAlignRight.Text = "RTL";
            this.btnAlignRight.Click += new System.EventHandler(this.btnAlignRight_Click);
            // 
            // btnAlignCenter
            // 
            this.btnAlignCenter.Location = new System.Drawing.Point(515, 6);
            this.btnAlignCenter.Name = "btnAlignCenter";
            this.btnAlignCenter.Size = new System.Drawing.Size(40, 26);
            this.btnAlignCenter.TabIndex = 7;
            this.btnAlignCenter.Text = "C";
            this.btnAlignCenter.Click += new System.EventHandler(this.btnAlignCenter_Click);
            // 
            // btnA
            // 
            this.btnA.Location = new System.Drawing.Point(560, 6);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(30, 26);
            this.btnA.TabIndex = 8;
            this.btnA.Text = "A";
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // btnB2
            // 
            this.btnB2.Location = new System.Drawing.Point(595, 6);
            this.btnB2.Name = "btnB2";
            this.btnB2.Size = new System.Drawing.Size(30, 26);
            this.btnB2.TabIndex = 9;
            this.btnB2.Text = "B";
            this.btnB2.Click += new System.EventHandler(this.btnB2_Click);
            // 
            // btnC
            // 
            this.btnC.Location = new System.Drawing.Point(630, 6);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(30, 26);
            this.btnC.TabIndex = 10;
            this.btnC.Text = "C";
            this.btnC.Click += new System.EventHandler(this.btnC_Click);
            // 
            // btnD
            // 
            this.btnD.Location = new System.Drawing.Point(665, 6);
            this.btnD.Name = "btnD";
            this.btnD.Size = new System.Drawing.Size(30, 26);
            this.btnD.TabIndex = 11;
            this.btnD.Text = "D";
            this.btnD.Click += new System.EventHandler(this.btnD_Click);
            // 
            // rtbEditor
            // 
            this.rtbEditor.AcceptsTab = true;
            this.rtbEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbEditor.Font = new System.Drawing.Font("Jameel Noori Nastaleeq", 14F);
            this.rtbEditor.LangEnglish = false;
            this.rtbEditor.Location = new System.Drawing.Point(0, 49);
            this.rtbEditor.Margin = new System.Windows.Forms.Padding(60, 40, 60, 40);
            this.rtbEditor.Name = "rtbEditor";
            this.rtbEditor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rtbEditor.Size = new System.Drawing.Size(710, 441);
            this.rtbEditor.TabIndex = 0;
            this.rtbEditor.Text = "";
            this.rtbEditor.WatermarkColor = System.Drawing.Color.Gray;
            this.rtbEditor.WatermarkText = "Urdu text here...";
            // 
            // txtWatermark
            // 
            this.txtWatermark.Location = new System.Drawing.Point(10, 500);
            this.txtWatermark.Name = "txtWatermark";
            this.txtWatermark.Size = new System.Drawing.Size(300, 20);
            this.txtWatermark.TabIndex = 2;
            // 
            // btnApplyWatermark
            // 
            this.btnApplyWatermark.Location = new System.Drawing.Point(320, 498);
            this.btnApplyWatermark.Name = "btnApplyWatermark";
            this.btnApplyWatermark.Size = new System.Drawing.Size(120, 26);
            this.btnApplyWatermark.TabIndex = 3;
            this.btnApplyWatermark.Text = "Apply Watermark";
            this.btnApplyWatermark.Click += new System.EventHandler(this.btnApplyWatermark_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(460, 498);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 26);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(550, 498);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 26);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // FrmUrduDocumentEditor
            // 
            this.ClientSize = new System.Drawing.Size(710, 530);
            this.Controls.Add(this.rtbEditor);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.txtWatermark);
            this.Controls.Add(this.btnApplyWatermark);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Name = "FrmUrduDocumentEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Urdu Contract / Letter Editor";
            this.Load += new System.EventHandler(this.FrmUrduDocumentEditor_Load);
            this.panelToolbar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.ComboBox cmbFont;
        private System.Windows.Forms.ComboBox cmbFontSize;
        private System.Windows.Forms.ComboBox cmbLineSpacing;
        private System.Windows.Forms.Button btnBold;
        private System.Windows.Forms.Button btnItalic;
        private System.Windows.Forms.Button btnUnderline;
        private System.Windows.Forms.Button btnAlignRight;
        private System.Windows.Forms.Button btnAlignCenter;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.Button btnB2;
        private System.Windows.Forms.Button btnC;
        private System.Windows.Forms.Button btnD;
        private UrduRichTextBox rtbEditor;
        private System.Windows.Forms.TextBox txtWatermark;
        private System.Windows.Forms.Button btnApplyWatermark;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
    }
}
