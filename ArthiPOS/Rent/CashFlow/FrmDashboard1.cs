using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArthiPOS.Rent.CashFlow
{
    public partial class FrmDashboard1 : Form
    {
        private readonly JsonDataService _dataService;
        private readonly CashFlowService _cashFlowService;
        private CashFlowSummary _currentSummary;
        private DateRange _currentDateRange;
        public FrmDashboard1()
        {
            _dataService = new JsonDataService();
            _cashFlowService = new CashFlowService();
            _currentDateRange = new DateRange();

            InitializeComponent();
            SetupTransactionsGrid();
            LoadDateRanges();

            this.Load += FrmDashboard1_Load;
            this.Resize += FrmDashboard1_Resize;

            LoadDashboardData();
            LoadTree();
            LoadGrid();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetSplitterDistance();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetSplitterDistance();
        }

        private void FrmDashboard1_Load(object sender, EventArgs e)
        {
            SetSplitterDistance();
        }

        private void FrmDashboard1_Resize(object sender, EventArgs e)
        {
            SetSplitterDistance();
        }

        private void SetSplitterDistance()
        {
            if (splitContainer == null) return;
            if (splitContainer.Width <= 0) return;

            int min = splitContainer.Panel1MinSize;
            int max = splitContainer.Width - splitContainer.Panel2MinSize;

            if (max <= min) return;

            int ideal = (int)(splitContainer.Width * 0.30);
            ideal = Math.Max(min, Math.Min(ideal, max));

            try { splitContainer.SplitterDistance = ideal; }
            catch { }
        }

        private void LoadTree()
        {
            tvYearlyBreakdown.Nodes.Clear();

            TreeNode year2025 = new TreeNode("2025");
            year2025.Nodes.Add("January");
            year2025.Nodes.Add("February");
            year2025.Nodes.Add("March");

            TreeNode year2024 = new TreeNode("2024");
            year2024.Nodes.Add("October");
            year2024.Nodes.Add("November");
            year2024.Nodes.Add("December");

            tvYearlyBreakdown.Nodes.Add(year2025);
            tvYearlyBreakdown.Nodes.Add(year2024);
            tvYearlyBreakdown.ExpandAll();
        }

        private void LoadGrid()
        {
            dgvTransactions.Rows.Clear();

            dgvTransactions.Rows.Add(DateTime.Now.ToShortDateString(), "Cash In", "Rent", "Shop Rent Payment", "Shop 101", 25000, "REF001");
            dgvTransactions.Rows.Add(DateTime.Now.ToShortDateString(), "Cash Out", "Expense", "Electric Bill", "WAPDA", 5000, "REF002");
        }

        private void SplitContainer_SizeChanged(object sender, EventArgs e)
        {
            splitContainer.SizeChanged -= SplitContainer_SizeChanged;
            SetSplitterDistance();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            try { SetSplitterDistance(); } catch { }
        }

        // ────────────────────────────────────────────────────────────────
        // Grid setup
        // ────────────────────────────────────────────────────────────────
        private void SetupTransactionsGrid()
        {
            dgvTransactions.Columns.Clear();

            dgvTransactions.Columns.Add("Date", "Date");
            dgvTransactions.Columns.Add("Type", "Type");
            dgvTransactions.Columns.Add("Category", "Category");
            dgvTransactions.Columns.Add("Description", "Description");
            dgvTransactions.Columns.Add("Party", "Tenant/Payee");
            dgvTransactions.Columns.Add("Amount", "Amount");
            dgvTransactions.Columns.Add("Reference", "Reference");

            dgvTransactions.Columns["Date"].Width = 80;
            dgvTransactions.Columns["Type"].Width = 70;
            dgvTransactions.Columns["Category"].Width = 100;
            dgvTransactions.Columns["Description"].Width = 200;
            dgvTransactions.Columns["Party"].Width = 150;
            dgvTransactions.Columns["Amount"].Width = 100;
            dgvTransactions.Columns["Reference"].Width = 100;

            dgvTransactions.Columns["Date"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dgvTransactions.Columns["Amount"].DefaultCellStyle.Format = "C";
            dgvTransactions.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvTransactions.RowPrePaint += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                var cell = dgvTransactions.Rows[e.RowIndex].Cells["Type"].Value;
                if (cell == null) return;
                string type = cell.ToString();
                if (type == "Cash In")
                    dgvTransactions.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);
                else if (type == "Cash Out")
                    dgvTransactions.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
            };
        }

        // ────────────────────────────────────────────────────────────────
        // Date-range combo
        // ────────────────────────────────────────────────────────────────
        private void LoadDateRanges()
        {
            cmbDateRange.Items.Clear();
            foreach (var range in _cashFlowService.GetPredefinedDateRanges())
                cmbDateRange.Items.Add(range);
            cmbDateRange.Items.Add("Custom Range");
            if (cmbDateRange.Items.Count > 0)
                cmbDateRange.SelectedIndex = 0;
        }

        private void CmbDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDateRange.SelectedItem is DateRange selected)
            {
                dtpStartDate.Value = selected.StartDate;
                dtpEndDate.Value = selected.EndDate;
                _currentDateRange = selected;
                LoadDashboardData();
            }
        }

        private void BtnApplyFilter_Click(object sender, EventArgs e)
        {
            _currentDateRange = new DateRange(dtpStartDate.Value, dtpEndDate.Value);
            LoadDashboardData();
            SyncComboSelection();
        }

        private void SyncComboSelection()
        {
            for (int i = 0; i < cmbDateRange.Items.Count - 1; i++)
            {
                if (cmbDateRange.Items[i] is DateRange r &&
                    r.StartDate == _currentDateRange.StartDate &&
                    r.EndDate == _currentDateRange.EndDate)
                {
                    cmbDateRange.SelectedIndex = i;
                    return;
                }
            }
            cmbDateRange.SelectedIndex = cmbDateRange.Items.Count - 1;
        }

        // ────────────────────────────────────────────────────────────────
        // Main data load
        // ────────────────────────────────────────────────────────────────
        private void LoadDashboardData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                _currentSummary = _cashFlowService.GetCashFlowSummary(_currentDateRange);

                lblCashInTotal.Text = _currentSummary.TotalCashIn.ToString("C");
                lblCashOutTotal.Text = _currentSummary.TotalCashOut.ToString("C");
                lblNetCashFlow.Text = _currentSummary.NetCashFlow.ToString("C");
                lblCashInCount.Text = _currentSummary.CashInTransactions.ToString();
                lblCashOutCount.Text = _currentSummary.CashOutTransactions.ToString();
                lblPeriod.Text = $"Period: {_currentSummary.StartDate:dd-MMM-yyyy} to {_currentSummary.EndDate:dd-MMM-yyyy}";

                lblNetCashFlow.ForeColor = _currentSummary.NetCashFlow >= 0
                    ? Color.FromArgb(39, 174, 96)
                    : Color.FromArgb(192, 57, 43);

                BuildYearlyTreeView();
                ShowAllTransactions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        // ────────────────────────────────────────────────────────────────
        // TreeView
        // ────────────────────────────────────────────────────────────────
        private void BuildYearlyTreeView()
        {
            tvYearlyBreakdown.Nodes.Clear();

            foreach (var yearKvp in _currentSummary.YearlyData.OrderByDescending(y => y.Key))
            {
                int year = yearKvp.Key;
                var yearlyData = yearKvp.Value;

                var yearNode = new TreeNode(
                    $"{year} - Cash In: {yearlyData.YearlyCashIn:C} | Cash Out: {yearlyData.YearlyCashOut:C} | Net: {yearlyData.YearlyNetCashFlow:C}")
                {
                    Tag = new { Type = "Year", Year = year },
                    ToolTipText = $"Yearly Total: {yearlyData.YearlyCashIn:C} in, {yearlyData.YearlyCashOut:C} out",
                    ForeColor = yearlyData.YearlyNetCashFlow >= 0 ? Color.DarkGreen : Color.DarkRed
                };

                foreach (var monthKvp in yearlyData.MonthlyData.OrderBy(m => m.Key))
                {
                    int month = monthKvp.Key;
                    var monthlyData = monthKvp.Value;

                    var monthNode = new TreeNode(
                        $"{monthlyData.MonthName} - In: {monthlyData.MonthlyCashIn:C} | Out: {monthlyData.MonthlyCashOut:C} | Net: {monthlyData.MonthlyNetCashFlow:C} " +
                        $"(Txns: {monthlyData.CashInCount} in / {monthlyData.CashOutCount} out)")
                    {
                        Tag = new { Type = "Month", Year = year, Month = month },
                        ToolTipText = $"{monthlyData.CashInCount} income, {monthlyData.CashOutCount} expense transactions",
                        ForeColor = monthlyData.MonthlyNetCashFlow >= 0 ? Color.Green : Color.Red
                    };

                    yearNode.Nodes.Add(monthNode);
                }

                tvYearlyBreakdown.Nodes.Add(yearNode);
            }

            tvYearlyBreakdown.ExpandAll();
        }

        private void TvYearlyBreakdown_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag == null) return;

            dynamic tag = e.Node.Tag;
            if (tag.Type == "Year")
                ShowYearTransactions((int)tag.Year);
            else if (tag.Type == "Month")
                ShowMonthTransactions((int)tag.Year, (int)tag.Month);
        }

        // ────────────────────────────────────────────────────────────────
        // Transaction display helpers
        // ────────────────────────────────────────────────────────────────
        private void ShowAllTransactions()
        {
            dgvTransactions.Rows.Clear();
            foreach (var txn in _currentSummary.RecentTransactions)
                dgvTransactions.Rows.Add(txn.Date, txn.TransactionType, txn.Category,
                    txn.Description, txn.RelatedParty, txn.Amount, txn.Reference);
        }

        private void ShowYearTransactions(int year)
        {
            dgvTransactions.Rows.Clear();
            if (!_currentSummary.YearlyData.TryGetValue(year, out var yearlyData)) return;
            foreach (var monthKvp in yearlyData.MonthlyData)
                AddMonthRows(monthKvp.Value);
            SortByDateDescending();
        }

        private void ShowMonthTransactions(int year, int month)
        {
            dgvTransactions.Rows.Clear();
            if (!_currentSummary.YearlyData.TryGetValue(year, out var yearlyData)) return;
            if (!yearlyData.MonthlyData.TryGetValue(month, out var monthData)) return;
            AddMonthRows(monthData);
            SortByDateDescending();
        }

        private void AddMonthRows(MonthlyCashFlow monthData)
        {
            foreach (var txn in monthData.CashInDetails)
                dgvTransactions.Rows.Add(txn.Date, "Cash In", txn.Category,
                    txn.Description, txn.RelatedParty, txn.Amount, txn.Reference);

            foreach (var txn in monthData.CashOutDetails)
                dgvTransactions.Rows.Add(txn.Date, "Cash Out", txn.Category,
                    txn.Description, txn.RelatedParty, txn.Amount, txn.Reference);
        }

        private void SortByDateDescending()
        {
            if (dgvTransactions.Rows.Count > 0)
                dgvTransactions.Sort(dgvTransactions.Columns["Date"],
                    System.ComponentModel.ListSortDirection.Descending);
        }

        // ────────────────────────────────────────────────────────────────
        // Export / Copy
        // ────────────────────────────────────────────────────────────────
        private void ExportTransactions()
        {
            if (dgvTransactions.Rows.Count == 0)
            {
                MessageBox.Show("No transactions to export.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = $"Transactions_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var writer = new System.IO.StreamWriter(dlg.FileName))
                    {
                        writer.WriteLine("Date,Type,Category,Description,Party,Amount,Reference");
                        foreach (DataGridViewRow row in dgvTransactions.Rows)
                        {
                            if (row.IsNewRow) continue;
                            writer.WriteLine(
                                $"{row.Cells["Date"].Value:dd-MMM-yyyy}," +
                                $"{row.Cells["Type"].Value}," +
                                $"\"{row.Cells["Category"].Value}\"," +
                                $"\"{row.Cells["Description"].Value}\"," +
                                $"\"{row.Cells["Party"].Value}\"," +
                                $"{row.Cells["Amount"].Value}," +
                                $"\"{row.Cells["Reference"].Value}\"");
                        }
                    }
                    MessageBox.Show($"Exported to:\n{dlg.FileName}", "Export Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CopyTransactionsToClipboard()
        {
            if (dgvTransactions.Rows.Count == 0)
            {
                MessageBox.Show("No transactions to copy.", "Copy",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("Date\tType\tCategory\tDescription\tParty\tAmount\tReference");
                foreach (DataGridViewRow row in dgvTransactions.Rows)
                {
                    if (row.IsNewRow) continue;
                    sb.AppendLine(
                        $"{row.Cells["Date"].Value:dd-MMM-yyyy}\t" +
                        $"{row.Cells["Type"].Value}\t" +
                        $"{row.Cells["Category"].Value}\t" +
                        $"{row.Cells["Description"].Value}\t" +
                        $"{row.Cells["Party"].Value}\t" +
                        $"{row.Cells["Amount"].Value}\t" +
                        $"{row.Cells["Reference"].Value}");
                }
                Clipboard.SetText(sb.ToString());
                MessageBox.Show("Copied to clipboard!", "Copy Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error copying: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ════════════════════════════════════════════════════════════════
        // PRINT  –  monthly date-wise breakdown + totals
        // ════════════════════════════════════════════════════════════════
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            var printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDocument_PrintPage;

            using (var preview = new PrintPreviewDialog
            {
                Document = printDoc,
                Width = 900,
                Height = 700,
                StartPosition = FormStartPosition.CenterScreen
            })
            {
                preview.ShowDialog();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // ── Fonts ────────────────────────────────────────────────────
            var titleFont = new Font("Arial", 14, FontStyle.Bold);
            var headingFont = new Font("Arial", 10, FontStyle.Bold);
            var normalFont = new Font("Arial", 9);
            var smallBold = new Font("Arial", 9, FontStyle.Bold);
            var subFont = new Font("Arial", 8, FontStyle.Italic);

            // ── Layout constants ─────────────────────────────────────────
            float left = e.MarginBounds.Left;
            float right = e.MarginBounds.Right;
            float pageWidth = e.MarginBounds.Width;
            float y = e.MarginBounds.Top;
            float lineH = normalFont.GetHeight(e.Graphics) + 2;
            float sectionGap = lineH * 0.6f;

            // Column X positions  (Date | Category | Description | Party | Amount)
            float colDate = left;
            float colCat = left + 80;
            float colDesc = left + 160;
            float colParty = left + 310;
            float colAmt = right - 70;   // right-aligned amount

            // ── Helper: draw a horizontal rule ───────────────────────────
            void HRule(float yPos, float thickness = 0.5f)
            {
                e.Graphics.DrawLine(new Pen(Color.Gray, thickness), left, yPos, right, yPos);
            }

            // ── Helper: draw a row ───────────────────────────────────────
            void DrawRow(Font f, Brush brush,
                         string date, string cat, string desc, string party, decimal amount,
                         bool isOut)
            {
                Brush amtBrush = isOut ? Brushes.DarkRed : Brushes.DarkGreen;
                e.Graphics.DrawString(date, f, brush, colDate, y);
                e.Graphics.DrawString(cat, f, brush, colCat, y);
                e.Graphics.DrawString(desc, f, brush, colDesc, y);
                e.Graphics.DrawString(party, f, brush, colParty, y);

                string amtStr = amount.ToString("N0");
                var amtSize = e.Graphics.MeasureString(amtStr, f);
                e.Graphics.DrawString(amtStr, f, amtBrush, colAmt - amtSize.Width, y);

                y += lineH;
            }

            // ── Title block ──────────────────────────────────────────────
            e.Graphics.DrawString("CASH FLOW STATEMENT",
                titleFont, Brushes.Black,
                left + (pageWidth - e.Graphics.MeasureString("CASH FLOW STATEMENT", titleFont).Width) / 2, y);
            y += titleFont.GetHeight(e.Graphics) + 4;

            e.Graphics.DrawString(
                $"Period: {_currentDateRange.StartDate:dd-MMM-yyyy}  to  {_currentDateRange.EndDate:dd-MMM-yyyy}",
                subFont, Brushes.Gray,
                left + (pageWidth - e.Graphics.MeasureString($"Period: {_currentDateRange.StartDate:dd-MMM-yyyy}  to  {_currentDateRange.EndDate:dd-MMM-yyyy}", subFont).Width) / 2, y);
            y += subFont.GetHeight(e.Graphics) + 2;

            HRule(y, 1.5f); y += 6;

            // ── Column headers ───────────────────────────────────────────
            e.Graphics.DrawString("Date", headingFont, Brushes.Black, colDate, y);
            e.Graphics.DrawString("Category", headingFont, Brushes.Black, colCat, y);
            e.Graphics.DrawString("Description", headingFont, Brushes.Black, colDesc, y);
            e.Graphics.DrawString("Party", headingFont, Brushes.Black, colParty, y);
            e.Graphics.DrawString("Amount", headingFont, Brushes.Black, colAmt - 40, y);
            y += headingFont.GetHeight(e.Graphics) + 2;
            HRule(y, 1f); y += 4;

            // ── Grand totals accumulators ────────────────────────────────
            decimal grandCashIn = 0;
            decimal grandCashOut = 0;

            // ── Iterate years → months (ordered) ────────────────────────
            foreach (var yearKvp in _currentSummary.YearlyData.OrderBy(y2 => y2.Key))
            {
                int year2 = yearKvp.Key;
                var yearData = yearKvp.Value;

                foreach (var monthKvp in yearData.MonthlyData.OrderBy(m => m.Key))
                {
                    var md = monthKvp.Value;

                    // ── Month heading ────────────────────────────────────
                    y += sectionGap;
                    string monthHeading = $"── {md.MonthName} {year2} ──";
                    e.Graphics.DrawString(monthHeading, smallBold, Brushes.DarkSlateBlue, left, y);
                    y += smallBold.GetHeight(e.Graphics) + 2;
                    HRule(y, 0.5f); y += 3;

                    decimal monthCashIn = 0;
                    decimal monthCashOut = 0;

                    // Combine & sort all transactions in this month by date
                    var allTxns = md.CashInDetails
                        .Select(t => new { t.Date, t.Category, t.Description, t.RelatedParty, t.Amount, IsOut = false })
                        .Concat(md.CashOutDetails
                        .Select(t => new { t.Date, t.Category, t.Description, t.RelatedParty, t.Amount, IsOut = true }))
                        .OrderBy(t => t.Date)
                        .ToList();

                    foreach (var txn in allTxns)
                    {
                        DrawRow(normalFont, Brushes.Black,
                            txn.Date.ToString("dd-MMM"),
                            txn.Category ?? "",
                            txn.Description ?? "",
                            txn.RelatedParty ?? "",
                            txn.Amount,
                            txn.IsOut);

                        if (txn.IsOut)
                            monthCashOut += txn.Amount;
                        else
                            monthCashIn += txn.Amount;
                    }

                    // ── Month sub-total ──────────────────────────────────
                    HRule(y, 0.5f); y += 2;

                    decimal monthNet = monthCashIn - monthCashOut;

                    // Cash In subtotal
                    string inLabel = "Month Cash In:";
                    string inValue = monthCashIn.ToString("N0");
                    var inVSize = e.Graphics.MeasureString(inValue, smallBold);
                    e.Graphics.DrawString(inLabel, smallBold, Brushes.DarkGreen, colParty, y);
                    e.Graphics.DrawString(inValue, smallBold, Brushes.DarkGreen, colAmt - inVSize.Width, y);
                    y += smallBold.GetHeight(e.Graphics) + 1;

                    // Cash Out subtotal
                    string outLabel = "Month Cash Out:";
                    string outValue = monthCashOut.ToString("N0");
                    var outVSize = e.Graphics.MeasureString(outValue, smallBold);
                    e.Graphics.DrawString(outLabel, smallBold, Brushes.DarkRed, colParty, y);
                    e.Graphics.DrawString(outValue, smallBold, Brushes.DarkRed, colAmt - outVSize.Width, y);
                    y += smallBold.GetHeight(e.Graphics) + 1;

                    // Net subtotal
                    string netLabel = "Month Net:";
                    string netValue = monthNet.ToString("N0");
                    Brush netBrush = monthNet >= 0 ? Brushes.DarkGreen : Brushes.DarkRed;
                    var netVSize = e.Graphics.MeasureString(netValue, smallBold);
                    e.Graphics.DrawString(netLabel, smallBold, netBrush, colParty, y);
                    e.Graphics.DrawString(netValue, smallBold, netBrush, colAmt - netVSize.Width, y);
                    y += smallBold.GetHeight(e.Graphics) + 1;

                    HRule(y, 0.5f); y += 4;

                    grandCashIn += monthCashIn;
                    grandCashOut += monthCashOut;
                }
            }

            // ════════════════════════════════════════════════════════════
            // GRAND TOTAL BLOCK
            // ════════════════════════════════════════════════════════════
            y += sectionGap;
            HRule(y, 2f); y += 4;

            decimal grandNet = grandCashIn - grandCashOut;

            void DrawGrandRow(string label, decimal value, Brush brush)
            {
                string valStr = value.ToString("N0");
                var valSize = e.Graphics.MeasureString(valStr, headingFont);
                e.Graphics.DrawString(label, headingFont, brush, left, y);
                e.Graphics.DrawString(valStr, headingFont, brush, colAmt - valSize.Width, y);
                y += headingFont.GetHeight(e.Graphics) + 3;
            }

            DrawGrandRow("TOTAL CASH IN :", grandCashIn, Brushes.DarkGreen);
            DrawGrandRow("TOTAL CASH OUT:", grandCashOut, Brushes.DarkRed);

            HRule(y, 1f); y += 3;

            DrawGrandRow("NET CASH FLOW :", grandNet,
                grandNet >= 0 ? Brushes.DarkGreen : Brushes.DarkRed);

            HRule(y, 2f);

            // ── Dispose fonts ────────────────────────────────────────────
            titleFont.Dispose();
            headingFont.Dispose();
            normalFont.Dispose();
            smallBold.Dispose();
            subFont.Dispose();
        }
    }
}
