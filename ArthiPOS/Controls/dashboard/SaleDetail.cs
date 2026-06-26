using ArthiPOS.Controls.dashboard;
using ArthiPOS.Properties;
using ArthiPOS.Utill;
using BAL;
using DataMember;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArthiPOS.controls.dashboard
{
    public partial class SaleDetail : Form
    {
        public Landlord landlord;
        public CustomerSales cs;
        public Customer customerIc;
        public string key, date;
        private BLogic bal;
        SaleParser saleParser;
        private bool isLocal = false;
        public Landlord getLandlord()
        {
            return landlord;
        }
        public CustomerSales getCustomerSale()
        {
            return cs;
        }
        public Customer getCustomer()
        {
            return customerIc;
        }
        public SaleDetail()
        {
            InitializeComponent();
        }
        public SaleDetail(Landlord landlord)
        {
            InitializeComponent();
            updateUI();
            this.landlord = landlord;
            bal = new BLogic();
        }

        public SaleDetail(string key, string date, Customer custIc)
        {
            InitializeComponent();
            updateUI();
            this.key = key;
            this.date = date;
            this.customerIc = custIc;
            bal = new BLogic();
        }

        public SaleDetail(bool isLocal, CustomerSales cs)
        {
            InitializeComponent();
            updateUI();
            this.cs = cs;
            this.isLocal = isLocal;
            bal = new BLogic();
        }

        public void updateUI()
        {
            _lbl_title_sale_detail.Text = Resources.ResourceManager.GetString("a1027");
            _lbl_date.Text = Resources.ResourceManager.GetString("a0009");
            _lbl_khata.Text = Resources.ResourceManager.GetString("a0013");
            _lbl_bipari.Text = Resources.ResourceManager.GetString("a0201");
            _lbl_total_quantity.Text = Resources.ResourceManager.GetString("a0507");
            _lbl_munshiana.Text = Resources.ResourceManager.GetString("a0509");
            _lbl_rent.Text = Resources.ResourceManager.GetString("a0508");
            _lbl_total_labour.Text = Resources.ResourceManager.GetString("a0506");
            _lbl_total_amount.Text = Resources.ResourceManager.GetString("a0503");

            _lbl_advance.Text = Resources.ResourceManager.GetString("a1025");
            _lbl_commission.Text = Resources.ResourceManager.GetString("a0302");
            _lbl_chongi.Text = Resources.ResourceManager.GetString("a0301");
            _lbl_chalan.Text = Resources.ResourceManager.GetString("a0511");

            datagrid_transport_detail.Columns[0].HeaderText = Resources.ResourceManager.GetString("a1091");// ID
            datagrid_transport_detail.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0012");// ID
            datagrid_transport_detail.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0205"); //Name
            datagrid_transport_detail.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0401"); //Quantity
            datagrid_transport_detail.Columns[4].HeaderText = Resources.ResourceManager.GetString("a0032"); // Price per beg
            datagrid_transport_detail.Columns[5].HeaderText = Resources.ResourceManager.GetString("a1091"); //Total
            datagrid_transport_detail.Columns[6].HeaderText = Resources.ResourceManager.GetString("a0033"); //Total
            datagrid_transport_detail.Columns[7].HeaderText = Resources.ResourceManager.GetString("a0206"); // ItemName
            datagrid_transport_detail.Columns[8].HeaderText = Resources.ResourceManager.GetString("a0403"); // Type
            datagrid_transport_detail.Columns[9].HeaderText = Resources.ResourceManager.GetString("a2014"); // Type

        }

        private void UCTransport_Detail_Load(object sender, EventArgs e)
        {

            if (landlord != null)
            {
                saleParser = new SaleParser(landlord.date,Admin.SaveLog, Authentication.Account.local == "0" ? false : true);

                showSale(landlord.land_person.pkey,
                    landlord.land_person.pname, landlord.client._person_cl.pname,
                    landlord.date, landlord.total_quantity,
                    (int)landlord.expense.total_rent,
                    (int)landlord.expense.total_labour,
                    (int)landlord.service.clerk_per_bill,
                    (int)landlord.service.marketfee,
                    landlord.customers.Count,
                    landlord.land_person.advance,
                    (int)landlord.GetCommission,
                    (int)landlord.GetChongi,
                    (int)landlord.GetGrandTotal,
                    ((int)landlord.expense.total_rent +
                    (int)landlord.expense.total_labour +
                    (int)landlord.service.clerk_per_bill + (int)landlord.service.marketfee +
                    landlord.land_person.advance +
                    (int)landlord.GetCommission +
                    (int)landlord.GetChongi));
                addSaleClient(landlord);
            }
            else
            {
                saleParser = new SaleParser(this.date,Admin.SaveLog, Authentication.Account.local == "0" ? false : true);
                readCustomerSale(key, this.date);
            }


        }
        public void addSaleClient(Landlord landlord)
        {
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                int total = 0;
                foreach (Customer cust in landlord.customers)
                {
                    total += (int)cust.sale.getTotalSale() + cust.sale.getTotalExtraAmountLandlord();
                    addRowTransportDetail(
                        cust.customer_profile.pid,
                        cust.customer_profile.pname,
                        "" + cust.sale._sale_quantity,
                        "" + (int)(cust.sale._sale_amount + cust.sale.add_extra_amount_Landlord
                        //+cust.sale.add_extra_amount_Landlord
                        ),
                        "" + (int)(cust.sale.getTotalSale() + cust.sale.getTotalExtraAmountLandlord()
                        //+cust.sale._sale_quantity*landlord.ExtraAmountLandlord.Extra_Amount
                        ),
                        cust.product._product_name, cust.product._weight, cust.sale.add_extra_amount_Landlord,
                        cust.product.marka);
                }
                if (total > 0)
                {
                    addRowTransportDetail("", "", "", "", "" + (total
                        //+landlord.ExtraAmountLandlord.Total_Amount_extra
                        ), "", "", 0, "");
                }
                //Records.AddCustomerUnique(item.customerID,item.customername, userkey, item.previous_amount);


            }
            catch (IOException ex)
            {

                Console.WriteLine(errorMessages.ToString());
            }
        }

        public void readCustomerSale(string key, string date)
        {
            if (key != null)
            {
                CustomerSales tcs = bal.readCustomerSale("SearchSalesByDate_Key", key, date, "Single");
                if (tcs != null)
                    cs = tcs;
                if (cs != null)
                {
                    updateCustomerSale();

                }
            }

            else
            {
                if (isLocal)
                {
                    //cs.customers.Add(customerIc);
                    cs.getTotalChongi();
                    cs.getTotalCommission();
                    cs.getSaleTotal();
                    cs.getQuantity();
                    updateCustomerSale();
                }
                else
                {
                    cs = new CustomerSales(date);
                    cs.person = customerIc.customer_profile;
                    cs.customers.Add(customerIc);
                    cs.getTotalChongi();
                    cs.getTotalCommission();
                    cs.getSaleTotal();
                    cs.getQuantity();
                    updateCustomerSale();
                }
            }
        }
        public void updateCustomerSale()
        {
            StringBuilder errorMessages = new StringBuilder();
            try
            {

                if (cs != null)

                    showSale(cs.person.pkey, cs.person.pname,
                        "",
                        date, cs.total_quantity,
                        0, 0, 0, 0, cs.total_chalan, 0,
                        (int)cs.Total_Commission, (int)cs.Total_Chongi,
                        (int)(cs.getGrandTotal()),
                        (int)(cs.Total_Commission + cs.Total_Chongi));
                foreach (Customer cu in cs.customers)
                {

                    addRowTransportDetail(cu._LandlordProfile.pid, cu._LandlordProfile.pname,
                        "" + cu.sale._sale_quantity, "" + (int)(cu.sale._sale_amount + cu.sale.add_extra_amount_Customer),
                        "" + (cu.sale.getTotalSale() + cu.sale.getTotalExtraAmountCustomer()),
                        cu.product._product_name, cu.product._weight, cu.sale.add_extra_amount_Customer, cu.product.marka);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(errorMessages.ToString());
            }
        }

        public void addRowTransportDetail(string id, string name, string quantity, string peramount,
            string total_amount, string product_name, string product_type, int extraamount, string marka)
        {
            int count = this.datagrid_transport_detail.Rows.Count;

            this.datagrid_transport_detail.Rows.Add();
            this.datagrid_transport_detail.Rows[count].Cells[1].Value = id;
            this.datagrid_transport_detail.Rows[count].Cells[2].Value = name;
            this.datagrid_transport_detail.Rows[count].Cells[3].Value = "" + quantity;
            this.datagrid_transport_detail.Rows[count].Cells[4].Value = peramount;
            this.datagrid_transport_detail.Rows[count].Cells[5].Value = extraamount;
            this.datagrid_transport_detail.Rows[count].Cells[6].Value = total_amount;
            this.datagrid_transport_detail.Rows[count].Cells[7].Value = product_name;
            this.datagrid_transport_detail.Rows[count].Cells[8].Value = product_type;
            this.datagrid_transport_detail.Rows[count].Cells[9].Value = marka;
        }

        private void datagrid_transport_detail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                if (landlord != null)
                {
                    AddExtraAmount extra = new AddExtraAmount(landlord, landlord.customers[index], index, "");
                    extra.ShowDialog();
                    if (extra.getCustomer().sale.add_extra_amount_Landlord > 0)
                    {
                        landlord.customers[index] = extra.getCustomer();

                        //landlord.total_sale = extra.getCustomer().sale._sale_quantity * extra.getCustomer().sale.add_extra_amount_Landlord;
                        //string id = new BLogic().addExtraAmountClient(extraAmount);
                        //extraAmount.Extra_ID = id;
                        landlord.total_sale = (int)landlord.customers.Sum(x => x.sale.getTotalSale() + x.sale.getTotalExtraAmountLandlord());
                        datagrid_transport_detail.Rows.Clear();
                        datagrid_transport_detail.Refresh();

                        UCTransport_Detail_Load(this, new EventArgs());

                    }
                }
                else
                {
                    cs.customers[index].isCustomerBill = true;
                    AddExtraAmount extra = new AddExtraAmount(cs, cs.customers[index], index, "");
                    extra.ShowDialog();
                    if (extra.getCustomer().sale.add_extra_amount_Customer > 0)
                    {
                        cs.customers[index] = extra.getCustomer();
                        cs.total_sale = cs.getGrandTotal();
                        cs.getTotalCommission();
                        cs.getTotalChongi();
                        datagrid_transport_detail.Rows.Clear();
                        datagrid_transport_detail.Refresh();
                        updateCustomerSale();

                        customerIc.sale._TotalSaleAmount = cs.getSaleTotal();
                        customerIc.GrandTotalCustomer = cs.getGrandTotal();
                        customerIc.Total_Commission = cs.Total_Commission;
                        customerIc.Total_Chongi = cs.Total_Chongi;
                        customerIc.date = date;
                    }
                }
            }
        }

        public void showSale(string id, string bill_name, string biapri, string date, int quantity,
            int rent, int labour, int munshiana, int marketfee, int chalan,
            int advance, int commission, int chongi, int grand_total,
            int total_service)
        {
            this.lbl_s_id.Text = "" + id;
            this.lbl_s_name.Text = "" + bill_name;
            this.lbl_s_bipari.Text = "" + biapri;
            this.lbl_s_date.Text = "" + date;
            this.lbl_s_quantity.Text = "" + quantity;
            this.lbl_s_rent.Text = "" + rent;
            this.lbl_s_labour.Text = "" + labour;
            this.lbl_s_munshiana.Text = "" + munshiana;
            this.lbl_s_chalan.Text = "" + chalan;
            this.lbl_s_advance.Text = "" + advance;
            this.lbl_commission.Text = "" + commission;
            this.lbl_chongi.Text = "" + chongi;
            this.lbl_grand_total.Text = "" + grand_total;
            this.lbl_total.Text = "" + total_service;
            this.lbl_s_marketfee.Text = "" + marketfee;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

}
