using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArthiPOS.shop;
using ArthiPOS.utill;
using BAL;
using DataMember;
using Newtonsoft.Json;
using ArthiPOS.Controls.dashboard;
using ArthiPOS.Utill;
using System.IO;
using CommonUtilities;
using ArthiPOS.Controls;
using ArthiPOS.Controls.test;
using Google.Apis.Drive.v3;

namespace ArthiPOS.controls
{
    public partial class Login : UserControl
    {
        Panel form;
        private Authentication authentication;
        private BLogic bal;
        private Account account;

        private int check = 0;
        
        public Login()
        {
            InitializeComponent();
        }

        public Login(Authentication authentication)
        {
            InitializeComponent();
            this.authentication = authentication;
            this.bal = new BLogic();
        }
        public Login(Authentication authentication,int check)
        {
            InitializeComponent();
            this.authentication = authentication;
            this.bal = new BLogic();
            this.check = check;


        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            DataRow dr;
            switch (keyData)
            {
                case Keys.Escape:
                    {
                        authentication.close();
                        return true;
                    }
                case Keys.Enter:
                    {
                        //if(txt_registration_no.ContainsFocus)
                        //{
                        //    btn_accountActiviate_Click(this, new EventArgs());
                        //}
                        //else
                        if (txt_username.ContainsFocus || txt_password.ContainsFocus)
                        {
                            btn_login_Click(this, new EventArgs());
                        }
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void password_OnValueChanged(object sender, EventArgs e)
        {
            txt_password.isPassword = true;
        }

        private async void btn_login_Click(object sender, EventArgs e)
        {
            //AccountLog acc = LogUtill.getAccountInputLog();
            //if (acc.api_key=="")
            {
                if (check != 0)
                {
                    authentication.close();
                    return;
                }
                if (txt_username.Text != "" && txt_password.Text != "")
                {
                    //try
                    //{
                    //    FirebaseApi api = new FirebaseApi();
                    //    string token = await api.SignInWithEmailAndPassword(txt_username.Text, txt_password.Text);
                    //    RegistryAccess.SetStringRegistryValue(Const.REGKEY, token);
                    //    Console.WriteLine("Token Signin: "+token);

                    //    // User successfully signed in, you can handle the next steps here
                    //    // For example, navigate to another form, display a success message, etc.
                    //}
                    //catch (Exception ex)
                    //{
                    //    // Handle sign-in error
                    //    MessageBox.Show(ex.Message, "Sign-in Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}

                    Account account = this.bal.check_User(txt_username.Text, txt_password.Text);
                    if (account != null)
                    {
                        Authentication.Account = account;

                        /*LogUtill.loadLastUseInputs_AccountForm(account.shop_name, account.username, account.registration_no,
                            account.address, account.propriters_name
                            , account.phone, account.capital_Cash,account.name1, 
                            account.phone1, account.name2, account.phone2);*/
                        LogUtill.LoginCount = LogUtill.LoginCount++;
                        //if (check==false)
                        {
                            this.Hide();
                            MainForm sistema = new MainForm();
                            sistema.ShowDialog();
                            //this.authentication.close();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Email/Password");
                }
            }
            /*else
            {
                this.Hide();
                MainForm sistema = new MainForm();
                sistema.ShowDialog();
                this.authentication.close();
            }*/
        }


        //private async void btn_accountActiviate_Click(object sender, EventArgs e)
        //{
        //    if (!CommonUtill.CheckForInternetConnection())
        //    {
        //        MessageBox.Show("Internet Not Available....");
        //        return;
        //    }


        //    string registration_no = txt_registration_no.Text;
        //    if (string.IsNullOrEmpty(registration_no))
        //    {
        //        MessageBox.Show("Please Enter Your Registration Key....");
        //    }
        //    else
        //    {
        //        //FirebaseApi api = new FirebaseApi();
        //        //string email = await api.GetEmailFromUserId(registration_no);

        //        //MessageBox.Show(email);
        //        Account account = bal.accountActivationAdd(registration_no);
        //        if (account != null)
        //        {
        //            txt_username.Text = account.username;
        //            RegistryAccess.SetStringRegistryValue(Const.REGKEY, registration_no);
        //            /*LogUtill.loadLastUseInputs_AccountForm(account.shop_name,account.username,account.api_key,
        //                    account.address, account.propriters_name
        //                    , account.phone,0,0);*/
        //            MessageBox.Show(account.username + " Account Activated....Please Login ");
        //            panel_login.Enabled = true;
        //            AddConfig cnf = new AddConfig();
        //            cnf.ShowDialog();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Registration Key Not Valid. Please Add Valid Registration Key....");
        //        }
        //    }
        //}

        private void Login_Load(object sender, EventArgs e)
        {
            //AccountLog acclog= LogUtill.getAccountInputLog();
            Account a = new Account();
            a.api_key = RegistryAccess.GetStringRegistryValue(Const.REGKEY, "");
            if (a.api_key == "")
                return;
            /*else 
            {
                try
                {
                    new EncrypDecrypt().DecryptDatabase(a.api_key);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("File Already Encrypted. Please Contact to Admin.");
                }
            }*/

            string version = System.Windows.Forms.Application.ProductVersion;
          
            lbl_version.Text= String.Format("Version {0}", version);
            account = new BLogic().check_User(a);

            if (account != null)
            {
                txt_username.Text = account.username;
                panel_login.Enabled = true;
                lbl_status.Text = "Database Connected.";
                //if (account.username!="")
                {
                    txt_password.Focus();
                }

            }
            else
            {
                lbl_status.Text = "Database Not Connected. Please Contact Admin.";
                panel_login.Enabled = false;
                return;
            }

            string conn = new BLogic().getLiveDB();
            if (conn == "c")
            {
                lbl_dbname.Text = "Testing";
            }
            else
            if (conn == "liveConn")
            {
                lbl_dbname.Text = "Live";
            }
            else
            {
                lbl_dbname.Text = conn;
            }

        }

        private void btn_forgotpassword_Click(object sender, EventArgs e)
        {
            PasswordForgot pf = new PasswordForgot();
            pf.ShowDialog();
        }

        
    }
}
