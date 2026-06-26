using ArthiPOS.Properties;
using BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace ArthiPOS
{
    public partial class TestHome : Form
    {
        public TestHome()
        {
            InitializeComponent();
        }

        private void Combo_lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_lang.SelectedIndex == 0)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

                label1.Text = Resources.ResourceManager.GetString("0008");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ur-PK");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ur-PK");

                label1.Text = Resources.ResourceManager.GetString("a0008");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadGridData(1);

        }


        #region GridPaging
        private int ActivePage = 1;//Here give the by default selected page in datagridview

        int PagesCounter = 1;//Here give the by default count page in datagridview

        int DefaultShowRows = 10;//Here we give how many data shows in datagridview

        BindingList<listData> Baselist = null;

        BindingList<listData> Templist = null;
        private void RebindGridForPageChange()
        {

            //Rebinding the Datagridview with data

            int datasourcestartIndex = (ActivePage - 1) * DefaultShowRows;

            Templist = new BindingList<listData>();

            for (int i = datasourcestartIndex; i < datasourcestartIndex + DefaultShowRows; i++)

            {

                if (i >= Baselist.Count)

                    break;



                Templist.Add(Baselist[i]);

            }



            dataGridView1.DataSource = Templist;

            dgvWidth(_dataTable);





        }

        private void dgvWidth(DataTable dt)
        {
            int count = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                dataGridView1.Columns[count].HeaderText = dc.ColumnName;
                count++;
            }
        }

        private void RefreshPagination()
        {
            ToolStripButton[] items = new ToolStripButton[] { btn_page1, btn_page2, btn_page3, btn_page4, btn_page5 };
            //pageStartIndex contains the first button number of pagination.
            int pageStartIndex = 1;
            if (PagesCounter > 5 && ActivePage > 2)
                pageStartIndex = ActivePage - 2;

            if (PagesCounter > 5 && ActivePage > PagesCounter - 2)
                pageStartIndex = PagesCounter - 4;

            for (int i = pageStartIndex; i < pageStartIndex + 5; i++)
            {
                if (i > PagesCounter)
                {
                    items[i - pageStartIndex].Visible = false;
                }
                else
                {
                    //Changing the page numbers
                    items[i - pageStartIndex].Text = i.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    //Setting the Appearance of the page number buttons
                    if (i == ActivePage)
                    {
                        items[i - pageStartIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#83D6F6");
                        items[i - pageStartIndex].ForeColor = Color.White;
                    }
                    else
                    {
                        items[i - pageStartIndex].BackColor = Color.White;
                        items[i - pageStartIndex].ForeColor = System.Drawing.ColorTranslator.FromHtml("#83D6F6");
                    }
                }
            }
            //Enabling or Disalbing pagination first, last, previous , next buttons
            if (ActivePage == 1)
                btnBackward.Enabled = btnFirst.Enabled = false;
            else
                btnBackward.Enabled = btnFirst.Enabled = true;

            if (ActivePage == PagesCounter)
                btnForward.Enabled = btnLast.Enabled = false;
            else
                btnForward.Enabled = btnLast.Enabled = true;
        }

        DataTable _dataTable = new DataTable();
        private BindingList<listData> FillDataforGrid()
        {
            BindingList<listData> list = new BindingList<listData>();
            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                listData obj = new listData(_dataTable.Rows[i][0].ToString(), _dataTable.Rows[i][1].ToString());
                list.Add(obj);
            }
            return list;
        }

        #region Page EventListner
        private void btnFirst_Click(object sender, EventArgs e)

        {

            try

            {

                ToolStripButton ToolStripButton = ((ToolStripButton)sender);



                //Determining the current page

                if (ToolStripButton == btnBackward)

                    ActivePage--;

                else if (ToolStripButton == btnForward)

                    ActivePage++;

                else if (ToolStripButton == btnLast)

                    ActivePage = PagesCounter;

                else if (ToolStripButton == btnFirst)

                    ActivePage = 1;

                else

                    ActivePage = Convert.ToInt32(ToolStripButton.Text, System.Globalization.CultureInfo.InvariantCulture);



                if (ActivePage < 1)

                    ActivePage = 1;

                else if (ActivePage > PagesCounter)

                    ActivePage = PagesCounter;



                //Rebind the Datagridview with the data.

                RebindGridForPageChange();



                //Change the pagiantions buttons according to page number

                RefreshPagination();

            }

            catch (Exception) { }

        }
        private void btnBackward_Click(object sender, EventArgs e)

        {

            try

            {

                ToolStripButton ToolStripButton = ((ToolStripButton)sender);



                //Determining the current page

                if (ToolStripButton == btnBackward)

                    ActivePage--;

                else if (ToolStripButton == btnForward)

                    ActivePage++;

                else if (ToolStripButton == btnLast)

                    ActivePage = PagesCounter;

                else if (ToolStripButton == btnFirst)

                    ActivePage = 1;

                else

                    ActivePage = Convert.ToInt32(ToolStripButton.Text, System.Globalization.CultureInfo.InvariantCulture);



                if (ActivePage < 1)

                    ActivePage = 1;

                else if (ActivePage > PagesCounter)

                    ActivePage = PagesCounter;



                //Rebind the Datagridview with the data.

                RebindGridForPageChange();



                //Change the pagiantions buttons according to page number

                RefreshPagination();

            }

            catch (Exception) { }

        }
        private void toolStripButton1_Click(object sender, EventArgs e)

        {

            try

            {

                ToolStripButton ToolStripButton = ((ToolStripButton)sender);



                //Determining the current page

                if (ToolStripButton == btnBackward)

                    ActivePage--;

                else if (ToolStripButton == btnForward)

                    ActivePage++;

                else if (ToolStripButton == btnLast)

                    ActivePage = PagesCounter;

                else if (ToolStripButton == btnFirst)

                    ActivePage = 1;

                else

                    ActivePage = Convert.ToInt32(ToolStripButton.Text, System.Globalization.CultureInfo.InvariantCulture);



                if (ActivePage < 1)

                    ActivePage = 1;

                else if (ActivePage > PagesCounter)

                    ActivePage = PagesCounter;



                //Rebind the Datagridview with the data.

                RebindGridForPageChange();



                //Change the pagiantions buttons according to page number

                RefreshPagination();



            }

            catch (Exception) { }

        }
        private void toolStripButton2_Click(object sender, EventArgs e)

        {

            try

            {

                ToolStripButton ToolStripButton = ((ToolStripButton)sender);



                //Determining the current page

                if (ToolStripButton == btnBackward)

                    ActivePage--;

                else if (ToolStripButton == btnForward)

                    ActivePage++;

                else if (ToolStripButton == btnLast)

                    ActivePage = PagesCounter;

                else if (ToolStripButton == btnFirst)

                    ActivePage = 1;

                else

                    ActivePage = Convert.ToInt32(ToolStripButton.Text, System.Globalization.CultureInfo.InvariantCulture);



                if (ActivePage < 1)

                    ActivePage = 1;

                else if (ActivePage > PagesCounter)

                    ActivePage = PagesCounter;



                //Rebind the Datagridview with the data.

                RebindGridForPageChange();



                //Change the pagiantions buttons according to page number

                RefreshPagination();

            }

            catch (Exception) { }

        }
        private void toolStripButton3_Click(object sender, EventArgs e)

        {

            try

            {

                ToolStripButton ToolStripButton = ((ToolStripButton)sender);



                //Determining the current page

                if (ToolStripButton == btnBackward)

                    ActivePage--;

                else if (ToolStripButton == btnForward)

                    ActivePage++;

                else if (ToolStripButton == btnLast)

                    ActivePage = PagesCounter;

                else if (ToolStripButton == btnFirst)

                    ActivePage = 1;

                else

                    ActivePage = Convert.ToInt32(ToolStripButton.Text, System.Globalization.CultureInfo.InvariantCulture);



                if (ActivePage < 1)

                    ActivePage = 1;

                else if (ActivePage > PagesCounter)

                    ActivePage = PagesCounter;



                //Rebind the Datagridview with the data.

                RebindGridForPageChange();



                //Change the pagiantions buttons according to page number

                RefreshPagination();

            }

            catch (Exception) { }

        }
        private void toolStripButton4_Click(object sender, EventArgs e)

        {

            try

            {

                ToolStripButton ToolStripButton = ((ToolStripButton)sender);



                //Determining the current page

                if (ToolStripButton == btnBackward)

                    ActivePage--;

                else if (ToolStripButton == btnForward)

                    ActivePage++;

                else if (ToolStripButton == btnLast)

                    ActivePage = PagesCounter;

                else if (ToolStripButton == btnFirst)

                    ActivePage = 1;

                else

                    ActivePage = Convert.ToInt32(ToolStripButton.Text, System.Globalization.CultureInfo.InvariantCulture);



                if (ActivePage < 1)

                    ActivePage = 1;

                else if (ActivePage > PagesCounter)

                    ActivePage = PagesCounter;



                //Rebind the Datagridview with the data.

                RebindGridForPageChange();



                //Change the pagiantions buttons according to page number

                RefreshPagination();

            }

            catch (Exception) { }

        }
        private void toolStripButton5_Click(object sender, EventArgs e)

        {

            try

            {

                ToolStripButton ToolStripButton = ((ToolStripButton)sender);



                //Determining the current page

                if (ToolStripButton == btnBackward)

                    ActivePage--;

                else if (ToolStripButton == btnForward)

                    ActivePage++;

                else if (ToolStripButton == btnLast)

                    ActivePage = PagesCounter;

                else if (ToolStripButton == btnFirst)

                    ActivePage = 1;

                else

                    ActivePage = Convert.ToInt32(ToolStripButton.Text, System.Globalization.CultureInfo.InvariantCulture);



                if (ActivePage < 1)

                    ActivePage = 1;

                else if (ActivePage > PagesCounter)

                    ActivePage = PagesCounter;



                //Rebind the Datagridview with the data.

                RebindGridForPageChange();



                //Change the pagiantions buttons according to page number

                RefreshPagination();

            }

            catch (Exception) { }

        }
        private void btnForward_Click(object sender, EventArgs e)

        {

            try

            {

                ToolStripButton ToolStripButton = ((ToolStripButton)sender);



                //Determining the current page

                if (ToolStripButton == btnBackward)

                    ActivePage--;

                else if (ToolStripButton == btnForward)

                    ActivePage++;

                else if (ToolStripButton == btnLast)

                    ActivePage = PagesCounter;

                else if (ToolStripButton == btnFirst)

                    ActivePage = 1;

                else

                    ActivePage = Convert.ToInt32(ToolStripButton.Text, System.Globalization.CultureInfo.InvariantCulture);



                if (ActivePage < 1)

                    ActivePage = 1;

                else if (ActivePage > PagesCounter)

                    ActivePage = PagesCounter;



                //Rebind the Datagridview with the data.

                RebindGridForPageChange();



                //Change the pagiantions buttons according to page number

                RefreshPagination();

            }

            catch (Exception) { }

        }
        private void btnLast_Click(object sender, EventArgs e)

        {

            try

            {

                ToolStripButton ToolStripButton = ((ToolStripButton)sender);

                //Determining the current page

                if (ToolStripButton == btnBackward)

                    ActivePage--;

                else if (ToolStripButton == btnForward)

                    ActivePage++;

                else if (ToolStripButton == btnLast)

                    ActivePage = PagesCounter;

                else if (ToolStripButton == btnFirst)

                    ActivePage = 1;

                else

                    ActivePage = Convert.ToInt32(ToolStripButton.Text, System.Globalization.CultureInfo.InvariantCulture);



                if (ActivePage < 1)

                    ActivePage = 1;

                else if (ActivePage > PagesCounter)

                    ActivePage = PagesCounter;



                //Rebind the Datagridview with the data.

                RebindGridForPageChange();



                //Change the pagiantions buttons according to page number

                RefreshPagination();

            }

            catch (Exception) { }

        }
        private void btnFirst_Click_1(object sender, EventArgs e)

        {

            try

            {

                ToolStripButton ToolStripButton = ((ToolStripButton)sender);



                //Determining the current page

                if (ToolStripButton == btnBackward)

                    ActivePage--;

                else if (ToolStripButton == btnForward)

                    ActivePage++;

                else if (ToolStripButton == btnLast)

                    ActivePage = PagesCounter;

                else if (ToolStripButton == btnFirst)

                    ActivePage = 1;

                else

                    ActivePage = Convert.ToInt32(ToolStripButton.Text, System.Globalization.CultureInfo.InvariantCulture);



                if (ActivePage < 1)

                    ActivePage = 1;

                else if (ActivePage > PagesCounter)

                    ActivePage = PagesCounter;



                //Rebind the Datagridview with the data.

                RebindGridForPageChange();



                //Change the pagiantions buttons according to page number

                RefreshPagination();

            }

            catch (Exception) { }

        }
        #endregion
        #region DataBinding
        class listData
        {
            public listData(string stuName, string stuClass)
            {
                this.stuName = stuName;
                this.stuClass = stuClass;
            }
            private string stuName;
            public string StudName
            {
                get { return stuName; }
                set { stuName = value; }
            }
            private string stuClass;
            public string StuClass
            {
                get { return stuClass; }
                set { stuClass = value; }
            }

        }
        #endregion
        #endregion




        #region Paging
        int pageindex = 1;
        int pageSize = 12;

        public void loadGridData(int index)
        {
            try
            {
                List<Object> obj = (List<object>)new BLogic().searchProfile("", "SClient", "", index, pageSize);
                dataGridView1.DataSource = (DataTable)obj[1];
                this.PopulatePager((int)obj[0], index);
            }
            catch (Exception ex)
            {

            }
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan = 3;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(pageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 3;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "First", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString() });
            }

            //Clear existing Pager Buttons.
            pnlPager.Controls.Clear();

            //Loop and add Buttons for Pager.
            int count = 0;
            foreach (Page page in pages)
            {
                Button btnPage = new Button();
                btnPage.Location = new System.Drawing.Point(38 * count, 5);
                btnPage.Size = new System.Drawing.Size(35, 20);
                btnPage.Name = page.Value;
                btnPage.Text = page.Text;
                btnPage.Enabled = !page.Selected;
                btnPage.Click += new System.EventHandler(this.Page_Click);
                pnlPager.Controls.Add(btnPage);
                count++;
            }



        }

        private void Page_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);
            loadGridData(int.Parse(btnPager.Name));
        }

        public class Page
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }
        }
        #endregion

    }


}
