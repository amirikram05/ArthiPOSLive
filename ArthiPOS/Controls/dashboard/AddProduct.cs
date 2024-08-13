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
    public partial class AddProduct : Form
    {
        ProfilesBL pbl;
        public AddProduct()
        {
            InitializeComponent();
            btn_del_veg.Enabled = false;
            btn_update_veg.Enabled = false;
            btn_add_veg.Enabled = true;
        }
        public AddProduct(string name)
        {
            InitializeComponent();
            btn_del_veg.Enabled = false;
            btn_update_veg.Enabled = false;
            btn_add_veg.Enabled = true;
            txt_veg_name_ur.Text = name;
        }

        public string Id
        {
            get; private set;
        }
        public string UName
        {
            get; private set;
        }
        public string Type
        {
            get; private set;
        }

        public string Ename { get; private set; }
        public string Rent { get; private set; }
        public string Labour { get; private set; }
        public string BipComm { get; private set; }
        public string CusComm { get; private set; }
        public string Laga { get; private set; }
        public string Chongi { get; private set; }
        public string Munshiana { get; private set; }
        public string MarketFee { get; private set; }

        public string Packing { get; private set; }
        public string PCode { get; private set; }
        public string Location { get; private set; }

        public AddProduct(int action,string code,string vegname,string ur_veg_name,string pack,string location, string freight,
            string labour, string bipari_commission,string customer_commission,string laga,string chongi,string munshiana,string marketFee)
        {
            InitializeComponent();
            pbl = new ProfilesBL();
            if (action==0)
            {
                btn_del_veg.Enabled = false;
                btn_update_veg.Enabled = false;
                btn_add_veg.Enabled = true;
            }else if(action==1)
            {
                btn_del_veg.Enabled = true;
                btn_update_veg.Enabled = true;
                btn_add_veg.Enabled = false;
                txt_veg_name.Text = vegname;
                txt_veg_name_ur.Text = ur_veg_name;
                txt_pack.Text = pack;
                txt_pcode.Text = location;
                txt_freight.Text = freight;
                txt_labour.Text = labour;
                txt_bip_comm.Text = bipari_commission;
                txt_cust_comm.Text = customer_commission;
                txt_laga.Text = laga;
                txt_chongi.Text = chongi;
                txt_munsihana.Text = munshiana;
                txt_market_fee.Text = marketFee;
                lbl_code.Text = code;
            }
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            pbl = new ProfilesBL();
        }

        private void btn_add_veg_Click(object sender, EventArgs e)
        {
            UName= txt_veg_name_ur.Text;
            Ename= txt_veg_name.Text;
            BipComm =BipComm= txt_bip_comm.Text;
            CusComm =CusComm= txt_cust_comm.Text;
            Packing =Packing= txt_pack.Text;
            PCode = txt_pcode.Text;
            Rent = txt_freight.Text;
            Labour = txt_labour.Text;
            Location = txt_location.Text;
            Laga = txt_laga.Text;
            Chongi= txt_chongi.Text;
            Munshiana = txt_munsihana.Text;
            MarketFee = txt_market_fee.Text;




            if (pbl.p_product_Insert("", UName, Ename, Rent, Labour, BipComm, PCode, Packing, CusComm, Location,Laga,Chongi,Munshiana,MarketFee))
            {
                MessageBox.Show("Product Inserted Successfully.");
                this.Close();
            }
            

        }

        private void btn_update_veg_Click(object sender, EventArgs e)
        {
            Id = lbl_code.Text;
            UName = txt_veg_name_ur.Text;
            Ename = txt_veg_name.Text;
            BipComm = BipComm = txt_bip_comm.Text;
            CusComm = CusComm = txt_cust_comm.Text;
            Packing = Packing = txt_pack.Text;
            PCode = txt_pcode.Text;
            Rent = txt_freight.Text;
            Labour = txt_labour.Text;
            Location = txt_location.Text;
            Laga = txt_laga.Text;
            Chongi = txt_chongi.Text;
            Munshiana = txt_munsihana.Text;
            MarketFee = txt_market_fee.Text;

            if (pbl.p_product_Update(Id, UName, Ename, Rent, Labour, BipComm, PCode, Packing, CusComm, Location, Laga, Chongi,Munshiana, MarketFee))
            {
                MessageBox.Show("Product Updated Successfully.");
                this.Close();
            }
        }

        private void btn_del_veg_Click(object sender, EventArgs e)
        {
            string code = lbl_code.Text;
            if (pbl.p_product_Delete(code))
            {
                MessageBox.Show("Product Deleted Successfully.");
                this.Close();
            }
        }
    }
}
