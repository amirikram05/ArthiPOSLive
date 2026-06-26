using ArthiPOS.controls.dashboard;
using ArthiPOS.Properties;
using ArthiPOS.utill;
using ArthiPOS.Utill;
using BAL;
using CommonUtilities;
using DataMember;
using DataMember.memberlog;
using Google.Cloud.Firestore;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class SalesNew : Form
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

        string date;
        SaleParser saleParser;
        public enum EnumShop
        {
            Sale,
            Customer,
            BegType
        };
        public EnumShop shop_enum = EnumShop.Sale;
        public SalesNew()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            localization();
        }

        public void localization()
        {

            detail_datagrid.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0101");
            detail_datagrid.Columns[1].HeaderText = Resources.ResourceManager.GetString("a1038");
            detail_datagrid.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0204");
            detail_datagrid.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0203");
            detail_datagrid.Columns[4].HeaderText = Resources.ResourceManager.GetString("a0401");
            detail_datagrid.Columns[5].HeaderText = Resources.ResourceManager.GetString("a0407");
            detail_datagrid.Columns[6].HeaderText = Resources.ResourceManager.GetString("a1039");
            detail_datagrid.Columns[7].HeaderText = Resources.ResourceManager.GetString("a1022");
            detail_datagrid.Columns[8].HeaderText = Resources.ResourceManager.GetString("a0503");
            detail_datagrid.Columns[9].HeaderText = Resources.ResourceManager.GetString("a1040");
            detail_datagrid.Columns[10].HeaderText = string.Format("{0}/{1}",
                Resources.ResourceManager.GetString("a0302"),
                Resources.ResourceManager.GetString("a0301"));
            detail_datagrid.Columns[11].HeaderText = Resources.ResourceManager.GetString("a0037");
            detail_datagrid.Columns[12].HeaderText = Resources.ResourceManager.GetString("a0013");
            detail_datagrid.Columns[13].HeaderText = Resources.ResourceManager.GetString("a0012");
            detail_datagrid.Columns[14].HeaderText = Resources.ResourceManager.GetString("a0009");

            check_khata.Text = Resources.ResourceManager.GetString("a0013");
            check_cust_name.Text = Resources.ResourceManager.GetString("a0022");
            check_bill_no.Text = Resources.ResourceManager.GetString("a1094");
            check_chalan.Text = Resources.ResourceManager.GetString("a1089");
            check_date.Text = Resources.ResourceManager.GetString("a0009");
            check_remaining_amount.Text = Resources.ResourceManager.GetString("a1076");
            check_remaining_begs.Text = Resources.ResourceManager.GetString("a1019");



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

            Landlord t_client = null;
            if (Admin.GetInstance.clients.Count > 0)
            {
                ll_index = Admin.GetInstance.clients.FindIndex(cl => cl.land_person.pkey == bill_id);
                t_client = Admin.GetInstance.clients[ll_index];

            }
            return t_client;
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
            string marka = land.land_product.marka;
            int count = this.detail_datagrid.Rows.Count;

            total_commission_chongi = (land.GetChongi + land.GetCommission);
            client_services = land.expense.total_munshiana + land.expense.total_marketfee +
                land.expense.total_rent +
                land.expense.total_labour +
                land.land_person.advance;
            int rQuantity = land.land_product.sale_remaining_product;

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
            total_bill_amount = land.GetTotalSaleLandLord -
                client_services - (int)total_commission_chongi;

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

            int index = Admin.GetInstance.clients.FindIndex(x => x.land_person.pkey == land.land_person.pkey);


            if (check)
            {
                index = 0;
                addUpdateRowGridLandlord(index, marka + " "+ billname, customernames,
                    "" + totalChalan, "" + client_services,
                    rQuantity + "",
                    "" + total_sale_amount, "" + total_bill_amount, bill_key,
                    date, "" + total_commission_chongi, land.status.ToString());

            }
            else
            {
                addRowGridLandlord(marka + " " + billname, customernames, "" + totalChalan, "" + client_services,
                    "" + land.land_product.total_Quantity, "" + total_sale_amount,
                    "" + total_bill_amount, bill_key, date,
                    "" + land.land_product.sale_remaining_product, land.land_person.pid,
                    "" + (land.GetCommission + land.GetChongi), land.status.ToString());
            }

        }
        private void addUpdateRowGridLandlord(int gridRow, string billname, string customernames, string totalChalan, string client_services, string remaining_quantity,
            string total_sale_amount, string total_bill_amount,
            string bill_key, string date, string commissionchongi, string status)
        {

            if (detail_datagrid.Rows.Count == 0)
            {
                gridRow = 0;
            }
            if (gridRow > 0)
            {
                this.detail_datagrid.Rows[gridRow].Cells[2].Value = billname != "" ? billname : billname;
                this.detail_datagrid.Rows[gridRow].Cells[3].Value = customernames != "" ? customernames : customernames;

                this.detail_datagrid.Rows[gridRow].Cells[5].Value = remaining_quantity;

                this.detail_datagrid.Rows[gridRow].Cells[6].Value = totalChalan != "0" ? "" + totalChalan : "" + 0;
                this.detail_datagrid.Rows[gridRow].Cells[7].Value = client_services;
                this.detail_datagrid.Rows[gridRow].Cells[8].Value = total_sale_amount;
                this.detail_datagrid.Rows[gridRow].Cells[9].Value = total_bill_amount;
                this.detail_datagrid.Rows[gridRow].Cells[10].Value = commissionchongi;//Commission Chongi
                this.detail_datagrid.Rows[gridRow].Cells[11].Value = 0;//Remaining Amount
                                                                       //this.detail_datagrid.Rows[gridRow].Cells[12].Value = bill_key;
                                                                       //this.detail_datagrid.Rows[gridRow].Cells[14].Value = date;
                this.detail_datagrid.Rows[gridRow].Cells[16].Value = status;
            }
        }

        private void gridColumnVisible()
        {
            if (check_cust_name.Checked)
            {
                this.detail_datagrid.Columns[3].Visible = true;
            }
            else
            {
                this.detail_datagrid.Columns[3].Visible = false;

            }
            if (check_remaining_begs.Checked)
            {
                this.detail_datagrid.Columns[5].Visible = true;

            }
            else
            {
                this.detail_datagrid.Columns[5].Visible = false;

            }
            if (check_chalan.Checked)
            {
                this.detail_datagrid.Columns[6].Visible = true;

            }
            else
            {
                this.detail_datagrid.Columns[6].Visible = false;

            }
            if (check_remaining_amount.Checked)
            {
                this.detail_datagrid.Columns[11].Visible = true;

            }
            else
            {
                this.detail_datagrid.Columns[11].Visible = false;

            }
            if (check_bill_no.Checked)
                this.detail_datagrid.Columns[12].Visible = true;
            else
                this.detail_datagrid.Columns[12].Visible = false;
            if (check_khata.Checked)
                this.detail_datagrid.Columns[13].Visible = true;
            else
                this.detail_datagrid.Columns[13].Visible = false;

            if (check_date.Checked)
                this.detail_datagrid.Columns[14].Visible = true;
            else
                this.detail_datagrid.Columns[14].Visible = false;
        }
        private void addRowGridLandlord(string billname, string customernames, string totalChalan, string client_services,
            string total_quantity, string total_sale_amount, string total_bill_amount,
            string bill_key, string date, string remaining_quantity, string ll_id,
            string chongi_commisison, string status)
        {

            int count = this.detail_datagrid.Rows.Count;



            if (count == 0)
            {
                count = 1;
            }
            else
            {
                count = count + 1;
            }
            this.detail_datagrid.Rows.Add();

            this.detail_datagrid.Rows[count - 1].Cells[2].Value = billname;
            this.detail_datagrid.Rows[count - 1].Cells[3].Value = customernames;

            this.detail_datagrid.Rows[count - 1].Cells[4].Value = total_quantity;
            this.detail_datagrid.Rows[count - 1].Cells[5].Value = remaining_quantity;

            this.detail_datagrid.Rows[count - 1].Cells[6].Value = totalChalan;
            this.detail_datagrid.Rows[count - 1].Cells[7].Value = client_services;
            this.detail_datagrid.Rows[count - 1].Cells[8].Value = total_sale_amount;
            this.detail_datagrid.Rows[count - 1].Cells[9].Value = total_bill_amount;
            this.detail_datagrid.Rows[count - 1].Cells[10].Value = chongi_commisison;//COmmission/Chongi
            this.detail_datagrid.Rows[count - 1].Cells[11].Value = 0;//Remaining Amount
            this.detail_datagrid.Rows[count - 1].Cells[12].Value = bill_key;
            this.detail_datagrid.Rows[count - 1].Cells[13].Value = ll_id;
            this.detail_datagrid.Rows[count - 1].Cells[14].Value = date;
            this.detail_datagrid.Rows[count - 1].Cells[16].Value = status;
        }



        private void SalesNew_Load(object sender, EventArgs e)
        {
            init();
            adminlog = LogUtill.getAdminInputLog();
        }
        private void init()
        {
            this.bal = new BLogic();
            date = sale_date.Text;
            saleParser = new SaleParser(date, Admin.SaveLog, Authentication.Account.local == "0" ? false : true);
            if (Authentication.Account.local == "1")
            {
                saleParser.SAVELOG = true;

            }
            //DisplayData("");
            link_file_path.Text = Path.GetDirectoryName(saleParser.filePath); 

            readDailySale(date, "");
            txt_userid.Select();
            gridColumnVisible();
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
                detail_datagrid.Columns[0].Visible = true;
                detail_datagrid.Columns[1].Visible = true;
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



        private void previousdate_Click(object sender, EventArgs e)
        {
            sale_date.Value = CommonUtill.ChangeDate(sale_date, -1);
            date = sale_date.Text;
            sale_date_CloseUp(this, new EventArgs());
        }

        private void nextdate_Click(object sender, EventArgs e)
        {
            sale_date.Value = CommonUtill.ChangeDate(sale_date, 1);
            date = sale_date.Text;
            sale_date_CloseUp(this, new EventArgs());
        }



        private void sale_date_ValueChanged(object sender, EventArgs e)
        {
            //date = sale_date.Text;
            //saleParser = new SaleParser(date, Admin.SaveLog, Authentication.Account.local == "0" ? false : true);
            //detail_datagrid.Rows.Clear();
            //detail_datagrid.Refresh();
            //init();
        }

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
                    if (txt_userid.Focused)
                        selectUpRow(detail_datagrid);
                    //else
                    //if (txt_customerID.Focused)
                    //selectUpRow(grid_bipari);
                    else
                    if (detail_datagrid.Focused)
                        selectUpRow(detail_datagrid);


                    return true;
                case Keys.Down:
                    if (txt_userid.Focused)
                        selectDownRow(detail_datagrid);
                    //else
                    //if (txt_customerID.Focused)
                    //  selectDownRow(grid_bipari);
                    else
                    if (detail_datagrid.Focused)
                        selectDownRow(detail_datagrid);

                    return true;
                case Keys.Delete:
                    if (detail_datagrid.Focused)
                        deleteCurrentRow();
                    //Stuff
                    return true;
                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.F5:
                    btn_refresh_Click(this, new EventArgs());
                    return true;
                case Keys.Control | Keys.G:
                    //Stuff
                    changeGridFocus();
                    return true;
                case Keys.Control | Keys.N:
                    btn_addstock_Click(this, new EventArgs());
                    return true;
                case Keys.Enter:

                    try
                    {
                        if (txt_userid.ContainsFocus)
                            chk_user = true;

                        grid_landload_CellClick(this, new DataGridViewCellEventArgs(0, currentrow));


                        if (templandlord.client._product.sale_remaining_product >= 0)
                        {
                            {
                                changetxtBoxFocus();
                            }

                        }
                        else
                        {
                        }

                        // If no row is selected, select the first row and initialize currentrow
                        detail_datagrid.ClearSelection();
                        if(detail_datagrid.Rows.Count>0) detail_datagrid.Rows[gridRow].Cells[0].Selected = true;
                        currentrow = gridRow;

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

                    return true;
                case Keys.Alt | Keys.Enter:
                    // btn_add_customer_Click(this, new EventArgs());

                    return true;


            }



            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void saleAddDailog()
        {


        }

        private void deleteCurrentRow()
        {
            throw new NotImplementedException();
        }

        private void updateUIData(Landlord temp)
        {
            //carrent.Text = "" + temp.expense.total_rent;
            //kharcha.Text = "" + temp.land_person.expense;
            //munshiana.Text = "" + temp.expense.total_munshiana;
            //mazdori.Text = "" + temp.expense.total_labour;
            remaining_quantity = temp.land_product.total_Quantity;
            if (temp.land_product.sale_remaining_product > 0)
            {
                // change_AddTOUpdate(true);
            }

        }

        #endregion

        #endregion

        #region Focus Change
        public void changeGridFocus()
        {
            {
                detail_datagrid.Select();
            }
        }

        public void changetxtBoxFocus()
        {

            if (txt_userid.ContainsFocus)
            {

                shop_enum = EnumShop.Customer;
                searchClientfortransportRent();

            }

            currentrow = 0;
        }
        #endregion
        int pageindex = 1;
        int pageSize = 20;


        public void searchClientfortransportRent()
        {


            {
                btn_refresh_Click(this, new EventArgs());
                /*string search = txt_userid.Text;
                List<Landlord> tclients = bal.getLandlordsList(date, search);
                detail_datagrid.Rows.Clear();
                detail_datagrid.Refresh();
                foreach (Landlord landlord in tclients)
                {
                    addSalesRowinGrid(landlord, false);
                }*/
                //detail_datagrid.DataSource = bal.searchRecords(date, "Sale", search);
            }

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


        public void refreshSalesData()
        {
            addSalesRowinGrid(templandlord, true);
            templandlord = null;
            shop_enum = EnumShop.Sale;
            clearAll();
            txt_userid.Clear();
            txt_userid.Select();

        }


        private void clear()
        {
            txt_userid.Clear();
        }


        private void clearAll()
        {
            ll_index = 0;
            clear();
        }


        private void txt_userid_TextChanged(object sender, EventArgs e)
        {
            shop_enum = EnumShop.Sale;
            searchClientfortransportRent();
        }








        private void detail_datagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            //user cannot delete after 2 days
            /*double days = CommonUtill.no_of_Days( sale_date.Value.Year, sale_date.Value.Month, sale_date.Value.Day);
            if (e.ColumnIndex == 0 && days > Const._Bill_Delete_After_Days)
            {
                MessageBox.Show(ConstMessages._TRANSPORT_TRYTODELETE_OLDBILL);
                return;
            }*/
            if (e.ColumnIndex == 0)
            {
                if (Admin.GetInstance.clients.Count() > 0)
                {

                    // testing file to move//saleParser.moveSaleFromProcesstoSale(date) ; return;
                    bool chk = false;
                    string billkey = detail_datagrid.Rows[index].Cells[12].Value.ToString();
                    Landlord land = Admin.GetInstance.clients.Find(x => x.land_person.pkey == billkey);
                    //if (land.isRecordSaleInserted)
                    {
                        DialogResult dialogResult = MessageBox.Show(
                            Resources.ResourceManager.GetString("1092"),
                            "Delete",
                            MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            //do something
                            string statuslive = lbl_status.Text;
                            if (Authentication.Account.local == "0" || statuslive == "Live")
                            {
                                if (land == null) return;

                                bool checkStatus = new BLogic().checkStatusofSale(billkey);
                                if (checkStatus)
                                {
                                   // ToastNotification.Show(this, "Inovice: " + billkey + " Can Not Deleted. Please Delete First From CashInout then Delete From POS");
                                    return;

                                }
                                //bool updateCheck = new BLogic().p_ud_cust_sale_product(billkey, land, land.category);

                                bool updateCheck = new BLogic().p_sales_delete("DeleteSales", date, billkey);


                                if (updateCheck)
                                {
                                    //ToastNotification.Show(this, Resources.record_delete);
                                    //new BLogic().addExtraAmountClient("DeleteSale", date, land.land_person.pid,
                                    //   (int)land.GetGrandTotal, land.bill_key, land.land_person.pname, 0);
                                    new BLogic().update_today_sales(land.date);
                                    //ToastNotification.Show(this, "Expense Updates...\nBalance Sheet Updated");
                                    btn_refresh_Click(this, new EventArgs());
                                    return;
                                }
                            }
                            else
                            {
                                if (saleParser.DeleteCustomer(billkey))
                                {
                                   // ToastNotification.Show(this, "Expense Updates...\nBalance Sheet Updated");
                                }
                                return;
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            //do something else
                        }
                        return;
                    }
                    if (!Admin.SaveLog)
                    {
                        return;
                    }
                    detail_datagrid.Rows.Clear();
                    detail_datagrid.Refresh();
                    readDailySale(date, "");
                    txt_userid.Select();
                }

            }
            else if (e.ColumnIndex == 1)
            {

                selected_detail_datagrid_index = index;
                templandlord = Admin.GetInstance.clients[index];
                if (templandlord.isRecordSaleInserted)
                {
                    // return;
                }

            }
            else if (e.ColumnIndex == 15)
            {

                SaleDetail sd = new SaleDetail(Admin.GetInstance.clients[index]);
                sd.ShowDialog();
            }
        }




        private void btn_refresh_Click(object sender, EventArgs e)
        {
            detail_datagrid.Rows.Clear();
            detail_datagrid.Refresh();
            init();
        }


        #region Edit Customer Sales
        bool updateCustRec = false;




        #endregion

        int oldData = 0, oldAmount = 0;

        bool chk_user = false;

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

            if ((txt_userid.ContainsFocus || detail_datagrid.ContainsFocus) && detail_datagrid.RowCount > 0 && e.RowIndex < Admin.GetInstance.clients.Count/* && !chk*/)
            {

                selectedRowBipari = detail_datagrid.Rows[e.RowIndex];
                row_selected_landlord = e.RowIndex;

            }
            // if (txt_customerID.ContainsFocus && grid_bipari.RowCount > 0 /*&& !chk*/)
            {
                // selectedRowCustomer = grid_bipari.Rows[e.RowIndex];
            }


            if ((txt_userid.ContainsFocus || detail_datagrid.ContainsFocus) && e.RowIndex < Admin.GetInstance.clients.Count)
            {
                bill_key = selectedRowBipari.Cells[12].Value.ToString();
                name = selectedRowBipari.Cells[2].Value.ToString();
                remainingitems = selectedRowBipari.Cells[5].Value.ToString();
                string cl_id = selectedRowBipari.Cells[13].Value.ToString();
                string billid = selectedRowBipari.Cells[12].Value.ToString();

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

                if (chk_user)
                {
                    SalesAdd sale1 = new SalesAdd(date, cl_id, name, templandlord.client._person_cl.pname, bill_key, remainingitems, templandlord.client._person_cl.pid, billid, lbl_status.Text, getZamidar(templandlord.client._person_cl.pkey));
                    sale1.ShowDialog();
                    chk_user = false;
                }

            }

            if (detail_datagrid.ContainsFocus)
            {
                detail_datagrid_CellClick(sender, e);

            }


        }
        private List<Landlord> getZamidar(string bipari)
        {
            List<Landlord> land = new List<Landlord>();
            for (int i = 0; i < Admin.GetInstance.clients.Count; i++)
            {
                Landlord tem = Admin.GetInstance.clients[i];
                if (tem.client._person_cl.pkey == bipari)
                {
                    land.Add(tem);
                }
            }
            return land;
        }



        Control cntObject;

        // Select DataGridView EditingControlShowing Event


        // TextBox TextChanged Event
        private void check_date_CheckedChanged(object sender, EventArgs e)
        {
            gridColumnVisible();
        }
        private void check_column_CheckedChanged(object sender, EventArgs e)
        {
            gridColumnVisible();
        }

        private void btn_addstock_Click(object sender, EventArgs e)
        {
            using (VendorForm vend = new VendorForm(date, 1, lbl_status.Text))
            {
                DialogResult res = vend.ShowDialog();
                vend.Close();
                detail_datagrid.Refresh();
                detail_datagrid.Rows.Clear();
                init();

                return;
            }
        }

        private void menu_box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_begtype_TextChanged(object sender, EventArgs e)
        {
            shop_enum = EnumShop.BegType;
            searchClientfortransportRent();
        }

        private void sale_date_CloseUp(object sender, EventArgs e)
        {
            date = sale_date.Text;
            saleParser = new SaleParser(date, Admin.SaveLog, Authentication.Account.local == "0" ? false : true);
            detail_datagrid.Rows.Clear();
            detail_datagrid.Refresh();
            init();
        }

        private void btn_movekahta_Click(object sender, EventArgs e)
        {
            MoveKhata m = new MoveKhata(sale_date.Text);
            m.ShowDialog();
        }

        private void link_file_path_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string folderPath = link_file_path.Text; // 🟢 change path as needed

            try
            {
                if (System.IO.Directory.Exists(folderPath))
                {
                    Process.Start("explorer.exe", folderPath);
                }
                else
                {
                      MessageBox.Show("Folder not found: " + folderPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening folder: " + ex.Message);
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
