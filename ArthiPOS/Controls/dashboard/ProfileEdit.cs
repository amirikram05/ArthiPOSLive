using BAL;
using DataMember;
using System;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class ProfileEdit : Form
    {
        private Account acc;
        private BLogic bal;
        public ProfileEdit()
        {
            InitializeComponent();
        }
        public ProfileEdit(Account acc)
        {
            InitializeComponent();
            this.acc = acc;
            this.bal = new BLogic();

        }

        private void ProfileEdit_Load(object sender, EventArgs e)
        {
            txt_user_name.Text = acc.username;
            txt_trade_name.Text = acc.shop_name;
            txt_proprietors.Text = acc.propriters_name;
            txt_address.Text = acc.address;
            txt_phone.Text = acc.phone;
            txt_name1.Text = acc.name1;
            txt_name2.Text = acc.name2;
            txt_phone1.Text = acc.phone1;
            txt_phone2.Text = acc.phone2;
            lbl_license.Text = acc.license_no;
            lbl_license_exp_date.Text = acc.license_exp_date;
            lbl_registration_id.Text = acc.api_key;
            lbl_reg_expiry_date.Text = acc.api_key_exp_date;
            txt_business_type.Text = acc.business_type;
            txt_trade_mark.Text = acc.trade_mark;
            lbl_webid.Text = acc.web_id;


        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            acc.username = txt_user_name.Text;
            acc.shop_name = txt_trade_name.Text;
            acc.propriters_name = txt_proprietors.Text;
            acc.address = txt_address.Text;
            acc.phone = txt_phone.Text;
            acc.name1 = txt_name1.Text;
            acc.name2 = txt_name2.Text;
            acc.phone1 = txt_phone1.Text;
            acc.phone2 = txt_phone2.Text;
            acc.license_no = lbl_license.Text;
            acc.license_exp_date = lbl_license_exp_date.Text;
            acc.api_key = lbl_registration_id.Text;
            acc.api_key_exp_date = lbl_reg_expiry_date.Text;
            acc.business_type = txt_business_type.Text;
            acc.trade_mark = txt_trade_mark.Text;

            bool chk = bal.account_update(acc, "Update");
            if (chk)
            {
                Authentication.Account = acc;

                MessageBox.Show("Profile Updated");
            }
        }

        private void txt_phone2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
