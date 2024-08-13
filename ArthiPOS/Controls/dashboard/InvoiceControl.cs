using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataMember;
using BAL;
using ArthiPOS.Reporting;
using System.IO;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using DevExpress.XtraPrinting;
using ArthiPOS.Properties;
using ArthiPOS.utill;
using ArthiPOS.Utill;
using ArthiPOS.Reporting.ReportView.Header;

namespace ArthiPOS.controls.dashboard
{
    public partial class InvoiceControl : UserControl
    {
        private Landlord landlord;
        private Customer customer;
        private CustomerSales custSale;
        private SaleParser saleParser;
        private string date="";
        public bool isLocal = false;


        public InvoiceControl()
        {
            InitializeComponent();

        }
        public void changeSize(MetroFramework.MetroLabelSize msize)
        {
            _lbl_khata_id.FontSize = msize;
            _lbl_total_amount.FontSize = msize;
            _lbl_total_quantity.FontSize = msize;
            _lbl_munshiana.FontSize = msize;
            _lbl_labour.FontSize = msize;
            _lbl_rent.FontSize = msize;
            _lbl_advance.FontSize = msize;
            _lbl_commission.FontSize = msize;
            _lbl_chongi.FontSize = msize;
        }
        public void updateUI()
        {
            _lbl_khata_id.Text = Resources.ResourceManager.GetString("a0013");
            _lbl_total_amount.Text = Resources.ResourceManager.GetString("a0503");
            _lbl_total_quantity.Text = Resources.ResourceManager.GetString("a0401");
            _lbl_munshiana.Text = Resources.ResourceManager.GetString("a0307");
            _lbl_labour.Text = Resources.ResourceManager.GetString("a0303");
            _lbl_rent.Text = Resources.ResourceManager.GetString("a0508");
            _lbl_advance.Text = Resources.ResourceManager.GetString("a1025");
            _lbl_commission.Text = Resources.ResourceManager.GetString("a0302");
            _lbl_chongi.Text = Resources.ResourceManager.GetString("a0301");

        }
        public InvoiceControl(Landlord landlord, CustomerSales custSales, Customer customer,string date)
        {
            InitializeComponent();
            updateUI();
            /*if (LogUtill.getLanguageLog().language=="ur-PK")
            {
                changeSize(MetroFramework.MetroLabelSize.Medium);
            }
            else
            {
                changeSize(MetroFramework.MetroLabelSize.Medium);
            }*/
            this.custSale = custSales;
            this.customer = customer;
            this.landlord = landlord;
            this.date = date;

            UpdateData();

        }
        


        public void UpdateData()
        {
            if (custSale != null)
            {
                lbl_invoice_no.Text = this.custSale.person.pkey;
                lbl_name.Text = this.custSale.person.pname;
                lbl_total_amount.Text = "" + this.custSale.SUM_GTotal_Sale;
                lbl_total_quantity.Text = "" + this.custSale.total_quantity;

                lbl_commission.Text = "" + this.custSale.Total_Commission;
                lbl_chongi.Text = "" + this.custSale.Total_Chongi;
                return;
            }

            if (this.customer != null)
            {
                saleParser = new SaleParser(date, Admin.SaveLog);
                lbl_invoice_no.Text = this.customer.customer_profile.pkey;
                lbl_name.Text = this.customer.customer_profile.pname;
                lbl_total_amount.Text = "" + (this.customer.GrandTotalCustomer);
                lbl_total_quantity.Text = "" + this.customer.total_quantity;
                lbl_commission.Text = "" + this.customer.Total_Commission;
                lbl_chongi.Text = "" + this.customer.Total_Chongi;
                return;
            }
            else if (this.landlord != null)
            {
                saleParser = new SaleParser(this.landlord.date, Admin.SaveLog);

                lbl_invoice_no.Text = this.landlord.land_person.pkey;
                lbl_name.Text = this.landlord.land_person.pname;

                lbl_commission.Text = "" + this.landlord.GetCommission;
                lbl_chongi.Text = "" + this.landlord.GetChongi;
                lbl_munshiana.Text = "" + this.landlord.expense.total_munshiana;

                lbl_rent.Text = "" + this.landlord.expense.total_rent;
                lbl_labour.Text = "" + this.landlord.expense.total_labour;
                lbl_advance.Text = "" + this.landlord.land_person.advance;
                lbl_total_quantity.Text = "" + this.landlord.land_product.total_Quantity;
                lbl_total_amount.Text = "" + this.landlord.GetGrandTotal;
            }

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            BLogic bal = new BLogic();
            SaleDetail sd;
            if (landlord!=null)
            {
                sd = new SaleDetail(landlord);
                sd.ShowDialog();
                this.landlord = sd.getLandlord();
                UpdateData();
            }
            else if(custSale != null)
            {
                if (isLocal)
                {
                    sd = new SaleDetail(isLocal,custSale);
                    sd.date = this.date;
                }
                else
                {
                    sd = new SaleDetail(custSale.person.pkey, this.date, customer);
                    sd.date = custSale.date;

                }
                sd.ShowDialog();
                this.customer = sd.getCustomer();
                UpdateData();

            }

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            bool isCustomer = false;
            if (landlord != null)
            {
                
                /*using (RCBilling rc = new RCBilling(landlord, landlord.date))
                {
                    rc.ShowDialog();
                }*/
                 isCustomer = false;


                ReportPages rp = new ReportPages(isCustomer,landlord);
                rp.ShowDialog();

            }
            else if (this.custSale != null)
            {
                /*using (RCBilling rc = new RCBilling(customer, customer.date))
                {
                    rc.ShowDialog();
                }*/
                isCustomer = true;
                ReportPages rp = new ReportPages(isCustomer,custSale, customer, custSale.date);
                rp.ShowDialog();
            }

           // publishReport(bill);
            
            /*AugraiDetailReport myForm = new AugraiDetailReport(bill);
            myForm.TopLevel = true;
            myForm.ShowInTaskbar = false;
            myForm.ShowDialog();*/
        }

        public void publishReport(XtraReport report)
        {
            ReportPrintTool printtool = new ReportPrintTool(report);
            printtool.ShowPreviewDialog();
        }

      }
}
