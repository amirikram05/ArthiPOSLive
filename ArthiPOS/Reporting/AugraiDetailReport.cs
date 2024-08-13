using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
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
