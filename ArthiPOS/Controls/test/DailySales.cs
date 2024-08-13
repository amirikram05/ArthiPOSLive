using ArthiPOS.Controls.dashboard;
using ArthiPOS.utill;
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

namespace ArthiPOS.Controls.test
{
    public partial class DailySales : Form
    {
        private BLogic bal;
        public DailySales(string date)
        {
            InitializeComponent();
            this.date = date;
            init();
        }
        public void init()
        {
            bal = new BLogic();
            comb_list.SelectedIndex = 0;
            chk_sort.Checked = false;
            readSales(7, chk_sort.Checked ? 1 : 0, date, date, date);
        }
        private int getCheck()
        {
            int tcheck = 0;
            if (comb_list.SelectedIndex == 0) { tcheck = 7; }//All  
            else if (!chk_date.Checked && comb_list.SelectedIndex == 1 && checkBox1.CheckState == CheckState.Indeterminate) { tcheck = 1; }//ALL Sales specific date
            else if (!chk_date.Checked && comb_list.SelectedIndex == 2) { tcheck = 2; }//Paid Sales
            else if (!chk_date.Checked && comb_list.SelectedIndex == 3) { tcheck = 3; }// UnPaid
            else if (chk_date.Checked == true && comb_list.SelectedIndex == 1) { if (checkBox1.CheckState == CheckState.Checked) tcheck = 4; else if (checkBox1.CheckState == CheckState.Unchecked || checkBox1.CheckState == CheckState.Indeterminate) tcheck = 5; }// All on Specific Date and UnPaid
            else if (chk_date.Checked == true && comb_list.SelectedIndex == 2) { tcheck = 5; }//All on Specific Date and Paid
            else if (chk_date.Checked == true && comb_list.SelectedIndex == 3) { tcheck = 6; }//All on Specific Date 
            else if (comb_list.SelectedIndex == 4) { tcheck = 8; }
            else if (comb_list.SelectedIndex == 5) { tcheck = 9; }
            return tcheck;
        }
        public void readSales(int tcheck, int sort, string sdate, string ldate, string date)
        {



            DataTable dt = new BLogic().readShopSales(label1.Text, sdate, ldate, sdate, chk_sort.Checked ? 1 : 0, tcheck, -1);

            if (tcheck == 8)
            {
                DataRow row = dt.Rows[0];
                if (row[0] == "") return;
                int quantity = int.Parse(row[0].ToString() == "" ? "0" : row[0].ToString());
                int total = int.Parse(row[1].ToString() == "" ? "0" : row[1].ToString());
                _lbl_total.Text = "Total     Quantity = " + quantity;
                lbl_total.Text = "" + total;
                //Total Calculate
            }
            else
            {
                ds_result.DataSource = dt;
            }
        }
        private void addCoumn(string name)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            if (name == "")
                return;
            // Set column values
            column.Name = "colx" + dgv_sales.ColumnCount + 1;
            column.HeaderText = name;

            //Add the columns to the grid
            int index = dgv_sales.ColumnCount - 3;
            dgv_sales.Columns.Insert(index, column);

        }
        private void addRow(string name, string userid, string quantity, string product, string t_date, string total_amount, string ispaid)
        {

            DataGridViewRow row = new DataGridViewRow();
            row.Cells[0].Value = dgv_sales.Rows.Count;
            //row.Cells[1].Value = 0;
            //row.Cells[2].Value = 0;
            row.Cells[0].Value = userid;
            row.Cells[1].Value = name;
            row.Cells[2].Value = quantity;
            row.Cells[3].Value = product;
            row.Cells[4].Value = date;
            row.Cells[7].Value = total_amount;
            row.Cells[8].Value = ds_result.Rows.Count + 1;


            //ds_result.Rows.Insert(0, row);
            dgv_sales.Rows.Add(row);

        }

        string date;

        private void btn_add_Click(object sender, EventArgs e)
        {

            date = date_start.Text;

        }

