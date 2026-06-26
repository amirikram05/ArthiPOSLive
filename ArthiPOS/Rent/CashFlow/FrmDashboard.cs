using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Printing;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;

namespace ShopRentManagementSystem
{
    public partial class FrmDashboard : Form
    {
        private readonly JsonDataService _dataService;
        private readonly CashFlowService _cashFlowService;
        private CashFlowSummary _currentSummary;
        private DateRange _currentDateRange;

        // Controls
        private ComboBox cmbDateRange;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnApplyFilter;
        private Button btnRefresh;
        private Button btnPrint;

        // Summary Labels
        private Label lblCashInTotal;
        private Label lblCashOutTotal;
        private Label lblNetCashFlow;
        private Label lblCashInCount;
        private Label lblCashOutCount;
        private Label lblPeriod;

        // TreeView for Year/Month grouping
        private TreeView tvYearlyBreakdown;
        private DataGridView dgvTransactions;
        private SplitContainer splitContainer;
        private bool _isHandleCreated = false;

        public FrmDashboard()
        {
            _dataService = new JsonDataService();
            _cashFlowService = new CashFlowService();
            _currentDateRange = new DateRange(); // Default to current year
            InitializeComponent();
            LoadDateRanges();

            // Hook SizeChanged on the SplitContainer itself — fires once it has real dimensions
            this.Load += (s, e) =>
            {
                splitContainer.SizeChanged += SplitContainer_SizeChanged;
            };

            LoadDashboardData();
        }

        private void SplitContainer_SizeChanged(object sender, EventArgs e)
        {
            // Unhook immediately — only need to set once on first valid resize
            splitContainer.SizeChanged -= SplitContainer_SizeChanged;
            SetSplitterDistance();
        }

        private void InitializeComponent()
        {
            this.Text = "Financial Dashboard - Cash Flow Analysis";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Create panels from top to bottom
            CreateTitlePanel();
            CreateFilterPanel();
            CreateSummaryCardsPanel();
            CreateMainContentPanel();

            // Ensure proper layout
            this.PerformLayout();
        }

        private void CreateTitlePanel()
        {
            Panel pnlTitle = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(45, 66, 91),
                Name = "pnlTitle"
            };

            Label lblTitle = new Label
            {
                Text = "💰 CASH FLOW DASHBOARD",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                Size = new Size(400, 35),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblSubTitle = new Label
            {
                Text = "Income (Rent/Commission) vs Expenses",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.LightGray,
                Location = new Point(430, 25),
                Size = new Size(300, 20)
            };

            pnlTitle.Controls.AddRange(new Control[] { lblTitle, lblSubTitle });
            this.Controls.Add(pnlTitle);
        }

