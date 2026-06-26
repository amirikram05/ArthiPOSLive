using BAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class Category : Form
    {
        ComboBox cat;
        DataTable dt;
        public BLogic bal;
        public Category()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            dt = new BLogic().getCategory("CateDetail", "");
            if (dt == null)
                return;
            this.dg_cate.Rows.Clear();
            this.dg_cate.DataSource = dt;
            /*for(int i= 0;i< dt.Rows.Count;i++)
            {
                // updateRow(i);
                this.dg_cate.Rows[i].Cells[0].Value = "Update";

            }*/

            //int count = this.dg_cate.Rows.Count;

        }
        private void updateRow(int index)
        {
            this.dg_cate.Rows[index].Cells[0].Value = "Update";
        }
        private void addGridRow(string ID, string CateName, string Key)
        {
            int count = this.dg_cate.Rows.Count;
            this.dg_cate.Rows.Add();
            this.dg_cate.Rows[count - 1].Cells[0].Value = ID;
            this.dg_cate.Rows[count - 1].Cells[1].Value = CateName;
            this.dg_cate.Rows[count - 1].Cells[2].Value = Key;
            this.dg_cate.Rows[count - 1].Cells[3].Value = "Update";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string name = text_cat.Text;
            if (name == "")
                return;
            int chk = bal.p_CategoryCreateDelete("Add", name, "", "" + dg_cate.Rows.Count + 1);

        }

        private void dg_cate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                string id = dg_cate.Rows[index].Cells[0].Value.ToString();
                string Name = dg_cate.Rows[index].Cells[1].Value.ToString();
                string Key = dg_cate.Rows[index].Cells[2].Value.ToString();
                new BLogic().updateCategory(id, Name, Key);

            }
        }
    }
}
