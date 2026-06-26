using ArthiPOS.Properties;
using ArthiPOS.Reporting.ReportView;
using ArthiPOS.utill;
using BAL;
using CommonUtilities;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DataMember.memberlog;
using DevExpress.XtraTab;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ArthiPOS.Reporting
{
    public partial class RepAugraiNewF : Form
    {
        public RepAugraiNewF()
        {
            InitializeComponent();
            //chk_printall.Checked = false;
            //rd_check.SelectedIndex = 0;
            PopulateYearComboBox();

        }
        public RepAugraiNewF(DataTable custAugrai)
        {
            InitializeComponent();
            //printReport(custAugrai);
            //chk_printall.Checked = false;
            //rd_check.SelectedIndex = 0;
            PopulateYearComboBox();
            updateGroup();

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
            cr = new TestAugraiCR();
            //if (cr != null)
            //{
            //    FrmPageSetup dlg = new FrmPageSetup();

            //    if (dlg.ShowDialog() == DialogResult.OK)
            //    {
            //        cr.PrintOptions.PaperSize = dlg.SelectedPaperSize;
            //        cr.PrintOptions.PaperOrientation = dlg.SelectedOrientation;
            //    }
            //}


            // Access the TextObject in the report
            TextObject title = cr.Section1.ReportObjects["Text1"] as TextObject; // Replace with actual section and TextObject name

            if (rb_client.Checked)
            {

                TextObject pre = cr.Section1.ReportObjects["Text6"] as TextObject; // Replace with actual section and TextObject name
                TextObject bill = cr.Section1.ReportObjects["Text11"] as TextObject; // Replace with actual section and TextObject name
                TextObject receive = cr.Section1.ReportObjects["Text12"] as TextObject; // Replace with actual section and TextObject name
                TextObject remaining = cr.Section1.ReportObjects["Text10"] as TextObject; // Replace with actual section and TextObject name
                TextObject tpre = cr.Section1.ReportObjects["Text7"] as TextObject; // Replace with actual section and TextObject name
                TextObject trec = cr.Section1.ReportObjects["Text14"] as TextObject; // Replace with actual section and TextObject name
                TextObject tbill = cr.Section1.ReportObjects["Text13"] as TextObject; // Replace with actual section and TextObject name
                TextObject ttotal = cr.Section1.ReportObjects["Text16"] as TextObject; // Replace with actual section and TextObject name


                if (chk_saleadvance.Checked)
                {
                    title.Text = Resources.ResourceManager.GetString("rt3");
                    pre.Text = Resources.ResourceManager.GetString("r1");
                    bill.Text = Resources.ResourceManager.GetString("r2");
                    receive.Text = Resources.ResourceManager.GetString("r3");
                    remaining.Text = Resources.ResourceManager.GetString("r4");

                    tpre.Text = Resources.ResourceManager.GetString("1053") + " " + Resources.ResourceManager.GetString("r1");
                    tbill.Text = Resources.ResourceManager.GetString("1053") + " " + Resources.ResourceManager.GetString("r2");
                    ttotal.Text = Resources.ResourceManager.GetString("1053") + " " + Resources.ResourceManager.GetString("r4");
                }
                else
                {
                    title.Text = Resources.ResourceManager.GetString("rt2");
                    pre.Text = Resources.ResourceManager.GetString("r11");
                    bill.Text = Resources.ResourceManager.GetString("r22");
                    receive.Text = Resources.ResourceManager.GetString("r3");
                    remaining.Text = Resources.ResourceManager.GetString("r44");

                    tpre.Text = Resources.ResourceManager.GetString("1053") + " " + Resources.ResourceManager.GetString("r11");
                    tbill.Text = Resources.ResourceManager.GetString("1053") + " " + Resources.ResourceManager.GetString("r22");
                    ttotal.Text = Resources.ResourceManager.GetString("1053") + " " + Resources.ResourceManager.GetString("r44");
                }
            }
            else
            {
                title.Text = Resources.ResourceManager.GetString("rt1");

            }

            cr.Database.Tables["CustAugrai"].SetDataSource(custAugrai);
            DataTable wm = new DataTable();
            //wm.Columns.Add("waterpath", typeof(string));
            //string startupPath = Environment.CurrentDirectory;
            //wm.Rows.Add(@startupPath + "\\watermark.jpg");
            //cr.Database.Tables["Watermark"].SetDataSource(wm);
            cr.SetParameterValue("logo", Authentication.Account.trade_mark);
            cr.SetParameterValue("def_to", def_to.Text);
            cr.SetParameterValue("def_from", def_from.Text);


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
        private void PopulateYearComboBox()
        {
            /*int currentYear = date_start.Value.Year;//DateTime.Now.Year;
            for (int i = 0; i < 10; i++)
            {
                def_list.Items.Add((currentYear - i).ToString());
            }*/
            //def_list.SelectedIndex = 0; // Set default selection
        }
        DataTable dt = null;
        private void rd_check_Click(object sender, EventArgs e)
        {

            if (rb_customer.Checked)
            {
                cust_panel.Visible = true;
                chk_saleadvance.Enabled = false;
                rb_client.Checked = false;
                //string defcount= int.Parse(def_from.Text) > 30 ? def_list.Text : 40+"";
                dt = new BLogic().p_augrai_read(!chk_printall.Checked ? "0" : "1", date_start.Text, def_to.Text);
                //dt = new BLogic().p_customer_CRUD("Augrai", !chk_printall.Checked ? "0" : "1", date);



                /*if (chk_printall.Checked)
                {
                    dt = new BLogic().p_augrai_read("1", date);

                }
                else
                {

                    dt = new BLogic().p_customer_CRUD("Augrai", "0", date);

                }*/
            }
            else
            if (rb_client.Checked)
            {
                cust_panel.Visible = false;
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
            updateGroup();
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

        string date = "";
        private void previousdate_Click(object sender, EventArgs e)
        {
            date_start.Value = CommonUtill.ChangeDate(date_start, -1);
            date = date_start.Text;
            rd_check_Click(this, new EventArgs());
        }

        private void nextdate_Click(object sender, EventArgs e)
        {
            date_start.Value = CommonUtill.ChangeDate(date_start, 1);
            date = date_start.Text;
            rd_check_Click(this, new EventArgs());

        }

        private void bt_browse_print_Click(object sender, EventArgs e)
        {

            string columns = "";
            int[] columnsToHide;
            if (string.IsNullOrWhiteSpace(columns))
            {
                // If input is empty, don't hide any columns
                columnsToHide = new int[0];
            }
            else
            {
                // Convert user input (column indexes) to an integer array
                columnsToHide = columns.Split(',')
                    .Select(int.Parse)
                    .ToArray();  // Parse the input if it's not empty
            }

            // Group by this column (optional)
            string groupByColumn = "address";   // or "t_date"

            // Days limit for defaulters (optional)
            int daysLimit = 30;

            // Generate the HTML
            //string htmlReport = GenerateHTMLReportUrdu(dt, daysLimit);
            string htmlReport = GenerateHTMLReportUrdu(dt, date, "");

            // Output the HTML to a file
            string filePath = @"report.html";
            File.WriteAllText(filePath, htmlReport);

            // Open the HTML report in the default web browser
            System.Diagnostics.Process.Start(filePath);
            Console.WriteLine(htmlReport);                         // debug output
        }


        private string GenerateHTMLReportUrdu(DataTable dt, string reportDate, string Header)
        {
            // Convert DataTable to JSON for embedding into HTML
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

            string html = $@"<!doctype html>
<html lang='ur' dir='rtl'>
<head>
<meta charset='utf-8'>
<title>Augrai Report</title>
<style>
  @import url('https://fonts.googleapis.com/css2?family=Noto+Nastaliq+Urdu&display=swap');
  @import url('https://fonts.googleapis.com/css2?family=Noto+Nastaliq+Urdu&display=swap');
  @import url('https://fonts.googleapis.com/css2?family=Noto+Naskh+Arabic&display=swap');
  @import url('https://fonts.googleapis.com/css2?family=Amiri&display=swap');
  body {{
    font-family: 'Noto Nastaliq Urdu', serif;
    direction: rtl;
    margin: 50px;
    padding: 0;
  }}

  .controls {{ 
    margin-bottom: 16px; 
    text-align: right;
  }}

  table {{ 
    width: 100%; 
    border-collapse: collapse; 
    margin-top: 8px; 
  }}

  th, td {{ 
    border: 1px solid #ccc; 
    padding: 1px; 
    text-align: right; 
    vertical-align: middle;
    word-wrap: break-word;
    font-family: 'Noto Nastaliq Urdu', serif;
    font-size: 16px;
  }}

  th {{ 
    background: #f2f2f2; 
    font-weight: bold; 
    font-size: 22px;
  }}
  td {{
	font-size: 20px;
  }}
  th:nth-child(2), td:nth-child(2) {{
    width: 25%;
  }}

  th, td {{
    width: auto;
  }}

  .group-header {{ 
    background: #e8eef7; 
    font-weight: bold; 
    padding: 4px; 
    margin-top: 10px; 
  }}

  .header {{ 
    text-align: center; 
    margin-bottom: 5px; 
  }}

  .defaulter-row {{
    background-color: #d9d9d9 !important;
  }}

  @media print {{
    html, body {{
      width: auto;
      height: 297mm;
      margin: 0 auto;
      padding: 0;
      zoom: 1;
      -webkit-print-color-adjust: exact;
      print-color-adjust: exact;
    }}

    .controls {{ display: none !important; }}
    transition: transform 0.3s ease, height 0.3s ease;
    body * {{ visibility: hidden; }}
    #printArea, #printArea * {{ visibility: visible; }}
    #printArea {{
      position: absolute;
      left: 0;
      top: 0;
      width: 100%;
    }}

    thead {{ display: table-header-group !important; }}
    tfoot {{ display: table-footer-group !important; }}

    th, td {{
      padding: 2px !important;
      line-height: 1.5 !important;
	  font-size:13px
    }}
  }}
</style>
</head>
<body>
<div class='controls'>

  <!-- 🆕 Font controls -->
  <label style=""margin-right:10px;"">فونٹ:</label>
  <select id=""fontSelect"" onchange=""changeFont()"">
    <option value=""'Noto Nastaliq Urdu', serif"">نوتو نستعلیق اردو</option>
    <option value=""'Noto Naskh Arabic', serif"">نوتو نسخ عربی</option>
    <option value=""'Amiri', serif"">امیری</option>
    <option value=""'Mehr Nastaliq Web', serif"">مہر نستعلیق (لوکل)</option>
  </select>

  <label>سائز:</label>
  <input type=""number"" id=""fontSizeInput"" value=""18"" min=""10"" max=""40"" style=""width:60px;"" oninput=""changeFont()"">
</div>
<div class='controls'>
  <label>گروپ بنائیں:</label>
  <select id='groupField'></select>
  <label>دن درج کریں:</label>
  <input type='number' id='daysInput' value='30' style='width:80px;'>
  <label>تلاش:</label>
  <input type=""text"" id=""searchInput"" placeholder=""نام، جگہ..."" oninput=""applyFilter()"">

  <label style=""margin-right:10px;"">دن:</label>
  
  <button onclick='applyFilter()'>تازہ کریں</button>
  <button onclick='window.print()'>پرنٹ کریں</button>
  <button onclick='saveFullHTML()'>محفوظ کریں</button>
</div>

<div id='printArea'></div>

<script>

const data =  {jsonData};
const reportDate = '{reportDate}';
let filteredData = []; // will hold current visible rows
let sortState = {{ field: null, asc: true }};

// Fill dropdown
const fields = Object.keys(data[0]);
const select = document.getElementById('groupField');
fields.forEach(f => {{
    const opt = document.createElement('option');
    opt.value = f;
    opt.textContent = f;
    select.appendChild(opt);
}});
select.value = ""Location"";

function groupBy(arr, key) {{
  return arr.reduce((acc, row) => {{
    const g = row[key] || ""N/A"";
    (acc[g] = acc[g] || []).push(row);
    return acc;
  }}, {{}});
}}
function isDate(v){{ return !isNaN(Date.parse(v)); }}
function isNumeric(v){{ return !isNaN(parseFloat(v)) && isFinite(v); }}

function computeSubtotal(rows){{
  const totals={{}};
  rows.forEach(r=>{{
    for (const k in r)
      if(isNumeric(r[k])) totals[k]=(totals[k]||0)+parseFloat(r[k]);
  }});
  return totals;
}}
function changeFont() {{
  const newFont = document.getElementById(""fontSelect"").value;
  const newSize = parseInt(document.getElementById(""fontSizeInput"").value);

  document.body.style.fontFamily = newFont;
  document.body.style.fontSize = newSize + ""px"";

  // Also apply to table cells and headers immediately
  document.querySelectorAll(""th, td"").forEach(el => {{
    el.style.fontFamily = newFont;
    el.style.fontSize = newSize + ""px"";
  }});
  document.querySelectorAll("".group-header, .subtotal, .grandtotal, .header"")
    .forEach(el => el.style.fontFamily = newFont);

}}
function sortTable(field){{
  if (sortState.field===field) sortState.asc=!sortState.asc;
  else {{ sortState.field=field; sortState.asc=true; }}

  filteredData.sort((a,b)=>{{
    let x=a[field], y=b[field];
    if(x==null) x=""""; if(y==null) y="""";
    if(isNumeric(x)&&isNumeric(y))
      return sortState.asc?x-y:y-x;
    return sortState.asc
      ? String(x).localeCompare(String(y),'ur')
      : String(y).localeCompare(String(x),'ur');
  }});

  renderTable(parseInt(document.getElementById('daysInput').value) || 0);
}}

function applyFilter(){{
   const days = parseInt(document.getElementById('daysInput').value) || 0;
  const search = document.getElementById('searchInput').value.trim();
  const searchLower = search.toLowerCase();
  const searchNum = !isNaN(search) && search !== """" ? Number(search) : null;

  filteredData = data.filter(r => {{
    // Basic text match logic
    const textMatch =
      (r.Name && r.Name.toLowerCase().includes(searchLower)) ||
      (r.Location && r.Location.toLowerCase().includes(searchLower));

    // Numeric match (for ID or numeric fields)
    const idMatch = searchNum !== null && (
      r.ID == searchNum ||
      (r.PreAmount && r.PreAmount == searchNum) ||
      (r.Amount && r.Amount == searchNum)
    );

    // If no search specified, show all — else only matches
    const matchSearch = !search || textMatch || idMatch;

    return matchSearch;
  }});

  renderTable(days);
}}

function renderTable(daysLimit){{
  const groupKey = select.value;
  const grouped = groupBy(filteredData, groupKey);

  const grandTotals = {{}};
  const dateGroups=[], numGroups=[], otherGroups=[];
  Object.keys(grouped).forEach(g=>{{
    if(isDate(g)) dateGroups.push(g);
    else if(isNumeric(g)) numGroups.push(g);
    else otherGroups.push(g);
  }});
  dateGroups.sort((a,b)=>new Date(a)-new Date(b));
  numGroups.sort((a,b)=>a-b);
  otherGroups.sort();
  const sortedGroups=[...dateGroups,...numGroups,...otherGroups];

  let html=`
  <div class='header'>
    <div style='display:flex; justify-content:space-between;'>
      <div>${reportDate} :تاریخ</div>
      <div style='font-weight:bold;'>اوگرائی رپورٹ</div>
      <div class='page-info'>صفحہ نمبر</div>
    </div>
  </div>

  <table>
    <thead>
      <tr>
        <th onclick=""sortTable('ID')"">نمبر</th>
        <th onclick=""sortTable('Name')"">نام</th>
        <th onclick=""sortTable('PreAmount')"">سابقہ بنام</th>
        <th onclick=""sortTable('LastBill')"">تازہ بنام</th>
        <th onclick=""sortTable('ReceiveAmount')"">وصولی</th>
        <th onclick=""sortTable('Amount')"">بقایا ‌بنام</th>
        <th>${reportDate}</th>
      </tr>
    </thead>
    <tbody>`;

  sortedGroups.forEach(group=>{{
    const rows=grouped[group];
    const subtotal=computeSubtotal(rows);
    for(const k in subtotal) grandTotals[k]=(grandTotals[k]||0)+subtotal[k];

    html += `<tr><td colspan='7' class='group-header'>شہر: ${{group}}</td></tr>`;

    rows.forEach(row=>{{
      const isDefaulter=row.lastPaymentDays>daysLimit;
      html += `
      <tr class='${{isDefaulter?""defaulter-row"":""""}}'>
        <td>${{row.ID}}</td>
        <td>${{row.Name}}</td>
        <td>${{row.PreAmount}}</td>
        <td>${{row.LastBill}}</td>
        <td>${{row.ReceiveAmount}}</td>
        <td>${{row.Amount}}</td>
        <td></td>
      </tr>`;
    }});

    html += `
    <tr style=""background:#f5f5f5; font-weight:bold;"">
      <td colspan=""2"">مجموعہ (${{group}})</td>
      <td>${{subtotal.PreAmount||""""}}</td>
      <td>${{subtotal.LastBill||""""}}</td>
      <td>${{subtotal.ReceiveAmount||""""}}</td>
      <td>${{subtotal.Amount||""""}}</td>
      <td></td>
    </tr>`;
  }});

  html += `
  <tr style=""background:#d9d9d9; font-weight:bold;"">
    <td colspan=""2"">کل مجموعی</td>
    <td>${{grandTotals.PreAmount||""""}}</td>
    <td>${{grandTotals.LastBill||""""}}</td>
    <td>${{grandTotals.ReceiveAmount||""""}}</td>
    <td>${{grandTotals.Amount||""""}}</td>
    <td></td>
  </tr>`;

  html += `</tbody></table>`;
  document.getElementById(""printArea"").innerHTML=html;
}}

// initial render
filteredData = [...data];
applyFilter();
// Save HTML - triggers WinForms C# event
function saveFullHTML() {{{{
    window.external.SaveHTML(); 
}}}}
</script>

</body>
</html>";

            return html;
        }
        private string GenerateHTMLReportUrdu(DataTable dt, string reportDate)
        {
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

            string html = $@"<!doctype html>
<html lang='ur' dir='rtl'>
<head>
<meta charset='utf-8'>
<title>اوگرائی رپورٹ</title>
<style>
  @import url('https://fonts.googleapis.com/css2?family=Noto+Nastaliq+Urdu&display=swap');

  body {{
    font-family: 'Noto Nastaliq Urdu', serif;
    direction: rtl;
    margin: 40px;
    padding: 0;
  }}

  .controls {{ 
    margin-bottom: 16px; 
    text-align: right;
  }}

  table {{ 
    width: 100%; 
    border-collapse: collapse; 
    margin-top: 8px; 
  }}

  th, td {{ 
    border: 1px solid #ccc; 
    padding: 4px; 
    text-align: right; 
    vertical-align: middle;
    word-wrap: break-word;
  }}

  th {{ 
    background: #f2f2f2; 
    font-weight: bold; 
    font-size: 20px;
  }}

  td {{
    font-size: 18px;
  }}

  .group-header {{ 
    background: #e8eef7; 
    font-weight: bold; 
    text-align: right; 
  }}

  .group-total {{
    background: #f9f9f9; 
    font-weight: bold; 
    text-align: right;
  }}

  .summary-total {{
    background: #d9f0d9;
    font-weight: bold;
    text-align: right;
  }}

  .defaulter-row {{
    background-color: #f7d9d9 !important;
  }}

  @media print {{
    .controls {{ display: none !important; }}
    html, body {{
      width: auto;
      height: 297mm;
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
  <label>گروپ بنائیں:</label>
  <select id='groupField'></select>
  <label>دن کی حد:</label>
  <input type='number' id='daysInput' value='30' style='width:80px;'>
  <button onclick='applyFilter()'>تازہ کریں</button>
  <button onclick='window.print()'>پرنٹ کریں</button>
</div>

<div id='printArea'></div>

<script>
  const data = {jsonData};
  const reportDate = '{reportDate}';

  // Fill group dropdown dynamically
  const fields = Object.keys(data[0]);
  const select = document.getElementById('groupField');
  fields.forEach(f => {{
    const opt = document.createElement('option');
    opt.value = f;
    opt.textContent = f;
    select.appendChild(opt);
  }});
  select.value = 'Location'; // Default grouping column

  function groupBy(arr, key) {{
    return arr.reduce((acc, item) => {{
      const group = item[key] || 'نامعلوم';
      (acc[group] = acc[group] || []).push(item);
      return acc;
    }}, {{}});
  }}

  function renderTable(daysLimit = 30) {{
    const groupKey = select.value;
    const grouped = groupBy(data, groupKey);

    let grandTotals = {{
      PreAmount: 0,
      LastBill: 0,
      ReceiveAmount: 0,
      Amount: 0
    }};

    let html = `
      <div style='text-align:center; margin-bottom:10px;'>
        <h1>اوگرائی رپورٹ</h1>
      </div>
      <div>تاریخ: ${reportDate}</div>

    `;

    for (const group in grouped) {{
      const rows = grouped[group];

      // Group totals
      let totalPre = 0, totalLast = 0, totalRec = 0, totalAmt = 0;

      html += `<table>
        <thead>
          <tr><th colspan='7' class='group-header'>مقام: ${{group}}</th></tr>
          <tr>
            <th>نمبر</th>
            <th>نام</th>
            <th>سابقہ ‌بنام</th>
            <th>تازہ ‌بنام</th>
            <th>‌وصولی</th>
            <th>بقایا ‌بنام</th>
            <th>${reportDate}</th>
          </tr>
        </thead>
        <tbody>`;

      rows.forEach((row, index) => {{
        const isDefaulter = row.lastPaymentDays > daysLimit;
        html += `
          <tr class='${{isDefaulter ? 'defaulter-row' : ''}}'>
            <td>${{row.ID}}</td>
            <td>${{row.Name}}</td>
            <td>${{row.PreAmount}}</td>
            <td>${{row.LastBill}}</td>
            <td>${{row.ReceiveAmount}}</td>
            <td>${{row.Amount}}</td>
            <td>${{row.BillDate || ''}}</td>
          </tr>`;

        // Sum group totals
        totalPre += parseFloat(row.PreAmount || 0);
        totalLast += parseFloat(row.LastBill || 0);
        totalRec += parseFloat(row.ReceiveAmount || 0);
        totalAmt += parseFloat(row.Amount || 0);
      }});

      html += `
        <tr class='group-total'>
          <td colspan='2'>گروپ کل:</td>
          <td>${{totalPre.toFixed(2)}}</td>
          <td>${{totalLast.toFixed(2)}}</td>
          <td>${{totalRec.toFixed(2)}}</td>
          <td>${{totalAmt.toFixed(2)}}</td>
          <td></td>
        </tr>
      `;

      html += `</tbody></table><br/>`;

      // Add to grand total
      grandTotals.PreAmount += totalPre;
      grandTotals.LastBill += totalLast;
      grandTotals.ReceiveAmount += totalRec;
      grandTotals.Amount += totalAmt;
    }}

    // Grand summary
    html += `
      <table>
        <tfoot>
          <tr class='summary-total'>
            <td colspan='2'>مکمل مجموعی کل:</td>
            <td>${{grandTotals.PreAmount.toFixed(2)}}</td>
            <td>${{grandTotals.LastBill.toFixed(2)}}</td>
            <td>${{grandTotals.ReceiveAmount.toFixed(2)}}</td>
            <td>${{grandTotals.Amount.toFixed(2)}}</td>
            <td></td>
          </tr>
        </tfoot>
      </table>
    `;

    document.getElementById('printArea').innerHTML = html;
  }}

  function applyFilter() {{
    const days = parseInt(document.getElementById('daysInput').value) || 0;
    renderTable(days);
  }}

  applyFilter();
</script>

</body>
</html>";

            return html;
        }
        private string GenerateHTMLReport(DataTable dt, int[] columnsToHide)
        {
            string header = "Augrai";

            // Build dropdown for group selection
            string groupOptions = "<option value=''>-- No Grouping --</option>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (!columnsToHide.Contains(i))
                    groupOptions += $"<option value='{dt.Columns[i].ColumnName}'>{dt.Columns[i].ColumnName}</option>";
            }

            // HTML with JS logic for grouping, sorting, and subtotaling
            string html = @"<!DOCTYPE html>
<html>
<head>
<meta charset='UTF-8'>
<title>" + header + @"</title>
<style>
    body { font-family: Arial; margin: 20px; }
    h1 { text-align: center; }
    select { margin: 10px 0; padding: 6px; }
    table { width: 100%; border-collapse: collapse; margin-top: 10px; }
    th, td { border: 1px solid #999; padding: 8px; text-align: left; }
    th { background-color: #f2f2f2; cursor: pointer; }
    tr.group-header td {
        background-color: #d9edf7;
        font-weight: bold;
        text-align: left;
    }
    tr.subtotal-row td {
        background-color: #f9f9f9;
        font-weight: bold;
        text-align: right;
    }
</style>
</head>
<body>

<h1>" + header + @"</h1>

<label for='groupSelect'><strong>Group By:</strong></label>
<select id='groupSelect' onchange='applyGrouping()'>
    " + groupOptions + @"
</select>

<table id='reportTable'>
<thead><tr>";

            // Header
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (!columnsToHide.Contains(i))
                    html += $"<th onclick='sortTable({i})'>{dt.Columns[i].ColumnName}</th>";
            }
            html += "</tr></thead><tbody>";

            // Body
            foreach (DataRow row in dt.Rows)
            {
                html += "<tr>";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (!columnsToHide.Contains(i))
                        html += $"<td>{row[i]}</td>";
                }
                html += "</tr>";
            }

            html += @"</tbody></table>

<script>
// --- Detect numeric values ---
function isNumeric(val) {
    return !isNaN(parseFloat(val)) && isFinite(val);
}

// --- Sorting ---
function sortTable(columnIndex) {
    var table = document.getElementById('reportTable');
    var tbody = table.tBodies[0];
    var rows = Array.from(tbody.querySelectorAll('tr:not(.group-header):not(.subtotal-row)'));
    var ascending = table.getAttribute('data-sort-asc') === 'true';

    rows.sort(function(a, b) {
        var cellA = a.cells[columnIndex]?.innerText || '';
        var cellB = b.cells[columnIndex]?.innerText || '';
        if (isNumeric(cellA) && isNumeric(cellB))
            return ascending ? (parseFloat(cellA) - parseFloat(cellB)) : (parseFloat(cellB) - parseFloat(cellA));
        return ascending ? cellA.localeCompare(cellB, undefined, {numeric:true}) 
                         : cellB.localeCompare(cellA, undefined, {numeric:true});
    });

    // Remove old group/subtotal rows
    tbody.querySelectorAll('.group-header, .subtotal-row').forEach(r => r.remove());
    rows.forEach(r => tbody.appendChild(r));

    table.setAttribute('data-sort-asc', !ascending);
    applyGrouping();
}

// --- Grouping + Subtotals ---
function applyGrouping() {
    var groupColumn = document.getElementById('groupSelect').value;
    var table = document.getElementById('reportTable');
    var tbody = table.tBodies[0];
    var rows = Array.from(tbody.querySelectorAll('tr:not(.group-header):not(.subtotal-row)'));

    // Remove previous groups
    tbody.querySelectorAll('.group-header, .subtotal-row').forEach(r => r.remove());
    if (!groupColumn) return;

    // Find column index
    var colIndex = Array.from(table.rows[0].cells).findIndex(th => th.innerText === groupColumn);
    if (colIndex === -1) return;

    // Sort by group column first
    rows.sort((a, b) => {
        var valA = a.cells[colIndex]?.innerText || '';
        var valB = b.cells[colIndex]?.innerText || '';
        return valA.localeCompare(valB, undefined, {numeric:true});
    });

    // Rebuild table with grouping + subtotal
    tbody.innerHTML = '';
    var currentGroup = null;
    var groupRows = [];

    function addSubtotal() {
        if (groupRows.length === 0) return;
        var numCols = table.rows[0].cells.length;
        var subtotalCells = Array(numCols).fill('');
        for (var i = 0; i < numCols; i++) {
            var vals = groupRows.map(r => r.cells[i]?.innerText || '');
            if (vals.every(v => isNumeric(v)))
                subtotalCells[i] = vals.reduce((a, b) => a + parseFloat(b), 0).toFixed(2);
        }
        var subtotalRow = document.createElement('tr');
        subtotalRow.classList.add('subtotal-row');
        subtotalRow.innerHTML = subtotalCells.map((v, i) => 
            i === 0 ? `<td colspan='1'><strong>Subtotal:</strong></td>` : `<td>${v || ''}</td>`
        ).join('');
        tbody.appendChild(subtotalRow);
    }

    rows.forEach(row => {
        var val = row.cells[colIndex]?.innerText || '';
        if (val !== currentGroup) {
            if (currentGroup !== null) addSubtotal(); // subtotal previous
            currentGroup = val;
            groupRows = [];
            var groupRow = document.createElement('tr');
            groupRow.classList.add('group-header');
            groupRow.innerHTML = `<td colspan='${table.rows[0].cells.length}'><strong>Group: ${val}</strong></td>`;
            tbody.appendChild(groupRow);
        }
        groupRows.push(row);
        tbody.appendChild(row);
    });

    // Add final subtotal
    addSubtotal();
}
</script>

</body></html>";

            return html;
        }
        private void updateGroup()
        {
            comb_groupby.Items.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                comb_groupby.Items.Add(dt.Columns[i].ColumnName);
            }
        }

        private void comb_groupby_SelectedIndexChanged(object sender, EventArgs e)
        {
            int colIndex = comb_groupby.SelectedIndex;

            if (colIndex >= 0)
            {
                string selectedColumn = dt.Columns[colIndex].ColumnName;

                // Ensure Location column exists
                if (!dt.Columns.Contains("Location"))
                {
                    MessageBox.Show("Location column not found");
                    return;
                }

                // Add backup column only once
                if (!dt.Columns.Contains("Location_Original"))
                {
                    dt.Columns.Add("Location_Original", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        row["Location_Original"] = row["Location"];
                    }
                }

                // Replace Location values with selected column values (for grouping)
                foreach (DataRow row in dt.Rows)
                {
                    row["Location"] = row[selectedColumn]?.ToString();
                }
                printReport(dt);

            }
        }
        
        private void btn_pagesetup_Click(object sender, EventArgs e)
        {
            // OPEN dialog with current report settings
            
            printReport(dt);

        }
    }
}
