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
    public partial class PasswordForgot : Form
    {
        private BLogic bal;
        public PasswordForgot()
        {
            InitializeComponent();
            bal = new BLogic();
        }

        private void btn_save_password_Click(object sender, EventArgs e)
        {
            string key = txt_key.Text;
            string oldpass = txt_last_password.Text;

            string newpass = txt_new_password.Text;
            if (bal.passwordChange(key,oldpass,newpass))
            {
                MessageBox.Show("Password Change Successfully..");
                this.Close();
            }else
            {
                MessageBox.Show("Key or Last Password Not Correct. If you forgot both Please Contact Admin..");
            }
            

        }
    }
}
