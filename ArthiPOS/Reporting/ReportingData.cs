using ArthiPOS.Controls.dashboard;
using ArthiPOS.Properties;
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
    public partial class ReportingData : Form
    {
        string startdate = "", lastdate = "",search="",action="";
        private BLReport bal;
        private DataTable dt;
        public ReportingData()
        {
            InitializeComponent();
            bal = new BLReport();
        }

        private void ReportingData_Load(object sender, EventArgs e)
        {
            startdate = date_start.Text;
            lastdate = date_last.Text;
            comboBox1.Items[0]= Resources.ResourceManager.GetString("rd1", ci);
            comboBox1.Items[1] = Resources.ResourceManager.GetString("rd2", ci);
            comboBox1.Items[2] = Resources.ResourceManager.GetString("rd3", ci);
            comboBox1.Items[3] = Resources.ResourceManager.GetString("rd4", ci);
            comboBox1.Items[4] = Resources.ResourceManager.GetString("rd5", ci);
        }

        int pageindex = 0;
        private void btn_search_Click(object sender, EventArgs e)
        {
            startdate = date_start.Text;
            lastdate = date_last.Text;
            //if (rd_rp1.Checked) { action = "SeasonDetail";}
            //if (rd_rp2.Checked) { action = "ExpenseDet"; }
            //if (rd_rp3.Checked) { action = "BipariDet"; }
            //if (rd_rp4.Checked) { action = "CustomerDet"; search = txt_name.Text; }
            //if (rd_rp5.Checked) { action = "AugraiTotDet"; }
            if (comboBox1.SelectedIndex ==0) { action = "SeasonDetail"; }
            else if (comboBox1.SelectedIndex ==1) { action = "ExpenseDet"; }
            else if (comboBox1.SelectedIndex ==2) { action = "BipariDet"; }
            else if (comboBox1.SelectedIndex ==3) { action = "CustomerDet"; search = txt_name.Text; }
            else if (comboBox1.SelectedIndex ==4) { action = "AugraiTotDet"; }
            else if (comboBox1.SelectedIndex == 5) {action= "AugraiFresh"; search = "RemainingFreshNotZero"; }
            else if (comboBox1.SelectedIndex == 6) { action = "AugraiFresh"; search = "AllAugrai"; }
            else if (comboBox1.SelectedIndex == 7) { action = "ReceDet"; }

            List<object> list =bal.p_reporting_CRUD(action,startdate, lastdate, pageindex, 100, search);
            dt = (DataTable)list[1];
            
            dg_data.DataSource = dt;
            if (dg_data.Rows.Count==0)
            {
                return;
            }
            if (comboBox1.SelectedIndex == 0)
            {
                dg_data.Columns[0].HeaderCell.Value = Resources.ResourceManager.GetString("a1094", ci);
                dg_data.Columns[1].HeaderCell.Value = Resources.ResourceManager.GetString("a0012", ci);
                dg_data.Columns[2].HeaderCell.Value = Resources.ResourceManager.GetString("a0205", ci);
                dg_data.Columns[3].HeaderCell.Value = Resources.ResourceManager.GetString("a0401", ci);
                dg_data.Columns[4].HeaderCell.Value = Resources.ResourceManager.GetString("a2024", ci);
                dg_data.Columns[5].HeaderCell.Value = Resources.ResourceManager.GetString("a1061", ci);
                dg_data.Columns[6].HeaderCell.Value = Resources.ResourceManager.GetString("a0512", ci);
                dg_data.Columns[7].HeaderCell.Value = Resources.ResourceManager.GetString("a2021", ci);
                dg_data.Columns[8].HeaderCell.Value = Resources.ResourceManager.GetString("a0038", ci);
                dg_data.Columns[9].HeaderCell.Value = Resources.ResourceManager.GetString("sa9", ci);
            }
            if (comboBox1.SelectedIndex == 1) { action = "ExpenseDet";
                dg_data.Columns[0].HeaderCell.Value = Resources.ResourceManager.GetString("ex1", ci);
                dg_data.Columns[1].HeaderCell.Value = Resources.ResourceManager.GetString("ex2", ci);
                dg_data.Columns[2].HeaderCell.Value = Resources.ResourceManager.GetString("ex3", ci);
                dg_data.Columns[3].HeaderCell.Value = Resources.ResourceManager.GetString("ex4", ci);
                dg_data.Columns[4].HeaderCell.Value = Resources.ResourceManager.GetString("ex5", ci);
            }
            if (comboBox1.SelectedIndex == 2) { action = "BipariDet";
                dg_data.Columns[0].HeaderCell.Value = Resources.ResourceManager.GetString("bd1", ci);
                dg_data.Columns[1].HeaderCell.Value = Resources.ResourceManager.GetString("bd2", ci);
                dg_data.Columns[2].HeaderCell.Value = Resources.ResourceManager.GetString("bd3", ci);
                dg_data.Columns[3].HeaderCell.Value = Resources.ResourceManager.GetString("bd4", ci);
                dg_data.Columns[4].HeaderCell.Value = Resources.ResourceManager.GetString("bd5", ci);
                dg_data.Columns[5].HeaderCell.Value = Resources.ResourceManager.GetString("bd6", ci);
            }
            if (comboBox1.SelectedIndex == 3) { action = "CustomerDet"; search = txt_name.Text;
                dg_data.Columns[0].HeaderCell.Value = Resources.ResourceManager.GetString("cd1", ci);
                dg_data.Columns[1].HeaderCell.Value = Resources.ResourceManager.GetString("cd2", ci);
                dg_data.Columns[2].HeaderCell.Value = Resources.ResourceManager.GetString("cd3", ci);
                dg_data.Columns[3].HeaderCell.Value = Resources.ResourceManager.GetString("cd4", ci);
                dg_data.Columns[4].HeaderCell.Value = Resources.ResourceManager.GetString("cd5", ci);
                dg_data.Columns[5].HeaderCell.Value = Resources.ResourceManager.GetString("cd6", ci);
                dg_data.Columns[6].HeaderCell.Value = Resources.ResourceManager.GetString("cd7", ci);
                dg_data.Columns[7].HeaderCell.Value = Resources.ResourceManager.GetString("cd8", ci);
                dg_data.Columns[8].HeaderCell.Value = Resources.ResourceManager.GetString("cd9", ci);
            }
            if (comboBox1.SelectedIndex == 4) { action = "AugraiTotDet";
                dg_data.Columns[0].HeaderCell.Value = Resources.ResourceManager.GetString("ad1", ci);
                dg_data.Columns[1].HeaderCell.Value = Resources.ResourceManager.GetString("ad2", ci);
                dg_data.Columns[2].HeaderCell.Value = Resources.ResourceManager.GetString("ad3", ci);
                dg_data.Columns[3].HeaderCell.Value = Resources.ResourceManager.GetString("ad4", ci);
                dg_data.Columns[4].HeaderCell.Value = Resources.ResourceManager.GetString("ad5", ci);
                dg_data.Columns[5].HeaderCell.Value = Resources.ResourceManager.GetString("ad6", ci);
                dg_data.Columns[6].HeaderCell.Value = Resources.ResourceManager.GetString("ad7", ci);
                dg_data.Columns[7].HeaderCell.Value = Resources.ResourceManager.GetString("ad8", ci);
                dg_data.Columns[8].HeaderCell.Value = Resources.ResourceManager.GetString("ad9", ci);
                dg_data.Columns[9].HeaderCell.Value = Resources.ResourceManager.GetString("ad10", ci);
                dg_data.Columns[10].HeaderCell.Value = Resources.ResourceManager.GetString("ad11", ci);
            }
            if(comboBox1.SelectedIndex == 5 || comboBox1.SelectedIndex == 6)
            {
                dg_data.Columns[0].HeaderCell.Value = Resources.ResourceManager.GetString("af1", ci);
                dg_data.Columns[1].HeaderCell.Value = Resources.ResourceManager.GetString("af2", ci);
                dg_data.Columns[2].HeaderCell.Value = Resources.ResourceManager.GetString("af3", ci);
                dg_data.Columns[3].HeaderCell.Value = Resources.ResourceManager.GetString("af4", ci);
                dg_data.Columns[4].HeaderCell.Value = Resources.ResourceManager.GetString("af5", ci);
                dg_data.Columns[5].HeaderCell.Value = Resources.ResourceManager.GetString("af6", ci);
                dg_data.Columns[6].HeaderCell.Value = Resources.ResourceManager.GetString("af7", ci);
                dg_data.Columns[7].HeaderCell.Value = Resources.ResourceManager.GetString("af8", ci);
            }
            
        }
        private string header = "";
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ur-PK");

        private void lbl_print_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void lbl_print_Click(object sender, EventArgs e)
        {
            AllReportsCC rp = new AllReportsCC();
            string[] c = new string[13];
            if (comboBox1.SelectedIndex == 0)
            {
                action = "SeasonDetail";
                c[0] = Resources.ResourceManager.GetString("a1094", ci);
                c[1] = Resources.ResourceManager.GetString("a0012", ci);
                c[2] = Resources.ResourceManager.GetString("a0205", ci);
                c[3] = Resources.ResourceManager.GetString("a0401", ci);
                c[4] = Resources.ResourceManager.GetString("a2024", ci);
                c[5] = Resources.ResourceManager.GetString("a1061", ci);
                c[6] = Resources.ResourceManager.GetString("a0512", ci);
                c[7] = Resources.ResourceManager.GetString("a2021", ci);
                c[8] = Resources.ResourceManager.GetString("a0038", ci);
                c[9] = Resources.ResourceManager.GetString("a0006", ci);
                c[10] = "";
                c[11] = "";
                c[12] = "";
            }
            else
            if (comboBox1.SelectedIndex == 1)
            {
                action = "ExpenseDet";
                c[0] = Resources.ResourceManager.GetString("ex1", ci);
                c[1] = Resources.ResourceManager.GetString("ex2", ci);
                c[2] = Resources.ResourceManager.GetString("ex3", ci);
                c[3] = Resources.ResourceManager.GetString("ex4", ci);
                c[4] = Resources.ResourceManager.GetString("ex5", ci);
                c[5] = Resources.ResourceManager.GetString("ex6", ci);
                c[6] = Resources.ResourceManager.GetString("ex7", ci);
                c[7] = Resources.ResourceManager.GetString("ex8", ci);
                c[8] = Resources.ResourceManager.GetString("ex9", ci);
                c[9] = Resources.ResourceManager.GetString("ex10", ci);
                c[10] = Resources.ResourceManager.GetString("ex11", ci);
                c[11] = Resources.ResourceManager.GetString("ex12", ci);
                c[12] = Resources.ResourceManager.GetString("ex13", ci);
            }
            else
            if (comboBox1.SelectedIndex == 2)
            {
                action = "BipariDet";
                c[0] = Resources.ResourceManager.GetString("bd1", ci);
                c[1] = Resources.ResourceManager.GetString("bd2", ci);
                c[2] = Resources.ResourceManager.GetString("bd3", ci);
                c[3] = Resources.ResourceManager.GetString("bd4", ci);
                c[4] = Resources.ResourceManager.GetString("bd5", ci);
                c[5] = Resources.ResourceManager.GetString("bd6", ci);
                c[6] = Resources.ResourceManager.GetString("bd7", ci);
                c[7] = Resources.ResourceManager.GetString("bd8", ci);
                c[8] = Resources.ResourceManager.GetString("bd9", ci);
                c[9] = Resources.ResourceManager.GetString("bd10", ci);
                c[10] = Resources.ResourceManager.GetString("bd11", ci);
                c[11] = Resources.ResourceManager.GetString("bd12", ci);
                c[12] = Resources.ResourceManager.GetString("bd13", ci);
            }
            else
            if (comboBox1.SelectedIndex == 3)
            {
                action = "CustomerDet";
                c[0] = Resources.ResourceManager.GetString("cd1", ci);
                c[1] = Resources.ResourceManager.GetString("cd2", ci);
                c[2] = Resources.ResourceManager.GetString("cd3", ci);
                c[3] = Resources.ResourceManager.GetString("cd4", ci);
                c[4] = Resources.ResourceManager.GetString("cd5", ci);
                c[5] = Resources.ResourceManager.GetString("cd6", ci);
                c[6] = Resources.ResourceManager.GetString("cd7", ci);
                c[7] = Resources.ResourceManager.GetString("cd8", ci);
                c[8] = Resources.ResourceManager.GetString("cd9", ci);
                c[9] = Resources.ResourceManager.GetString("cd10", ci);
                c[10] = Resources.ResourceManager.GetString("cd11", ci);
                c[11] = "";
                c[12] = "";
            }
            else
            if (comboBox1.SelectedIndex == 4)
            {
                action = "AugraiTotDet";
                c[0] = Resources.ResourceManager.GetString("ad1", ci);
                c[1] = Resources.ResourceManager.GetString("ad2", ci);
                c[2] = Resources.ResourceManager.GetString("ad3", ci);
                c[3] = Resources.ResourceManager.GetString("ad4", ci);
                c[4] = Resources.ResourceManager.GetString("ad5", ci);
                c[5] = Resources.ResourceManager.GetString("ad6", ci);
                c[6] = Resources.ResourceManager.GetString("ad7", ci);
                c[7] = Resources.ResourceManager.GetString("ad8", ci);
                c[8] = Resources.ResourceManager.GetString("ad9", ci);
                c[9] = Resources.ResourceManager.GetString("ad10", ci);
                c[10] = Resources.ResourceManager.GetString("ad11", ci);
                c[11] = Resources.ResourceManager.GetString("ad12", ci);
                c[12] = Resources.ResourceManager.GetString("ad13", ci);
            }
            else
            if (comboBox1.SelectedIndex == 5 || comboBox1.SelectedIndex == 6)
            {
                action = "AugraiFresh";
                c[0] = Resources.ResourceManager.GetString("af1", ci);
                c[1] = "";
                c[2] = Resources.ResourceManager.GetString("af2", ci);
                c[3] = Resources.ResourceManager.GetString("af3", ci);
                c[4] = Resources.ResourceManager.GetString("af4", ci);
                c[5] = Resources.ResourceManager.GetString("af5", ci);
                c[6] = Resources.ResourceManager.GetString("af6", ci);
                c[7] = Resources.ResourceManager.GetString("af7", ci);
                c[8] = Resources.ResourceManager.GetString("af8", ci);
            }
            



            if (comboBox1.SelectedIndex == 1 || comboBox1.SelectedIndex == 7)
                rp.Reportingdata(dt);
            else
                rp.ReportingData(dt, header, c[0], c[1], c[2], c[3], c[4], c[5], c[6],
                 c[7], c[8], c[9], c[10], c[11], c[12]);


            rp.ShowDialog();
        }

        private void chk_sort_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_sort.Checked)
            {
                pageindex = 1;
            }else
            {
                pageindex = 0;
            }
            btn_search_Click(this,new EventArgs());
        }

        private void btn_recalSale_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex==0) { action = "SeasonDetail"; header = Resources.ResourceManager.GetString("rd1", ci); btn_recalSale.Visible = txt_name.Visible = chk_sort.Visible = false; chk_sort.Visible=false; }
            if (comboBox1.SelectedIndex ==1) { action = "ExpenseDet"; header = Resources.ResourceManager.GetString("rd2", ci); btn_recalSale.Visible = txt_name.Visible = chk_sort.Visible = false; }
            if (comboBox1.SelectedIndex ==2) { action = "BipariDet"; header = Resources.ResourceManager.GetString("rd3", ci); txt_name.Visible = btn_recalSale.Visible = chk_sort.Visible = false; }
            if (comboBox1.SelectedIndex ==3)
            {
                action = "CustomerDet";
                header = Resources.ResourceManager.GetString("rd4", ci);
                txt_name.Visible = true;chk_sort.Visible = false;
                btn_recalSale.Visible = true;

            }
            if (comboBox1.SelectedIndex ==4) { action = "AugraiTotDet"; header = Resources.ResourceManager.GetString("rd5", ci); txt_name.Visible = btn_recalSale.Visible = chk_sort.Visible = false;  }
            if (comboBox1.SelectedIndex == 5) { action = "AugraiFresh"; header = Resources.ResourceManager.GetString("rd5", ci); txt_name.Visible =btn_recalSale.Visible =  false; search = "RemaingFreshNotZero"; chk_sort.Visible = true; }
            if (comboBox1.SelectedIndex == 6) { action = "AugraiFresh"; header = Resources.ResourceManager.GetString("rd5", ci); txt_name.Visible = btn_recalSale.Visible = false; search = "AllAugrai"; chk_sort.Visible = true; }
            else if (comboBox1.SelectedIndex == 7)
            {
                action = "ReceDet";
                txt_name.Visible = true; chk_sort.Visible = false;
            }
            startdate = date_start.Text;
            lastdate = date_last.Text;
        }

       
        public void searchDialog()
        {
            using (Search search = new Search(6, txt_name.Text))
            {
                DialogResult res = search.ShowDialog();
                txt_name.Text = search.Id;
                btn_search_Click(this,new EventArgs());
                search.Close();

                return;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {

                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.Enter:

                    try
                    {
                        searchDialog();
                       
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    return true;
                case Keys.Control | Keys.P:

                    lbl_print_Click(this,new EventArgs());
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

}
