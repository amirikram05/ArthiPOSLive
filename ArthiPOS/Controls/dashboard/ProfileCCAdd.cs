using BAL;
using System;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class ProfileCCAdd : Form
    {
        private int type;
        private string tablename;
        public ProfilesBL pbl;
        public ProfileCCAdd(int type, string tablename)
        {
            InitializeComponent();
            this.type = type;
            this.tablename = tablename;
            if (tablename == "tbl_customer")
                this.cb_type.SelectedIndex = 0;
            else
                this.cb_type.SelectedIndex = 1;

        }

        private void ProfileCCAdd_Load(object sender, EventArgs e)
        {
            pbl = new ProfilesBL();
            lbl_type.Text = tablename;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {

                case Keys.Enter:

                    if (cc_txt_name.Focused)
                    {
                        if (cb_type.SelectedIndex == 0)
                            searchDialog(2); //Customer
                        else if (cb_type.SelectedIndex == 1)
                            searchDialog(1);
                    }
                    else if (txt_quick_amount.Focused)
                    {

                    }


                    return true;
                case Keys.Control | Keys.Enter:

                    addUser();
                    cc_txt_name.Focus();
                    return true;
                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.Tab:
                    if (cc_txt_name.Focused || cc_txt_name.ContainsFocus)
                    {
                        txt_address.Focus();
                    }
                    else if (txt_address.Focused || txt_address.ContainsFocus)
                    {
                        txt_quick_amount.Focus();
                    }
                    else if (txt_quick_amount.Focused || txt_quick_amount.ContainsFocus)
                    {
                        btn_cc_add.Focus();
                    }
                    return true;
            }



            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void searchDialog(int searchtype)
        {
            using (Search search = new Search(searchtype, cc_txt_name.Text))
            {
                DialogResult res = search.ShowDialog();
                cc_txt_name.Text = search.Name;
                lbl_id.Text = search.Id;
                lbl_augrai.Text = search.RAmount + "";
                txt_address.Text = search.Address;
                lbl_oldaugrai.Text = "" + search.OldAmount;
                search.Close();
                txt_quick_amount.Focus();

                return;
            }
        }
        public void addUser()
        {

            if (type == 0)
            {
                if (cc_txt_name.Text == "")
                {
                    return;
                }

                if (txt_quick_amount.Text == "")
                {
                    txt_quick_amount.Text = "0";
                }


                bool chk = false;
                if (cb_type.SelectedIndex == 0)
                {
                    chk = pbl.insert_oldRecord("Customer", lbl_id.Text, cc_txt_name.Text, today_date.Text, int.Parse(txt_quick_amount.Text), txt_address.Text);
                    new BLogic().p_fin_BalanceSheet_CRUD("I", today_date.Text, "2", "21", int.Parse(txt_quick_amount.Text), "+");

                }
                else
                {
                    chk = pbl.insert_oldRecord("Client", lbl_id.Text, cc_txt_name.Text, today_date.Text, int.Parse(txt_quick_amount.Text), txt_address.Text);
                    new BLogic().p_fin_BalanceSheet_CRUD("I", today_date.Text, "2", "22", int.Parse(txt_quick_amount.Text), "+");

                }
            }
            else if (type == 1)
            {
                if (cc_txt_name.Text == "")
                {
                    return;
                }
                if (txt_quick_amount.Text == "")
                {
                    txt_quick_amount.Text = "0";
                }



                bool chk = pbl.insert_CC_OldRecord(tablename, cc_txt_name.Text, "", "", "", int.Parse(txt_quick_amount.Text), today_date.Text);
                if (tablename == "tbl_customer")
                {
                    chk = pbl.insert_oldRecord("Customer", lbl_id.Text, cc_txt_name.Text, today_date.Text, int.Parse(txt_quick_amount.Text), txt_address.Text);
                    new BLogic().p_fin_BalanceSheet_CRUD("I", today_date.Text, "2", "21", int.Parse(txt_quick_amount.Text), "+");

                }
                else
                {
                    chk = pbl.insert_oldRecord("Client", lbl_id.Text, cc_txt_name.Text, today_date.Text, int.Parse(txt_quick_amount.Text), txt_address.Text);
                    new BLogic().p_fin_BalanceSheet_CRUD("I", today_date.Text, "2", "22", int.Parse(txt_quick_amount.Text), "+");

                }

            }
            txt_quick_amount.Text = "";
            cc_txt_name.Text = "";

        }

        private void btn_cc_add_Click(object sender, EventArgs e)
        {
            addUser();
        }

        private void cb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_type.SelectedIndex == 0)
            {
                lbl_type.Text = "tbl_customer";
            }
            else
            {
                lbl_type.Text = "tbl_client";

            }

        }
    }
}
