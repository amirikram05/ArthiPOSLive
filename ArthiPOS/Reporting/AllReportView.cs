using ArthiPOS.Reporting.ReportView;
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
    public partial class AllReportView : Form
    {
        private DataTable dt;
        private int choice=0;
        private string sdate;
        private string ldate;
        public AllReportView(DataTable dta,int choice,string sdate,string ldate)
        {
            InitializeComponent();
            this.dt = dta;
            this.choice = choice;
            this.sdate = sdate;
            this.ldate = ldate;
            load();
        }
        private void load()
        {
            if (choice == 0)
            {
                ReportingsAll cr = new ReportingsAll();
                cr.Database.Tables["Chatha"].SetDataSource(dt);
                DataTable wm = new DataTable();
                cr_cashflow.ReportSource = null;
                cr_cashflow.ReportSource = cr;
            }
            else
            if (choice == 12) { 
                ReportCashFlowSP cr = new ReportCashFlowSP();
                cr.Database.Tables["cashflow"].SetDataSource(dt);
                DataTable wm = new DataTable();
                cr_cashflow.ReportSource = null;
                cr_cashflow.ReportSource = cr;
            }
            else
            if (choice==5)
            {
                AllReportsCC rp = new AllReportsCC();
                DataRow cr = dt.Rows[0];

                DataRow cr11 = dt.Rows[1];
                DataRow cr1 = dt.Rows[dt.Rows.Count-1];
                int acc_open = int.Parse(cr[3].ToString());
                string datec =    datec = sdate + " To " + ldate;
                DataTable dtprd = new BLogic().readFardHisab("AllProduct", "", sdate, ldate);
                rp.DetailReport(dt, dtprd, sdate, ldate, acc_open, datec);
                rp.ShowDialog();
            }
            else if (choice == 6)
            {
                AllReportsCC rp = new AllReportsCC();
                rp.dailyAccounts(dt);
                rp.ShowDialog();
            }
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
