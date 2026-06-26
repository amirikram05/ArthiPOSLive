using ArthiPOS.Reporting;
using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class BillPaidOut : Form
    {
        private BLogic bal;
        private DataTable dt;
        private string client_id = "", amounttobepaid = "", name = "";
        private string sdate = "", ldate = "";
        private bool isCustomer = false;
        public BillPaidOut()
        {
            InitializeComponent();
        }
        public BillPaidOut(bool isCustomer, string client_id, string name, string amounttobepaid)
        {
            InitializeComponent();
            this.client_id = client_id;
            this.name = name;
            this.amounttobepaid = amounttobepaid;
            this.isCustomer = isCustomer;
        }
        private void BillPaidOut_Load(object sender, EventArgs e)
        {
            bal = new BLogic();
            lbl_id.Text = client_id;
            lbl_name.Text = name;
            chk_date_enable.Checked = true;
            rb_customer.Checked = isCustomer = true;
            //searchData(); 
        }

        private void init()
        {
            if (isCustomer)
            {
                dt = bal.getListLandlordBill("ReadCust", client_id, sdate, ldate, "-1", "");
            }
            else
            {
                dt = bal.getListLandlordBill("Read", client_id, sdate, ldate, "-1", "");
            }
            loadData(dt);
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            //refresh();
            searchData();


        }

        private void searchData()
        {
            //dg_bilpaid.Rows.Clear();
            //dg_bilpaid.Refresh();


            string idname = lbl_id.Text == "" ? "" : lbl_id.Text;
            string sdate = chk_date_enable.Checked ? date_start.Text : "";
            string ldate = chk_date_enable.Checked ? date_last.Text : "";
            string status = "";
            string customer = "0";
            if (rb_customer.Checked)
            {
                customer = "1";
            }
            else if (rb_client.Checked)
            {
                customer = "0";
            }
            if (chkpaid.Checked)
            {
                status = "1";
            }
            else if (chk_unpaid.Checked)
            {
                status = "0";
            }

            dt = bal.searchBillDetail(customer, idname, sdate, ldate, status);
            if (dt == null)
                return;

            loadData(dt);
        }

        private void chk_paid_unpaid_CheckedChanged(object sender, EventArgs e)
        {
            searchData();
        }

        //public void refresh()
        //{
        //    dg_bilpaid.Rows.Clear();
        //    dg_bilpaid.Refresh();
        //    if (isCustomer)
        //    {
        //        if (chk_date_enable.Checked)
        //        {
        //            dt = bal.getListLandlordBill("ReadCust", client_id, date_start.Text, date_last.Text, "-2", "");
        //        }
        //    }
        //    else
        //    {
        //        if (chk_date_enable.Checked && chk_paid_unpaid.Checked)
        //        {
        //            dt = bal.getListLandlordBill("Read", client_id, date_start.Text, date_last.Text, "-2", "");
        //        }
        //        else
        //    if (!chk_date_enable.Checked && !chk_paid_unpaid.Checked)
        //        {
        //            dt = bal.getListLandlordBill("Read", client_id, "", "", "-1", "");
        //        }
        //        else
        //    if (!chk_date_enable.Checked && chk_paid_unpaid.Checked)
        //        {
        //            dt = bal.getListLandlordBill("Read", client_id, "", "", "1", "");
        //        }
        //        if (chk_date_enable.Checked && !chk_paid_unpaid.Checked)
        //        {
        //            dt = bal.getListLandlordBill("Read", client_id, "", "", "0", "");
        //        }
        //    }
        //    if (dt == null)
        //        return;
        //    loadData(dt);

        //}

        private void chk_date_enable_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_date_enable.Checked)
            {
                p_date.Enabled = true;
            }
            else
            {
                p_date.Enabled = false;
            }
        }

        private void chk_unpaid_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void dg_bilpaid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (e.ColumnIndex == 8)
            {
                if (dg_bilpaid.Rows[index].Cells[8].Value.ToString() == "Not Paid")
                {
                    string key = dg_bilpaid.Rows[index].Cells[1].Value.ToString();
                    string date = dg_bilpaid.Rows[index].Cells[3].Value.ToString();
                    string clientid = dg_bilpaid.Rows[index].Cells[0].Value.ToString();
                    string amount = dg_bilpaid.Rows[index].Cells[6].Value.ToString();
                    string desc = dg_bilpaid.Rows[index].Cells[7].Value.ToString();
                    string name = dg_bilpaid.Rows[index].Cells[2].Value.ToString();
                    bool chk = bal.addExtraAmountClient("PaidOutAmount", today_date.Text, clientid, int.Parse(amount), key, name, 0, "14");
                    if (chk)
                    {
                        searchData();
                    }
                    //bal.billPaidOut(key, client_id, date, amount,desc);
                }

            }
            else if (e.ColumnIndex == 7)
            {

                using (UrduDailog ud = new UrduDailog())
                {
                    ud.ShowDialog();
                    dg_bilpaid.Rows[index].Cells[7].Value = ud.Description;
                }
            }
            else if (e.ColumnIndex == 11)
            {
                string key = dg_bilpaid.Rows[index].Cells[1].Value.ToString();
                string date = dg_bilpaid.Rows[index].Cells[3].Value.ToString();
                string clientid = dg_bilpaid.Rows[index].Cells[0].Value.ToString(); ;
                if (isCustomer)
                    printData("CustomerBillingByID", date, date, key);
                else
                    printData("ClientBillingByID", date, date, key);
            }
        }

        private void btn_print_all_Click(object sender, EventArgs e)
        {

            if (chk_date_enable.Checked)
            {
                if (isCustomer)
                {
                    printData("CustomerBilling", date_start.Text, date_last.Text, lbl_id.Text);
                }
                else
                {
                    printData("ClientBilling", date_start.Text, date_last.Text, lbl_id.Text);
                }
            }
        }

        public void printData(string action, string sdate, string ldate, string id)
        {
            DataTable dt = null;
            using (AllReportsCC rc = new AllReportsCC())
            {
                dt = new BLogic().p_report_CustomerClient(action, id, sdate, ldate);
                if (isCustomer)
                    rc.printA7Report(isCustomer, dt);
                else
                    rc.printA4hReport(false, dt);
                rc.ShowDialog();
            }
        }

        public void loadData(DataTable dt)
        {
            /*hideColumnm();

            foreach (DataRow dr in dt.Rows)
            {
                string id = dr[0].ToString();
                string name = dr[1].ToString();
                string key = dr[2].ToString();
                string date = dr[3].ToString();
                string quantity = dr[4].ToString();
                string gtotal = dr[5].ToString();
                string status = dr[6].ToString();
                string paidoutdate = dr[7].ToString();
                string desc = dr[8].ToString();
                string product = dr[9].ToString();
                string rem_amount= dr[10].ToString();
                string paidamount = dr[11].ToString();
                addGridRow(id, date,quantity,name,key,gtotal,status,paidoutdate,desc,product,rem_amount, paidamount);
            }*/
            if (dt == null)
                return;
            if (dt.Rows.Count == 0)
                return;
            dg_bilpaid.DataSource = dt;

        }



        private void chk_all_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rb_customer_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_customer.Checked)
            {
                rb_client.Checked = false;
                isCustomer = true;
                lbl_id.Text = "";
                lbl_name.Text = "";

                btn_search_Click(this, new EventArgs());
            }
        }

        private void rb_client_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_client.Checked)
            {
                rb_customer.Checked = false;
                isCustomer = false;
                lbl_id.Text = "";
                lbl_name.Text = "";

                searchData();
            }
        }



        private void chkpaid_Click(object sender, EventArgs e)
        {
            if (chkpaid.Checked && chk_unpaid.Checked)
            {
                chkpaid.Checked = true;
                chk_unpaid.Checked = false;
            }

        }

        private void chk_unpaid_Click(object sender, EventArgs e)
        {
            if (chkpaid.Checked && chk_unpaid.Checked)
            {
                chkpaid.Checked = false;
                chk_unpaid.Checked = true;
            }

        }

        private void btn_printlist_Click(object sender, EventArgs e)
        {
            using (AllReportsCC rc = new AllReportsCC())
            {
                bool chk = true;
                if (rb_customer.Checked)
                {
                    List<object> obj = (List<object>)new BLReport().p_CustBillsandReceivings(sdate, ldate, lbl_id.Text);
                    if (obj == null)
                    {
                        return;
                    }
                    dt = (DataTable)obj[1];

                    chk = true;


                    int balanceR = 0, bill = 0, receiving = 0, initialBalance = 0;
                    DataRow cr = bal.getLastBalance(lbl_id.Text, sdate);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            initialBalance = int.Parse(dr[5].ToString()) - int.Parse(dr[3].ToString());
                        }
                    }
                    if (cr != null)
                    {
                        //DataRow cr = dt_customersale.Rows[0];


                        balanceR = int.Parse(cr[1].ToString());
                        bill = int.Parse(cr[2].ToString());
                        receiving = int.Parse(cr[3].ToString());
                        // initialBalance = int.Parse(cr[4].ToString()) ;
                    }

                    rc.BillandRecevings(null, dt, null, lbl_id.Text, lbl_name.Text, sdate, ldate, initialBalance + "",5);
                    rc.ShowDialog();



                }
                else if (rb_client.Checked)
                {
                    chk = false;
                    rc.printBillList(dt, lbl_id.Text, lbl_name.Text, String.Format("{0} - {1}", date_start.Text, date_last.Text));
                    rc.ShowDialog();
                }
            }
        }

        public void hideColumnm()
        {
            if (isCustomer)
            {
                this.dg_bilpaid.Columns[5].Visible = false;
                this.dg_bilpaid.Columns[8].Visible = false;
                this.dg_bilpaid.Columns[9].Visible = false;
                this.dg_bilpaid.Columns[10].HeaderText = "Cash Received";
                this.dg_bilpaid.Columns[11].HeaderText = "Print";
            }
            else
            {
                this.dg_bilpaid.Columns[5].Visible = true;
                this.dg_bilpaid.Columns[9].Visible = true;
                //this.dg_bilpaid.Columns[8].Visible = false;
                this.dg_bilpaid.Columns[8].Visible = true;
                this.dg_bilpaid.Columns[11].HeaderText = "Status";
                this.dg_bilpaid.Columns[10].HeaderText = "Paid Date";
            }
        }
        private void addGridRow(string _ID, string _date, string quantity, string name, string _key, string gtotal, string status
            , string paidoutdate, string desc, string product, string rem_amount, string paidAmount)
        {
            int count = this.dg_bilpaid.Rows.Count;
            this.dg_bilpaid.Rows.Add();
            this.dg_bilpaid.Rows[count - 1].Cells[0].Value = _ID;
            this.dg_bilpaid.Rows[count - 1].Cells[1].Value = _key;
            this.dg_bilpaid.Rows[count - 1].Cells[2].Value = name;
            this.dg_bilpaid.Rows[count - 1].Cells[3].Value = _date;
            this.dg_bilpaid.Rows[count - 1].Cells[4].Value = product;
            this.dg_bilpaid.Rows[count - 1].Cells[5].Value = quantity;
            this.dg_bilpaid.Rows[count - 1].Cells[6].Value = gtotal;
            this.dg_bilpaid.Rows[count - 1].Cells[7].Value = desc;

            if (status == "0")
            {
                this.dg_bilpaid.Rows[count - 1].Cells[8].Value = "Not Paid";
            }
            else
            {
                this.dg_bilpaid.Rows[count - 1].Cells[8].Value = "Paid";
            }

            if (isCustomer)
                this.dg_bilpaid.Rows[count - 1].Cells[9].Value = rem_amount;
            else
                this.dg_bilpaid.Rows[count - 1].Cells[9].Value = paidoutdate;
            this.dg_bilpaid.Rows[count - 1].Cells[10].Value = paidAmount;
            this.dg_bilpaid.Rows[count - 1].Cells[11].Value = "Print";
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            DataRow dr;

            switch (keyData)
            {
                case Keys.Escape:
                    {
                        this.Close();
                        return true;
                    }
                case Keys.Enter:
                    if (txt_search.Focused || txt_search.ContainsFocus)
                    {
                        Search s = null;
                        if (rb_customer.Checked)
                        {
                            s = new Search(2, txt_search.Text);
                        }
                        else
                        {
                            s = new Search(1, txt_search.Text);
                        }
                        s.ShowDialog();
                        txt_search.Text = s.Name;
                        lbl_name.Text = s.Name;
                        lbl_id.Text = s.Id;
                        btn_search.Focus();

                    }
                    else if (btn_search.ContainsFocus)
                    {
                        btn_search_Click(this, new EventArgs());
                    }
                    return true;
            }



            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
