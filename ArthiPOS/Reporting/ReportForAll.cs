using ArthiPOS.Reporting.ReportView;
using BAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArthiPOS.Reporting
{
    public partial class ReportForAll : Form
    {
        public ReportForAll()
        {
            InitializeComponent();
        }
        DataTable dt;
        private void btn_search_Click(object sender, EventArgs e)
        {

            if (cm_data.SelectedIndex==0)//chatha
            {
                dt = new BLogic().p_chatha(date_start.Text, date_last.Text);

            }
            else if(cm_data.SelectedIndex==1)//Advance
            { }
            else if (cm_data.SelectedIndex == 2)//Season
            {
                List<object> list = new BLReport().p_reporting_CRUD("SeasonDetail", date_start.Text, date_last.Text, 0, 100, "season");
                dt = (DataTable)list[1];
            }
            else if (cm_data.SelectedIndex == 3)//Ledger
            { }
            else if (cm_data.SelectedIndex == 4)//BalanceSheet
            {
                List<Object> obj = (List<object>)new BLReport().p_balance_sheet_read(date_start.Text, date_last.Text,
                                0, 0);
                if (obj == null)
                {
                    return;
                }
                dt = (DataTable)obj[1];
            }
            else if (cm_data.SelectedIndex == 5)//SERP
            {
                List<Object> obj = (List<object>)new BLReport().p_dailyProfitSalesExpense("SERP", date_start.Text, date_last.Text,
                                0, 0);
                dt = (DataTable)obj[1];
            }
            else if (cm_data.SelectedIndex == 6)//Daily Accounts
            {
                dt = new BLogic().p_bs_read("DAcc", date_start.Text, date_last.Text);
            }
            else if (cm_data.SelectedIndex == 7)//Fright/Expense Detail
            { }
            else if (cm_data.SelectedIndex == 8)//  Customer Augrai Detail
            {
                dt = new BLogic().p_customer_CRUD("Augrai", "0", date_start.Text);
            }
            else if (cm_data.SelectedIndex == 9)//Advance / Investment Detail
            {
                List<Object> obj = (List<object>)new BLogic().searchProfile("", "SClient", "", 0, 0);
                dt = (DataTable)obj[1];

            }
            else if(cm_data.SelectedIndex==10)//Detail Porofti/expense
            {
                List<object> obj = (List<object>)new BLReport().p_DetailReport(date_start.Text, date_last.Text, "");
                if (obj == null)
                {
                    return;
                }
                dt = (DataTable)obj[1];
            }
            else if (cm_data.SelectedIndex == 11)//Product sale detail
            { 
                dt = new BLogic().readFardHisab("AllProduct", "", date_start.Text, date_last.Text);
            }
            else if (cm_data.SelectedIndex == 12)//Product sale detail
            {
                dt = new BLogic().p_cashflow_SP( date_start.Text, date_last.Text);
            }

            dataGridView1.DataSource = dt;
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            //Open the print dialog
            /* PrintDialog printDialog = new PrintDialog();
             printDialog.Document = printDocument1;
             printDialog.UseEXDialog = true;
             //Get the document
             if (DialogResult.OK == printDialog.ShowDialog())
             {
                 printDocument1.DocumentName = "Test Page Print";
                 printDocument1.BeginPrint += printDocument1_BeginPrint;
                 printDocument1.PrintPage += printDocument1_PrintPage;
                 printDocument1.Print();
             }*/
            /*
            Note: In case you want to show the Print Preview Dialog instead of 
            Print Dialog then comment the above code and uncomment the following code
            */

            //Open the print preview dialog
            if (cm_data.SelectedIndex == 0 || cm_data.SelectedIndex == 6 
                || cm_data.SelectedIndex == 10 || cm_data.SelectedIndex == 12)
            {

                AllReportView ar = new AllReportView(dt, cm_data.SelectedIndex,date_start.Text,date_last.Text);
                ar.ShowDialog();

            }
            else
            {
                PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
                objPPdialog.Document = printDocument1;
                objPPdialog.ShowDialog();
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
        #region Member Variables
        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height
        #endregion
        #region Print Button Click Event
        /// <summary>
        /// Handles the print button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        #endregion

        #region Begin Print Event Handler
        /// <summary>
        /// Handles the begin print event of print document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dataGridView1.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Print Page Event
        /// <summary>
        /// Handles the print page event of print document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                // Determine if the document should be printed in landscape mode
                bool landscapeMode = dataGridView1.Columns.Count > 5;

                // Set the print document orientation
                if (landscapeMode)
                {
                    e.PageSettings.Landscape = true;
                }
                else
                {
                    e.PageSettings.Landscape = false;
                }
                //Set the left margin
                int iLeftMargin = 10;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headres
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dataGridView1.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dataGridView1.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Customer Summary", new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Customer Summary", new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Customer Summary", new Font(new Font(dataGridView1.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    
    }

}
