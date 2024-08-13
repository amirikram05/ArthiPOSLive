using ArthiPOS.Properties;
using ArthiPOS.Reporting;
using ArthiPOS.utill;
using ArthiPOS.Utilllll;
using BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class AddExpenseCash : Form
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ur-PK");

        private bool AccountStatus = false;
        public AddExpenseCash(string title)
        {
            InitializeComponent();
            lbl_title.Text = title;
            txt_desc.Focus();
            label3.Text = Resources.ResourceManager.GetString("desc", ci);
            //btn_refreshkey_Click(this,new EventArgs());
            lbl_id.Text = "";
            DataTable dt = new BLogic().getCashInoutAccount();
            DataRow dr = dt.Rows[0];
            string cashinoutStatus = dr[0].ToString();
            if(cashinoutStatus=="" || cashinoutStatus=="0")
            {
                AccountStatus = false;
            }
            else
            {
                AccountStatus = true;
            }
        }
        private Search search = null;
        private string temp = "";
        public void searchDialog(int action, string searchTxt)
        {
            
            int ck = int.Parse(lbl_cate_id.Text==""?"0": lbl_cate_id.Text);




            using (search = new Search(action, searchTxt, ck))
            {

                
                DialogResult res = search.ShowDialog();
                if (action == 2)
                {
                    txt_nameid.Text = search.Name;
                    lbl_id.Text = search.Id;
                    lbl_augrai.Text = "" + search.RAmount;
                }
                else if (action == 1|| action == 4  || action == 6 || action == 7 )
                {
                    txt_nameid.Text = search.Name;
                    lbl_id.Text = search.Id;
                    lbl_augrai.Text = "" + search.RAmount;
                    lbl_msg.AppendText("Augrai = " + search.RAmount + "<br />");

                }
                else if (action == 5)
                {
                    //if (string.IsNullOrEmpty(txt_desc.Text))
                    {
                        txt_desc.Text = search.Name;

                    }
                    /*else
                    {
                        txt_desc.AppendText(" ("+search.Name+") ");

                    }*/
                    txt_cate.Text = search.ExpenseID;
                    lbl_expid.Text = search.Id;
                    //lbl_account_trans_name.Text = search.TransName;
                    //lbl_transaction.Text = search.Transid;
                    //lbl_account_trans_id.Text = search.AccountTransactionid;
                    
                    search.Close();
                    if (search.isEscapePress)
                        return;
                    txt_desc.Focus();
                }
                else if (action == 8)
                {
                    txt_cate.Text = search.DetailsUrdu;
                    lbl_catedetail.Text = search.DetailsEng;
                    lbl_cate_id.Text = search.Id;
                    lbl_cate_name.Text = search.Name;

                    lbl_transaction_id.Text = search.Transid;
                    lbl_transname.Text = search.TransName;
                    lbl_account_trans_id.Text = search.AccountTransactionid;
                    lbl_account_trans_name.Text = search.AccountTransactionName;



                    search.Close();

                }
                
                search.Close();
              

                return;
            }
        }

        
       
       
        private void btnAddcash_Click(object sender, EventArgs e)
        {

             if (!checkCondtionaMatch())
                return;
            string date = ledger_date.Text;
            string desc = txt_desc.Text;
            string id = lbl_id.Text;
            string name = txt_nameid.Text;
            int cash = int.Parse(txt_cash.Text == "" ? "" + 0 : txt_cash.Text);
            int discount = int.Parse(txt_discount.Text == "" ? "" + 0 : txt_discount.Text);
            string expensetypeid = lbl_expid.Text;
            string transactionid = lbl_transaction_id.Text;
            string acctransid=lbl_account_trans_id.Text;
            string cateid = lbl_cate_id.Text;
            if (expensetypeid == "" || expensetypeid == "xxx") { MessageBox.Show("Transaction ID Empty. Please update."); return; }
            if ((lbl_cate_name.Text != "Expense" && lbl_cate_name.Text != "ShopExpense") && (id == "" || name=="") )
            {
                txt_nameid.Focus();
                return;
            }
            if (cash + discount >= 0)
            {
                string txt = txt_desc.Text;
                if (txt != "")
                    txt = ", " + txt;


               
                //string action = "Customer";
                //if (rb_client.Checked)
                //{ 
                //    action = "Client";
                //    if (checkSelection == 2 || checkSelection==3)
                //        action = "ClientInvest";
                    
                //}




                string lkey = lbl_key.Text;
                if (lkey == "")
                {
                    MessageBox.Show("Refreshing Invoice ID...");
                    btn_refreshkey_Click(this, new EventArgs());
                    lkey = lbl_key.Text;
                }


                string idnam = txt_nameid.Text + "-" + lbl_id.Text;
                //desc = $"{desc}={lbl_catedetail.Text},{"="},{txt_cash.Text} ,{Resources.ResourceManager.GetString("a2021", ci)} = { (lbl_augrai.Text == "" || txt_cash.Text == "" || ((int.Parse(lbl_augrai.Text) - int.Parse(txt_cash.Text))) == 0 ? "0" : "" + (int.Parse(lbl_augrai.Text) - (int.Parse(txt_cash.Text) + (txt_discount.Text == "" ? 0 : int.Parse(txt_discount.Text)))))} ,{txt_cate.Text}";
                string augrait = "0";
                if (lbl_augrai.Text == "" || txt_cash.Text == "" || ((int.Parse(lbl_augrai.Text) - int.Parse(txt_cash.Text))) <= 0)
                {
                    augrait = "0";
                }
                else
                {
                    string tid = lbl_cate_id.Text;
                    if (tid=="1" || tid == "2" || tid == "5" || tid == "8")
                        augrait = "" + (int.Parse(lbl_augrai.Text) - (int.Parse(txt_cash.Text) + (txt_discount.Text == "" ? 0 : int.Parse(txt_discount.Text))));
                    else
                        augrait = "" + (int.Parse(lbl_augrai.Text) + (int.Parse(txt_cash.Text) + (txt_discount.Text == "" ? 0 : int.Parse(txt_discount.Text))));
                }

                if(txt_discount.Text=="")
                {
                    txt_discount.Text = "0";

                }

                    desc = string.Format("{3} = {2} ({1}) {0}",
                        txt_desc.Text,
                        lbl_key.Text
                    , name == "" ? "" : name+"-"+lbl_id.Text
                    //, Resources.ResourceManager.GetString("a2021", ci)+"="+ augrait
                    //, Resources.ResourceManager.GetString("a0006", ci) + "=" + txt_discount.Text
                    ,  txt_cash.Text
                    );
                lbl_msg.Text = desc;
                bool check = false;
                /*if (AccountStatus)
                {
                    new BLogic().addTodaySales(date);
                    if (lbl_cate_name.Text == "Customer")
                    {
                        check = new BLogic().p_addCash(lbl_cate_name.Text, date, int.Parse(id), desc,
                            cash, discount, 4, lbl_key.Text, expensetypeid, transactionid, txt_nameid.Text, acctransid,"","I",cateid);
                    }
                    else if (lbl_cate_name.Text == "Expense" || lbl_cate_name.Text == "ShopExpense")
                    {
                        desc = string.Format("{1} ,{0}",
                        lbl_catedetail.Text
                        , txt_desc.Text + "=" + txt_cash.Text
                        );
                        lbl_msg.Text = desc;
                        //new BLogic().addTodaySales(date);
                        if (new BLogic().insertTodayExpense(date, txt_desc.Text, "" + cash, lbl_key.Text,
                            string.Format("p_{0}_{1}", lbl_cate_name.Text, 0), lbl_cate_name.Text, "" + 0, expensetypeid, desc, transactionid,cateid, expensetypeid))
                        {
                            new BLogic().p_ledger_CRUD("Insert", transactionid, acctransid, "C", cash, int.Parse(id),
                                lbl_cate_name.Text, date, lbl_key.Text, expensetypeid, "I", cateid);
                            new BLogic().addBalanceSheetExpense(desc, "" + cash, date,
                                lbl_cate_name.Text, lbl_key.Text, "credit", "Insert", "0",acctransid, cateid);
                            new BLogic().update_today_sales(date);
                            lbl_msg.Text = "Expense Added.";
                           // btn_refreshkey_Click(this, new EventArgs());
                            txt_cash.Text = "";
                            txt_desc.Text = "";
                            txt_desc.Focus();
                            check = true;
                        }
                    }
                    else if (lbl_cate_name.Text == "ClientInvest")
                    {
                        check = new BLogic().p_addCash(lbl_cate_name.Text, date, int.Parse(id), desc, cash,
                            discount, int.Parse(lbl_cate_id.Text), lbl_key.Text, expensetypeid, transactionid,
                            txt_nameid.Text, acctransid,"","I", cateid);
                    }
                    else if (lbl_cate_name.Text == "Client" || lbl_cate_name.Text == "ClientRemReceive")
                    { 
                        check = new BLogic().p_addCash(lbl_cate_name.Text, date, int.Parse(id),
                            desc, cash, discount, int.Parse(lbl_cate_id.Text), 
                            lbl_key.Text, expensetypeid, transactionid, txt_nameid.Text, acctransid,"", "I", cateid);
                    }
                    else if (lbl_cate_name.Text == "Admin")
                    {
                        //return;
                        check = new BLogic().p_addCash(lbl_cate_name.Text, date, int.Parse(id), desc, cash, discount,
                            int.Parse(lbl_cate_id.Text), lbl_key.Text, expensetypeid, transactionid,
                            txt_nameid.Text, acctransid,"", "I", cateid);
                    }
                }
                else*/
                {
                    string entrytype = "";
                    string action = lbl_cate_name.Text;
                    int cashtype = int.Parse(lbl_cate_id.Text);
                    string nametem = "";
                    
                    if (action == "Customer")
                    {
                        entrytype = "R";
                        nametem = txt_nameid.Text;

                    }
                    else if (action == "Expense" || action=="ShopExpense")
                    {
                        entrytype = "E";
                        nametem = txt_desc.Text;

                    }
                    else if (action == "Client" || action == "ClientInvest" || action == "Admin" || action == "ClientRemReceive")
                    {
                        if (cashtype == 7 || cashtype == 5 || cashtype == 3 || cashtype == 15)
                        {
                            entrytype = "R";
                            nametem = txt_nameid.Text;

                        }
                        else if (cashtype == 8 || cashtype == 4 || cashtype == 1)
                        {
                            entrytype = "E";
                            nametem = txt_nameid.Text;

                        }
                    }
                    check=new BLogic().p_cashinout_Crud("I", lbl_key.Text, date, lbl_cate_name.Text, int.Parse(lbl_cate_id.Text), 
                        int.Parse(transactionid),int.Parse(acctransid), int.Parse(lbl_expid.Text), lbl_cate_id.Text, 
                        int.Parse(lbl_id.Text=="" ? "0": lbl_id.Text), nametem, desc, cash, discount, entrytype,cateid,"i");



                }
                if (check)
                {
                    lbl_msg.AppendText(Resources.ResourceManager.GetString("recevingcash", ci));
                    //txt_desc.Text ="";
                    txt_nameid.Focus();
                    lbl_key.Text = "";
                    txt_cash.Text = "0";
                    txt_discount.Text = "0";
                    lbl_id.Text = "";
                    txt_nameid.Text = "";
                    btn_refreshkey_Click(this,new EventArgs());
                }
            }
        }
        public void changeFocus()
        {
            if (txt_desc.Focused)
            {
                if (txt_desc.Text == "Search")
                    txt_desc.Text = "";
                if (search!=null && search.isEscapePress)
                {
                    txt_desc.Text = "";
                    return;
                }
                txt_cate.Focus();
            }
            else
            if (txt_cate.Focused)
            {
                if (txt_cate.Text == "Search")
                    txt_cate.Text = "";
                if (search.isEscapePress)
                    return;
                if (!txt_nameid.Enabled)
                    txt_cash.Focus();
                txt_nameid.Focus();
            }
            else if(txt_nameid.Focused )
            {
                if (txt_nameid.Text == "Search")
                    txt_nameid.Text = "";
                if (search.isEscapePress)
                    return;
                txt_cash.Focus();
            }
            else if (txt_cash.Focused)
            {
                if (search.isEscapePress)
                    return;
                txt_discount.Focus();
            }else
            if (txt_discount.Focused)
            {

                btnAddcash.Focus();
            }


        }

        private int item = 0;
        
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
                    txt_desc.Focus();
                    return true;
                case Keys.Control | Keys.G:
                    //Stuff
                    return true;
                case Keys.Control | Keys.N:
                    return true;
                case Keys.Enter:

                    if (txt_cate.Focused)
                    {
                        searchDialog(8, txt_cate.Text);
                        if(lbl_cate_id.Text=="12" || lbl_cate_id.Text == "14")
                            txt_nameid.Enabled = false;
                        else
                            txt_nameid.Enabled = true;
                    }
                    else if(txt_nameid.Focused)
                    {
                        if (lbl_cate_id.Text != "")
                        {
                            int cid = int.Parse(lbl_cate_id.Text);
                            int act = 1;
                            if (cid == 2)
                            { act = 2; }
                            else if (cid == 8)
                            { act = 1;
                            }

                            searchDialog(act, txt_nameid.Text);
                        }
                    }
                    /*else if (txt_desc.ContainsFocus)
                    {
                        btn_bipari_search_Click(this, new EventArgs());
                    }*/

                    changeFocus();



                    return true;
                case Keys.Control | Keys.P:

                    return true;
                case Keys.Control | Keys.Enter:
                    if (txt_nameid.ContainsFocus)
                    {
                        lbl_key.Text = new BLogic().p_getInvoiceID("Other", "0", ledger_date.Text);

                        btn_bipari_search_Click(this, new EventArgs());
                    }
                    else if (txt_desc.ContainsFocus)
                    {
                        btn_bipari_search_Click(this, new EventArgs());
                    }
                    else
                    if (checkCondtionaMatch())
                    {

                        btnAddcash_Click(this, new EventArgs());
                    }
                    return true;
                case Keys.Alt | Keys.Enter:

                    return true;


            }



            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool checkCondtionaMatch()
        {
            //if (txt_desc.Text == null)
            //{ txt_nameid.Focus(); return false; }

            //if (txt_nameid.Text == null)
            //{ txt_cash.Focus(); return false; }

            //if (txt_cash.Text == null)
            //{ txt_discount.Focus(); return false; }

            //if (txt_password.Text == null)
            //{ txt_password.Focus(); return false; }

            return true;
        }

        private void btn_bipari_search_Click(object sender, EventArgs e)
        {
            int action = 1;
            if (lbl_cate_name.Text=="Client")
            {

                action = 1;
                
                
            }
            else if (lbl_cate_name.Text == "Customer")
            {
                action = 6;
            }
           

            if (txt_desc.ContainsFocus)
            {
                searchDialog(5, txt_desc.Text);

            }
            else
            {
                searchDialog(action, txt_nameid.Text);

            }


        }

        private void txt_cash_TextChanged(object sender, EventArgs e)
        {

            int aug = lbl_augrai.Text==""?0: int.Parse(lbl_augrai.Text) ;
            int cash = 0;
            int discount = 0;

            try
            {
                cash= txt_cash.Text == "" ? 0 : int.Parse(txt_cash.Text);
                discount = txt_discount.Text == "" ? 0 : int.Parse(txt_discount.Text);

            }
            catch (FormatException)
            {
                return;
            }
            int total = 0;
            if(lbl_cate_id.Text=="1" || lbl_cate_id.Text == "2" || lbl_cate_id.Text == "5" || lbl_cate_id.Text == "7")
            {
                total = aug - (cash + discount);
                
            }
            else if (lbl_cate_id.Text == "15" || lbl_cate_id.Text == "4" || lbl_cate_id.Text == "8")
            {
                total = aug + (cash + discount);

            }

            /*if (total < 0)
            {
                total = (cash + discount);
            }*/
            lbl_remaining_amount.Text = "" + total;
            lbl_msg.Text = ""+total;
        }
        private void btn_refreshkey_Click(object sender, EventArgs e)
        {
            lbl_key.Text = new BLogic().p_getInvoiceID("Other", "0", ledger_date.Text);
        }

        private void rb_customer_CheckedChanged(object sender, EventArgs e)
        {
            lbl_id.Text = "";
            txt_nameid.Text = "";
            txt_nameid.Focus();
        }

        private void btn_cate_Click(object sender, EventArgs e)
        {

        }

        private void AddExpenseCash_Load(object sender, EventArgs e)
        {
            btn_refreshkey_Click(this,new EventArgs());
        }

        private void btn_ndate_Click(object sender, EventArgs e)
        {
            ledger_date.Value = CommonUtill.ChangeDate(ledger_date, 1);
        }

        private void btn_pdate_Click(object sender, EventArgs e)
        {
            ledger_date.Value = CommonUtill.ChangeDate(ledger_date, -1);
        }

        private void lbl_fardhisab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReportFardHisab fh = new ReportFardHisab();
            fh.ShowDialog();
        }

        private void lbl_fright_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrightDetail fd = new FrightDetail(ledger_date.Text);
            fd.ShowDialog();
        }
    }
}
