using ArthiPOS.utill;
using ArthiPOS.Utilllll;
using BAL;
using System;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class AddInvestment : Form
    {
        public string id = "", ename = "", name = "", date = "", phone = "", address = "";
        public int remAmount = 0;

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (id != "" && (name != "") || phone != "" || address != "")
            {
                int oldamount = 0, amount = 0;
                if (txt_advance.Text == "")
                {
                    MessageBox.Show("Add Amount OR Select User");
                    return;
                }

                oldamount = remAmount;
                amount = int.Parse(txt_advance.Text);
                int total = amount;
                date = today_date.Text;

                //string clkey = BillKey.getBillID(BillKey.EnumUser.ClientInvest, date, "" + id, 0);
                string clkey = new BLogic().p_getInvoiceID("Other", "0", date);
                ProfilesBL pbl = new ProfilesBL();
                if (pbl.updateAddAmount("AddClAmount", clkey, int.Parse(id), name, ename, phone, address, total, date, "", nameof(BillKey.EnumUser.ClientInvest)))
                {
                    //new BLogic().insertTodayExpense(date, name, ""+ total, clkey, nameof(BillKey.EnumUser.ClientInvest).Substring(0, 2)+"_"+ nameof(BillKey.EnumUser.ClientInvest) + "_"+ id, nameof(BillKey.EnumUser.ClientInvest), id);
                    new BLogic().addTodaySales(date);
                    new BLogic().update_today_sales(date);
                    MessageBox.Show("Record Updated Successfully");
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void txt_advance_TextChanged(object sender, EventArgs e)
        {
            int total = remAmount + int.Parse(txt_advance.Text == "" ? "0" : txt_advance.Text);
            lbl_total.Text = total + "";
        }

        public AddInvestment(string date, string id, string name, int remAmount, string phone, string address, string ename)
        {
            InitializeComponent();
            this.remAmount = remAmount;
            this.id = id;
            this.date = date;
            this.name = name;
            this.ename = ename;
            this.phone = phone;
            this.address = address;
            lbl_id.Text = id;
            lbl_name.Text = name;
            this.date = today_date.Text;
            lbl_remaining_amount.Text = remAmount + "";
        }

        private void AddInvestment_Load(object sender, EventArgs e)
        {
            date = today_date.Text;
        }
        private void previousdate_Click(object sender, EventArgs e)
        {
            today_date.Value = CommonUtill.ChangeDate(today_date, -1);
            date = today_date.Text;
        }

        private void nextdate_Click(object sender, EventArgs e)
        {
            today_date.Value = CommonUtill.ChangeDate(today_date, 1);
            date = today_date.Text;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Control | Keys.Enter:
                    if (txt_advance.ContainsFocus)
                    {
                        btn_save_Click(this, new EventArgs());
                    }

                    return true;

                case Keys.Escape:
                    this.Close();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
