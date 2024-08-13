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
    public partial class ReportBalanceSheet : Form
    {
        public ReportBalanceSheet()
        {
            InitializeComponent();
        }

        private void btn_checkbs_Click(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            string sdate = date_start.Text;
            string ldate = date_last.Text;
            DataTable dt = new BLogic().p_bs_read("Diff", sdate, ldate);
            DataTable dt_cc = new BLogic().p_bs_read("CurrentCash", sdate, ldate);
            dg_balancest.DataSource = dt;
            DataRow dr = dt_cc.Rows[0];
            lbl_ccash.Text = dr[1].ToString();
            lbl_corcash.Text = dr[0].ToString();


        }

        private void btn_correctbs_Click(object sender, EventArgs e)
        {
            if(chk_correctbs.Checked)
            {
                string sdate = date_start.Text;
                string ldate = date_last.Text;
                if (new BLogic().p_bs_read("UPbs", sdate, ldate)!=null)
                {
                    init();
                }
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            string sdate = date_start.Text;
            string ldate = date_last.Text;
            AllReportsCC rp = new AllReportsCC();
            List<Object> obj = (List<object>)new BLReport().p_balance_sheet_read(sdate, ldate,
                                1, 100);
            if (obj == null)
            {
                return;
            }
            DataTable dt = (DataTable)obj[1];
            DataRow cr = dt.Rows[0];
            int acc_open = int.Parse(cr[3].ToString());
            string datec = "";
            datec = date_start.Text + " To " + date_last.Text;

            rp.ReportBSeet(dt, datec);
            rp.ShowDialog();
        }
    }
}