        private void dgv_sales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            calulateTotal(e);
        }
        private void calulateTotal(DataGridViewCellEventArgs e)
        {
            int indexC = e.ColumnIndex;
            int indexR = e.RowIndex;
            int columnCount = dgv_sales.ColumnCount;
            if (indexC == 5)
            {
                int total = 0;
                int commission = 0;
                int quantity = int.Parse(dgv_sales.Rows[indexR].Cells[2].FormattedValue.ToString());
                int rate = int.Parse(dgv_sales.Rows[indexR].Cells[3].FormattedValue.ToString());
                {
                    //total= getTotal(dgv_sales.Rows[indexR].Cells[2].FormattedValue.ToString());
                    //int.Parse(dgv_sales.Rows[indexR].Cells[i].FormattedValue.ToString() == "" ? "0" : dgv_sales.Rows[indexR].Cells[i].FormattedValue.ToString());
                }
                total = quantity * rate;

                dgv_sales.Rows[indexR].Cells[dgv_sales.ColumnCount - 2].Value = "" + total;


            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
                /* case Keys.Enter:
                     int indexC = currentViewCell.ColumnIndex;
                     int indexR = currentViewCell.RowIndex;
                     if (indexC == 1)
                     {
                         searchDialog(indexR, indexC, 3, "");
                     }
                     else if(txt_name.ContainsFocus)
                     {
                         searchDialog(0, 0, 1, txt_name.Text);
                     }
                     return true;*/
                case Keys.Control | Keys.U:
                    ds_result_CellClick(this, currentViewCell);
                    return true;
                case Keys.Delete:
                    ds_result_CellClick(this, currentViewCell);
                    return true;
                case Keys.Control | Keys.Up:
                        dgv_sales.Focus();
                        dgv_sales.CurrentCell = dgv_sales.Rows[0].Cells[1];
                        dgv_sales.BeginEdit(true);
                    return true;
                case Keys.Control | Keys.Down:
                        // Move focus to the first cell of the first row in the second DataGridView
                        ds_result.Focus();
                        ds_result.CurrentCell = ds_result.Rows[0].Cells[2];
                        ds_result.BeginEdit(true);
                    return true;
                case Keys.Control | Keys.F://Search Name
                                           //searchDialog(0, 0, 1, txt_name.Text);
                    searchDialog(currentViewCell.RowIndex, 1, 1,
                        dgv_sales.Rows[currentViewCell.RowIndex].Cells[1].FormattedValue.ToString());

                    return true;
                case Keys.Control | Keys.I:
                        searchDialog(currentViewCell.RowIndex, 2, 1,
                       dgv_sales.Rows[currentViewCell.RowIndex].Cells[1].FormattedValue.ToString());
                        return true;
                case Keys.Control | Keys.D://date
                    if (currentViewCell.ColumnIndex == 5)
                    {
                        int ind = -1;
                        if (currentViewCell.RowIndex == 0)
                            ind = 0;

                        dgv_sales.Rows[currentViewCell.RowIndex].Cells[5].Value = date_start.Text;
                        calulateTotal(currentViewCell);

                    }
                    else if (currentViewCell.ColumnIndex == 6)
                    {
                        int ind = -1;
                        if (currentViewCell.RowIndex == 0)
                            ind = 0;

                        dgv_sales.Rows[currentViewCell.RowIndex].Cells[6].Value = date_start.Text;
                        calulateTotal(currentViewCell);

                    }
                    return true;
                case Keys.Control | Keys.V://search product
                    searchDialog(currentViewCell.RowIndex, 4, 3, "");
                    return true;
                case Keys.Control | Keys.S:
                    if (dgv_sales.IsCurrentCellInEditMode)
                    {
                        dgv_sales.EndEdit();
                    }
                    selectedRowsCalulate();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        public int getTotal(string input)
        {
            // Split the input by commas to get each quantity-rate pair
            string[] pairsC = input.Split(',');
            int total = 0;
            foreach (string pairc in pairsC)
            {
                string[] parts = pairc.Split('=');

                if (parts.Length == 2)
                {
                    string quantityPart = parts[0].Trim();
                    string ratePart = parts[1].Trim();
                    int quantity = 0;
                    int rate = 0;
                    // Try to parse the parts into integers
                    if (int.TryParse(quantityPart, out quantity) && int.TryParse(ratePart, out rate))
                    {
                        // Now you have the quantity and rate per item
                        total += rate * quantity;
                    }
                    else
                    {
                        Console.WriteLine("Invalid format: Unable to parse quantity or rate.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format: Input string does not contain quantity and rate separated by '='.");

                }
            }
            return total;
        }
        private DataGridViewCellEventArgs currentViewCell;
        private void dgv_sales_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.currentViewCell = e;
            int indexC = e.ColumnIndex;
            int indexR = e.RowIndex;
            calulateTotal(e);
        }
        Search search;
        public void searchDialog(int indexR, int indexC, int action, string searchTxt)
        {


            using (search = new Search(action, searchTxt, 1))
            {


                DialogResult res = search.ShowDialog();
                if (action == 1)
                {
                    if (indexC == 1)
                    {
                        dgv_sales.Rows[currentViewCell.RowIndex].Cells[1].Value = search.Name;
                        dgv_sales.Rows[currentViewCell.RowIndex].Cells[0].Value = search.Id;
                    }
                    else if (indexC == 2)
                    {
                        txt_name.Text = search.Name;
                        label1.Text = search.Id;
                    }
                }
                else
                if (action == 3)
                {
                    dgv_sales.Rows[currentViewCell.RowIndex].Cells[4].Value = search.Name;
                    dgv_sales.Rows[currentViewCell.RowIndex].Cells[dgv_sales.ColumnCount-1].Value = search.Id;

                    //dgv_sales.Rows[currentViewCell.RowIndex].Cells[2].Value = search.BipComm;
                    //dgv_sales.Rows[currentViewCell.RowIndex].Cells[3].Value = search.Labour;
                }

                search.Close();



                return;
            }
        }
        private void selectedRowsCalulate()
        {
            dgv_sales_CellClick(this, currentViewCell);

        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {
        }
        int sort = 1;
        private void chk_sort_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_sort.Checked)
                sort = 1;
            else
                sort = 2;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            readSales(getCheck(), chk_sort.Checked ? 1 : 0, date_start.Text, date_last.Text, date_start.Text);
        }

        private void chk_date_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_date.Checked) date_panel.Enabled = true;
            else date_panel.Enabled = false;
        }
        private void dgv_sales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexC = e.ColumnIndex;
            int indexR = e.RowIndex;
            if (indexC == 8)
            {
                string name = dgv_sales.Columns[indexC].HeaderText.ToString();
                //string commission = dgv_sales.Rows[indexR].Cells[2].FormattedValue.ToString();
                //string labour = dgv_sales.Rows[indexR].Cells[3].FormattedValue.ToString();
                int total = 0;
                int commission = 0;
                int quantity = int.Parse(dgv_sales.Rows[indexR].Cells[2].FormattedValue.ToString());
                int rate = int.Parse(dgv_sales.Rows[indexR].Cells[3].FormattedValue.ToString());
                {
                    //total= getTotal(dgv_sales.Rows[indexR].Cells[2].FormattedValue.ToString());
                    //int.Parse(dgv_sales.Rows[indexR].Cells[i].FormattedValue.ToString() == "" ? "0" : dgv_sales.Rows[indexR].Cells[i].FormattedValue.ToString());
                }
                total = quantity * rate;
                string id = "0";
                string uid = dgv_sales.Rows[indexR].Cells[0].FormattedValue.ToString();
                string uname = dgv_sales.Rows[indexR].Cells[1].FormattedValue.ToString();
                string product = dgv_sales.Rows[indexR].Cells[4].FormattedValue.ToString();
                string tdate = dgv_sales.Rows[indexR].Cells[5].FormattedValue.ToString();
                string lasttdate = dgv_sales.Rows[indexR].Cells[6].FormattedValue.ToString();

                string size = dgv_sales.Rows[indexR].Cells[7].FormattedValue.ToString();
                string labour = dgv_sales.Rows[indexR].Cells[11].FormattedValue.ToString();
                string productid = dgv_sales.Rows[indexR].Cells[12].FormattedValue.ToString();

                if ((uid == "" || uid==null || uid=="0") || (name == "" || name == null)) return;

                dgv_sales.Rows[indexR].Cells[dgv_sales.ColumnCount - 2].Value = "" + total;

                List<object> obj = (List<object>)new BLogic().shopCrud_InsertUpdate("I", tdate, lasttdate, uname, uid, "" + quantity,
                    "" + rate, size, product, tdate, "" + total, "" + 0, 0, -1, labour == "" ? 0 : int.Parse(labour), productid);
                if (obj == null)
                {
                    MessageBox.Show("Not Save");
                    return;
                }
                //int chk = (int)obj[0];
                readSales(7, chk_sort.Checked ? 1 : 0, date_start.Text, date_last.Text, date);
                dgv_sales.Rows.RemoveAt(0);
                dgv_sales.Rows.Add();
            }
        }

        private void ds_result_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (comb_list.SelectedIndex != 5)
            {
                int indexC = e.ColumnIndex;
                int indexR = e.RowIndex;
                if (indexC == 0)//Update
                {
                    string id = ds_result.Rows[indexR].Cells[2].FormattedValue.ToString();
                    string tdate = ds_result.Rows[indexR].Cells[3].FormattedValue.ToString();
                    string enddate = ds_result.Rows[indexR].Cells[4].FormattedValue.ToString();
                    string uid = ds_result.Rows[indexR].Cells[5].FormattedValue.ToString();
                    string uname = ds_result.Rows[indexR].Cells[6].FormattedValue.ToString();
                    string quantity = ds_result.Rows[indexR].Cells[7].FormattedValue.ToString();
                    string rate = ds_result.Rows[indexR].Cells[8].FormattedValue.ToString();
                    string product = ds_result.Rows[indexR].Cells[9].FormattedValue.ToString();
                    string size = ds_result.Rows[indexR].Cells[10].FormattedValue.ToString();
                    string total = ds_result.Rows[indexR].Cells[11].FormattedValue.ToString();
                    string labour = ds_result.Rows[indexR].Cells[13].FormattedValue.ToString();
                    string productid = ds_result.Rows[indexR].Cells[14].FormattedValue.ToString();

                    List<object> obj = (List<object>)new BLogic().shopCrud_InsertUpdate("U", date_start.Text, date_last.Text, uname, uid, "" + quantity, "" + rate, size, product, tdate, "" + total, "" + 1, 0, id==""?0:int.Parse(id), labour == "" ? 0 : int.Parse(labour), productid);
                    if (obj == null)
                    {
                        MessageBox.Show("Not Save");
                        return;
                    }
                }
                else if (indexC == 1)//Delete
                {
                    string id = ds_result.Rows[indexR].Cells["No"].FormattedValue.ToString();
                    var result = MessageBox.Show("Are you sure you want to delete this item?",
                                         "Confirm Delete",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        // Perform delete operation here
                        List<object> obj = (List<object>)bal.shopCrud_InsertUpdate("D", "", "", "", "", "", "", "", "", "", "", "" + 0, 0, id == "" ? 0 : int.Parse(id), 0,"0");
                        if (obj == null)
                        {
                            MessageBox.Show("Not Delete");
                            return;
                        }
                        MessageBox.Show("Item deleted.");
                        readSales(7, chk_sort.Checked ? 1 : 0, date_start.Text, date_start.Text, date_last.Text);

                    }


                }
            }
        }

        private void btn_add_Click_1(object sender, EventArgs e)
        {
            List<object> obj = (List<object>)bal.shopCrud_InsertUpdate("U", date_start.Text, date_last.Text, "", label1.Text, "", "", "", "", "", "", "" + 2, 0, -1, 0,"0");
            if (obj == null)
            {
                MessageBox.Show("Not Save");
                return;
            }
            readSales(7, chk_sort.Checked ? 1 : 0, date, date, date);

        }
        private int isPaid = 0;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isPaid == 0)
            {
                isPaid = 1;
                checkBox1.CheckState = CheckState.Checked;
            }
            else if (isPaid == 1)
            {
                isPaid = 2;
                checkBox1.CheckState = CheckState.Indeterminate;

            }
            else if (isPaid == 2)
            {
                isPaid = 0;
                checkBox1.CheckState = CheckState.Unchecked;
            }

        }

        private void ds_result_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.currentViewCell = e;
        }
    }
}
