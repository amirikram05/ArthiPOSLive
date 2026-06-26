using ArthiPOS.utill;
using BAL;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraPrinting.Native;
using EnvDTE;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using System.Windows.Markup;

namespace ArthiPOS.Reporting
{
    public partial class ReportAllData : Form
    {
        string startdate = "", lastdate = "", search = "", action = "";
        private BLReport bal;

        private void lbl_print_Click(object sender, EventArgs e)
        {
            string columns = txt_hide_col.Text;
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

            //string htmlReport = GenerateHTMLReport(dt, columnsToHide);
            string date = comb_19.Text+$"\nFrom {date_start.Text} To {date_last.Text}";
            string htmlReport = CommonUtill.GenerateHTMLReportUrdu(dt, date);
            // Output the HTML to a file
            string filePath = @"report.html";
            File.WriteAllText(filePath, htmlReport);

            // Open the HTML report in the default web browser
            System.Diagnostics.Process.Start(filePath);

            Console.WriteLine("HTML report generated successfully!");
        }
        private string GenerateHTMLReport(DataTable dt, int[] columnsToHide)
        {
            string header = comb_list.Text;

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
<title>"+ header + @"</title>
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


        /*       private string GenerateHTMLReport(DataTable dt, int[] columnsToHide)
               {
                   string header = comb_list.Text;

                   // --- Build dropdown for group selection ---
                   string groupOptions = "<option value=''>-- No Grouping --</option>";
                   for (int i = 0; i < dt.Columns.Count; i++)
                   {
                       if (!columnsToHide.Contains(i))
                       {
                           groupOptions += $"<option value='{dt.Columns[i].ColumnName}'>{dt.Columns[i].ColumnName}</option>";
                       }
                   }

                   // --- Build HTML with JS-based grouping & sorting ---
                   string html = @"<!DOCTYPE html>
       <html>
       <head>
       <meta charset='UTF-8'>
       <title>HTML Report with Dynamic Grouping & Sorting</title>
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

                   // Header row
                   for (int i = 0; i < dt.Columns.Count; i++)
                   {
                       if (!columnsToHide.Contains(i))
                       {
                           html += $"<th onclick='sortTable({i})'>{dt.Columns[i].ColumnName}</th>";
                       }
                   }

                   html += "</tr></thead><tbody>";

                   // Add all rows (flat list)
                   foreach (DataRow row in dt.Rows)
                   {
                       html += "<tr>";
                       for (int i = 0; i < dt.Columns.Count; i++)
                       {
                           if (!columnsToHide.Contains(i))
                           {
                               html += $"<td>{row[i]}</td>";
                           }
                       }
                       html += "</tr>";
                   }

                   html += @"</tbody></table>

       <script>
       // --- Sorting logic ---
       function sortTable(columnIndex) {
           var table = document.getElementById('reportTable');
           var tbody = table.tBodies[0];
           var rows = Array.from(tbody.querySelectorAll('tr:not(.group-header)'));
           var ascending = table.getAttribute('data-sort-asc') === 'true';

           rows.sort(function(a, b) {
               var cellA = a.cells[columnIndex]?.innerText || '';
               var cellB = b.cells[columnIndex]?.innerText || '';
               return ascending ? cellA.localeCompare(cellB, undefined, {numeric:true}) 
                                : cellB.localeCompare(cellA, undefined, {numeric:true});
           });

           // Remove existing group headers before resorting
           tbody.querySelectorAll('.group-header').forEach(r => r.remove());
           rows.forEach(r => tbody.appendChild(r));

           table.setAttribute('data-sort-asc', !ascending);
           applyGrouping();
       }

       // --- Grouping logic ---
       function applyGrouping() {
           var groupColumn = document.getElementById('groupSelect').value;
           var table = document.getElementById('reportTable');
           var tbody = table.tBodies[0];
           var rows = Array.from(tbody.querySelectorAll('tr:not(.group-header)'));

           // Remove old group headers
           tbody.querySelectorAll('.group-header').forEach(r => r.remove());
           if (!groupColumn) return;

           var colIndex = Array.from(table.rows[0].cells)
               .findIndex(th => th.innerText === groupColumn);

           if (colIndex === -1) return;

           // Sort rows by group column first
           rows.sort((a, b) => {
               var valA = a.cells[colIndex]?.innerText || '';
               var valB = b.cells[colIndex]?.innerText || '';
               return valA.localeCompare(valB, undefined, {numeric:true});
           });

           tbody.innerHTML = '';
           var currentGroup = null;

           rows.forEach(row => {
               var val = row.cells[colIndex]?.innerText || '';
               if (val !== currentGroup) {
                   currentGroup = val;
                   var groupRow = document.createElement('tr');
                   groupRow.classList.add('group-header');
                   groupRow.innerHTML = `<td colspan='${table.rows[0].cells.length}'><strong>Group: ${val}</strong></td>`;
                   tbody.appendChild(groupRow);
               }
               tbody.appendChild(row);
           });
       }
       </script>

       </body></html>";

                   return html;
               }*/

        //private string GenerateHTMLReport(DataTable dt, int[] columnsToHide, string groupByColumn = null)
        //{
        //    string header = comb_list.Text;
        //    // Start building the HTML string
        //    string html = "<!DOCTYPE html>";
        //    html += "<html>";
        //    html += "<head>";
        //    html += "<meta charset='UTF-8'>";
        //    html += "<title>HTML Report with Sorting and Grouping</title>";
        //    html += "<style>";
        //    html += "table { width: 100%; border-collapse: collapse; }";
        //    html += "th, td { border: 1px solid black; padding: 8px; text-align: left; }";
        //    html += "th { background-color: #f2f2f2; cursor: pointer; }";  // Sortable columns
        //    html += "</style>";
        //    html += "</head>";
        //    html += "<body>";
        //    html += "<h1>"+comb_list.Text+"</h1>";
        //    html += "<table id='reportTable'>";

        //    // Create table header dynamically based on DataTable columns
        //    html += "<thead><tr>";
        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        if (!columnsToHide.Contains(i))  // Check if the column index is NOT in the columnsToHide list
        //        {
        //            // Add a clickable header for sorting
        //            html += $"<th onclick='sortTable({i})'>{dt.Columns[i].ColumnName}</th>";
        //        }
        //    }
        //    html += "</tr></thead>";

        //    // Group data if the groupByColumn is provided
        //    if (!string.IsNullOrEmpty(groupByColumn))
        //    {
        //        // Sort the DataTable by the groupByColumn before building the HTML
        //        DataView dv = dt.DefaultView;
        //        dv.Sort = groupByColumn;
        //        dt = dv.ToTable();
        //    }

        //    // Populate rows dynamically based on DataTable rows, handling grouping
        //    html += "<tbody>";
        //    string currentGroupValue = null;
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        // If grouping, check for change in group value and add a group header
        //        if (!string.IsNullOrEmpty(groupByColumn) && row[groupByColumn].ToString() != currentGroupValue)
        //        {
        //            currentGroupValue = row[groupByColumn].ToString();
        //            html += $"<tr><td colspan='{dt.Columns.Count - columnsToHide.Length}'><strong>Group: {currentGroupValue}</strong></td></tr>";
        //        }

        //        html += "<tr>";
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            if (!columnsToHide.Contains(i))  // Check if the column index is NOT in the columnsToHide list
        //            {
        //                html += $"<td>{row[i]}</td>";
        //            }
        //        }
        //        html += "</tr>";
        //    }
        //    html += "</tbody>";
        //    html += "</table>";

        //    // JavaScript for sorting columns
        //    html += "<script>";
        //    html += "function sortTable(columnIndex) {";
        //    html += "    var table = document.getElementById('reportTable');";
        //    html += "    var rows = Array.from(table.rows).slice(1);";  // Get all rows except the header
        //    html += "    var ascending = table.getAttribute('data-sort-asc') == 'true';";
        //    html += "    rows.sort(function(rowA, rowB) {";
        //    html += "        var cellA = rowA.cells[columnIndex].innerText;";
        //    html += "        var cellB = rowB.cells[columnIndex].innerText;";
        //    html += "        return ascending ? cellA.localeCompare(cellB) : cellB.localeCompare(cellA);";
        //    html += "    });";
        //    html += "    // Toggle sorting direction";
        //    html += "    table.setAttribute('data-sort-asc', !ascending);";
        //    html += "    // Rebuild table body";
        //    html += "    var tbody = table.tBodies[0];";
        //    html += "    rows.forEach(function(row) { tbody.appendChild(row); });";
        //    html += "}";
        //    html += "</script>";

        //    html += "</body>";
        //    html += "</html>";

        //    return html;
        //}

        private DataTable dt, dt_report;

        private void comb_list_DropDownClosed(object sender, EventArgs e)
        {
            string t=comb_list.Text;
            comb_list.Text = t.Trim();
        }

        private void comb_list_Click(object sender, EventArgs e)
        {
            string t = comb_list.Text;
            comb_list.Text = t.Trim();
        }

        private void comb_19_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comb_19.SelectedIndex;
            dt = bal.GetShopExpenseReport(date_start.Text, date_last.Text, index+1);
            dgv_data.DataSource = dt;
        }

