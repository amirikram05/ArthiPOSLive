using BAL;
using DataMember;
using System;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class SalesChangeCustomer : Form
    {
        private Customer cust;
        public SalesChangeCustomer(string date, Customer cust)
        {
            InitializeComponent();
            this.cust = cust;
            lbl_Date.Text = date;
            lbl_custid.Text = cust.customer_profile.pid;
            lbl_custname.Text = cust.customer_profile.pname;
            lbl_quantity.Text = "" + cust.sale._sale_quantity;
            lbl_rate.Text = "" + cust.sale._sale_amount;
            lbl_totalsale.Text = "" + cust.sale._TotalSaleAmount;
            lbl_gtotal.Text = "" + cust.getGrandTotalCustomer();
            lbl_saleid.Text = "" + cust.cust_bill_id;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            bool check = new BLogic().updateNameCustomerSales(lbl_Date.Text, cust.cust_bill_id, lbl_ncustid.Text); ;
            if (check)
                this.Close();


        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {

                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.Enter:

                    try
                    {
                        btn_seach_beg_Click(this, new EventArgs());
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }


                    return true;



            }



            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btn_seach_beg_Click(object sender, EventArgs e)
        {
            using (Search search = new Search(6, txt_customerID.Text))
            {
                DialogResult res = search.ShowDialog();
                lbl_custnewname.Text = search.Name;
                lbl_ncustid.Text = search.Id;
                search.Close();

            }

        }
    }
}
