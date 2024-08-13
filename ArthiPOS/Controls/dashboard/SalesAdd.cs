using ArthiPOS.controls.dashboard;
using ArthiPOS.Properties;
using ArthiPOS.utill;
using ArthiPOS.Utill;
using BAL;
using DataMember;
using DevComponents.DotNetBar;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonUtilities;
using DataMember.memberlog;

namespace ArthiPOS.Controls.dashboard
{
    public partial class SalesAdd : Form
    {
       

        #region Client object , client list index, Landlord list index

        Landlord templandlord = null;
        int ll_index;
        int cust_index = 0;
        int currentrow = 0;
        int row_selected_landlord = 0;
        bool check_TextBox = false;
        private int selected_detail_datagrid_index = -1;
        private bool check_data_action = false;
        private int gridRow = 0;
        int remaining_quantity = 0;
        AdminLog adminlog;
        MetroGrid selectedgrid;


        #endregion
        BLogic bal;

        private string date;
        SaleParser saleParser;
        public enum EnumShop
        {
            Sale,
            Customer,
            BegType
        };
        public EnumShop shop_enum = EnumShop.Sale;
        private string landkey;
        public SalesAdd(string date,string userid,string bipariname,string landlordname,
            string bill_key,string remainingitems,string cl_id,string billid,string status)
        {
            InitializeComponent();
            this.date = date;
            localization();
            string search = userid;
            shop_enum = EnumShop.Customer;
            bal = new BLogic();
            lbl_status.Text = status;
            saleParser = new SaleParser(date, Admin.SaveLog);
            if (Authentication.Account.local == "1")
            {
                saleParser.SAVELOG = true;

            }
            change_AddTOUpdate(false);
            this.cl_id = cl_id;
            this.billid = billid;
            initRefresh(bipariname, landlordname,bill_key,  remainingitems,cl_id,billid);





        }
        private string cl_id = "", billid = "";
        private void initRefresh(string bipariname, string landlordname,
            string bill_key, string remainingitems, string cl_id, string billid)
        {
            List<Landlord> tclients = bal.getLandlordsList(date, "");
            foreach (Landlord landlord in tclients)
            {
                if (billid == landlord.bill_key)
                    addSalesRowinGrid(landlord, false);
            }


            {

                lbl_remaining_sale.Text = remainingitems;
                tlbl_khata_id.Text = bill_key;
                txt_client_nameid.Text = bipariname;
                txt_landloard_nameid.Text = landlordname;

                // Search Landlord
                templandlord = searchbill(billid, cl_id);
                cust_index = templandlord.customers.Count;
                templandlord.tag_Action = "insert";
                updateUIData(templandlord);
                if (templandlord.status == EStatus.InComplete)
                {
                    oldData = templandlord.customers.Count;
                    oldAmount = (int)templandlord.GetGrandTotal;
                }
            }
        }

