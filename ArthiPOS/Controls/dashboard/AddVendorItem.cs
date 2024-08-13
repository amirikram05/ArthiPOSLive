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
using ArthiPOS.controls;
using DataMember;
using ArthiPOS.Properties;
using ArthiPOS.utill;
using MetroFramework.Controls;
using ArthiPOS.shop;
using ArthiPOS.Utill;
using CommonUtilities;
using DataMember.memberlog;

namespace ArthiPOS.Controls.dashboard
{
    public partial class AddVendorItem : UserControl
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

        
        public void initReady()
        {
            bal = new BLogic();
            localization();
            ZamidarGridLocalize();

        }
        private int check = 0;
        private string status = "";
        public VendorForm vForm;
        public AddVendorItem(string date, int check,string status)
        {
            InitializeComponent();
            this.date = date;
            this.check = check;
            this.status = status;
            lbl_status.Text = status;
            if (status=="Live")
            {
                lbl_status.Text = "Live";
                lbl_status.BackColor = Color.YellowGreen;
            }else
            {
                lbl_status.Text = "Not Live";
                lbl_status.BackColor = Color.DarkOrange;

            }

            date_today.Text = date;
            objectIndex = Admin.GetInstance.clients.Count();
            initReady();
            AdminLog adlog = LogUtill.getAdminInputLog();
            if (adlog.product == "")
                searchDialog(3, txt_product_name.Text);
            //updatefields(adlog);
            loadLastInputData();
        }
        public void updatefields(AdminLog adlog)
        { txt_bipari_chongi.Text = adlog.client_chongi;
            txt_bipari_commission.Text = adlog.client_commission;
            txt_client_munshiana.Text = adlog.munshiana;
            txt_customer_chongi.Text = adlog.customer_chongi;
            txt_customer_commission.Text = adlog.client_commission;
            txt_driver_rent.Text = adlog._rent_per_product;
            txt_labour.Text = adlog.labour;
            txt_product_name.Text = adlog.product;
            lbl_product_id.Text = adlog.product_id;
            txt_product_weight.Text = adlog.weight;
            lbl_product_weight.Text = adlog.weight_id;
            txt_product_type.Text = adlog._pack;
            txt_marketfee.Text = adlog.marketfee;
        }

        /*public AddVendorItem(string date, int check, string status, Landlord landup, bool updateData) : this(date, check, status)
        {
            InitializeComponent();
            this.landup = landup;
            this.updateData = updateData;
            lbl_status.Text = status;
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


            objectIndex = Admin.GetInstance.clients.Count();
            initReady();
            AdminLog adlog = LoggetAdminInputLog();
            if (adlog.product == "")
                searchDialog(3, txt_product_name.Text);
            
            loadLastInputData();
            updateRecord(landup);

        }*/

        public  void updateRecord(Landlord land,bool updateData,VendorForm vForm)
        {
            //Edit Data
            //throw new NotImplementedException();
            //txt_client_nameid.Text = land.client._person_cl.pname;
            this.landup = land;
            this.updateData = updateData;
            this.vForm = vForm;
            if (land == null)
                return;
            

        }

        private void loaddataValues()
        {
            if (landup==null)
            {
                return;
            }
            lbl_landlorid.Text=landup.land_person.pkey;
            lbl_bipariid.Text = landup.client._person_cl.pkey;
            txt_client_nameid.Text = landup.client._person_cl.pname;
            lbl_client_id.Text = landup.client._person_cl.pid;
            txt_landloard_nameid.Text = landup.land_person.pname;
            lbl_ll_client_id.Text = landup.land_person.pid;
            lbl_client_id.Text = landup.client._person_cl.pid;
            txt_vehicle_id.Text = landup.client._vehicle_id;
            lbl_product_id.Text = landup.land_product._product_id;
            txt_product_name.Text = landup.land_product._product_name;
            lbl_weight_id.Text = landup.land_product._weight_id;
            txt_product_weight.Text = landup.land_product._weight;
            txt_product_type.Text = landup.land_product._type;
            txt_product_quantity.Text = landup.land_product.total_Quantity + "";
            txt_product_quantity.Enabled = false;
            txt_bipari_chongi.Text = landup.service.client_chongi + "";
            txt_bipari_commission.Text = landup.service.commission_client_product + "";
            txt_customer_chongi.Text = landup.service.customer_chongi + "";
            txt_customer_commission.Text = landup.service.commission_customer_product + "";
            txt_driver_rent.Text = landup.service.rent_per_product + "";
            txt_labour.Text = landup.service.labour_per_product + "";
            txt_total_labour.Text = landup.expense.total_labour + "";
            txt_total_rent.Text = landup.expense.total_rent + "";
            txt_client_munshiana.Text = landup.expense.total_munshiana + "";
            txt_client_advance.Text = landup.expense.total_advance_amount + "";
            txt_driver_expense.Text = landup.expense.total_expense + "";
            btn_Add.LabelText = "Update";

        }
        #region Update UI Localization
        public void localization()
        {
            lbl_bipari_setting.Text = Resources.ResourceManager.GetString("a0208");
            lbl_customer_setting.Text = Resources.ResourceManager.GetString("a0202");
            lbl_bipari_chongi.Text = Resources.ResourceManager.GetString("a0301");
            lbl_bipari_commisison.Text = Resources.ResourceManager.GetString("a0302");
            lbl_customer_chongi.Text = Resources.ResourceManager.GetString("a0301");
            lbl_customer_commission.Text = Resources.ResourceManager.GetString("a0302");
            lbl_labour.Text = Resources.ResourceManager.GetString("a0303");
            lbl_rent_car.Text = Resources.ResourceManager.GetString("a0304");
            lbl_vehicle_no.Text = Resources.ResourceManager.GetString("a1030");
            lbl_bipari_name.Text = Resources.ResourceManager.GetString("a0207");
            lbl_landlord.Text = Resources.ResourceManager.GetString("a0201");
            lbl_quantity.Text = Resources.ResourceManager.GetString("a0401");

            lbl_total_rent.Text = Resources.ResourceManager.GetString("a0501");
            lbl_total_labour.Text = Resources.ResourceManager.GetString("a0502");
            lbl_munshiana.Text = Resources.ResourceManager.GetString("a0307");
            lbl_expense.Text = Resources.ResourceManager.GetString("a1095");
            lbl_product_name.Text = Resources.ResourceManager.GetString("a0206");
            lbl_product_weight.Text = Resources.ResourceManager.GetString("a0403");
            lbl_product_packing.Text = Resources.ResourceManager.GetString("a1031");
            lbl_marka.Text = Resources.ResourceManager.GetString("a2014");
            lbl_advance.Text = Resources.ResourceManager.GetString("a305");

            /*lbl_total_Begs.Text = Resources.ResourceManager.GetString("a0401");
            lbl_rent.Text = Resources.ResourceManager.GetString("a0304");
            lbl_totalmazdori.Text = Resources.ResourceManager.GetString("a0303");
            lbl_totalmunhiana.Text = Resources.ResourceManager.GetString("a0307");
            lbl_reskharcha.Text = Resources.ResourceManager.GetString("a0311");
            _lbl_advance.Text = Resources.ResourceManager.GetString("a0305");
            lbl_advance.Text = Resources.ResourceManager.GetString("a0305");
            lbl_remaining_prodcut.Text = Resources.ResourceManager.GetString("a0407");
            check_settings.Text = Resources.ResourceManager.GetString("a1036");*/




            grid_landlords.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0012"); //Resources.id;
            grid_landlords.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0201"); // Resources.landlord;
            grid_landlords.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0401"); //Resources.lbl_quantity;
            grid_landlords.Columns[4].HeaderText = Resources.ResourceManager.GetString("a0304"); // Resources.lbl_rent;
            grid_landlords.Columns[5].HeaderText = Resources.ResourceManager.GetString("a0303"); // Resources.lbl_advance;
            grid_landlords.Columns[6].HeaderText = Resources.ResourceManager.GetString("a0305"); // Resources.lbl_labour;
            grid_landlords.Columns[7].HeaderText = Resources.ResourceManager.GetString("a0307"); // Resources.lbl_munshiana;
            grid_landlords.Columns[8].HeaderText = Resources.ResourceManager.GetString("a0306"); // Resources.bipari_chongi;
                                                                                                 // grid_landlords.Columns[9].HeaderText = Resources.ResourceManager.GetString("a1034"); // Resources.bipari_commssion;
                                                                                                 //grid_landlords.Columns[10].HeaderText = Resources.ResourceManager.GetString("a1033"); // Resources.cust_chongi;
                                                                                                 //grid_landlords.Columns[11].HeaderText = Resources.ResourceManager.GetString("a1032"); // Resources.cust_commission;
            grid_landlords.Columns[9].HeaderText = Resources.ResourceManager.GetString("a1053"); // Resources.cust_commission;

        }
        
