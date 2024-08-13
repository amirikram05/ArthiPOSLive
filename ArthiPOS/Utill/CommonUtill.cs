using ArthiPOS.Utill;
using BAL;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArthiPOS.utill
{
    public class CommonUtill
    {
        public static void dim_Background(Control parent, Form actionform)
        {
            // take a screenshot of the form and darken it:
            Bitmap bmp = new Bitmap(parent.ClientRectangle.Width, parent.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                G.CopyFromScreen(parent.PointToScreen(new Point(0, 0)), new Point(0, 0), parent.ClientRectangle.Size);
                double percent = 0.60;
                Color darken = Color.FromArgb((int)(255 * percent), Color.Black);
                using (Brush brsh = new SolidBrush(darken))
                {
                    G.FillRectangle(brsh, parent.ClientRectangle);
                }
            }

            // put the darkened screenshot into a Panel and bring it to the front:
            using (Panel p = new Panel())
            {
                p.Location = new Point(0, 0);
                p.Size = parent.ClientRectangle.Size;
                p.BackgroundImage = bmp;
                parent.Controls.Add(p);
                p.BringToFront();

                // display your dialog somehow:
                //Form frm = new Form();
                //frm.StartPosition = FormStartPosition.CenterParent;
                //frm.ShowDialog(this);

                // Transport actionform = new Transport();
                actionform.TopLevel = true;
                actionform.ShowInTaskbar = false;
                actionform.ShowDialog();

            } // panel will be disposed and the form will "lighten" again...
        }

        public static string getKey(string _pid,string tag,string date)
        {
            return string.Format("{0}-{1}-{2}", _pid, tag, date.Replace("-", "")); ;
        }
        public static DateTime ChangeDate(MetroDateTime datepicker, int day)
        {
            DateTime iDate;
            iDate = datepicker.Value;
            iDate = iDate.AddDays(day);
            string date = iDate.ToString("dd-MM-yyyy");
            return iDate;
        }
        public static double no_of_Days(int year,int month,int day)
        {
            DateTime today = DateTime.Today;
            DateTime xmas = new DateTime(year,month,day);
            double days = today.Subtract(xmas).TotalDays;
            return days;
        }

        public enum EnumUser
        {
            Client,Customer,LandLoard,Admin,Expense
        }
       
        public static EnumUser e_User = EnumUser.Client;
        public static string getBillID(EnumUser euser,string date,string userid,int multiplebill_id)
        {
            string cdate = date.Remove('-');
            if (EnumUser.Client== euser)
            {
                return string.Format("1{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if(EnumUser.LandLoard == euser)
            {
                return string.Format("2{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.Customer==euser)
            {
                return string.Format("3{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.Expense == euser)
            {
                return string.Format("3{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.Admin == euser)
            {
                return string.Format("Ad{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            //string id= new BLogic().p_getInvoiceID(); 
            return  "";
        }
        public static float FloorTo(float value, float interval)
        {
            var remainder = value % interval;
            return value - remainder;
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsFileinUse()
        {
            //new BLogic().closeConnection();
            string path = "..\\ArthiPOS\\bin\\Debug\\db\\db_pt";
            bool blnReturn = false;
            System.IO.FileStream fs;
            try
            {
                fs = System.IO.File.Open(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
                fs.Close();
                return true;
            }
            catch (System.IO.IOException ex)
            {
                blnReturn = false;
            }
            return blnReturn;
        }

        
    }
}
