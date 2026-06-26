using BAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace ArthiPOS.Reporting
{
    public partial class ReportLedgerForm : Form
    {
        public ReportLedgerForm()
        {
            InitializeComponent();
        }

        private void ReportLedgerForm_Load(object sender, EventArgs e)
        {
            cb_ledger.SelectedIndex = 0;
        }
        private DataTable dt;
        private void btn_search_Click(object sender, EventArgs e)
        {

            if (cb_ledger.SelectedIndex == 0)
            {
                dt = new BLogic().getLedgerRead("LAB", date_start.Text, date_last.Text);
            }
            else
            if (cb_ledger.SelectedIndex == 1)
            {
                dt = new BLogic().getLedgerRead("Ledger", date_start.Text, date_last.Text);
            }
            else
            if (cb_ledger.SelectedIndex == 2)
            {
                dt = new BLogic().getLedgerRead("CGJ", date_start.Text, date_last.Text);
            }
            else
            if (cb_ledger.SelectedIndex == 3)
            {
                dt = new BLogic().getLedgerRead("NetCash", date_start.Text, date_last.Text);
            }
            else
            if (cb_ledger.SelectedIndex == 4)
            {
                dt = new BLogic().getLedgerRead("Trial", date_start.Text, date_last.Text);
            }

            dg_invoice.DataSource = dt;
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            AllReportsCC a = new AllReportsCC();
            a.printLedger(cb_ledger.SelectedIndex, dt);
            a.ShowDialog();
        }
    }
}
