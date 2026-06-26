using ArthiPOS.Controls.dashboard;
using ArthiPOS.Properties;
using BAL;
using EnvDTE;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ArthiPOS.Reporting
{
    public partial class ReportingData : Form
    {
        string startdate = "", lastdate = "", search = "", action = "";
        private BLReport bal;
        private DataTable dt;
        private DataTable dt_report;
        public ReportingData()
        {
            InitializeComponent();
            bal = new BLReport();
        }

        private void ReportingData_Load(object sender, EventArgs e)
        {
            startdate = date_start.Text;
            lastdate = date_last.Text;
            comboBox1.Items[0] = Resources.ResourceManager.GetString("rd1", ci);
            comboBox1.Items[1] = Resources.ResourceManager.GetString("rd2", ci);
            comboBox1.Items[2] = Resources.ResourceManager.GetString("rd3", ci);
            comboBox1.Items[3] = Resources.ResourceManager.GetString("rd4", ci);
            comboBox1.Items[4] = Resources.ResourceManager.GetString("rd5", ci);
        }

        int pageindex = 0;
        private void btn_search_Click(object sender, EventArgs e)
        {
            startdate = date_start.Text;
            lastdate = date_last.Text;
            //if (rd_rp1.Checked) { action = "SeasonDetail";}
            //if (rd_rp2.Checked) { action = "ExpenseDet"; }
            //if (rd_rp3.Checked) { action = "BipariDet"; }
            //if (rd_rp4.Checked) { action = "CustomerDet"; search = txt_name.Text; }
            //if (rd_rp5.Checked) { action = "AugraiTotDet"; }
            if (comboBox1.SelectedIndex == 0) { action = "SeasonDetail"; }
            else if (comboBox1.SelectedIndex == 1) { action = "ExpenseDet"; }
            else if (comboBox1.SelectedIndex == 2) { action = "BipariDet"; }
            else if (comboBox1.SelectedIndex == 3) { action = "CustomerDet"; search = txt_name.Text; }
            else if (comboBox1.SelectedIndex == 4) { action = "AugraiTotDet"; }
            else if (comboBox1.SelectedIndex == 5) { action = "AugraiFresh"; search = "RemainingFreshNotZero"; }
            else if (comboBox1.SelectedIndex == 6) { action = "AugraiFresh"; search = "AllAugrai"; }
            else if (comboBox1.SelectedIndex == 7) { action = "ReceDet"; }
            else if (comboBox1.SelectedIndex == 8) { action = "BikriCustomerSales"; search = "B"; }
            else if (comboBox1.SelectedIndex == 9) { action = "BikriCustomerSales"; search = ""; }


            List<object> list = bal.p_reporting_CRUD(action, startdate, lastdate, pageindex, 100, search);
            dt = (DataTable)list[1];

            dg_data.DataSource = dt;
            if (dg_data.Rows.Count == 0)
            {
                return;
            }
            if (comboBox1.SelectedIndex == 0)
            {
                dg_data.Columns[0].HeaderCell.Value = Resources.ResourceManager.GetString("a1094", ci);
                dg_data.Columns[1].HeaderCell.Value = Resources.ResourceManager.GetString("a0012", ci);
                dg_data.Columns[2].HeaderCell.Value = Resources.ResourceManager.GetString("a0205", ci);
                dg_data.Columns[3].HeaderCell.Value = Resources.ResourceManager.GetString("a0401", ci);
                dg_data.Columns[4].HeaderCell.Value = Resources.ResourceManager.GetString("a2024", ci);
                dg_data.Columns[5].HeaderCell.Value = Resources.ResourceManager.GetString("a1061", ci);
                dg_data.Columns[6].HeaderCell.Value = Resources.ResourceManager.GetString("a0512", ci);
                dg_data.Columns[7].HeaderCell.Value = Resources.ResourceManager.GetString("a2021", ci);
                dg_data.Columns[8].HeaderCell.Value = Resources.ResourceManager.GetString("a0038", ci);
                dg_data.Columns[9].HeaderCell.Value = Resources.ResourceManager.GetString("sa9", ci);
            }
            if (comboBox1.SelectedIndex == 1)
            {
                action = "ExpenseDet";
                dg_data.Columns[0].HeaderCell.Value = Resources.ResourceManager.GetString("ex1", ci);
                dg_data.Columns[1].HeaderCell.Value = Resources.ResourceManager.GetString("ex2", ci);
                dg_data.Columns[2].HeaderCell.Value = Resources.ResourceManager.GetString("ex3", ci);
                dg_data.Columns[3].HeaderCell.Value = Resources.ResourceManager.GetString("ex4", ci);
                dg_data.Columns[4].HeaderCell.Value = Resources.ResourceManager.GetString("ex5", ci);
            }
            if (comboBox1.SelectedIndex == 2)
            {
                action = "BipariDet";
                dg_data.Columns[0].HeaderCell.Value = Resources.ResourceManager.GetString("bd1", ci);
                dg_data.Columns[1].HeaderCell.Value = Resources.ResourceManager.GetString("bd2", ci);
                dg_data.Columns[2].HeaderCell.Value = Resources.ResourceManager.GetString("bd3", ci);
                dg_data.Columns[3].HeaderCell.Value = Resources.ResourceManager.GetString("bd4", ci);
                dg_data.Columns[4].HeaderCell.Value = Resources.ResourceManager.GetString("bd5", ci);
                dg_data.Columns[5].HeaderCell.Value = Resources.ResourceManager.GetString("bd6", ci);
            }
            if (comboBox1.SelectedIndex == 3)
            {
                action = "CustomerDet"; search = txt_name.Text;
                dg_data.Columns[0].HeaderCell.Value = Resources.ResourceManager.GetString("cd1", ci);
                dg_data.Columns[1].HeaderCell.Value = Resources.ResourceManager.GetString("cd2", ci);
                dg_data.Columns[2].HeaderCell.Value = Resources.ResourceManager.GetString("cd3", ci);
                dg_data.Columns[3].HeaderCell.Value = Resources.ResourceManager.GetString("cd4", ci);
                dg_data.Columns[4].HeaderCell.Value = Resources.ResourceManager.GetString("cd5", ci);
                dg_data.Columns[5].HeaderCell.Value = Resources.ResourceManager.GetString("cd6", ci);
                dg_data.Columns[6].HeaderCell.Value = Resources.ResourceManager.GetString("cd7", ci);
                dg_data.Columns[7].HeaderCell.Value = Resources.ResourceManager.GetString("cd8", ci);
                dg_data.Columns[8].HeaderCell.Value = Resources.ResourceManager.GetString("cd9", ci);
            }
            if (comboBox1.SelectedIndex == 4)
            {
                action = "AugraiTotDet";
                dg_data.Columns[0].HeaderCell.Value = Resources.ResourceManager.GetString("ad1", ci);
                dg_data.Columns[1].HeaderCell.Value = Resources.ResourceManager.GetString("ad2", ci);
                dg_data.Columns[2].HeaderCell.Value = Resources.ResourceManager.GetString("ad3", ci);
                dg_data.Columns[3].HeaderCell.Value = Resources.ResourceManager.GetString("ad4", ci);
                dg_data.Columns[4].HeaderCell.Value = Resources.ResourceManager.GetString("ad5", ci);
                dg_data.Columns[5].HeaderCell.Value = Resources.ResourceManager.GetString("ad6", ci);
                dg_data.Columns[6].HeaderCell.Value = Resources.ResourceManager.GetString("ad7", ci);
                dg_data.Columns[7].HeaderCell.Value = Resources.ResourceManager.GetString("ad8", ci);
                dg_data.Columns[8].HeaderCell.Value = Resources.ResourceManager.GetString("ad9", ci);
                dg_data.Columns[9].HeaderCell.Value = Resources.ResourceManager.GetString("ad10", ci);
                dg_data.Columns[10].HeaderCell.Value = Resources.ResourceManager.GetString("ad11", ci);
            }
            if (comboBox1.SelectedIndex == 5 || comboBox1.SelectedIndex == 6)
            {
                dg_data.Columns[0].HeaderCell.Value = Resources.ResourceManager.GetString("af1", ci);
                dg_data.Columns[1].HeaderCell.Value = Resources.ResourceManager.GetString("af2", ci);
                dg_data.Columns[2].HeaderCell.Value = Resources.ResourceManager.GetString("af3", ci);
                dg_data.Columns[3].HeaderCell.Value = Resources.ResourceManager.GetString("af4", ci);
                dg_data.Columns[4].HeaderCell.Value = Resources.ResourceManager.GetString("af5", ci);
                dg_data.Columns[5].HeaderCell.Value = Resources.ResourceManager.GetString("af6", ci);
                dg_data.Columns[6].HeaderCell.Value = Resources.ResourceManager.GetString("af7", ci);
                dg_data.Columns[7].HeaderCell.Value = Resources.ResourceManager.GetString("af8", ci);
            }

        }
        private string header = "";
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ur-PK");

        private void lbl_print_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lbl_print_Click(object sender, EventArgs e)
        {
            AllReportsCC rp = new AllReportsCC();
            string[] c = new string[13];
            if (comboBox1.SelectedIndex == 0)
            {
                action = "SeasonDetail";
                c[0] = Resources.ResourceManager.GetString("a1094", ci);
                c[1] = Resources.ResourceManager.GetString("a0012", ci);
                c[2] = Resources.ResourceManager.GetString("a0205", ci);
                c[3] = Resources.ResourceManager.GetString("a0401", ci);
                c[4] = Resources.ResourceManager.GetString("a2024", ci);
                c[5] = Resources.ResourceManager.GetString("a1061", ci);
                c[6] = Resources.ResourceManager.GetString("a0512", ci);
                c[7] = Resources.ResourceManager.GetString("a2021", ci);
                c[8] = Resources.ResourceManager.GetString("a0038", ci);
                c[9] = Resources.ResourceManager.GetString("a0006", ci);
                c[10] = "";
                c[11] = "";
                c[12] = "";
            }
            else
            if (comboBox1.SelectedIndex == 1)
            {
                action = "ExpenseDet";
                c[0] = Resources.ResourceManager.GetString("ex1", ci);
                c[1] = Resources.ResourceManager.GetString("ex2", ci);
                c[2] = Resources.ResourceManager.GetString("ex3", ci);
                c[3] = Resources.ResourceManager.GetString("ex4", ci);
                c[4] = Resources.ResourceManager.GetString("ex5", ci);
                c[5] = Resources.ResourceManager.GetString("ex6", ci);
                c[6] = Resources.ResourceManager.GetString("ex7", ci);
                c[7] = Resources.ResourceManager.GetString("ex8", ci);
                c[8] = Resources.ResourceManager.GetString("ex9", ci);
                c[9] = Resources.ResourceManager.GetString("ex10", ci);
                c[10] = Resources.ResourceManager.GetString("ex11", ci);
                c[11] = Resources.ResourceManager.GetString("ex12", ci);
                c[12] = Resources.ResourceManager.GetString("ex13", ci);
            }
            else
            if (comboBox1.SelectedIndex == 2)
            {
                action = "BipariDet";
                c[0] = Resources.ResourceManager.GetString("bd1", ci);
                c[1] = Resources.ResourceManager.GetString("bd2", ci);
                c[2] = Resources.ResourceManager.GetString("bd3", ci);
                c[3] = Resources.ResourceManager.GetString("bd4", ci);
                c[4] = Resources.ResourceManager.GetString("bd5", ci);
                c[5] = Resources.ResourceManager.GetString("bd6", ci);
                c[6] = Resources.ResourceManager.GetString("bd7", ci);
                c[7] = Resources.ResourceManager.GetString("bd8", ci);
                c[8] = Resources.ResourceManager.GetString("bd9", ci);
                c[9] = Resources.ResourceManager.GetString("bd10", ci);
                c[10] = Resources.ResourceManager.GetString("bd11", ci);
                c[11] = Resources.ResourceManager.GetString("bd12", ci);
                c[12] = Resources.ResourceManager.GetString("bd13", ci);
            }
            else
            if (comboBox1.SelectedIndex == 3)
            {
                action = "CustomerDet";
                c[0] = Resources.ResourceManager.GetString("cd1", ci);
                c[1] = Resources.ResourceManager.GetString("cd2", ci);
                c[2] = Resources.ResourceManager.GetString("cd3", ci);
                c[3] = Resources.ResourceManager.GetString("cd4", ci);
                c[4] = Resources.ResourceManager.GetString("cd5", ci);
                c[5] = Resources.ResourceManager.GetString("cd6", ci);
                c[6] = Resources.ResourceManager.GetString("cd7", ci);
                c[7] = Resources.ResourceManager.GetString("cd8", ci);
                c[8] = Resources.ResourceManager.GetString("cd9", ci);
                c[9] = Resources.ResourceManager.GetString("cd10", ci);
                c[10] = Resources.ResourceManager.GetString("cd11", ci);
                c[11] = "";
                c[12] = "";
            }
            else
            if (comboBox1.SelectedIndex == 4)
            {
                action = "AugraiTotDet";
                c[0] = Resources.ResourceManager.GetString("ad1", ci);
                c[1] = Resources.ResourceManager.GetString("ad2", ci);
                c[2] = Resources.ResourceManager.GetString("ad3", ci);
                c[3] = Resources.ResourceManager.GetString("ad4", ci);
                c[4] = Resources.ResourceManager.GetString("ad5", ci);
                c[5] = Resources.ResourceManager.GetString("ad6", ci);
                c[6] = Resources.ResourceManager.GetString("ad7", ci);
                c[7] = Resources.ResourceManager.GetString("ad8", ci);
                c[8] = Resources.ResourceManager.GetString("ad9", ci);
                c[9] = Resources.ResourceManager.GetString("ad10", ci);
                c[10] = Resources.ResourceManager.GetString("ad11", ci);
                c[11] = Resources.ResourceManager.GetString("ad12", ci);
                c[12] = Resources.ResourceManager.GetString("ad13", ci);
            }
            else
            if (comboBox1.SelectedIndex == 5 || comboBox1.SelectedIndex == 6)
            {
                action = "AugraiFresh";
                c[0] = Resources.ResourceManager.GetString("af1", ci);
                c[1] = "";
                c[2] = Resources.ResourceManager.GetString("af2", ci);
                c[3] = Resources.ResourceManager.GetString("af3", ci);
                c[4] = Resources.ResourceManager.GetString("af4", ci);
                c[5] = Resources.ResourceManager.GetString("af5", ci);
                c[6] = Resources.ResourceManager.GetString("af6", ci);
                c[7] = Resources.ResourceManager.GetString("af7", ci);
                c[8] = Resources.ResourceManager.GetString("af8", ci);
            }




            if (comboBox1.SelectedIndex == 1 || comboBox1.SelectedIndex == 7)
                rp.Reportingdata(dt);
            else
                rp.ReportingData(dt, header, c[0], c[1], c[2], c[3], c[4], c[5], c[6],
                 c[7], c[8], c[9], c[10], c[11], c[12]);


            rp.ShowDialog();
        }

        private void chk_sort_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_sort.Checked)
            {
                pageindex = 1;
            }
            else
            {
                pageindex = 0;
            }
            btn_search_Click(this, new EventArgs());
        }

        private void btn_recalSale_Click(object sender, EventArgs e)
        {

        }

        private void btn_browserprint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string header = comboBox1.SelectedText;
            if (comboBox1.SelectedIndex == 5) { search = "name1"; }
            else if (comboBox1.SelectedIndex == 6) { search = "name2"; }
            else if (comboBox1.SelectedIndex == 8) { search = "B"; }
            else if (comboBox1.SelectedIndex == 9) { search = ""; }
            else { search = "name"; }

            List<object> list = bal.p_reporting_CRUD(action, startdate, lastdate, pageindex, 100, search);
            dt = (DataTable)list[1];
            string htmlReport = GenerateHTMLReportUrdu(dt, header);

            // Output the HTML to a file
            string filePath = @"report.html";
            File.WriteAllText(filePath, htmlReport);

            // Open the HTML report in the default web browser
            System.Diagnostics.Process.Start(filePath);
            Console.WriteLine(htmlReport);
        }
        private string GenerateHTMLReportUrdu(DataTable dt, string Header)
        {
            // Convert DataTable to JSON for embedding into HTML
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

            string html = $@"<!doctype html>
<html lang='ur' dir='rtl'>
<head>
<meta charset='utf-8'>
<title>Augrai Report</title>
<style>
  <style>
@import url('https://fonts.googleapis.com/css2?family=Noto+Nastaliq+Urdu&display=swap');

body {{
  font-family: 'Noto Nastaliq Urdu', serif;
  margin: 25px;
}}

.controls {{
  margin-bottom: 10px;
}}

table {{
  width: 100%;
  border-collapse: collapse;
  table-layout: fixed;
}}

th, td {{
  border: 1px solid #bbb;
  padding: 4px;
  text-align: right;
  position: relative;
  overflow-wrap: break-word; /* wrap text */
  word-wrap: break-word;
  white-space: normal;       /* allow multiple lines */
}}

th {{
  background: #f2f2f2;
  cursor: pointer;
}}

h1 {{
  text-align: center;
  margin-bottom: 20px;
}}

/* Resize handle */
th .resizer {{
  position: absolute;
  left: 0;
  top: 0;
  width: 5px;
  height: 100%;
  cursor: col-resize;
}}

.group-header {{
  background: #e4ebf5;
  font-weight: bold;
}}

.subtotal {{
  background: #f6f6f6;
  font-weight: bold;
}}

.grandtotal {{
  background: #d9d9d9;
  font-weight: bold;
}}

.column-panel label {{
  margin-left: 10px;
  display: inline-block;
  margin-right: 10px;
}}

/* PRINT STYLES */
@media print {{
  .controls, .column-panel {{ display:none; }}

  body {{
    margin: 0;
    counter-reset: page;
  }}

  table {{
    page-break-inside: auto;
	font-size: 16px;
  }}

  tr {{
    page-break-inside: avoid;
    page-break-after: auto;
  }}

  thead {{ display: table-header-group; }} /* repeat header */
  tfoot {{ display: table-footer-group; }} /* repeat footer */

  /* Footer for page numbers */
  .page-footer {{
    position: fixed;
    bottom: 0;
    width: 100%;
    text-align: center;
    font-size: 12px;
  }}

  .page-footer:after {{
    content: ""صفحہ "" counter(page);
  }}
}}
</style>
</head>

<body>

<!-- CONTROLS -->
<div class=""controls"">
  رپورٹ کا عنوان:
  <input id=""reportTitleInput"" value=""Report"" style=""width:200px;"">
  
  تلاش:
  <input id=""searchInput"">

  گروپ کریں:
  <select id=""groupSelect""></select>

  فونٹ سائز:
  <input type=""number"" id=""fontSizeInput"" value=""16"" style=""width:60px;""> px

  <button onclick=""window.print()"">پرنٹ</button>
</div>

<!-- COLUMN VISIBILITY -->
<div class=""column-panel"" id=""columnPanel""></div>

<!-- HEADER -->
<h1 id=""reportTitle"">Report</h1>

<div id=""printArea""></div>

<!-- FOOTER for page numbers -->
<div class=""page-footer""></div>



<script>

const data =  {jsonData};



/* ================= DOM ================= */
const searchInput = document.getElementById(""searchInput"");
const groupSelect = document.getElementById(""groupSelect"");
const columnPanel = document.getElementById(""columnPanel"");
const printArea = document.getElementById(""printArea"");
const reportTitleInput = document.getElementById(""reportTitleInput"");
const reportTitle = document.getElementById(""reportTitle"");
const fontSizeInput = document.getElementById(""fontSizeInput"");

/* ================= STATE ================= */
let filteredData = [...data];
let sortState = {{ field:null, asc:true }};
const allColumns = Object.keys(data[0]);
let visibleColumns = {{}};
let columnWidths = {{}};

/* ================= INIT ================= */
init();

function init(){{
  allColumns.forEach(c=>{{
    visibleColumns[c] = true;
    columnWidths[c] = 150;
  }});

  buildGroupDropdown();
  buildColumnPanel();

  groupSelect.onchange = applyFilter;
  searchInput.oninput  = applyFilter;

  reportTitleInput.oninput = () => {{
    reportTitle.textContent = reportTitleInput.value;
  }};

  fontSizeInput.oninput = () => {{
    const size = parseInt(fontSizeInput.value) || 16;
    printArea.style.fontSize = size + ""px"";
  }};

  applyFilter();
}}

/* ================= UI BUILDERS ================= */
function buildGroupDropdown(){{
  groupSelect.innerHTML = `<option value="""">کوئی گروپ نہیں</option>`;
  allColumns.forEach(c=>{{
    groupSelect.innerHTML += `<option value=""${{c}}"">${{c}}</option>`;
  }});
}}

function buildColumnPanel(){{
  columnPanel.innerHTML = ""<b>کالم دکھائیں:</b><br>"";
  allColumns.forEach(c=>{{
    columnPanel.innerHTML += `
      <label>
        <input type=""checkbox"" checked onchange=""toggleColumn('${{c}}')""> ${{c}}
      </label>
      <input type=""number"" placeholder=""px"" style=""width:55px"" onchange=""setWidth('${{c}}', this.value)"">
    `;
  }});
}}

/* ================= HELPERS ================= */
function isNumeric(v){{ return typeof v === ""number"" && !isNaN(v); }}

function groupBy(arr, key){{
  if(!key) return {{ """": arr }};
  return arr.reduce((acc,row)=>{{
    let g = row[key] ?? ""دیگر"";
    acc[g] = acc[g] || [];
    acc[g].push(row);
    return acc;
  }},{{}});
}}

/* ================= FILTER ================= */
function applyFilter(){{
  const search = searchInput.value.toLowerCase();
  filteredData = data.filter(r =>
    Object.values(r).join("" "").toLowerCase().includes(search)
  );
  renderTable(filteredData);
}}

/* ================= SORT ================= */
function sortTable(field){{
  sortState.asc = sortState.field === field ? !sortState.asc : true;
  sortState.field = field;

  filteredData.sort((a,b)=>{{
    const x = a[field] ?? """";
    const y = b[field] ?? """";

    if(isNumeric(x) && isNumeric(y))
      return sortState.asc ? x-y : y-x;

    return sortState.asc
      ? String(x).localeCompare(String(y),'ur')
      : String(y).localeCompare(String(x),'ur');
  }});

  renderTable(filteredData);
}}

/* ================= COLUMN VISIBILITY ================= */
function toggleColumn(col){{
  visibleColumns[col] = !visibleColumns[col];
  renderTable(filteredData);
}}

function setWidth(col, val){{
  if(!val) {{ columnWidths[col] = ""auto""; }}
  else {{
    let w = parseInt(val);
    if(isNaN(w) || w<40) w=40;
    columnWidths[col] = w + ""px"";
  }}
  renderTable(filteredData);
}}

/* ================= COLUMN RESIZE ================= */
function makeResizable(table){{
  const ths = table.querySelectorAll(""th"");
  ths.forEach(th=>{{
    const resizer = document.createElement(""div"");
    resizer.className = ""resizer"";
    th.appendChild(resizer);

    let startX, startWidth;
    resizer.addEventListener(""mousedown"", e=>{{
      startX = e.pageX;
      startWidth = th.offsetWidth;
      document.onmousemove = ev=>{{
        const newWidth = startWidth + (startX - ev.pageX);
        if(newWidth > 40) th.style.width = newWidth + ""px"";
      }};
      document.onmouseup = ()=>{{ document.onmousemove=document.onmouseup=null; }};
    }});
  }});
}}

/* ================= RENDER ================= */
function renderTable(rows){{
  const groupKey = groupSelect.value;
  const grouped  = groupBy(rows, groupKey);
  const columns  = allColumns.filter(c=>visibleColumns[c]);

  let grandTotals = {{}};
  columns.forEach(c=>grandTotals[c]=0);

  let html = `<table><thead><tr>`;
  columns.forEach(c=>{{
    html += `<th style=""width:${{columnWidths[c]}}"" onclick=""sortTable('${{c}}')"">${{c}}</th>`;
  }});
  html += `</tr></thead><tbody>`;

  for(const group in grouped){{
    if(groupKey){{
      html += `<tr class=""group-header""><td colspan=""${{columns.length}}"">${{groupKey}} : ${{group}}</td></tr>`;
    }}

    let subTotals = {{}};
    columns.forEach(c=>subTotals[c]=0);

    grouped[group].forEach(r=>{{
      html += `<tr>`;
      columns.forEach(c=>{{
        const val = r[c] ?? """";
        html += `<td>${{val}}</td>`;
        if(isNumeric(val)){{
          subTotals[c]+=val;
          grandTotals[c]+=val;
        }}
      }});
      html += `</tr>`;
    }});

    html += `<tr class=""subtotal"">`;
    columns.forEach((c,i)=>{{
      html += i===0 ? `<td>سب ٹوٹل</td>` : `<td>${{subTotals[c]||""""}}</td>`;
    }});
    html += `</tr>`;
  }}

  html += `<tr class=""grandtotal"">`;
  columns.forEach((c,i)=>{{
    html += i===0 ? `<td>کل مجموعہ</td>` : `<td>${{grandTotals[c]||""""}}</td>`;
  }});
  html += `</tr>`;

  html += `</tbody></table>`;
  printArea.innerHTML = html;

  makeResizable(printArea.querySelector(""table""));
}}
</script>

