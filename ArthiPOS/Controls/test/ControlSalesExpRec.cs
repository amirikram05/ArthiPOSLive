using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArthiPOS.Utill;
using System.IO;
using DataMember;
using ArthiPOS.Properties;
using BAL;
using CommonUtilities;
using DataMember.memberlog;

namespace ArthiPOS.Controls.test
{
    public partial class ControlSalesExpRec : UserControl
    {
        private string date;
        private SaleParser saleParser;
        AdminLog adminlog;
        public int Count = 0;

        public ControlSalesExpRec(string date)
        {
            InitializeComponent();
            this.date = date;
            adminlog = LogUtill.getAdminInputLog();
            saleParser = new SaleParser("", Admin.SaveLog);

        }

        internal void initSalesUpdate(SaleParser saleParser, string path, string _grid)
        {
            FileInfo[] files = saleParser.getAllFiles(path,false);
             addFiles(files, dg_SER);
        }

        private void addFiles(FileInfo[] _files, DataGridView grid)
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
                    expense += (int)(land.GetTotalService + land.GetCommission + land.GetChongi);
                    //if (land.status == EStatus.Complete)
                    {
                        tstatus = Enum.GetName(typeof(EStatus), land.status);
                    }
                }

                addRowAllSales(dg_SER, wraplandl.date, "Vendour Sales","", quantity + "", "" + total, "" + expense, "" + bill,"E", "Sales","");

            }
        }
        public void addRowAllSales(DataGridView grid, string date,string description,string amount, string quantity,
            string totalSale, string expense, string billamount,string entry, string type,string key)
        {
            /*int count = grid.Rows.Count;
            if (count == 0)
            {
                count = 1;
            }
            else
            {
                count = count + 1;
            }*/
            grid.Rows.Add();
            int count = grid.Rows.Count;

            grid.Rows[count - 1].Cells[1].Value = date;
            grid.Rows[count - 1].Cells[2].Value = description;
            grid.Rows[count - 1].Cells[3].Value = amount;
            grid.Rows[count - 1].Cells[4].Value = quantity;
            grid.Rows[count - 1].Cells[5].Value = totalSale;
            grid.Rows[count - 1].Cells[6].Value = expense;
            grid.Rows[count - 1].Cells[7].Value = entry;
            grid.Rows[count - 1].Cells[8].Value = type;
            grid.Rows[count - 1].Cells[9].Value = key;


        }

        private void expenseandreceiving()
        {
            DataTable dt = new BLogic().getCashInout("ER", date);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                addData(dr, i);
            }

        }

        private void addData(DataRow d, int i)
        {
            string idcashinout = d[0].ToString();
            string date = d[2].ToString();
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

            // return;
            bool check = false;

            addRowAllSales(dg_SER, date, ccname, ""+ cash
                , "","", "", "", entrytype, cate_name, idcashinout);
        }

        private void ControlSalesExpRec_Load(object sender, EventArgs e)
        {
            expenseandreceiving();
            initSalesUpdate(saleParser, adminlog.SalesInProccessedFolder, "Default");
            Count = dg_SER.Rows.Count;


        }

        private void dg_SER_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                string id = dg_SER.Rows[index].Cells[9].Value.ToString();
                string entry = dg_SER.Rows[index].Cells[7].Value.ToString();
                string type = dg_SER.Rows[index].Cells[8].Value.ToString();

                if (type == "Sales") return;


                bool check = new BLogic().p_cashinout_Crud("D", "", "", "", 0, 0, 0, 0,
                    "", int.Parse(id), "", "", 0, 0, "", "", "d");
                if (check)
                {
                    dg_SER.Rows.RemoveAt(index);
                    dg_SER.Rows.Clear();
                    dg_SER.Refresh();
                    ControlSalesExpRec_Load(this, new EventArgs());
                }
            }
        }
    }
}
