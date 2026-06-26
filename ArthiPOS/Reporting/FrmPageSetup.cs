using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using DevExpress.XtraTab;
namespace ArthiPOS.Reporting
{
    public partial class FrmPageSetup : Form
    {
        public PaperSize SelectedPaperSize { get; private set; }
        public PaperOrientation SelectedOrientation { get; private set; }

        public FrmPageSetup()
        {
            InitializeComponent();


            // ENTER key will trigger OK
            this.AcceptButton = btnOK;

            cmbOrientation.Items.Clear();
            cmbOrientation.Items.Add("Portrait");
            cmbOrientation.Items.Add("Landscape");

            cmbPaperSize.Items.Clear();

            // Common paper sizes (Crystal-supported)
            cmbPaperSize.Items.Add("Paper10x14");
            cmbPaperSize.Items.Add("Paper11x17");
            cmbPaperSize.Items.Add("A4");
            cmbPaperSize.Items.Add("Letter");
            cmbPaperSize.Items.Add("Legal");



            cmbPaperSize.SelectedIndex = 2;     // A4 default
            cmbOrientation.SelectedIndex = 0;   // Portrait default

        }
        private PaperSize GetPaperSize(string paper)
        {
            switch (paper)
            {
                case "Paper10x14": return PaperSize.Paper10x14;
                case "Paper11x17": return PaperSize.Paper11x17;
                case "A4": return PaperSize.PaperA4;
                case "A5": return PaperSize.PaperA5;
                case "Letter": return PaperSize.PaperLetter;
                case "Legal": return PaperSize.PaperLegal;
                case "Tabloid": return PaperSize.PaperTabloid;
                case "Executive": return PaperSize.PaperExecutive;
                case "Folio": return PaperSize.PaperFolio;

                default: return PaperSize.PaperA4;
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            // Paper Size
            switch (cmbPaperSize.Text)
            {
                case "A4":
                    SelectedPaperSize = PaperSize.PaperA4;
                    break;
                case "Letter":
                    SelectedPaperSize = PaperSize.PaperLetter;
                    break;
                case "Legal":
                    SelectedPaperSize = PaperSize.PaperLegal;
                    break;
                case "A3":
                    SelectedPaperSize = PaperSize.PaperA3;
                    break;
                case "A5":
                    SelectedPaperSize = PaperSize.PaperA5;
                    break;
            }
            SelectedPaperSize = GetPaperSize(cmbPaperSize.Text);

            // Orientation
            SelectedOrientation = (cmbOrientation.Text == "Landscape")
                ? PaperOrientation.Landscape
                : PaperOrientation.Portrait;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