</body>
</html>";

            return html;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) { action = "SeasonDetail"; header = Resources.ResourceManager.GetString("rd1", ci); btn_recalSale.Visible = txt_name.Visible = chk_sort.Visible = false; chk_sort.Visible = false; }
            if (comboBox1.SelectedIndex == 1) { action = "ExpenseDet"; header = Resources.ResourceManager.GetString("rd2", ci); btn_recalSale.Visible = txt_name.Visible = chk_sort.Visible = false; }
            if (comboBox1.SelectedIndex == 2) { action = "BipariDet"; header = Resources.ResourceManager.GetString("rd3", ci); txt_name.Visible = btn_recalSale.Visible = chk_sort.Visible = false; }
            if (comboBox1.SelectedIndex == 3)
            {
                action = "CustomerDet";
                header = Resources.ResourceManager.GetString("rd4", ci);
                txt_name.Visible = true; chk_sort.Visible = false;
                btn_recalSale.Visible = true;

            }
            if (comboBox1.SelectedIndex == 4) { action = "AugraiTotDet"; header = Resources.ResourceManager.GetString("rd5", ci); txt_name.Visible = btn_recalSale.Visible = chk_sort.Visible = false; }
            if (comboBox1.SelectedIndex == 5) { action = "AugraiFresh"; header = Resources.ResourceManager.GetString("rd5", ci); txt_name.Visible = btn_recalSale.Visible = false; search = "RemaingFreshNotZero"; chk_sort.Visible = true; }
            if (comboBox1.SelectedIndex == 6) { action = "AugraiFresh"; header = Resources.ResourceManager.GetString("rd5", ci); txt_name.Visible = btn_recalSale.Visible = false; search = "AllAugrai"; chk_sort.Visible = true; }
            else if (comboBox1.SelectedIndex == 7)
            {
                action = "ReceDet";
                txt_name.Visible = true; chk_sort.Visible = false;
            }else if(comboBox1.SelectedIndex==8 || comboBox1.SelectedIndex ==9)
            {
                action = "BikriCustomerSales";
            }
            startdate = date_start.Text;
            lastdate = date_last.Text;
        }


        public void searchDialog()
        {
            using (Search search = new Search(6, txt_name.Text))
            {
                DialogResult res = search.ShowDialog();
                txt_name.Text = search.Id;
                btn_search_Click(this, new EventArgs());
                search.Close();

                return;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {

                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.Enter:

                    try
                    {
                        searchDialog();

                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    return true;
                case Keys.Control | Keys.P:

                    lbl_print_Click(this, new EventArgs());
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

}
