using ArthiPOS.Reporting;
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
    public partial class SeasonList : Form
    {
        private BLogic bal;
        private DataTable dt;
        public SeasonList()
        {
            InitializeComponent();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            string sdate = date_start.Text;
            string ldate = date_last.Text;

           List<object> obj=(List<object>)new BLogic().createSeason(sdate,ldate);
            if (obj[0].ToString()!="")
            {
                reload();
            }
        }

        private void reload()
        {
            dgv_season.Refresh();
            dgv_season.Rows.Clear();
            dt = new BLogic().seasonList("","");
            foreach (DataRow dr in dt.Rows)
            {
                addUpdateRowGridLandlord(dr[0].ToString(),
                    dr[1].ToString(),
                    dr[2].ToString(),
                    dr[3].ToString(),
                    dr[4].ToString(),
                    dr[5].ToString(),
                    dr[6].ToString(),
                    dr[7].ToString(),
                    dr[8].ToString(),
                    dr[9].ToString(),
                    dr[10].ToString(),
                    dr[11].ToString(),
                    dr[12].ToString(),
                    dr[13].ToString(),
                    dr[14].ToString(),
                    dr[15].ToString(),
                    dr[16].ToString(),
                    dr[17].ToString(),
                    dr[18].ToString(), dr[19].ToString());
            }


        }

        private void addUpdateRowGridLandlord(
            string col2,
            string col3,
            string col4,
            string col5,
            string col6,
            string col7,
            string col8,
            string col9,
            string col10,
            string col11,
            string col12,
            string col13,
            string col14,
            string col15,
            string col16,
            string col17,
            string col18,
            string col19,
            string col20,string col21)
        {
            int count = this.dgv_season.Rows.Count;
            
            this.dgv_season.Rows.Add();

            this.dgv_season.Rows[count - 1].Cells[2].Value = col2;
            this.dgv_season.Rows[count - 1].Cells[3].Value = col3;
            this.dgv_season.Rows[count - 1].Cells[4].Value = col4;
            this.dgv_season.Rows[count - 1].Cells[5].Value = col5;
            this.dgv_season.Rows[count - 1].Cells[6].Value = col6;
            this.dgv_season.Rows[count - 1].Cells[7].Value = col7;
            this.dgv_season.Rows[count - 1].Cells[8].Value = col8;
            this.dgv_season.Rows[count - 1].Cells[9].Value = col9;
            this.dgv_season.Rows[count - 1].Cells[10].Value = col10;
            this.dgv_season.Rows[count - 1].Cells[11].Value = col11;
            this.dgv_season.Rows[count - 1].Cells[12].Value = col12;
            this.dgv_season.Rows[count - 1].Cells[13].Value = col13;
            this.dgv_season.Rows[count - 1].Cells[14].Value = col14;
            this.dgv_season.Rows[count - 1].Cells[15].Value = col15;
            this.dgv_season.Rows[count - 1].Cells[16].Value = col16;
            this.dgv_season.Rows[count - 1].Cells[17].Value = col17;
            this.dgv_season.Rows[count - 1].Cells[18].Value = col18;
            this.dgv_season.Rows[count - 1].Cells[19].Value = col19;
            this.dgv_season.Rows[count - 1].Cells[20].Value = col20;
            this.dgv_season.Rows[count - 1].Cells[21].Value = col21;

        }


        private void SeasonList_Load(object sender, EventArgs e)
        {
            bal = new BLogic();
            reload();
        }

        private void dgv_season_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (e.ColumnIndex == 0)
            {
                string id = dgv_season.Rows[index].Cells[21].Value.ToString();
                DialogResult dialogResult = MessageBox.Show( "Do You Want To Delete. "+id+"?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bal.deleteSeason(id);
                    reload();
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
        }

        private void btn_print_report_Click(object sender, EventArgs e)
        {
            AllReportsCC rp = new AllReportsCC();
            rp.printSeason(dt);
            rp.ShowDialog();
        }
    }
}
