using System.Drawing;
using System.Windows.Forms;

namespace ArthiPOS.Reporting
{
    public partial class ReportAugrai : UserControl
    {
        public ReportAugrai()
        {
            InitializeComponent();
            documentViewer1.Size = new Size(1218, 530);
            documentViewer1.Location = new Point(0, 141);
        }
        public ReportAugrai(CustomerAugrai obj_src)
        {
            InitializeComponent();
            documentViewer1.DocumentSource = obj_src;
        }
        public ReportAugrai(BillReports bill)
        {
            InitializeComponent();
            documentViewer1.DocumentSource = bill;

        }
    }
}