        public void localization()
        {
            _lbl_khata.Text = Resources.ResourceManager.GetString("a0013");
            _lbl_bipari.Text = Resources.ResourceManager.GetString("a0207");
            _lbl_billname.Text = Resources.ResourceManager.GetString("a0201");
            _lbl_cust_id.Text = Resources.ResourceManager.GetString("a0012");
            _lbl_no_of_beg.Text = Resources.ResourceManager.GetString("a0401");
            _lbl_amount.Text = Resources.ResourceManager.GetString("a0039");


            _lbl_total_Begs.Text = Resources.ResourceManager.GetString("a0401");

            _lbl_total_rent.Text = Resources.ResourceManager.GetString("a0501");
            _lbl_total_labour.Text = Resources.ResourceManager.GetString("a0502");
            _lbl_total_munshiana.Text = Resources.ResourceManager.GetString("a0307");
            _lbl_total_expense.Text = Resources.ResourceManager.GetString("a0510");
            _lbl_total_advance.Text = Resources.ResourceManager.GetString("a0305");
            //_lbl_total_quantity.Text = Resources.ResourceManager.GetString("a0401");

            //_lbl_total_customer.Text = Resources.ResourceManager.GetString("a0202");
            //_lbl_total_clients.Text = Resources.ResourceManager.GetString("a0201");

            _lbl_total_sale.Text = Resources.ResourceManager.GetString("a0504");




            item_datagrid.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0101");
            item_datagrid.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0012");
            item_datagrid.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0022");
            item_datagrid.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0401");
            item_datagrid.Columns[4].HeaderText = Resources.ResourceManager.GetString("a1037");

            item_datagrid.Columns[5].HeaderText = Resources.ResourceManager.GetString("a1053");
            item_datagrid.Columns[6].HeaderText = Resources.ResourceManager.GetString("a2015");
            item_datagrid.Columns[7].HeaderText = Resources.ResourceManager.GetString("a2016");



            
            _lbl_rquantity.Text= Resources.ResourceManager.GetString("a2017");
            _lbl_total_amount.Text= Resources.ResourceManager.GetString("a0503");
            _lbl_grandtotal.Text= Resources.ResourceManager.GetString("a0512"); 
            _lbl_total_comlga.Text= Resources.ResourceManager.GetString("a1026");



        }
        private void updateCustomerGridLocal()
        {
            //grid_bipari.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0201");
            //grid_bipari.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0012");
            //grid_bipari.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0512");
        }
        public void refreshUI(List<Landlord> tclients)
        {
            if (tclients==null)
            {
                return;
            }
            Admin.GetInstance.clients.Clear();
            Admin.GetInstance.clients = tclients;

            foreach (Landlord landlord in tclients)
            {
                addSalesRowinGrid(landlord,false);

            }



        }
        private Landlord searchLandlord(Landlord temp)
        {
            return null;
        }
        public Landlord searchbill(string bill_id, string bipariid)
        {
            item_datagrid.Rows.Clear();
            item_datagrid.Refresh();
            Landlord t_client = null;
            if (Admin.GetInstance.clients.Count > 0)
            {
                ll_index = Admin.GetInstance.clients.FindIndex(cl => cl.land_person.pkey == bill_id);
                t_client = Admin.GetInstance.clients[ll_index];

            }
            return t_client;
        }
        private void addRowingrid_bipari(Customer cust)
        {
            int count = this.item_datagrid.Rows.Count;
            if (count == 0)
            {
                count = 1;
            }
            else
            {
                count = count + 1;
            }
            this.item_datagrid.Rows.Add();
            this.item_datagrid.Rows[count - 1].Cells[1].Value = cust.customer_profile.pid;
            this.item_datagrid.Rows[count - 1].Cells[2].Value = cust.customer_profile.pname;
            this.item_datagrid.Rows[count - 1].Cells[3].Value = cust.sale._sale_quantity;
            this.item_datagrid.Rows[count - 1].Cells[4].Value = cust.sale._sale_amount;
            this.item_datagrid.Rows[count - 1].Cells[5].Value = cust.sale.getTotalSale()
                //+cust.sale.getTotalExtraAmountLandlord()
                ;
            this.item_datagrid.Rows[count - 1].Cells[6].Value = cust.sale.getTotalExtraAmountCustomer();
            this.item_datagrid.Rows[count - 1].Cells[7].Value = cust.sale.getTotalExtraAmountLandlord();
            this.item_datagrid.Rows[count - 1].Cells[10].Value = cust.cust_bill_id;
            this.item_datagrid.Rows[count - 1].Cells[11].Value = cust.status;

        }
        private void addSalesRowinGrid(Landlord land,bool check)
        {
            /*if (land.customers.Count == 0)
            {
                return;
            }*/
            if (land == null)
                return;
            int total_quantity = 0, total_sale_amount = 0,
                total_bill_amount = 0, client_services = 0;
            float total_commission_chongi = 0;

            string billid = land.record_id;
            string bill_key = land.land_person.pkey;
            string date = land.date;
            string billname = land.land_person.pname;
            int totalChalan = land.customers.Count();
            
            total_commission_chongi = (land.GetChongi + land.GetCommission);
            client_services = land.expense.total_munshiana +land.expense.total_marketfee+
                land.expense.total_rent +
                land.expense.total_labour +
                land.land_person.advance;

            string customernames = "";
            for (int i = 0; i < land.customers.Count(); i++)
            {
                Customer customer = land.customers[i];
                //customer.landloard = land;
                customernames += customer.customer_profile.pname + ", ";
                total_quantity += customer.sale._sale_quantity;
                total_sale_amount += (int)customer.sale._TotalSaleAmount 
                    + customer.sale._TotalExtraAmountLandlord;
                total_bill_amount += (int)customer.GrandTotalLandlord;
                


            }
            total_sale_amount = land.GetTotalSaleLandLord;
            total_bill_amount = total_sale_amount - (client_services + (int)total_commission_chongi);



            v_lbl_total_comlaga.Text = "" + (int)total_commission_chongi;
            v_lbl_total_munshiana.Text = "" + land.expense.total_munshiana;
            v_lbl_total_marketfee.Text = "" + land.expense.total_marketfee;
            v_lbl_total_labour.Text = "" + land.expense.total_labour;
            v_lbl_total_rent.Text = "" + land.expense.total_rent;
            v_lbl_total_expense.Text = "" + land.expense.total_expense;
            v_lbl_total_advance.Text = "" + land.land_person.advance;
            v_lbl_total_sale.Text = "" + total_sale_amount;
            v_lbl_total_expense.Text = ""+(client_services + (int)total_commission_chongi);

            total_amountsum.Text = "" + total_sale_amount;
            lbl_expenset.Text = "" + (client_services + (int)total_commission_chongi);
            grand_total.Text = "" + total_bill_amount;


            #region Commented
            /*if (count == 0)
            {
                count = 1;
            }
            else
            {
                count += 1;
            }
            this.detail_datagrid.Rows.Add();

            this.detail_datagrid.Rows[count - 1].Cells[1].Value = billname;
            this.detail_datagrid.Rows[count - 1].Cells[2].Value = customernames;
            this.detail_datagrid.Rows[count - 1].Cells[3].Value = totalChalan;
            this.detail_datagrid.Rows[count - 1].Cells[4].Value = client_services;
            this.detail_datagrid.Rows[count - 1].Cells[5].Value = total_quantity;
            this.detail_datagrid.Rows[count - 1].Cells[6].Value = total_sale_amount;
            this.detail_datagrid.Rows[count - 1].Cells[7].Value = total_bill_amount;
            this.detail_datagrid.Rows[count - 1].Cells[8].Value = 0;//Remaining Amount
            //this.detail_datagrid.Rows[count - 1].Cells[9].Value = 0;//Remaining Amount
            this.detail_datagrid.Rows[count - 1].Cells[10].Value = bill_key;
            this.detail_datagrid.Rows[count - 1].Cells[11].Value = date;*/
            #endregion


        }
       


        private void SalesAdd_Load(object sender, EventArgs e)
        {
            //init();
            adminlog = LogUtill.getAdminInputLog();
        }
        private void init()
        {
            this.bal = new BLogic();
            saleParser = new SaleParser(date, Admin.SaveLog);
            if (Authentication.Account.local=="1")
            {
                saleParser.SAVELOG = true;

            }
            change_AddTOUpdate(false);
            //DisplayData("");
            readDailySale(date,"");
        }

        #region Refresh

        bool localRecord = true;
        public void readDailySale(string date,string text)
        {
            List<Landlord> tclients = bal.getLandlordsList(date, text);//Test Comment
            if (tclients.Count>0)
            {
                localRecord = false;

                lbl_status.Text = "Live";
                lbl_status.BackColor = Color.YellowGreen;
                //grid_bipari.Columns[0].Visible=false;
                //detail_datagrid.Columns[0].Visible=false;
                //detail_datagrid.Columns[1].Visible=false;
            }
            else
            {
                lbl_status.Text = "Not Live";
                lbl_status.BackColor = Color.DarkOrange;
                //grid_bipari.Columns[0].Visible = false;
                if (Authentication.Account.local=="1")
                {
                    localRecord = true;
                    tclients= saleParser.LoadTodaySale();
                        /*List<Landlord> landList = saleParser.LoadTodaySale();
                                if (landList!=null)
                                {
                                    if (landList.Count > 0)
                                    {
                                        if (tclients.Count > 0)
                                        {
                                            tclients.Clear();
                                            //merge both files and load sales
                                            tclients = saleParser.mergeBothFiles().data;
                                        }
                                        else
                                        {
                                            tclients = landList;
                                        }
                                    }
                                }*/
                }
            }
            refreshUI(tclients);
            //lbl_quantity.Text = "" + Admin.GetInstance.getTotalRQuantity();
        }
        #endregion

       

