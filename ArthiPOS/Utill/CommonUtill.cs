using MetroFramework.Controls;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ArthiPOS.utill
{
    public class CommonUtill
    {
        public static void dim_Background(Control parent, Form actionform)
        {
            // take a screenshot of the form and darken it:
            Bitmap bmp = new Bitmap(parent.ClientRectangle.Width, parent.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                G.CopyFromScreen(parent.PointToScreen(new Point(0, 0)), new Point(0, 0), parent.ClientRectangle.Size);
                double percent = 0.60;
                Color darken = Color.FromArgb((int)(255 * percent), Color.Black);
                using (Brush brsh = new SolidBrush(darken))
                {
                    G.FillRectangle(brsh, parent.ClientRectangle);
                }
            }

            // put the darkened screenshot into a Panel and bring it to the front:
            using (Panel p = new Panel())
            {
                p.Location = new Point(0, 0);
                p.Size = parent.ClientRectangle.Size;
                p.BackgroundImage = bmp;
                parent.Controls.Add(p);
                p.BringToFront();

                // display your dialog somehow:
                //Form frm = new Form();
                //frm.StartPosition = FormStartPosition.CenterParent;
                //frm.ShowDialog(this);

                // Transport actionform = new Transport();
                actionform.TopLevel = true;
                actionform.ShowInTaskbar = false;
                actionform.ShowDialog();

            } // panel will be disposed and the form will "lighten" again...
        }

        public static string getKey(string _pid, string tag, string date)
        {
            return string.Format("{0}-{1}-{2}", _pid, tag, date.Replace("-", "")); ;
        }
        //        public static string GenerateHTMLReportUrdu(DataTable dt, string Header)
        //        {
        //            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

        //            var sb = new System.Text.StringBuilder();

        //            sb.Append(@"<!doctype html>
        //<html lang='ur' dir='rtl'>
        //<head>
        //<meta charset='utf-8'>
        //<title>");
        //            sb.Append(Header);
        //            sb.Append(@"</title>
        //<style>
        //@import url('https://fonts.googleapis.com/css2?family=Noto+Nastaliq+Urdu&display=swap');

        //body {
        //  font-family: 'Noto Nastaliq Urdu', serif;
        //  margin: 25px;
        //}

        //.controls {
        //  margin-bottom: 10px;
        //}

        //table {
        //  width: 100%;
        //  border-collapse: collapse;
        //  table-layout: fixed;
        //}

        //th, td {
        //  border: 1px solid #bbb;
        //  padding: 4px;
        //  text-align: right;
        //  position: relative;
        //  overflow-wrap: break-word;
        //  word-wrap: break-word;
        //  white-space: normal;
        //}

        //th {
        //  background: #f2f2f2;
        //  cursor: pointer;
        //}

        //h1 {
        //  text-align: center;
        //  margin-bottom: 20px;
        //}

        //th .resizer {
        //  position: absolute;
        //  left: 0;
        //  top: 0;
        //  width: 5px;
        //  height: 100%;
        //  cursor: col-resize;
        //}

        //.group-header {
        //  background: #e4ebf5;
        //  font-weight: bold;
        //}

        //.subtotal {
        //  background: #f6f6f6;
        //  font-weight: bold;
        //}

        //.grandtotal {
        //  background: #d9d9d9;
        //  font-weight: bold;
        //}

        //.column-panel label {
        //  margin-left: 10px;
        //  display: inline-block;
        //  margin-right: 10px;
        //}

        //@media print {
        //  .controls, .column-panel { display: none; }

        //  body {
        //    margin: 0;
        //    counter-reset: page;
        //  }

        //  table {
        //    page-break-inside: auto;
        //    font-size: 16px;
        //  }

        //  tr {
        //    page-break-inside: avoid;
        //    page-break-after: auto;
        //  }

        //  thead { display: table-header-group; }
        //  tfoot { display: table-footer-group; }

        //  .page-footer {
        //    position: fixed;
        //    bottom: 0;
        //    width: 100%;
        //    text-align: center;
        //    font-size: 12px;
        //  }

        //  .page-footer:after {
        //    content: 'صفحہ ' counter(page);
        //  }
        //}
        //</style>
        //</head>

        //<body>

        //<div class='controls'>
        //  رپورٹ کا عنوان:
        //  <input id='reportTitleInput' value='Report' style='width:200px;'>
        //  تلاش:
        //  <input id='searchInput'>
        //  گروپ کریں:
        //  <select id='groupSelect'></select>
        //  فونٹ سائز:
        //  <input type='number' id='fontSizeInput' value='16' style='width:60px;'> px
        //  <button onclick='window.print()'>پرنٹ</button>
        //</div>

        //<div class='column-panel' id='columnPanel'></div>

        //<h1 id='reportTitle'>");
        //            sb.Append(Header);
        //            sb.Append(@"</h1>

        //<div id='printArea'></div>
        //<div class='page-footer'></div>

        //<script>

        //const data = ");
        //            sb.Append(jsonData);
        //            sb.Append(@";

        //const searchInput      = document.getElementById('searchInput');
        //const groupSelect      = document.getElementById('groupSelect');
        //const columnPanel      = document.getElementById('columnPanel');
        //const printArea        = document.getElementById('printArea');
        //const reportTitleInput = document.getElementById('reportTitleInput');
        //const reportTitle      = document.getElementById('reportTitle');
        //const fontSizeInput    = document.getElementById('fontSizeInput');

        //let filteredData   = [...data];
        //let sortState      = { field: null, asc: true };
        //const allColumns   = Object.keys(data[0]);
        //let visibleColumns = {};
        //let columnWidths   = {};

        //init();

        //function init() {
        //  allColumns.forEach(c => {
        //    visibleColumns[c] = true;
        //    columnWidths[c]   = 150;
        //  });

        //  buildGroupDropdown();
        //  buildColumnPanel();

        //  groupSelect.onchange = applyFilter;
        //  searchInput.oninput  = applyFilter;

        //  reportTitleInput.oninput = () => {
        //    reportTitle.textContent = reportTitleInput.value;
        //  };

        //  fontSizeInput.oninput = () => {
        //    const size = parseInt(fontSizeInput.value) || 16;
        //    printArea.style.fontSize = size + 'px';
        //  };

        //  applyFilter();
        //}

        //function buildGroupDropdown() {
        //  groupSelect.innerHTML = '<option value="""">کوئی گروپ نہیں</option>';
        //  allColumns.forEach(c => {
        //    groupSelect.innerHTML += '<option value=""' + c + '"">' + c + '</option>';
        //  });
        //}

        //function buildColumnPanel() {
        //  columnPanel.innerHTML = '<b>کالم دکھائیں:</b><br>';
        //  allColumns.forEach(c => {
        //    columnPanel.innerHTML +=
        //      '<label><input type=""checkbox"" checked onchange=""toggleColumn(' + c + ')""> ' + c + '</label>' +
        //      '<input type=""number"" placeholder=""px"" style=""width:55px"" onchange=""setWidth(' + c + ', this.value)"">';
        //  });
        //}





        //function groupBy(arr, key) {
        //  if (!key) return { '': arr };
        //  return arr.reduce((acc, row) => {
        //    let g = row[key] ?? 'دیگر';
        //    acc[g] = acc[g] || [];
        //    acc[g].push(row);
        //    return acc;
        //  }, {});
        //}

        //function applyFilter() {
        //  const search = searchInput.value.toLowerCase();
        //  filteredData = data.filter(r =>
        //    Object.values(r).join(' ').toLowerCase().includes(search)
        //  );
        //  renderTable(filteredData);
        //}

        //function sortTable(field) {
        //  sortState.asc   = sortState.field === field ? !sortState.asc : true;
        //  sortState.field = field;

        //  filteredData.sort((a, b) => {
        //    const x = a[field] ?? '';
        //    const y = b[field] ?? '';
        //    if (isNumeric(x) && isNumeric(y))
        //      return sortState.asc ? x - y : y - x;
        //    return sortState.asc
        //      ? String(x).localeCompare(String(y), 'ur')
        //      : String(y).localeCompare(String(x), 'ur');
        //  });

        //  renderTable(filteredData);
        //}

        //function toggleColumn(col) {
        //  visibleColumns[col] = !visibleColumns[col];
        //  renderTable(filteredData);
        //}

        //function setWidth(col, val) {
        //  if (!val) {
        //    columnWidths[col] = 'auto';
        //  } else {
        //    let w = parseInt(val);
        //    if (isNaN(w) || w < 40) w = 40;
        //    columnWidths[col] = w + 'px';
        //  }
        //  renderTable(filteredData);
        //}

        //function makeResizable(table) {
        //  const ths = table.querySelectorAll('th');
        //  ths.forEach(th => {
        //    const resizer = document.createElement('div');
        //    resizer.className = 'resizer';
        //    th.appendChild(resizer);

        //    let startX, startWidth;
        //    resizer.addEventListener('mousedown', e => {
        //      startX     = e.pageX;
        //      startWidth = th.offsetWidth;
        //      document.onmousemove = ev => {
        //        const newWidth = startWidth + (startX - ev.pageX);
        //        if (newWidth > 40) th.style.width = newWidth + 'px';
        //      };
        //      document.onmouseup = () => {
        //        document.onmousemove = document.onmouseup = null;
        //      };
        //    });
        //  });
        //}

        //function renderTable(rows) {
        //  const groupKey = groupSelect.value;
        //  const grouped  = groupBy(rows, groupKey);
        //  const columns  = allColumns.filter(c => visibleColumns[c]);

        //  let grandTotals = {};
        //  let grandHasNum = {};
        //  columns.forEach(c => { grandTotals[c] = 0; grandHasNum[c] = false; });

        //  let html = '<table><thead><tr>';
        //  columns.forEach(c => {
        //    html += '<th style=""width:' + columnWidths[c] + '"" onclick=""sortTable(\'' + c + '\')"">' + c + '</th>';
        //  });
        //  html += '</tr></thead><tbody>';

        //  for (const group in grouped) {
        //    if (groupKey) {
        //      html += '<tr class=""group-header""><td colspan=""' + columns.length + '"">' + groupKey + ' : ' + group + '</td></tr>';
        //    }

        //    let subTotals = {};
        //    let subHasNum = {};
        //    columns.forEach(c => { subTotals[c] = 0; subHasNum[c] = false; });

        //    grouped[group].forEach(r => {
        //      html += '<tr>';
        //      columns.forEach(c => {
        //        const val = r[c] ?? '';
        //        html += '<td>' + val + '</td>';
        //        if (isNumeric(val)) {
        //          const num = parseFloat(val);  // ← convert to actual number first
        //          subTotals[c]   += num;
        //          grandTotals[c] += num;
        //          subHasNum[c]    = true;
        //          grandHasNum[c]  = true;
        //        }
        //      });
        //      html += '</tr>';
        //    });

        //    if (groupKey) {
        //      html += '<tr class=""subtotal"">';
        //      columns.forEach((c, i) => {
        //        html += i === 0
        //          ? '<td>سب ٹوٹل</td>'
        //          : '<td>' + (subHasNum[c] ? subTotals[c] : '') + '</td>';
        //      });
        //      html += '</tr>';
        //    }
        //  }

        //  html += '<tr class=""grandtotal"">';
        //  columns.forEach((c, i) => {
        //    html += i === 0
        //      ? '<td>کل مجموعہ</td>'
        //      : '<td>' + (grandHasNum[c] ? grandTotals[c] : '') + '</td>';
        //  });
        //  html += '</tr>';

        //  html += '</tbody></table>';
        //  printArea.innerHTML = html;

        //  makeResizable(printArea.querySelector('table'));
        //}

        //</script>
        //</body>
        //</html>");

        //            return sb.ToString();
        //        }


        public static string GenerateHTMLReportUrdu(DataTable dt, string Header)
        {
            // Convert DataTable to JSON for embedding into HTML
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

            string html = $@"<!doctype html>
        <html lang='ur' dir='rtl'>
        <head>
        <meta charset='utf-8'>
        <title>{Header}</title>
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
        <h1 id=""reportTitle"">{Header}</h1>

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
        function isNumeric(v) {{
            if (v === null || v === undefined || v === '') return false;

            const cleaned = String(v).replace(/,/g, '');
            // Use a stricter check to ensure the whole string is a number
            return !isNaN(cleaned) && !isNaN(parseFloat(cleaned));
        }}

        function parseNumeric(v) {{
            if (v === null || v === undefined || v === '') return 0;

            return parseFloat(String(v).replace(/,/g, '')) || 0;
        }}
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

            grouped[group].forEach(r => {{
          {{
            html += `<tr>`;

            columns.forEach(c => {{
              {{
                let val = r[c] ?? """";

                if (typeof val === 'object' && val !== null) {{
                  {{
                    val = JSON.stringify(val);
                  }}
                }}

                html += `<td>${{val}}</td>`;


                if (isNumeric(val)) {{
                  {{
                    const num = parseNumeric(val);   // convert safely
                    subTotals[c] += num;
                    grandTotals[c] += num;
                  }}
                }}
              }}
            }});

            html += `</tr>`;
          }}
        }});

        html += `<tr class=""subtotal"">`;
        columns.forEach((c, i) => {{
          html += i === 0 ? `<td>سب ٹوٹل</td>` : `<td>${{subTotals[c] !== 0 ? subTotals[c].toLocaleString() : """"}}</td>`;
        }});
        html += `</tr>`;
      }}

      html += `<tr class=""grandtotal"">`;
      columns.forEach((c, i) => {{
        html += i === 0 ? `<td>کل مجموعہ</td>` : `<td>${{grandTotals[c] !== 0 ? grandTotals[c].toLocaleString() : """"}}</td>`;
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

        public static DateTime ChangeDate(MetroDateTime datepicker, int day)
        {
            DateTime iDate;
            iDate = datepicker.Value;
            iDate = iDate.AddDays(day);
            string date = iDate.ToString("dd-MM-yyyy");
            return iDate;
        }
        public static double no_of_Days(int year, int month, int day)
        {
            DateTime today = DateTime.Today;
            DateTime xmas = new DateTime(year, month, day);
            double days = today.Subtract(xmas).TotalDays;
            return days;
        }

        public enum EnumUser
        {
            Client, Customer, LandLoard, Admin, Expense
        }

        public static EnumUser e_User = EnumUser.Client;
        public static string getBillID(EnumUser euser, string date, string userid, int multiplebill_id)
        {
            string cdate = date.Remove('-');
            if (EnumUser.Client == euser)
            {
                return string.Format("1{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.LandLoard == euser)
            {
                return string.Format("2{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.Customer == euser)
            {
                return string.Format("3{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.Expense == euser)
            {
                return string.Format("3{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.Admin == euser)
            {
                return string.Format("Ad{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            //string id= new BLogic().p_getInvoiceID(); 
            return "";
        }
        public static float FloorTo(float value, float interval)
        {
            var remainder = value % interval;
            return value - remainder;
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsFileinUse()
        {
            //new BLogic().closeConnection();
            string path = "..\\ArthiPOS\\bin\\Debug\\db\\db_pt";
            bool blnReturn = false;
            System.IO.FileStream fs;
            try
            {
                fs = System.IO.File.Open(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
                fs.Close();
                return true;
            }
            catch (System.IO.IOException ex)
            {
                blnReturn = false;
            }
            return blnReturn;
        }


    }
}
