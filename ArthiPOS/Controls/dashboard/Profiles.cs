using ArthiPOS.Controls.dashboard;
using ArthiPOS.Properties;
using BAL;
using DataMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ArthiPOS.controls.dashboard
{
    public partial class Profiles : UserControl
    {
        public ProfilesBL pbl;
        private DataTable dt;
        string date;
        private BLogic bal;
        public Profiles()
        {
            InitializeComponent();
            pbl = new ProfilesBL();
            bal = new BLogic();
            date = today_date.Text;
            UIUpdate();
        }


        public void UIUpdate()
        {
            lbl_search.Text = Resources.ResourceManager.GetString("a1048");
            lbl_add.Text = Resources.ResourceManager.GetString("a1051");
            lbl_edit.Text = Resources.ResourceManager.GetString("a1052");
            lbl_amount.Text = Resources.ResourceManager.GetString("a1050");
        }
        public void updateGrid()
        {
            cust_detailgrid.Columns[0].HeaderText = Resources.ResourceManager.GetString("a0012");
            cust_detailgrid.Columns[1].HeaderText = Resources.ResourceManager.GetString("a1043");
            cust_detailgrid.Columns[2].HeaderText = Resources.ResourceManager.GetString("a1044");
            cust_detailgrid.Columns[3].HeaderText = Resources.ResourceManager.GetString("a1042");
            cust_detailgrid.Columns[4].HeaderText = Resources.ResourceManager.GetString("a1045");
            //cust_detailgrid.Columns[5].HeaderText = Resources.ResourceManager.GetString("a1046");
        }

        //ID variable used in Updating and Deleting Record   
        int ID = 0;

        String tableName = "tbl_customer";

        enum DetailType
        {
            Customer,
            Client
        };

        DetailType detail_typeenum = DetailType.Customer;




        private void btn_update_Click_1(object sender, EventArgs e)
        {
            if(detail_type.SelectedIndex==2)
                tableName = "ClBipari";
            

            if (ID != 0 && (u_txt_name.Text != "" || txt_name.Text != "") || txt_phone.Text != "" || txt_address.Text != "")
            {
                if (pbl.update_CC(tableName, ID, u_txt_name.Text, txt_name.Text, txt_phone.Text, txt_address.Text, txt_old_amount.Text == "" ? "0" : txt_old_amount.Text, chk_admin.Text))
                {
                    MessageBox.Show("Record Updated Successfully");
                    ClearData();
                    DisplayData();
                    
                    txt_searchCustomer.Focus();
                    if (txt_old_amount.Enabled)
                    {
                        txt_old_amount.Enabled = false;
                    }
                }

            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {


            if (pbl.delete_CC(tableName, txt_id.Text))
            {
                msg.Text = "Record Deleted Successfully!";
                DisplayData();
                ClearData();
                txt_searchCustomer.Focus();
            }

        }

        private void DisplayData()
        {

            msg.Text = "";

            // cust_detailgrid.DataSource = dt;
            if (cust_detailgrid.Columns["col_update"] != null)
                cust_detailgrid.Columns.Remove("col_update");
            if (cust_detailgrid.Columns["col_delete"] != null)
                cust_detailgrid.Columns.Remove("col_delete");


            string tbl = "";

            if (tab4.SelectedIndex == 0)
            {
                if(detail_type.SelectedIndex==0)
                    tbl = "SCustomer";
                else if (detail_type.SelectedIndex == 1)
                {
                    if (tableName == "tbl_client")
                    {
                        pageindex = 1;
                        tbl = "SClient";
                    }
                }
                else if (detail_type.SelectedIndex == 2)
                    tbl = "ClBipari";
                
                //addGridButton("col_delete", "Delete", "Delete", cust_detailgrid.Rows.Count + 1);

            }
            else if (tab4.SelectedIndex == 1)
            {
                tbl = "p_product";
                //addGridButton("col_update", "Update", "Update", cust_detailgrid.Rows.Count + 1);
                //addGridButton("col_delete", "Delete", "Delete", cust_detailgrid.Rows.Count + 1);

            }
            else if (tab4.SelectedIndex == 2)
            {
                tbl = "p_weight";
                //addGridButton("col_update", "Update", "Update", cust_detailgrid.Rows.Count + 1);
                //addGridButton("col_delete", "Delete", "Delete", cust_detailgrid.Rows.Count + 1);

            }
            else if (tab4.SelectedIndex == 3)
            {
                tbl = "p_category";
                //addGridButton("col_update", "Update", "Update", cust_detailgrid.Rows.Count + 1);
                //addGridButton("col_delete", "Delete", "Delete", cust_detailgrid.Rows.Count + 1);

            }
            else if (tab4.SelectedIndex == 4)
            {
                tbl = "ExpenseType";
                //addGridButton("col_update", "Update", "Update", cust_detailgrid.Rows.Count + 1);
                //addGridButton("col_delete", "Delete", "Delete", cust_detailgrid.Rows.Count + 1);

            }

            //dt = new BLogic().searchRecords("", tbl, "",pageindex,pageSize);
            //cust_detailgrid.DataSource = dt;

            loadGridData(pageindex, "", tbl);




        }
        //Clear Data  
        private void ClearData()
        {
            txt_id.Text = "";
            txt_name.Text = "";
            u_txt_name.Text = "";
            txt_phone.Text = "";
            txt_address.Text = "";
            ID = 0;
        }


        //Get Selected Row Values From DataGridView Into TextBox
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (cust_detailgrid.Columns["col_update"] != null && e.ColumnIndex == cust_detailgrid.Columns["col_update"].Index)
            {
                if (tab4.SelectedIndex == 1)
                {
                    productDisplay(index);
                }

                else if (tab4.SelectedIndex == 2)
                {
                    string code = cust_detailgrid.Rows[index].Cells[2].Value.ToString();
                    string uname = cust_detailgrid.Rows[index].Cells[3].Value.ToString();
                    string ename = cust_detailgrid.Rows[index].Cells[4].Value.ToString();
                    txt_uname.Text = uname;
                    txt_ename.Text = ename;
                    lbl_id.Text = code;
                }
                else if (tab4.SelectedIndex == 4)
                {
                    string code = cust_detailgrid.Rows[index].Cells[0].Value.ToString();
                    string uname = cust_detailgrid.Rows[index].Cells[1].Value.ToString();
                    string catid = cust_detailgrid.Rows[index].Cells[3].Value.ToString();
                    string acccatid = cust_detailgrid.Rows[index].Cells[4].Value.ToString();
                    string acccatname = cust_detailgrid.Rows[index].Cells[5].Value.ToString();
                    txt_typeid.Text = code;
                    txt_typename.Text = uname;
                    txt_catid.Text = catid;
                    txt_account_cat_id.Text = acccatname;
                    lbl_acc_caid.Text = acccatid;

                }
            }
            else if (cust_detailgrid.Columns["col_delete"] != null && e.ColumnIndex == cust_detailgrid.Columns["col_delete"].Index)
            {
                //Do something with your button.
                if (tab4.SelectedIndex == 0)
                {
                    string tbl = "tbl_customer";
                    if (tableName == "tbl_client")
                    {
                        pageindex = 1;
                        tbl = "tbl_client";
                    }

                    string code = cust_detailgrid.Rows[index].Cells[1].Value.ToString();
                    if (pbl.delete_CC(tbl, code))
                    {
                        cust_detailgrid.Rows.RemoveAt(index);
                    }
                }
                else if (tab4.SelectedIndex == 1)
                {
                    string code = cust_detailgrid.Rows[index].Cells[2].Value.ToString();
                    if (pbl.p_product_Delete(code))
                    {
                        cust_detailgrid.Rows.RemoveAt(index);
                    }
                }
                else if (tab4.SelectedIndex == 2)
                {
                    string code = cust_detailgrid.Rows[index].Cells[2].Value.ToString();
                    if (pbl.p_weight_Delete(code))
                    {
                        cust_detailgrid.Rows.RemoveAt(index);
                    }
                }

                DisplayData();

            }
            else
            {
                showDatainFields(index);
            }
        }
        string clkey = "";
        public void showDatainFields(int index)
        {
            if (tab4.SelectedIndex == 0)
            {

                ID = Convert.ToInt32(cust_detailgrid.Rows[index].Cells[0].Value.ToString());
                txt_id.Text = "" + ID;
                DataGridViewRow selectedRow = cust_detailgrid.Rows[index];
                txt_name.Text = selectedRow.Cells[1].Value.ToString();
                u_txt_name.Text = selectedRow.Cells[2].Value.ToString();
                txt_phone.Text = selectedRow.Cells[3].Value.ToString();
                txt_address.Text = selectedRow.Cells[4].Value.ToString();
                if (detail_type.SelectedIndex != 2)
                {
                    txt_old_amount.Text = selectedRow.Cells[5].Value.ToString();
                    lbl_old_amount.Text = selectedRow.Cells[5].Value.ToString();
                }
                if (detail_type.SelectedIndex == 1)
                {
                    string admin = selectedRow.Cells[7].Value.ToString();
                    if (admin == "") { chk_admin.Text = "Client"; chk_admin.Checked = false; }
                    else
                    {
                        chk_admin.Text = admin;
                        chk_admin.Checked = true;
                    }
                }
                //clkey = selectedRow.Cells[7].Value.ToString();
            }
            else if (tab4.SelectedIndex == 1)
            {
                ID = Convert.ToInt32(cust_detailgrid.Rows[index].Cells[0].Value.ToString());
                productDisplay(index);


            }
            else if (tab4.SelectedIndex == 2)
            {
                ID = Convert.ToInt32(cust_detailgrid.Rows[index].Cells[0].Value.ToString());
                txt_id.Text = "" + ID;
                DataGridViewRow selectedRow = cust_detailgrid.Rows[index];
                txt_ename.Text = selectedRow.Cells[1].Value.ToString();
                txt_uname.Text = selectedRow.Cells[2].Value.ToString();



            }
            else if (tab4.SelectedIndex == 2)
            {
                ID = Convert.ToInt32(cust_detailgrid.Rows[index].Cells[0].Value.ToString());
                txt_id.Text = "" + ID;
                DataGridViewRow selectedRow = cust_detailgrid.Rows[index];
                txt_ename.Text = selectedRow.Cells[1].Value.ToString();
                txt_uname.Text = selectedRow.Cells[2].Value.ToString();



            }
            else if (tab4.SelectedIndex == 4)
            {
                ID = Convert.ToInt32(cust_detailgrid.Rows[index].Cells[9].Value.ToString());
                txt_typeid.Text = "" + ID;
                DataGridViewRow selectedRow = cust_detailgrid.Rows[index];
                txt_typename.Text = selectedRow.Cells[0].Value.ToString();
                txt_catid.Text = selectedRow.Cells[10].Value.ToString();
                string acccatid = selectedRow.Cells[8].Value.ToString();
                lbl_acc_caid.Text = acccatid;
                txt_account_cat_id.Text = selectedRow.Cells[4].Value.ToString();



            }


        }

        private void productDisplay(int index)
        {
            string code = cust_detailgrid.Rows[index].Cells[0].Value.ToString();
            string vegname = cust_detailgrid.Rows[index].Cells[1].Value.ToString();
            string vegname_ur = cust_detailgrid.Rows[index].Cells[2].Value.ToString();
            string pack = cust_detailgrid.Rows[index].Cells[4].Value.ToString();
            string loc = cust_detailgrid.Rows[index].Cells[3].Value.ToString();
            string freight = cust_detailgrid.Rows[index].Cells[5].Value.ToString();
            string labour = cust_detailgrid.Rows[index].Cells[6].Value.ToString();
            string bip_comm = cust_detailgrid.Rows[index].Cells[7].Value.ToString();
            string cust_comm = cust_detailgrid.Rows[index].Cells[8].Value.ToString();
            string laga = cust_detailgrid.Rows[index].Cells[10].Value.ToString();
            string chongi = cust_detailgrid.Rows[index].Cells[11].Value.ToString();
            string munshiana = cust_detailgrid.Rows[index].Cells[12].Value.ToString();
            string marketFee = cust_detailgrid.Rows[index].Cells[13].Value.ToString();
            string shopcomm = cust_detailgrid.Rows[index].Cells[14].Value.ToString();
            string shoplabour = cust_detailgrid.Rows[index].Cells[15].Value.ToString();


            AddProduct ap = new AddProduct(1, code, vegname, vegname_ur, pack, loc, freight, labour, bip_comm, cust_comm, laga, chongi, munshiana, marketFee, shopcomm, shoplabour);
            ap.ShowDialog();

        }


        private void detail_type_SelectedIndexChanged(object sender, EventArgs e)
        {


            //DisplayData();
        }

        private void txt_searchCustomer_TextChanged(object sender, EventArgs e)
        {
            /* txt_searchCustomer.AutoCompleteMode = AutoCompleteMode.Suggest;
             txt_searchCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;
             AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
             getData(DataCollection);
             txt_searchCustomer.AutoCompleteCustomSource = DataCollection;*/

            //searchCC();
            /*DataView DV = new DataView(dt);
            int value;
            if (int.TryParse(txt_searchCustomer.Text, out value))
            {
                DV.RowFilter = string.Format("ID>={0}", txt_searchCustomer.Text);
            }
            else
            {
                DV.RowFilter = string.Format("EngName LIKE '%{0}%'", txt_searchCustomer.Text);
            }
            cust_detailgrid.DataSource = DV;
            int index = 0;// get the Row Index
            if (cust_detailgrid.RowCount > 0)
            {
                ID = Convert.ToInt32(cust_detailgrid.Rows[0].Cells[0].Value.ToString());
                txt_id.Text = "" + ID;
                DataGridViewRow selectedRow = cust_detailgrid.Rows[index];
                txt_name.Text = selectedRow.Cells[1].Value.ToString();
                u_txt_name.Text = selectedRow.Cells[2].Value.ToString();
                txt_phone.Text = selectedRow.Cells[3].Value.ToString();
                txt_address.Text = selectedRow.Cells[4].Value.ToString();
                txt_old_amount.Text = selectedRow.Cells[5].Value.ToString();
            }

            if (txt_searchCustomer.Text.Count()==0)
            {
                cust_detailgrid.DataSource = dt;
            }*/

            BLogic bal = new BLogic();
            string search = "";
            if (txt_searchCustomer.ContainsFocus)
            {
                search = txt_searchCustomer.Text;
            }
            if (cc_txt_name.ContainsFocus)
            {
                search = cc_txt_name.Text;
            }
            string tbl = "";

            if (tab4.SelectedIndex == 0)
            {
                tbl = "SCustomer";
                if (detail_type.SelectedIndex == 1)
                {
                    pageindex = 1;
                    tbl = "SClient";
                }else if ( detail_type.SelectedIndex == 2)
                {
                    pageindex = 1;
                    tbl = "ClBipari";
                }
            }
            else if (tab4.SelectedIndex == 1)
            {
                tbl = "p_product";
            }
            else if (tab4.SelectedIndex == 2)
            {
                tbl = "p_weight";
            }
            else if (tab4.SelectedIndex == 4)
            {
                tbl = "ExpenseType";
            }
            // DataTable dt= bal.searchRecords("", tbl, search,pageindex,pageSize);
            // cust_detailgrid.DataSource = dt;

            loadGridData(pageindex, search, tbl);


        }


        public void searchCC()
        {
            /*            string sql = "";
                                   if (detail_typeenum == DetailType.Client)
                                   {
                                       sql = "Select client_id as ID,eng_client_name as EngName, client_name as Name,client_phone as Phone,client_address as Address, client_advance_amount as RemainingAmount from tbl_client where client_name like '%'" + txt_searchCustomer.Text + "'%' OR (client_id LIKE '" + txt_searchCustomer.Text + "%')";
                                   }
                                   else
                                   {
                                       sql = "Select cust_id as ID,eng_cust_name as EngName ,cust_name as Name,cust_phone as Phone,cust_address as Address,remaining_amount as  RemainingAmount  from tbl_customer where cust_name like '%'" + txt_searchCustomer.Text + "'%' OR (cust_id LIKE '" + txt_searchCustomer.Text + "%')";
                                   }

                                   using (SqlConnection connection = new SqlConnection(Records.ConnectionSTring))
                                   {
                                       connection.Open();
                                       adapt = new SqlDataAdapter(sql, connection);
                                       dt = new DataTable();
                                       adapt.Fill(dt);
                                       cust_detailgrid.DataSource = dt;
                                       Records.getConnection().Close();

                                       int index = 0;// get the Row Index
                                       if (cust_detailgrid.RowCount>0)
                                       {
                                           ID = Convert.ToInt32(cust_detailgrid.Rows[0].Cells[0].Value.ToString());
                                           txt_id.Text = "" + ID;
                                           DataGridViewRow selectedRow = cust_detailgrid.Rows[index];
                                           txt_name.Text = selectedRow.Cells[1].Value.ToString();
                                           u_txt_name.Text = selectedRow.Cells[2].Value.ToString();
                                           txt_phone.Text = selectedRow.Cells[3].Value.ToString();
                                           txt_address.Text = selectedRow.Cells[4].Value.ToString();
                                           txt_old_amount.Text = selectedRow.Cells[5].Value.ToString();
                                       }

                                   }
           */


            //WHERE eng_cust_name LIKE '%2%' OR cust_id LIKE '2%' order by eng_cust_name asc;
            /*msg.Text = "";
            Records.getConnection().Open();
            using (SqlConnection connection = new SqlConnection(Records.ConnectionSTring))
            {
                connection.Open();
                dt = new DataTable();
                String sql = string.Format("Select * FROM tbl_customer WHERE eng_cust_name LIKE  '%'{0}'%' OR (cust_id LIKE '{1}%') order by eng_cust_name asc", txt_searchCustomer.Text, txt_searchCustomer.Text);
                if (detail_typeenum == DetailType.Client)
                {
                    string.Format("Select * FROM tbl_client WHERE eng_client_name LIKE  '%'{0}'%' OR (client_id LIKE '{1}%') order by eng_client_name asc", txt_searchCustomer.Text, txt_searchCustomer.Text);
                }
                adapt = new SqlDataAdapter(sql, connection);
                adapt.Fill(dt);
                cust_detailgrid.DataSource = dt;
                Records.getConnection().Close();

                int index = 0;// get the Row Index
                if (cust_detailgrid.RowCount > 0)
                {
                    ID = Convert.ToInt32(cust_detailgrid.Rows[0].Cells[0].Value.ToString());
                    txt_id.Text = "" + ID;
                    DataGridViewRow selectedRow = cust_detailgrid.Rows[index];
                    txt_name.Text = selectedRow.Cells[1].Value.ToString();
                    u_txt_name.Text = selectedRow.Cells[2].Value.ToString();
                    txt_phone.Text = selectedRow.Cells[3].Value.ToString();
                    txt_address.Text = selectedRow.Cells[4].Value.ToString();
                    txt_old_amount.Text = selectedRow.Cells[5].Value.ToString();
                }
            }*/



            //textchanged event of texbox when user enter a word in the textbox then through this dataview object string format it will filter and attached the filter result in to the datagridview







        }


        private void button1_Click(object sender, EventArgs e)
        {
            string txt_search = txt_searchCustomer.Text.ToString();

        }

        private void UCUpdateCCInfo_Load(object sender, EventArgs e)
        {
            DisplayData();

            detail_type.SelectedIndex = 0;
            txt_id.Enabled = false;
            txt_searchCustomer.Focus();
            btn_addinv.Enabled = false;

            /* txt_searchCustomer.AutoCompleteMode = AutoCompleteMode.Suggest;
             txt_searchCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;
             AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
             getData(DataCollection);
             txt_searchCustomer.AutoCompleteCustomSource = DataCollection;*/
        }

        private void cust_detailgrid_KeyUp(object sender, KeyEventArgs e)
        {


        }
        int indexDel = -1;
        private void DeleteSelectedCell()
        {

            int index = cust_detailgrid.SelectedRows[2].Index;// get the Row Index
            indexDel = index;
            cust_detailgrid.Rows.RemoveAt(index);
        }
        private void selectCellValue()
        {
            int index = cust_detailgrid.SelectedCells[0].OwningRow.Index;// get the Row Index
            showDatainFields(index);
        }

        int currentrow = 0, gridRow = 0;
        /* private void selectUpRow(DataGridView grid)
         {
             DataGridView dgv = grid;
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

         private void selectDownRow(DataGridView grid)
         {
             DataGridView dgv = grid;
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
         */
        private void selectUpRow(DataGridView grid)
        {
            DataGridView dgv = grid;
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
                currentrow--;
                if (currentrow < 0)
                {
                    currentrow = 0;
                }


            }

        }
        private void selectDownRow(DataGridView grid)
        {
            DataGridView dgv = grid;
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
                currentrow++;
                if (currentrow > totalRows)
                {
                    currentrow = totalRows;
                }


            }

        }

        private void cust_detailgrid_KeyDown(object sender, KeyEventArgs e)
        {




            if (cust_detailgrid.Visible == true)
            {
                if (e.KeyCode.Equals(Keys.Up))
                {
                    selectUpRow(cust_detailgrid);
                }
                if (e.KeyCode.Equals(Keys.Down))
                {
                    selectDownRow(cust_detailgrid);
                }
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    selectCellValue();

                }
                if (e.KeyCode.Equals(Keys.Control) | e.KeyCode.Equals(Keys.Delete))
                {
                    DeleteSelectedCell();

                }
                e.Handled = true;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!this.ProcessKey(msg, keyData))
            {


                return base.ProcessCmdKey(ref msg, keyData);
            }
            else
            {

            }
            return false;
        }

        private bool ProcessKey(Message msg, Keys keyData)
        {
            bool retval = false;

            if ((keyData & Keys.Escape) == Keys.Escape)
            {
                Control control = Control.FromChildHandle(msg.HWnd);
                retval = control.Name == this.Name;

                if (!retval)
                {
                    Control parentControl = control.Parent;
                    while (parentControl != null)
                    {
                        if (parentControl.Name == this.Name)
                        {
                            retval = true;
                            break;
                        }
                        parentControl = parentControl.Parent;
                    }
                }
            }
            /* else if (cc_txt_name.Focused && (keyData & Keys.Enter) == Keys.Enter && (keyData & Keys.M) != Keys.M)
             {
                 cc_txt_name_Click(this, new EventArgs());

             }*/

            else if (txt_searchCustomer.Focused && (keyData & Keys.Enter) == Keys.Enter)
            {
                txt_name.Focus();
                selectCellValue();
            }
            else if (txt_searchCustomer.Focused && ((keyData & Keys.Down) == Keys.Down))
            {
                cust_detailgrid.ClearSelection();
                cust_detailgrid.Rows[0].Selected = true;
                cust_detailgrid.FirstDisplayedScrollingRowIndex = 0;
                cust_detailgrid.Focus();
            }
            else if ((txt_searchCustomer.Focused || cust_detailgrid.Focused) && keyData == Keys.Left)
            {
                string searchx = txt_searchCustomer.Text;
                string tbl = "SCustomer";
                if (detail_type.SelectedIndex == 0)
                {
                    tbl = "SCustomer";
                }
                else if(detail_type.SelectedIndex==1)
                {
                    tbl = "SClient";
                }
                else if(detail_type.SelectedIndex==2)
                {
                    tbl = "ClBipari";
                }
                if (pageindex > 1)
                {
                    --pageindex;

                }
                loadGridData(pageindex, searchx, tbl);

            }
            else if ((txt_searchCustomer.Focused || cust_detailgrid.Focused) && keyData == Keys.Right)
            {
                string searchx = txt_searchCustomer.Text;
                string tbl = "SCustomer";
                if (tableName == "tbl_client")
                {
                    tbl = "SClient";
                }
                if (pageindex < totalPage)
                {
                    ++pageindex;
                }
                loadGridData(pageindex, searchx, tbl);

            }
            else if ((cc_txt_name.Focused || txt_quick_amount.Focused) && (keyData == (Keys.Control | Keys.Enter)))
            {
                cc_txt_name_Click(this, new EventArgs());
                txt_name.Focus();
            }

            else if (txt_addtype.Focused && keyData == (Keys.Enter))
            {
                using (Search search = new Search(5, txt_addtype.Text))
                {
                    DialogResult res = search.ShowDialog();
                    txt_addtype.Text = search.Name;

                    search.Close();
                }
            }
            else if (keyData == (Keys.Control | Keys.U))
            {
                btn_update_Click_1(this, new EventArgs());
            }
            else if (keyData == (Keys.Control | Keys.F5))
            {
                txt_searchCustomer.Focus();
            }
            else
            {
                if (keyData == Keys.Enter)
                    changeFocus();
            }

            return retval;
        }

        private void changeFocus()
        {
            if (txt_name.Focused)
            {
                u_txt_name.Focus();
            }
            else if (u_txt_name.Focused)
            {
                txt_phone.Focus();
            }
            else if (txt_phone.Focused)
            {
                txt_address.Focus();
            }
            else if (txt_address.Focused)
            {
                //txt_phone.Focus();
            }
        }

        private void cc_txt_name_Click(object sender, EventArgs e)
        {
            if (detail_type.SelectedIndex == 2)
                tableName = "tbl_client_bipari";
            else if (detail_type.SelectedIndex == 1)
                tableName = "tbl_client";
            else
                tableName = "tbl_customer";
            if (tab4.SelectedIndex == 0)
            {
                if (cc_txt_name.Text == "")
                {
                    return;
                }
                if (txt_quick_amount.Text == "")
                {
                    txt_quick_amount.Text = "0";
                }


                bool chk = pbl.insert_CC_OldRecord(tableName, cc_txt_name.Text, "", "", "", int.Parse(txt_quick_amount.Text), Admin.Date);


            }
            else if (tab4.SelectedIndex == 1)
            {
                if (cc_txt_name.Text == "")
                {
                    return;
                }
                if (txt_quick_amount.Text == "")
                {
                    txt_quick_amount.Text = "0";
                }



                bool chk = pbl.insert_CC_OldRecord(tableName, cc_txt_name.Text, "", "", "", int.Parse(txt_quick_amount.Text), Admin.Date);


            }
            else if (tab4.SelectedIndex == 2)
            {
                if (cc_txt_name.Text == "")
                {
                    return;
                }
                if (txt_quick_amount.Text == "")
                {
                    txt_quick_amount.Text = "0";
                }



                bool chk = pbl.insert_CC_OldRecord(tableName, cc_txt_name.Text, "", "", "", int.Parse(txt_quick_amount.Text), Admin.Date);

            }

            DisplayData();

            cc_txt_name.Clear();
            cc_txt_name.Focus();
        }


        private void cc_txt_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cc_txt_name_Click(this, new EventArgs());
            }
        }

        private void Btn_refresh_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btn_update_amount_Click(object sender, EventArgs e)
        {
            if (ID != 0 && (u_txt_name.Text != "" || txt_name.Text != "") || txt_phone.Text != "" || txt_address.Text != "")
            {
                int oldamount = 0, amount = 0;
                if (txt_receive_amount.Text == "")
                {
                    MessageBox.Show("Unable to Update");
                    return;
                }
                date = today_date.Text;
                string key = BillKey.getBillID(BillKey.EnumUser.ClientInvest, date, "" + ID, 0);

                oldamount = int.Parse(txt_old_amount.Text);
                amount = int.Parse(txt_receive_amount.Text);

                if (pbl.AddClAmount(tableName, "client", ID, u_txt_name.Text, txt_name.Text, txt_phone.Text, txt_address.Text, amount, today_date.Text, key, "", nameof(BillKey.EnumUser.ClientInvest)))
                {
                    bal.addTodaySales(date);
                    bal.update_today_sales(date);
                    MessageBox.Show("Record Updated Successfully");
                    DisplayData();
                    ClearData();
                }

            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        #region Paging
        int pageindex = 1;
        int pageSize = 20;

        public void loadGridData(int index, string search, string action)
        {
            try
            {
                pageindex = index;
                cust_detailgrid.DataSource = null;
                List<Object> obj = (List<object>)new BLogic().searchProfile("", action, search, index, pageSize);
                DataTable dt = null;
                if (detail_type.SelectedIndex==2)
                {
                    dt = (DataTable)obj[1];

                }
                else
                {
                    dt = (DataTable)obj[1];
                }
                //if (tabControl1.SelectedIndex == 0)
                {


                }


                cust_detailgrid.DataSource = dt;


                if (tab4.SelectedIndex == 0)
                    updateGrid();

                this.PopulatePager((int)obj[0], index);

            }
            catch (Exception ex)
            {

            }
        }
        int totalPage = 0;
        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan = 3;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(pageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            totalPage = pageCount;
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 3;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<<", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = ">>>", Value = pageCount.ToString() });
            }

            //Clear existing Pager Buttons.
            //pnlPager.Controls.Clear();

            //Loop and add Buttons for Pager.
            int count = 0;
            foreach (Page page in pages)
            {
                Button btnPage = new Button();
                btnPage.Location = new System.Drawing.Point(38 * count, 5);
                btnPage.Size = new System.Drawing.Size(35, 20);
                btnPage.Name = page.Value;
                btnPage.Text = page.Text;
                btnPage.Enabled = !page.Selected;
                btnPage.Click += new System.EventHandler(this.Page_Click);
                //pnlPager.Controls.Add(btnPage);
                count++;
            }



        }

        private void Page_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);
            string tbl = "SCustomer";
            if (tableName == "tbl_client")
            {
                pageindex = 1;
                tbl = "SClient";
            }
            loadGridData(int.Parse(btnPager.Name), "", tbl);
        }

        public class Page
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }
        }


        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab4.SelectedIndex == 0)
            {
                UCUpdateCCInfo_Load(this, new EventArgs());
                panel5.Enabled = true;
            }
            else if (tab4.SelectedIndex == 1)
            {
                panel5.Enabled = false;
                DisplayData();
            }
            else if (tab4.SelectedIndex == 2)
            {
                panel5.Enabled = false;
                DisplayData();
            }
            else if (tab4.SelectedIndex == 3)
            {
                panel5.Enabled = false;
                DisplayData();
            }
            else if (tab4.SelectedIndex == 4)
            {
                panel5.Enabled = false;
                DisplayData();
            }
        }

        private void btn_add_veg_Click(object sender, EventArgs e)
        {
            AddProduct ap = new AddProduct();
            ap.ShowDialog();
            DisplayData();
        }


        private void addGridButton(string id, string header, string text, int columnPosition)
        {
            /*DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = id;
            btn.Text = text;
            if (cust_detailgrid.Columns[id] == null)
            {
                cust_detailgrid.Columns.Insert(columnPosition, btn);
            }*/
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            {
                button.Name = id;
                button.HeaderText = header;
                button.Text = text;
                button.UseColumnTextForButtonValue = true; //dont forget this line
                this.cust_detailgrid.Columns.Add(button);
            }
        }

        private void btn_add_weight_Click(object sender, EventArgs e)
        {
            string uname = txt_uname.Text;
            string ename = txt_ename.Text;
            if (pbl.p_weight_Insert("", uname, ename))
            {
                MessageBox.Show("Weight Inserted Successfully.");
                DisplayData();
            }
        }

        private void btn_update_weight_Click(object sender, EventArgs e)
        {
            string id = lbl_id.Text;
            string uname = txt_uname.Text;
            string ename = txt_ename.Text;
            if (pbl.p_weight_Update(id, uname, ename))
            {
                MessageBox.Show("Weight Inserted Successfully.");
                DisplayData();
            }
        }

        private void btn_addinv_Click(object sender, EventArgs e)
        {

            string id = txt_id.Text;
            string name = u_txt_name.Text;
            string amount = txt_old_amount.Text;
            if (id == "")
            {
                return;
            }
            AddInvestment add = new AddInvestment(today_date.Text, id, name, amount == "" ? 0 : int.Parse(amount), txt_phone.Text, txt_address.Text, txt_ename.Text);
            add.ShowDialog();
            DisplayData();
            txt_searchCustomer.Focus();
        }

        private void rd_check_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageindex = 1;
            if (detail_type.SelectedIndex == 0)
            {
                detail_typeenum = DetailType.Customer;
                tableName = "tbl_customer";
                pan_add_amount.Enabled = false;
                btn_addinv.Enabled = false;
                chk_admin.Enabled = false;
                loadGridData(pageindex, "", "SCustomer");


            }
            else
            if (detail_type.SelectedIndex == 1)
            {
                detail_typeenum = DetailType.Client;
                tableName = "tbl_client";
                pan_add_amount.Enabled = true;
                chk_admin.Enabled = true;
                btn_addinv.Enabled = true;
                loadGridData(pageindex, "", "SClient");
            }
            else
            if (detail_type.SelectedIndex == 2)
            {
                detail_typeenum = DetailType.Client;
                tableName = "tbl_client_bipari";
                pan_add_amount.Enabled = true;
                chk_admin.Enabled = true;
                btn_addinv.Enabled = true;
                loadGridData(pageindex, "", "ClBipari");
            }

            txt_searchCustomer.Focus();
            txt_id.Text = "";
            txt_name.Text = "";
            txt_phone.Text = "";
            txt_address.Text = "";

        }

        //private void btn_add_Click_1(object sender, EventArgs e)
        //{
        //    if (tab4.SelectedIndex == 0)
        //    {
        //        string tbl = "tbl_customer";
        //        if (tableName == "tbl_client")
        //        {
        //            AddCC c1 = new AddCC(BillKey.EnumUser.Client);
        //            c1.ShowDialog();
        //        }
        //        else
        //        {
        //            AddCC c = new AddCC(BillKey.EnumUser.Customer);
        //            c.ShowDialog();
        //        }
        //    }
        //}

        private void btn_cat_Click(object sender, EventArgs e)
        {
            Category c = new Category();
            c.ShowDialog();

        }

        private void btn_oldrec_Click(object sender, EventArgs e)
        {
            ProfileCCAdd pr = new ProfileCCAdd(detail_type.SelectedIndex, tableName);
            pr.ShowDialog();
            DisplayData();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (txt_old_amount.Enabled)
            {
                txt_old_amount.Enabled = false;
            }
            else
            {
                txt_old_amount.Enabled = true;
            }
        }

        private void chk_admin_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_admin.Checked)
                chk_admin.Text = "Admin";
            else
                chk_admin.Text = "Client";
        }


        private void btn_uptype_Click(object sender, EventArgs e)
        {
            string id = txt_typeid.Text;
            string name = txt_typename.Text;
            string catid = txt_catid.Text;
            string accid = lbl_acc_caid.Text;
            if (name == "")
                return;

            bal.p_weigt_CRUD("ExpTypeUPDATE", id, name, catid, accid);
            DisplayData();

        }

        private void btn_typdel_Click(object sender, EventArgs e)
        {
            string id = txt_typeid.Text;
            string name = txt_typename.Text;

            if (name == "")
                return;

            bal.p_weigt_CRUD("ExpTypeDel", id, name, "", "");
            DisplayData();
        }

        private void txt_catid_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_catid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchDialog(8, txt_catid.Text);
            }
        }

        private Search search = null;
        private string temp = "";
        public void searchDialog(int action, string searchTxt)
        {




            using (search = new Search(action, searchTxt))
            {
                DialogResult res = search.ShowDialog();
                if (action == 8)
                {
                    txt_catid.Text = search.Type;
                    search.Close();

                }
                else if (action == 9)
                {
                    lbl_acc_caid.Text = search.Id;
                    txt_account_cat_id.Text = search.TransName;
                    search.Close();

                }
                search.Close();
                return;
            }
        }

        private void txt_account_cat_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchDialog(9, txt_account_cat_id.Text);
            }
        }

        private void btn_trans_account_Click(object sender, EventArgs e)
        {
            TransactionEntryUpdate t = new TransactionEntryUpdate();
            t.ShowDialog();
        }

        private void btn_acc_trans_exp_Click(object sender, EventArgs e)
        {
            AccountExpenseTransactionForm t = new AccountExpenseTransactionForm();
            t.ShowDialog();
        }

        private void btn_del_weight_Click(object sender, EventArgs e)
        {
            string id = lbl_id.Text;
            if (pbl.p_weight_Delete(id))
            {
                MessageBox.Show("Weight Inserted Successfully.");
                DisplayData();
            }
        }
    }
}
