using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataMember;
using BAL;
using ArthiPOS.Reporting;
using DevComponents.DotNetBar;
using ArthiPOS.shop;

namespace ArthiPOS.Controls.dashboard
{
    public partial class Sub_DasboardControl : UserControl
    {
        public List<Landlord> tclients;
        public List<Customer> customers;
        public List<ReceiveCash> lrc;
        enum eMenu
        {
            Vendor,Purchases,Recevings,Expenses
        }
        eMenu emenu = eMenu.Vendor;
        BLogic bal;
        public string date;
        public Sub_DasboardControl(string date)
        {
            InitializeComponent();
            this.date = date;
           
        }
        private void Sub_DasboardControl_Load(object sender, EventArgs e)
        {
            init(date);
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage current = (sender as System.Windows.Forms.TabControl).SelectedTab;
            switch(current.Text)
            {
                case "Vendor Sales":
                    emenu = eMenu.Vendor;
                    readDailySale(date,"");
                    break;
                case "Purchases":
                    emenu = eMenu.Purchases;
                    readCustomerDailySale(date);
                    break;
                case "Recevings":
                    emenu = eMenu.Recevings;
                    readCashRecived(date);
                    break;
                case "Expenses":
                    emenu = eMenu.Expenses;
                    readDailyExpenses(date);
                    break;
            }

        }
        #region Vendor Sales
        public void readDailySale(string date, string text)
        {
            List<Landlord> clients = bal.getLandlordsList(date, text);
            if (clients == null)
                return; 
            grid_vendor.Rows.Clear();
            grid_vendor.Refresh();
            foreach (Landlord landlord in clients)
            {
                addSalesRowinGrid(landlord, false);

            }
            tclients = clients;
        }
        private void addSalesRowinGrid(Landlord land, bool check)
        {
            /*if (land.customers.Count == 0)
            {
                return;
            }*/
            string billid = land.record_id;
            string bill_key = land.land_person.pkey;
            string date = land.date;
            string billname = land.land_person.pname;
            int totalChalan = land.customers.Count();
            int count = this.grid_vendor.Rows.Count;
            int total_quantity = 0, total_sale_amount = 0,
                total_bill_amount = 0, client_services = 0,
                total_commission_chongi = 0;


            client_services = land.expense.total_munshiana +
                land.expense.total_rent +
                land.expense.total_labour +
                land.land_person.advance + (int)land.Total_Commission + (int)land.Total_Chongi;
            string customernames = "";
            for (int i = 0; i < land.customers.Count(); i++)
            {
                Customer customer = land.customers[i];
                customernames += customer.customer_profile.pname + ", ";
                total_quantity += customer.sale._sale_quantity;
                total_sale_amount += (int)customer.sale.getTotalSale() + customer.sale.getTotalExtraAmountCustomer();
                total_bill_amount += (int)customer.sale.getTotalSale() + customer.sale.getTotalExtraAmountLandlord();
               


            }
            total_sale_amount = land.total_sale;

            total_bill_amount = land.total_sale - client_services;
            int index = Admin.GetInstance.clients.FindIndex(x => x.land_person.pkey == land.land_person.pkey);
            
            addRowGridLandlord(billname, customernames, "" + totalChalan, "" + client_services, "" + land.land_product.total_Quantity, 
                "" + total_sale_amount, "" + total_bill_amount, bill_key, date, "" + land.land_product.sale_remaining_product, 
                land.land_person.pid, "" + (land.Total_Commission + land.Total_Chongi),land.status.ToString());
        }
        private void addRowGridLandlord(string billname, string customernames, string totalChalan, string client_services,
          string total_quantity, string total_sale_amount, string total_bill_amount, string bill_key, 
          string date, string remaining_quantity, string ll_id, string chongi_commisison,string status)
        {

            int count = this.grid_vendor.Rows.Count;



            if (count == 0)
            {
                count = 1;
            }
            else
            {
                count = count + 1;
            }
            this.grid_vendor.Rows.Add();
            this.grid_vendor.Rows[count - 1].Cells[0].Value = count;
            this.grid_vendor.Rows[count - 1].Cells[1].Value = date;

            this.grid_vendor.Rows[count - 1].Cells[2].Value = bill_key;
            this.grid_vendor.Rows[count - 1].Cells[3].Value = billname;

            if (int.Parse(remaining_quantity)>0)
            {
                this.grid_vendor.Rows[count - 1].Cells[4].Value = "InComplete";
                this.grid_vendor.Rows[count - 1].Cells[4].Style.BackColor = Color.Red;
            }
            else
            {
                this.grid_vendor.Rows[count - 1].Cells[4].Value = "Completed";
                this.grid_vendor.Rows[count - 1].Cells[4].Style.BackColor = Color.LimeGreen;
            }
            this.grid_vendor.Rows[count - 1].Cells[5].Value = total_bill_amount;
            
            if (status == "Paid")
            {
                grid_vendor.Rows[count - 1].Cells[6].Value = 1;
            }
            else
            {
                grid_vendor.Rows[count - 1].Cells[6].Value = 0;
            }





        }
       



