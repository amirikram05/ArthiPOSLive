using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArthiPOS.Reporting.ReportView;
using BAL;
using DataMember;

namespace ArthiPOS.Reporting
{
    public partial class RepAugrai : UserControl
    {
        public RepAugrai()
        {
            InitializeComponent();

        }
        public RepAugrai(DataTable custAugrai)
        {
            InitializeComponent();
            //printReport(custAugrai);
            chk_printall.Checked = false;
            rd_check.SelectedIndex = 0;


        }
        public void printReport(DataTable custAugrai)
        {
            TestAugraiCR cr = new TestAugraiCR();

            cr.Database.Tables["CustAugrai"].SetDataSource(custAugrai);
            DataTable wm = new DataTable();
            wm.Columns.Add("waterpath", typeof(string));
            string startupPath = Environment.CurrentDirectory;
            wm.Rows.Add(@startupPath + "\\watermark.jpg");
            cr.Database.Tables["Watermark"].SetDataSource(wm);
            crystal_view_customer.ReportSource = null;
            crystal_view_customer.ReportSource = cr;
        }
        private void RepAugrai_Load(object sender, EventArgs e)
        {
            
        }

        private void chk_printall_CheckedChanged(object sender, EventArgs e)
        {
            rd_check_SelectedIndexChanged(this, new EventArgs());
        }

        private void rd_check_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (rd_check.SelectedIndex==0)
            {
                chk_saleadvance.Enabled = false;
                if (chk_printall.Checked)
                {
                        dt = new BLogic().p_customer_CRUD("Augrai", "1",date_start.Text);
                    
                }
                else
                {
                    
                        dt = new BLogic().p_customer_CRUD("Augrai","0", date_start.Text);
                    
                }
            }
            else
            if (rd_check.SelectedIndex == 1)
            {
                chk_saleadvance.Enabled = true;
                if (chk_printall.Checked)
                {
                    if (chk_saleadvance.Checked)
                        dt = new BLogic().p_customer_CRUD("ClientSale", "1", "");
                    else
                        dt = new BLogic().p_customer_CRUD("ClientInv", "1", "");


                }
                else
                {
                    if (chk_saleadvance.Checked)
                        dt = new BLogic().p_customer_CRUD("ClientSale", "0", "");
                    else
                        dt = new BLogic().p_customer_CRUD("ClientInv","0","");
                }
            }
            printReport(dt);

        }

        

        private void chk_full_detail_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = new BLogic().p_customer_CRUD("Augrai", "2", date_start.Text);
            printReport(dt);
        }

        private void crystal_view_customer_Load(object sender, EventArgs e)
        {

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            

        }

        private void date_start_CloseUp(object sender, EventArgs e)
        {
            rd_check_SelectedIndexChanged(this, new EventArgs());
        }

        private void chk_saleadvance_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_saleadvance.Checked)
            {
                chk_saleadvance.Text = "Sales";
            }else
                chk_saleadvance.Text = "Advance";
            rd_check_SelectedIndexChanged(this, new EventArgs());

        }
    }
}
