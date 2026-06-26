using ArthiPOS.Utill;
using DataMember;
using System;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class AddExtraAmount : Form
    {
        private Landlord landlord;
        private CustomerSales cs;
        private Customer customer;
        SaleParser saleParser;
        private string date;
        private string status;
        int row = 0;
        public AddExtraAmount()
        {
            InitializeComponent();
        }

        public AddExtraAmount(Landlord landlord, Customer customer, int row, string status)
        {
            InitializeComponent();
            this.landlord = landlord;
            this.customer = customer;
            this.status = status;
            this.row = row;
            date = landlord.date;
            updateExtraAmount();

        }

        private void updateExtraAmount()
        {
            if (landlord != null)
            {
                lbl_s_name.Text = landlord.land_person.pname;
                lbl_s_id.Text = landlord.land_person.pkey;
                lbl_grand_total.Text = landlord.GetGrandTotal + "";

                if (customer != null)
                {
                    txt_add_land_extra_amount.Text = customer.sale.add_extra_amount_Landlord + "";
                    txt_customer_extra_amount.Text = customer.sale.add_extra_amount_Customer + "";
                    lbl_list_sale.Text = customer.sale._sale_quantity + " X " +
                        (customer.sale._sale_amount + customer.sale.add_extra_amount_Landlord) +
                        " = " + (customer.sale._sale_quantity * customer.sale._sale_amount + customer.sale._sale_quantity * customer.sale.add_extra_amount_Landlord) + ""
                        + "\n"
                        + customer.sale._sale_quantity + " X " +
                        (customer.sale._sale_amount + customer.sale.add_extra_amount_Customer) +
                        " = " + (customer.sale._sale_quantity * customer.sale._sale_amount
                        + customer.sale._sale_quantity * customer.sale.add_extra_amount_Customer) + "";
                }

            }
            else if (cs != null)
            {
                lbl_s_name.Text = cs.person.pname;
                lbl_s_id.Text = cs.person.pkey;
                lbl_grand_total.Text = (cs.getGrandTotal()) + "";

                if (customer != null)
                {
                    txt_add_land_extra_amount.Text = customer.sale.add_extra_amount_Landlord + "";
                    lbl_list_sale.Text = customer.sale._sale_quantity + " X " + (customer.sale._sale_amount + customer.sale.add_extra_amount_Customer) +
                        " = " + (customer.sale._sale_quantity * (customer.sale._sale_amount + customer.sale.add_extra_amount_Customer)) + "";
                }
            }
        }

        public AddExtraAmount(CustomerSales cs, Customer customer, int row, string status)
        {
            InitializeComponent();
            this.cs = cs;
            this.customer = customer;
            this.status = status;
            this.row = row;
            this.date = cs.date;
            updateExtraAmount();
        }

        private void AddExtraAmount_Load(object sender, EventArgs e)
        {
            saleParser = new SaleParser(this.date, Admin.SaveLog, Authentication.Account.local == "0" ? false : true);
        }
        public Customer getCustomer()
        {
            customer.updateTotal();
            return this.customer;
        }

        private void btn_save_extra_amount_Click(object sender, EventArgs e)
        {
            string amount = txt_add_land_extra_amount.Text;
            int ex_amount = amount == "" ? 0 : int.Parse(amount);

            if (landlord != null)
            {
                customer.sale.add_extra_amount_Landlord = ex_amount;
                customer.sale.updateSale();
                landlord.UpdateTotal();
                landlord.customers[row] = customer;
                bool check = false;

                Account acc = Authentication.Account;
                if (acc.local == "0" || status == "Live")
                {
                    check = true;
                }
                else
                {
                    check = saleParser.updateLandLord(this.landlord);
                }
                if (check)
                {
                    lbl_grand_total.Text = this.landlord.GetGrandTotal + "";
                    updateExtraAmount();
                }
            }
            else if (cs != null)
            {
                customer.sale.add_extra_amount_Customer = ex_amount;
                customer.updateTotal();
                customer.sale.updateSale();
                cs.customers[row] = customer;
                lbl_grand_total.Text = (cs.getGrandTotal()) + "";
                updateExtraAmount();
                //new BLogic().addExtraAmountCustomer();
            }
        }

        private void btn_customer_amount_Click(object sender, EventArgs e)
        {
            string amount = txt_customer_extra_amount.Text;
            int ex_amount = amount == "" ? 0 : int.Parse(amount);

            customer.sale.add_extra_amount_Customer = ex_amount;
            customer.sale.updateSale();
            customer.updateTotal();
            landlord.UpdateTotal();
            landlord.customers[row] = customer;
            bool check = false;
            Account acc = Authentication.Account;
            if (acc.local == "0" || status == "Live")
            {
                check = true;

            }
            else
            {
                check = saleParser.updateLandLord(this.landlord);

            }


            if (check)
            {
                lbl_grand_total.Text = this.landlord.GetGrandTotal + "";
                updateExtraAmount();
            }
        }
    }
}
