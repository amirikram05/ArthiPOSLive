using System;
using System.Windows.Forms;

namespace ArthiPOS.Reporting
{
    public partial class ReportSetting : Form
    {
        public ReportSetting()
        {
            InitializeComponent();

        }

        private void header_sect_EditValueChanged(object sender, EventArgs e)
        {
            int value = header_sect.Value;
            label1.Text = value + "";

            TemplateHA52.GroupHeaderSection3.Height = value;
        }
    }
}