       /*
        //Combo Box in gridview
            private void grid_vendor_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (grid_vendor.CurrentCell.ColumnIndex == 6 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedValueChanged-= LastColumnComboSelectionChanged;
                comboBox.SelectedValueChanged += LastColumnComboSelectionChanged;
            }
        }

        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            var currentcell = grid_vendor.CurrentCellAddress;
            var sendingCB = sender as DataGridViewComboBoxEditingControl;
            DataGridViewTextBoxCell cel = (DataGridViewTextBoxCell)grid_vendor.Rows[currentcell.Y].Cells[0];
            cel.Value = sendingCB.EditingControlFormattedValue.ToString();
            string status= sendingCB.EditingControlFormattedValue.ToString();
            //grid_vendor.Rows[currentcell.Y].Cells[]
            string key = "";
            MessageBox.Show(sendingCB.Text);
            if (bal.changeSaleStatus(key,status))
            {

            }
        }*/




        #endregion

        #region Purchases
        public void readCustomerDailySale(string date)
        {
            customers = bal.getCustomerBills(date,true);
            grid_purchaes.Rows.Clear();
            grid_purchaes.Refresh();
            foreach(Customer customer in customers)
            {
                addRowCustomerGrid(customer.date,
                    customer.customer_profile.pkey,customer.customer_profile.pname,""+ customer.GrandTotalCustomer);
            }
        }
        private void addRowCustomerGrid(string date,string key,string billname,string total)
        {

            int count = this.grid_purchaes.Rows.Count;



            if (count == 0)
            {
                count = 1;
            }
            else
            {
                count = count + 1;
            }
            this.grid_purchaes.Rows.Add();
            this.grid_purchaes.Rows[count - 1].Cells[0].Value = count;
            this.grid_purchaes.Rows[count - 1].Cells[1].Value = date;

            this.grid_purchaes.Rows[count - 1].Cells[2].Value = key;
            this.grid_purchaes.Rows[count - 1].Cells[3].Value = billname;

           
            this.grid_purchaes.Rows[count - 1].Cells[4].Value = total;



        }

        #endregion

        #region Receivings
        private void readCashRecived(string date)
        {
            DataTable dt = bal.getRecivedCash("ReadCashCust",date,"","");
            lrc = null;
            lrc = new List<ReceiveCash>();
            grid_receivings.Rows.Clear();
            grid_receivings.Refresh();
            int count = 0,total=0;
            foreach (DataRow rw in dt.Rows)
            {
                ReceiveCash rc = new ReceiveCash(rw[0].ToString(),
                    rw[1].ToString(), int.Parse(rw[2].ToString()), rw[3].ToString(),
                    rw[4].ToString(), int.Parse(rw[5].ToString()) > 0 ? int.Parse(rw[5].ToString()) : 0,
                    0, rw[6].ToString(),rw[7].ToString(),"");
                addGridRow(rc.date, rc.name, rc.amount, rc.key, rw[5].ToString());
                lrc.Add(rc);
                total += rc.amount;
            }
            if (total > 0)
            {
                count = this.grid_receivings.Rows.Count;
                this.grid_receivings.Rows.Add();
                this.grid_receivings.Rows[count - 1].Cells[3].Value = "Total";
                this.grid_receivings.Rows[count - 1].Cells[4].Value = total;

            }
        }

        private void addGridRow(string _date, string _name, int _amount, string _key, string discount)
        {
            int count = this.grid_receivings.Rows.Count;

            this.grid_receivings.Rows.Add();
            this.grid_receivings.Rows[count - 1].Cells[0].Value = count;
            this.grid_receivings.Rows[count - 1].Cells[1].Value = _key;
            this.grid_receivings.Rows[count - 1].Cells[2].Value = _date;
            this.grid_receivings.Rows[count - 1].Cells[3].Value = _name;
            this.grid_receivings.Rows[count - 1].Cells[4].Value = _amount;
            this.grid_receivings.Rows[count - 1].Cells[5].Value = discount;

        }
        #endregion

