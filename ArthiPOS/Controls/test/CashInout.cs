using ArthiPOS.Controls.dashboard;
using ArthiPOS.Properties;
using ArthiPOS.Reporting;
using ArthiPOS.utill;
using ArthiPOS.Utill;
using BAL;
using CommonUtilities;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using DataMember;
using DataMember.memberlog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ArthiPOS.Controls.test
{
    public partial class CashInout : Form
    {

        BLogic bal;
        List<ReceiveCash> lrc;
        DataTable dtex = null, dtrec = null;
        private bool closeingform = false;

        public enum Cash
        {
            TodayCash, Searchhistory
        };
        public Cash enum_cash = Cash.TodayCash;
        private string local = "0";
        string date;
        SaleParser saleParser;
        public CashInout()
        {
            InitializeComponent();
            updateUI();
            lrc = new List<ReceiveCash>();

        }
        public CashInout(string date, bool check)
        {
            InitializeComponent();
            updateUI();
            lrc = new List<ReceiveCash>();
            this.date = date;
            saleParser = new SaleParser(date, Admin.SaveLog, Authentication.Account.local == "0" ? false : true);
            if (Authentication.Account.local == "1")
            {
                saleParser.SAVELOG = true;

            }
            this.closeingform = check;

            local = Authentication.Account.local;
            local = "0";


        }

        public void updateUI()
        {
            grid_cash.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0101");//Delete
            grid_cash.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0009");//Date
            grid_cash.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0021");//Description
            grid_cash.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0032");//Amount
            grid_cash.Columns[4].HeaderText = Resources.ResourceManager.GetString("a0006");//Key
            grid_cash.Columns[5].HeaderText = Resources.ResourceManager.GetString("a0013");//Key

            grid_expense.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0101");//Delete
            grid_expense.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0009");//Date
            grid_expense.Columns[2].HeaderText = Resources.ResourceManager.GetString("a0021");//Descripton
            grid_expense.Columns[3].HeaderText = Resources.ResourceManager.GetString("a0030");//Cash Recived
            grid_expense.Columns[4].HeaderText = Resources.ResourceManager.GetString("a0013");// Cash ID


        }
        private void init()
        {
            if (!closeingform)
            {
                date = ledger_date.Text;
                saleParser = new SaleParser(date, Admin.SaveLog, Authentication.Account.local == "0" ? false : true);
                if (Authentication.Account.local == "1")
                {
                    saleParser.SAVELOG = true;

                }
            }
            else
            {
                ledger_date.Text = date;
            }
            closeingform = false;
            //readDailySale(date);
            //UpdateGridCash(date);
            string startdate = ledger_date.Text;
            string lastdate = ledger_date.Text;

            loadSearchExpense(startdate, lastdate);
            loadSearchReceiving(startdate, lastdate);
            refreshClientList();
            btn_AddExpense.Focus();
        }
        private void CashInout_Load(object sender, EventArgs e)
        {
            bal = new BLogic();
            init();
            backupdb();
        }
        public void refreshClientList()
        {
            List<Landlord> tclients = bal.getLandlordsList(date, "");
            DataTable dt1 = bal.getTodayMaalAmad(date);
            Admin.GetInstance.clients.Clear();
            Admin.GetInstance.clients = tclients;
        }

        private void previousdate_Click(object sender, EventArgs e)
        {
            ledger_date.Value = CommonUtill.ChangeDate(ledger_date, -1);
            date = ledger_date.Text;
        }

        private void nextdate_Click(object sender, EventArgs e)
        {
            ledger_date.Value = CommonUtill.ChangeDate(ledger_date, 1);
            date = ledger_date.Text;
        }



        private void sale_date_ValueChanged(object sender, EventArgs e)
        {
            date = ledger_date.Text;
            date_start = ledger_date;
            grid_expense.Rows.Clear();
            grid_expense.Refresh();
            saleParser = new SaleParser(date, Admin.SaveLog, Authentication.Account.local == "0" ? false : true);
            if (Authentication.Account.local == "1")
            {
                saleParser.SAVELOG = true;

            }
            //readDailySale(date);
            //UpdateGridCash(ledger_date.Text);
            grid_expense.Rows.Clear();
            grid_expense.Refresh();
            grid_cash.Rows.Clear();
            grid_cash.Refresh();
            string startdate = ledger_date.Text;
            string lastdate = ledger_date.Text;

            loadSearchExpense(startdate, lastdate);
            loadSearchReceiving(startdate, lastdate);
            refreshClientList();

        }

        private void readDailySale(string date)
        {
            int count = 0, total_expense = 0;
            if (Authentication.Account.local == "0")
            {
                lbl_status.Text = "Live";
                lbl_status.BackColor = Color.YellowGreen;
            }
            else
            {
                lbl_status.Text = "Not Live";
                lbl_status.BackColor = Color.DarkOrange;
            }







            //if (Authentication.Account.local== "0")
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

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        count = this.grid_expense.Rows.Count;

                        string _date = row[0].ToString();
                        string _name = row[1].ToString();
                        string _amount = row[2].ToString();
                        string _key = row[3].ToString();
                        string _type = row[6].ToString();

                        total_expense += int.Parse(_amount);
                        addGridRow(_date, _name, _amount, _key, _type);


                    }
                    if (total_expense > 0)
                    {
                        count = this.grid_expense.Rows.Count;
                        this.grid_expense.Rows.Add();
                        this.grid_expense.Rows[count - 1].Cells[2].Value = "Total";
                        this.grid_expense.Rows[count - 1].Cells[3].Value = total_expense;
                        exp = new List<object>();
                        exp.Add("");
                        exp.Add(dt);

                    }
                    lbl_status.Text = "DB Live";
                    lbl_status.BackColor = Color.YellowGreen;
                    return;
                }


            }
            //else
            {

                List<Landlord> landload = saleParser.LoadTodaySale();
                int munshaia = 0, advance = 0, expense = 0, rent = 0, labour = 0, tea = 0;
                if (landload == null)
                    return;
                foreach (Landlord land in landload)
                {
                    string _date = land.date;
                    string _name = land.land_person.pname;
                    string _amount = land.GetGrandTotal.ToString();
                    string _key = land.land_person.pkey;
                    string category = land.expense.category;
                    rent += land.expense.total_rent;
                    labour += land.expense.total_labour;
                    munshaia += land.expense.total_munshiana;
                    advance += land.expense.total_advance_amount;
                    expense += land.expense.total_expense;

                    total_expense += int.Parse(_amount);
                    addGridRow(_date, _name, _amount, _key, category);
                }
                Expense exp = bal.getExpenseLable();
                addGridRow(date, string.Format("{0} + {1} + {2} + {3} + {4} + {5}", Resources.ResourceManager.GetString("a1022"), rent, labour, munshaia, advance, expense), (rent + labour + munshaia + advance + expense).ToString(), BillKey.getDateKey(date), "");
                total_expense += (rent + labour + munshaia + advance + expense);

                if (landload.Count > 0)
                {
                    lbl_status.Text = "Local Records";
                    lbl_status.BackColor = Color.DarkOrange;
                }

            }

            if (total_expense > 0)
            {
                count = this.grid_expense.Rows.Count;
                this.grid_expense.Rows.Add();
                this.grid_expense.Rows[count - 1].Cells[2].Value = "Total";
                this.grid_expense.Rows[count - 1].Cells[3].Value = total_expense;

            }
        }
        private void readCashRecived(string date)
        {
            DataTable dt = bal.getRecivedCash("ReadCashCust", date, "", "");
            lrc = null;
            lrc = new List<ReceiveCash>();
            int totalamount = 0, count = 0;
            foreach (DataRow rw in dt.Rows)
            {
                string tdate = rw[0].ToString();
                string name = rw[1].ToString();
                int amountpaid = int.Parse(rw[2].ToString());//amount paid
                int discount = int.Parse(rw[3].ToString()) > 0 ? int.Parse(rw[3].ToString()) : 0;//Rec id
                string billid = rw[4].ToString(); //bill id
                string recid = rw[5].ToString();//discount
                string custclientid = rw[6].ToString();
                string type = rw[7].ToString();//Type
                int remainging_cash = int.Parse(rw[3].ToString()); // Remainging amount 
                ReceiveCash rc = new ReceiveCash(tdate, name, amountpaid, billid, custclientid, discount, remainging_cash, recid, type, "");

                addGridRow(tdate, name, amountpaid, billid, "" + discount, type);
                totalamount += rc.amount;
                lrc.Add(rc);
            }
            if (totalamount > 0)
            {
                count = this.grid_cash.Rows.Count;
                this.grid_cash.Rows.Add();
                this.grid_cash.Rows[count - 1].Cells[2].Value = "Total";
                this.grid_cash.Rows[count - 1].Cells[3].Value = totalamount;

            }
        }

        private void addGridRow(string _date, string _name, int _amount, string _key, string discount, string type)
        {
            int count = this.grid_cash.Rows.Count;

            this.grid_cash.Rows.Add();
            this.grid_cash.Rows[count - 1].Cells[1].Value = _date;
            this.grid_cash.Rows[count - 1].Cells[2].Value = _name;
            this.grid_cash.Rows[count - 1].Cells[3].Value = _amount;
            this.grid_cash.Rows[count - 1].Cells[4].Value = discount;
            this.grid_cash.Rows[count - 1].Cells[5].Value = _key;
            this.grid_cash.Rows[count - 1].Cells[6].Value = type;

        }
        private void addGridRow(string _date, string _name, string _amount, string _key, string type)
        {
            int count = this.grid_expense.Rows.Count;
            this.grid_expense.Rows.Add();
            this.grid_expense.Rows[count - 1].Cells[1].Value = _date;
            this.grid_expense.Rows[count - 1].Cells[2].Value = _name;
            this.grid_expense.Rows[count - 1].Cells[3].Value = _amount;
            this.grid_expense.Rows[count - 1].Cells[4].Value = _key;
            this.grid_expense.Rows[count - 1].Cells[5].Value = type;
        }








        //AutoCompleteStringCollection auto;
        //bool check_exist = false;
        /*private void autoCompleteData()
        {
            
            auto = bal.autoCompleteData();
            //Set AutoCompleteSource property of txt_StateName as CustomSource
            txt_expense_name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //Set AutoCompleteMode property of txt_StateName as SuggestAppend. SuggestAppend Applies both Suggest and Append
            txt_expense_name.AutoCompleteMode = AutoCompleteMode.Suggest;
            txt_expense_name.AutoCompleteCustomSource = auto;
        }*/

        private void btn_add_customer_Click(object sender, EventArgs e)
        {

        }

        private void Grid_expense_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                if ((grid_expense.Rows[index].Cells[4].Value.ToString() == null ||
                    grid_expense.Rows[index].Cells[4].Value.ToString() == ""))
                {
                    return;
                }
                string billkey = grid_expense.Rows[index].Cells[4].Value.ToString();
                string name = grid_expense.Rows[index].Cells[2].Value.ToString();
                string date = grid_expense.Rows[index].Cells[1].Value.ToString();
                string amount = grid_expense.Rows[index].Cells[3].Value.ToString();
                string type = grid_expense.Rows[index].Cells[5].Value.ToString();
                DataRow dr = dtex.Rows[index];
                string id = dr[7].ToString();//id
                string discount = dr[8].ToString();
                string oldAmount = dr[8].ToString();
                string expid = dr[2].ToString();
                string transid = dr[11].ToString();
                string transname = "";
                string actypeid = dr[12].ToString();
                string actype = "";
                string expenseid = dr[10].ToString();
                string cateid = dr[13].ToString();

                int chkType = 0;
                if (type == "Client")
                { chkType = 1; }
                else if (type == "ClientInvest")
                { chkType = 5; }
                else if (type == "Admin")
                { chkType = 7; }
                else if (type == "Expense")
                { chkType = 12; }
                else if (type == "ShopExpense")
                { chkType = 14; }
                DeleteDailog dd = new DeleteDailog(id, type, name, id, billkey, expid, oldAmount, amount, discount, date, chkType, transid, expenseid, actypeid, actype, cateid);

                dd.ShowDialog();
                if (dd.check)
                {
                    //grid_cash.Rows.RemoveAt(index);
                    new BLogic().p_insert_date(date);
                   // ToastNotification.Show(this, "Cash Deleted..");
                    init();
                }


                //bool check = new BLogic().p_addCash("Delete", date, int.Parse(id), name, int.Parse(amount), int.Parse(discount), chkType, billkey);
                //if(check)
                //{
                //    //new BLogic().addBalanceSheetExpense(name, ""+(int.Parse(amount) + int.Parse(discount)), date, type, billkey, "debit", "deleted", "1");

                //    MessageBox.Show(name+" "+type+" "+(int.Parse(amount)+ int.Parse(discount)) +" : "+"Deleted");
                //    init();
                //}

                /*Landlord land = Admin.GetInstance.clients.Find(x => x.land_person.pkey == billkey);
                bool checkStatus = bal.checkStatusofSale(billkey);
                if (!checkStatus && type=="Client")
                    return;


                if (land != null)
                {
                    int count = land.customers.Count;
                    //int chk = bal.deleteRecordTransport(billkey, date, land, count, land.expense.category);

                    bool chk = bal.updateStatusSale(date,int.Parse(amount),billkey, type, 0,land.land_person.pid);
                    if (chk)
                    {
                        //bal.addBalanceSheet("debit", 1, land, nameof(BillKey.EnumUser.Expense), "deleted", land.land_person.pkey);

                        grid_expense.Rows.RemoveAt(index);
                    }
                    //bal.addExpense_IUExpense(date, land.expense.category);
                    bal.update_today_sales(date);
                    grid_expense.Rows.Clear();
                    grid_expense.Refresh();
                    readDailySale(date);
                }
                else
                {
                    //int chk = bal.deletExpenseShop(billkey, date, type) ? 1 : 0;
                    //if (chk > 0)
                    {
                        if (type == "Client")
                        {
                            new BLogic().addExtraAmountClient("UpdateDel", date, "" + 0, int.Parse(amount),
                              billkey, name, 0);
                        }
                        else
                        {
                            int chk = bal.deletExpenseShop(billkey, date, type) ? 1 : 0;
                        }
                        new BLogic().addBalanceSheetExpense(name, amount, date, type, billkey, "debit", "deleted", "1");
                        grid_expense.Rows.RemoveAt(index);
                        grid_expense.Rows.Clear();
                        grid_expense.Refresh();
                        readDailySale(date);
                    }
                }*/

            }
        }

        private void UpdateGridCash(string date)
        {
            //update grid
            grid_cash.Rows.Clear();
            grid_cash.Refresh();
            readCashRecived(date);
        }
        private void grid_cash_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool chk = false;
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                if (grid_cash.Rows.Count > 0)
                {
                    ReceiveCash temp = lrc[index];


                    //bool chk =bal.p_customer_CRUD("Delete", temp.date, temp.name, temp.key, temp.amount+temp.discount);


                    // chk = bal.p_customer_CRUD("Delete", temp.date, temp.name, temp.key, temp.amount + temp.discount,(temp.recID == "" ?  0 : int.Parse(temp.recID)), temp.type,"");

                    //DeleteDailog dd = new DeleteDailog(temp.id,temp.type, temp.name, temp.id, temp.key, temp.recID, "", "" + temp.amount, "" + temp.discount, temp.date);

                    int chkType = 0;
                    if (temp.type == "Customer")
                    { chkType = 2; }
                    else if (temp.type == "ClientInvest")
                    { chkType = 4; }
                    else if (temp.type == "Admin")
                    { chkType = 8; }
                    else if (temp.type == "ClientRemReceive")
                    { chkType = 15; }

                    DeleteDailog dd = new DeleteDailog(dtrec.Rows[index], chkType);

                    dd.ShowDialog();
                    if (dd.check)
                    {
                        new BLogic().p_insert_date(temp.date);
                        //ToastNotification.Show(this, "Cash Deleted..");
                        init();
                    }
                }
            }
        }





        List<Object> exp = null, rec = null;
        private void btn_print_closeing_Click(object sender, EventArgs e)
        {
            // pageSize = 18;
            List<Object> obj = (List<object>)new BLReport().p_expenseCashReceive(date, date, 1, 18);

            //List<Object> exp = (List<object>)new BLReport().p_reporting_CRUD("ExpenseCash", date_start.Text, date_last.Text, 1, 18,"");
            //List<Object> rec = (List<object>)new BLReport().p_reporting_CRUD("ReceivingCash", date_start.Text, date_last.Text, 1, 18, "");
            if (exp == null)
            {
                return;
            }
            DataTable dtexp = (DataTable)exp[1];
            if (rec == null)
            {
                return;
            }
            DataTable dtrec = (DataTable)rec[1];
            //AllReportsCC rp = new AllReportsCC(dt, ReportMenu.ExpenseCashReceive, date, date);
            AllReportsCC rp = new AllReportsCC();
            //rp.ExpenseReceiving(dt, date, date);
            DataRow dr = new BLogic().getLastCash(date_start.Text, date_last.Text);

            int balance = GetInt(dr, 0);
            int receivings = GetInt(dr, 1);
            int expense = GetInt(dr, 2);
            int cbalance = (int)GetDecimal(dr, 3);



            rp.ExpenseRecSection(dtrec, dtexp, balance, receivings, expense, cbalance);
            rp.ShowDialog();

        }
        private int GetInt(DataRow dr, int index)
        {
            if (dr == null || index >= dr.Table.Columns.Count)
                return 0;

            decimal value;
            return decimal.TryParse(dr[index]?.ToString(), out value)
                ? (int)value
                : 0;
        }

        private decimal GetDecimal(DataRow dr, int index)
        {
            if (dr == null || index >= dr.Table.Columns.Count)
                return 0;

            decimal value;
            return decimal.TryParse(dr[index]?.ToString(), out value)
                ? value
                : 0;
        }
        private void btn_savedata_Click(object sender, EventArgs e)
        {
            //AddExpense exp = new AddExpense(ledger_date.Text, grid_expense.Rows.Count);
            AddExpenseCash exp = new AddExpenseCash("Expense/Receivings");
            exp.ShowDialog();
            grid_expense.Rows.Clear();
            grid_expense.Refresh();
            //readDailySale(date);
            init();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.Control | Keys.P:
                    btn_print_closeing_Click(this, new EventArgs());
                    return true;
                case Keys.Control | Keys.D:
                    btn_dailysales_Click(this, new EventArgs());
                    return true;
                case Keys.Control | Keys.R:
                    btn_report_Click(this, new EventArgs());
                    return true;
                case Keys.Control | Keys.E:
                    btn_savedata_Click(this, new EventArgs());
                    return true;
                case Keys.Control | Keys.C:
                    btn_closing_today_Click(this, new EventArgs());

                    return true;
                case Keys.Control | Keys.B:
                    //btn_billings_Click(this, new EventArgs());
                    return true;

            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btn_todayRep_Click(object sender, EventArgs e)
        {
            AllReportsCC rp = new AllReportsCC();

            rp.printTodayReport(date, date);
            rp.ShowDialog();
        }
        private void dailyaccounts()
        {
            string sdate = date_start.Text;
            string ldate = date_last.Text;
            DataTable dt = new BLogic().p_bs_read("DAcc", sdate, ldate);
            AllReportsCC rp = new AllReportsCC();
            rp.dailyAccounts(dt);
            rp.ShowDialog();
        }

        private void btn_todayreportdet_Click(object sender, EventArgs e)
        {
            bal.update_today_sales(ledger_date.Text);
            List<object> obj = (List<object>)new BLReport().p_DetailReport(date_start.Text, date_last.Text, "");
            if (obj == null)
            {
                return;
            }
            DataTable dt = (DataTable)obj[1];
            if (dt.Rows.Count == 0)
                return;
            DataRow cr = dt.Rows[0];
            int acc_open = int.Parse(cr[3].ToString());
            string datec = datec = ledger_date.Text + " To " + ledger_date.Text;

            AllReportsCC rp = new AllReportsCC();
            DataTable dtprd = new BLogic().readFardHisab("AllProduct", "", date_start.Text, date_last.Text);

            rp.DetailReport(dt, dtprd, ledger_date.Text, ledger_date.Text, acc_open, datec);
            rp.ShowDialog();
        }



        private void btnAddcash_Click(object sender, EventArgs e)
        {
            //AddAdminCash add = new AddAdminCash();
            AddExpenseCash add = new AddExpenseCash("Receivings");
            add.ShowDialog();
            grid_expense.Rows.Clear();
            grid_expense.Refresh();
            grid_cash.Rows.Clear();
            grid_cash.Refresh();
            init();
        }

        private void btn_billings_Click(object sender, EventArgs e)
        {
            BillPaidOut bp = new BillPaidOut();
            bp.ShowDialog();
            grid_expense.Rows.Clear();
            grid_expense.Refresh();
            grid_cash.Rows.Clear();
            grid_cash.Refresh();
            init();
        }



        private void btn_search_Click_1(object sender, EventArgs e)
        {
            string startdate = date_start.Text;
            string lastdate = date_last.Text;

            loadSearchExpense(startdate, lastdate);
            loadSearchReceiving(startdate, lastdate);


        }
        public void loadSearchReceiving(string startdate, string lastdate)
        {
            if (grid_cash.Rows.Count > 0)
            {
                grid_cash.Rows.Clear();
                grid_cash.Refresh();
            }
            rec = (List<object>)new BLReport().p_reporting_CRUD("ReceivingCash", startdate, lastdate, 1, 18, "");
            if (rec == null)
            {
                return;
            }
            this.dtrec = (DataTable)rec[1];

            refreshReceving(this.dtrec);
        }
        public void loadSearchExpense(string startdate, string lastdate)
        {
            if (grid_expense.Rows.Count > 0)
            {
                grid_expense.Rows.Clear();
                grid_expense.Refresh();
            }
            exp = (List<object>)new BLReport().p_reporting_CRUD("ExpenseCash", startdate, lastdate, 1, 18, "");
            if (exp == null)
            {
                return;
            }
            this.dtex = (DataTable)exp[1];
            refreshExpense(this.dtex);
        }

        private void backupdb()
        {
            try
            {

                DialogResult dialogResult = MessageBox.Show("Do you Want To Take Backup?(Y/N)", "Backup Database", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    AdminLog adminLog = LogUtill.getAdminInputLog();
                    DatabaseLog db = LogUtill.getDatabaseLog();

                    if (bal.backupDb(adminLog.BackupPath, db.LocalCheck))
                    {
                        MessageBox.Show(Resources.ResourceManager.GetString("a1084"));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void CashInout_FormClosing(object sender, FormClosingEventArgs e)
        {
            backupdb();
        }
        private Thread aclosing = null;

        private void btn_closing_today_Click(object sender, EventArgs e)
        {
            if (aclosing != null && aclosing.IsAlive)
                return;
            else
            {
                aclosing = new Thread(new ThreadStart(accountclosingcall));
                aclosing.Name = "cashinout";
                aclosing.Start();

            }
        }
        private void accountclosingcall()
        {


            try
            {
                //AccountClosing ac = new AccountClosing(date);
                AccountClosed ac = new AccountClosed(date);

                ac.ShowDialog();
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(init));
                }
                else
                {
                    // Do Something
                    init();
                }
            }
            catch (NullReferenceException e) {
                Admin.LogExecMang.LogException(e, e.Message);
            }
            catch (ThreadStateException e)
            {
                Admin.LogExecMang.LogException(e, e.Message);

            }
            catch (InvalidOperationException e)
            {
                Admin.LogExecMang.LogException(e, e.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dailyaccounts();
        }

        private void btn_dailysales_Click(object sender, EventArgs e)
        {
            DailySales ds = new DailySales(date);
            ds.ShowDialog();
        }

        private void btn_report_Click(object sender, EventArgs e)
        {
            ReportAllData rp = new ReportAllData();
            rp.ShowDialog();

        }

        

        public void refreshReceving(DataTable dt)
        {
            lrc = new List<ReceiveCash>();
            int totalamount = 0, count = 0;
            foreach (DataRow rw in dt.Rows)
            {
                string tdate = rw[2].ToString();
                string name = rw[4].ToString();
                int amountpaid = int.Parse(rw[5].ToString());//amount paid
                int discount = int.Parse(rw[6].ToString()) > 0 ? int.Parse(rw[6].ToString()) : 0;//Rec id
                string billid = rw[7].ToString(); //bill id
                string recid = rw[1].ToString();//discount
                string custclientid = rw[3].ToString();
                string type = rw[8].ToString();//Type
                int remainging_cash = int.Parse(rw[5].ToString() == "" ? "0" : rw[5].ToString()); // Remainging amount 
                ReceiveCash rc = new ReceiveCash(tdate, name, amountpaid, billid, custclientid, discount, remainging_cash, recid, type, "");

                addGridRow(tdate, name, amountpaid, billid, "" + discount, type);
                totalamount += rc.amount;
                lrc.Add(rc);
            }
            if (totalamount > 0)
            {
                count = this.grid_cash.Rows.Count;
                this.grid_cash.Rows.Add();
                this.grid_cash.Rows[count - 1].Cells[2].Value = "Total";
                this.grid_cash.Rows[count - 1].Cells[3].Value = totalamount;

            }
        }
        public void refreshExpense(DataTable dt)
        {
            int count = 0, total_expense = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    count = this.grid_expense.Rows.Count;
                    string _date = row[1].ToString();
                    string _name = row[3].ToString();
                    string _amount = row[4].ToString();
                    string _key = row[5].ToString();
                    string _type = row[6].ToString();

                    total_expense += int.Parse(_amount);
                    addGridRow(_date, _name, _amount, _key, _type);


                }
                if (total_expense > 0)
                {
                    count = this.grid_expense.Rows.Count;
                    this.grid_expense.Rows.Add();
                    this.grid_expense.Rows[count - 1].Cells[2].Value = "Total";
                    this.grid_expense.Rows[count - 1].Cells[3].Value = total_expense;

                }
                lbl_status.Text = "DB Live";
                lbl_status.BackColor = Color.YellowGreen;
                return;
            }

        }
    }
}
