using ArthiPOS.controls.dashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataMember;
using ArthiPOS.Controls.test;

namespace ArthiPOS.Controls.dashboard
{
    public partial class VendorForm : Form
    {
        private string date = "";
        private int choice = 0;
        string status = "Not Live";
        public Landlord land;
        public bool updateData;

        public VendorForm(string date,int choice,string status)
        {
            InitializeComponent();
            this.date = date;
            this.choice = choice;
            this.status = status;
        }

        public VendorForm(string date, int choice, string status, Landlord land, bool updateData) : this(date, choice, status)
        {
            InitializeComponent();
            this.land = land;
            this.updateData = updateData;
        }

        private void VendorForm_Load(object sender, EventArgs e)
        {
            switch (choice)
            {
                case 1:
                    //Transport sales = new Transport(date, 1);
                    VendorStock sales = new VendorStock(date, 1, status);
                    int widths = this.Width - sales.Width;
                    sales.Left = widths / 2;
                    panel_vendor.Controls.Add(sales);
                    break;
                case 2:
                    AddVendorItem item = new AddVendorItem(date, 1, status);
                    if (updateData)
                        item.updateRecord(land, updateData,this);
                    int width = this.Width-item.Width;
                    item.Left = width / 2;
                    panel_vendor.Controls.Add(item);
                    break;
                case 3:
                    CashInout flow = new CashInout(date, true);
                    flow.ShowDialog();
                    break;
                case 4:
                    Invoicing inc = new Invoicing(date);
                    if(inc.Width> this.Width)
                    {
                        this.Width = inc.Width;
                    }
                    if (inc.Height>this.Height)
                    {
                        this.Height = inc.Height;
                    }
                    int widthinc = this.Width - inc.Width;
                    inc.Left = widthinc / 2;
                    panel_vendor.Controls.Add(inc);
                    break;

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
