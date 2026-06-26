using ArthiPOS.controls;
using ArthiPOS.Reporting;
using ArthiPOS.shop;
using ArthiPOS.Utill;
using BAL;
using CommonUtilities;
using DataMember;
using DataMember.memberlog;
using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArthiPOS.Controls.test
{
    public partial class AccountClosed : Form
    {


        private string date;
        private int Count = 0;
        public AccountClosed(string date)
        {
            InitializeComponent();
            this.date = date;
            lbl_date.Text = date;
            string db = RegistryAccess.GetStringRegistryValue("db", "Test");
            if (db == "Test")
                testorlive = @"Test\";
            else if (db == "Live")
                testorlive = @"Live\";
            else if (db == "Local")
                testorlive = @"Local\";

        }

        DataTable exp, rec;

        private void btnclosing_Click(object sender, EventArgs e)
        {
            if (Count > 0)
            {
                double lStart = DateTime.Now.Ticks;
                Admin.LogExecMang.Log("Closing Start Time->" + lStart);
                Admin.LogExecMang.Log("\nExpense and Receivings Processing Start-> ");
                expenseReceivingsClosing();
               
                Admin.LogExecMang.Log("\nEnd-> " + (DateTime.Now.Ticks- lStart) / 10000000.0);
                Admin.LogExecMang.Log("\nSales Processing Start-> ");

                vednourSalesClosing();
                Admin.LogExecMang.Log("\nEnd-> " + (DateTime.Now.Ticks- lStart) / 10000000.0);

                double lFinish = DateTime.Now.Ticks;
                Admin.LogExecMang.Log("\nClosing Time " + (lFinish - lStart) / 10000000.0);

                btn_refresh_Click(this, new EventArgs());

                /*Task task1 = Task.Run(() => expenseReceivingsClosing());

                // When taskmethod1 is completed, start taskmethod2
                task1.ContinueWith((task) =>
                {
                    vednourSalesClosing();
                });

                // Wait for both tasks to complete before calling RefreshData
                Task.WaitAll(task1);
                btn_refresh_Click(this, new EventArgs());
                */



            }

        }

        private void expenseReceivingsClosing()
        {

            // do something
            long start1 = DateTime.Now.Ticks;
            Admin.LogExecMang.Log("\n\t(expenseReceivingsClosing)Get ER Data-> ");

            DataTable dt = new BLogic().getCashInout("ER", date);
            Admin.LogExecMang.Log("\n\tEnd-> " + (DateTime.Now.Ticks- start1 )/ 10000000.0);

            if (dt == null) return;
            if (dt.Rows.Count == 0) return;
            if (dt.Rows.Count > 0)
                new BLogic().addTodaySales(date);
            Admin.LogExecMang.Log("\n\tAdd ER Data-> ");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                addData(dr, i);
            }
            Admin.LogExecMang.Log("\n\tEnd-> " + (DateTime.Now.Ticks- start1) / 10000000.0);

            if (dt.Rows.Count > 0)
            {
                new BLogic().update_today_sales(date);

            }
            


        }
        string testorlive = @"Test\";
        private void vednourSalesClosing()
        {
            AdminLog adminlog = LogUtill.getAdminInputLog();
            SaleParser saleParser = new SaleParser("", Admin.SaveLog, Authentication.Account.local == "0" ? false : true);
            string db = RegistryAccess.GetStringRegistryValue("db", "Test");
            if (db == "Test")
                testorlive = @"Test\";
            else if (db == "Live")
                testorlive = @"Live\";
            else if (db == "Local")
                testorlive = @"Local\";
            FileInfo[] _files = saleParser.getAllFiles(adminlog.SalesInProccessedFolder+ testorlive, false);

            for (int i = 0; i < _files.Length; i++)
            {
                string file = _files[i].FullName;
                Wrapper wraplandl = saleParser.LoadTodaySale(file);
                string date = wraplandl.date;
                closingSales(date, saleParser, adminlog);
            }
        }
        private void closingSales(string mdate, SaleParser saleParser, AdminLog adminlog)
        {
            saleParser = new SaleParser(mdate, Admin.SaveLog, Authentication.Account.local == "0" ? false : true);
            string filePath = "";
            // filePath = files[index].FullName;
            filePath = string.Format("{0}{1}.json", adminlog.SalesInProccessedFolder+testorlive, mdate.Replace("-", ""));
            if (!File.Exists(filePath))
            {
                MessageBox.Show(string.Format("{0}\n{1}", ConstMessages._FileNotExist, filePath));
            }

            //return;

            Wrapper wrapland = saleParser.LoadTodaySale(filePath);
            if (wrapland == null)
            {
                return;
            }
            if (wrapland.db_status == "Updated")
            {
                return;
            }
            wrapland.db_status = "Updated";
            if (saleParser.updateLandLord(filePath, wrapland))//Updating status sale 
            {

                bool oneTimeCheck = false;
                #region AddProducts
                if (!oneTimeCheck)
                {
                    new BLogic().addTodaySales(wrapland.date);// add Todays sales date
                    oneTimeCheck = true;
                }

                wrapland.data = new BLogic().updateLocalToDB(wrapland.date, wrapland.data, true);
                if (wrapland.data == null)
                {
                    wrapland.db_status = "None";
                    saleParser.updateLandLord(filePath, wrapland);
                    return;
                }
                if (wrapland.data.Count > 0)
                {
                    if (wrapland.data[0].record_id != "")
                    {
                        saleParser.moveSaleinProcess(filePath);
                    }
                }

                #endregion
            }
        }

        private void addData(DataRow d, int i)
        {
            string idcashinout = d[0].ToString();
            string cate_name = d[3].ToString();
            string id = d[10].ToString();
            string desc = d[12].ToString();
            int cash = int.Parse(d[13].ToString());
            int discount = int.Parse(d[14].ToString());
            string key = d[1].ToString();
            string expenseid = d[7].ToString();
            string transactionid = d[5].ToString();
            string ccname = d[9].ToString();
            string acctransid = d[6].ToString();
            int cashtype = int.Parse(d[8].ToString());
            string entrytype = d[15].ToString();
            string datetime = d[17].ToString();
            string cateid = d[18].ToString();
            string date = d[2].ToString();

            // return;
            bool check = false;
            long start1 = DateTime.Now.Ticks;
            if (cate_name == "Customer" || cate_name == "ClientInvest"
                || cate_name == "Client" || cate_name == "ClientRemReceive"
                || cate_name == "Admin")
            {


                Admin.LogExecMang.Log("\n\t(addData) Add Cash-> ");

                check = new BLogic().p_addCash(cate_name, date, int.Parse(id), desc,
                    cash, discount, cashtype, key, expenseid, transactionid, ccname, acctransid, datetime, "I", cateid);
                Admin.LogExecMang.Log("\n\tEnd-> " + (DateTime.Now.Ticks-start1)/ 10000000.0);


                //if (check)
                {

                    new BLogic().p_fin_BalanceSheet_CRUD("I", date, transactionid, acctransid, cash, "+");
                    new BLogic().p_cashinout_Crud("D", key, "", "", 0, 0, 0, 0, "", int.Parse(idcashinout), "", "", 0, 0, "", cateid, "d");
                }
            }
            else if (cate_name == "Expense" || cate_name == "ShopExpense")
            {

                Admin.LogExecMang.Log("\n\tExpenses Process-> ");

                if (new BLogic().insertTodayExpense(date, ccname, "" + cash, key,
                    string.Format("p_{0}_{1}", cate_name, expenseid), cate_name, "" + 0, expenseid, desc, transactionid, cateid, expenseid))
                {
                    Admin.LogExecMang.Log("\n\tEnd-> " + (DateTime.Now.Ticks-start1)/ 10000000.0);

                    check = true;
                    new BLogic().p_ledger_CRUD("Insert", transactionid, acctransid, "C", cash, int.Parse(id), cate_name, date,
                        key, expenseid, "I", cateid);
                    new BLogic().addBalanceSheetExpense(desc, "" + cash, date, cate_name, key, "credit", "Insert", "0", acctransid, cateid);
                    new BLogic().update_today_sales(date);
                    new BLogic().p_fin_BalanceSheet_CRUD("I", date, transactionid, acctransid, cash, "-");
                    if (check)
                    {
                        new BLogic().p_cashinout_Crud("D", key, "", "", 0, 0, 0, 0, "", int.Parse(idcashinout), "", "", 0, 0, "", cateid, "d");
                    }
                }

            }
            else if(cate_name=="Sales")
            {

            }

        }



        private void AccountClosed_Load(object sender, EventArgs e)
        {

            
            if (rd_ex_rec.Checked)
            {
                readExpenseReceiving(date);
            }
            else if (rd_sales.Checked)
            {
                salesUpdate();
            }
            else if (rd_both.Checked)
            {
                salesExpRece();
            }



        }
        private void salesUpdate()
        {
            panel_info.Controls.Clear();
            ControlSalesUpdate sales = new ControlSalesUpdate();
            panel_info.Controls.Add(sales);
            Count = sales.Count;
        }
        private void readExpenseReceiving(string date)
        {
            panel_info.Controls.Clear();
            ControlEXREC sales = new ControlEXREC(date);
            panel_info.Controls.Add(sales);
            Count = sales.CountRec + sales.CountExp;

        }


        private void btn_refresh_Click(object sender, EventArgs e)
        {
            long start1 = DateTime.Now.Ticks;
            Admin.LogExecMang.Log("\n*******Refresh*************");
            AccountClosed_Load(this, new EventArgs());
            Admin.LogExecMang.Log("\n   Loading Ends-> " + (DateTime.Now.Ticks - start1) / 10000000.0);

            DataTable dt = new BLogic().getDates();
            Admin.LogExecMang.Log("\n   Get Dates-> " + (DateTime.Now.Ticks - start1) / 10000000.0);
            Admin.LogExecMang.LogStart("p_insertStart");
            int count = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Task.Run(() => new BLogic().p_insert_date(dr[0].ToString()));
                Admin.LogExecMang.LogEnd(""+count++);

                //break;

            }
            Admin.LogExecMang.LogEnd("p_insertStart");

            Admin.LogExecMang.Log("\n   Insertion Time Ends-> " + (DateTime.Now.Ticks - start1) / 10000000.0);

            double end = (DateTime.Now.Ticks-start1)/ 10000000.0;
            Admin.LogExecMang.Log("\n******* End Refresh*************-> "+ end);

        }

        private void print()
        {
            if (exp == null)
            {
                return;
            }
            AllReportsCC rp = new AllReportsCC();
            DataRow dr = new BLogic().getLastCash(date, date);

            int balance = int.Parse(dr[0].ToString() == "" || dr == null ? "0" : dr[0].ToString());
            int receivings = int.Parse(dr[1].ToString() == "" || dr == null ? "0" : dr[1].ToString());
            int expense = int.Parse(dr[2].ToString() == "" || dr == null ? "0" : dr[2].ToString());
            int cbalance = int.Parse(dr[3].ToString() == "" ? "0" : dr[3].ToString());

            rp.ExpenseRecSection(rec, exp, balance, receivings, expense, cbalance);
            rp.ShowDialog();
        }

        private void btn_print_closeing_Click(object sender, EventArgs e)
        {
            print();
        }

        private void rd_ex_rec_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_ex_rec.Checked)
            {
                readExpenseReceiving(date);
            }
        }

        private void rd_sales_CheckedChanged(object sender, EventArgs e)
        {
            salesUpdate();
        }

        private void rd_both_CheckedChanged(object sender, EventArgs e)
        {
            salesExpRece();
        }

        private void salesExpRece()
        {
            panel_info.Controls.Clear();
            ControlSalesExpRec sales = new ControlSalesExpRec(date);
            panel_info.Controls.Add(sales);
            Count = sales.Count;
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
                case Keys.Control | Keys.S:
                    btnclosing_Click(this, new EventArgs());
                    return true;
                case Keys.F5:
                    btn_refresh_Click(this, new EventArgs());
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

}