        private void CreateFilterPanel()
        {
            Panel pnlFilter = new Panel
            {
                Height = 80,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(10),
                Name = "pnlFilter"
            };

            int xPos = 20;
            int yPos = 15;

            // Date Range ComboBox
            Label lblDateRange = new Label
            {
                Text = "Date Range:",
                Location = new Point(xPos, yPos + 5),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            cmbDateRange = new ComboBox
            {
                Location = new Point(xPos + 85, yPos),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                Name = "cmbDateRange"
            };
            cmbDateRange.SelectedIndexChanged += CmbDateRange_SelectedIndexChanged;

            xPos += 250;

            // Custom Date Range
            Label lblCustom = new Label
            {
                Text = "Custom:",
                Location = new Point(xPos, yPos + 5),
                Size = new Size(60, 25),
                Font = new Font("Segoe UI", 9)
            };
            dtpStartDate = new DateTimePicker
            {
                Location = new Point(xPos + 65, yPos),
                Size = new Size(120, 25),
                Value = _currentDateRange.StartDate,
                Format = DateTimePickerFormat.Short,
                Name = "dtpStartDate"
            };
            Label lblTo = new Label
            {
                Text = "to",
                Location = new Point(xPos + 190, yPos + 5),
                Size = new Size(30, 20)
            };
            dtpEndDate = new DateTimePicker
            {
                Location = new Point(xPos + 220, yPos),
                Size = new Size(120, 25),
                Value = _currentDateRange.EndDate,
                Format = DateTimePickerFormat.Short,
                Name = "dtpEndDate"
            };

            xPos += 360;

            // Apply Button
            btnApplyFilter = new Button
            {
                Text = "Apply Filter",
                Location = new Point(xPos, yPos),
                Size = new Size(100, 28),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Name = "btnApplyFilter"
            };
            btnApplyFilter.Click += BtnApplyFilter_Click;

            xPos += 110;

            btnRefresh = new Button
            {
                Text = "⟳ Refresh",
                Location = new Point(xPos, yPos),
                Size = new Size(80, 28),
                BackColor = Color.LightGray,
                Name = "btnRefresh"
            };
            btnRefresh.Click += (s, e) => LoadDashboardData();

            xPos += 90;

            btnPrint = new Button
            {
                Text = "🖨️ Print",
                Location = new Point(xPos, yPos),
                Size = new Size(80, 28),
                BackColor = Color.LightBlue,
                Name = "btnPrint"
            };
            btnPrint.Click += BtnPrint_Click;

            pnlFilter.Controls.AddRange(new Control[] {
                lblDateRange, cmbDateRange,
                lblCustom, dtpStartDate, lblTo, dtpEndDate,
                btnApplyFilter, btnRefresh, btnPrint
            });

            this.Controls.Add(pnlFilter);
        }

        private void CreateSummaryCardsPanel()
        {
            Panel pnlSummaryCards = new Panel
            {
                Height = 120,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                Padding = new Padding(10),
                Name = "pnlSummaryCards"
            };

            int cardWidth = 200;
            int cardSpacing = 15;

            // Period Label
            lblPeriod = new Label
            {
                Text = $"Period: {_currentDateRange.StartDate:dd-MMM-yyyy} to {_currentDateRange.EndDate:dd-MMM-yyyy}",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.Gray,
                Location = new Point(20, 5),
                Size = new Size(400, 20),
                Name = "lblPeriod"
            };
            pnlSummaryCards.Controls.Add(lblPeriod);

            // Create summary cards
            CreateSummaryCard(pnlSummaryCards, "💰 Total Cash In", "₹0", 20, 35, Color.FromArgb(39, 174, 96), ref lblCashInTotal);
            CreateSummaryCard(pnlSummaryCards, "💸 Total Cash Out", "₹0", 20 + (cardWidth + cardSpacing), 35, Color.FromArgb(192, 57, 43), ref lblCashOutTotal);
            CreateSummaryCard(pnlSummaryCards, "📊 Net Cash Flow", "₹0", 20 + 2 * (cardWidth + cardSpacing), 35, Color.FromArgb(41, 128, 185), ref lblNetCashFlow);
            CreateSummaryCard(pnlSummaryCards, "📋 Cash In Txns", "0", 20 + 3 * (cardWidth + cardSpacing), 35, Color.FromArgb(155, 89, 182), ref lblCashInCount);
            CreateSummaryCard(pnlSummaryCards, "📋 Cash Out Txns", "0", 20 + 4 * (cardWidth + cardSpacing), 35, Color.FromArgb(230, 126, 34), ref lblCashOutCount);

            this.Controls.Add(pnlSummaryCards);
        }

        private void CreateMainContentPanel()
        {
            // Create a container panel for the splitter
            Panel pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                Name = "pnlContent",
                BackColor = Color.FromArgb(240, 240, 240)
            };

            // Create SplitContainer WITHOUT setting SplitterDistance initially
            splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                Panel1MinSize = 300,
                Panel2MinSize = 400,
                SplitterWidth = 8,
                BorderStyle = BorderStyle.Fixed3D,
                Name = "splitContainer"
            };

            

            // Left Panel - Yearly/Monthly TreeView
            Panel pnlLeft = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(250, 250, 250)
            };

            Label lblBreakdown = new Label
            {
                Text = "📅 Yearly & Monthly Breakdown",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue,
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft
            };

