using ArthiPOS.Controls.dashboard;
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
    public partial class ReportFardHisab : Form
    {
        private DataTable dt,dtprd;
        private int count=1;
        public ReportFardHisab()
        {
            InitializeComponent();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            readData();

        }
        private void setChecked()
        {
            rb_customer.Checked = false;
            rb_admin.Checked = false;
            rb_client.Checked = false;
            rb_advance.Checked = false;
        }

       

        private void rb_admin_Click(object sender, EventArgs e)
        {
            setChecked();
            rb_admin.Checked = true;

        }

        private void rb_client_Click(object sender, EventArgs e)
        {
            setChecked();
            rb_client.Checked = true;
        }

        private void rb_customer_Click(object sender, EventArgs e)
        {
            setChecked();
            rb_customer.Checked = true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Up:
                    upChangeFocus();

                    return true;
                case Keys.Down:
                    downChangeFocus();
                    return true;
                case Keys.Delete:
                    return true;
                case Keys.F2:
                    return true;
                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.F5:
                    return true;
                case Keys.Control | Keys.P:
                    //Stuff
                    btn_print_Click(this,new EventArgs());
                    return true;
                case Keys.Control | Keys.N:
                    return true;
                case Keys.Enter:
                    updateFocus();
                    searchUser();
                    return true;
             
                case Keys.Control | Keys.Enter:
                    
                    return true;
                case Keys.Alt | Keys.Enter:

                    return true;


            }



            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void downChangeFocus()
        {
            count++;
            if (count >= 9)
                count = 9;
            if (count>=1 && count<=9)
            {
                changeFocus();
            }
        }
        private string type = "Customer";
        private void updateFocus()
        {
            if (rb_customer.Focused)
            { setChecked(); rb_customer.Checked = true; txt_nameid.Focus(); type = "Customer"; count = 5; }
            else if (rb_client.Focused)
            {  setChecked(); rb_client.Checked = true; ; txt_nameid.Focus(); type = "Client"; count = 5; }
            else if (rb_advance.Focused)
            {  setChecked(); rb_advance.Checked = true; type = "Advance" ; txt_nameid.Focus(); count = 5; }
            else if (rb_admin.Focused)
            { setChecked(); rb_admin.Checked = true; type = "Admin";  txt_nameid.Focus(); count = 5; }
            else if (txt_nameid.Focused)
            { date_start.Focus(); count = 6; }
            else if (date_start.Focused)
            { date_last.Focus(); count = 7; }
            else if (date_last.Focused)
            { btn_search.Focus(); count = 8; }
            else if (btn_search.Focused)
            { txt_nameid.Focus(); count = 5; }
            else if (btn_print.Focused)
            { txt_nameid.Focus(); }
        }
        public void changeFocus()
        {
            if (count == 1)
            { rb_customer.Focus();}
            else if (count == 2)
            { rb_client.Focus(); }
            else if (count == 3)
            { rb_advance.Focus(); }
            else if (count == 4)
            { rb_admin.Focus(); }
            else if (count == 5)
            { txt_nameid.Focus(); }
            else if (count == 6)
            { date_start.Focus(); }
            else if (count == 7)
            { date_last.Focus(); }
            else if (count == 8)
            { btn_search.Focus(); }
            else if (count == 9)
            { btn_print.Focus(); }


        }

        private void upChangeFocus()
        {
            count--;
            if (count <=1)
                count = 1;

            if (count >= 1 && count <= 9)
            {
                changeFocus();
            }
        }
        private Search search = null;
        private void searchUser()
        {
            int action = 1;
            int chk = 1;
            if (rb_client.Checked)
            {
                action = 1;
                chk = 1;
            }
            else if (rb_advance.Checked)
            {
                action = 1;
                chk = 4;
            }
            else if (rb_admin.Checked )
            {
                action = 1;
                chk = 7;
            }
            else if (rb_customer.Checked)
            {
                action = 6;
                chk = 4;
            }
            searchDialog(action, txt_nameid.Text, chk);
        }
        private int amount=0;
        public void searchDialog(int action, string searchTxt,int chk)
        {
            using (search = new Search(action, searchTxt,chk))
            {
                DialogResult res = search.ShowDialog();
                if (action == 1)
                {
                    txt_nameid.Text = search.Name;
                    lbl_id.Text = search.Id;
                    amount = search.RAmount;
                }
                else if (action == 6)
                {
                    txt_nameid.Text = search.Name;
                    lbl_id.Text = search.Id;
                    amount = search.RAmount;

                }
                search.Close();
                return;
            }
        }


        private void readCustomerFard()
        {
            string id= lbl_id.Text;
            string name = txt_nameid.Text;
            string sdate = date_start.Text;
            string ldate = date_last.Text;
            //DataTable dt=new BLogic()
        }

        public void readData()
        {
            string id = lbl_id.Text;
            string sdate = date_start.Text;
            string ldate = date_last.Text;
            if (rb_customer.Checked) type = "Customer";
            else if (rb_client.Checked) type = "Client";
            else if (rb_advance.Checked) type = "ClientInvest";
            else if (rb_admin.Checked) type = "Admin";
            if (txt_nameid.Text == "")
                return;

            dt = new BLogic().readFardHisab(type, id, sdate, ldate);
            string ptype = "";
            if (type == "Client")
            {
                ptype = "ClientProduct";
            }
            else if(type=="Customer")
            {
                ptype = "CustomerProduct";
            }
            else
            {
                ptype = "None";
            }
            dtprd = new BLogic().readFardHisab(ptype, id, sdate, ldate);


            dg_invoice.Columns.Clear();
            dg_invoice.DataSource = dt;
        }
        public void showReport(DataTable dtr,string id,string name,string sdate,string ldate)
        {
            AllReportsCC rp = new AllReportsCC();
            int initialBalance = 0;
            if (dtr != null)
            {
                if (dtr.Rows.Count > 0)
                {
                    DataRow dr = dtr.Rows[0];
                    if (rb_customer.Checked) initialBalance = int.Parse(dr[7].ToString());
                    else if (rb_client.Checked) initialBalance = int.Parse(dr[8].ToString());
                    else if (rb_advance.Checked) initialBalance = int.Parse(dr[8].ToString());
                    else if (rb_admin.Checked) initialBalance = int.Parse(dr[8].ToString());
                }
            }
            rp.BillandRecevings(null, dtr, dtprd, id, name, sdate, ldate, initialBalance + "");
            rp.ShowDialog();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            showReport(dt, lbl_id.Text, txt_nameid.Text, date_start.Text, date_last.Text);
        }
    }
}