        #region SelectRow Grid Movement by row
        private void selectCellValue(DataGridView grid)
        {
            int index = -1;
            try
            {
                check_TextBox = false;
                index = grid.SelectedCells[0].OwningRow.Index;// get the Row Index
            }
            catch (ArgumentOutOfRangeException ex)
            {

                check_TextBox = true;

                return;
            }
            if (index > -1)
            {
                check_TextBox = false;
                showDatainFields(index);
            }
        }

        public void showDatainFields(int index)
        {
            /*int ID = 0;
            try
            {
                ID = Convert.ToInt32(this.grid_bipari.Rows[0].Cells[3].Value.ToString());
            }
            catch (NullReferenceException e)
            {
                return;
            }


            DataGridViewRow selectedRow = this.grid_bipari.Rows[index];
            if (shop_enum == EnumShop.Customer && txt_customerID.ContainsFocus)
            {
                string id = selectedRow.Cells[0].Value.ToString();
                txt_client_nameid.Text = selectedRow.Cells[2].Value.ToString();


            }*/


        }
        private void selectUpRow(MetroGrid grid)
        {
            MetroGrid dgv = grid;
            int totalRows = dgv.Rows.Count;
            if (totalRows > 0)
            {

                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
                grid.FirstDisplayedScrollingRowIndex = rowIndex - 1;
                currentrow--;
                if (currentrow < 0)
                {
                    currentrow = 0;
                }
                if (grid.Name== "detail_datagrid")
                {
                    gridRow--;
                    if (gridRow<0)
                    {
                        gridRow = 0;
                    }
                }

            }

        }
        private void selectDownRow(MetroGrid grid)
        {
            MetroGrid dgv = grid;
            int totalRows = dgv.Rows.Count;
            if (totalRows > 0)
            {

                //int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (currentrow == totalRows - 1)
                    return;
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[currentrow];
                dgv.ClearSelection();
                dgv.Rows[currentrow + 1].Cells[colIndex].Selected = true;
                grid.FirstDisplayedScrollingRowIndex = currentrow + 1;
                currentrow++;
                if (currentrow > totalRows)
                {
                    currentrow = totalRows-1;
                }

                if (grid.Name == "detail_datagrid")
                {
                    gridRow++;
                    if (gridRow > totalRows)
                    {
                        gridRow = totalRows-1;
                    }
                }
            }

        }
        #region Control Keys,Events
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Up:
                    

                    return true;
                case Keys.Down:
                    
                    return true;
                case Keys.Delete:
                    return true;
                case Keys.F2:
                    return true;
                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.F5:
                    btn_refresh_Click(this, new EventArgs());
                    return true;
                case Keys.Control | Keys.G:
                    //Stuff
                    return true;
                    case Keys.Control | Keys.N:
                    btn_addstock_Click(this,new EventArgs());
                    return true;
                case Keys.Enter:

                    try
                    {
                        grid_landload_CellClick(this, new DataGridViewCellEventArgs(0, currentrow));
                        if (btn_calculate.ContainsFocus)
                        {
                            btn_calculate_Click(this, new EventArgs());
                        }

                        if (templandlord.client._product.sale_remaining_product >= 0)
                        {
                            //if (!grid_bipari.ContainsFocus)
                            {
                                changetxtBoxFocus();
                            }
                            //else
                            //{ txt_customerID.Select(); }
                            //txt_customerID.Select();
                        }
                        else
                        {
                            btnAddCalculate.Select();
                        }

                        if (txt_begamount.ContainsFocus || txt_begamount.ContainsFocus)
                        {
                            int rsale = int.Parse(lbl_remaining_sale.Text == "" ? "0" : lbl_remaining_sale.Text);
                            if (rsale==0)
                            {
                                return true;

                                //txt_userid.Select();
                                //MessageBox.Show("No Remaining Product...");
                            }

                        }

                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }




                    return true;
                case Keys.Control | Keys.P:

                    SaleDetail sd = new SaleDetail(Admin.GetInstance.clients[currentrow]);
                    sd.ShowDialog();
                    return true;
                case Keys.Control | Keys.Enter:
                    //btn_Add.colorActive = Color.MediumSeaGreen;
                    btnAddCalculate_Click(this, new EventArgs());
                    //calculateTotal_UIData();
                    shop_enum = EnumShop.Sale;

                    return true;
                case Keys.Alt | Keys.Enter:
                   // btn_add_customer_Click(this, new EventArgs());

                    return true;


            }



            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void deleteCurrentRow()
        {
            throw new NotImplementedException();
        }

