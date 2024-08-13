using BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArthiPOS.Reporting
{
    public partial class ReportAccounts : Form
    {
        private DataTable dt;
        public ReportAccounts()
        {
            InitializeComponent();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            string sdate = date_start.Text;
            string ldate = date_last.Text;
            dt = new BLogic().p_bs_read("DAcc", sdate, ldate);
            dg_balancest.DataSource = dt;
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            AllReportsCC rp = new AllReportsCC();
            rp.dailyAccounts(dt);
            rp.ShowDialog();
        }
    }
}
