using System;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class ProfileVendour : Form
    {
        public ProfileVendour(int type, bool isNew, string bipari_name, string bipari_id, string zamidar_name,
            string zamidar_id, string billtopay_augrai, string advance, string phone, string address)
        {
            InitializeComponent();
            if (isNew)
            {
                lbl_edit.Text = "New Vendour/Customr";
                btn_addinv.Enabled = true;
            }
            else
            {
                btn_addinv.Enabled = false;
                lbl_edit.Text = "Update Vendour/Customr";
            }
            comb_vend.SelectedIndex = type;
            txt_bipari.Text = bipari_name;
            lbl_bipari_id.Text = bipari_id;
            txt_advance_amount.Text = advance;
            txt_amount_bill_augrai.Text = billtopay_augrai;
            txt_phone.Text = phone;
            txt_address.Text = address;


        }

        private void btn_addinv_Click(object sender, EventArgs e)
        {
            if (comb_vend.SelectedIndex == 3)
            {
                string refid = txt_ref_id.Text;
                string zamidarid = refid;
                string type = comb_vend.Text;
                string name = txt_bipari.Text;
                string billtopay = txt_amount_bill_augrai.Text;
                string advance = txt_advance_amount.Text;
                string phone = txt_phone.Text;
                string address = txt_address.Text;
                string bipid = lbl_bipari_id.Text;
            }
            else if (comb_vend.SelectedIndex == 0 || comb_vend.SelectedIndex == 1
                 || comb_vend.SelectedIndex == 2)
            {
                // bool chk = pbl.insert_CC_OldRecord(tableName, cc_txt_name.Text, "", "", "", int.Parse(txt_quick_amount.Text), Admin.Date);
            }

        }

        private void btn_updatei_Click(object sender, EventArgs e)
        {

        }

        private void btn_deletei_Click(object sender, EventArgs e)
        {

        }
    }
}
