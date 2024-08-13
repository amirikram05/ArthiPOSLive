using ArthiPOS.callback;
using ArthiPOS.Properties;
using BAL;
using CommonUtilities;
using DataMember;
using MetroFramework.Controls;
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
    public partial class Search : Form
    {
        private BLogic bal;
        private int pageindex = 1;
        private int pageSize = 20;
        public int searchType;
        private static Search instance = null;
        private string date = "";
        private int clientType = 2;
        public bool isEscapePress = false;

        public Search(int searchType, string search)
        {
            this.
            InitializeComponent();
            bal = new BLogic();
            txt_searach.Focus();
            this.searchType = searchType;
            //comb_select_searchtype.SelectedIndex = searchType;
            comb_select_searchtype.TabIndex = searchType;
            searchccpw();
            LocalizeNameGrid();
            txt_searach.Text = search;

        }

        public Search(int searchType, string search, int clientType)
        {
            this.
            InitializeComponent();
            bal = new BLogic();
            txt_searach.Focus();
            this.searchType = searchType;
            this.clientType = clientType;
            //comb_select_searchtype.SelectedIndex = searchType;
            comb_select_searchtype.TabIndex = searchType;

            searchccpw();
            LocalizeNameGrid();
            txt_searach.Text = search;
        }


        public Search(string date, int searchType, string search)
        {
            this.
            InitializeComponent();
            bal = new BLogic();
            txt_searach.Focus();
            this.searchType = searchType;
            //comb_select_searchtype.SelectedIndex = searchType;
            comb_select_searchtype.TabIndex = searchType;
            searchccpw();
            LocalizeNameGrid();
            txt_searach.Text = search;
            this.date = date;

        }

        private string id = "";
        private string name = "";
        private string type = "";
        private int ramount = 0;
        private string expID = "";

        private string transid = "";
        private string transName = "";

        private string account_transactionid = "";
        private string account_transactionname = "";
        private string address = "";
        private int oldamount = 0;


        public string ExpenseID
        {
            get { return expID; }
            set { expID = value; }
        }
        public int OldAmount
        {
            get { return oldamount; }
            set { oldamount = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public int RAmount
        {
            get { return ramount; }
            set { ramount = value; }
        }
        public string Id {
            get { return id; }
            set { id = value; }
        }
        public string AccountTransactionid
        {
            get { return account_transactionid; }
            set { account_transactionid = value; }
        }
        public string AccountTransactionName
        {
            get { return account_transactionname; }
            set { account_transactionname = value; }
        }
        public string TransName
        {
            get { return transName; }
            set { transName = value; }
        }
        public string Transid
        {
            get { return transid; }
            set { transid = value; }
        }
        public string Name {
            get { return name; }
            set { name = value; }
        }
        public string DetailsEng { get; private set; }
        public string DetailsUrdu { get; private set; }

        public string Type {
            get {return type ; }
            set { type = value; } }

        public string Rent { get; private set; }
        public string Labour { get; private set; }
        public string BipComm { get; private set; }
        public string CusComm { get; private set; }
        public string Laga { get; private set; }
        public string Chongi { get; private set; }
        public string Munshiana { get; private set; }
        public string MarketFee { get; private set; }

        public string MSG { get; private set; }

        private void searchCategory(string search)
        {
            if (grid_shop.RowCount >= 1)
            {
                DataGridViewRow selectedRow = grid_shop.Rows[gridRow];
                //string name= comb_select_searchtype.Items[searchType].ToString();
                //int index = comb_select_searchtype.Items.IndexOf(name);
                if (searchType == 1 ||
                    searchType == 2)
                {
                    Id = selectedRow.Cells[0].Value.ToString();
                    Name = selectedRow.Cells[1].Value.ToString();
                    RAmount = int.Parse(selectedRow.Cells[2].Value.ToString()==""?"0": selectedRow.Cells[2].Value.ToString());
                    if (selectedRow.Cells.Count > 3) { 
                        Address = selectedRow.Cells[4].Value.ToString();
                        OldAmount = int.Parse(selectedRow.Cells[3].Value.ToString() == "" ? "0" : selectedRow.Cells[3].Value.ToString());
                    }
                }
                else 
                if (searchType == 3)
                {
                    Id = selectedRow.Cells[0].Value.ToString();
                    Name = selectedRow.Cells[2].Value.ToString();
                    Type = selectedRow.Cells[4].Value.ToString();
                    Rent = selectedRow.Cells[5].Value.ToString();
                    Labour = selectedRow.Cells[6].Value.ToString();
                    BipComm = selectedRow.Cells[7].Value.ToString();
                    CusComm = selectedRow.Cells[8].Value.ToString();
                    Laga = selectedRow.Cells[10].Value.ToString();
                    Chongi = selectedRow.Cells[11].Value.ToString();
                    Munshiana = selectedRow.Cells[12].Value.ToString();
                    MarketFee = selectedRow.Cells[13].Value.ToString();


                }
                else if (searchType == 4)
                {
                    Id = selectedRow.Cells[0].Value.ToString();
                    Name = selectedRow.Cells[1].Value.ToString();
                }
                else if (searchType == 5)
                {
                    Id = selectedRow.Cells[0].Value.ToString();
                    Name = selectedRow.Cells[1].Value.ToString();
                    ExpenseID= selectedRow.Cells[3].Value.ToString();
                    //Transid = selectedRow.Cells[4].Value.ToString();
                    //AccountTransactionid = selectedRow.Cells[5].Value.ToString();
                    //AccountTransactionName = selectedRow.Cells[5].Value.ToString();
                }
                else if (searchType == 6)
                {
                    Id = selectedRow.Cells[0].Value.ToString();
                    Name = selectedRow.Cells[1].Value.ToString();
                    RAmount= int.Parse(selectedRow.Cells[2].Value.ToString());
                }
                else if (searchType == 7)
                {
                    Id = selectedRow.Cells[0].Value.ToString();
                    Name = selectedRow.Cells[1].Value.ToString();
                }

                else if (searchType == 8)
                {
                    Id = selectedRow.Cells[0].Value.ToString();
                    DetailsUrdu = selectedRow.Cells[1].Value.ToString();
                    DetailsEng = selectedRow.Cells[2].Value.ToString();
                    Name = selectedRow.Cells[3].Value.ToString();
                    Type= selectedRow.Cells[4].Value.ToString();
                    AccountTransactionid = selectedRow.Cells[5].Value.ToString();
                    AccountTransactionName = selectedRow.Cells[6].Value.ToString();
                    Transid = selectedRow.Cells[7].Value.ToString();
                    TransName = selectedRow.Cells[8].Value.ToString();

                }
                else if (searchType == 9)
                {
                    Id = selectedRow.Cells[0].Value.ToString();
                    TransName = selectedRow.Cells[1].Value.ToString();
                }


            }
            
        }

        private void Search_Load(object sender, EventArgs e)
        {
            string[] sort= LogUtill.getSorSearch();

            if (sort[1] == "asc")
                rd_asc.Checked = true;
            else if (sort[1] == "desc")
                rd_desc.Checked = true;

            if (sort[0] == "id")
                chk_id.Checked = true;
            else if (sort[0] == "name")
                chk_name.Checked = true;
            else if (sort[0] == "amount")
                chk_amount.Checked = true;

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {


            switch (keyData)
            {
                case Keys.Up:
                    if (txt_searach.Focused)
                        selectUpRow(grid_shop);
                    else
                    if (grid_shop.Focused)
                        selectUpRow(grid_shop);


                    return true;
                case Keys.Down:
                    if (txt_searach.Focused)
                        selectDownRow(grid_shop);
                    else
                    if (grid_shop.Focused)
                        selectDownRow(grid_shop);

                    return true;
                case Keys.Enter:
                    searchCategory(txt_searach.Text);
                    this.Close();
                    return true;
                case Keys.Tab:
                    if (txt_searach.ContainsFocus)
                    {
                        txt_address.Focus();
                        txt_address.SelectAll();
                    }else
                     if (txt_address.ContainsFocus)
                    {
                        txt_searach.Focus();
                        txt_searach.SelectAll();
                    }
                    return true;
                case Keys.Control | Keys.Enter:
                    btn_add_Item_Click(this, new EventArgs());
                    return true;
                case Keys.Escape:
                    isEscapePress = true;
                    this.Close();
                    return true;
            }



            return base.ProcessCmdKey(ref msg, keyData);
        }
        /*private void selectUpRow(MetroGrid grid)
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
            //grid_shop.Rows[currentrow].Selected = true;
        }
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
            //grid_shop.Rows[currentrow].Selected = true;
        }*/
        int gridRow = 0;
        private void selectUpRow(MetroGrid grid)
        {
            MetroGrid dgv = grid;
            int totalRows = dgv.Rows.Count;
            if (totalRows > 0)
            {

                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
                grid.FirstDisplayedScrollingRowIndex = rowIndex - 1;
                
                if (grid.Name == "grid_shop")
                {
                    //gridRow--;
                    if (gridRow < 0)
                    {
                        gridRow = 0;
                    }
                }

            }

        }

        private void selectDownRow(MetroGrid grid)
        {
            MetroGrid dgv = grid;
            int totalRows = dgv.Rows.Count;
            if (totalRows > 0)
            {

                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;


                grid.FirstDisplayedScrollingRowIndex = rowIndex + 1;
                
                if (grid.Name == "grid_shop")
                {
                    //gridRow++;
                    if (gridRow > totalRows)
                    {
                        gridRow = totalRows;
                    }
                }
            }

        }





        private void txt_searach_TextChanged(object sender, EventArgs e)
        {
            searchccpw();
        }

        private void searchccpw()
        {
            if (searchType == 1 || searchType == 0 )
            {
                if (searchType == 0)
                    searchType = 1;
                grid_shop.DataSource = new BLogic().searchRecords(""+clientType, "Client", txt_searach.Text, pageindex, pageSize);
            }
            else
            if (searchType == 2)
            {
                grid_shop.DataSource = new BLogic().searchRecords("", "Customer", txt_searach.Text, pageindex, pageSize);
            }
            else if (searchType == 3)
            {
                List<Object> obj = (List<object>)new BLogic().searchProfile("", "p_product", txt_searach.Text, pageindex, pageSize);
                DataTable dt = (DataTable)obj[1];
                grid_shop.DataSource = dt;
                // grid_shop.Columns[4].Visible = false;
                //grid_shop.Columns[3].Visible = false;
                //grid_shop.Columns[5].Visible = false;
                //grid_shop.Columns[6].Visible = false;
                //grid_shop.Columns[7].Visible = false;
                //grid_shop.Columns[8].Visible = false;
                //grid_shop.Columns[9].Visible = false;
                //grid_shop.Columns[10].Visible = false;
                //grid_shop.Columns[11].Visible = false;
            }
            else if (searchType == 4)
            {
                string search = txt_searach.Text;
                List<Object> obj = (List<object>)new BLogic().searchProfile("", "p_weight", search, pageindex, pageSize);
                DataTable dt = (DataTable)obj[1];
                grid_shop.DataSource = dt;
            }
            else if (searchType == 5)
            {
                string search = txt_searach.Text;
                List<Object> obj = (List<object>)new BLogic().searchProfile("", "ExpenseType", search, pageindex, pageSize);
                DataTable dt = (DataTable)obj[1];
                grid_shop.DataSource = dt;
            }
            else if (searchType == 6)
            {
                string search = txt_searach.Text;
                DataTable dt = new BLogic().searchRecords(date, "Customer", search, pageindex, pageSize); ;
                grid_shop.DataSource = dt;
            }
            else if (searchType == 7)
            {
                string search = txt_searach.Text;
                DataTable dt = new BLogic().getCategory("Read",search);
                grid_shop.DataSource = dt;
            }
            else if (searchType == 8)
            {
                string search = txt_searach.Text;
                DataTable dt = new BLogic().getCategory("CateDetail", search);
                grid_shop.DataSource = dt;
            }
            else if (searchType == 9)
            {
                string search = txt_searach.Text;
                DataTable dt = (DataTable)new BLogic().searchProfile("", "p_account_trans", search, pageindex, pageSize);

                if (dt == null) return;
                grid_shop.DataSource = dt;
            }
        }
        public void LocalizeNameGrid()
        {
            if (grid_shop.Rows.Count == 0)
            {
                return;
            }
            if (searchType ==3)
            {
                this.grid_shop.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0012");
                this.grid_shop.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0205");
                this.grid_shop.Columns[2].HeaderText = Resources.ResourceManager.GetString("a1031");
            }
            else
            {
                this.grid_shop.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0012");
                this.grid_shop.Columns[1].HeaderText = Resources.ResourceManager.GetString("a0205");
            }
        }

        private void btn_add_Item_Click(object sender, EventArgs e)
        {
            string txt = "";
            string adres = "";
            int id = 0;
            if (searchType == 1 || searchType == 2 || searchType == 6)
            {
                txt = txt_searach.Text;
                adres = txt_address.Text;
                if (string.IsNullOrEmpty(txt) || string.IsNullOrWhiteSpace(txt))
                {
                    return;
                }
                if (string.IsNullOrEmpty(adres) || string.IsNullOrWhiteSpace(adres))
                {
                    adres="";
                }
                if (searchType==6 || searchType == 2)// Customer
                    id = bal.insertDataCPW(2, txt,adres);
                else// Client
                    id = bal.insertDataCPW(1, txt, adres);
                Name = txt;
                searchccpw();
            }
            else if (searchType == 3)
            {
                txt = txt_searach.Text;
                id = bal.insertDataCPW(3, txt);
                if (id != 0)
                {
                    AddProduct ap = new AddProduct(1, "" + id, "", txt, "", "", "0", "0", "0", "0","0","0","0","0");
                    ap.ShowDialog();
                    Id = ap.Id;
                    Name = ap.UName;
                    Type = ap.Type;
                    Rent = ap.Rent;
                    Labour = ap.Labour;
                    BipComm = ap.BipComm;
                    CusComm = ap.CusComm;
                    Laga = ap.Laga;
                    Chongi = ap.Chongi;

                    searchccpw();

                    return;

                }
            }
            else if (searchType == 4)
            {
                txt = txt_searach.Text;
                id = bal.insertDataCPW(4, txt);
                if (id != 0)
                {
                    Id = "" + id;
                    MSG = string.Format("{0} {1}", txt, Resources.added_in_database);
                }
                searchccpw();

            }
            else if (searchType == 5)
            {
                txt = txt_searach.Text;
                id = bal.insertDataCPW(5, txt);
                if (id != 0)
                {
                    Id = "" + id;
                    MSG = string.Format("{0} {1}", txt, Resources.added_in_database);
                }
                searchccpw();
            }
            else if(searchType==7)
            {
                txt = txt_searach.Text;
                id = bal.p_CategoryCreateDelete("Add", txt,"",""+grid_shop.Rows.Count+1);
                if (id != 0)
                {
                    Id = "" + id;
                    MSG = string.Format("{0} {1}", txt, Resources.added_in_database);
                }
                searchccpw();
            }

        }
        private void grid_shop_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grid_shop.SelectedRows)
            {
                gridRow = row.Index;
                //...
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string sorttype = "", sortid = "";
            if (rd_asc.Checked)
            {
                sorttype = "asc";
                if (chk_id.Checked && (this.grid_shop.Columns.Count > 0 && this.grid_shop.Columns.Count <= 1 || this.grid_shop.Columns.Count <= 3))
                {
                    sortid = "id";
                    this.grid_shop.Sort(this.grid_shop.Columns[0], ListSortDirection.Ascending);
                }
                else if (chk_name.Checked && (this.grid_shop.Columns.Count > 0 && this.grid_shop.Columns.Count <= 2 || this.grid_shop.Columns.Count <= 3))
                {
                    sortid = "name";
                    this.grid_shop.Sort(this.grid_shop.Columns[1], ListSortDirection.Ascending);
                }
                else if (chk_amount.Checked && this.grid_shop.Columns.Count > 0 && this.grid_shop.Columns.Count <= 3)
                {
                    sortid = "amount";
                    this.grid_shop.Sort(this.grid_shop.Columns[2], ListSortDirection.Ascending);
                }
            }
            else if (rd_desc.Checked)
            {
                sorttype = "desc";

                if (chk_id.Checked && (this.grid_shop.Columns.Count > 0 && this.grid_shop.Columns.Count <= 1 || this.grid_shop.Columns.Count <= 3))
                {
                    sortid = "id";
                    this.grid_shop.Sort(this.grid_shop.Columns[0], ListSortDirection.Descending);
                }
                else if (chk_name.Checked && (this.grid_shop.Columns.Count > 0 && this.grid_shop.Columns.Count <= 2 || this.grid_shop.Columns.Count <= 3))
                {
                    sortid = "name";
                    this.grid_shop.Sort(this.grid_shop.Columns[1], ListSortDirection.Descending);
                }
                else if (chk_amount.Checked && (this.grid_shop.Columns.Count > 0 && this.grid_shop.Columns.Count <= 3))
                {
                    sortid = "amount";
                    this.grid_shop.Sort(this.grid_shop.Columns[2], ListSortDirection.Descending);
                }
            }
            LogUtill.setSorSearch(sortid, sorttype);
        }

        private void grid_shop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (searchType == 8)
            {
                Type = "";
                var selectedRows = grid_shop.SelectedRows
                    .OfType<DataGridViewRow>().Where(row => !row.IsNewRow)
                    .ToArray();
                int count = selectedRows.Count();
                for (int i = 0; i < count; i++)
                {

                    Type += grid_shop.Rows[i].Cells[0].Value.ToString();
                    if (selectedRows.Count() > 1 && i < selectedRows.Count() - 1)
                        Type += ",";
                }
            }
        }
    }
}
