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
    public partial class PageSettings : Form
    {
        private BLogic bal;
        public PageSettings()
        {
            InitializeComponent();
            bal = new BLogic();
        }

        private void PageSettings_Load(object sender, EventArgs e)
        {
            getservicesetting();
        }

        private void getservicesetting()
        {
            DataTable dt = bal.p_pagetSetting("Read");
            if (dt == null)
                return;
            DataRow dr = dt.Rows[0];
            chk_rent.Checked = dr[0].ToString() == "" || dr[0].ToString() == "0" ? false : true;
            chk_labour.Checked = dr[1].ToString() == "" || dr[1].ToString() == "0" ? false : true;
            chk_munshiana.Checked = dr[2].ToString() == "" || dr[2].ToString() == "0" ? false : true;
            chk_bipari_commisison.Checked = dr[3].ToString() == "" || dr[3].ToString() == "0" ? false : true;
            chk_bipari_laga.Checked = dr[4].ToString() == "" || dr[4].ToString() == "0" ? false : true;
            chk_cust_chongi.Checked = dr[5].ToString() == "" || dr[5].ToString() == "0" ? false : true;
            chk_cust_commission.Checked = dr[6].ToString() == "" || dr[6].ToString() == "0" ? false : true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            int labour=chk_rent.Checked==true?1 : 0;
            int rent = chk_labour.Checked == true ? 1 : 0;
            int munshiana = chk_munshiana.Checked == true ? 1 : 0;
            int bip_laga = chk_bipari_laga.Checked == true ? 1 : 0;
            int bip_commission = chk_bipari_commisison.Checked == true ? 1 : 0;
            int cust_chongi = chk_cust_chongi.Checked == true ? 1 : 0;
            int cust_commission = chk_cust_commission.Checked == true ? 1 : 0;
            bal.p_pagesetting("Update",labour,rent,munshiana,bip_commission,bip_laga,cust_commission,cust_chongi);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            getIDsData();
        }
        private void getIDsData()
        {
            DataTable dt = bal.p_pagetSetting("ReadIDS");
            if (dt == null)
                return;
            DataRow dr = dt.Rows[0];
            lbl1.Text= dr[0].ToString();
            lbl2.Text = dr[1].ToString();
            lbl3.Text = dr[2].ToString();
            lbl04.Text = dr[3].ToString();
            lbl05.Text = dr[4].ToString();
            lbl06.Text = dr[5].ToString();
            lbl07.Text = dr[6].ToString();
            lbl8.Text = dr[7].ToString();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if(bal.p_updateALLIDS())
                getIDsData();

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex==0)
            {
                getservicesetting();
            }
            else if (tabControl1.SelectedIndex == 1)
            {

            }else if (tabControl1.SelectedIndex == 2)
            {
                getIDsData();
            }
        }
    }
}
