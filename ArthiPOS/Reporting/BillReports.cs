using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace ArthiPOS.Reporting
{
    public partial class BillReports : DevExpress.XtraReports.UI.XtraReport
    {
        public BillReports()
        {
            InitializeComponent();
        }
        public BillReports(string cc_id,string bill_id,string name,string date,
            string sale,string advance,string rent,string labour,
            string munshiana,string tot_service,string grandtotal,
            string commission,string chongi,string total_quantity)
        {
            InitializeComponent();
            lbl_cc_id.Text = cc_id;
            lbl_bill_key.Text = bill_id;
            lbl_name.Text = name;
            lbl_date.Text = date;
            lbl_sales.Text = sale;
            lbl_advance.Text = advance;
            lbl_rent.Text = rent;
            lbl_labour.Text = labour;
            lbl_munshiana.Text = munshiana;
            lbl_total_service.Text = tot_service;
            lbl_bill_total.Text = grandtotal;
            lbl_quantity.Text = total_quantity;




        }


    }
}
