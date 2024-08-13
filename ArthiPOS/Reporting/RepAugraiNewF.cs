using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArthiPOS.Reporting.ReportView;
using BAL;
using DataMember;
using CrystalDecisions.Shared;
using System.IO;
using CommonUtilities;
using DataMember.memberlog;
using System.Diagnostics;

namespace ArthiPOS.Reporting
{
    public partial class RepAugraiNewF : Form
    {
        public RepAugraiNewF()
        {
            InitializeComponent();
            //chk_printall.Checked = false;
            //rd_check.SelectedIndex = 0;

        }
        public RepAugraiNewF(DataTable custAugrai)
        {
            InitializeComponent();
            //printReport(custAugrai);
            //chk_printall.Checked = false;
            //rd_check.SelectedIndex = 0;


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
        TestAugraiCR cr;
        public void printReport(DataTable custAugrai)
        {
             cr= new TestAugraiCR();

            cr.Database.Tables["CustAugrai"].SetDataSource(custAugrai);
            DataTable wm = new DataTable();
            //wm.Columns.Add("waterpath", typeof(string));
            //string startupPath = Environment.CurrentDirectory;
            //wm.Rows.Add(@startupPath + "\\watermark.jpg");
            //cr.Database.Tables["Watermark"].SetDataSource(wm);
            cr.SetParameterValue("logo", Authentication.Account.trade_mark);
            crystal_view_customer.ReportSource = null;
            crystal_view_customer.ReportSource = cr;
        }
        private void RepAugrai_Load(object sender, EventArgs e)
        {
            
        }

        public void savetoPDF(string filetype)
        {
            AdminLog adminLog = LogUtill.getAdminInputLog();
            string path = adminLog.ReportPath;
            if (!Directory.Exists(path) && path != "")
            {
                Directory.CreateDirectory(path);
            }
            path = path + "Augrai-" + date_start.Text + filetype;
            //PageMargins margins = new PageMargins { topMargin = 100, leftMargin = 250, bottomMargin = 100, rightMargin = 100 };
            //cr.PrintOptions.ApplyPageMargins(margins);
            //Stream str = cr.ExportToStream(ExportFormatType.PortableDocFormat);
            //int length = Convert.ToInt32(str.Length);
            //byte[] bytes = new byte[length];
            //str.Read(bytes, 0, length);
            //str.Close();
            //File.WriteAllBytes(@path, bytes);
            //Process.Start("C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe", "file:///"+path);

            // Export the report to HTML format
            if (filetype == ".html")
            {
                ExportOptions exportOptions = new ExportOptions();
                exportOptions.ExportFormatType = ExportFormatType.HTML32; // or HTML40 if needed
                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                DiskFileDestinationOptions diskOptions = new DiskFileDestinationOptions();
                diskOptions.DiskFileName = path;
                exportOptions.DestinationOptions = diskOptions;

                // Set HTML export options to include page breaks
                HTMLFormatOptions htmlOptions = new HTMLFormatOptions();
                htmlOptions.HTMLBaseFolderName = Path.GetDirectoryName(path);
                htmlOptions.HTMLFileName = Path.GetFileNameWithoutExtension(path);
                htmlOptions.HTMLEnableSeparatedPages = true; // Enable page breaks
                exportOptions.FormatOptions = htmlOptions;

                cr.Export(exportOptions);
            }
            else if (filetype == ".pdf")
                cr.ExportToDisk(ExportFormatType.PortableDocFormat, path);
            else if (filetype == ".xlsx")
                cr.ExportToDisk(ExportFormatType.Excel, path);



            // Check if the HTML file exists
            if (File.Exists(path))
            {
                // Open the HTML file in the default web browser
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
            else
            {
                Console.WriteLine("HTML file not found.");
            }
        }

        private void chk_printall_CheckedChanged(object sender, EventArgs e)
        {
            rd_check_Click(this, new EventArgs());
        }

        

        private void chk_full_detail_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = new BLogic().p_customer_CRUD("Augrai", "2", date_start.Text);
            printReport(dt);
        }

        private void crystal_view_customer_Load(object sender, EventArgs e)
        {

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            

        }

        private void date_start_CloseUp(object sender, EventArgs e)
        {
            rd_check_Click(this, new EventArgs());
        }

        private void rd_check_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (rb_customer.Checked)
            {
                chk_saleadvance.Enabled = false;
                rb_client.Checked = false;
                if (chk_printall.Checked)
                {
                    dt = new BLogic().p_customer_CRUD("Augrai", "1", date_start.Text);

                }
                else
                {

                    dt = new BLogic().p_customer_CRUD("Augrai", "0", date_start.Text);

                }
            }
            else
            if (rb_client.Checked)
            {
                chk_saleadvance.Enabled = true;
                if (chk_printall.Checked)
                {
                    if (chk_saleadvance.Checked)
                        dt = new BLogic().p_customer_CRUD("ClientSale", "1", "");
                    else
                        dt = new BLogic().p_customer_CRUD("ClientInv", "1", "");


                }
                else
                {
                    if (chk_saleadvance.Checked)
                        dt = new BLogic().p_customer_CRUD("ClientSale", "0", "");
                    else
                        dt = new BLogic().p_customer_CRUD("ClientInv", "0", "");
                }
            }
            printReport(dt);

        }

        private void chk_saleadvance_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_saleadvance.Checked)
            {
                chk_saleadvance.Text = "Sales";
            }
            else
                chk_saleadvance.Text = "Advance";
            rd_check_Click(this, new EventArgs());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savetoPDF(".pdf");
        }

        private void hTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savetoPDF(".html");

        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savetoPDF(".xlsx");

        }
    }
}
