using ArthiPOS.controls;
using ArthiPOS.shop;
using ArthiPOS.utill;
using ArthiPOS.Utill;
using BAL;
using DataMember;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonUtilities;
using ArthiPOS.Controls;

namespace ArthiPOS
{
    public partial class Authentication : Form
    {
        bool check=false;
        int tcheck = 0;
        public Authentication()
        {
            InitializeComponent();
            Login login1 = new Login(this);
            panel1.Controls.Add(login1);
        }
        public Authentication(int check)
        {
            InitializeComponent();
            this.tcheck = check;
            Login login1 = new Login(this,check);
            panel1.Controls.Add(login1);
        }

        public static Account Account { get; set; }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            
            close();
        }
        public void close()
        {
            new BLogic().closeConnection();
            //CommonUtill.IsFileinUse();
            if (check==false)
            {
                Authentication.Account = null;
                Application.Exit();
            }
            this.Close();
        }

        private void Authentication_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            try
            { 
                new EncrypDecrypt().EncryptDatabase(RegistryAccess.GetStringRegistryValue(Const.REGKEY, ""));
            }
            catch (IOException ex)
            {
                MessageBox.Show("Fail To Encrypt..");
            }
        }

        private void click_file_security_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddConfig cnf = new AddConfig(false);
            cnf.ShowDialog();
            Authentication_Load(this,new EventArgs());

        }

        private void Authentication_Load(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Login login1 = new Login(this);
            panel1.Controls.Add(login1);
        }
    }
}
