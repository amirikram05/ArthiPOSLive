using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArthiPOS.controls;
using BAL;
using DataMember;
using ArthiPOS.Utill;
using ArthiPOS.Properties;
using MetroFramework.Controls;
using ArthiPOS.shop;
using ArthiPOS.utill;
using CommonUtilities;

namespace ArthiPOS.Controls.dashboard
{
    public partial class VendorStock : UserControl
    {
        
        private ViewTransport view_transport;
        private BLogic bal;
        private Landlord tempclient;

        private string date;
        public static int duplicateID = 0;
        SaleParser saleParser;
        private Search search;


        public enum SHOP
        {
            Client, Customer, Product, Weight, Type
        };
        public SHOP shop = SHOP.Client;

        public VendorStock()
        {
            InitializeComponent();
            initReady();

        }
        public void initReady()
        {
            bal = new BLogic();
            if (status == "Live")
            {
                lbl_status.Text = "Live";
                lbl_status.BackColor = Color.YellowGreen;
            }
            else
            {
                lbl_status.Text = "Not Live";
                lbl_status.BackColor = Color.DarkOrange;

            }
            date_today.Text = date;
            localizationExtracted();
            chk_box_CheckedChanged(this, new EventArgs());

        }
        private int check = 0;
        private string status = "";
        public VendorStock(string date, int check,string status)
        {
            InitializeComponent();
            this.date = date;
            this.check = check;
            this.status = status;
            initReady();

        }
        private void chk_box_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_box_khata.Checked)
            {
                this.grid_client_detail.Columns[3].Visible = true;
            }
            else
            {
                this.grid_client_detail.Columns[3].Visible = false;

            }
            if (chk_box_bipari.Checked)
            {
                this.grid_client_detail.Columns[5].Visible = true;
            }
            else
            {
                this.grid_client_detail.Columns[5].Visible = false;

            }
            /*if (chk_box_allexpense.Checked)
            {
                this.grid_client_detail.Columns[6].Visible = true;
                this.grid_client_detail.Columns[7].Visible = true;
                this.grid_client_detail.Columns[8].Visible = true;
                this.grid_client_detail.Columns[9].Visible = true;
            }
            else
            {
                this.grid_client_detail.Columns[6].Visible = false;
                this.grid_client_detail.Columns[7].Visible = false;
                this.grid_client_detail.Columns[8].Visible = false;
                this.grid_client_detail.Columns[9].Visible = false;
            }*/
            if (chk_box_bipari_khata.Checked)
            {
                this.grid_client_detail.Columns[13].Visible = true;
            }
            else
            {
                this.grid_client_detail.Columns[13].Visible = false;

            }
            /*if (chk_box_Date.Checked)
            {
                this.grid_client_detail.Columns[10].Visible = true;
            }
            else
            {
                this.grid_client_detail.Columns[10].Visible = false;

            }*/
        }
        #region Update UI Localization
        private void localizationExtracted()
        {
            grid_client_detail.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0012"); //Resources.id;
            grid_client_detail.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0013"); //Resources.key;
            grid_client_detail.Columns[4].HeaderText = Resources.ResourceManager.GetString("a0210"); //Resources.landlord;
            grid_client_detail.Columns[5].HeaderText = Resources.ResourceManager.GetString("a0201"); //Resources.client;
            grid_client_detail.Columns[6].HeaderText = Resources.ResourceManager.GetString("a0401"); //Resources.lbl_quantity;
            grid_client_detail.Columns[7].HeaderText = Resources.ResourceManager.GetString("a0501"); //Resources.lbl_total_rent;
            grid_client_detail.Columns[8].HeaderText = Resources.ResourceManager.GetString("a0502"); //Resources.lbl_total_labour;
            grid_client_detail.Columns[9].HeaderText = Resources.ResourceManager.GetString("a0305"); //Resources.lbl_advance;
            grid_client_detail.Columns[10].HeaderText = Resources.ResourceManager.GetString("a0307"); //Resources.lbl_munshiana;
            grid_client_detail.Columns[11].HeaderText = Resources.ResourceManager.GetString("marketfee"); //Resources.lbl_marketfee;
            grid_client_detail.Columns[12].HeaderText = Resources.ResourceManager.GetString("a0009"); //Resources.date;
            grid_client_detail.Columns[13].HeaderText = Resources.ResourceManager.GetString("a0013"); //Resources.date;
            grid_client_detail.Columns[14].HeaderText = Resources.ResourceManager.GetString("a0306"); //Resources.date;
            grid_client_detail.Columns[15].HeaderText = Resources.ResourceManager.GetString("a0407"); //Resources.date;
            grid_client_detail.Columns[16].HeaderText = Resources.ResourceManager.GetString("a1053"); //Resources.date;
        }
       

        /*public void SearchGrid()
        {
            if (grid_shop.Rows.Count==0)
            {
                return;
            }
            if (shop == SHOP.Product || txt_product_name.ContainsFocus)
            {
                this.grid_shop.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0012");
                this.grid_shop.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0205");
                this.grid_shop.Columns[2].HeaderText = Resources.ResourceManager.GetString("a1031");
            }
            else
            if ((shop == SHOP.Product || txt_client_nameid.ContainsFocus) || (shop == SHOP.Client || txt_landloard_nameid.ContainsFocus)
                ||  (shop == SHOP.Weight || txt_product_weight.ContainsFocus))
            {
                this.grid_shop.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0012");
                this.grid_shop.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0205");
            }
        }*/
        #endregion

        private void addBipariTotalRows(DataRow row)
        {
            int count = this.grid_client_detail.Rows.Count;

            this.grid_client_detail.Rows.Add();
            this.grid_client_detail.Rows[count - 1].Cells[2].Value = "Total"; //Resources.id;
            this.grid_client_detail.Rows[count - 1].Cells[3].Value = ""; //Resources.key;
            this.grid_client_detail.Rows[count - 1].Cells[4].Value = ""; //Resources.landlord;
            this.grid_client_detail.Rows[count - 1].Cells[5].Value = ""; //Resources.client;
            this.grid_client_detail.Rows[count - 1].Cells[6].Value = row[0].ToString(); //Resources.lbl_quantity;
            this.grid_client_detail.Rows[count - 1].Cells[7].Value = row[1].ToString(); //Resources.lbl_total_rent;
            this.grid_client_detail.Rows[count - 1].Cells[8].Value = row[2].ToString(); //Resources.lbl_total_labour;
            this.grid_client_detail.Rows[count - 1].Cells[9].Value = row[4].ToString(); //Resources.lbl_advance;
            this.grid_client_detail.Rows[count - 1].Cells[10].Value = row[3].ToString(); //Resources.lbl_munshiana;
            this.grid_client_detail.Rows[count - 1].Cells[11].Value = row[3].ToString(); //Resources.lbl_munshiana;
            this.grid_client_detail.Rows[count - 1].Cells[12].Value = "";//Resources.date;
            this.grid_client_detail.Rows[count - 1].Cells[13].Value = ""; //Resources.dat
            this.grid_client_detail.Rows[count - 1].Cells[14].Value = row[5].ToString(); ; //Resources.dat
            this.grid_client_detail.Rows[count - 1].Cells[15].Value = row[6].ToString(); ; //Resources.dat
            this.grid_client_detail.Rows[count - 1].Cells[16].Value = row[7].ToString(); ; //Resources.dat
            this.grid_client_detail.Rows[count - 1].DefaultCellStyle.BackColor = Color.Green;
            this.grid_client_detail.Rows[count - 1].DefaultCellStyle.ForeColor = Color.White;

        }
        private void addBipariRows(Landlord land)
        {
            int count = this.grid_client_detail.Rows.Count;

            this.grid_client_detail.Rows.Add();
            this.grid_client_detail.Rows[count - 1].Cells[2].Value = land.land_person.pid.ToString(); //Resources.id;
            this.grid_client_detail.Rows[count - 1].Cells[3].Value = land.land_person.pkey.ToString(); //Resources.key;
            this.grid_client_detail.Rows[count - 1].Cells[4].Value = land.land_person.pname.ToString()+ " - "+ land.land_product._product_name; //Resources.landlord;
            this.grid_client_detail.Rows[count - 1].Cells[5].Value = land.client._person_cl.pname.ToString(); //Resources.client;
            this.grid_client_detail.Rows[count - 1].Cells[6].Value = land.land_product.total_Quantity.ToString(); //Resources.lbl_quantity;
            this.grid_client_detail.Rows[count - 1].Cells[7].Value = land.expense.total_rent.ToString(); //Resources.lbl_total_rent;
            this.grid_client_detail.Rows[count - 1].Cells[8].Value = land.expense.total_labour.ToString(); //Resources.lbl_total_labour;
            this.grid_client_detail.Rows[count - 1].Cells[9].Value = land.land_person.advance.ToString(); //Resources.lbl_advance;
            this.grid_client_detail.Rows[count - 1].Cells[10].Value = land.expense.total_munshiana.ToString(); //Resources.lbl_munshiana;
            this.grid_client_detail.Rows[count - 1].Cells[11].Value = land.expense.total_marketfee.ToString(); //Resources.lbl_munshiana;
            this.grid_client_detail.Rows[count - 1].Cells[12].Value = land.date.ToString(); //Resources.date;
            this.grid_client_detail.Rows[count - 1].Cells[13].Value = land.client._person_cl.pid.ToString(); //Resources.dat
            this.grid_client_detail.Rows[count - 1].Cells[14].Value = land.client._person_cl.expense; //Resources.dat
            this.grid_client_detail.Rows[count - 1].Cells[15].Value = land.land_product.sale_remaining_product; //Resources.dat
            this.grid_client_detail.Rows[count - 1].Cells[16].Value = land.GetTotalService; //Resources.dat
            this.grid_client_detail.Rows[count - 1].Cells[17].Value = land.land_product._product_name; //Resources.Product
        }

        private void VendorStock_Load(object sender, EventArgs e)
        {
            
            saleParser = new SaleParser(date, Admin.SaveLog);
            shop = SHOP.Client;
            //addViews();
            init();

        }
        public void init()
        {
            objectIndex = 0;
            shop = SHOP.Client;
            //new Thread(new ThreadStart(() =>
            {
                
                readDailySale(date);

            }//)).Start();



        }
        public async Task MyMethodAsync()
        {
            Task<int> longRunningTask = LongRunningOperationAsync();
            // independent work which doesn't need the result of LongRunningOperationAsync can be done here

            //and now we call await on the task 
            int result = await longRunningTask;
            //use the result 
            Console.WriteLine(result);
        }

        public async Task<int> LongRunningOperationAsync() // assume we return an int from this long running operation 
        {
            init();
            await Task.Delay(1000); // 1 second delay
            return 1;
        }





       

        #region Business Logic, init


        private void addView(Landlord client, string names)
        {
            view_transport = new ViewTransport(client, names);
            //transport_layout.Controls.Add(view_transport);

        }


        


        #endregion
        #region Control Keys,Events

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Up:
                    if (grid_client_detail.Focused)
                        selectUpRow(grid_client_detail);
                    return true;
                case Keys.Down:
                    if (grid_client_detail.Focused)
                        selectDownRow(grid_client_detail);

                    return true;
                case Keys.F1:
                    //Stuff
                    return true;
                case Keys.Control | Keys.N:
                    //Stuff
                    btn_additems_Click(this, new EventArgs());
                    return true;
                

            }



            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btn_additems_Click(object sender, EventArgs e)
        {
            using (VendorForm vend = new VendorForm(date, 2, status))
            {
                DialogResult res = vend.ShowDialog();
                vend.Close();
                readDailySale(date);
                return;
            }
        }
      


        #endregion

        

         private List<Landlord> tem_ll;

        private void addRowingrid_Clients(Landlord landlord)
        {
            int count = this.grid_client_detail.Rows.Count;

            this.grid_client_detail.Rows.Add();
            this.grid_client_detail.Rows[count - 1].Cells[2].Value = landlord.land_person.pid;
            this.grid_client_detail.Rows[count - 1].Cells[3].Value = landlord.land_person.pkey;
            this.grid_client_detail.Rows[count - 1].Cells[4].Value = landlord.land_person.pname;
            this.grid_client_detail.Rows[count - 1].Cells[5].Value = landlord.client._person_cl.pname;
            this.grid_client_detail.Rows[count - 1].Cells[6].Value = landlord.land_product.total_Quantity;
            this.grid_client_detail.Rows[count - 1].Cells[7].Value = landlord.expense.total_rent;
            this.grid_client_detail.Rows[count - 1].Cells[8].Value = landlord.expense.total_labour;
            this.grid_client_detail.Rows[count - 1].Cells[9].Value = landlord.land_person.advance;
            this.grid_client_detail.Rows[count - 1].Cells[10].Value = landlord.expense.total_munshiana;
            this.grid_client_detail.Rows[count - 1].Cells[11].Value = landlord.expense.total_marketfee;
            this.grid_client_detail.Rows[count - 1].Cells[12].Value = landlord.date;
            this.grid_client_detail.Rows[count - 1].Cells[17].Value = landlord.land_product._product_name;

        }



        #region SelectRow Grid Movement by row
        bool check_TextBox = false;
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
                //showDatainFields(index);
            }
        }
        /*public void showDatainFields(int index)
        {
            int ID = 0;
            try
            {
                ID = Convert.ToInt32(grid_shop.Rows[0].Cells[0].Value.ToString());
            }
            catch (NullReferenceException e)
            {
                return;
            }






            DataGridViewRow selectedRow = grid_shop.Rows[index];
            if (shop == SHOP.Client && txt_client_nameid.ContainsFocus)
            {
                string id = selectedRow.Cells[0].Value.ToString();
                txt_client_nameid.Text = selectedRow.Cells[2].Value.ToString();


            }
            else if (shop == SHOP.Client && txt_landloard_nameid.ContainsFocus)
            {
                txt_landloard_nameid.Text = selectedRow.Cells[2].Value.ToString();
            }
            else if (shop == SHOP.Product)
            {
                string product_id = selectedRow.Cells[0].Value.ToString();
                txt_product_name.Text = selectedRow.Cells[1].Value.ToString();

            }
            else if (shop == SHOP.Weight)
            {
                string weight_id = selectedRow.Cells[0].Value.ToString();
                txt_product_weight.Text = selectedRow.Cells[1].Value.ToString();

            }
            else if (shop == SHOP.Type)
            {
                string type_id = selectedRow.Cells[0].Value.ToString();
                txt_product_type.Text = selectedRow.Cells[1].Value.ToString();
            }
            //txt_drivername.Focus();


        }
        */
        private void selectUpRow(MetroGrid grid)
        {
            MetroGrid dgv = grid;
            int totalRows = dgv.Rows.Count;

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
            grid.Rows[currentrow].Selected = true;

        }

        int currentrow = 0;

        private void selectDownRow(MetroGrid grid)
        {
            MetroGrid dgv = grid;
            int totalRows = dgv.Rows.Count;

            int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
            if (rowIndex == totalRows - 1)
                return;
            int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
            DataGridViewRow selectedRow = dgv.Rows[rowIndex];
            dgv.ClearSelection();
            dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
            grid.FirstDisplayedScrollingRowIndex = rowIndex + 1;
            currentrow++;
            if (currentrow > totalRows)
            {
                currentrow = totalRows;
            }
            grid.Rows[currentrow].Selected = true;
        }


        #endregion

        int pageindex = 1;
        int pageSize = 20;

        int chk = 0;







        bool oneTimeCheck = false;
        private void btn_Add_Click(object sender, EventArgs e)
        {
            int cash = int.Parse(bal.getCapitalCash(RegistryAccess.GetStringRegistryValue(Const.REGKEY, "")));
            if (cash == 0)
            {
                //return;
            }

            if (tempclient != null)
            {
                if (!oneTimeCheck)
                {
                    this.bal.addTodaySales(date);

                    oneTimeCheck = true;
                }

                tempclient = null;
            }
            tempclient = null;

           

        }
        public int objectIndex = 0;
        private void addDriverView()
        {
            if (Admin.GetInstance.clients.Count() == 0)
            {
                return;
            }


            //transport_layout.Visible = false;
            for (int i = objectIndex; i < Admin.GetInstance.clients.Count(); i++)
            {
                Landlord land = Admin.GetInstance.clients[i];
                addRowingrid_Clients(land);

                /*string names = land.client._person_cl.pname;

                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate ()
                    {
                        addView(land, names);
                    });
                }
                else
                {
                    addView(land, names);
                }*/
                objectIndex = Admin.GetInstance.clients.Count();


            }
            //    transport_layout.Visible = true;




            addTotalRow();


        }

        public void addTotalRow()
        {
            string _total="0",_quantity = "0", _rent = "0", _mazdori = "0", _munshiana = "0", _marketfee="0", _advance = "0", _naqdi = "0", _remainingitems = "0";
            // Thread t2 = new Thread(new ThreadStart(() =>
            {



                if (localRecord)
                {
                    int v_total=0,v_quantity = 0, v_rent = 0, v_mazdori = 0, v_munshiana = 0,
                        v_advance = 0, v_naqdi = 0, v_remainingitems = 0, v_marketfee=0;

                    foreach (Landlord land in Admin.GetInstance.clients)
                    {
                        v_quantity += land.land_product.total_Quantity;
                        v_remainingitems += land.land_product.sale_remaining_product;
                        v_rent += land.expense.total_rent;
                        v_mazdori += land.expense.total_labour;
                        v_munshiana += land.expense.total_munshiana;
                        v_marketfee += land.expense.total_marketfee;
                        v_advance += land.expense.total_advance_amount;
                        v_naqdi += land.expense.total_expense;
                        v_total += (int)land.GetTotalService;
                    }
                    _quantity = "" + v_quantity;
                    _remainingitems = "" + v_remainingitems;
                    _rent = "" + v_rent;
                    _mazdori = "" + v_mazdori;
                    _munshiana = "" + v_munshiana;
                    _marketfee = "" + v_marketfee;
                    _advance = "" + v_advance;
                    _naqdi = "" + v_naqdi;
                    _total = "" + v_total;
                    int count = this.grid_client_detail.Rows.Add();
                    this.grid_client_detail.Rows[count].Cells[4].Value="Total";
                    this.grid_client_detail.Rows[count].Cells[6].Value=_quantity;
                    this.grid_client_detail.Rows[count].Cells[7].Value= _rent;
                    this.grid_client_detail.Rows[count].Cells[8].Value=_mazdori;
                    this.grid_client_detail.Rows[count].Cells[9].Value= _advance;
                    this.grid_client_detail.Rows[count].Cells[10].Value= _munshiana;
                    this.grid_client_detail.Rows[count].Cells[11].Value = _marketfee;
                    this.grid_client_detail.Rows[count].Cells[14].Value=_naqdi ;
                    this.grid_client_detail.Rows[count].Cells[15].Value= _remainingitems;
                    this.grid_client_detail.Rows[count].Cells[16].Value= _total;
                    //addBipariTotalRows(newRow1);
                }
                else
                {
                    DataTable dt = (DataTable)bal.getClient_TodayRent_Total(date);
                    DataRow row = dt.Rows[0];
                    addBipariTotalRows(row);

                    _quantity = row[0].ToString();
                    _rent = row[1].ToString();
                    _mazdori = row[2].ToString();
                    _munshiana = row[3].ToString();
                    _advance = row[4].ToString();
                    _naqdi = row[5].ToString();
                    _remainingitems = row[6].ToString();
                }

                
            }//));
            //t2.Start();
        }

        private void txt_landloard_nameid_TextChanged(object sender, EventArgs e)
        {

        }



        

        #region Refresh
        public void refreshUI(List<Landlord> tclients)
        {
            //transport_layout.Controls.Clear();
            grid_client_detail.Rows.Clear();
            grid_client_detail.Refresh();

            if (tclients == null)
            {
                return;
            }
            Admin.GetInstance.clients.Clear();
            Admin.GetInstance.clients = tclients;

            addDriverView();

        }
        bool localRecord = true;
        public void readDailySale(string date)
        {
            List<Landlord> tclients = bal.getLandlordsList(date, "");

            if (Authentication.Account.local == "1")
            {
                #region Load Local Data 

                if (tclients.Count > 0)
                {
                    localRecord = false;
                }
                else
                {
                    //grid_bipari.Columns[0].Visible = false;
                    localRecord = true;
                    tclients = saleParser.LoadTodaySale();
                    List<Landlord> landList = saleParser.LoadTodaySale();
                    if (landList != null)
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

                    }
                }
                #endregion

                //   return;
            }

            {
                grid_client_detail.Rows.Clear();
                grid_client_detail.Refresh();

                if (tclients != null)
                {
                    foreach (Landlord temp in tclients)
                    {
                        addBipariRows(temp);
                    }
                    Admin.GetInstance.clients = tclients;
                    objectIndex = Admin.GetInstance.clients.Count();
                    addTotalRow();
                }
            }


        }

        #endregion



        #region Insert and Delete
        private void updateUIData()
        {
            readDailySale(date);
            tem_ll = null;
        }
        private int deleteSale(string billkey, Landlord land)
        {
            if (saleParser.SAVELOG && saleParser.DeleteLandlord(billkey))
            {
                return 1;
            }
            else
            {
                int count = land.customers.Count;//Temp Comment
                //return bal.deleteRecordTransport(billkey, date, land, count);//Temp Comment
                return bal.deleteRecordTransport(billkey, date, land,1,land.expense.category);
            }
        }
        #endregion
        private void grid_client_detail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;

            //double days = CommonUtill.no_of_Days(trans_date.Value.Year, trans_date.Value.Month, trans_date.Value.Day);
            /*if (e.ColumnIndex == 0 && days > Const._Bill_Delete_After_Days)
            {
                MessageBox.Show(ConstMessages._TRANSPORT_TRYTODELETE_OLDBILL);
                return;
            }*/

            if (e.ColumnIndex == 0)
            {
                if (Admin.GetInstance.clients.Count() > 0)
                {

                    if (Authentication.Account.local == "0" || status=="Live")
                    {
                        if (Admin.GetInstance.clients.Count() > 0)
                        {

                            if ((grid_client_detail.Rows[index].Cells[2].Value.ToString() == null
                                && grid_client_detail.Rows[index].Cells[10].Value.ToString() == null) ||
                                (grid_client_detail.Rows[index].Cells[2].Value.ToString() == ""
                                && grid_client_detail.Rows[index].Cells[10].Value.ToString() == ""
                                && grid_client_detail.Rows[index].Cells[11].Value.ToString() == ""))
                            {
                                return;
                            }
                            string billkey = grid_client_detail.Rows[index].Cells[3].Value.ToString();
                            string date = grid_client_detail.Rows[index].Cells[12].Value.ToString();
                            string bill_id = grid_client_detail.Rows[index].Cells[grid_client_detail.ColumnCount - 1].Value.ToString();

                            Landlord land = Admin.GetInstance.clients.Find(x => x.land_person.pkey == billkey);
                            bool check=bal.p_sales_delete("DeleteSalesAll", date, billkey);

                            //int chk = deleteSale(billkey, land);
                            readDailySale(date);

                        }
                    }
                    else
                    {
                        string billkey = grid_client_detail.Rows[index].Cells[3].Value.ToString();
                        string date = grid_client_detail.Rows[index].Cells[12].Value.ToString();
                        string bill_id = grid_client_detail.Rows[index].Cells[grid_client_detail.ColumnCount - 1].Value.ToString();

                        if (saleParser.DeleteLandlord(billkey))
                        {
                            grid_client_detail.Rows.RemoveAt(index);
                            Admin.GetInstance.clients.RemoveAt(index);
                            readDailySale(date);
                        }
                    }


                }
            }
            else 
            if (e.ColumnIndex == 1)
            {
                //Edit Details
                string billkey = grid_client_detail.Rows[index].Cells[3].Value.ToString();
                string date = grid_client_detail.Rows[index].Cells[12].Value.ToString();
                //string bill_id = grid_client_detail.Rows[index].Cells[grid_client_detail.ColumnCount - 1].Value.ToString()==null?"": grid_client_detail.Rows[index].Cells[grid_client_detail.ColumnCount - 1].Value.ToString();

                Landlord land = Admin.GetInstance.clients.Find(x => x.land_person.pkey == billkey);
                bool updateData = true;
                using (VendorForm vend = new VendorForm(date, 2, status))
                {
                    vend.updateData = true;//For Update Must be True
                    vend.land = land;//assign landlord for update record
                    DialogResult res = vend.ShowDialog();
                    vend.Close();
                    readDailySale(date);
                }
            }
        }

        private void btn_refreshed_Click(object sender, EventArgs e)
        {

            init();
        }

        private void bunifuCustomLabel15_Click(object sender, EventArgs e)
        {

        }

       
        #region OnlyNumeric Enter
        private void Txt_Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }




        #endregion

        private void btn_frieght_detail_Click(object sender, EventArgs e)
        {
            FrightDetail fd = new FrightDetail(this.date);
            fd.ShowDialog();
        }
    }
}
