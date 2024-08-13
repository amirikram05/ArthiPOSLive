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
    public partial class DeleteDailog : Form
    {
        private BLogic bal;
        public DeleteDailog()
        {
            InitializeComponent();
        }
        public string ID;
        public string type;
        public string khataid;
        public string billid;
        public string rec_id;
        public string bill_amount;
        public string paid_amount;
        public string discount;
        public string date;
        public string name;
        public bool check = false;
        public int cashType=0;
        public string transaction = "";
        public string transactionid = "";
        public string account_type = "";
        public string account_typeid = "";
        public string category_id = "";

        public DeleteDailog(string id,string type,string name,string khataid,
            string billid,string rec_id,string bill_amount,string paid_amount,
            string discount,string date,int cashType,string tranid,string transname,string actypeid,string actype,string cateid)
        {
            InitializeComponent();
            this.ID = id;
            this.bal = new BLogic();
            this.type = type;
            this.khataid = khataid;
            this.billid = billid;
            this.rec_id = rec_id;
            this.bill_amount = bill_amount;
            this.paid_amount = paid_amount;
            this.discount = discount;
            this.date = date;
            this.name = name;
            this.cashType = cashType;
            this.transactionid = tranid;
            this.account_typeid = actypeid;
            this.transaction = transname;//used as expenseid
            this.account_type = actype;
            this.category_id = cateid;
            lbl_cateid.Text = this.category_id;

            label10.Text = this.name;
            lbl_date.Text = this.date;
            lbl_bill_id.Text = this.billid;
            lbl_type.Text = this.type;
            lbl_khata.Text = this.khataid;
            lbl_rec_id.Text = this.rec_id;
            lbl_paid_amount.Text = this.bill_amount;
            lbl_paid_amount.Text = this.paid_amount;
            lbl_discount.Text = this.discount;
            lbl_total.Text = "" + (int.Parse(this.paid_amount) + int.Parse(this.discount));
            lbl_transid.Text = this.transactionid;
            lbl_transaction.Text = this.transaction;
            lbl_acctypeid.Text = this.account_typeid;
            lbl_acctype.Text = this.account_type;

        }
        public DeleteDailog(DataRow dr,int cashType)
        {
            InitializeComponent();
            this.bal = new BLogic();
            this.ID = dr[3].ToString();
            this.khataid = dr[3].ToString();
            this.rec_id = dr[1].ToString();
            this.date = dr[2].ToString();
            this.name = dr[4].ToString();
            this.paid_amount = dr[5].ToString();
            this.discount = dr[6].ToString();
            this.billid = dr[7].ToString(); 
            this.type = dr[8].ToString(); 
            this.bill_amount = dr[9].ToString();
            this.cashType = cashType;
            
            this.transactionid = dr[13].ToString();
            this.transaction = dr[12].ToString(); ;
            this.account_type = "";
            this.account_typeid = dr[14].ToString();
            this.category_id = dr[15].ToString();

            lbl_cateid.Text = this.category_id;
            label10.Text = this.name;
            lbl_date.Text = this.date;
            lbl_bill_id.Text = this.billid;
            lbl_type.Text = this.type;
            lbl_khata.Text = this.khataid;
            lbl_rec_id.Text = this.rec_id;
            lbl_paid_amount.Text = this.bill_amount;
            lbl_paid_amount.Text = this.paid_amount;
            lbl_discount.Text = this.discount;
            lbl_total.Text = "" + (int.Parse(this.paid_amount) + int.Parse(this.discount));
            lbl_transid.Text = this.transactionid;
            lbl_transaction.Text = this.transaction;
            lbl_acctypeid.Text = this.account_typeid;
            lbl_acctype.Text = this.account_type;
        }



        private void btn_delete_Click(object sender, EventArgs e)
        {
            //int chkType = 0;
            //if (type == "Customer")
            //{ chkType = 2; }
            //else if (type == "ClientInvest")
            //{ chkType = 4; }
            //else if (type == "Admin")
            //{ chkType = 8; }
            //return;
            bool check = new BLogic().p_addCash("Delete", date, int.Parse(ID), name, int.Parse(paid_amount),
                int.Parse(discount), cashType, billid, this.transaction, lbl_transid.Text, lbl_type.Text, lbl_acctypeid.Text, "","D",category_id);
            if (check)
            {
                this.check = check;
                this.Close();

            }
            else
                MessageBox.Show("Error, Contact Admin..");

            /* if (type=="Customer")
             {
                 check = bal.p_customer_Delete("Delete",date, name, "", int.Parse(this.paid_amount) , int.Parse(this.discount), (rec_id == "" ? 0 : int.Parse(rec_id)), type, "");
                 bal.update_today_sales(date);
             }
             else if(type=="ClientInvest")
             {
                 //check = bal.p_customer_Delete("Delete", date, name, "", int.Parse(this.paid_amount), int.Parse(this.discount), (rec_id == "" ? 0 : int.Parse(rec_id)), type, "");

             }*/

        }
    }
}
