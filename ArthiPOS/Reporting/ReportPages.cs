using ArthiPOS.Reporting.ReportView.NoHeader;
using ArthiPOS.utill;
using BAL;
using DataMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ArthiPOS.Reporting
{
    public partial class ReportPages : Form
    {
        public bool isCustomer = false;
        public Landlord landlord;
        public CustomerSales custSale;
        public Customer customer;
        string date = "";
        private List<Landlord> tclients;
        private List<Customer> customers;
        private bool checkAllReport = false;
        public bool isLocal = false;
        public ReportPages()
        {
            InitializeComponent();
        }

        public ReportPages(bool isCustomer, Landlord landlord)
        {
            InitializeComponent();
            this.isCustomer = isCustomer;
            this.landlord = landlord;
            if (isCustomer)
                btn_printA4.Enabled = true;
            else
                btn_printA4.Enabled = false;


        }
        public ReportPages(bool isCustomer, CustomerSales custSale, Customer customer, string date)
        {
            InitializeComponent();
            this.isCustomer = isCustomer;
            this.custSale = custSale;
            this.customer = customer;
            this.date = date;
        }
        //All Report Customer
        public ReportPages(bool checkAllReport, bool isCustomer, List<Landlord> tclients, List<Customer> customers, string date)
        {
            InitializeComponent();
            this.isCustomer = isCustomer;
            this.tclients = tclients;
            this.customers = customers;
            this.date = date;
            this.checkAllReport = checkAllReport;
        }
        private bool checkdate=false;
        private DataTable dt_clcu;
        private string startdate;
        private string lastdate;
        public ReportPages(bool checkDate, bool isCustomer, DataTable dt_clcu,string startdate, string lastdate)
        {
            InitializeComponent();
            this.isCustomer = isCustomer;
            this.checkdate = checkDate;
            this.dt_clcu = dt_clcu;
            this.startdate = startdate;
            this.lastdate = lastdate;
        }


        private void btn_A5_Click(object sender, EventArgs e)
        {
            if (!checkAllReport)
            {
                if (isCustomer == false)
                {
                    using (AllReportsCC rc = new AllReportsCC(isCustomer, landlord.land_person.pid,
                            landlord.date, 2, true))
                    {
                        rc.isLocal = isLocal;
                        rc.ShowDialog();
                    }
                }
                else
                {
                    if (custSale != null)
                    {
                        using (AllReportsCC rc = new AllReportsCC(isCustomer,
                            custSale.person.pid, date, 2, true))
                        {
                            rc.ShowDialog();
                        }
                    }
                    else
                        using (AllReportsCC rc = new AllReportsCC(isCustomer,
                            customer.customer_profile.pid, date, 2, true))
                        {
                            rc.ShowDialog();
                        }
                }
            }
            else
            {

                if (isCustomer == false)
                {
                    AllReportsCC rep = new AllReportsCC(false, tclients, customers, date, 2);
                    rep.ShowDialog();
                }
                else
                {
                    AllReportsCC rep = new AllReportsCC(true, tclients, customers, date, 2);
                    rep.ShowDialog();
                }
            }

            this.Close();
        }

        private void btn_A6_Click(object sender, EventArgs e)
        {
            if (!checkAllReport)
            {
                if (isCustomer == false)
                {
                    using (AllReportsCC rc = new AllReportsCC(isCustomer, landlord.land_person.pid,
                            landlord.date, 3, true))
                    {
                        rc.ShowDialog();
                    }
                }
                else
                {
                    if (custSale != null)
                    {
                        using (AllReportsCC rc = new AllReportsCC(isCustomer,
                            custSale.person.pid, date, 3, true))
                        {
                            rc.ShowDialog();
                        }
                    }
                    else
                        using (AllReportsCC rc = new AllReportsCC(isCustomer,
                        customer.customer_profile.pid, date, 3, true))
                        {
                            rc.ShowDialog();
                        }
                }
            }
            else
            {
                if (isCustomer == false)
                {
                    AllReportsCC rep = new AllReportsCC(false, tclients, customers, date, 3);
                    rep.ShowDialog();
                }
                else
                {
                    AllReportsCC rep = new AllReportsCC(true, tclients, customers, date, 3);
                    rep.ShowDialog();
                }
            }
            this.Close();
        }

        private void btn_nA6_Click(object sender, EventArgs e)
        {
            if (!checkAllReport)
            {
                if (isCustomer == false)
                {
                    using (AllReportsCC rc = new AllReportsCC(isCustomer, landlord.land_person.pid,
                            landlord.date, 3, false))
                    {
                        rc.ShowDialog();
                    }
                }
                else
                {
                    if (custSale != null)
                    {
                        using (AllReportsCC rc = new AllReportsCC(isCustomer,
                            custSale.person.pid, date, 3, false))
                        {
                            rc.ShowDialog();
                        }
                    }
                    else
                        using (AllReportsCC rc = new AllReportsCC(isCustomer,
                        customer.customer_profile.pid, date, 3, false))
                        {
                            rc.ShowDialog();
                        }
                }
            }
            else
            {
                if (isCustomer == false)
                {
                    AllReportsCC rep = new AllReportsCC(false, tclients, customers, date, 3);
                    rep.ShowDialog();
                }
                else
                {
                    AllReportsCC rep = new AllReportsCC(true, tclients, customers, date, 3);
                    rep.ShowDialog();
                }
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!checkAllReport)
            {
                if (isCustomer == false)
                {
                    using (AllReportsCC rc = new AllReportsCC(isCustomer, landlord.land_person.pid,
                            landlord.date, 4, false))
                    {
                        rc.ShowDialog();
                    }
                }
                else
                {
                    if (custSale != null)
                    {
                        using (AllReportsCC rc = new AllReportsCC(isCustomer,
                            custSale.person.pid, date, 4, false))
                        {
                            rc.ShowDialog();
                        }
                    }
                    else
                        using (AllReportsCC rc = new AllReportsCC(isCustomer,
                        customer.customer_profile.pid, date, 4, false))
                        {
                            rc.ShowDialog();
                        }
                }
            }
            else
            {
                if (isCustomer == false)
                {
                    AllReportsCC rep = new AllReportsCC(false, tclients, customers, date, 4);
                    rep.ShowDialog();
                }
                else
                {
                    AllReportsCC rep = new AllReportsCC(true, tclients, customers, date, 4);
                    rep.ShowDialog();
                }
            }
            this.Close();
        }

        private void btn_bill_Report_Click(object sender, EventArgs e)
        {

            if (checkAllReport)
            {

                if (isCustomer)
                {
                    using (AllReportsCC rc = new AllReportsCC(true, tclients, customers, date, 5))
                    {
                        rc.ShowDialog();
                    }
                }
                else
                {
                    using (AllReportsCC rc = new AllReportsCC(false, tclients, customers, date, 5))
                    {
                        rc.ShowDialog();
                    }
                }
            }
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (AllReportsCC rc = new AllReportsCC())
            {
                if(checkdate)
                    rc.printAllCustDetailRep(false, startdate, lastdate);
                else
                    rc.printAllCustDetailRep(false, date,date);
                rc.ShowDialog();
            }


            /*
            //SalesTodayNHA7 rb = new SalesTodayNHA7();
            ReportTest rb = new ReportTest();
            //ReportA5 rb = new ReportA5();
            if (rb == null)
            {
                return;
            }
            DataTable dt = new BLogic().p_report_CustomerClient("sClient", date, date);
            
           
            if (dt == null)
                return;
            rb.Database.Tables["Sales"].SetDataSource(dt);
            rb.Subreports["SaleDetail"].SetDataSource(dt);
            rb.Subreports["SaleExpense"].SetDataSource(dt);
            AllReportsCC all = new AllReportsCC();
            all.crystalReportViewer1.ReportSource = rb;
            //rb.SetParameterValue("Name1","test");
            //rb.SetParameterValue("Phone1", "555555");
            all.ShowDialog();
            */
        }

        private void btn_nA5_Click(object sender, EventArgs e)
        {
            if (isCustomer)
            {

            }
            else
            {
                ReportA5 rb = new ReportA5();
                if (rb == null)
                {
                    return;
                }
                if (checkAllReport)
                {
                    using (AllReportsCC rc = new AllReportsCC(false, tclients, customers, date, 5))
                    {
                        rc.ShowDialog();
                    }


                }
                else
                {
                    using (AllReportsCC rc = new AllReportsCC(isCustomer, landlord.land_person.pid,
                           landlord.date, 2, false))
                    {
                        rc.ShowDialog();
                    }
                }



            }
        }

        private void ReportPages_Load(object sender, EventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {

                case Keys.Control | Keys.D0: { button3_Click(this, new EventArgs()); return true; }
                case Keys.Control | Keys.D1: { btn_cust_rep_Click(this, new EventArgs()); return true; }

                case Keys.Escape:
                    this.Close();
                    return true;


            }



            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btn_printA4_Click(object sender, EventArgs e)
        {
            if (isCustomer)
            {
                DataTable dt = null;
                using (AllReportsCC rc = new AllReportsCC())
                {
                    if (checkAllReport)
                    {
                        dt = new BLogic().p_report_CustomerClient("sCustomer", date, date);
                    }
                    else
                    {
                        if (custSale != null)
                        {
                            dt = new BLogic().p_report_CustomerClient("CustomerBilling", custSale.person.pid, date, date);

                        }
                        else
                        {
                            dt = new BLogic().p_report_CustomerClient("CustomerBilling", "1", date, date);
                            return;
                        }

                    }
                    rc.printA4FullPage(isCustomer, dt);
                    rc.ShowDialog();
                }

            }
        }


        public void printSalesClient(string date, string id)
        {
            DataTable dt = null;
            using (AllReportsCC rc = new AllReportsCC())
            {
                if (checkAllReport)
                {
                    dt = new BLogic().p_report_CustomerClient("sClient", date, date);
                }
                else
                {
                    dt = new BLogic().p_report_CustomerClient("sClientBill", id, date, date);
                }
                rc.printA4hReport(isCustomer, dt);
                rc.ShowDialog();
            }
        }

        public void printSalesCustomer(string date, string id)
        {
            DataTable dt = null;
            using (AllReportsCC rc = new AllReportsCC())
            {
                if (checkAllReport)
                {
                    dt = new BLogic().p_report_CustomerClient("sCustomer", date, date);
                }
                else
                {
                    dt = new BLogic().p_report_CustomerClient("CustomerBilling", id, date, date);
                }

                rc.printA7Report(isCustomer, dt);
                rc.ShowDialog();
            }
        }



        private void btn_print_a4h_Click(object sender, EventArgs e)
        {
            if (isCustomer)
            {
                DataTable dt = null;
                using (AllReportsCC rc = new AllReportsCC())
                {
                    if (checkAllReport)
                    {
                        dt = new BLogic().p_report_CustomerClient("sCustomer", date, date);
                    }
                    else
                    {
                        if (customer == null) return;
                        dt = new BLogic().p_report_CustomerClient("sCustomerBill", customer.customer_profile.pid, date, date);
                        if (custSale != null)
                        {
                            dt = new BLogic().p_report_CustomerClient("sCustomerBill", custSale.person.pid, date, date);

                        }
                        else
                        {
                            dt = new BLogic().p_report_CustomerClient("sCustomerBill", customer.customer_profile.pid, date, date);
                        }
                    }
                    rc.printA4hReport(isCustomer, dt);
                    rc.ShowDialog();
                }

            }
            else
            {
                DataTable dt = null;
                using (AllReportsCC rc = new AllReportsCC())
                {
                    if (!checkdate)
                    {
                        if (checkAllReport)
                        {
                            dt = new BLogic().p_report_CustomerClient("sClient", date, date);
                        }
                        else
                        {
                            if (landlord == null)
                                return;
                            dt = new BLogic().p_report_CustomerClient("sClientBill", landlord.land_person.pid, landlord.date, landlord.date);
                        }
                    }
                    else
                    {
                        dt = new BLogic().p_report_CustomerClient("sClient", startdate, lastdate);

                        //dt = dt_clcu;
                    }
                    rc.printA4hReport(isCustomer, dt);
                    rc.ShowDialog();
                }
            }
        }

        private void btn_print_A7_Click(object sender, EventArgs e)
        {
            if (isCustomer)
            {
                DataTable dt = null;
                using (AllReportsCC rc = new AllReportsCC())
                {
                    if (!checkdate)
                    {
                        if (checkAllReport)
                        {
                            dt = new BLogic().p_report_CustomerClient("sCustomer", date, date);
                        }
                        else
                        {
                            if (custSale != null)
                            {
                                dt = new BLogic().p_report_CustomerClient("CustomerBilling", custSale.person.pid, date, date);

                            }
                            else
                            {
                                dt = new BLogic().p_report_CustomerClient("CustomerBilling", "1", date, date);
                                return;
                            }

                        }
                    }
                    else
                    {
                        dt = new BLogic().p_report_CustomerClient("sCustomer", startdate, lastdate);

                    }
                    rc.printA7Report(isCustomer, dt);
                    rc.ShowDialog();
                }

            }
            else
            {
                DataTable dt = null;
                using (AllReportsCC rc = new AllReportsCC())
                {
                    if (checkAllReport)
                    {
                        dt = new BLogic().p_report_CustomerClient("sClient", date, date);
                    }
                    else
                    {
                        dt = new BLogic().p_report_CustomerClient("sClientBill", landlord.land_person.pid, landlord.date);
                    }
                    rc.printA7Report(isCustomer, dt);
                    rc.ShowDialog();
                }

            }

        }

        private void btn_cust_rep_Click(object sender, EventArgs e)
        {
            using (AllReportsCC rc = new AllReportsCC())
            {
                if (checkdate)
                    rc.printAllCustDetailRep(true, startdate, lastdate);
                else
                    rc.printAllCustDetailRep(true, date, date);
                rc.ShowDialog();
            }
        }

        private void btn_browse_html_Click(object sender, EventArgs e)
        {
        }
    }
}
