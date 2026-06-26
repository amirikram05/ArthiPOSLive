using ArthiPOS.Reporting;
using ArthiPOS.Reporting.ReportView.Bills;
using ArthiPOS.Reporting.ReportView.NoHeader;
using DataMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ArthiPOS.Utill
{
    public class CustomDailog : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private string type;
        private List<Landlord> tclients;

        public CustomDailog()
        {
        }
        
        public CustomDailog(List<Landlord> tclients, string type, string buttonText1, string buttonText2)
        {
            InitializeComponent();
            button1.Text = buttonText1;
            button2.Text = buttonText2;
            this.type = type;
            this.tclients = tclients;
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(64, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "All Page";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(64, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 49);
            this.button2.TabIndex = 1;
            this.button2.Text = "Local Bills";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CustomDailog
            // 
            this.ClientSize = new System.Drawing.Size(267, 189);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CustomDailog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CustomDailog_Load);
            this.ResumeLayout(false);

        }

        private void CustomDailog_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //All
            if (type == "Client")
            {
                DataTable dt = ReportData.createSaleDataset(tclients);
                /*SalesTodayAllDetail rb = new SalesTodayAllDetail();
                DataTable dt = ReportData.createSaleDataset(tclients);
                AllReportsCC all = new AllReportsCC();
                rb.Database.Tables["Sales"].SetDataSource(dt);
                if (dt == null)
                    return;
                rb.SetParameterValue("Title", Authentication.Account.shop_name);
                rb.SetParameterValue("Propriter", Authentication.Account.propriters_name);
                rb.SetParameterValue("Name1", Authentication.Account.name1 ?? "");
                rb.SetParameterValue("Phone1", Authentication.Account.phone1 ?? "");
                rb.SetParameterValue("Name2", Authentication.Account.name2 ?? "");
                rb.SetParameterValue("Phone2", Authentication.Account.phone2 ?? "");
                rb.SetParameterValue("Address", Authentication.Account.address ?? "");
                rb.SetParameterValue("Business", Authentication.Account.business_type ?? "");
                all.crystalReportViewer1.ReportSource = rb;
                all.ShowDialog();*/
                //ReportTest rb = new ReportTest();
                SalesTodayAllDetail rb = new SalesTodayAllDetail();
                if (dt == null)
                    return;
                rb.Database.Tables["Sales"].SetDataSource(dt);
                DataTable wm = new DataTable();
                wm.Columns.Add("waterpath", typeof(string));
                string startupPath = Environment.CurrentDirectory;
                wm.Rows.Add(@startupPath + "\\watermark.jpg");
                rb.Database.Tables["Watermark"].SetDataSource(wm);


                AllReportsCC all = new AllReportsCC();
                all.crystalReportViewer1.ReportSource = rb;
                all.ShowDialog();
            }
            else
            if (type == "Customer")
            {
                SalesCustAllDetail rb = new SalesCustAllDetail();
                DataTable dt = ReportDataCustomer.createSaleDataset(tclients);
                AllReportsCC all = new AllReportsCC();
                rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                if (dt == null)
                    return;
                rb.SetParameterValue("Title", Authentication.Account.shop_name);
                rb.SetParameterValue("Propriter", Authentication.Account.propriters_name);
                rb.SetParameterValue("Name1", Authentication.Account.name1 ?? "");
                rb.SetParameterValue("Phone1", Authentication.Account.phone1 ?? "");
                rb.SetParameterValue("Name2", Authentication.Account.name2 ?? "");
                rb.SetParameterValue("Phone2", Authentication.Account.phone2 ?? "");
                rb.SetParameterValue("Address", Authentication.Account.address ?? "");
                rb.SetParameterValue("Business", Authentication.Account.business_type ?? "");
                all.crystalReportViewer1.ReportSource = rb;
                all.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (type == "Client")
            {
                /*SalesTodayNHA7 rb = new SalesTodayNHA7();
                DataTable dt = ReportData.createSaleDataset(tclients);
                AllReportsCC all = new AllReportsCC();
                rb.Database.Tables["Sales"].SetDataSource(dt);
                if (dt == null)
                    return;
                rb.SetParameterValue("Title", Authentication.Account.shop_name);
                rb.SetParameterValue("Propriter", Authentication.Account.propriters_name);
                rb.SetParameterValue("Name1", Authentication.Account.name1 ?? "");
                rb.SetParameterValue("Phone1", Authentication.Account.phone1 ?? "");
                rb.SetParameterValue("Name2", Authentication.Account.name2 ?? "");
                rb.SetParameterValue("Phone2", Authentication.Account.phone2 ?? "");
                rb.SetParameterValue("Address", Authentication.Account.address ?? "");
                rb.SetParameterValue("Business", Authentication.Account.business_type ?? "");
                all.crystalReportViewer1.ReportSource = rb;
                all.ShowDialog();*/

                ReportA5 rb = new ReportA5();
                DataTable dt = ReportData.createSaleDataset(tclients);
                if (dt == null)
                    return;
                AllReportsCC all = new AllReportsCC();
                rb.Database.Tables["Sales"].SetDataSource(dt);
                rb.Subreports["SaleDetail"].SetDataSource(dt);
                rb.Subreports["SaleExpense"].SetDataSource(dt);
                all.crystalReportViewer1.ReportSource = rb;
                all.ShowDialog();

            }
            else
            if (type == "Customer")
            {
                /*SalesTodayCustNHA7 rb = new SalesTodayCustNHA7();
                DataTable dt = ReportDataCustomer.createSaleDataset(tclients);
                AllReportsCC all = new AllReportsCC();
                rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                if (dt == null)
                    return;
                rb.SetParameterValue("Title", Authentication.Account.shop_name);
                rb.SetParameterValue("Propriter", Authentication.Account.propriters_name);
                rb.SetParameterValue("Name1", Authentication.Account.name1 ?? "");
                rb.SetParameterValue("Phone1", Authentication.Account.phone1 ?? "");
                rb.SetParameterValue("Name2", Authentication.Account.name2 ?? "");
                rb.SetParameterValue("Phone2", Authentication.Account.phone2 ?? "");
                rb.SetParameterValue("Address", Authentication.Account.address ?? "");
                rb.SetParameterValue("Business", Authentication.Account.business_type ?? "");
                all.crystalReportViewer1.ReportSource = rb;
                all.ShowDialog();*/


                ReportCustA7T rb = new ReportCustA7T();
                DataTable dt = ReportDataCustomer.createSaleDataset(tclients);
                rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                rb.Subreports["SaleDetail"].SetDataSource(dt);
                rb.SetParameterValue("Name1", "");
                rb.SetParameterValue("Phone1", "");
                AllReportsCC all = new AllReportsCC();
                rb.Database.Tables["CustomerSales"].SetDataSource(dt);
                if (dt == null)
                    return;
                all.crystalReportViewer1.ReportSource = rb;
                all.ShowDialog();
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