            tvYearlyBreakdown = new TreeView
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                ShowNodeToolTips = true,
                HideSelection = false,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Name = "tvYearlyBreakdown"
            };
            tvYearlyBreakdown.AfterSelect += TvYearlyBreakdown_AfterSelect;

            pnlLeft.Controls.AddRange(new Control[] { tvYearlyBreakdown, lblBreakdown });

            // Right Panel - Transactions Grid
            Panel pnlRight = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(250, 250, 250)
            };

            Label lblTransactions = new Label
            {
                Text = "📋 Transaction Details",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue,
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Toolbar for transactions
            Panel pnlTransactionToolbar = new Panel
            {
                Height = 35,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            Button btnExportTransactions = new Button
            {
                Text = "📤 Export",
                Location = new Point(5, 5),
                Size = new Size(80, 25),
                FlatStyle = FlatStyle.Flat
            };
            btnExportTransactions.Click += (s, e) => ExportTransactions();

            Button btnCopyTransactions = new Button
            {
                Text = "📋 Copy",
                Location = new Point(90, 5),
                Size = new Size(80, 25),
                FlatStyle = FlatStyle.Flat
            };
            btnCopyTransactions.Click += (s, e) => CopyTransactionsToClipboard();

            pnlTransactionToolbar.Controls.AddRange(new Control[] { btnExportTransactions, btnCopyTransactions });

            dgvTransactions = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Name = "dgvTransactions"
            };
            SetupTransactionsGrid();

            pnlRight.Controls.AddRange(new Control[] {
                dgvTransactions, pnlTransactionToolbar, lblTransactions
            });

            // Assign panels to split container
            splitContainer.Panel1.Controls.Add(pnlLeft);
            splitContainer.Panel2.Controls.Add(pnlRight);

            // Add split container to content panel
            pnlContent.Controls.Add(splitContainer);

            // Add content panel to form
            this.Controls.Add(pnlContent);
        }

        private void SetSplitterDistance()
        {
            try
            {
                if (splitContainer == null || splitContainer.IsDisposed)
                    return;

                if (!this.IsHandleCreated || !this.Visible)
                    return;

                // Force layout so Width is populated
                splitContainer.PerformLayout();

                int minRequired = splitContainer.Panel1MinSize + splitContainer.Panel2MinSize + splitContainer.SplitterWidth;

                // Width is not ready yet — bail out silently
                if (splitContainer.Width <= minRequired)
                    return;

                int availableWidth = splitContainer.Width - splitContainer.SplitterWidth;
                int newDistance = (int)(availableWidth * 0.4);

                // Clamp strictly within allowed bounds
                newDistance = Math.Max(splitContainer.Panel1MinSize,
                             Math.Min(newDistance, availableWidth - splitContainer.Panel2MinSize));

                if (newDistance != splitContainer.SplitterDistance)
                    splitContainer.SplitterDistance = newDistance;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Splitter error: {ex.Message}");
            }
        }

        private void CreateSummaryCard(Panel parent, string title, string value, int x, int y, Color color, ref Label valueLabel)
        {
            Panel card = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(200, 70),
                BackColor = color,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblTitle = new Label
            {
                Text = title,
                Location = new Point(10, 8),
                Size = new Size(180, 20),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };

            valueLabel = new Label
            {
                Text = value,
                Location = new Point(10, 30),
                Size = new Size(180, 30),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Name = $"lblValue_{Guid.NewGuid()}"
            };

            card.Controls.AddRange(new Control[] { lblTitle, valueLabel });
            parent.Controls.Add(card);
        }

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

            // Set column widths
            dgvTransactions.Columns["Date"].Width = 80;
            dgvTransactions.Columns["Type"].Width = 70;
            dgvTransactions.Columns["Category"].Width = 100;
            dgvTransactions.Columns["Description"].Width = 200;
            dgvTransactions.Columns["Party"].Width = 150;
            dgvTransactions.Columns["Amount"].Width = 100;
            dgvTransactions.Columns["Reference"].Width = 100;

            // Format columns
            dgvTransactions.Columns["Date"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dgvTransactions.Columns["Amount"].DefaultCellStyle.Format = "C";
            dgvTransactions.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Color coding for rows
            dgvTransactions.RowPrePaint += (s, e) =>
            {
                if (e.RowIndex >= 0 && dgvTransactions.Rows[e.RowIndex].Cells["Type"].Value != null)
                {
                    string type = dgvTransactions.Rows[e.RowIndex].Cells["Type"].Value.ToString();
                    if (type == "Cash In")
                        dgvTransactions.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);
                    else if (type == "Cash Out")
                        dgvTransactions.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                }
            };
        }

        private void LoadDateRanges()
        {
            var ranges = _cashFlowService.GetPredefinedDateRanges();
            cmbDateRange.Items.Clear();

            foreach (var range in ranges)
            {
                cmbDateRange.Items.Add(range);
            }

            // Add Custom option
            cmbDateRange.Items.Add("Custom Range");

            if (cmbDateRange.Items.Count > 0)
                cmbDateRange.SelectedIndex = 0; // Year to Date
        }

        private void CmbDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDateRange.SelectedItem is DateRange selectedRange)
            {
                dtpStartDate.Value = selectedRange.StartDate;
                dtpEndDate.Value = selectedRange.EndDate;
                _currentDateRange = selectedRange;
                LoadDashboardData();
            }
            else if (cmbDateRange.SelectedItem?.ToString() == "Custom Range")
            {
                // Do nothing, let user set custom dates
            }
        }

        private void BtnApplyFilter_Click(object sender, EventArgs e)
        {
            _currentDateRange = new DateRange(dtpStartDate.Value, dtpEndDate.Value);
            LoadDashboardData();

            // Update combo box selection if it matches a predefined range
            UpdateComboBoxSelection();
        }

        private void UpdateComboBoxSelection()
        {
            for (int i = 0; i < cmbDateRange.Items.Count - 1; i++)
            {
                if (cmbDateRange.Items[i] is DateRange range)
                {
                    if (range.StartDate == _currentDateRange.StartDate &&
                        range.EndDate == _currentDateRange.EndDate)
                    {
                        cmbDateRange.SelectedIndex = i;
                        return;
                    }
                }
            }
            cmbDateRange.SelectedIndex = cmbDateRange.Items.Count - 1; // Custom Range
        }

        private void LoadDashboardData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                _currentSummary = _cashFlowService.GetCashFlowSummary(_currentDateRange);

                // Update summary cards
                if (lblCashInTotal != null) lblCashInTotal.Text = _currentSummary.TotalCashIn.ToString("C");
                if (lblCashOutTotal != null) lblCashOutTotal.Text = _currentSummary.TotalCashOut.ToString("C");
                if (lblNetCashFlow != null) lblNetCashFlow.Text = _currentSummary.NetCashFlow.ToString("C");
                if (lblCashInCount != null) lblCashInCount.Text = _currentSummary.CashInTransactions.ToString();
                if (lblCashOutCount != null) lblCashOutCount.Text = _currentSummary.CashOutTransactions.ToString();
                if (lblPeriod != null) lblPeriod.Text = $"Period: {_currentSummary.StartDate:dd-MMM-yyyy} to {_currentSummary.EndDate:dd-MMM-yyyy}";

                // Color code net cash flow
                if (lblNetCashFlow != null)
                {
                    if (_currentSummary.NetCashFlow >= 0)
                        lblNetCashFlow.ForeColor = Color.FromArgb(39, 174, 96); // Green
                    else
                        lblNetCashFlow.ForeColor = Color.FromArgb(192, 57, 43); // Red
                }

                // Build TreeView
                BuildYearlyTreeView();

                // Show all transactions initially
                ShowAllTransactions();

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuildYearlyTreeView()
        {
            if (tvYearlyBreakdown == null) return;

            tvYearlyBreakdown.Nodes.Clear();

            foreach (var yearKvp in _currentSummary.YearlyData.OrderByDescending(y => y.Key))
            {
                var year = yearKvp.Key;
                var yearlyData = yearKvp.Value;

                // Year node
                TreeNode yearNode = new TreeNode($"{year} - Cash In: {yearlyData.YearlyCashIn:C} | Cash Out: {yearlyData.YearlyCashOut:C} | Net: {yearlyData.YearlyNetCashFlow:C}");
                yearNode.Tag = new { Type = "Year", Year = year };
                yearNode.ToolTipText = $"Yearly Total: {yearlyData.YearlyCashIn:C} in, {yearlyData.YearlyCashOut:C} out";

                // Color code year node
                if (yearlyData.YearlyNetCashFlow >= 0)
                    yearNode.ForeColor = Color.DarkGreen;
                else
                    yearNode.ForeColor = Color.DarkRed;

                // Add month nodes
                foreach (var monthKvp in yearlyData.MonthlyData.OrderBy(m => m.Key))
                {
                    var month = monthKvp.Key;
                    var monthlyData = monthKvp.Value;

                    TreeNode monthNode = new TreeNode(
                        $"{monthlyData.MonthName} - In: {monthlyData.MonthlyCashIn:C} | Out: {monthlyData.MonthlyCashOut:C} | Net: {monthlyData.MonthlyNetCashFlow:C} (Txns: {monthlyData.CashInCount} in / {monthlyData.CashOutCount} out)"
                    );
                    monthNode.Tag = new { Type = "Month", Year = year, Month = month };
                    monthNode.ToolTipText = $"{monthlyData.CashInCount} income transactions, {monthlyData.CashOutCount} expense transactions";

                    // Color code month node
                    if (monthlyData.MonthlyNetCashFlow >= 0)
                        monthNode.ForeColor = Color.Green;
                    else
                        monthNode.ForeColor = Color.Red;

                    yearNode.Nodes.Add(monthNode);
                }

                tvYearlyBreakdown.Nodes.Add(yearNode);
            }

            // Expand all nodes
            tvYearlyBreakdown.ExpandAll();
        }

        private void TvYearlyBreakdown_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag == null) return;

            dynamic tag = e.Node.Tag;
            string type = tag.Type;

            if (type == "Year")
            {
                ShowYearTransactions(tag.Year);
            }
            else if (type == "Month")
            {
                ShowMonthTransactions(tag.Year, tag.Month);
            }
        }

        private void ShowYearTransactions(int year)
        {
            if (dgvTransactions == null) return;

            dgvTransactions.Rows.Clear();

            if (_currentSummary.YearlyData.ContainsKey(year))
            {
                var yearlyData = _currentSummary.YearlyData[year];

                foreach (var monthKvp in yearlyData.MonthlyData)
                {
                    var monthData = monthKvp.Value;

                    // Add Cash In transactions
                    foreach (var txn in monthData.CashInDetails)
                    {
                        dgvTransactions.Rows.Add(
                            txn.Date,
                            "Cash In",
                            txn.Category,
                            txn.Description,
                            txn.RelatedParty,
                            txn.Amount,
                            txn.Reference
                        );
                    }

                    // Add Cash Out transactions
                    foreach (var txn in monthData.CashOutDetails)
                    {
                        dgvTransactions.Rows.Add(
                            txn.Date,
                            "Cash Out",
                            txn.Category,
                            txn.Description,
                            txn.RelatedParty,
                            txn.Amount,
                            txn.Reference
                        );
                    }
                }

                // Sort by date descending
                if (dgvTransactions.Rows.Count > 0)
                {
                    dgvTransactions.Sort(dgvTransactions.Columns["Date"], System.ComponentModel.ListSortDirection.Descending);
                }
            }
        }

        private void ShowMonthTransactions(int year, int month)
        {
            if (dgvTransactions == null) return;

            dgvTransactions.Rows.Clear();

            if (_currentSummary.YearlyData.ContainsKey(year) &&
                _currentSummary.YearlyData[year].MonthlyData.ContainsKey(month))
            {
                var monthData = _currentSummary.YearlyData[year].MonthlyData[month];

                // Add Cash In transactions
                foreach (var txn in monthData.CashInDetails)
                {
                    dgvTransactions.Rows.Add(
                        txn.Date,
                        "Cash In",
                        txn.Category,
                        txn.Description,
                        txn.RelatedParty,
                        txn.Amount,
                        txn.Reference
                    );
                }

                // Add Cash Out transactions
                foreach (var txn in monthData.CashOutDetails)
                {
                    dgvTransactions.Rows.Add(
                        txn.Date,
                        "Cash Out",
                        txn.Category,
                        txn.Description,
                        txn.RelatedParty,
                        txn.Amount,
                        txn.Reference
                    );
                }

                // Sort by date descending
                if (dgvTransactions.Rows.Count > 0)
                {
                    dgvTransactions.Sort(dgvTransactions.Columns["Date"], System.ComponentModel.ListSortDirection.Descending);
                }
            }
        }

        private void ShowAllTransactions()
        {
            if (dgvTransactions == null) return;

            dgvTransactions.Rows.Clear();

            foreach (var txn in _currentSummary.RecentTransactions)
            {
                dgvTransactions.Rows.Add(
                    txn.Date,
                    txn.TransactionType,
                    txn.Category,
                    txn.Description,
                    txn.RelatedParty,
                    txn.Amount,
                    txn.Reference
                );
            }
        }

        private void ExportTransactions()
        {
            if (dgvTransactions.Rows.Count == 0)
            {
                MessageBox.Show("No transactions to export.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = $"Transactions_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var writer = new System.IO.StreamWriter(saveDialog.FileName))
                    {
                        // Write header
                        writer.WriteLine("Date,Type,Category,Description,Party,Amount,Reference");

                        // Write data
                        foreach (DataGridViewRow row in dgvTransactions.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                writer.WriteLine($"{row.Cells["Date"].Value:dd-MMM-yyyy}," +
                                    $"{row.Cells["Type"].Value}," +
                                    $"\"{row.Cells["Category"].Value}\"," +
                                    $"\"{row.Cells["Description"].Value}\"," +
                                    $"\"{row.Cells["Party"].Value}\"," +
                                    $"{row.Cells["Amount"].Value}," +
                                    $"\"{row.Cells["Reference"].Value}\"");
                            }
                        }
                    }

                    MessageBox.Show($"Transactions exported successfully to:\n{saveDialog.FileName}",
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string clipboardText = "Date\tType\tCategory\tDescription\tParty\tAmount\tReference\n";

                foreach (DataGridViewRow row in dgvTransactions.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        clipboardText += $"{row.Cells["Date"].Value:dd-MMM-yyyy}\t" +
                            $"{row.Cells["Type"].Value}\t" +
                            $"{row.Cells["Category"].Value}\t" +
                            $"{row.Cells["Description"].Value}\t" +
                            $"{row.Cells["Party"].Value}\t" +
                            $"{row.Cells["Amount"].Value}\t" +
                            $"{row.Cells["Reference"].Value}\n";
                    }
                }

                Clipboard.SetText(clipboardText);
                MessageBox.Show("Transactions copied to clipboard!", "Copy Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error copying: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 800,
                Height = 600,
                StartPosition = FormStartPosition.CenterScreen
            };

            previewDialog.ShowDialog();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;

            // Title
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            line = "CASH FLOW STATEMENT";
            yPos = topMargin + count * titleFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(line, titleFont, Brushes.Black, leftMargin, yPos);

            count += 2;

            // Period
            Font normalFont = new Font("Arial", 10);
            line = $"Period: {_currentDateRange.StartDate:dd-MMM-yyyy} to {_currentDateRange.EndDate:dd-MMM-yyyy}";
            yPos = topMargin + count * normalFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(line, normalFont, Brushes.Black, leftMargin, yPos);

            count += 2;

            // Summary
            Font boldFont = new Font("Arial", 12, FontStyle.Bold);

            line = $"Total Cash In: {_currentSummary.TotalCashIn:C}";
            yPos = topMargin + count * boldFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(line, boldFont, Brushes.Green, leftMargin, yPos);
            count++;

            line = $"Total Cash Out: {_currentSummary.TotalCashOut:C}";
            yPos = topMargin + count * boldFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(line, boldFont, Brushes.Red, leftMargin, yPos);
            count++;

            line = $"Net Cash Flow: {_currentSummary.NetCashFlow:C}";
            yPos = topMargin + count * boldFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(line, boldFont,
                _currentSummary.NetCashFlow >= 0 ? Brushes.DarkGreen : Brushes.DarkRed,
                leftMargin, yPos);
            count += 2;

            // Yearly Breakdown
            line = "YEARLY BREAKDOWN";
            yPos = topMargin + count * boldFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(line, boldFont, Brushes.Black, leftMargin, yPos);
            count++;

            foreach (var yearKvp in _currentSummary.YearlyData.OrderByDescending(y => y.Key))
            {
                var year = yearKvp.Key;
                var yearlyData = yearKvp.Value;

                line = $"{year}: In: {yearlyData.YearlyCashIn:C}, Out: {yearlyData.YearlyCashOut:C}, Net: {yearlyData.YearlyNetCashFlow:C}";
                yPos = topMargin + count * normalFont.GetHeight(e.Graphics);
                e.Graphics.DrawString(line, normalFont, Brushes.Black, leftMargin + 20, yPos);
                count++;
            }

            count += 2;

            // Monthly Breakdown (last 6 months only)
            line = "RECENT MONTHLY BREAKDOWN (Last 6 Months)";
            yPos = topMargin + count * boldFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(line, boldFont, Brushes.Black, leftMargin, yPos);
            count++;

            var recentMonths = _currentSummary.YearlyData
                .SelectMany(y => y.Value.MonthlyData)
                .OrderByDescending(m => new DateTime(m.Value.Year, m.Key, 1))
                .Take(6);

            foreach (var monthKvp in recentMonths)
            {
                var monthData = monthKvp.Value;
                line = $"{monthData.MonthName} {monthData.Year}: In: {monthData.MonthlyCashIn:C}, Out: {monthData.MonthlyCashOut:C}, Net: {monthData.MonthlyNetCashFlow:C}";
                yPos = topMargin + count * normalFont.GetHeight(e.Graphics);
                e.Graphics.DrawString(line, normalFont, Brushes.Black, leftMargin + 20, yPos);
                count++;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.WindowState != FormWindowState.Minimized && this.Visible && this.IsHandleCreated)
            {
                try
                {
                    SetSplitterDistance();
                }
                catch
                {
                    // Ignore resize errors
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            try
            {
                SetSplitterDistance();
            }
            catch
            {
                // Ignore errors
            }
        }
    }
}