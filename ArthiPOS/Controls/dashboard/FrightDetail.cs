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

namespace ArthiPOS.Controls.dashboard
{
    public partial class FrightDetail : Form
    {
        private string date;
        private BLogic bal;


        public FrightDetail()
        {
            InitializeComponent();
        }
        public FrightDetail(string date)
        {
            InitializeComponent();
            this.date = date;
            this.bal = new BLogic();
        }

        private void FrightDetail_Load(object sender, EventArgs e)
        {
           
            lbl_Date.Text = date;
            DataTable dt=bal.searchRecords(this.date, "Fright", this.date, 1, 20);
            dg_fright.DataSource = dt;

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                
                case Keys.Escape:

                    //dg_invoice_CellClick(this,new DataGridViewCellEventArgs(8,currentrow));
                    this.Close();
                    return true;
              


            }



            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            int ck= 0;
            if (chk_zm.Checked)
            {
                ck = 1;
            }
            else
            {
                ck = 0;
            }
            DataTable dt = bal.searchRecords(date_start.Text, "Fright", date_last.Text, ck, 20);
            dg_fright.DataSource = dt;
        }

        private void chk_zm_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_zm.Checked)
            {
                chk_zm.Text = "Bipari";
            }
            else
            {
                chk_zm.Text = "Zamidar";
            }
        }
    }
}
