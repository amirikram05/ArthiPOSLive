using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ArthiPOS.Controls
{
    public class UrduRichTextBox : RichTextBox
    {
        private string _watermarkText = "Urdu Text Here";
        private Color _watermarkColor = Color.Gray;
        private bool _watermarkEnabled = true;
        private bool _langEnglish = false;

        private bool handled = false;

        public string WatermarkText
        {
            get { return _watermarkText; }
            set { _watermarkText = value; Invalidate(); }
        }

        public Color WatermarkColor
        {
            get { return _watermarkColor; }
            set { _watermarkColor = value; Invalidate(); }
        }

        public bool LangEnglish
        {
            get { return _langEnglish; }
            set { _langEnglish = value; }
        }

        public UrduRichTextBox()
        {
            this.RightToLeft = RightToLeft.Yes;
            this.Font = new Font("Jameel Noori Nastaleeq", 14F);

            // Events
            this.KeyDown += UrduRichTextBox_KeyDown;
            this.TextChanged += UrduRichTextBox_TextChanged;
        }

        // =========================
        // Insert Urdu character at caret
        // =========================
        private bool InsertAtCaret(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;

            int pos = this.SelectionStart;
            this.SelectionStart = pos;
            this.SelectionLength = 0;

            this.SelectedText = text;

            // Ensure RTL & Nastaliq
            this.SelectionAlignment = HorizontalAlignment.Right;
            this.SelectionFont = new Font("Jameel Noori Nastaleeq", 14F);

            this.SelectionStart = pos + text.Length;
            this.SelectionLength = 0;
            this.ScrollToCaret();

            return true;
        }


        // =========================
        // Keyboard mapping
        // =========================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // If in English mode, just use default behavior
            if (_langEnglish) return base.ProcessCmdKey(ref msg, keyData);

            // Handle only character keys
            string urduChar = MapKeyToUrdu(keyData);

            if (!string.IsNullOrEmpty(urduChar))
            {
                InsertAtCaret(urduChar);
                return true; // handled
            }

            // Allow Enter, Backspace, Delete, Tab, etc. to work normally
            if (keyData == Keys.Enter
                || keyData == Keys.Back || keyData == Keys.Delete || keyData == Keys.Tab)
                return base.ProcessCmdKey(ref msg, keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // =========================
        // Map Keys to Urdu
        // =========================
        private string MapKeyToUrdu(Keys keyData)
        {
            StringBuilder sb = new StringBuilder(this.Text);
            switch (keyData)
            {

                case Keys.D0:
                    return ("\u0660");
                    

                case Keys.D1:
                    return ("\u0661");
                    

                case Keys.D2:
                    return ("\u0662");
                    

                case Keys.D3:
                    return ("\u0663");
                    

                case Keys.D4:
                    return ("\u0664");
                    

                case Keys.D5:
                    return ("\u0665");
                    

                case Keys.D6:
                    return ("\u0666");
                    

                case Keys.D7:
                    return ("\u0667");
                    

                case Keys.D8:
                    return ("\u0668");
                    

                case Keys.D9:
                    return ("\u0669");
                    

                case Keys.Space:
                    return (" \u200c");
                    

                case Keys.A:
                    return (((keyData == Keys.Shift) ? "\u0622" : "\u0627"));
                    

                case Keys.B:
                    return (((keyData == Keys.Shift) ? "\u0613" : "\u0628"));
                    

                case Keys.C:
                    return (((keyData == Keys.Shift) ? "\u062b" : "\u0686"));
                    

                case Keys.D:
                    return (((keyData == Keys.Shift) ? "\u0688" : "\u062f"));
                    

                case Keys.E:
                    return (((keyData == Keys.Shift) ? "\u0610" : "\u0639"));
                    

                case Keys.F:
                    return ("\u0641");
                    

                case Keys.G:
                    return (((keyData == Keys.Shift) ? "\u063a" : "\u06af"));
                    

                case Keys.H:
                    return (((keyData == Keys.Shift) ? "\u062d" : "\u06be"));//0647 also
                    

                case Keys.I:
                    return ("\u06cc");//0649 also
                    

                case Keys.J:
                    return (((keyData == Keys.Shift) ? "\u0636" : "\u062c"));
                    

                case Keys.K:
                    return (((keyData == Keys.Shift) ? "\u062e" : "\u0643"));
                    

                case Keys.L:
                    return (((keyData == Keys.Shift) ? "\u0612" : "\u0644"));
                    

                case Keys.M:
                    return ("\u0645");
                    

                case Keys.N:
                    return (((keyData == Keys.Shift) ? "\u06ba" : "\u0646"));
                    

                case Keys.O:
                    return (((keyData == Keys.Shift) ? "\u0629" : "\u06c1"));
                    

                case Keys.P:
                    return (((keyData == Keys.Shift) ? "\u0645" : "\u067e"));//paish
                    

                case Keys.Q:
                    return ("\u0642");
                    

                case Keys.R:
                    return (((keyData == Keys.Shift) ? "\u0691" : "\u0631"));
                    

                case Keys.S:
                    return (((keyData == Keys.Shift) ? "\u0635" : "\u0633"));
                    

                case Keys.T:
                    return (((keyData == Keys.Shift) ? "\u0679" : "\u062a"));
                    

                case Keys.U:
                    return ("\u0621");
                    

                case Keys.V:
                    return (((keyData == Keys.Shift) ? "\u0638" : "\u0637"));
                    

                case Keys.W:
                    return (((keyData == Keys.Shift) ? "\u0635\u0644\u0649\u0020\u0627\u0644\u0644\u0647\u0020\u0639\u0644\u064a\u0647\u0020\u0648\u0633\u0644\u0645" : "\u0648"));
                    

                case Keys.X:
                    return (((keyData == Keys.Shift) ? "\u0698" : "\u0634"));
                    

                case Keys.Y:
                    return ("\u06d2");
                    

                case Keys.Z:
                    return (((keyData == Keys.Shift) ? "\u0630" : "\u0632"));
                    

                #region Shift Alphabets

                case Keys.Shift | Keys.A:
                    return (((true) ? "\u0622" : "\u0627"));
                    

                case Keys.Shift | Keys.B:
                    return (((true) ? "\u0613" : "\u0628"));
                    

                case Keys.Shift | Keys.C:
                    return (((true) ? "\u062b" : "\u0686"));
                    

                case Keys.Shift | Keys.D:
                    return (((true) ? "\u0688" : "\u062f"));
                    

                case Keys.Shift | Keys.E:
                    return (((true) ? "\u0610" : "\u0639"));
                    

                case Keys.Shift | Keys.F:
                    return ("\u0641");
                    

                case Keys.Shift | Keys.G:
                    return (((true) ? "\u063a" : "\u06af"));
                    

                case Keys.Shift | Keys.H:
                    return (((true) ? "\u062d" : "\u06be"));//0647 also
                    

                case Keys.Shift | Keys.I:
                    return ("\u06cc");//0649 also
                    

                case Keys.Shift | Keys.J:
                    return (((true) ? "\u0636" : "\u062c"));
                    

                case Keys.Shift | Keys.K:
                    return (((true) ? "\u062e" : "\u0643"));
                    

                case Keys.Shift | Keys.L:
                    return (((true) ? "\u0612" : "\u0644"));
                    

                case Keys.Shift | Keys.M:
                    return ("\u0645");
                    

                case Keys.Shift | Keys.N:
                    return (((true) ? "\u06ba" : "\u0646"));
                    

                case Keys.Shift | Keys.O:
                    return (((true) ? "\u0629" : "\u06c1"));
                    

                case Keys.Shift | Keys.P:
                    return (((true) ? "\u0645" : "\u067e"));//paish
                    

                case Keys.Shift | Keys.Q:
                    return ("\u0642");
                    

                case Keys.Shift | Keys.R:
                    return (((true) ? "\u0691" : "\u0631"));
                    

                case Keys.Shift | Keys.S:
                    return (((true) ? "\u0635" : "\u0633"));
                    

                case Keys.Shift | Keys.T:
                    return (((true) ? "\u0679" : "\u062a"));
                    

                case Keys.Shift | Keys.U:
                    return ("\u0621");
                    

                case Keys.Shift | Keys.V:
                    return (((true) ? "\u0638" : "\u0637"));
                    

                case Keys.Shift | Keys.W:
                    return (((true) ? "\u0635\u0644\u0649\u0020\u0627\u0644\u0644\u0647\u0020\u0639\u0644\u064a\u0647\u0020\u0648\u0633\u0644\u0645" : "\u0648"));
                    

                case Keys.Shift | Keys.X:
                    return (((true) ? "\u0698" : "\u0634"));
                    

                case Keys.Shift | Keys.Y:
                    return ("\u06d2");
                    

                case Keys.Shift | Keys.Z:
                    return (((true) ? "\u0630" : "\u0632"));
                    

                #endregion



                case Keys.Decimal:
                    return ("\u06d4");
                    

                case Keys.Oemcomma:
                    return ("\u060c");
                    

                case Keys.OemQuestion:
                    return ("\u061f");
                    

                case Keys.OemPipe:
                    return ("\u06d4");
                    

                case Keys.OemBackslash:
                    return ("\u0602");
                    

                case Keys.OemSemicolon:
                    return ("\u061b");
                    

                case Keys.OemQuotes:
                    return ("\u0022");
                    

                case Keys.OemOpenBrackets:
                    return ("\u007b");
                    this.Text = sb.ToString();
                    this.SelectionStart = this.Text.Length; 

                case Keys.OemCloseBrackets:
                    return ("\u007d");
                    this.Text = sb.ToString();
                    this.SelectionStart = this.Text.Length;
                default: return null;
            }
        }

        // =========================
        // Paragraph / Formatting
        // =========================
        public void AlignParagraph(HorizontalAlignment alignment)
        {
            // Select the current paragraph if no selection
            int start = this.SelectionStart;
            int length = this.SelectionLength;

            if (length == 0)
            {
                // Expand selection to full paragraph
                int paraStart = this.Text.LastIndexOf('\n', start);
                paraStart = (paraStart == -1) ? 0 : paraStart + 1;
                int paraEnd = this.Text.IndexOf('\n', start);
                paraEnd = (paraEnd == -1) ? this.Text.Length : paraEnd;
                this.Select(paraStart, paraEnd - paraStart);
            }

            string rtf = this.SelectedRtf;

            string alignTag = @"\qr"; // default Right
            if (alignment == HorizontalAlignment.Left) alignTag = @"\ql";
            else if (alignment == HorizontalAlignment.Center) alignTag = @"\qc";

            // Replace any existing alignment
            rtf = Regex.Replace(rtf, @"\\q[lcr]", alignTag);

            // Ensure paragraph is RTL
            if (!rtf.Contains(@"\rtlpar"))
                rtf = rtf.Replace(@"\par", @"\rtlpar\par");

            this.SelectedRtf = rtf;

            // Restore original selection
            this.Select(start, length);
        }


        public void JustifyParagraph()
        {
            if (this.SelectionLength == 0)
                this.Select(this.SelectionStart, 1);

            string rtf = this.SelectedRtf;
            rtf = Regex.Replace(rtf, @"\\q[lcr]", @"\qj");

            if (!rtf.Contains(@"\rtlpar"))
                rtf = rtf.Replace(@"\par", @"\rtlpar\par");

            this.SelectedRtf = rtf;
        }

        public void ToggleStyle(FontStyle style)
        {
            if (this.SelectionFont == null) return;
            Font f = this.SelectionFont;
            this.SelectionFont = new Font(f.FontFamily, f.Size, f.Style ^ style);
        }

        // =========================
        // Keyboard shortcuts
        // =========================
        private void UrduRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.B: ToggleStyle(FontStyle.Bold); e.Handled = true; break;
                    case Keys.I: ToggleStyle(FontStyle.Italic); e.Handled = true; break;
                    case Keys.U: ToggleStyle(FontStyle.Underline); e.Handled = true; break;
                    case Keys.L: AlignParagraph(HorizontalAlignment.Left); e.Handled = true; break;
                    case Keys.R: AlignParagraph(HorizontalAlignment.Right); e.Handled = true; break;
                    case Keys.E: AlignParagraph(HorizontalAlignment.Center); e.Handled = true; break;
                    case Keys.J: JustifyParagraph(); e.Handled = true; break;
                }
            }
        }

        // =========================
        // Watermark
        // =========================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_watermarkEnabled && string.IsNullOrEmpty(this.Text))
            {
                using (SolidBrush brush = new SolidBrush(_watermarkColor))
                {
                    e.Graphics.DrawString(_watermarkText, this.Font, brush, new PointF(0, 0));
                }
            }
        }

        private void UrduRichTextBox_TextChanged(object sender, EventArgs e)
        {
            this.Invalidate(); // redraw watermark
        }
    }
}
