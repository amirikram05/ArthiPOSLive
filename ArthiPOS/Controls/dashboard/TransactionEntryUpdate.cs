using BAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace ArthiPOS.Controls.dashboard
{
    public partial class TransactionEntryUpdate : Form
    {
        public TransactionEntryUpdate()
        {
            InitializeComponent();
        }
        string name = "", urduname = "", transid = "", id = "";

        private void btn_tr_add_Click(object sender, EventArgs e)
        {
            name = txt_tr_engname.Text;
            urduname = txt_tr_urduname.Text;
            new BLogic().p_acc_transcation_crud("Trans", 1, name, urduname, 0, 0);
            refresh(1);
        }

        private void btn_tr_update_Click(object sender, EventArgs e)
        {
            name = txt_tr_engname.Text;
            urduname = txt_tr_urduname.Text;
            id = txt_trid.Text;
            if (id == "") return;
            new BLogic().p_acc_transcation_crud("Trans", 2, name, urduname, 0, int.Parse(id));

            refresh(1);
        }

        private void btn_atr_update_Click(object sender, EventArgs e)
        {
            id = txt_tr_ac_id.Text;
            name = txt_tr_engname.Text;
            urduname = txt_tr_urduname.Text;
            transid = lbl_tranc_id.Text;
            if (transid == "") return;

            new BLogic().p_acc_transcation_crud("ACCTranc", 2, name, urduname, int.Parse(transid), int.Parse(id));
            refresh(0);
        }

        private void btn_atr_add_Click(object sender, EventArgs e)
        {
            id = txt_tr_ac_id.Text;
            if (string.IsNullOrEmpty(id))
                id = "-1";
            name = txt_tr_engname.Text;
            urduname = txt_tr_urduname.Text;
            transid = lbl_tranc_id.Text;
            if (transid == "") return;

            new BLogic().p_acc_transcation_crud("ACCTranc", 1, name, urduname, int.Parse(transid), 0);
            refresh(0);
        }

        private void dgv_acc_trac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            string id = dgv_acc_trac.Rows[index].Cells[0].Value.ToString();
            string ename = dgv_acc_trac.Rows[index].Cells[1].Value.ToString();
            string uname = dgv_acc_trac.Rows[index].Cells[2].Value.ToString();
            string transid = dgv_acc_trac.Rows[index].Cells[3].Value.ToString();
            string transname = dgv_acc_trac.Rows[index].Cells[4].Value.ToString();
            txt_tr_ac_id.Text = id;
            txt_tr_ac_engname.Text = ename;
            txt_tr_ac_urduname.Text = uname;
            lbl_tranc_id.Text = transid;
            txt_trac_name.Text = transname;

        }

        private void dgv_trac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index < 0)
                return;
            string id = dgv_trac.Rows[index].Cells[0].Value.ToString();
            string ename = dgv_trac.Rows[index].Cells[1].Value.ToString();
            string uname = dgv_trac.Rows[index].Cells[2].Value.ToString();
            txt_trid.Text = id;
            txt_tr_engname.Text = ename;
            txt_tr_urduname.Text = uname;
        }

        private void btn_act_del_Click(object sender, EventArgs e)
        {
            id = txt_tr_ac_id.Text;
            name = txt_tr_engname.Text;
            urduname = txt_tr_urduname.Text;
            transid = lbl_tranc_id.Text;
            if (transid == "") return;

            new BLogic().p_acc_transcation_crud("ACCTranc", 3, name, urduname, int.Parse(transid), int.Parse(id));
            refresh(0);
        }

        private void btn_trans_del_Click(object sender, EventArgs e)
        {
            name = txt_tr_engname.Text;
            urduname = txt_tr_urduname.Text;
            id = txt_trid.Text;
            if (id == "") return;
            new BLogic().p_acc_transcation_crud("Trans", 3, name, urduname, 0, int.Parse(id));

        }

        private void TransactionEntryUpdate_Load(object sender, EventArgs e)
        {
            refresh(0);
            refresh(1);

        }
        public void refresh(int chk)
        {
            if (chk == 1)
            {
                DataTable dtrans = new BLogic().p_acc_transcation_Read("Trans", 4);
                dgv_trac.DataSource = dtrans;

            }
            else
            {
                DataTable dacctrans = new BLogic().p_acc_transcation_Read("ACCTranc", 4);
                dgv_acc_trac.DataSource = dacctrans;

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Up:
                    dgv_acc_trac.Focus();
                    return true;
                case Keys.Down:
                    dgv_trac.Focus();
                    return true;
                case Keys.Delete:
                    return true;
                case Keys.F2:
                    return true;
                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.F5:
                    refresh(0);
                    refresh(1);
                    return true;
                case Keys.Enter:
                    using (Search search = new Search(101, txt_trac_name.Text))
                    {
                        DialogResult res = search.ShowDialog();
                        txt_trac_name.Text = search.Name;
                        lbl_tranc_id.Text = search.Id;
                        search.Close();
                    }
                    return true;

            }



            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