        #endregion

        
        

        private void AddVendorItem_Load(object sender, EventArgs e)
        {
            saleParser = new SaleParser(date, Admin.SaveLog);
            if (Authentication.Account.local == "1")
            {
                saleParser.SAVELOG = true;
            }
            txt_bipari_chongi.Select();
            shop = SHOP.Client;
            //addViews();
            init();
            objectIndex = Admin.GetInstance.clients.Count();
            loaddataValues();
        }
        public void init()
        {
            circularProgress1.Visible = true;
            objectIndex = 0;
            shop = SHOP.Client;
            loadLastInputData();
            DriverList(date);



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
            circularProgress1.Visible = false;
            return 1;
        }





        public void loadLastInputData()
        {
            AdminLog log = AdminLog.Load();
            txt_customer_commission.Text = log.customer_commission;
            txt_bipari_commission.Text = log.client_commission;
            txt_bipari_chongi.Text = log.client_chongi;
            txt_customer_chongi.Text = log.customer_chongi;
            txt_labour.Text = log.labour;
            txt_client_munshiana.Text = log.munshiana;
            txt_product_name.Text = log.product;
            lbl_product_id.Text = log.product_id;
            lbl_weight_id.Text = log.weight_id;
            txt_product_weight.Text = log.weight;
            txt_driver_rent.Text = log._rent_per_product;
            txt_product_type.Text = log._pack;
            txt_marketfee.Text = log.marketfee;

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
                    if (grid_landlords.Focused)
                        selectUpRow(grid_landlords);
                    return true;
                case Keys.Down:
                    if (grid_landlords.Focused)
                        selectDownRow(grid_landlords);

                    return true;
                case Keys.F1:
                    //Stuff
                    return true;
                case Keys.Shift | Keys.Enter:
                    //Stuff
                    btn_calculate_Click_1(this, new EventArgs());
                    txt_product_name.Select();
                    return true;
                case Keys.Alt | Keys.Down:
                    //Stuff
                    return true;
                case Keys.Enter:

                    {

                        if (txt_product_quantity.ContainsFocus)
                        {
                            string renttxt = txt_driver_rent.Text;
                            if (renttxt == "")
                            {
                                txt_driver_rent.Text = "0";
                            }
                            else
                            {
                                if (txt_product_quantity.Text == "" || txt_product_quantity.Text == null)
                                {
                                    return true;
                                }



                                calculateService();
                            }
                        }
                        else
                        if (txt_total_rent.ContainsFocus)
                        {
                            // if (txt_driver_rent.Text == "" || txt_driver_rent.Text == "0")
                            {
                                string rent_per_product = "";
                                string total_rent = txt_total_rent.Text;

                                string t_quantity = txt_product_quantity.Text;

                                if (total_rent == "" || t_quantity=="")
                                {
                                    return true;
                                }


                                int rent = int.Parse(total_rent);
                                int quantity = int.Parse(t_quantity);
                                if (txt_total_rent.Text != "")
                                {
                                    rent_per_product = "" + (rent / quantity);
                                    //txt_driver_rent.Text = rent_per_product;
                                }
                            }
                        }
                        else
                            btn_search_Click(this, new EventArgs());
                        /*if(txt_client_nameid.ContainsFocus)
                        {
                            //txt_client_nameid_Enter(this, new EventArgs());
                            searchDialog(1, txt_client_nameid.Text);
                        }
                        else
                        if (txt_landloard_nameid.ContainsFocus)
                        {
                            //txt_landloard_nameid_FocusEnter(this, new EventArgs());
                            searchDialog(1, txt_landloard_nameid.Text);
                        }
                        else
                        if (txt_product_weight.ContainsFocus)
                        {
                            //txt_landloard_nameid_FocusEnter(this, new EventArgs());
                            searchDialog(4, txt_product_weight.Text);
                        }else
                        if (txt_product_type.ContainsFocus)
                        {
                            //txt_landloard_nameid_FocusEnter(this, new EventArgs());
                            searchDialog(5, txt_product_type.Text);
                        }*/

                        currentrow = 0;
                    }


                    changetxtBoxFocus();

                    return true;
                case Keys.Control | Keys.Enter:
                    btn_Add.colorActive = Color.MediumSeaGreen;
                    btn_Add_Click(this, new EventArgs());

                    return true;
                case Keys.Alt | Keys.Enter:
                    return true;


            }



            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void calculateService()
        {
            string labour_per_product = txt_labour.Text;
            string rent_per_product = txt_driver_rent.Text;
            string t_quantity = txt_product_quantity.Text;
            if (t_quantity == "")
            {
                return;
            }
            int labour = int.Parse(labour_per_product.Trim());
            int rent = int.Parse(rent_per_product.Trim());
            int quantity = int.Parse(t_quantity.Trim());
            txt_total_labour.Text = "" + (labour * quantity);
            txt_total_rent.Text = "" + (rent * quantity);
        }


