using BAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class MoveKhata : Form
    {
        private string date = "";
        private DataTable sales;
        public MoveKhata(string date)
        {
            InitializeComponent();
            this.date = date;

            getSales(date);

        }

        private void getSales(string date)
        {
            this.sales = new BLogic().getp_DailyCRUD("SaleToMove", date, date_move.Text);
            this.dt_movesales.Rows.Clear();
            this.dt_movesales.DataSource = sales;
        }

        private void btn_movekahta_Click(object sender, EventArgs e)
        {
            string todaydate = sale_date.Text;
            string moveto = date_move.Text;

            bool chk = new BLogic().p_moveSaleDate("All", todaydate, moveto);
            if (chk)
                getSales(date);

        }
    }
}
