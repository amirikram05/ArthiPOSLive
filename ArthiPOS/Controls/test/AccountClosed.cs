using ArthiPOS.Reporting;
using ArthiPOS.shop;
using ArthiPOS.Utill;
using BAL;
using CommonUtilities;
using DataMember;
using DataMember.memberlog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

        }

        DataTable exp, rec;

        private void btnclosing_Click(object sender, EventArgs e)
        {
            if (Count > 0)
            {
                expenseReceivingsClosing();
                vednourSalesClosing();
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
            DataTable dt = new BLogic().getCashInout("ER", date);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                addData(dr, i);
            }
        }

        private void vednourSalesClosing()
        {
            AdminLog adminlog = LogUtill.getAdminInputLog();
            SaleParser saleParser = new SaleParser("", Admin.SaveLog);
            FileInfo[] _files = saleParser.getAllFiles(adminlog.SalesInProccessedFolder, false);

            for (int i = 0; i < _files.Length; i++)
            {
                string file = _files[i].FullName;
                Wrapper wraplandl = saleParser.LoadTodaySale(file);
                string date = wraplandl.date;
                closingSales(date,saleParser,adminlog);
            }
        }
        private void closingSales(string mdate,SaleParser saleParser, AdminLog adminlog)
        {
            saleParser = new SaleParser(mdate, Admin.SaveLog);
            string filePath = "";
            // filePath = files[index].FullName;
            filePath = string.Format("{0}{1}.json", adminlog.SalesInProccessedFolder, mdate.Replace("-", ""));
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
                    return;
                }
                if (wrapland.data.Count > 0)
                {
                    if (wrapland.data[0].record_id != "")
                    {
                        new BLogic().p_insert_date(date);
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
            string date= d[2].ToString();

            // return;
            bool check = false;

            new BLogic().addTodaySales(date);
            if (cate_name == "Customer" || cate_name == "ClientInvest"
                || cate_name == "Client" || cate_name == "ClientRemReceive"
                || cate_name == "Admin")
            {

                //return;
                check = new BLogic().p_addCash(cate_name, date, int.Parse(id), desc,
                    cash, discount, cashtype, key, expenseid, transactionid, ccname, acctransid, datetime, "I", cateid);
                if (check)
                    new BLogic().p_cashinout_Crud("D", "", "", "", 0, 0, 0, 0, "", int.Parse(idcashinout), "", "", 0, 0, "", cateid,"d");

            }
            else if (cate_name == "Expense" || cate_name == "ShopExpense")
            {
                //return;
                if (new BLogic().insertTodayExpense(date, ccname, "" + cash, key,
                    string.Format("p_{0}_{1}", cate_name, 0), cate_name, "" + 0, expenseid, desc, transactionid, cateid,expenseid))
                {
                    check = true;
                    new BLogic().p_ledger_CRUD("Insert", transactionid, acctransid, "C", cash, int.Parse(id), cate_name, date,
                        key, expenseid, "I", cateid);
                    new BLogic().addBalanceSheetExpense(desc, "" + cash, date, cate_name, key, "credit", "Insert", "0", acctransid, cateid);
                    new BLogic().update_today_sales(date);
                    if (check)
                        new BLogic().p_cashinout_Crud("D", "", "", "", 0, 0, 0, 0,
                            "", int.Parse(idcashinout), "", "", 0, 0, "", cateid, "d");
                }

            }


            if (check)
            {
                new BLogic().p_insert_date(date);
                btn_refresh_Click(this, new EventArgs());

            }
            //else if (cate_name == "ClientInvest")
            //{
            //    check = new BLogic().p_addCash(cate_name, date, int.Parse(id), desc, cash,
            //        discount, cashtype, key, expenseid, transactionid, ccname, acctransid);
            //}
            //else if (cate_name == "Client" || cate_name == "ClientRemReceive")
            //{
            //    check = new BLogic().p_addCash(cate_name, date, int.Parse(id), desc, cash,
            //        discount, cashtype, key, expenseid, transactionid, ccname, acctransid);
            //}
            //else if (cate_name == "Admin")
            //{
            //    //return;
            //    check = new BLogic().p_addCash(cate_name, date, int.Parse(id), desc, cash,
            //        discount, cashtype, key, expenseid, transactionid, ccname, acctransid);
            //}



        }

        

        private void AccountClosed_Load(object sender, EventArgs e)
        {
            if (rd_ex_rec.Checked)
            {
                readExpenseReceiving(date);
            }
            else if(rd_sales.Checked)
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
            AccountClosed_Load(this, new EventArgs());
           DataTable dt = new BLogic().getDates();
           foreach (DataRow dr in dt.Rows)
           {
               new BLogic().p_insert_date(dr[0].ToString());
               //break;

           }
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
