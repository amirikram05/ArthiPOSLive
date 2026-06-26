using BAL;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ArthiPOS.Controls.test
{
    public partial class ControlEXREC : UserControl
    {
        public DataTable exp;
        public DataTable rec;
        private string date;
        public int CountExp = 0;
        public int CountRec = 0;
        public ControlEXREC(string date)
        {
            InitializeComponent();
            this.date = date;
            refresh();
        }

        private void refresh()
        {
            readExpense(date);
            readReceiving(date);
        }
        private void readExpenseGrid(DataTable exp)
        {
            dt_expense.DataSource = exp;
            dt_expense.Columns[2].Visible = false;
            dt_expense.Columns[6].Visible = false;
            dt_expense.Columns[8].Visible = false;
            dt_expense.Columns[10].Visible = false;
            CountExp = dt_expense.Rows.Count;
        }
        private void readReceivingGrid(DataTable rec)
        {
            dt_receive.DataSource = rec;
            dt_receive.Columns[2].Visible = false;
            dt_receive.Columns[4].Visible = false;
            dt_receive.Columns[8].Visible = false;
            //dt_receive.Columns[9].Visible = false;
            dt_receive.Columns[10].Visible = false;
            dt_receive.Columns[11].Visible = false;
            dt_receive.Columns[12].Visible = false;
            CountRec = dt_receive.Rows.Count;
        }
        private void dt_expense_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                string id = dt_expense.Rows[index].Cells[2].Value.ToString();
                return;
                bool check = new BLogic().p_cashinout_Crud("D", "", "", "", 0, 0, 0, 0,
                    "", int.Parse(id), "", "", 0, 0, "", "", "d");
                if (check)
                {
                    dt_expense.Rows.RemoveAt(index);
                    dt_expense.Refresh();
                    refresh();
                }
            }
        }

        private void dt_receive_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                string id = dt_receive.Rows[index].Cells[2].Value.ToString();
                bool check = new BLogic().p_cashinout_Crud("D", "", "", "",
                    0, 0, 0, 0, "", int.Parse(id), "", "", 0, 0, "", "", "d");
                if (check)
                {
                    dt_receive.Rows.RemoveAt(index);
                    dt_receive.Refresh();
                    ControlExRec_Load(this, new EventArgs());
                }
            }
        }

        private void ControlExRec_Load(object sender, EventArgs e)
        {
            readExpense(date);
            DataRow dr = new BLogic().getLastCash(date, date);

            int balance = int.Parse(dr[0].ToString() == "" || dr == null ? "0" : dr[0].ToString());
            int receivings = int.Parse(lbl_receive.Text);
            int expense = int.Parse(lbl_expense.Text);
            int cbalance = int.Parse(dr[3].ToString() == "" ? "0" : dr[3].ToString());
            lbl_cash.Text = "" + balance;
            lbl_total.Text = "" + (balance + receivings - expense);
        }
        private void readReceiving(string date)
        {
            rec = new BLogic().getCashInout("Rec", date);
            readReceivingGrid(rec);
            lbl_receive.Text = "" + rec.AsEnumerable().Sum(row => row.Field<int>("CashReceive"));
        }

        private void readExpense(string date)
        {
            exp = new BLogic().getCashInout("Exp", date);
            readExpenseGrid(exp);
            lbl_expense.Text = "" + exp.AsEnumerable().Sum(row => row.Field<int>("ExpenseAmount"));


        }
    }
}
