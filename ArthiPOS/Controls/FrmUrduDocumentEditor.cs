using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArthiPOS.Controls
{
    public partial class FrmUrduDocumentEditor : Form
    {
        // ===== PRINT STATE =====
        private int printCharIndex = 0;
        private int pageNumber = 1;

        // ===== HEADER / FOOTER =====
        private string HeaderText = "بسم اللہ الرحمن الرحیم";
        private string FooterText = "صفحہ {0}";

        // ===== TEMPLATES =====
        private List<UrduTemplate> templates;

        public FrmUrduDocumentEditor()
        {
            InitializeComponent();

            rtbEditor.Margin = new Padding(60, 40, 60, 40);

            rtbEditor.Enter += ForceRTL;
            rtbEditor.KeyUp += KeepRTLOnEnter;
            rtbEditor.SelectionChanged += KeepUrduFont;
            rtbEditor.TextChanged += ApplyUrduFormatting;

            InitTemplates();
        }

        private void FrmUrduDocumentEditor_Load(object sender, EventArgs e)
        {
            ConvertToUrduRichText();

            cmbFont.Items.AddRange(new object[]
            {
                "Jameel Noori Nastaleeq",
                "Noto Nastaliq Urdu",
                "Urdu Typesetting"
            });

            cmbFontSize.Items.AddRange(new object[]
            {
                "12","14","16","18","20","24","28"
            });

            cmbLineSpacing.Items.AddRange(new object[]
            {
                "2","4","6","8"
            });

            cmbFont.SelectedIndex = 0;
            cmbFontSize.SelectedIndex = 1;
            cmbLineSpacing.SelectedIndex = 0;

            printDocument1.DefaultPageSettings.Margins =
                new Margins(80, 80, 100, 80);
        }

        // =====================================================
        // URDU CORE
        // =====================================================

        private void ConvertToUrduRichText()
        {
            rtbEditor.RightToLeft = RightToLeft.Yes;
            rtbEditor.Font = new Font("Jameel Noori Nastaleeq", 14);
            rtbEditor.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void ForceRTL(object sender, EventArgs e)
        {
            rtbEditor.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void KeepRTLOnEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rtbEditor.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void KeepUrduFont(object sender, EventArgs e)
        {
            if (rtbEditor.SelectionFont == null)
                rtbEditor.SelectionFont =
                    new Font("Jameel Noori Nastaleeq", 14);
        }

        private void ApplyUrduFormatting(object sender, EventArgs e)
        {
            FixRtlRtf();
            JustifyUrdu();
        }

        private void FixRtlRtf()
        {
            if (!rtbEditor.Rtf.Contains(@"\rtlpar"))
                rtbEditor.Rtf = rtbEditor.Rtf.Replace(@"\par", @"\rtlpar");
        }

        private void JustifyUrdu()
        {
            string rtf = rtbEditor.Rtf;
            rtf = rtf.Replace(@"\ql", @"\qj")
                     .Replace(@"\qr", @"\qj")
                     .Replace(@"\qc", @"\qj");
            rtbEditor.Rtf = rtf;
        }

        // =====================================================
        // FONT & STYLE
        // =====================================================

        private void cmbFont_SelectedIndexChanged(object sender, EventArgs e) => ApplyFont();
        private void cmbFontSize_SelectedIndexChanged(object sender, EventArgs e) => ApplyFont();

        private void ApplyFont()
        {
            if (rtbEditor.SelectionFont == null) return;
            if (string.IsNullOrWhiteSpace(cmbFontSize.Text)) return;
            float size = float.Parse(cmbFontSize.Text);
            FontStyle style = rtbEditor.SelectionFont.Style;

            rtbEditor.SelectionFont =
                new Font(cmbFont.Text, size, style);
        }

        private void ToggleStyle(FontStyle style)
        {
            if (rtbEditor.SelectionFont == null) return;

            Font f = rtbEditor.SelectionFont;
            rtbEditor.SelectionFont =
                new Font(f.FontFamily, f.Size, f.Style ^ style);
        }

        private void btnBold_Click(object sender, EventArgs e) => ToggleStyle(FontStyle.Bold);
        private void btnItalic_Click(object sender, EventArgs e) => ToggleStyle(FontStyle.Italic);
        private void btnUnderline_Click(object sender, EventArgs e) => ToggleStyle(FontStyle.Underline);

        private void btnAlignRight_Click(object sender, EventArgs e)
        {
            rtbEditor.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void btnAlignCenter_Click(object sender, EventArgs e)
        {
            rtbEditor.SelectionAlignment = HorizontalAlignment.Center;
        }
        private void AlignParagraphRtl(HorizontalAlignment alignment)
        {
            if (rtbEditor.SelectionLength == 0)
                rtbEditor.Select(rtbEditor.SelectionStart, 1); // select at least one char

            string rtf = rtbEditor.SelectedRtf;

            string alignTag = "\\qr"; // default to right
            if (alignment == HorizontalAlignment.Left) alignTag = "\\ql";
            else if (alignment == HorizontalAlignment.Center) alignTag = "\\qc";
            else if (alignment == HorizontalAlignment.Right) alignTag = "\\qr";

            // Replace existing alignment tags (\ql, \qr, \qc) with the new one
            rtf = System.Text.RegularExpressions.Regex.Replace(rtf, @"\\q[lcr]", alignTag);

            // Ensure RTL paragraph
            if (!rtf.Contains(@"\rtlpar"))
            {
                rtf = rtf.Replace(@"\par", @"\rtlpar\par");
            }

            rtbEditor.SelectedRtf = rtf;
        }

        private void cmbLineSpacing_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbEditor.SelectionCharOffset = int.Parse(cmbLineSpacing.Text);
        }

        // =====================================================
        // WATERMARK (EDITOR)
        // =====================================================

        private void btnApplyWatermark_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWatermark.Text)) return;

            rtbEditor.SelectionStart = 0;
            rtbEditor.SelectionLength = 0;

            rtbEditor.SelectionFont =
                new Font("Jameel Noori Nastaleeq", 48, FontStyle.Bold);
            rtbEditor.SelectionColor = Color.LightGray;

            rtbEditor.AppendText("\n\n" + txtWatermark.Text + "\n\n");

            rtbEditor.SelectionFont =
                new Font("Jameel Noori Nastaleeq", 14);
            rtbEditor.SelectionColor = Color.Black;
        }

        // =====================================================
        // PRINT + PDF
        // =====================================================

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            pageNumber = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            dlg.Document = printDocument1;
            if (dlg.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            printDocument1.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            printDocument1.PrinterSettings.PrintToFile = true;
            printDocument1.PrinterSettings.PrintFileName = "UrduDocument.pdf";
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle m = e.MarginBounds;
            Graphics g = e.Graphics;

            DrawHeaderFooter(g, m);
            DrawWatermark(g, m);

            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Far,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };

            string text = rtbEditor.Text.Substring(printCharIndex);

            g.MeasureString(text, rtbEditor.Font,
                new SizeF(m.Width, m.Height),
                sf, out int chars, out _);

            g.DrawString(text.Substring(0, chars),
                rtbEditor.Font, Brushes.Black, m, sf);

            printCharIndex += chars;
            e.HasMorePages = printCharIndex < rtbEditor.Text.Length;

            if (!e.HasMorePages)
            {
                printCharIndex = 0;
                pageNumber = 1;
            }
            else pageNumber++;
        }

        private void DrawHeaderFooter(Graphics g, Rectangle m)
        {
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };

            g.DrawString(HeaderText,
                new Font("Jameel Noori Nastaleeq", 16, FontStyle.Bold),
                Brushes.Black,
                new Rectangle(m.Left, m.Top - 50, m.Width, 40), sf);

            g.DrawString(FooterText.Replace("{0}", ToUrduDigits(pageNumber.ToString())),
                new Font("Jameel Noori Nastaleeq", 10),
                Brushes.Gray,
                new Rectangle(m.Left, m.Bottom + 10, m.Width, 30), sf);
        }

        private void DrawWatermark(Graphics g, Rectangle m)
        {
            if (string.IsNullOrWhiteSpace(txtWatermark.Text)) return;

            g.TranslateTransform(m.Left + m.Width / 2, m.Top + m.Height / 2);
            g.RotateTransform(-30);

            g.DrawString(txtWatermark.Text,
                new Font("Jameel Noori Nastaleeq", 48, FontStyle.Bold),
                new SolidBrush(Color.FromArgb(40, Color.Gray)),
                0, 0,
                new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.DirectionRightToLeft
                });

            g.ResetTransform();
        }

        // =====================================================
        // HTML EXPORT
        // =====================================================

        private void btnExportHtml_Click(object sender, EventArgs e)
        {
            File.WriteAllText("urdu.html", ExportHtml(), Encoding.UTF8);
        }

       

        // =====================================================
        // TEMPLATES
        // =====================================================

        private void InitTemplates()
        {
            templates = new List<UrduTemplate>
            {
                new UrduTemplate
                {
                    Name="Affidavit",
                    Content="حلف نامہ\n\nمیں حلفاً بیان کرتا ہوں کہ یہ بیان درست ہے۔"
                },
                new UrduTemplate
                {
                    Name="Agreement",
                    Content="معاہدہ\n\nیہ معاہدہ فریقین کے درمیان طے پایا۔"
                }
            };
        }

        private void LoadTemplate(string name)
        {
            var tpl = templates.FirstOrDefault(t => t.Name == name);
            if (tpl == null) return;

            rtbEditor.Text = tpl.Content;
        }

        // =====================================================
        // UTIL
        // =====================================================

        private string ToUrduDigits(string s)
        {
            return s.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲")
                    .Replace("3", "۳").Replace("4", "۴").Replace("5", "۵")
                    .Replace("6", "۶").Replace("7", "۷").Replace("8", "۸")
                    .Replace("9", "۹");
        }
        // ====================== BUTTON A: Export PDF ======================
        private void btnA_Click(object sender, EventArgs e)
        {
            try
            {
                printDocument1.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                printDocument1.PrinterSettings.PrintToFile = true;

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Files|*.pdf";
                sfd.FileName = "UrduDocument.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings.PrintFileName = sfd.FileName;
                    printDocument1.Print();
                    MessageBox.Show("PDF exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting PDF:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====================== BUTTON B: Export HTML ======================
        private void btnB2_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "HTML Files|*.html";
                sfd.FileName = "UrduDocument.html";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, ExportHtml(), Encoding.UTF8);
                    MessageBox.Show("HTML exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting HTML:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method for HTML
        private string ExportHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html dir='rtl' lang='ur'><head><meta charset='utf-8'>");
            sb.AppendLine("<style>body{font-family:'Jameel Noori Nastaleeq';margin:60px;line-height:2.2;text-align:justify}</style>");
            sb.AppendLine("</head><body>");
            foreach (string line in rtbEditor.Text.Split('\n'))
                sb.AppendLine($"<p>{System.Net.WebUtility.HtmlEncode(line)}</p>");
            sb.AppendLine("</body></html>");
            return sb.ToString();
        }

        // ====================== BUTTON C: Load Template ======================
        private void btnC_Click(object sender, EventArgs e)
        {
            if (templates == null || templates.Count == 0)
            {
                MessageBox.Show("No templates available.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Form popup = new Form();
            popup.Text = "Select Template";
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Size = new Size(300, 200);

            ListBox lb = new ListBox();
            lb.Dock = DockStyle.Fill;
            lb.Items.AddRange(templates.Select(t => t.Name).ToArray());
            popup.Controls.Add(lb);

            Button btnLoad = new Button() { Text = "Load", Dock = DockStyle.Bottom };
            btnLoad.Click += (s, ev) =>
            {
                if (lb.SelectedItem != null)
                {
                    LoadTemplate(lb.SelectedItem.ToString());
                    popup.Close();
                }
            };
            popup.Controls.Add(btnLoad);

            popup.ShowDialog();
        }

        // ====================== BUTTON D: Header/Footer Editor ======================
        private void btnD_Click(object sender, EventArgs e)
        {
            Form headerFooter = new Form();
            headerFooter.Text = "Header / Footer Settings";
            headerFooter.StartPosition = FormStartPosition.CenterParent;
            headerFooter.Size = new Size(400, 200);

            Label lblHeader = new Label() { Text = "Header:", Location = new Point(10, 20) };
            UrduTextBox txtHeader = new UrduTextBox() { Text = HeaderText, Location = new Point(80, 18), Width = 300 };
            Label lblFooter = new Label() { Text = "Footer:", Location = new Point(10, 60) };
            UrduTextBox txtFooter = new UrduTextBox() { Text = FooterText, Location = new Point(80, 58), Width = 300 };

            Button btnSave = new Button() { Text = "Save", Location = new Point(150, 120) };
            btnSave.Click += (s, ev) =>
            {
                HeaderText = txtHeader.Text;
                FooterText = txtFooter.Text;
                headerFooter.Close();
            };

            headerFooter.Controls.Add(lblHeader);
            headerFooter.Controls.Add(txtHeader);
            headerFooter.Controls.Add(lblFooter);
            headerFooter.Controls.Add(txtFooter);
            headerFooter.Controls.Add(btnSave);

            headerFooter.ShowDialog();
        }
    }

    public class UrduTemplate
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
