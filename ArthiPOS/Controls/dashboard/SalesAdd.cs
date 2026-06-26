using ArthiPOS.controls.dashboard;
using ArthiPOS.Properties;
using ArthiPOS.Reporting.ReportDataSet;
using ArthiPOS.Utill;
using BAL;
using CommonUtilities;
using DataMember;
using DataMember.memberlog;
using DevExpress.XtraPrinting.Native;
using Google.Apis.Drive.v3.Data;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SaleDetail = ArthiPOS.controls.dashboard.SaleDetail;

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
        private List<Landlord> landlist;
        public SalesAdd(string date, string userid, string bipariname, string landlordname,
            string bill_key, string remainingitems, string cl_id, string billid, string status, List<Landlord> landlist)
        {
            InitializeComponent();

            item_datagrid.RowTemplate.Height = 30;
            item_datagrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            item_datagrid.DefaultCellStyle.Font = new Font("Arial", 18);
            item_datagrid.Columns[2].DefaultCellStyle.Font = new Font("Arial", 18, FontStyle.Bold); // Replace 0 with the index of your target column.
           // loadExpense(0, 0, 0, 0, 0, 0, 0, 0);


            this.date = date;
            localization();
            string search = userid;
            shop_enum = EnumShop.Customer;
            bal = new BLogic();
            lbl_status.Text = status;
            saleParser = new SaleParser(date, Admin.SaveLog, Authentication.Account.local == "0" ? false : true);
            this.landlist = landlist;
            if (Authentication.Account.local == "1")
            {
                saleParser.SAVELOG = true;

            }
            change_AddTOUpdate(false);
            this.cl_id = cl_id;
            this.billid = billid;
            lbl_head_date.Text = date;
            initRefresh(bipariname, landlordname, bill_key, remainingitems, cl_id, billid,"");
            changeColor(Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White, Color.White, Color.White, Color.White);
            txt_customerID.Focus();

        }
        private string cl_id = "", billid = "";
        private void initRefresh(string bipariname, string landlordname,
            string bill_key, string remainingitems, string cl_id, string billid,string bilti_vehicle)
        {
             List<Landlord> tclients = bal.getLandlordsList(date, "");
           
            if (string.IsNullOrEmpty(billid) && string.IsNullOrEmpty(bilti_vehicle))
            {
                return;
            }
            

            foreach (Landlord landlord in tclients)
            {
                
                if (billid == landlord.bill_key || bilti_vehicle==landlord.client._vehicle_id)
                    addSalesRowinGrid(landlord, false);
                
            }


            {

                lbl_remaining_sale.Text = remainingitems;
                tlbl_khata_id.Text = bill_key;
                txt_client_nameid.Text = bipariname;
                txt_landloard_nameid.Text = landlordname;

                // Search Landlord
                templandlord = searchbill(billid, cl_id);
                lbl_bipari_id.Text = templandlord.client._person_cl.pkey;

                cust_index = templandlord.customers.Count;
                templandlord.tag_Action = "insert";
                updateUIData(templandlord);
                if (templandlord.status == EStatus.InComplete)
                {
                    oldData = templandlord.customers.Count;
                    oldAmount = (int)templandlord.GetGrandTotal;
                }
            }
            showTotal((int)templandlord.GetTotalService + (int)templandlord.GetChongi + (int)templandlord.GetCommission, templandlord.GetTotalSaleLandLord, (int)templandlord.GetGrandTotal);

        }

        public void localization()
        {
            //_lbl_khata.Text = Resources.ResourceManager.GetString("a0013");
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





            item_datagrid.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0101");
            item_datagrid.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0012");
            item_datagrid.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0022");
            item_datagrid.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0401");
            item_datagrid.Columns[4].HeaderText = Resources.ResourceManager.GetString("a1037");

            item_datagrid.Columns[5].HeaderText = Resources.ResourceManager.GetString("a1053");
            item_datagrid.Columns[6].HeaderText = Resources.ResourceManager.GetString("a2015");
            item_datagrid.Columns[7].HeaderText = Resources.ResourceManager.GetString("a2016");




            _lbl_rquantity.Text = Resources.ResourceManager.GetString("a2017");
            _lbl_total_amount.Text = Resources.ResourceManager.GetString("a0503");
            _lbl_grandtotal.Text = Resources.ResourceManager.GetString("a0512");
            _lbl_total_comlga.Text = Resources.ResourceManager.GetString("a1026");



        }
        private void updateCustomerGridLocal()
        {
            //grid_bipari.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0201");
            //grid_bipari.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0012");
            //grid_bipari.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0512");
        }
        public void refreshUI(List<Landlord> tclients)
        {
            if (tclients == null)
            {
                return;
            }
            Admin.GetInstance.clients.Clear();
            Admin.GetInstance.clients = tclients;

            foreach (Landlord landlord in tclients)
            {
                addSalesRowinGrid(landlord, false);

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
            this.item_datagrid.Rows[count - 1].Cells[5].Value = (cust.sale.getTotalSale() + cust.Total_Commission + cust.Total_Chongi);
                //+cust.sale.getTotalExtraAmountLandlord()
                ;
            this.item_datagrid.Rows[count - 1].Cells[6].Value = cust.sale.getTotalExtraAmountCustomer();
            this.item_datagrid.Rows[count - 1].Cells[7].Value = cust.sale.getTotalExtraAmountLandlord();
            this.item_datagrid.Rows[count - 1].Cells[10].Value = cust.cust_bill_id;
            this.item_datagrid.Rows[count - 1].Cells[11].Value = cust.status;

        }

        private void addRowingrid_Zamidar(Landlord land)
        {
            int count = this.dgv_zamidar.Rows.Count;

            this.dgv_zamidar.Rows.Add();
            this.dgv_zamidar.Rows[count - 1].Cells[0].Value = land.land_person.pkey;
            this.dgv_zamidar.Rows[count - 1].Cells[1].Value = land.land_product.marka+" " + land.land_product._product_name+" "+land.land_person.pname;
            this.dgv_zamidar.Rows[count - 1].Cells[2].Value = land.total_quantity;
            this.dgv_zamidar.Rows[count - 1].Cells[3].Value = (int)land.GetTotalService;
            this.dgv_zamidar.Rows[count - 1].Cells[4].Value = (int)land.GetGrandTotal;

        }
        private void addSalesRowinGrid(Landlord land, bool check)
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
            client_services = land.expense.total_munshiana + land.expense.total_marketfee +
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
            v_lbl_total_expense.Text = "" + (client_services + (int)total_commission_chongi);

            total_amountsum.Text = "" + total_sale_amount;
            //lbl_expenset.Text = "" + (client_services + (int)total_commission_chongi);
            //grand_total.Text = "" + total_bill_amount;


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
        //private void SetTotalValue(int rowIndex, object value,Color color)
        //{
            
        //    if (rowIndex >= 0 && rowIndex < dgv_expense.Rows.Count)
        //    {
        //        dgv_expense.Rows[rowIndex].DefaultCellStyle.ForeColor = color;
        //        dgv_expense.Rows[rowIndex].Cells[1].Value = value;
        //    }
        //}
        //private void loadExpense(int Freight, int Labour, float Commission, int Laga, int Munshiana, int MarketFee, int Advance,int Extra)
        //{
        //    dgv_expense.Rows.Add(Resources.ResourceManager.GetString("efreight"), ""+ Freight);       // Add row with "Frieght" in the first column
        //    dgv_expense.Rows.Add(Resources.ResourceManager.GetString("elabour"), "" + Labour);        // Add row with "Labour" in the first column
        //    dgv_expense.Rows.Add(Resources.ResourceManager.GetString("ecommission"), "" + Commission);    // Add row with "Commission" in the first column
        //    dgv_expense.Rows.Add(Resources.ResourceManager.GetString("elaga"), "" + Laga);          // Add row with "Laga" in the first column
        //    dgv_expense.Rows.Add(Resources.ResourceManager.GetString("emunshiana"), ""+ Munshiana);    // Add row with "Munshiana" in the first column
        //    dgv_expense.Rows.Add(Resources.ResourceManager.GetString("emarketfee"), ""+ MarketFee);    // Add row with "Munshiana" in the first column
        //    dgv_expense.Rows.Add(Resources.ResourceManager.GetString("eadvance"), ""+ Advance);      // Add row with "Advance" in the first column
        //    dgv_expense.Rows.Add(Resources.ResourceManager.GetString("eextra"), ""+ Extra);
        //    dgv_expense.Rows.Add("Total", ""+ 0);        // Add row with "Extra" in the first column
        //}

        private void SalesAdd_Load(object sender, EventArgs e)
        {
            //init();
            adminlog = LogUtill.getAdminInputLog();
            int row = 0;
            foreach (Landlord l in landlist)
            {


                addRowingrid_Zamidar(l);
                if (tlbl_khata_id.Text == l.land_person.pkey)
                {
                    row++;
                }
            }
            dgv_zamidar.ClearSelection();
            try
            {
                dgv_zamidar.Rows[row - 1].Cells[0].Selected = true;
            }
            catch (Exception ex) {
            }
            currentrow = row - 1;
            gridRow = row - 1;
            txt_customerID.Focus();

        }
        private void init()
        {
            this.bal = new BLogic();
            saleParser = new SaleParser(date, Admin.SaveLog, Authentication.Account.local == "0" ? false : true);
            if (Authentication.Account.local == "1")
            {
                saleParser.SAVELOG = true;

            }
            change_AddTOUpdate(false);
            //DisplayData("");
            readDailySale(date, "");
        }

        #region Refresh

        bool localRecord = true;
        public void readDailySale(string date, string text)
        {
            List<Landlord> tclients = bal.getLandlordsList(date, text);//Test Comment
            if (tclients.Count > 0)
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
                if (Authentication.Account.local == "1")
                {
                    localRecord = true;
                    tclients = saleParser.LoadTodaySale();
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
                if (grid.Name == "detail_datagrid")
                {
                    gridRow--;
                    if (gridRow < 0)
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
                    currentrow = totalRows - 1;
                }

                if (grid.Name == "detail_datagrid")
                {
                    gridRow++;
                    if (gridRow > totalRows)
                    {
                        gridRow = totalRows - 1;
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
                    if (txt_client_nameid.Focused)
                        selectUpRow(dgv_zamidar);
                    else
                    if (dgv_zamidar.Focused)
                        selectUpRow(dgv_zamidar);


                    return true;
                case Keys.Down:
                    if (txt_client_nameid.Focused)
                        selectDownRow(dgv_zamidar);
                    else
                    if (dgv_zamidar.Focused)
                        selectDownRow(dgv_zamidar);

                    return true;
                case Keys.Delete:
                    return true;
                case Keys.F2:
                    return true;
                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.Control | Keys.F5:
                    btn_refresh_Click(this, new EventArgs());
                    return true;
                case Keys.F5:
                    //Stuff
                    btn_change_Click(this, new EventArgs());
                    return true;
                case Keys.Control | Keys.N:
                    btn_addstock_Click(this, new EventArgs());
                    return true;
                case Keys.Enter:

                    try
                    {
                        if(txt_bikri_quantity.ContainsFocus)
                        {
                            txt_bikri_rate.Focus();
                        }
                        else if(txt_bikri_rate.ContainsFocus)
                        {
                            txt_customerID.Focus();
                        }
                        else if (txt_biltino.ContainsFocus)
                        {
                            searchByVehicle(txt_biltino.Text);
                            txt_client_nameid.Focus();
                        }
                        else
                        if (txt_client_nameid.ContainsFocus)
                        {
                            Landlord land = landlist[currentrow];
                            lbl_vehicle_no.Text = land.client._vehicle_id;
                            lbl_marka.Text = land.land_product.marka;
                            lbl_product.Text = land.land_product._product_name;
                            lbl_zamid.Text = land.land_person.pid;
                            lbl_bipari_id.Text = land.client._person_cl.pid;
                            txt_landloard_nameid.Text = land.client._person_cl.pname;
                            initRefresh(land.land_person.pname, land.client._person_cl.pname, land.bill_key, "" + land.land_product.sale_remaining_product, land.client._person_cl.pid, land.land_person.pkey,"");
                            txt_customerID.Focus();
                        }
                        else
                        {
                            if (txt_nobegs.ContainsFocus || txt_begamount.ContainsFocus)
                            {

                                if (txt_nobegs.ContainsFocus)
                                {
                                    string quanti = txt_nobegs.Text;
                                    if (quanti == "" || !quanti.Any(char.IsDigit)) return true;
                                }
                                else
                                if (txt_begamount.ContainsFocus)
                                {
                                    string rate = txt_begamount.Text;
                                    if (rate == "" || !rate.Any(char.IsDigit)) return true;
                                }
                            }
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

                            if (txt_nobegs.ContainsFocus || txt_begamount.ContainsFocus)
                            {

                                int rsale = int.Parse(lbl_remaining_sale.Text == "" ? "0" : lbl_remaining_sale.Text);
                                if (rsale == 0)
                                {
                                    return true;

                                    //txt_userid.Select();
                                    //MessageBox.Show("No Remaining Product...");
                                }

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

        private void searchByVehicle(string bilti_vehicle)
        {
            //1 Vehicle no
            //2 Marka
            //3 Bipari Name
            //4 Zamidar Name
            //5 Product
            //6 Bipari ID
            //7 Zamidar ID
            //8 Bipari Key
            //9 Zamidar Key
            

            if (!string.IsNullOrEmpty(bilti_vehicle) && bilti_vehicle!="")
            {
                dgv_zamidar.Rows.Clear();
                foreach (Landlord land in landlist)
                {
                    
                    if (cm_seach.SelectedIndex == 0) {
                        if (land.client._vehicle_id == bilti_vehicle)
                        {
                            addRowingrid_Zamidar(land);
                        }
                    }
                    else if (cm_seach.SelectedIndex == 1) {
                        if (land.land_product.marka == bilti_vehicle)
                        {
                            addRowingrid_Zamidar(land);
                        }
                    }
                    else if (cm_seach.SelectedIndex == 2) {
                        if (land.client._person_cl.pname == bilti_vehicle)
                        {
                            addRowingrid_Zamidar(land);
                        }
                    }
                    else if (cm_seach.SelectedIndex == 3) {
                        if (land.land_person.pname == bilti_vehicle)
                        {
                            addRowingrid_Zamidar(land);
                        }
                    }
                    else if (cm_seach.SelectedIndex == 4) {
                        if (land.land_product._product_name == bilti_vehicle)
                        {
                            addRowingrid_Zamidar(land);
                        }
                    }
                    else if (cm_seach.SelectedIndex == 5) {
                        if (land.client._person_cl.pid == bilti_vehicle)
                        {
                            addRowingrid_Zamidar(land);
                        }
                    }
                    else if (cm_seach.SelectedIndex == 6) {
                        if (land.land_person.pid == bilti_vehicle)
                        {
                            addRowingrid_Zamidar(land);
                        }
                    }
                    else if (cm_seach.SelectedIndex == 7) {
                        if (land.client._person_cl.pkey == bilti_vehicle)
                        {
                            addRowingrid_Zamidar(land);
                        }
                    }
                    else if (cm_seach.SelectedIndex == 8) {
                        if (land.land_person.pkey == bilti_vehicle)
                        {
                            addRowingrid_Zamidar(land);
                        }
                    }
                }
            }
            else
            {
                dgv_zamidar.Rows.Clear();
                SalesAdd_Load(this, new EventArgs());
            }
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
            client_services = temp.expense.total_munshiana + temp.expense.total_marketfee +
                temp.expense.total_rent +
                temp.expense.total_labour +
                temp.land_person.advance;
            if (temp.land_product.sale_remaining_product > 0)
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
            v_lbl_total_expense.Text = "" + (client_services + (int)total_commission_chongi);


            // Set total values using the function
            //SetTotalValue(0, temp.expense.total_rent, Color.Black); // Frieght
            //SetTotalValue(1, temp.expense.total_labour, Color.Black); // Labour
            //SetTotalValue(2, temp.GetCommission, Color.Black); // Commission
            //SetTotalValue(3, temp.GetChongi, Color.Black);  // Laga
            //SetTotalValue(4, temp.expense.total_munshiana, Color.Black);  // Munshiana
            //SetTotalValue(5, temp.expense.total_marketfee, Color.Black);  // Munshiana
            //SetTotalValue(6, temp.land_person.advance, Color.Black); // Advance
            //SetTotalValue(7, 0, Color.Black); // Extra
            //SetTotalValue(8, (client_services + (int)total_commission_chongi), Color.Red); // Total

            showTotal((int)temp.GetTotalService + (int)temp.GetChongi + (int)temp.GetCommission, temp.GetTotalSaleLandLord, (int)temp.GetGrandTotal);



        }




        #endregion

        #endregion

        #region Focus Change
        private void changeColor(Color customer ,Color begs,Color rate, Color cal, Color add)
        {
            txt_customerID.BackColor = customer;
            txt_nobegs.BackColor = begs;
            txt_begamount.BackColor = rate;
            btn_calculate.BackColor = cal;
            btnAddCalculate.BackColor = add;
        }
        public void changetxtBoxFocus()
        {

            if (txt_customerID.ContainsFocus)
            {
                changeColor(Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE),  Color.White, Color.White, Color.White);

                txt_nobegs.Select();
                txt_nobegs.SelectAll();
            }
            else if (txt_nobegs.ContainsFocus)
            {
                changeColor(Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White, Color.White);
                txt_begamount.Select();
                txt_begamount.SelectAll();
            }
            else if (txt_begamount.ContainsFocus)
            {
                changeColor(Color.White, Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White);

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
                changeColor(Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White, Color.White, Color.White, Color.White);

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
        private void showTotal(int expense, int salestotal, int grandtotal)
        {
            lbl_expenset.Text = "" + expense;
            total_amountsum.Text = "" + salestotal;
            grand_total.Text = "" + grandtotal;
        }
        private void btn_calculate_Click(object sender, EventArgs e)
        {

            if (txt_client_nameid.Text == "")
            {

                return;
            }


            string bikritype = "";
            float bikri_quantity = 0, bikri_rate = 0;
            string customerid = lbl_custid.Text;
            string customer_name = txt_customerID.Text;
            int _sale_quantity = int.Parse(txt_nobegs.Text == "" ? "0" : txt_nobegs.Text);
            int _sale_amount = int.Parse(txt_begamount.Text == "" ? "0" : txt_begamount.Text);

            if (chk_bikri.Checked)
            {
                bikritype = "B";
                bikri_quantity = float.Parse(txt_bikri_quantity.Text == "" ? "0" : txt_bikri_quantity.Text);
                bikri_rate = float.Parse(txt_bikri_rate.Text == "" ? "0" : txt_bikri_rate.Text);
            }

            remaining_quantity = int.Parse(lbl_remaining_sale.Text == "" ? "0" : lbl_remaining_sale.Text);
            if (_sale_quantity == 0)
            {
                return;
            }

            if (remaining_quantity - _sale_quantity < 0)
            {
                txt_nobegs.Text = remaining_quantity + "";
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
                if (ptype != "")
                {
                    prCust._type = txt_begtype.Text;
                }
                Sale msale = new Sale(_sale_quantity, _sale_amount);
                Customer cust = new Customer(date, templandlord.service, false, msale, templandlord.land_person);
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
                templandlord.bikri_quantity = bikri_quantity;
                templandlord.bikri_rate = bikri_rate;
                templandlord.bill_type = bikritype;
                templandlord.total_bikri = (int)Math.Ceiling(bikri_quantity * bikri_rate);

                templandlord.total_quantity = templandlord.land_product.total_Quantity;
                templandlord.land_product.sale_remaining_product = templandlord.land_product.sale_remaining_product - _sale_quantity; //Error

                //if (chk_bikri.Checked)
                //    templandlord.total_bikri = bikri_rate * bikri_quantity;
                //else
                templandlord.total_sale += (int)cust.sale.getTotalSale() + cust.sale.getTotalExtraAmountLandlord();


                templandlord.getCommission();
                templandlord.getChongi();
                #endregion

                if (templandlord.land_product.sale_remaining_product > 0)
                {
                    templandlord.status = EStatus.InComplete;
                }
                else
                if (templandlord.land_product.sale_remaining_product == 0)
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
                templandlord.total_services = (int)(templandlord.GetTotalService + templandlord.GetChongi + templandlord.GetCommission);


                addRowingrid_bipari(cust);
                clear();
                showTotal((int)templandlord.GetTotalService+(int)templandlord.GetChongi+(int)templandlord.GetCommission, templandlord.GetTotalSaleLandLord, (int)templandlord.GetGrandTotal);
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
            int rq = (lbl_remaining_sale.Text == "" || lbl_remaining_sale.Text == "0") ? 0 : int.Parse(lbl_remaining_sale.Text);
            int rtitems = (lbl_totalitems.Text == "" || lbl_totalitems.Text == "0") ? 0 : int.Parse(lbl_totalitems.Text);

            if (rq == rtitems)
            {
                return;
            }
            Account acc = Authentication.Account;
            templandlord.UpdateTotal();
            templandlord.category = nameof(BillKey.EnumUser.Client);

            //  return;
            string statuslive = lbl_status.Text;
            if (acc.local == "0" || statuslive == "Live")
            {

                addSales(templandlord);

            }
            else if (acc.local == "1")
            {
                // Safely parse and assign values from DataGridView
                addLocalandDBSale(templandlord);


                //This code use to send sales in temp table
                /*foreach (Customer c in templandlord.customers)
                {
                    new BLogic().p_daily_temp_table_crud("", templandlord.date, int.Parse(templandlord.land_person.pid), int.Parse(templandlord.client._person_cl.pid) ,
                      templandlord.total_quantity, templandlord.expense.total_rent,
                      templandlord.expense.total_labour, templandlord.expense.total_advance_amount,
                      templandlord.GetCommission, (int)templandlord.GetChongi,
                      c.Total_Commission, (int)c.getChongi(), templandlord.expense.total_munshiana, templandlord.expense.total_marketfee
                      , int.Parse(c.customer_profile.pid), c.sale._sale_quantity, (int)c.sale._sale_amount, c.sale._TotalSaleAmount, c.GrandTotalCustomer, c.GrandTotalLandlord,
                      c.sale.add_extra_amount_Customer, c.sale.add_extra_amount_Landlord, int.Parse(templandlord.land_product._product_id),
                      templandlord.land_product._product_name, templandlord.land_product.marka, int.Parse(templandlord.land_product._weight_id), templandlord.land_product._weight,
                      templandlord.client._person_cl.pkey, templandlord.land_person.pkey,
                      c.customer_profile.pkey, int.Parse(templandlord.land_product._weight_id),
                      templandlord.land_product._weight, templandlord.bill_type, templandlord.bikri_quantity,
                      templandlord.bikri_rate, templandlord.client._vehicle_id, templandlord.land_product._weight, 1,templandlord.service.commission_customer_product, templandlord.service.commission_client_product, templandlord.service.client_chongi,
                      templandlord.service.customer_chongi, templandlord.service.rent_per_product, templandlord.service.labour_per_product);
                }*/
            }
            if (templandlord == null) return;
            showTotal((int)templandlord.GetTotalService+(int)(templandlord.GetChongi+templandlord.GetCommission), templandlord.GetTotalSaleLandLord, (int)templandlord.GetGrandTotal);


        }

        private void addSales(Landlord landlord)
        {
            // Landlord temdata = null;
            string statuslive = lbl_status.Text;
            //return;

            if (landlord.status == EStatus.CompleteUpdate || landlord.status == EStatus.Complete)
            {
                //Refresh Sale Data of landlord
                int tchk = 1;
                List<string> msgSuccess=new List<string>();
                List<string> msgError = new List<string>();

                for (int i = 0; i < item_datagrid.Rows.Count; i++)
                {
                    
                    string status = item_datagrid.Rows[i].Cells[11].Value.ToString();
                    if (status == "0")
                    {
                        //InsertRecord


                        Customer customer = templandlord.customers[i];
                        /*bool chk = new BLogic().updateCusomerAmountandBalanceShet(templandlord.date,
                         templandlord.land_person.pkey, customer.customer_profile.pkey,
                       customer.customer_profile.pid, tchk, customer.cust_bill_id);*/

                        string[] result = new BLogic().p_singlesaleadd(templandlord, customer);
                        tchk = 0;

                        if (result[0] == "OK" || result[0] == "1")
                        {
                           
                            //int chk = new BLogic().post_to_journal_sales("S_ALL", "", date);
                            //chk += new BLogic().post_to_journal_sales("P_ALL", "", date);
                            //if (result[0] == "OK" || result[0] == "1")
                            //{

                            //}
                            List<string> msgup = new BLogic().p_singlesaleupdate_landlord_customer(date, templandlord.land_person.pid, customer.customer_profile.pid);

                            msgSuccess.Add(result[1] + "- Update:- "  + string.Join(", ", msgup));

                        }
                        else
                        {
                            msgError.Add(result[1]);
                        }
                        //return;
                    }
                }
                if(msgSuccess.Count>0)
                {
                    tchk = 1;
                    //int chk = new BLogic().post_to_journal_sales("S_ALL", "", date);
                    //chk += new BLogic().post_to_journal_sales("P_ALL", "", date);
                    MessageBox.Show($"✅ Sale added. New IDs: {string.Join(", ", msgSuccess)}",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if(msgError.Count>0)
                {
                    tchk = 0;
                    MessageBox.Show($"⚠️ Error adding sale:\n{string.Join(", ", msgError)}",
                                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (tchk == 0 || tchk == 1)
                {
                   // int chk = new BLogic().post_to_journal_sales("S_ALL", "", date);
                   // chk += new BLogic().post_to_journal_sales("P_ALL", "", date);
                    item_datagrid.Rows.Clear();
                    item_datagrid.Refresh();
                    refresh();
                    txt_client_nameid.Focus(); 
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
                    (int)landlord.GetGrandTotal, landlord.land_person.pkey, landlord.land_person.pname, 0, "19");
                //bal.addExpense_IUSales(landlord.date, landlord.land_person.pkey,landlord.land_person.pname, (int)landlord.GetGrandTotal,nameof(BillKey.EnumUser.PaymentSale));
                //bal.addBalanceSheet("credit", 0, landlord,nameof(BillKey.EnumUser.Client), "insert", landlord.land_person.pkey,"");
                int chk = new BLogic().post_to_journal_sales("S_ALL", "", date);
                chk += new BLogic().post_to_journal_sales("P_ALL", "", date);
                refresh();

            }
        }
        public void refresh()
        {
            refreshSalesData();
            readDailySale(date, "");
            lbl_status.Text = "Sale Record Inserted.";
        }
        private void addLocalandDBSale(Landlord temp)
        {
            if (temp.customers != null)
            {
                bool check = false;
                Admin.GetInstance.clients[ll_index] = temp;
                try
                {
                    if (saleParser.SAVELOG)
                    {
                        if (temp.land_product.sale_remaining_product == 0)
                            temp.status = EStatus.Complete;
                        else
                        if (temp.land_product.sale_remaining_product > 0)
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
                    int selectedRowIndex = 0;
                    if (dgv_zamidar.SelectedRows.Count > 0)
                    {
                        selectedRowIndex = dgv_zamidar.SelectedRows[0].Index;
                    }
                    dgv_zamidar.Rows[selectedRowIndex].Cells[4].Value = templandlord.GetGrandTotal;
                    landlist[selectedRowIndex] = templandlord;

                    item_datagrid.Rows.Clear();
                    item_datagrid.Refresh();
                    total_amountsum.Text = "" + temp.total_sale;
                    //lbl_expenset.Text = "" + (temp.GetTotalService + temp.GetChongi + temp.GetCommission);
                    //grand_total.Text = "" + temp.GetGrandTotal;
                    readDailySale(date, "");
                    refreshSalesData();
                    lbl_status.Text = "Sale Record Inserted.";
                    txt_client_nameid.Focus();

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
            txt_client_nameid.Focus();
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
            txt_nobegs.Text = customer.sale._sale_quantity + "";
            txt_begamount.Text = customer.sale._sale_amount + "";


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
            else*/
            if (e.ColumnIndex == 0)
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
                    float totsale = float.Parse(item_datagrid.Rows[index].Cells[5].Value.ToString());
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
                        initRefresh(txt_client_nameid.Text, txt_landloard_nameid.Text, tlbl_khata_id.Text, lbl_remaining_sale.Text, this.cl_id, this.billid,"");

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

            }
            else if (e.ColumnIndex == 8)
            {
                AddExtraAmount extra = new AddExtraAmount(templandlord, templandlord.customers[index], index, lbl_status.Text);
                int ex_amountLandlor = extra.getCustomer().sale.add_extra_amount_Landlord;
                int ex_amountCustomer = extra.getCustomer().sale.add_extra_amount_Customer;
                extra.ShowDialog();
                if (ex_amountLandlor != extra.getCustomer().sale.add_extra_amount_Landlord)
                {
                    Customer cust = extra.getCustomer();
                    templandlord.customers[index] = cust;
                    templandlord.total_sale = (int)templandlord.customers.Sum(x => x.sale._TotalSaleAmount);
                    item_datagrid.Rows.Clear();
                    item_datagrid.Refresh();
                    bal.updateExtraAmount(templandlord, templandlord.customers[index], "Client");


                    showCustomerSale(templandlord);
                    addSalesRowinGrid(templandlord, true);

                }
                else if (ex_amountCustomer != extra.getCustomer().sale.add_extra_amount_Customer)
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
            else if (e.ColumnIndex == 9)//Changename
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
                    bal.p_ud_cust_sale_product(templandlord.land_person.pkey, templandlord, templandlord.category);

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
                btn_seach_beg_Click(this, new EventArgs());
            }
            else
            if ((txt_customerID.ContainsFocus))
            {
                btn_search_cust_Click(this, new EventArgs());
            }
            else
            if ((txt_client_nameid.ContainsFocus))
            {
                btn_clientsearch_Click(this, new EventArgs());
            }









        }
        int oldData = 0, oldAmount = 0;
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
                sendingCB.EditingControlFormattedValue = int.Parse(b.ToString()) * int.Parse(r.ToString());



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
            using (VendorForm vend = new VendorForm(date, 1, lbl_status.Text))
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
            if (templandlord == null) return;
            showTotal((int)templandlord.GetTotalService + (int)templandlord.GetChongi + (int)templandlord.GetCommission, templandlord.GetTotalSaleLandLord, (int)templandlord.GetGrandTotal);
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

        private void btn_change_Click(object sender, EventArgs e)
        {
            txt_client_nameid.Focus();
        }

        private void btn_update_expense_Click(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
           
            searchByVehicle(txt_biltino.Text);
            txt_client_nameid.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < item_datagrid.Rows.Count; i++)
            {

                string status = item_datagrid.Rows[i].Cells[11].Value.ToString();
                //if (status == "0")
                {
                    Customer customer = templandlord.customers[i];
                    List<string> msgUp = new BLogic().p_singlesaleupdate_landlord_customer(date, templandlord.land_person.pid, customer.customer_profile.pid);
                    MessageBox.Show(string.Join(", ", msgUp));

                }
            }
        }

        private void txt_bikri_rate_TextChanged(object sender, EventArgs e)
        {
            float bikri_quantity=float.Parse(txt_bikri_quantity.Text == "" ? "0" : txt_bikri_quantity.Text);
            float bikri_rate = float.Parse(txt_bikri_rate.Text == "" ? "0" : txt_bikri_rate.Text);
            float total_bikri = bikri_quantity * bikri_rate;
            lbl_total_bikri.Text = total_bikri.ToString();
        }

        private void txt_bikri_quantity_TextChanged(object sender, EventArgs e)
        {
            float bikri_quantity = float.Parse(txt_bikri_quantity.Text == "" ? "0" : txt_bikri_quantity.Text);
            float bikri_rate = float.Parse(txt_bikri_rate.Text == "" ? "0" : txt_bikri_rate.Text);
            float total_bikri = bikri_quantity * bikri_rate;
            lbl_total_bikri.Text = total_bikri.ToString();
        }

        #region New Code For DataGridview of dgv_zamidar
        // Function to select the previous row (Move Up)
        private void selectUpRow(DataGridView grid)
        {
            DataGridView dgv = grid;
            int totalRows = dgv.Rows.Count;

            // Ensure there are rows and at least one cell is selected
            if (totalRows > 0 && dgv.SelectedCells.Count > 0)
            {
                // Get the currently selected row and column
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;

                // Check if we are not at the first row
                if (rowIndex > 0)
                {
                    dgv.ClearSelection(); // Clear previous selection
                    dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true; // Select the previous row
                    grid.FirstDisplayedScrollingRowIndex = rowIndex - 1; // Scroll to the previous row

                    // Update currentrow variable
                    currentrow = rowIndex - 1;

                    if (grid.Name == "detail_datagrid")
                    {
                        gridRow = currentrow; // Update gridRow for detail_datagrid
                    }
                }
            }
            else
            {
                // If no row is selected, select the first row and initialize currentrow
                dgv.ClearSelection();
                dgv.Rows[0].Cells[0].Selected = true;
                currentrow = 0;
            }
        }

        // Function to select the next row (Move Down)
        private void selectDownRow(DataGridView grid)
        {
            DataGridView dgv = grid;
            int totalRows = dgv.Rows.Count;

            // Ensure there are rows and at least one cell is selected
            if (totalRows > 0 && dgv.SelectedCells.Count > 0)
            {
                // Get the currently selected row and column
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;

                // Check if we are not at the last row
                if (rowIndex < totalRows - 1)
                {
                    dgv.ClearSelection(); // Clear previous selection
                    dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true; // Select the next row
                    grid.FirstDisplayedScrollingRowIndex = rowIndex + 1; // Scroll to the next row

                    // Update currentrow variable
                    currentrow = rowIndex + 1;

                    if (grid.Name == "detail_datagrid")
                    {
                        gridRow = currentrow; // Update gridRow for detail_datagrid
                    }
                }
            }
            else
            {
                // If no row is selected, select the first row and initialize currentrow
                dgv.ClearSelection();
                dgv.Rows[0].Cells[0].Selected = true;
                currentrow = 0;
            }
        }

        #endregion
    }
}