        public void updateUIData(Landlord temp)
        {
            int total_quantity = 0, total_sale_amount = 0,
               total_bill_amount = 0, client_services = 0;
            float total_commission_chongi = 0;
            lbl_totalitems.Text = "" + temp.land_product.total_Quantity;
            remaining_quantity = temp.land_product.total_Quantity;


            total_commission_chongi = (temp.GetChongi + temp.GetCommission);
            client_services = temp.expense.total_munshiana +temp.expense.total_marketfee+
                temp.expense.total_rent +
                temp.expense.total_labour +
                temp.land_person.advance;
            if (temp.land_product.sale_remaining_product>0)
            {
               // change_AddTOUpdate(true);
            }
            showCustomerSale(templandlord);

           
            if (temp.bill_type == "B")
            {
                txt_bikri_rate.Text = "" + temp.bikri_rate;
                txt_bikri_quantity.Text = "" + temp.bikri_quantity;
                lbl_total_bikri.Text = "" + temp.total_bikri;
                panel_bikri.Enabled = true;
                chk_bikri.Checked = true;
                total_sale_amount = temp.total_bikri;
                total_bill_amount = total_sale_amount - (client_services + (int)total_commission_chongi);
                total_amountsum.Text = "" + total_sale_amount;
            }
            else
            {
                total_sale_amount = temp.GetTotalSaleLandLord;
                total_bill_amount = total_sale_amount - (client_services + (int)total_commission_chongi);
                total_amountsum.Text = "" + total_sale_amount;

            }
             v_lbl_total_comlaga.Text = "" + (int)total_commission_chongi;
            v_lbl_total_munshiana.Text = "" + temp.expense.total_munshiana;
            v_lbl_total_marketfee.Text = "" + temp.expense.total_marketfee;
            v_lbl_total_labour.Text = "" + temp.expense.total_labour;
            v_lbl_total_rent.Text = "" + temp.expense.total_rent;
            v_lbl_total_expense.Text = "" + temp.expense.total_expense;
            v_lbl_total_advance.Text = "" + temp.land_person.advance;
            v_lbl_total_sale.Text = "" + total_sale_amount;
            v_lbl_total_expense.Text = "" + (client_services + (int)total_commission_chongi);
            lbl_expenset.Text = "" + (client_services + (int)total_commission_chongi);

            grand_total.Text = "" + total_bill_amount;
        }
        



        #endregion

        #endregion

        #region Focus Change
        

        public void changetxtBoxFocus()
        {

            if (txt_customerID.ContainsFocus)
            {
                txt_nobegs.Select();
                txt_nobegs.SelectAll();
            }
            else if (txt_nobegs.ContainsFocus)
            {
                txt_begamount.Select();
                txt_begamount.SelectAll();
            }
            else if (txt_begamount.ContainsFocus)
            {
                //btn_calculate_Click(this, new EventArgs());
                //btn_calculate.Select();
                //btn_calculate.Focus();
                btn_calculate.selected = true;
                btn_calculate.Select();
            }
            else if (btn_calculate.ContainsFocus)
            {
                btn_calculate.selected = false;
                txt_customerID.Select();
            }
            else if (btnAddCalculate.ContainsFocus)
            {
            }
            currentrow = 0;
        }
        #endregion
        int pageindex = 1;
        int pageSize = 20;
        private void DisplayData(string search)
        {
            /*DataTable dtr = bal.searchRecords(date, "Sale", search);
            foreach (DataRow row in dtr.Rows)
            {
                addRowGridLandlord(row[0].ToString(), "", "0", "0", row[1].ToString(), "0", "0", row[5].ToString(), row[3].ToString(), row[2].ToString(), row[4].ToString(), "0");
            }*/
            //DataTable dt = bal.searchRecords(date, "Customer", search, pageindex, pageSize); ;
            //grid_bipari.DataSource = dt;
            //grid_bipari.Columns[0].AutoSizeMode=DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grid_bipari.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grid_bipari.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            
        }

        public void searchClientfortransportRent()
        {

            if (shop_enum == EnumShop.Customer && txt_customerID.ContainsFocus)
            {
                string search = txt_customerID.Text;
                
                //grid_bipari.DataSource = bal.searchRecords(date, "Customer", search, pageindex, pageSize);
            }
            else
            if (shop_enum == EnumShop.BegType && txt_begtype.ContainsFocus)
            {
                string search = txt_begtype.Text;

                //grid_bipari.DataSource = bal.searchRecords(date, "Customer", search, pageindex, pageSize);
            }
            else
            if (txt_client_nameid.ContainsFocus)
            {
                string search = txt_client_nameid.Text;

                //grid_bipari.DataSource = bal.searchRecords(date, "Customer", search, pageindex, pageSize);
            }

        }
        private void btn_calculate_Click(object sender, EventArgs e)
        {
            
            if (txt_client_nameid.Text=="")
            {
                
                return;
            }
            string bikri = "";
            //if (chk_bikri.Checked)
            //    bikri = "B";

            string customerid = lbl_custid.Text;
            string customer_name = txt_customerID.Text;
            int _sale_quantity = int.Parse(txt_nobegs.Text == "" ? "0" : txt_nobegs.Text);
            int _sale_amount = int.Parse(txt_begamount.Text == "" ? "0" : txt_begamount.Text);

            //int bikri_quantity = int.Parse(txt_bikri_quantity.Text == "" ? "0" : txt_bikri_quantity.Text);
            //int bikri_rate= int.Parse(txt_bikri_rate.Text == "" ? "0" : txt_bikri_rate.Text);


            remaining_quantity = int.Parse(lbl_remaining_sale.Text=="" ? "0" : lbl_remaining_sale.Text);
            if (_sale_quantity==0)
            {
                return;
            }

            if (remaining_quantity-_sale_quantity<0)
            {
                txt_nobegs.Text = remaining_quantity+"";
                txt_customerID.Focus();
                return;
            }
            else
            {
                remaining_quantity -= _sale_quantity;
            }


            //int total_sale = _sale_quantity * _sale_amount;

            if (templandlord.land_product.sale_remaining_product > 0)
            {
                //Customer Sale


                //Customer
                Person customer = new Person();
                customer.pid = customerid;
                customer.pname = customer_name;
                string searc = CheckCustomerIDExist(customerid);
                if (searc == "")
                {
                    string key = bal.checkCustSaleKeyExist(customerid, templandlord.date);
                    if (key == "")
                        customer.pkey = bal.p_getInvoiceID("Other", customerid, date);//Cus
                    else
                        customer.pkey = key;
                }
                else
                {
                    customer.pkey = searc;
                }

                //BillKey.getBillID(BillKey.EnumUser.Customer, date, customerid, 0);
                Product prCust = new Product(templandlord.land_product._product_id, templandlord.land_product._product_name,
                    templandlord.land_product._type, templandlord.land_product._weight_id, templandlord.land_product._weight, _sale_quantity, templandlord.land_product.marka);
                string ptype = txt_begtype.Text;
                if (ptype!="")
                {
                    prCust._type = txt_begtype.Text;
                }
                Sale msale = new Sale(_sale_quantity, _sale_amount);
                Customer cust = new Customer(date,templandlord.service, false, msale,templandlord.land_person);
                cust.tag_Action = "newinsert";
                cust.customer_profile = customer;
                cust.product = prCust;

                cust.total_quantity += _sale_quantity;




                #region Calculations
                /**
                 * 
                 * 1: Calculate Single sale for Customer.
                 * 2: Calculate Commission for Commission.
                 * 3: Calculate Chongi for Customer.
                 * 4: Remaining Sale Update on Client.
                 */
                // 1
                /*Sale msale = new Sale();
                msale._sale_quantity = _sale_quantity;
                msale._sale_amount = _sale_amount;
                msale.getTotalSale();*/
                //msale.add_extra_amount = _extra_amount;
                //msale.GetTotalSale = total_sale;
                cust.sale = msale;
                cust.updateTotal();

                /*
                //TotalSale client_total_sale = new TotalSale();
                // 2
                cust.getCommission();
                // 3
                cust.getChongi();
                
                float chongicommisison= cust.getCommission()+ cust.getChongi();
                cust.GetGrandTotalCustomer =(int) (cust.GetTotalSaleCustomer+chongicommisison);
                */
                // MessageBox.Show("ChongiCommission = "+chongicommisison);
                // 4
                //cust.landloard = templandlord;
                templandlord.bill_type = bikri;
                //templandlord.bikri_rate = bikri_rate;
                //templandlord.bikri_quantity = bikri_quantity;

                templandlord.total_quantity = templandlord.land_product.total_Quantity;
                templandlord.land_product.sale_remaining_product = templandlord.land_product.sale_remaining_product - _sale_quantity; //Error

                //if (chk_bikri.Checked)
                //    templandlord.total_bikri = bikri_rate * bikri_quantity;
                //else
                    templandlord.total_sale +=(int) cust.sale.getTotalSale()+cust.sale.getTotalExtraAmountLandlord();


                templandlord.getCommission();
                templandlord.getChongi();
                #endregion

                if (templandlord.land_product.sale_remaining_product>0)
                {
                    templandlord.status = EStatus.InComplete;
                }else 
                if(templandlord.land_product.sale_remaining_product==0)
                {
                    if (templandlord.status == EStatus.InComplete)
                    {
                        templandlord.status = EStatus.CompleteUpdate;
                    }
                    else
                    {
                        templandlord.status = EStatus.Complete;
                    }
                }
                lbl_remaining_sale.Text = "" + templandlord.land_product.sale_remaining_product;
                total_amountsum.Text = "" + cust.total_sale;
                templandlord.customers.Add(cust);
                templandlord.total_services = (int)(templandlord.GetTotalService+templandlord.GetChongi+templandlord.GetCommission);

               
                addRowingrid_bipari(cust);
                clear();
            }
            else
            {
                MessageBox.Show(templandlord.land_person.pname + " ,No remaining items 'CTRL + Enter'");
                clear();
            }
        }

