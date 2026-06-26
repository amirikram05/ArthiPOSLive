using System.Windows.Forms;

namespace ArthiPOS.Reporting
{
    public partial class AugraiDetailReport : Form
    {
        public AugraiDetailReport()
        {
            InitializeComponent();

        }
        public AugraiDetailReport(CustomerAugrai obj_src)
        {
            InitializeComponent();
            documentViewer1.DocumentSource = obj_src;
        }
        public AugraiDetailReport(BillReports bill)
        {
            InitializeComponent();
            documentViewer1.DocumentSource = bill;

        }




    }
}
