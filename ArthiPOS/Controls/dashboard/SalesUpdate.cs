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
using ArthiPOS.Properties;
using ArthiPOS.shop;
using ArthiPOS.utill;
using ArthiPOS.Utill;
using BAL;
using DataMember;
using MetroFramework.Controls;
using CommonUtilities;
using DataMember.memberlog;

namespace ArthiPOS.Controls.dashboard
{
    public partial class SalesUpdate : Form
    {
        SaleParser saleParser;
        FileInfo[] files;
        AdminLog adminlog;
        BLogic bal;
        string path;
        string _grid;

        public SalesUpdate()
        {
            InitializeComponent();
            adminlog = LogUtill.getAdminInputLog();
            bal = new BLogic();

        }

        internal void initSalesUpdate(SaleParser saleParser,string path,string  _grid)
        {
            this.saleParser = saleParser;
            this.path = path;
            this._grid = _grid;
            if (_grid== "Default")
            {
                files = saleParser.getAllFiles(path, true);
                addFiles(files, grid_all_sales);
            }
            else if(_grid== "Proccessed")
            {
                files = saleParser.getAllFiles(path, true);
                if (files == null)
                    return;
                addFiles(files, grid_processSale);

            }

        }

        private void addFiles(FileInfo[] _files,MetroGrid grid)
        {
            string tstatus = "";

            for (int i = 0; i < _files.Length; i++)
            {
                string file = _files[i].FullName;
                Wrapper wraplandl = saleParser.LoadTodaySale(file);
                int total = 0, expense = 0, bill = 0, quantity = 0;
                foreach (Landlord land in wraplandl.data)
                {
                    quantity += land.land_product.total_Quantity;
                    total += land.total_sale;
                    bill += (int)land.GetGrandTotal;
                    expense += (int)(land.GetTotalService+land.GetCommission+land.GetChongi);
                    //if (land.status == EStatus.Complete)
                    {
                        tstatus = Enum.GetName(typeof(EStatus), land.status);
                    }
                }

                addRowAllSales(grid,wraplandl.date, quantity + "", "" + total, "" + expense, "" + bill, wraplandl.db_status, tstatus);

            }
        }
        public void addRowAllSales(MetroGrid grid, string date,string quantity,
            string totalSale,string expense,string billamount,string status,string salestatus)
        {
            int count = grid.Rows.Count;
            if (count == 0)
            {
                count = 1;
            }
            else
            {
                count = count + 1;
            }
            grid.Rows.Add();
            grid.Rows[count - 1].Cells[1].Value = count;
            grid.Rows[count - 1].Cells[2].Value = date;
            grid.Rows[count - 1].Cells[3].Value = quantity;
            grid.Rows[count - 1].Cells[4].Value = totalSale;
            grid.Rows[count - 1].Cells[5].Value = expense;
            grid.Rows[count - 1].Cells[6].Value = billamount;
            if (status=="Updated")
            {
                grid.Rows[count - 1].Cells[7].Value = Resources.ResourceManager.GetString("a1087");
            }
            else
            {
                grid.Rows[count - 1].Cells[7].Value = Resources.ResourceManager.GetString("a1086");
            }
            int index = tabControl1.SelectedIndex;
            if (index == 0)
                grid.Rows[count - 1].Cells[8].Value = salestatus;

        }

        private void grid_all_sales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                string mdate= grid_all_sales.Rows[index].Cells[2].Value.ToString();
                string tstatus= grid_all_sales.Rows[index].Cells[8].Value.ToString();
                if (tstatus=="InComplete")
                {
                    DialogResult dialogResult = MessageBox.Show("InComplete Sales", "Are You Sure?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                        return;

                    }
                }

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
                if (wrapland==null)
                {
                    return;
                }
                if (wrapland.db_status=="Updated")
                {
                    return;
                }
                wrapland.db_status = "Updated";
                if(saleParser.updateLandLord(filePath, wrapland))//Updating status sale 
                {
                    
                    bool oneTimeCheck = false;
                    #region AddProducts
                    if (!oneTimeCheck)
                    {
                        bal.addTodaySales(wrapland.date);// add Todays sales date
                        oneTimeCheck = true;
                    }

                    wrapland.data = bal.updateLocalToDB(wrapland.date, wrapland.data,true);
                    if (wrapland.data==null)
                    {
                        return;
                    }
                    if (wrapland.data.Count>0)
                    {
                        if (wrapland.data[0].record_id!="")
                        {
                            saleParser.moveSaleinProcess(filePath);
                            this.grid_all_sales.Rows[index].Cells[7].Value = Resources.ResourceManager.GetString("a1087");
                            this.grid_all_sales.Rows.RemoveAt(index);
                            
                        }
                    }

                    #endregion
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tabControl1.SelectedIndex;
            if (index == 0)
            {
                grid_all_sales.Rows.Clear();
                grid_all_sales.Refresh();
                initSalesUpdate(saleParser,adminlog.SalesInProccessedFolder, "Default");
            }
            else
            {
                grid_processSale.Rows.Clear();
                grid_processSale.Refresh();
                initSalesUpdate(saleParser,adminlog.SaleProcessedDir,"Proccessed");
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
                
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
