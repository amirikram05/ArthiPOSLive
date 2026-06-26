using ArthiPOS.Controls.dashboard;
using ArthiPOS.utill;
using BAL;
using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ArthiPOS.Controls.test
{
    public partial class DailySales : Form
    {
        private BLogic bal;
        public DailySales(string date)
        {
            InitializeComponent();
            this.date = date;
            init();
        }
        public void init()
        {
            bal = new BLogic();
            comb_list.SelectedIndex = 0;
            chk_sort.Checked = false;
            txt_hide_col.Text = "3,5,11,15";
            UpdateGridVisibility();
            readSales(7, chk_sort.Checked ? 1 : 0, date, date, date);
        }
        private int getCheck()
        {
            int tcheck = 0;
            if (comb_list.SelectedIndex == 0) { tcheck = 7; }//All  
            else if (!chk_date.Checked && comb_list.SelectedIndex == 1 && chk_paid_un.CheckState == CheckState.Indeterminate) { tcheck = 1; }//ALL Sales specific date
            else if (!chk_date.Checked && comb_list.SelectedIndex == 2) { tcheck = 2; }//Paid Sales
            else if (!chk_date.Checked && comb_list.SelectedIndex == 3) { tcheck = 3; }// UnPaid
            else if (chk_date.Checked == true && comb_list.SelectedIndex == 1) { if (chk_paid_un.CheckState == CheckState.Checked) tcheck = 4; else if (chk_paid_un.CheckState == CheckState.Unchecked || chk_paid_un.CheckState == CheckState.Indeterminate) tcheck = 5; }// All on Specific Date and UnPaid
            else if (chk_date.Checked == true && comb_list.SelectedIndex == 2) { tcheck = 5; }//All on Specific Date and Paid
            else if (chk_date.Checked == true && comb_list.SelectedIndex == 3) { tcheck = 6; }//All on Specific Date 
            else if (comb_list.SelectedIndex == 4) { tcheck = 8; }
            else if (comb_list.SelectedIndex == 5) { tcheck = 9; }

            



            return tcheck;
        }


        DataTable dt;
        public void readSales(int tcheck, int sort, string sdate, string ldate, string date)
        {

            int selectedindex = GetPaidFilter();
            int ispaid = chk_paid_un.Checked ? 1 : 0;
            string uid = string.IsNullOrEmpty(label1.Text) ? "" : label1.Text;
            if (!chk_date.Checked) sdate = "";ldate = "";
            dt = new BLogic().readShopSales(uid, sdate, ldate, sdate, chk_sort.Checked ? 1 : 0, ispaid, -1, selectedindex);

            if (selectedindex == 8)
            {
                DataRow row = dt.Rows[0];
                if (row[0] == "") return;
                int quantity = int.Parse(row[0].ToString() == "" ? "0" : row[0].ToString());
                int total = int.Parse(row[1].ToString() == "" ? "0" : row[1].ToString());
                _lbl_total.Text = "Total     Quantity = " + quantity;
                lbl_total.Text = "" + total;

                //Total Calculate
            }
            else
            {
                ds_result.DataSource = dt;

                FormatDataGridView(ds_result);
            }
            txt_hide_col_KeyDown(this, new KeyEventArgs(Keys.Enter));
        }
        private void FormatDataGridView(DataGridView dgv)
        {
            if (dgv == null) return;
            if (dgv.Columns.Count == 0) return;

            // ✳️ Base visual setup
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = true;

            dgv.DefaultCellStyle.Font = new Font("Jameel Noori Nastaleeq", 11);
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.DefaultCellStyle.Padding = new Padding(4);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.Gainsboro;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 35;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // ❌ REMOVE this — it's breaking your width settings
            // dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ✅ Instead use "DisplayedCells" so width is respected but adjusts if content is wider
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            try
            {
                // ✅ Freeze first two columns
                if (dgv.Columns.Contains("No"))
                {
                    dgv.Columns["No"].Frozen = true;
                    dgv.Columns["No"].Width = 50;
                    dgv.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgv.Columns.Contains("Date"))
                {
                    dgv.Columns["Date"].Frozen = true;
                    dgv.Columns["Date"].Width = 90;
                    dgv.Columns["Date"].DefaultCellStyle.Font = new Font("Segoe UI", 8);
                    dgv.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns["Date"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }

                if (dgv.Columns.Contains("EndDate"))
                {
                    dgv.Columns["EndDate"].Width = 90;
                    dgv.Columns["EndDate"].DefaultCellStyle.Font = new Font("Segoe UI", 8);
                    dgv.Columns["EndDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgv.Columns.Contains("Uid"))
                {
                    dgv.Columns["Uid"].Width = 60;
                    dgv.Columns["Uid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgv.Columns.Contains("Name"))
                {
                    dgv.Columns["Name"].Width = 170;
                    dgv.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgv.Columns["Name"].DefaultCellStyle.Font = new Font("Segoe UI", 10);
                    dgv.Columns["Name"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dgv.Columns["Name"].DefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
                }

                if (dgv.Columns.Contains("Quantity")) dgv.Columns["Quantity"].Width = 60;
                if (dgv.Columns.Contains("Rate")) dgv.Columns["Rate"].Width = 60;
                if (dgv.Columns.Contains("Product"))
                {
                    dgv.Columns["Product"].Width = 80;
                    dgv.Columns["Product"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dgv.Columns["Product"].DefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
                }
                if (dgv.Columns.Contains("Size")) dgv.Columns["Size"].Width = 60;

                if (dgv.Columns.Contains("Total"))
                {
                    dgv.Columns["Total"].Width = 80;
                    dgv.Columns["Total"].DefaultCellStyle.ForeColor = Color.Green;
                    dgv.Columns["Total"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dgv.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dgv.Columns.Contains("IsPaid"))
                {
                    dgv.Columns["IsPaid"].Width = 70;
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.Cells["IsPaid"].Value?.ToString() == "Paid")
                            row.Cells["IsPaid"].Style.ForeColor = Color.Green;
                        else
                            row.Cells["IsPaid"].Style.ForeColor = Color.Red;
                    }
                }

                if (dgv.Columns.Contains("Labour"))
                {
                    dgv.Columns["Labour"].Width = 80;
                    dgv.Columns["Labour"].DefaultCellStyle.ForeColor = Color.Red;
                    dgv.Columns["Labour"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dgv.Columns["Labour"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dgv.Columns.Contains("ProductID"))
                    dgv.Columns["ProductID"].Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Format error: " + ex.Message);
            }
        }


        
       

        string date;

        private void btn_add_Click(object sender, EventArgs e)
        {

            date = date_start.Text;

        }
        Search search;
        public void searchDialog( int action, string searchTxt)
        {


            using (search = new Search(action, searchTxt, 1))
            {


                DialogResult res = search.ShowDialog();
                if (action == 1)
                {
                    
                        txt_name.Text = search.Name;
                        label1.Text = search.Id;
                    
                }
                

                search.Close();



                return;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {

                case Keys.Control | Keys.A:
                    if (this.ActiveControl is TextBox)
                    {

                    }else
                        btn_sale.PerformClick();
                    return true;
                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.F5:
                    init();
                    return true;
                case Keys.Control | Keys.I:
                    searchDialog( 1,txt_name.Text);
                    return true;
                case Keys.Control | Keys.Down:
                    // Move focus to the first cell of the first row in the second DataGridView
                    ds_result.Focus();
                    ds_result.CurrentCell = ds_result.Rows[0].Cells[2];
                    ds_result.BeginEdit(true);
                    return true;
                case Keys.Control | Keys.H:
                    txt_hide_col.Focus();
                    return true;
                case Keys.Control | Keys.S:
                    btn_search.PerformClick();
                    return true;
                case Keys.Enter:
                    if (this.ActiveControl is TextBox)
                    {
                        SendKeys.Send("{TAB}");
                        return true;
                    }
                    if (txt_name.ContainsFocus)
                    {
                        searchDialog(1, txt_name.Text);
                        return true;
                    }
                    break;

            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void UpdateGridVisibility()
        {
            if (ds_result == null || ds_result.Columns.Count == 0)
                return;

            // Read textbox and split into column numbers
            string[] input = txt_hide_col.Text.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Convert to integer list safely
            var hiddenCols = new List<int>();
            foreach (string val in input)
            {
                if (int.TryParse(val, out int colIndex))
                    hiddenCols.Add(colIndex);
            }

            // Update visibility for all columns
            for (int i = 0; i < ds_result.Columns.Count; i++)
            {
                // Column indexes are 0-based internally, user may enter 1-based numbers
                ds_result.Columns[i].Visible = !hiddenCols.Contains(i + 1);
            }
        }
        public int getTotal(string input)
        {
            // Split the input by commas to get each quantity-rate pair
            string[] pairsC = input.Split(',');
            int total = 0;
            foreach (string pairc in pairsC)
            {
                string[] parts = pairc.Split('=');

                if (parts.Length == 2)
                {
                    string quantityPart = parts[0].Trim();
                    string ratePart = parts[1].Trim();
                    int quantity = 0;
                    int rate = 0;
                    // Try to parse the parts into integers
                    if (int.TryParse(quantityPart, out quantity) && int.TryParse(ratePart, out rate))
                    {
                        // Now you have the quantity and rate per item
                        total += rate * quantity;
                    }
                    else
                    {
                        Console.WriteLine("Invalid format: Unable to parse quantity or rate.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format: Input string does not contain quantity and rate separated by '='.");

                }
            }
            return total;
        }
      

        private void txt_name_TextChanged(object sender, EventArgs e)
        {
        }
        int sort = 1;
        private void chk_sort_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_sort.Checked)
                sort = 1;
            else
                sort = 2;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            readSales(getCheck(), chk_sort.Checked ? 1 : 0, date_start.Text, date_last.Text, date_start.Text);
        }

        private void chk_date_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_date.Checked) date_panel.Enabled = true;
            else date_panel.Enabled = false;
        }
       
        private void ds_result_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (comb_list.SelectedIndex != 5)
            {
                int indexC = e.ColumnIndex;
                int indexR = e.RowIndex;
                if (indexC == 0)//Update
                {
                    string id = ds_result.Rows[indexR].Cells[2].FormattedValue.ToString();
                    string tdate = ds_result.Rows[indexR].Cells[3].FormattedValue.ToString();
                    string enddate = ds_result.Rows[indexR].Cells[4].FormattedValue.ToString();
                    string uid = ds_result.Rows[indexR].Cells[5].FormattedValue.ToString();
                    string uname = ds_result.Rows[indexR].Cells[6].FormattedValue.ToString();
                    string quantity = ds_result.Rows[indexR].Cells[7].FormattedValue.ToString();
                    string rate = ds_result.Rows[indexR].Cells[8].FormattedValue.ToString();
                    string product = ds_result.Rows[indexR].Cells[9].FormattedValue.ToString();
                    string size = ds_result.Rows[indexR].Cells[10].FormattedValue.ToString();
                    string total = ds_result.Rows[indexR].Cells[11].FormattedValue.ToString();
                    string labour = ds_result.Rows[indexR].Cells[13].FormattedValue.ToString();
                    string productid = ds_result.Rows[indexR].Cells[14].FormattedValue.ToString();
                    string remarks = ds_result.Rows[indexR].Cells[10].FormattedValue.ToString();
                    List<object> obj = (List<object>)new BLogic().shopCrud_InsertUpdate("U", date_start.Text, date_last.Text, uname, uid, "" + quantity,  rate, "", product, tdate, "" + total, "" + 1, 0, id == "" ? 0 : int.Parse(id), labour == "" ? 0 : int.Parse(labour), productid, remarks);
                    if (obj == null)
                    {
                        MessageBox.Show("Not Save");
                        return;
                    }
                }
                else if (indexC == 1)//Delete
                {
                    string id = ds_result.Rows[indexR].Cells["No"].FormattedValue.ToString();
                    var result = MessageBox.Show("Are you sure you want to delete this item?",
                                         "Confirm Delete",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        // Perform delete operation here
                        List<object> obj = (List<object>)bal.shopCrud_InsertUpdate("D", "", "", "", "", "", "", "", "", "", "", "" + 0, 0, id == "" ? 0 : int.Parse(id), 0, "0","");
                        if (obj == null)
                        {
                            MessageBox.Show("Not Delete");
                            return;
                        }
                        MessageBox.Show("Item deleted.");
                        readSales(7, chk_sort.Checked ? 1 : 0, date_start.Text, date_start.Text, date_last.Text);

                    }


                }
            }
        }

        private void btn_add_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_name.Text)) { MessageBox.Show("Select User.."); return; }
            List<object> obj = (List<object>)bal.shopCrud_InsertUpdate("U", date_start.Text, date_last.Text, "", label1.Text, "", "", "", "", "", "", chk_paid_un.Checked?"1":"0", 0, -1, 0, "0","");
            if (obj == null)
            {
                MessageBox.Show("Not Save");
                return;
            }
            readSales(1, chk_sort.Checked ? 1 : 0, date, date, date);

        }
        private int isPaid = 0;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isPaid == 0)
            {
                isPaid = 1;
                chk_paid_un.CheckState = CheckState.Checked;
            }
            else if (isPaid == 1)
            {
                isPaid = 2;
                chk_paid_un.CheckState = CheckState.Indeterminate;

            }
            else if (isPaid == 2)
            {
                isPaid = 0;
                chk_paid_un.CheckState = CheckState.Unchecked;
            }

        }

        private void btn_sale_Click(object sender, EventArgs e)
        {
            Shopmaal shop = new Shopmaal();
            var result = shop.ShowDialog(); // show modal dialog

            // When dialog closes, refresh sales
            if (shop.IsSaved)
            {
                readSales(7, chk_sort.Checked ? 1 : 0, date_start.Text, date_last.Text, date);
            }
        }

        private void txt_hide_col_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep sound

                UpdateGridVisibility();
            }
        }
        private int GetPaidFilter()
        {
            switch (comb_list.Text)
            {
                case "ALL":
                    return 1;
                case "Paid":
                    return 2;
                case "UnPaid":
                    return 3;
                case "Save":
                    return 4;
                case "Show Total":
                    return 8;
                default:
                    return 1; // None / default
            }
        }

        private string GenerateHTMLReportUrdu(DataTable dt, string reportDate)
        {
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

            string html = $@"<!doctype html>
<html lang='ur' dir='rtl'>
<head>
<meta charset='utf-8'>
<title>دکان مال رپورٹ</title>
<style>
  @import url('https://fonts.googleapis.com/css2?family=Noto+Nastaliq+Urdu&display=swap');

  body {{
    font-family: 'Noto Nastaliq Urdu', serif;
    direction: rtl;
    margin: 40px;
    background-color: #fff;
  }}

  .controls {{
    margin-bottom: 12px;
    text-align: right;
  }}

  button {{
    font-family: 'Noto Nastaliq Urdu', serif;
    font-size: 18px;
    padding: 4px 10px;
    margin-left: 4px;
    cursor: pointer;
  }}

  table {{
    width: 100%;
    border-collapse: collapse;
    margin-top: 8px;
  }}

  th, td {{
    border: 1px solid #ccc;
    padding: 6px 4px;
    text-align: right;
    vertical-align: middle;
    word-wrap: break-word;
  }}

  th {{
    background-color: #f2f2f2;
    font-weight: bold;
    font-size: 18px;
  }}

  td {{
    font-size: 16px;
  }}

  .header {{
    text-align: center;
    margin-bottom: 8px;
  }}

  .total-cell {{
    color: green;
    font-weight: bold;
  }}

  .labour-cell {{
    color: red;
    font-weight: bold;
  }}

  .highlight-row:hover {{
    background-color: #f1f1f1;
  }}

  .group-header {{
    background: #e8eef7;
    font-weight: bold;
  }}

  @media print {{
    .controls {{ display: none !important; }}
    html, body {{
      margin: 0;
      padding: 0;
      -webkit-print-color-adjust: exact;
      print-color-adjust: exact;
    }}
  }}
</style>
</head>
<body>

<div class='controls'>
  <button onclick='updateData()'>🔄 اپڈیٹ</button>
  <button onclick='window.print()'>🖨️ پرنٹ</button>

  <label style=""margin-right:10px;"">گروپ بنائیں:</label>
  <select id=""groupField""></select>
</div>

<div class='header'>
  <h2>دکان مال رپورٹ</h2>
  <div>تاریخ: {{reportDate}}</div>
</div>

<table id='reportTable'>
  <thead>
    <tr>
      <th>نمبر</th>
      <th>تاریخ</th>
      <th>آخری تاریخ</th>
      <th>نام</th>
      <th>تعداد</th>
      <th>ریٹ</th>
      <th>پراڈکٹ</th>
      <th>سائز</th>
      <th>کل</th>
      <th>لیبر</th>
      <th>ادائیگی</th>
    </tr>
  </thead>
  <tbody></tbody>
</table>

<script>
  const data = {jsonData};

  function isNumeric(v) {{
    return !isNaN(parseFloat(v)) && isFinite(v);
  }}

  function groupBy(arr, key) {{
    return arr.reduce((acc, row) => {{
      const g = row[key] || ""نامعلوم"";
      (acc[g] = acc[g] || []).push(row);
      return acc;
    }}, {{}});
  }}

  function computeSubtotal(rows) {{
    return {{
      Quantity: rows.reduce((s, r) => s + (parseFloat(r.Quantity) || 0), 0),
      Total: rows.reduce((s, r) => s + (parseFloat(r.Total) || 0), 0),
      Labour: rows.reduce((s, r) => s + (parseFloat(r.Labour) || 0), 0)
    }};
  }}

  // ====== GROUP DROPDOWN ======
  const groupSelect = document.getElementById(""groupField"");
  const groupOptions = [""Shop All"", ""Name"", ""Product"", ""Date"", ""EndDate"", ""Size""];

  groupOptions.forEach(f => {{
    const opt = document.createElement(""option"");
    opt.value = f;
    opt.textContent = f;
    groupSelect.appendChild(opt);
  }});

  groupSelect.value = ""Name"";
  groupSelect.addEventListener(""change"", renderTable);

  // ====== RENDER TABLE ======
  function renderTable() {{
    const tbody = document.querySelector('#reportTable tbody');
    tbody.innerHTML = '';

    const groupKey = groupSelect.value;
    const isShopAll = (groupKey === ""Shop All"");

    let grouped = isShopAll
      ? {{ ""ALL"": data }}
      : groupBy(data, groupKey);

    let grandTotal = 0, grandLabour = 0, grandQty = 0;
    let counter = 1;

    for (const group in grouped) {{
      const rows = grouped[group];
      const sub = computeSubtotal(rows);

      // GROUP HEADER (skip for Shop All)
      if (!isShopAll) {{
        const gh = document.createElement('tr');
        gh.className = ""group-header"";
        gh.innerHTML = `
          <td colspan=""11"">${{groupKey}}: ${{group}}</td>
        `;
        tbody.appendChild(gh);
      }}

      // DETAIL ROWS
      rows.forEach(row => {{
        const tr = document.createElement('tr');
        tr.className = 'highlight-row';

        tr.innerHTML = `
          <td>${{counter++}}</td>
          <td>${{row.Date || ''}}</td>
          <td>${{row.EndDate || ''}}</td>
          <td>${{row.Name || ''}}</td>
          <td>${{row.Quantity || ''}}</td>
          <td>${{row.Rate || ''}}</td>
          <td>${{row.Product || ''}}</td>
          <td>${{row.Size || ''}}</td>
          <td class='total-cell'>${{row.Total || 0}}</td>
          <td class='labour-cell'>${{row.Labour || 0}}</td>
          <td>${{row.IsPaid || ''}}</td>
        `;
        tbody.appendChild(tr);
      }});

      // SUBTOTAL (skip for Shop All)
      if (!isShopAll) {{
        const st = document.createElement('tr');
        st.style.background = ""#f5f5f5"";
        st.style.fontWeight = ""bold"";
        st.innerHTML = `
          <td colspan=""4"">مجموعہ (${{group}})</td>
          <td>${{sub.Quantity}}</td>
          <td></td>
          <td></td>
          <td></td>
          <td class='total-cell'>${{sub.Total}}</td>
          <td class='labour-cell'>${{sub.Labour}}</td>
          <td></td>
        `;
        tbody.appendChild(st);
      }}

      grandQty += sub.Quantity;
      grandTotal += sub.Total;
      grandLabour += sub.Labour;
    }}

    // GRAND TOTAL (ALWAYS)
    const gt = document.createElement('tr');
    gt.style.background = ""#d9d9d9"";
    gt.style.fontWeight = ""bold"";
    gt.innerHTML = `
      <td colspan=""4"">کل مجموعی</td>
      <td>${{grandQty}}</td>
      <td></td>
      <td></td>
      <td></td>
      <td class='total-cell'>${{grandTotal}}</td>
      <td class='labour-cell'>${{grandLabour}}</td>
      <td></td>
    `;
    tbody.appendChild(gt);
  }}

  function updateData() {{
    alert('🔄 اپڈیٹ مکمل ہو گیا!');
    renderTable();
  }}

  renderTable();
</script>

</body>
</html>
";

            return html;
        }
        
        private void bt_browse_print_Click(object sender, EventArgs e)
        {
            string header= $"دکان مال رپورٹ\n "+date;
            string htmlReport = CommonUtill.GenerateHTMLReportUrdu(dt, date);
            // Output the HTML to a file
            string filePath = @"report.html";
            File.WriteAllText(filePath, htmlReport);

            // Open the HTML report in the default web browser
            System.Diagnostics.Process.Start(filePath);
            Console.WriteLine(htmlReport);
        }
    }
}
