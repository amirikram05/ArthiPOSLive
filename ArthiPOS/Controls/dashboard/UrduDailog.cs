using System;
using System.Data;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class UrduDailog : Form
    {
        public UrduDailog()
        {
            InitializeComponent();
        }
        public UrduDailog(string descx)
        {
            InitializeComponent();
            desc = descx;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

        }
        private string desc;

        private void UrduDailog_Load(object sender, EventArgs e)
        {
            txt_urdu.Focus();
            txt_urdu.Text = desc;
        }

        private void txt_urdu_TextChanged(object sender, EventArgs e)
        {
            desc = txt_urdu.Text;
        }
        public string Description
        {
            get { return txt_urdu.Text; }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            DataRow dr;

            switch (keyData)
            {
                case Keys.Escape:
                    {
                        this.Close();
                        return true;
                    }
            }



            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