        public ReportAllData()
        {
            InitializeComponent();
            bal = new BLReport();
            dt_report = bal.getRpName("0");
            DataRow row = dt_report.Rows[0];
            //lbl_reports.Text = row[0].ToString();
            listDownNames(row[0].ToString());

        }
        private void listDownNames(string data)
        {

            string[] parts = data.Split('،');
            foreach (string name in parts)
            {
                // Finding the index of '='
                comb_list.Items.Add(name.Trim().TrimStart());
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string action = (comb_list.SelectedIndex+1)+"";
            string reportno = action;
            string search = txt_name.Text;
            string sdate = date_start.Text;
            string ldate = date_last.Text;
            string filter = txt_filter.Text;

            if (comb_list.SelectedIndex == 18)
            {
                comb_19.Visible = true;
                comb_19.Items.Clear();
                int index = 0;
                if (comb_19.Items.Count == 0) 
                { 
                    string[] sectionNames =
                    {
                        "📄 Report Header",
                        "📊 Overall Summary",
                        "💰 Expense Category",
                        "🏪 Shop Location",
                        "📈 Daily Trend",
                        "📅 Monthly Summary",
                        "📆 Weekly Summary",
                        "🗓 Weekday Analysis",
                        "🔁 Small Recurring Expenses",
                        "📋 Top Transactions",
                        "⚠️ Anomaly Detection",
                        "📌 Running Total",
                        "🕒 Hourly Pattern",
                        "🔥 Top Expense Days",
                        "📆្នាំ Yearly Summary",
                        "📋 DETAILED TRANSACTION LIST (All Records)"
                    };
                    foreach (string name in sectionNames)
                    {
                        comb_19.Items.Add(name);
                    }
                    comb_19.SelectedIndex = 0;
                }
                index = comb_19.SelectedIndex+1;
                dt = bal.GetShopExpenseReport(  sdate, ldate, index);


            }
            else
            {
                comb_19.Visible = false;
                comb_19.Items.Clear();
                dt = bal.getReportDataAll(action, reportno, search, sdate, ldate, filter);


            }
            dgv_data.DataSource = dt;


        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.Control | Keys.P:
                    lbl_print_Click(this, new EventArgs());
                    return true;

                case Keys.Enter:
                    btn_search_Click(this, new EventArgs());
                    return true;

            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