        #region Expenses
        private void readDailyExpenses(string date)
        {
            DataTable dt = null;
            try
            {
                dt = (DataTable)bal.getTodayExpense(date);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.StackTrace);
                return;
            }

            if (dt == null)
            {
                return;
            }
            grid_expense.Rows.Clear();
            grid_expense.Refresh();
            int count = 0, total_expense = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                count = this.grid_expense.Rows.Count;

                string _date = row[0].ToString();
                string _name = row[1].ToString();
                string _amount = row[2].ToString();
                string _key = row[3].ToString();

                total_expense += int.Parse(_amount);
                addGridRow(_date, _name, _amount, _key);


            }
            if (total_expense > 0)
            {
                count = this.grid_expense.Rows.Count;
                this.grid_expense.Rows.Add();
                this.grid_expense.Rows[count - 1].Cells[3].Value = "Total";
                this.grid_expense.Rows[count - 1].Cells[4].Value = total_expense;

            }
        }
        private void addGridRow(string _date, string _name, string _amount, string _key)
        {
            int count = this.grid_expense.Rows.Count;
            this.grid_expense.Rows.Add();
            this.grid_expense.Rows[count - 1].Cells[0].Value = count;
            this.grid_expense.Rows[count - 1].Cells[1].Value = _date;
            this.grid_expense.Rows[count - 1].Cells[2].Value = _key;
            this.grid_expense.Rows[count - 1].Cells[3].Value = _name;
            this.grid_expense.Rows[count - 1].Cells[4].Value = _amount;
            
        }
        #endregion
        public void init( string date)
        {
            this.date = date;
            bal = new BLogic();
            grid_vendor.Rows.Clear();
            grid_vendor.Refresh();


          


            if (emenu==eMenu.Vendor)
            {
                readDailySale(date, "");

            }else if (emenu==eMenu.Purchases)
            {
                readCustomerDailySale(date);
            }
            else if (emenu == eMenu.Recevings)
            {
                readCashRecived(date);
            }
            else if (emenu == eMenu.Expenses)
            {
                readDailyExpenses(date);
            }

        }



        private void grid_vendor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (e.ColumnIndex == 6)
            {
                if (Convert.ToBoolean(grid_vendor.Rows[index].Cells[6].EditedFormattedValue) == true)
                {
                    //EXAMPLE OF OTHER CODE
                    //grid_vendor.Rows[e.RowIndex].Cells[5].Value = DateTime.Now.ToShortDateString();


                    //SET BY CODE THE CHECK BOX
                    grid_vendor.Rows[e.RowIndex].Cells[6].Value = 1;


                    string key =grid_vendor.Rows[e.RowIndex].Cells[2].Value.ToString();
                    if (bal.changeSaleStatus(key, "Paid",date, grid_vendor.Rows[e.RowIndex].Cells[3].Value.ToString()))
                    {
                        ToastNotification.Show(this, ConstMessages._StatusChange);
                    }
                }
                else //When you decheck
                {
                   // grid_vendor.Rows[e.RowIndex].Cells[5].Value = String.Empty;

                    //SET BY CODE THE CHECK BOX
                    grid_vendor.Rows[e.RowIndex].Cells[6].Value = 0;

                    string key = grid_vendor.Rows[e.RowIndex].Cells[2].Value.ToString();
                    if (bal.changeSaleStatus(key, "unPaid", date, grid_vendor.Rows[e.RowIndex].Cells[3].Value.ToString()))
                    {
                        ToastNotification.Show(this, ConstMessages._StatusChange);
                    }

                }
            }

        }

        private void grid_vendor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (e.ColumnIndex == 7)
            {
                string billkey = grid_vendor.Rows[index].Cells[2].Value.ToString();
                Landlord land = tclients.Find(x => x.land_person.pkey == billkey);
                if (land!=null)
                {
                    using (RCBilling rc = new RCBilling(land, land.date))
                    {
                        rc.ShowDialog();
                    }
                }
            }
        }

        private void grid_purchaes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (e.ColumnIndex == 5)
            {
                string billkey = grid_purchaes.Rows[index].Cells[2].Value.ToString();
                Customer cust = customers.Find(x => x.customer_profile.pkey == billkey);
                if (cust != null)
                {
                    using (RCBilling rc = new RCBilling(cust, cust.date))
                    {
                        rc.ShowDialog();
                    }
                }

            }
        }

        private void btn_add_customer_Click(object sender, EventArgs e)
        {
            //AddCash ac = new AddCash(date);
            //ac.ShowDialog();
            //readCashRecived(date);
        }

       
    }
}
