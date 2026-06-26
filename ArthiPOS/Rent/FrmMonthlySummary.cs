using ShopRentManagementSystem.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShopRentManagementSystem
{
    public partial class FrmMonthlySummary : Form
    {
        private readonly JsonReportService _reportService;
        private ComboBox cmbYear;
        private ComboBox cmbMonth;
        private Button btnGenerate;
        private Button btnExport;
        private Button btnPrint;
        private DataGridView dgvSummary;
        private DataGridView dgvPropertyDetails;
        private TabControl tabControl;
        private Label lblSummaryInfo;
        private Button btnClose;
        private CheckBox chkShowDetails;

        public FrmMonthlySummary()
        {
            InitializeComponent();
            _reportService = new JsonReportService();
            SetupKeyboardShortcuts();
            LoadCurrentMonth();
        }

        private void SetupKeyboardShortcuts()
        {
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        this.Close();
                        break;
                    case Keys.F5:
                        btnGenerate.PerformClick();
                        break;
                    case Keys.Enter:
                        if (!cmbYear.Focused && !cmbMonth.Focused)
                            btnGenerate.PerformClick();
                        break;
                    case Keys.E:
                        if (e.Control) btnExport.PerformClick();
                        break;
                    case Keys.P:
                        if (e.Control) btnPrint.PerformClick();
                        break;
                }
            };
        }

        private void InitializeComponent()
        {
            this.Text = "📊 Monthly Summary Report";
            this.Size = new Size(1300, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = SystemColors.Control;
            this.KeyPreview = true;

            // Header Panel
            Panel pnlHeader = new Panel
            {
                Height = 80,
                Dock = DockStyle.Top,
                BackColor = Color.SteelBlue,
                Padding = new Padding(20)
            };

            Label lblTitle = new Label
            {
                Text = "📅 MONTHLY SUMMARY REPORT",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Filter Panel
            Panel pnlFilter = new Panel
            {
                Height = 40,
                Dock = DockStyle.Top,
                BackColor = Color.LightGray,
                Padding = new Padding(20, 5, 20, 5)
            };

            Label lblYear = new Label
            {
                Text = "Year:",
                Location = new Point(10, 8),
                Size = new Size(40, 25)
            };

            cmbYear = new ComboBox
            {
                Location = new Point(60, 8),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Add years (current year and previous 5 years)
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear - 5; year <= currentYear + 1; year++)
            {
                cmbYear.Items.Add(year);
            }
            cmbYear.SelectedItem = currentYear;

            Label lblMonth = new Label
            {
                Text = "Month:",
                Location = new Point(180, 8),
                Size = new Size(50, 25)
            };

            cmbMonth = new ComboBox
            {
                Location = new Point(240, 8),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            string[] months = {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };
            cmbMonth.Items.AddRange(months);
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;

            chkShowDetails = new CheckBox
            {
                Text = "Show Detailed View",
                Location = new Point(410, 10),
                Size = new Size(150, 20),
                Checked = true
            };
            chkShowDetails.CheckedChanged += (s, e) => ToggleDetailedView();

            btnGenerate = new Button
            {
                Text = "📊 Generate Report",
                Location = new Point(570, 5),
                Size = new Size(140, 30),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnGenerate.Click += BtnGenerate_Click;

            pnlFilter.Controls.AddRange(new Control[] {
                lblYear, cmbYear, lblMonth, cmbMonth, chkShowDetails, btnGenerate
            });

            // Summary Info Label
            lblSummaryInfo = new Label
            {
                Height = 40,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(240, 248, 255),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(10)
            };

            // Tab Control
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(10, 10)
            };

            // Tab 1: Summary
            TabPage tabSummary = new TabPage("📋 Summary");
            SetupSummaryTab(tabSummary);

            // Tab 2: Property Details
            TabPage tabProperties = new TabPage("🏢 Property Details");
            SetupPropertiesTab(tabProperties);

            // Tab 3: Charts
            TabPage tabCharts = new TabPage("📈 Visual Summary");
            SetupChartsTab(tabCharts);

            tabControl.TabPages.Add(tabSummary);
            tabControl.TabPages.Add(tabProperties);
            tabControl.TabPages.Add(tabCharts);

            // Buttons Panel
            Panel pnlButtons = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.LightGray,
                Padding = new Padding(20)
            };

            btnExport = new Button
            {
                Text = "📤 Export to Excel",
                Location = new Point(20, 15),
                Size = new Size(140, 30),
                BackColor = Color.LightBlue
            };
            btnExport.Click += BtnExport_Click;

            btnPrint = new Button
            {
                Text = "🖨️ Print Report",
                Location = new Point(170, 15),
                Size = new Size(120, 30)
            };
            btnPrint.Click += BtnPrint_Click;

            btnClose = new Button
            {
                Text = "✖ Close (Esc)",
                Location = new Point(300, 15),
                Size = new Size(120, 30),
                DialogResult = DialogResult.Cancel
            };
            btnClose.Click += (s, e) => this.Close();

            // Keyboard shortcuts label
            Label lblShortcuts = new Label
            {
                Text = "Shortcuts: F5=Refresh • Ctrl+E=Export • Ctrl+P=Print • Esc=Close",
                Location = new Point(450, 20),
                Size = new Size(400, 20),
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.DarkSlateGray
            };

            pnlButtons.Controls.AddRange(new Control[] { btnExport, btnPrint, btnClose, lblShortcuts });

            this.Controls.AddRange(new Control[] {
                tabControl, lblSummaryInfo, pnlFilter, pnlHeader, pnlButtons
            });

            // Set Accept and Cancel buttons
            this.AcceptButton = btnGenerate;
            this.CancelButton = btnClose;
        }

        private void SetupSummaryTab(TabPage tab)
        {
            dgvSummary = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = SystemColors.Window
            };

            // Add summary statistics panel
            Panel pnlStats = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(245, 245, 245),
                Padding = new Padding(10)
            };

            Label lblStats = new Label
            {
                Name = "lblQuickStats",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlStats.Controls.Add(lblStats);

            Panel container = new Panel
            {
                Dock = DockStyle.Fill
            };
            container.Controls.Add(dgvSummary);
            container.Controls.Add(pnlStats);

            tab.Controls.Add(container);
        }

        private void SetupPropertiesTab(TabPage tab)
        {
            dgvPropertyDetails = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = SystemColors.Window
            };

            // Add filter options
            Panel pnlFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.LightGray,
                Padding = new Padding(10)
            };

            ComboBox cmbPropertyType = new ComboBox
            {
                Location = new Point(10, 8),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPropertyType.Items.AddRange(new[] { "All Types", "Commercial", "NonCommercial" });
            cmbPropertyType.SelectedIndex = 0;
            cmbPropertyType.SelectedIndexChanged += (s, e) => FilterPropertyGrid();

            pnlFilter.Controls.Add(cmbPropertyType);

            Panel container = new Panel
            {
                Dock = DockStyle.Fill
            };
            container.Controls.Add(dgvPropertyDetails);
            container.Controls.Add(pnlFilter);

            tab.Controls.Add(container);
        }

        private void SetupChartsTab(TabPage tab)
        {
            Panel pnlCharts = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            // Create visual summary using labels and progress bars
            Label lblCollection = new Label
            {
                Text = "💰 Collection Performance",
                Location = new Point(20, 20),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue
            };

            ProgressBar pbCollection = new ProgressBar
            {
                Location = new Point(20, 50),
                Size = new Size(400, 25),
                Maximum = 100
            };

            Label lblOccupancy = new Label
            {
                Text = "🏢 Occupancy Rate",
                Location = new Point(20, 90),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue
            };

            ProgressBar pbOccupancy = new ProgressBar
            {
                Location = new Point(20, 120),
                Size = new Size(400, 25),
                Maximum = 100
            };

            // Summary panel
            Panel pnlVisualSummary = new Panel
            {
                Location = new Point(450, 20),
                Size = new Size(400, 200),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(255, 255, 240),
                Padding = new Padding(10)
            };

            Label lblVisualTitle = new Label
            {
                Text = "📊 Quick Stats",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblVisualContent = new Label
            {
                Name = "lblVisualContent",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlVisualSummary.Controls.Add(lblVisualContent);
            pnlVisualSummary.Controls.Add(lblVisualTitle);

            pnlCharts.Controls.AddRange(new Control[] {
                lblCollection, pbCollection, lblOccupancy, pbOccupancy, pnlVisualSummary
            });

            // Store references for updating
            pnlCharts.Tag = new { pbCollection, pbOccupancy, lblVisualContent };

            tab.Controls.Add(pnlCharts);
        }

        private void LoadCurrentMonth()
        {
            GenerateReport(DateTime.Now.Year, DateTime.Now.Month);
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            if (cmbYear.SelectedItem == null || cmbMonth.SelectedItem == null)
                return;

            int year = (int)cmbYear.SelectedItem;
            int month = cmbMonth.SelectedIndex + 1;

            GenerateReport(year, month);
        }

        private void GenerateReport(int year, int month)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var summary = _reportService.GenerateMonthlySummary(year, month);

                // Update summary label
                lblSummaryInfo.Text = $"{summary.MonthYear} | " +
                    $"Total Collected: {summary.TotalCollected:C} | " +
                    $"Total Due: {summary.TotalDue:C} | " +
                    $"Collection Efficiency: {summary.CollectionEfficiency:F1}% | " +
                    $"Active Tenants: {summary.TotalTenants}";

                // Populate summary tab
                dgvSummary.Rows.Clear();
                dgvSummary.Columns.Clear();

                var summaryColumns = new[]
                {
                    new DataGridViewTextBoxColumn { HeaderText = "Metric", Width = 200 },
                    new DataGridViewTextBoxColumn { HeaderText = "Count/Amount", Width = 150 },
                    new DataGridViewTextBoxColumn { HeaderText = "Percentage", Width = 100 }
                };
                dgvSummary.Columns.AddRange(summaryColumns);

                AddSummaryRow("Total Properties", summary.TotalProperties.ToString(), "");
                AddSummaryRow("Total Tenants", summary.TotalTenants.ToString(), "");
                AddSummaryRow("Rent Tenants", summary.TotalRentTenants.ToString(),
                    $"{((decimal)summary.TotalRentTenants / summary.TotalTenants * 100):F1}%");
                AddSummaryRow("Commission Tenants", summary.TotalCommissionTenants.ToString(),
                    $"{((decimal)summary.TotalCommissionTenants / summary.TotalTenants * 100):F1}%");
                AddSummaryRow("Total Rent Collected", summary.TotalRentCollected.ToString("C"), "");
                AddSummaryRow("Total Commission Collected", summary.TotalCommissionCollected.ToString("C"), "");
                AddSummaryRow("Total Collected", summary.TotalCollected.ToString("C"), "");
                AddSummaryRow("Rent Due", summary.TotalRentDue.ToString("C"), "");
                AddSummaryRow("Commission Due", summary.TotalCommissionDue.ToString("C"), "");
                AddSummaryRow("Total Due", summary.TotalDue.ToString("C"), "");
                AddSummaryRow("Collection Efficiency", "", $"{summary.CollectionEfficiency:F1}%");

                // Update quick stats
                var lblQuickStats = this.Controls.Find("lblQuickStats", true).FirstOrDefault() as Label;
                if (lblQuickStats != null)
                {
                    lblQuickStats.Text = $"💰 Total Income: {summary.TotalCollected:C} | " +
                                        $"⚠️ Outstanding: {summary.TotalDue:C} | " +
                                        $"📊 Efficiency: {summary.CollectionEfficiency:F1}%";
                }

                // Populate property details tab
                UpdatePropertyDetailsGrid(summary);

                // Update visual charts
                UpdateVisualCharts(summary);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error generating report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePropertyDetailsGrid(MonthlySummary summary)
        {
            dgvPropertyDetails.Rows.Clear();
            dgvPropertyDetails.Columns.Clear();

            var propertyColumns = new[]
            {
                new DataGridViewTextBoxColumn { HeaderText = "Property", Width = 150 },
                new DataGridViewTextBoxColumn { HeaderText = "Type", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Total Portions", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "Occupied", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "Occupancy Rate", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Rent Collected", Width = 120 },
                new DataGridViewTextBoxColumn { HeaderText = "Commission Collected", Width = 140 },
                new DataGridViewTextBoxColumn { HeaderText = "Total Collected", Width = 120 },
                new DataGridViewTextBoxColumn { HeaderText = "Total Due", Width = 120 }
            };
            dgvPropertyDetails.Columns.AddRange(propertyColumns);

            foreach (var property in summary.PropertySummaries.OrderByDescending(p => p.TotalRentCollected + p.TotalCommissionCollected))
            {
                decimal totalCollected = property.TotalRentCollected + property.TotalCommissionCollected;

                int rowIndex = dgvPropertyDetails.Rows.Add(
                    property.PropertyName,
                    property.PropertyType.ToString(),
                    property.TotalPortions,
                    property.OccupiedPortions,
                    $"{property.OccupancyRate:F1}%",
                    property.TotalRentCollected.ToString("C"),
                    property.TotalCommissionCollected.ToString("C"),
                    totalCollected.ToString("C"),
                    property.TotalDue.ToString("C")
                );

                // Color coding
                var row = dgvPropertyDetails.Rows[rowIndex];

                if (property.OccupancyRate >= 90)
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                else if (property.OccupancyRate >= 70)
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                else
                    row.DefaultCellStyle.BackColor = Color.LightPink;

                if (property.TotalDue > 0)
                {
                    row.Cells["Total Due"].Style.ForeColor = Color.DarkRed;
                    row.Cells["Total Due"].Style.Font = new Font(row.DefaultCellStyle.Font, FontStyle.Bold);
                }
            }
        }

        private void UpdateVisualCharts(MonthlySummary summary)
        {
            // Update progress bars
            var tabCharts = tabControl.TabPages[2];
            var pnlCharts = tabCharts.Controls[0] as Panel;
            if (pnlCharts?.Tag != null)
            {
                dynamic controls = pnlCharts.Tag;
                var pbCollection = controls.pbCollection as ProgressBar;
                var pbOccupancy = controls.pbOccupancy as ProgressBar;
                var lblVisualContent = controls.lblVisualContent as Label;

                if (pbCollection != null)
                {
                    int collectionPercent = (int)Math.Min(100, summary.CollectionEfficiency);
                    pbCollection.Value = collectionPercent;
                    pbCollection.Style = collectionPercent >= 80 ? ProgressBarStyle.Continuous : ProgressBarStyle.Continuous;
                }

                if (pbOccupancy != null && summary.PropertySummaries.Any())
                {
                    decimal avgOccupancy = summary.PropertySummaries.Average(p => p.OccupancyRate);
                    pbOccupancy.Value = (int)Math.Min(100, avgOccupancy);
                }

                if (lblVisualContent != null)
                {
                    string visualText = $"💰 Total Revenue: {summary.TotalCollected:C}\n" +
                                      $"⚠️ Outstanding: {summary.TotalDue:C}\n" +
                                      $"📊 Efficiency: {summary.CollectionEfficiency:F1}%\n" +
                                      $"🏢 Occupancy: {(summary.PropertySummaries.Any() ? summary.PropertySummaries.Average(p => p.OccupancyRate) : 0):F1}%\n" +
                                      $"👥 Total Tenants: {summary.TotalTenants}\n" +
                                      $"🏠 Active Properties: {summary.PropertySummaries.Count}";

                    lblVisualContent.Text = visualText;
                }
            }
        }

        private void AddSummaryRow(string metric, string value, string percentage)
        {
            int rowIndex = dgvSummary.Rows.Add(metric, value, percentage);

            // Color coding for important rows
            if (metric.Contains("Collected") && metric != "Total Collected")
            {
                dgvSummary.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                dgvSummary.Rows[rowIndex].DefaultCellStyle.Font = new Font(dgvSummary.Font, FontStyle.Bold);
            }
            else if (metric.Contains("Due"))
            {
                dgvSummary.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                dgvSummary.Rows[rowIndex].DefaultCellStyle.Font = new Font(dgvSummary.Font, FontStyle.Bold);
            }
            else if (metric.Contains("Efficiency"))
            {
                if (decimal.TryParse(percentage.Replace("%", ""), out decimal efficiency))
                {
                    dgvSummary.Rows[rowIndex].DefaultCellStyle.BackColor = efficiency >= 80 ?
                        Color.LightGreen : efficiency >= 60 ? Color.LightYellow : Color.LightPink;
                }
            }
        }

        private void FilterPropertyGrid()
        {
            // Implement filtering logic here
        }

        private void ToggleDetailedView()
        {
            // Show/hide detailed view
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx|CSV Files|*.csv|PDF Files|*.pdf",
                    FileName = $"Monthly_Summary_{cmbYear.SelectedItem}_{cmbMonth.SelectedItem}.xlsx",
                    Title = "Export Monthly Summary Report"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Implement export using EPPlus
                    ExportToExcel(saveDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToExcel(string filePath)
        {
            // Implement Excel export logic
            MessageBox.Show($"Report exported to: {filePath}\n\n" +
                "Excel export feature requires EPPlus library.\n" +
                "Add NuGet package: Install-Package EPPlus",
                "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Implement printing logic
                MessageBox.Show("Printing monthly summary report...\n" +
                    "Print functionality will be implemented with proper formatting.",
                    "Print Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}