        private string CheckCustomerIDExist(string customerid)
        {

            foreach (Landlord land in Admin.GetInstance.clients)
            {
                foreach (Customer cus in land.customers)
                {
                    if (cus.customer_profile.pid == customerid)
                    {
                        return cus.customer_profile.pkey;
                    }
                }
            }
            return "";
        }

        /**
             *  1: Insert One by One Record in Daily Sales
             *  2.0: Get Total Sales of Customers.
             *  2.1 After Insertion in daily table update customer credite amount
             *  2.2: Total Labour,Rent,Munshiana,Advance
             *  3: Seperate from total Expense
             *  4: Update Daily Expense Table and Update Acounts table(Capital Cash)
             *     4.1 Calculate All Client Bills add in expense and deduct these bills from Capital Cash
             *     4.2 Calculate Total Munshiana, Labour, Rent and Advance for Client.  
             * */
        private void btnAddCalculate_Click(object sender, EventArgs e)
        {
            if (templandlord == null)
            {
                return;
            }
            int rq=(lbl_remaining_sale.Text=="" || lbl_remaining_sale.Text == "0")?0:int.Parse(lbl_remaining_sale.Text);
            int rtitems = (lbl_totalitems.Text == "" || lbl_totalitems.Text == "0") ? 0 : int.Parse(lbl_totalitems.Text);

            if (rq==rtitems)
            {
                return;
            }
            Account acc = Authentication.Account;
            templandlord.UpdateTotal();
            templandlord.category = nameof(BillKey.EnumUser.Client);

            //  return;
            string statuslive = lbl_status.Text;
            if (acc.local == "0" || statuslive=="Live")
            {

                addSales(templandlord);

            }
            else if (acc.local == "1")
            {
                addLocalandDBSale(templandlord);
            }

        }

