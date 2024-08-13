using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAL;
using DataMember;
using ArthiPOS.controls.dashboard;
using ArthiPOS.utill;
using ArthiPOS.Properties;

namespace ArthiPOS.controls
{
    public partial class ViewTransport : UserControl
    {
        private Landlord client;
        private Customer customer;
        private BLogic bal;
        public ViewTransport()
        {
            InitializeComponent();
        }

        public void updateUI()
        {
            lbl_khata_id.Text = Resources.ResourceManager.GetString("a0013");
            lbl_total.Text = Resources.ResourceManager.GetString("a0503");
            _lbl_quantity.Text = Resources.ResourceManager.GetString("a0401");
            _lbl_munshiana.Text = Resources.ResourceManager.GetString("a0307");
            _lbl_labour.Text = Resources.ResourceManager.GetString("a0303");
            _lbl_rent.Text = Resources.ResourceManager.GetString("a0508");
            _lbl_advance.Text = Resources.ResourceManager.GetString("a1025");
            _lbl_comision_chongi.Text = Resources.ResourceManager.GetString("a1026");

        }

        public ViewTransport(Landlord client,string names)
        {
            InitializeComponent();
            updateUI();
            this.client = client;
            this.bal = new BLogic();

            int rent = client.expense.total_rent;
            int labour = client.expense.total_labour;
            int munshiana = client.expense.total_munshiana;
            int quantity = client.land_product.total_Quantity;

            lbl_date.Text = "" + client.date;
            lbl_id.Text = client.land_person.pkey;
            lbl_name.Text = client.land_person.pname;
            lbl_landlord_name.Text = names;
            lbl_total_rent.Text =""+rent ;
            lbl_total_quantity.Text = "" + quantity;
            lbl_total_Labour.Text = "" + labour;
            lbl_total_munshiana.Text = "" + munshiana;
            lbl_advance.Text = ""+client.land_person.advance;
            lbl_commission_chongi.Text = "" + (client.Total_Commission+client.Total_Chongi);
            lbl_total.Text = ""+client.GetGrandTotal;
        }

        public ViewTransport(Customer customer)
        {
            InitializeComponent();
            updateUI();
            this.customer = customer;
            this.bal = new BLogic();

            pan_header.BackColor = CustomColors.colors[0];
            lbl_date.Text = "" + this.customer.date;
            lbl_id.Text = this.customer.customer_profile.pkey;
            lbl_name.Text = this.customer.customer_profile.pname;
            lbl_total.Text = "" + this.customer.getGrandTotalCustomer();
            lbl_total_quantity.Text = "" + this.customer.total_quantity;
            lbl_commission_chongi.Text = "" + (this.customer.Total_Commission+ this.customer.Total_Chongi);

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            var item = Admin.GetInstance.clients.Find(x => x.land_person.pkey == client.land_person.pkey);
            if (this.bal.delete_DailyMaal(item.land_person.pid,item.date, "Maal"))
            {
                Admin.GetInstance.clients.Remove(item);
                this.Dispose();
            }
            else
            {
                AlertMsg.Show(item.land_person.pid+" : Record Not Deleted..", AlertMsg.AlertType.error);
            }
            
        }

        private void Btn_view_sale_Click(object sender, EventArgs e)
        {
            BLogic bal = new BLogic();
            SaleDetail sd;
            if (client != null)
            {
                sd = new SaleDetail(client);
                sd.ShowDialog();

            }
            else if (customer != null)
            {
                sd = new SaleDetail(customer.customer_profile.pkey,customer.date, customer);
                sd.ShowDialog();

            }
        }
    }
}
