using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArthiPOS
{
    public partial class AlertMsg : Form
    {
        
        public AlertMsg(string message1, AlertType type)
        {
            InitializeComponent();
            switch (type)
            {
                case AlertType.success:
                    this.BackColor = Color.SeaGreen;
                    pictureBox1.Image = imageList1.Images[0];
                    break;
                case AlertType.info:
                    this.BackColor = Color.Gray;
                    pictureBox1.Image = imageList1.Images[1];
                    break;
                case AlertType.warning:
                    this.BackColor = Color.FromArgb(255, 128, 0);
                    pictureBox1.Image = imageList1.Images[2];
                    break;
                case AlertType.error:
                    this.BackColor = Color.Crimson;
                    pictureBox1.Image = imageList1.Images[3];
                    break;
            }
            message.Text = message1;
        }
        
        public enum AlertType
        {
            success, info, warning, error
        }

        private void AlertMsg_Load(object sender, EventArgs e)
        {
            this.Top = 60;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 60;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            close.Start();
        }

        private void timeout_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        //show translation
        int interval = 0;
        private void show_Tick(object sender, EventArgs e)
        {
            if (this.Top<60)
            {
                this.Top += interval;
                interval += 1;
            }
            else
            {
                show.Stop();
            }


        }
        public static void Show(string message1, AlertType type)
        {
            new AlertMsg(message1, type).Show();
        }
        private void close_Tick(object sender, EventArgs e)
        {
            if (this.Opacity>0)
            {
                this.Opacity-=0.5;
            }else
            {
                this.Close();
            }
        }
    }
   
}