        private void addSales(Landlord landlord)
        {
            // Landlord temdata = null;
            string statuslive = lbl_status.Text;
            //return;

            if (landlord.status==EStatus.CompleteUpdate)
            {
                //Refresh Sale Data of landlord
                int tchk = 1;

                for (int i=0;i< item_datagrid.Rows.Count; i++)
                {
                    string status = item_datagrid.Rows[i].Cells[11].Value.ToString();
                    if (status == "0")
                    {
                        //InsertRecord


                        Customer customer = templandlord.customers[i];
                        /*bool chk = new BLogic().updateCusomerAmountandBalanceShet(templandlord.date,
                         templandlord.land_person.pkey, customer.customer_profile.pkey,
                       customer.customer_profile.pid, tchk, customer.cust_bill_id);*/

                        string[] temp = new BLogic().p_singlesaleadd(templandlord, customer);
                        tchk = 0;

                    }
                }

                if (tchk == 0 || tchk == 1)
                {
                    item_datagrid.Rows.Clear();
                    item_datagrid.Refresh();
                    refresh();
                    return;
                }
                /*
                for (int i= 0; i< oldData;i++)
                {
                    Customer customer = templandlord.customers[i];
                    //bool chk = bal.checkIfSaleExist();

                    //return;
                    bool chk = new BLogic().updateCusomerAmountandBalanceShet(templandlord.date, 
                        templandlord.land_person.pkey,customer.customer_profile.pkey, 
                        customer.customer_profile.pid, tchk,customer.cust_bill_id);
                    tchk = 0;
                }*/
            }
            //return;
            bool check = new BLogic().insertCustomerSale(landlord, cust_index);


            if (check)
            {
                item_datagrid.Rows.Clear();
                item_datagrid.Refresh();
            }

            if (check)
            {
                //when client receive amount then add bill amount in expense and balance sheet
                new BLogic().addExtraAmountClient("updateClientAmount", landlord.date, landlord.land_person.pid,
                    (int)landlord.GetGrandTotal,landlord.land_person.pkey,landlord.land_person.pname,0,"19");
                //bal.addExpense_IUSales(landlord.date, landlord.land_person.pkey,landlord.land_person.pname, (int)landlord.GetGrandTotal,nameof(BillKey.EnumUser.PaymentSale));
                //bal.addBalanceSheet("credit", 0, landlord,nameof(BillKey.EnumUser.Client), "insert", landlord.land_person.pkey,"");
                refresh();

            }
        }
        public void refresh()
        {
            refreshSalesData();
            readDailySale(date, "");
            ToastNotification.Show(this, "Sale Record Inserted.");
            lbl_status.Text = "Sale Record Inserted.";
        }
        private void addLocalandDBSale(Landlord temp)
        {
            if(temp.customers!=null)
            {
                bool check = false;
                Admin.GetInstance.clients[ll_index] = temp;
                try
                {
                    if (saleParser.SAVELOG)
                    {
                        if(temp.land_product.sale_remaining_product==0)
                            temp.status = EStatus.Complete;
                        else
                        if(temp.land_product.sale_remaining_product > 0)
                            temp.status = EStatus.InComplete;

                        check = saleParser.updateLandLord(temp);
                    }
                    else
                    {
                        check = bal.insertCustomerSale(temp, cust_index);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (check)
                {
                    item_datagrid.Rows.Clear();
                    item_datagrid.Refresh();
                    total_amountsum.Text = "" + temp.total_sale;
                    lbl_expenset.Text = "" + (temp.GetTotalService+temp.GetChongi+temp.GetCommission);
                    grand_total.Text = "" + temp.GetGrandTotal;
                    refreshSalesData();
                    readDailySale(date, "");
                    ToastNotification.Show(this, "Sale Record Inserted.");
                    lbl_status.Text = "Sale Record Inserted.";
                }
            }
            
        }
        public void refreshSalesData()
        {
            addSalesRowinGrid(templandlord, true);
            //DisplayData("");
            lbl_status.Text = "";
            //changetxtBoxFocus();
            templandlord = null;
            shop_enum = EnumShop.Sale;
            //templandlord = null;
            //clearAll();
            if (check_data_action)
            {
                change_AddTOUpdate(false);
                
            }
        }


        private void clear()
        {
            txt_customerID.Clear();
            txt_nobegs.Clear();
            txt_begamount.Clear();
        }


        private void clearAll()
        {
            txt_client_nameid.Clear();
            txt_landloard_nameid.Clear();
            ll_index = 0;
            clear();
        }


        private void txt_userid_TextChanged(object sender, EventArgs e)
        {
            shop_enum = EnumShop.Sale;
            searchClientfortransportRent();
        }

        private void grid_bipari_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (this.grid_bipari.Visible == true)
            {
                if (shop_enum == EnumShop.Sale && txt_userid.Focused)
                {
                    if (e.KeyCode.Equals(Keys.Up) && txt_userid.Focused)
                    {
                        selectUpRow(this.grid_bipari);
                    }
                    if (e.KeyCode.Equals(Keys.Down) && txt_userid.Focused)
                    {
                        selectDownRow(this.grid_bipari);
                    }
                    if (e.KeyData == Keys.Enter && txt_userid.Focused)
                    {
                        selectCellValue(this.grid_bipari);
                    }


                }

            

            }
            else if (this.grid_bipari.Visible == true)
            {
                if (shop_enum == EnumShop.Customer && txt_customerID.Focused)
                {
                    if (e.KeyCode.Equals(Keys.Up) && txt_customerID.Focused)
                    {
                        selectUpRow(this.grid_bipari);
                    }
                    if (e.KeyCode.Equals(Keys.Down) && txt_customerID.Focused)
                    {
                        selectDownRow(this.grid_bipari);
                    }
                    if (e.KeyData == Keys.Enter && txt_customerID.Focused)
                    {
                        selectCellValue(this.grid_bipari);
                    }


                }

            }*/

            e.Handled = true;



        }




        private void txt_customerID_TextChanged(object sender, EventArgs e)
        {
            shop_enum = EnumShop.Customer;
            searchClientfortransportRent();
        }

        private void btn_add_customer_Click1(object sender, EventArgs e)
        {
            string txt = "";
            int id = 0;
            if (shop_enum == EnumShop.Customer && txt_customerID.ContainsFocus)
            {
                txt = txt_customerID.Text;
                id = bal.insertDataCPW(2, txt);
                if (id != 0)
                {

                    searchClientfortransportRent();
                    lbl_custid.Text = "" + id;

                    ToastNotification.Show(this, id + " " + txt + " " + Resources.added_in_database);
                }

            }

        }



        public void showCustomerSale(Landlord landlord)
        {
            foreach (Customer cust in landlord.customers)
            {
                addRowingrid_bipari(cust);
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            /*item_datagrid.Rows.Clear();
            item_datagrid.Refresh();
            init();*/
        }


        #region Edit Customer Sales
        bool updateCustRec = false;

        
        private void updateCustomerSaleView(Customer customer)
        {

            change_AddTOUpdate(true);
            lbl_custid.Text = customer.customer_profile.pid;
            txt_customerID.Text = customer.customer_profile.pname;
            txt_nobegs.Text = customer.sale._sale_quantity+"";
            txt_begamount.Text = customer.sale._sale_amount+"";
            
            
        }

        #endregion


        private void item_datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;

            if (!localRecord)
            {
                //return;
            }
            /*if (e.ColumnIndex == 2)
            {
                using (Search search = new Search(6, item_datagrid.Rows[index].Cells[2].Value.ToString()))
                {
                    DialogResult res = search.ShowDialog();
                    item_datagrid.Rows[index].Cells[1].Value= search.Id;
                    item_datagrid.Rows[index].Cells[2].Value=search.Name;
                    search.Close();

                    return;
                }
            }
            else*/ if (e.ColumnIndex == 0)
            {
                if (Authentication.Account.local == "0" || lbl_status.Text == "Live")
                {
                    string id = item_datagrid.Rows[index].Cells[1].Value.ToString();
                    Customer cust = templandlord.customers[index];
                    templandlord.land_product.sale_remaining_product += cust.sale._sale_quantity;
                    lbl_remaining_sale.Text = "" + templandlord.land_product.sale_remaining_product;
                    templandlord.total_sale -= (int)cust.sale.getTotalSale()
                        //+ cust.sale.getTotalExtraAmountLandlord()
                        ;
                    string _date = this.date;
                    string _landkey = tlbl_khata_id.Text;
                    string custid = id;
                    int delquantity = int.Parse(item_datagrid.Rows[index].Cells[3].Value.ToString());
                    int delrate = int.Parse(item_datagrid.Rows[index].Cells[4].Value.ToString());
                    int totsale = int.Parse(item_datagrid.Rows[index].Cells[5].Value.ToString());
                    string status = item_datagrid.Rows[index].Cells[11].Value.ToString();
                    string customerpurchaseid = item_datagrid.Rows[index].Cells[10].Value.ToString();

                    

                    #region UpdateDB
                    //bal.changeSaleDelete(date,landkey, cust.cust_bill_id, custid,delquantity,delrate,totsale);
                    if (bal.customersaleDelete(_date, _landkey, custid, cust.customer_profile.pkey, cust.cust_bill_id))
                    {
                        item_datagrid.Rows.RemoveAt(index);
                        templandlord.customers.RemoveAt(index);
                        updateCustomerSaleView(cust);
                        txt_customerID.Focus();
                        initRefresh(txt_client_nameid.Text, txt_landloard_nameid.Text, tlbl_khata_id.Text, lbl_remaining_sale.Text, this.cl_id, this.billid);

                    }
                    //oldData = templandlord.customers.Count;
                    
                    #endregion



                }
                else
                {

                    if (saleParser.DeleteCustomer(tlbl_khata_id.Text, index))
                    {
                        //item_datagrid.Rows.RemoveAt(index);
                        //templandlord.customers.RemoveAt(index);
                        string id = item_datagrid.Rows[index].Cells[1].Value.ToString();
                        Customer cust = templandlord.customers[index];
                        templandlord.land_product.sale_remaining_product += cust.sale._sale_quantity;
                        lbl_remaining_sale.Text = "" + templandlord.land_product.sale_remaining_product;
                        templandlord.total_sale -= (int)cust.sale.getTotalSale()
                            //+ cust.sale.getTotalExtraAmountLandlord()
                            ;


                        item_datagrid.Rows.RemoveAt(index);
                        templandlord.customers.RemoveAt(index);
                        updateCustomerSaleView(cust);


                    }
                }

            }else if(e.ColumnIndex == 8)
            {
                AddExtraAmount extra = new AddExtraAmount(templandlord, templandlord.customers[index], index, lbl_status.Text);
                int ex_amountLandlor=extra.getCustomer().sale.add_extra_amount_Landlord;
                int ex_amountCustomer = extra.getCustomer().sale.add_extra_amount_Customer;
                extra.ShowDialog();
                if (ex_amountLandlor != extra.getCustomer().sale.add_extra_amount_Landlord)
                {
                    Customer cust= extra.getCustomer();
                    templandlord.customers[index] = cust;
                    templandlord.total_sale = (int)templandlord.customers.Sum(x => x.sale._TotalSaleAmount);
                    item_datagrid.Rows.Clear();
                    item_datagrid.Refresh();
                    bal.updateExtraAmount(templandlord, templandlord.customers[index],"Client");
                    

                    showCustomerSale(templandlord);
                    addSalesRowinGrid(templandlord, true);

                }else if (ex_amountCustomer != extra.getCustomer().sale.add_extra_amount_Customer)
                {
                    Customer cust = extra.getCustomer();
                    templandlord.customers[index] = cust;
                    item_datagrid.Rows.Clear();
                    item_datagrid.Refresh();
                    templandlord.total_sale = 
                        (int)templandlord.customers.Sum(x => x.sale._TotalSaleAmount);
                    
                    bal.updateExtraAmount(templandlord, templandlord.customers[index], "Customer");
                    showCustomerSale(templandlord);
                    addSalesRowinGrid(templandlord, true);
                }
            }
            else if(e.ColumnIndex==9)//Changename
            {
                string id = item_datagrid.Rows[index].Cells[1].Value.ToString();
                Customer cust = templandlord.customers[index];
                templandlord.land_product.sale_remaining_product += cust.sale._sale_quantity;
                lbl_remaining_sale.Text = "" + templandlord.land_product.sale_remaining_product;
                templandlord.total_sale -= (int)cust.sale.getTotalSale()
                    //+ cust.sale.getTotalExtraAmountLandlord()
                    ;
                string _date = this.date;
                string _landkey = tlbl_khata_id.Text;
                string custid = id;
                if (lbl_status.Text == "Live")
                {
                    SalesChangeCustomer sc = new SalesChangeCustomer(date, cust);
                    sc.ShowDialog();
                }
            }
            if (e.ColumnIndex == 0 && false)// && false from its condition
            {

                DeleteCustomerSale(index);

                change_AddTOUpdate(true);

            }
            index = 0;


        }

        private void DeleteCustomerSale(int index)
        {
            string id = item_datagrid.Rows[index].Cells[1].Value.ToString();
            Customer cust = templandlord.customers[index];
            item_datagrid.Rows.RemoveAt(index);


            if (selected_detail_datagrid_index >= 0)
            {
                for (int i = 0; i < templandlord.customers.Count; i++)
                {
                    Customer temp = templandlord.customers[i];
                    bal.p_ud_cust_sale_product(templandlord.land_person.pkey, templandlord,templandlord.category);

                }
                int land_index = 0;
                templandlord.total_sale -= (int)cust.sale.getTotalSale() + cust.sale.getTotalExtraAmountLandlord();
                templandlord.land_product.sale_remaining_product += (int)cust.sale._sale_quantity;
                templandlord.customers.RemoveAt(index);

                selected_detail_datagrid_index = -1;
                txt_customerID.Select();
            }
        }

        private void grid_landload_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //bool chk = checkData();
            DataGridViewRow selectedRowCustomer = null;
            DataGridViewRow selectedRowBipari = null;
            int row = currentrow;
            string id = "";
            string name = "", bill_key = "";
            string remainingitems = "";
            currentrow = e.RowIndex;
            if (e.RowIndex < 0)
                return;

            

            
            if (txt_begtype.ContainsFocus)
            {
                btn_seach_beg_Click(this,new EventArgs());
            }    
            else
            if ((txt_customerID.ContainsFocus))
            {
                btn_search_cust_Click(this,new EventArgs());
            }
            else
            if ((txt_client_nameid.ContainsFocus))
            {
                btn_clientsearch_Click(this, new EventArgs());
            }









        }
        int oldData=0, oldAmount=0;
        public void change_AddTOUpdate(bool check)
        {
            if (check)
            {
                btnAddCalculate.Text = "Save";
                btn_calculate.BackColor = MetroFramework.MetroColors.Yellow;
                btn_calculate.Text = "Update";
                check_data_action = check;
                
            }
            else
            {
                btn_calculate.BackColor = MetroFramework.MetroColors.Green;
                btnAddCalculate.Text = "Add/Calculate";
                btn_calculate.Text = "Add Item";
                check_data_action = check;
            }
        }