        #endregion

        #region Focus Change

        public void colorChange(
             Color col_customer_chongi,
            Color col_bipari_chongi,
            Color col_bipari_commission,
            Color col_driver_rent,
            Color col_labour,
            Color col_customer_commission,
            Color col_vehicle_id,
            Color col_client_nameid,
            Color col_landloard_nameid,

            Color col_product_quantity,
            Color color_mark,
            Color col_product_type,
            Color col_product_name,
            Color col_product_weight,
            Color col_total_labour,
            Color col_total_rent,
            Color col_driver_expense,
            Color col_client_munshiana,
            Color col_client_advance
            )
        {
            txt_customer_chongi.BackColor = col_customer_chongi;
            txt_bipari_chongi.BackColor = col_bipari_chongi;
            txt_bipari_commission.BackColor = col_bipari_commission;
            txt_driver_rent.BackColor = col_driver_rent;
            txt_labour.BackColor = col_labour;
            txt_customer_commission.BackColor = col_customer_commission;
            txt_vehicle_id.BackColor = col_vehicle_id;
            txt_client_nameid.BackColor = col_client_nameid;
            txt_product_quantity.BackColor = col_product_quantity;
            txt_mark.BackColor = color_mark;
            txt_product_type.BackColor = col_product_type;
            txt_product_name.BackColor = col_product_name;
            txt_product_weight.BackColor = col_product_weight;
            txt_total_labour.BackColor = col_total_labour;
            txt_total_rent.BackColor = col_total_rent;
            txt_driver_expense.BackColor = col_driver_expense;
            txt_client_munshiana.BackColor = col_client_munshiana;
            txt_client_advance.BackColor = col_client_advance;
            txt_landloard_nameid.BackColor = col_landloard_nameid;
        }
        public void changetxtBoxFocus()
        {
            #region Focus Change
            if (txt_bipari_chongi.ContainsFocus)
            {
                
                colorChange(Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White, Color.White, Color.White, Color.White,
                    Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, 
                    Color.White, Color.White, Color.White, Color.White, Color.White);

                txt_bipari_commission.Select();

            }
            else if (txt_bipari_commission.ContainsFocus)
            {
                
                colorChange(Color.White, Color.White, Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White,
                    Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White,
                    Color.White, Color.White, Color.White, Color.White, Color.White);
                txt_labour.Select();

            }
            else if (txt_labour.ContainsFocus)
            {
                colorChange(Color.White, Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White,Color.White,
                    Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White,
                    Color.White, Color.White, Color.White, Color.White, Color.White);
               
                txt_driver_rent.Select();


            }
            else if (txt_driver_rent.ContainsFocus)
            {
                colorChange(Color.FromArgb(0xEE, 0xEE, 0xEE),Color.White, Color.White, Color.White , Color.White, Color.White,
                   Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White,
                   Color.White, Color.White, Color.White, Color.White, Color.White);
                
                txt_customer_chongi.Select();


            }
            else if (txt_customer_chongi.ContainsFocus)
            {
                colorChange(Color.White, Color.White, Color.White, Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE),
                  Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White,
                  Color.White, Color.White, Color.White, Color.White, Color.White);

                txt_customer_commission.Select();

            }
            else if (txt_customer_commission.ContainsFocus)
            {
                colorChange(Color.White, Color.White, Color.White, Color.White,
                  Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White,
                  Color.White, Color.White, Color.White, Color.White, Color.White, Color.White,
                  Color.White, Color.White, Color.White, Color.White, Color.White);
                txt_vehicle_id.Select();
                

            }
            else if (txt_vehicle_id.ContainsFocus)
            {
                shop = SHOP.Client;
                colorChange(Color.White, Color.White, Color.White, Color.White,
                 Color.White, Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), 
                 Color.White, Color.White, Color.White, Color.White, Color.White, Color.White,
                 Color.White, Color.White, Color.White, Color.White, Color.White);
                txt_client_nameid.Select();

            }
            else if (txt_client_nameid.ContainsFocus)
            {
                shop = SHOP.Client;
                colorChange(Color.White, Color.White, Color.White, Color.White,
                 Color.White, Color.White, Color.White, Color.White, Color.White
                 , Color.White, Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White, Color.White,
                 Color.White, Color.White, Color.White, Color.White);

                if (clkey == "")
                {
                    string idCheck = checkClientIDExit(lbl_client_id.Text, "");
                    if (idCheck == "")
                        clkey = bal.p_getInvoiceID("Bip", lbl_client_id.Text, date);//Bip
                    else
                        clkey = idCheck;
                }
                lbl_bipariid.Text = clkey;
                txt_product_name.Select();

            }
            else if (txt_landloard_nameid.ContainsFocus)
            {
                colorChange(Color.White, Color.White, Color.White, Color.White,
                 Color.White, Color.White, Color.White, Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE)
                 , Color.White, Color.White, Color.White, Color.White, Color.White,
                 Color.White, Color.White, Color.White, Color.White);


                string llkey = checkClientIDExit("", lbl_ll_client_id.Text);//Zam
                if (llkey == "")
                    llkey = bal.p_getInvoiceID("Zam", lbl_ll_client_id.Text, date);
                else
                { 
                    DialogResult dialogResult = MessageBox.Show("Zamidar invoice already exist. Do you want Generate New ID ?", "New Invoice", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        llkey = bal.p_getInvoiceID("Zam", lbl_ll_client_id.Text, date);
                        lbl_landlorid.Text = llkey;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                txt_product_quantity.Select();

            }
            else if (txt_product_quantity.ContainsFocus)
            {

                colorChange(Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White
                , Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White);
                txt_mark.Select();

            }
            else if (txt_mark.ContainsFocus)
            {


                colorChange(Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White
                , Color.White, Color.White, Color.White, Color.White, Color.White, Color.White,
                Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White, Color.White, Color.White);

                txt_total_rent.Select();

            }

            else if (txt_total_rent.ContainsFocus)
            {
                colorChange(Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White
                , Color.White, Color.White, Color.White, Color.White, Color.White,
                 Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White, Color.White, Color.White, Color.White);


                txt_total_labour.Select();


            }
            else if (txt_total_labour.ContainsFocus)
            {
                colorChange(Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White
                , Color.White, Color.White, Color.White, Color.White, Color.White,
                  Color.White, Color.White, Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE));
                txt_client_advance.Select();


            }
            else if (txt_product_name.ContainsFocus)
            {

                shop = SHOP.Weight;
                colorChange(Color.White, Color.White, Color.White, Color.White,
                 Color.White, Color.White, Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE)
                 , Color.White, Color.White, Color.White, Color.White, Color.White, Color.White,
                 Color.White, Color.White, Color.White, Color.White);
               
                txt_landloard_nameid.Select();

            }
            else if (txt_product_weight.ContainsFocus)
            {

                colorChange(Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White
                , Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White, Color.White, Color.White,
                  Color.White, Color.White, Color.White, Color.White);

               txt_product_type.Select();
            }

            else if (txt_client_munshiana.ContainsFocus)
            {
                shop = SHOP.Product;
                colorChange(Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White
                , Color.White, Color.White, Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White,
                  Color.White, Color.White, Color.White, Color.White);

                txt_product_weight.Select();


            }
            else if (txt_product_type.ContainsFocus)
            {
                colorChange(Color.White,Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White
                , Color.White, Color.White, Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White,
                  Color.White, Color.White, Color.White, Color.White);

                btn_calculate.Select();

            }
            else if (txt_client_advance.ContainsFocus)
            {
                colorChange(Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White
                , Color.White, Color.White, Color.White, Color.White, Color.White,
                  Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White, Color.White);

                
                txt_driver_expense.Select();


            }
            else if (txt_driver_expense.ContainsFocus)
            {
                colorChange(Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White
                , Color.White, Color.White, Color.White, Color.White, Color.White, Color.White,
                  Color.White, Color.White, Color.FromArgb(0xEE, 0xEE, 0xEE), Color.White);

                txt_client_munshiana.Select();

            }
            else if (btn_calculate.ContainsFocus)
            {
                // AlertMsg.Show("Record Calclulated", AlertMsg.AlertType.info);
                btn_calculate_Click_1(this, new EventArgs());
                txt_landloard_nameid.BackColor = Color.FromArgb(0xEE, 0xEE, 0xEE); ;
                txt_bipari_chongi.BackColor = Color.White;
                txt_bipari_commission.BackColor = Color.White;
                txt_driver_rent.BackColor = Color.White;
                txt_labour.BackColor = Color.White;
                txt_customer_chongi.BackColor = Color.White;
                txt_customer_commission.BackColor = Color.White;
                txt_vehicle_id.BackColor = Color.White;
                txt_client_nameid.BackColor = Color.White;
                txt_product_quantity.BackColor = Color.White;
                txt_mark.BackColor = Color.White;
                txt_product_type.BackColor = Color.White;
                txt_product_name.BackColor = Color.White;
                txt_product_weight.BackColor = Color.White;
                txt_total_labour.BackColor = Color.White;
                txt_total_rent.BackColor = Color.White;
                txt_driver_expense.BackColor = Color.White;
                txt_client_munshiana.BackColor = Color.White;
                txt_client_advance.BackColor = Color.White;
                txt_product_name.Select();

            }
            #endregion

        }
        #endregion

        #region Check Object Empty
        private bool checkNotEmpty(ArthiPOS.Controls.UrduTextBox textbox)
        {
            if (textbox.Text != "")
            {
                return true;
            }
            else
            {
                textbox.Select();
                return false;
            }

        }
        #endregion

        #region Controls Event Listners
        public Predicate<Landlord> Byid(string id)
        {
            return delegate (Landlord landlord)
            {
                return landlord.land_person.pid == id;
            };
        }
        public Predicate<Landlord> ByKey(string key)
        {
            return delegate (Landlord landlord)
            {
                return landlord.land_person.pkey == key;
            };
        }
        private string clkey="";
        private void btn_calculate_Click_1(object sender, EventArgs e)
        {



            Services services = new Services();

            if (checkNotEmpty(txt_bipari_commission) && checkNotEmpty(txt_labour)
                && checkNotEmpty(txt_customer_chongi))
            {
                services.client_chongi = float.Parse(txt_bipari_chongi.Text);
                services.customer_chongi = float.Parse(txt_customer_chongi.Text);
                services.commission_client_product = float.Parse(txt_bipari_commission.Text);
                services.commission_customer_product = float.Parse(txt_customer_commission.Text);
                services.labour_per_product = float.Parse(txt_labour.Text);
                services.clerk_per_bill = float.Parse(txt_client_munshiana.Text);
                services.marketfee = float.Parse(txt_marketfee.Text);
                services.rent_per_product = float.Parse(txt_driver_rent.Text);

            }


            if (checkNotEmpty(txt_client_nameid) &&
                checkNotEmpty(txt_product_name) &&
                checkNotEmpty(txt_product_quantity) &&
                checkNotEmpty(txt_product_weight) &&
                checkNotEmpty(txt_total_rent) &&
                checkNotEmpty(txt_total_labour))
            {


                string _vehicle_id = txt_vehicle_id.Text;
                string _id = lbl_client_id.Text;
                string _clientnameid = txt_client_nameid.Text;
                string _ll_id = lbl_ll_client_id.Text;
                string _landloardnameid = txt_landloard_nameid.Text;
                string _product_id = lbl_product_id.Text;
                string _product_name = txt_product_name.Text;
                string _quantity = txt_product_quantity.Text;
                string _type = txt_product_type.Text;
                string _weight = txt_product_weight.Text;
                string _rent = txt_total_rent.Text;
                string _rent_per_product = txt_driver_rent.Text;
                string _labour = txt_total_labour.Text;
                string _advance = txt_client_advance.Text;
                string _expense = txt_driver_expense.Text;
                string _weight_id = lbl_weight_id.Text;
                string marka = txt_mark.Text;


                if (_id == "")
                {
                    txt_client_nameid.Select();
                    return;
                }
                if (_ll_id == "")
                {
                    txt_landloard_nameid.Select();
                    return;
                }

                if (_product_id == "")
                {
                    txt_product_name.Select();
                    return;
                }





                //save log
                LogUtill.loadLastUseInputs_TransportForm("" + services.client_chongi,
                    "" + services.commission_client_product,
                    "" + services.customer_chongi,
                    "" + services.commission_customer_product, "" + services.labour_per_product,
                     "" + services.clerk_per_bill,
                    _product_id, _product_name, _weight_id, _weight, _rent_per_product, _type, "" + services.marketfee);

                if (txt_landloard_nameid.Text == "")
                {
                    _ll_id = lbl_client_id.Text;
                    _landloardnameid = txt_client_nameid.Text;
                }

                if (_expense == "")
                {
                    _expense = "0";
                }

                if (_advance == "")
                {
                    _advance = "0";
                }



                /* int remainingProduct = getRemaingProduct("date");

                 if (remainingProduct <int.Parse(_quantity))
                 {
                     MessageBox.Show(remainingProduct+" "+ shop.ConstMessages._TRANSPORT_MESSAGE);
                     return;
                 }*/
               
                if (lbl_bipariid.Text == "" ||lbl_bipariid.Text=="0") { MessageBox.Show("Bipari Invoice ID Missing"); return;}
                if (lbl_landlorid.Text == "" || lbl_landlorid.Text=="0") { MessageBox.Show("Zamidar Invoice ID Missing"); return;}

                
                


                string _pid = "";
                string _pkey = clkey;//BillKey.getBillID(BillKey.EnumUser.Client, date, _pid, 0);
                string _pname = "";
                string _phone = "";
                if (tempclient == null)
                {
                    tempclient = new Landlord();

                }
                tempclient.date = date;
                tempclient.client.date = date;
                tempclient.client._vehicle_id = _vehicle_id;

                Product product = new Product(_product_id, _product_name,
                   _type, _weight_id, _weight, int.Parse(_quantity), marka);

                Landlord land = new Landlord();
                land.date = date;
                string tpKey = lbl_landlorid.Text == "" || lbl_landlorid.Text == "0" ? "0" : lbl_landlorid.Text; //checkKey(date, _ll_id, 0,llkey);
                if (tpKey == "" || tpKey == "0")
                {
                    txt_landloard_nameid.Select();
                    return;
                }

                land.bill_key = tpKey;
                land.expense.category = nameof(BillKey.EnumUser.Client);
                land.expense.total_rent = int.Parse(_rent);
                land.expense.total_labour = int.Parse(_labour);
                land.expense.total_munshiana = (int)services.clerk_per_bill;
                land.expense.total_marketfee = (int)services.marketfee;
                land.expense.total_expense = int.Parse(_expense);
                land.expense.total_advance_amount = int.Parse(_advance);
                land.Total_Chongi = int.Parse(_quantity) * services.client_chongi;



                //for client
                _pid = _id;
                _pname = _clientnameid;
                _pkey = clkey;//BillKey.getBillID(BillKey.EnumUser.Client, date, _pid, 0);//CommongetKey(_pid, "CL", date); //Date-cl-cl_id

                _phone = "";

                Person cl_person = new Person(_pid, _pkey, _pname, _phone, 0, int.Parse(_expense));

                land.client._person_cl = cl_person;
                land.client._product = product;
                land.client._services = services;




                if (!checkNotEmpty(txt_landloard_nameid))
                {

                    //tempclient._person_cl = cl_person;
                    //tempclient._services = services;
                    //tempclient._product = product;
                    //tempclient.has_Own_Products = true;
                    //tempclient.sale = sale;
                    //addRowingrid_Clients(tempclient);


                }
                //Landloard we will add when we add sale on POS

                {


                    // for landlord
                    _pid = _ll_id;
                    _pname = _landloardnameid;
                    _pkey = tpKey;//BillKey.getBillID(BillKey.EnumUser.LandLoard, date, _pid, duplicateID);//CommongetKey(_pid, "LL", date); //Date-cl-cl_id

                    _phone = "";
                    Person landperson = new Person(_pid, _pkey, _pname, _phone, int.Parse(_advance), 0);


                    land.service = services;
                    land.land_person = landperson;
                    land.land_product = product;
                    if (!updateData)
                    {
                        tem_ll.Add(land);
                        Admin.GetInstance.clients.Add(land);
                        addRowingrid_landlords(land);
                    }else
                    {
                        if (tem_ll==null)
                        {
                            tem_ll = new List<Landlord>();
                        }
                        if (grid_landlords.Rows.Count <= 1)
                        {
                            addRowingrid_landlords(land);
                        }else
                        {
                            MessageBox.Show("Add New Bipri/Landlord Not Allowed.");
                            return;
                        }
                    }
                    tempclient = land;
                    AdminLog log = AdminLog.Load();
                    txt_driver_rent.Text = log._rent_per_product;
                }
                btn_Add.Focus();


                clear();

            }

        }

        private string checkKey(string date,string _ll_id,int dupID)
        {
            string key=BillKey.getBillID(BillKey.EnumUser.LandLoard, date, _ll_id, dupID);
            Landlord templand = Admin.GetInstance.clients.Find(ByKey(key));

            if (templand != null)
            {

                ++duplicateID;
                key=checkKey(date,_ll_id, duplicateID);
            }
            return key;
        }

        private List<Landlord> tem_ll;

        private void addRowingrid_landlords(Landlord landlord)
        {
            int count = this.grid_landlords.Rows.Count;

            this.grid_landlords.Rows.Add();
            this.grid_landlords.Rows[count - 1].Cells[1].Value = landlord.land_person.pid;
            this.grid_landlords.Rows[count - 1].Cells[2].Value = landlord.land_person.pname;
            this.grid_landlords.Rows[count - 1].Cells[3].Value = landlord.land_product.total_Quantity;
            this.grid_landlords.Rows[count - 1].Cells[4].Value = landlord.expense.total_rent;
            this.grid_landlords.Rows[count - 1].Cells[5].Value = landlord.expense.total_labour;
            this.grid_landlords.Rows[count - 1].Cells[6].Value = landlord.land_person.advance;
            this.grid_landlords.Rows[count - 1].Cells[7].Value = landlord.expense.total_munshiana;
            this.grid_landlords.Rows[count - 1].Cells[8].Value = landlord.expense.total_expense;
            this.grid_landlords.Rows[count - 1].Cells[9].Value = landlord.GetTotalService + landlord.expense.total_expense;
            //this.grid_landlords.Rows[count - 1].Cells[10].Value = landlord.service.customer_chongi;
            //this.grid_landlords.Rows[count - 1].Cells[11].Value = landlord.service.commission_customer_product;
            //this.grid_landlords.Rows[count - 1].Cells[12].Value = landlord.GetTotalService+landlord.expense.total_expense;


        }
        
        public void clear()
        {
            txt_landloard_nameid.Clear();
            txt_total_labour.Clear();
            txt_total_rent.Clear();
            txt_product_quantity.Clear();
            //txt_product_type.Clear();
            //txt_product_wight.Clear();
            txt_driver_expense.Clear();
            txt_client_advance.Clear();
            lbl_ll_client_id.Text = "";

        }

        #endregion

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
            //int cash=int.Parse(bal.getCapitalCash(RegistryAccess.GetStringRegistryValue(Const.REGKEY, "")));
            //if (cash==0)
            //{
            //return;
            //}

            clkey = bal.p_getInvoiceID("Bip", lbl_client_id.Text,date);



            if (updateData)
            {

                if (tempclient==null)
                {
                    btn_calculate_Click_1(this, new EventArgs());
                    return;
                }
                tempclient.expense.category = nameof(BillKey.EnumUser.Client);
                landup.expense.category=nameof(BillKey.EnumUser.Client);

                int remQuantity = tempclient.land_product.sale_remaining_product - landup.land_product.sale_remaining_product;
                if (remQuantity > 0)
                {
                    tempclient.land_product.sale_remaining_product = remQuantity;
                    tempclient.status = EStatus.InComplete;//Enum.GetName(typeof(EStatus), 0);
                }
                else
                {

                }

                if (Authentication.Account.local == "0" || status == "Live")
                {
                    //Update Record Live 
                    //btn_calculate_Click_1(this,new EventArgs());
                    bal.updateDailyRecord(tempclient);
                }
                else
                {
                    //Update Local Record
                    //btn_calculate_Click_1(this, new EventArgs());
                    tempclient.status = EStatus.Initial;
                    saleParser.updateLandLord(landup, tempclient);

                }
                clearandLoad();
                this.vForm.Close();
                return;
            }
            if (tempclient != null)
            {
                insertSales(tempclient);
                tempclient = null;
                clkey = "";
            }
            clearandLoad();
            clkey = "";
        }

        private void clearandLoad()
        {
            tempclient = null;
            clear();
            txt_vehicle_id.Clear();
            txt_vehicle_id.Select();
            loadLastInputData();
        }

        public int objectIndex = 0;
        private Landlord landup;
        private bool updateData;

        public void searchDialog(int action, string searchTxt)
        {


            using (search = new Search(action, searchTxt,1))
            {


                DialogResult res = search.ShowDialog();
                if (action == 1)
                {
                    if (txt_client_nameid.ContainsFocus)
                    {
                        txt_client_nameid.Text = search.Name;
                        lbl_client_id.Text = search.Id;
                        if (tem_ll == null)
                        {
                            tem_ll = new List<Landlord>();
                        }
                    }
                    else if (txt_landloard_nameid.ContainsFocus)
                    {
                        txt_landloard_nameid.Text = search.Name;
                        lbl_ll_client_id.Text = search.Id;

                    }
                }
                else
                if (action == 3)
                {
                    txt_product_name.Text = search.Name;
                    lbl_product_id.Text = search.Id;
                    txt_labour.Text = search.Labour;
                    txt_driver_rent.Text = search.Rent;
                    txt_bipari_commission.Text = search.BipComm;
                    txt_customer_commission.Text = search.CusComm;
                    txt_bipari_chongi.Text = search.Laga;
                    txt_customer_chongi.Text = search.Chongi;
                    txt_product_type.Text = search.Type;
                    txt_client_munshiana.Text = search.Munshiana;
                    txt_marketfee.Text = search.MarketFee;
                    calculateService();
                }
                else
                if (action == 4)
                {
                    txt_product_weight.Text = search.Name;
                    lbl_weight_id.Text = search.Id;

                }
                else
                if (action == 5)
                {
                    txt_product_name.Text = search.Name;
                }
                search.Close();



                return;
            }
        }
        /*
          private void txt_client_nameid_Enter(object sender, EventArgs e)
         {
             shop = SHOP.Client;
             using (search = new Search(1, txt_client_nameid.Text))
             {
                 DialogResult res = search.ShowDialog();
                     txt_client_nameid.Text = search.Name;
                     lbl_client_id.Text = search.Id;
                     if (tem_ll == null)
                     {
                         tem_ll = new List<Landlord>();
                     }
                     search.Close();

                 return;
             }
         }
         private void txt_landloard_nameid_FocusEnter(object sender, EventArgs e)
         {
             shop = SHOP.Client;
             using (search = new Search(1, txt_landloard_nameid.Text))
             {
                 DialogResult res = search.ShowDialog();
                 txt_landloard_nameid.Text = search.Name;
                 lbl_ll_client_id.Text = search.Id;
                 search.Close();
             }
         }
         private void txt_product_name_FocusEnter(object sender, EventArgs e)
         {

             using (search = new Search(3, txt_product_name.Text))
             {
                 DialogResult res = search.ShowDialog();
                     txt_product_name.Text = search.Name;
                     lbl_product_id.Text = search.Id;
                     txt_labour.Text = search.Labour;
                     txt_driver_rent.Text = search.Rent;
                     txt_bipari_commission.Text = search.BipComm;
                     txt_customer_commission.Text = search.CusComm;
                     txt_bipari_chongi.Text = search.Laga;
                     txt_customer_chongi.Text = search.Chongi;
                     calculateService();
                     search.Close();

             }
         }
         private void txt_product_name_TextChanged(object sender, EventArgs e)
         {



         }
         */
        private void txt_product_wight_TextChanged(object sender, EventArgs e)
        {
            shop = SHOP.Weight;

            /*using (Search search = new Search(4, txt_product_name.Text))
            {
                DialogResult res = search.ShowDialog();
                // if ( res== DialogResult.OK)
                {
                    lbl_weight_id.Text = search.Id;
                    txt_product_weight.Text = search.Name;
                    search.Close();

                }
            }*/
            //DisplayData("Weight", "");

        }




        private void grid_landlords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                if (tempclient != null)
                {
                    if (Admin.GetInstance.clients.Count() > 0)
                    {
                        grid_landlords.Rows.RemoveAt(index);
                        Admin.GetInstance.clients.RemoveAt(index);
                        lbl_msg_result.Text = Resources.record_delete;
                    }
                }

            }
        }


        #region Insert and Delete
        private void insertSales(Landlord temp)
        {
            //bool check = bal.addClient_Landlord(objectIndex);//Temp Comment
            bool check = true;
            if (check)
            {
                if (Authentication.Account.local == "0" || status=="Live")
                {
                    #region dbinsert
                    if (!oneTimeCheck)
                    {
                        new BLogic().addTodaySales(date);
                        oneTimeCheck = true;
                    }

                    bool chk = new BLogic().addClient_Landlord(objectIndex);
                    if (chk)
                    {
                        //Temp Comment

                        // its update only expenses only in tblsale
                        new BLogic().update_today_sales(date);//Temp Comment
                        //bal.addExpense_IUExpense(date,tempclient.expense.category);//Temp Comment
                        //foreach (Landlord tland in tem_ll)
                        {
                            //int i = 5;
                            //bal.addBalanceSheet("credit", 0, tland, nameof(BillKey.EnumUser.Expense), "insert",temp.land_person.pkey,"");//Temp Comment
                        }
                        tem_ll = null;
                    }
                    #endregion

                }
                else
                {
                    
                        #region LocalFile Insertion
                        foreach (Landlord tland in tem_ll)
                        {
                            int i = 5;
                            saleParser.writeJson(tland, "");
                            //string key = i++ + date.Replace("-", "");
                        }
                        #endregion

                }
                updateUIData();
                lbl_msg_result.Text = Resources.msg_database_success;
            }
            else
            {
                lbl_msg_result.Text = Resources.insertion_error;
            }
        }

        private void updateUIData()
        {
            tem_ll = null;
            DriverList(date);
            grid_landlords.Rows.Clear();
            grid_landlords.Refresh();
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
                return bal.deleteRecordTransport(billkey, date, land,land.category,"17");
            }
        }
        #endregion
       
        private void btn_refreshed_Click(object sender, EventArgs e)
        {
            tempclient = null;
            grid_landlords.Rows.Clear();
            grid_landlords.Refresh();

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

     
        private void btn_search_Click(object sender, EventArgs e)
        {
            if (txt_client_nameid.ContainsFocus)
            {
                //txt_client_nameid_Enter(this, new EventArgs());
                searchDialog(1, txt_client_nameid.Text);
                string idCheck=checkClientIDExit(lbl_client_id.Text,"");
                if (idCheck == "")
                    clkey = bal.p_getInvoiceID("Bip", lbl_client_id.Text,date);
                else
                    clkey = idCheck;


            }
            else
            if (txt_landloard_nameid.ContainsFocus)
            {
                //txt_landloard_nameid_FocusEnter(this, new EventArgs());
                searchDialog(1, txt_landloard_nameid.Text);
            }
            else
            if (txt_product_name.ContainsFocus)
            {
                //txt_landloard_nameid_FocusEnter(this, new EventArgs());
                searchDialog(3, txt_product_name.Text);
            }
            else
            if (txt_product_weight.ContainsFocus)
            {
                //txt_landloard_nameid_FocusEnter(this, new EventArgs());
                searchDialog(4, txt_product_weight.Text);
            }/*else
            if (txt_product_type.ContainsFocus)
            {
                //txt_landloard_nameid_FocusEnter(this, new EventArgs());
                searchDialog(5, txt_product_type.Text);
            }*/
        }

        private string checkClientIDExit(string clid,string landid)
        {
            if (clid != "")
            {
                foreach (Landlord l in Admin.GetInstance.clients)
                {
                    if (l.client._person_cl.pid == clid)
                    {
                        return l.client._person_cl.pkey;
                    }

                }
            }
            if (landid != "")
            {
                foreach (Landlord l in Admin.GetInstance.clients)
                {
                    if (l.land_person.pid == landid)
                    {
                        return l.land_person.pkey;
                    }

                }
            }
            return "";
        }
        #region Driver
        public void ZamidarGridLocalize()
        {
            grid_driverlist.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0201");
            grid_driverlist.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0401");
            grid_driverlist.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0304");
            grid_driverlist.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0309");
            grid_driverlist.Columns[4].HeaderText = Resources.ResourceManager.GetString("a0311");
            grid_driverlist.Columns[5].HeaderText = Resources.ResourceManager.GetString("a0307");
            grid_driverlist.Columns[6].HeaderText = Resources.ResourceManager.GetString("a0305");
            grid_driverlist.Columns[7].HeaderText = Resources.ResourceManager.GetString("a1061");
            grid_driverlist.Columns[8].HeaderText = Resources.ResourceManager.GetString("a0013");

        }
        private void ZamidarList(string row1, string row2, string row3, string row4, string row5
            , string row6, string row7, string row8, string row9)
        {
            int count = this.grid_driverlist.Rows.Count;

            this.grid_driverlist.Rows.Add();
            this.grid_driverlist.Rows[count - 1].Cells[0].Value = row1.ToString(); //Zamidar name;
            this.grid_driverlist.Rows[count - 1].Cells[1].Value = row2.ToString(); //Quantity;
            this.grid_driverlist.Rows[count - 1].Cells[2].Value = row3.ToString(); //Rent;
            this.grid_driverlist.Rows[count - 1].Cells[3].Value = row4.ToString(); //Labour;
            this.grid_driverlist.Rows[count - 1].Cells[4].Value = row5.ToString(); //Expense;
            this.grid_driverlist.Rows[count - 1].Cells[5].Value = row6.ToString(); //Client id;
            if (grid_driverlist.Columns.Count < 6)
                return;
            this.grid_driverlist.Rows[count - 1].Cells[6].Value = row7.ToString(); //Client id;
            this.grid_driverlist.Rows[count - 1].Cells[7].Value = row8.ToString(); //Client id;
            this.grid_driverlist.Rows[count - 1].Cells[8].Value = row9.ToString(); //Client id;
        }
        private void DriverList(string tdate)
        {
            grid_driverlist.Rows.Clear();
            grid_driverlist.Refresh();
            if (status == "Live")
            {

                DataTable rdt = bal.p_maalList(tdate);
                foreach (DataRow row in rdt.Rows)
                {
                    ZamidarList(row[0].ToString(),
                        row[1].ToString(),
                        row[2].ToString(),
                        row[3].ToString(),
                        row[4].ToString(),
                        row[5].ToString(),
                        row[6].ToString(),
                        row[7].ToString(),
                        row[8].ToString());

                }
            }else
            {
                List<Landlord> tclients = saleParser.LoadTodaySale();
                if (tclients == null)
                    return;
                int tr = 0, tl=0,te=0,tq=0,tm=0,ta=0,tcc=0 ;

                foreach (Landlord land in tclients)
                {
                    ZamidarList(land.land_person.pname,
                        land.land_product.total_Quantity.ToString(),
                        land.expense.total_rent.ToString(),
                        land.expense.total_labour.ToString(),
                        land.expense.total_expense.ToString(),
                        land.expense.total_munshiana.ToString(), land.expense.total_advance_amount.ToString(),
                        ((int)(land.Total_Chongi+land.Total_Commission)).ToString(),
                        land.land_person.pid);
                    tq += land.land_product.total_Quantity;
                    tr += land.expense.total_rent;
                    tl += land.expense.total_labour;
                    te += land.expense.total_expense;
                    tm += land.expense.total_munshiana;
                    ta += land.expense.total_advance_amount;
                    tcc += (int)(land.Total_Chongi + land.Total_Commission);



                }
                ZamidarList("Total",tq.ToString(),tr.ToString(), tl.ToString(), te.ToString(),
                    tm.ToString(),ta.ToString(),tcc.ToString(),"");
            }
           
            //grid_driverlist.DataSource = rdt; 
        }
        public void addZamidarColumn(string columnname,string headertext)
        {
            this.grid_driverlist.Columns.Add(columnname,headertext);
        }




        #endregion

        private void btn_bipari_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_assignid_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want Generate New Invoice IDs OF Bipari And Zamidar ? Yes=Generate Zamidar And Bipari ID, NO=Generate Bipari ID", "Generate Invoice IDs", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //string ZamcountBill = bal.p_getInvoiceID("ZamCount", lbl_ll_client_id.Text, date);
                //string BipcountBill = bal.p_getInvoiceID("BipCount", lbl_client_id.Text, date);
                //clkey = clkey + "-" + ((BipcountBill == "0" || BipcountBill == "") ? "" : BipcountBill);
                //llkey = llkey + "-" + ((ZamcountBill == "0" || ZamcountBill == "") ? "" : ZamcountBill);
                string llkey = bal.p_getInvoiceID("Zam", lbl_ll_client_id.Text, date);
                lbl_landlorid.Text = llkey;

                string bipkey = bal.p_getInvoiceID("Bip", lbl_ll_client_id.Text, date);
                lbl_bipariid.Text = bipkey;
            }
            else if (dialogResult == DialogResult.No)
            {
                string llkey = bal.p_getInvoiceID("Zam", lbl_ll_client_id.Text, date);
                lbl_landlorid.Text = llkey;

                return;
            }
        }
    }
}