        Control cntObject;

        // Select DataGridView EditingControlShowing Event
        private void datagridview_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.TextChanged += new EventHandler(textBox_TextChanged);
            cntObject = e.Control;
            cntObject.TextChanged += textBox_TextChanged;
        }

        // TextBox TextChanged Event
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (cntObject.Text != string.Empty)
            {
                var currentcell = item_datagrid.CurrentCellAddress;
                var sendingCB = sender as DataGridViewComboBoxEditingControl;
                DataGridViewTextBoxCell begs = (DataGridViewTextBoxCell)item_datagrid.Rows[currentcell.Y].Cells[3];
                begs.Value = sendingCB.EditingControlFormattedValue.ToString();
                string b = sendingCB.EditingControlFormattedValue.ToString();

                DataGridViewTextBoxCell rate = (DataGridViewTextBoxCell)item_datagrid.Rows[currentcell.Y].Cells[4];
                rate.Value = sendingCB.EditingControlFormattedValue.ToString();
                string r = sendingCB.EditingControlFormattedValue.ToString();

                DataGridViewTextBoxCell total = (DataGridViewTextBoxCell)item_datagrid.Rows[currentcell.Y].Cells[5];
                rate.Value = sendingCB.EditingControlFormattedValue.ToString();
                sendingCB.EditingControlFormattedValue= int.Parse(b.ToString()) * int.Parse(r.ToString());



            }
        }
        

        private void txt_begamount_TextChanged(object sender, EventArgs e)
        {

            calculateAmount();
        }
        private void txt_nobegs_TextChanged(object sender, EventArgs e)
        {
            calculateAmount();
        }
        private void calculateAmount()
        {
            string beg = txt_nobegs.Text;
            string amount = txt_begamount.Text;
            if (amount == "")
            {
                return;
            }
            if (beg == "")
            {
                return;
            }
            int _beg = int.Parse(beg == "" ? "0" : beg);
            int _amount = int.Parse(amount == "" ? "0" : amount);
            lbl_total.Text = "" + _beg * _amount;
        }

        private void btn_search_cust_Click(object sender, EventArgs e)
        {
            using (Search search = new Search(6, txt_customerID.Text))
            {
                DialogResult res = search.ShowDialog();
                lbl_custid.Text = search.Id;
                txt_customerID.Text = search.Name;
                search.Close();

                return;
            }
        }

        private void btn_addstock_Click(object sender, EventArgs e)
        {
            using (VendorForm vend = new VendorForm(date,1,lbl_status.Text))
            {
                DialogResult res = vend.ShowDialog();
                vend.Close();
                init();

                return;
            }
        }

        
        private void txt_begtype_TextChanged(object sender, EventArgs e)
        {
            shop_enum = EnumShop.BegType;
            searchClientfortransportRent();
        }

        private void txt_client_nameid_TextChanged(object sender, EventArgs e)
        {
            searchClientfortransportRent();
        }

        private void btn_clientsearch_Click(object sender, EventArgs e)
        {
            using (Search search = new Search(1, txt_client_nameid.Text, 1))
            {
                DialogResult res = search.ShowDialog();
                txt_client_nameid.Text = search.Name;
                string statuslive = lbl_status.Text;
                if (statuslive == "Live")
                {
                    bal.p_changeLandlordName(date, tlbl_khata_id.Text, search.Id);
                }
                else
                {
                    Landlord newTemp = templandlord;
                    newTemp.land_person.pid = search.Id;
                    newTemp.land_person.pname = search.Name;
                    saleParser.updateLandLord(templandlord, newTemp);
                }
                search.Close();

                return;
            }
        }

        private void chk_bikri_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_bikri.Checked)
                panel_bikri.Enabled = true;
            else
                panel_bikri.Enabled = false;

        }

        private void btn_seach_beg_Click(object sender, EventArgs e)
        {
            using (Search search = new Search(4, txt_customerID.Text))
            {
                DialogResult res = search.ShowDialog();
                txt_begtype.Text = search.Name;
                
                search.Close();

                return;
            }
        }

        private void btn_cust_reasses_id_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Admin.GetInstance.clients.Count; i++)
            {
                Landlord land = Admin.GetInstance.clients[i];
                for (int j = 0; j < land.customers.Count; j++)
                {
                    land.customers[j].customer_profile.pkey = BillKey.getBillID(BillKey.EnumUser.Customer, date, land.customers[j].customer_profile.pid, 0);

                }
                bool check = saleParser.updateLandLord(land);
                if (!check)
                {
                    MessageBox.Show("Unable To Update Customer Keys");
                    break;
                }
            }
        }
    }
}
