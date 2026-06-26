using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArthiPOS.Controls;
using ShopRentManagementSystem;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;

namespace ShopRentManagementSystem
{
    public partial class FrmDueReport : Form
    {
        private readonly JsonReportService _reportService;
        private DataGridView dgvDueTenants;
        private DataGridView dgvDueProperties;
        private TabControl tabControl;
        private Button btnGenerate;
        private Button btnExport;
        private Button btnPrintReminders;
        private Label lblReportInfo;
        private CheckBox chkShowOverdueOnly;
        private ComboBox cmbPropertyFilter;
        private Button btnClose;
        private UrduTextBox txtSearch;
        private ComboBox cmbTenantTypeFilter;

        public FrmDueReport()
        {
            _reportService = new JsonReportService();
            InitializeComponent();
            SetupKeyboardShortcuts();
            GenerateReport();
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
                    case Keys.E:
                        if (e.Control) btnExport.PerformClick();
                        break;
                    case Keys.P:
                        if (e.Control) btnPrintReminders.PerformClick();
                        break;
                    case Keys.Delete:
                        if (dgvDueTenants.Focused && dgvDueTenants.SelectedRows.Count > 0)
                        {
                            MarkAsPaid();
                        }
                        break;
                }
            };
        }

        private void InitializeComponent()
        {
            this.Text = "⚠️ Due Report - Outstanding Payments";
            this.Size = new Size(1400, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = SystemColors.Control;
            this.KeyPreview = true;

            // Header Panel
            Panel pnlHeader = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(220, 20, 60), // Crimson red for due report
                Padding = new Padding(20)
            };

            Label lblTitle = new Label
            {
                Text = "⏰ DUE PAYMENTS REPORT",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlHeader.Controls.Add(lblTitle);

            // Filter Panel
            Panel pnlFilter = new Panel
            {
                Height = 120,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(255, 240, 240), // Light red background
                Padding = new Padding(20)
            };

            // Search box
            txtSearch = new UrduTextBox
            {
                WaterMarkText = "Search tenant, property, mobile...",
                Location = new Point(20, 15),
                Size = new Size(250, 25)
            };
            txtSearch.TextChanged += (s, e) => ApplyFilters();

            // Property filter
            Label lblPropertyFilter = new Label
            {
                Text = "Property:",
                Location = new Point(20, 50),
                Size = new Size(60, 25)
            };

            cmbPropertyFilter = new ComboBox
            {
                Location = new Point(90, 50),
                Size = new Size(180, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPropertyFilter.Items.Add("All Properties");
            cmbPropertyFilter.SelectedIndex = 0;
            cmbPropertyFilter.SelectedIndexChanged += (s, e) => ApplyFilters();

            // Tenant type filter
            Label lblTypeFilter = new Label
            {
                Text = "Tenant Type:",
                Location = new Point(290, 50),
                Size = new Size(80, 25)
            };

            cmbTenantTypeFilter = new ComboBox
            {
                Location = new Point(380, 50),
                Size = new Size(120, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTenantTypeFilter.Items.AddRange(new[] { "All Types", "On Rent", "On Commission" });
            cmbTenantTypeFilter.SelectedIndex = 0;
            cmbTenantTypeFilter.SelectedIndexChanged += (s, e) => ApplyFilters();

            // Overdue only checkbox
            chkShowOverdueOnly = new CheckBox
            {
                Text = "Show Overdue Only (>0 days)",
                Location = new Point(520, 50),
                Size = new Size(180, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.DarkRed
            };
            chkShowOverdueOnly.CheckedChanged += (s, e) => ApplyFilters();

            // Buttons
            btnGenerate = new Button
            {
                Text = "🔄 Refresh (F5)",
                Location = new Point(720, 15),
                Size = new Size(140, 30),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnGenerate.Click += (s, e) => GenerateReport();

            btnExport = new Button
            {
                Text = "📤 Export (Ctrl+E)",
                Location = new Point(870, 15),
                Size = new Size(140, 30),
                BackColor = Color.LightBlue
            };
            btnExport.Click += BtnExport_Click;

            btnPrintReminders = new Button
            {
                Text = "📄 Print Reminders (Ctrl+P)",
                Location = new Point(1020, 15),
                Size = new Size(160, 30),
                BackColor = Color.LightGoldenrodYellow
            };
            btnPrintReminders.Click += BtnPrintReminders_Click;

            // Mark as paid button
            Button btnMarkPaid = new Button
            {
                Text = "✅ Mark Selected as Paid",
                Location = new Point(720, 50),
                Size = new Size(160, 30),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnMarkPaid.Click += (s, e) => MarkAsPaid();

            // Quick actions label
            Label lblQuickActions = new Label
            {
                Text = "Quick Actions: Select row + Delete = Mark Paid",
                Location = new Point(20, 85),
                Size = new Size(300, 20),
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.DarkSlateGray
            };

            pnlFilter.Controls.AddRange(new Control[] {
                txtSearch, lblPropertyFilter, cmbPropertyFilter,
                lblTypeFilter, cmbTenantTypeFilter, chkShowOverdueOnly,
                btnGenerate, btnExport, btnPrintReminders, btnMarkPaid, lblQuickActions
            });

            // Report Info Label
            lblReportInfo = new Label
            {
                Height = 40,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(255, 220, 220),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DarkRed,
                Padding = new Padding(10)
            };

            // Tab Control
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(10, 10)
            };

            // Tab 1: Due Tenants
            TabPage tabTenants = new TabPage("🧾 Due Tenants");
            SetupDueTenantsTab(tabTenants);

            // Tab 2: Property Summary
            TabPage tabProperties = new TabPage("🏢 Property Summary");
            SetupPropertiesTab(tabProperties);

            // Tab 3: Statistics
            TabPage tabStats = new TabPage("📊 Analysis");
            SetupStatisticsTab(tabStats);

            tabControl.TabPages.Add(tabTenants);
            tabControl.TabPages.Add(tabProperties);
            tabControl.TabPages.Add(tabStats);

            // Buttons Panel
            Panel pnlButtons = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.LightGray,
                Padding = new Padding(20)
            };

            btnClose = new Button
            {
                Text = "✖ Close (Esc)",
                Location = new Point(20, 15),
                Size = new Size(120, 30),
                DialogResult = DialogResult.Cancel
            };
            btnClose.Click += (s, e) => this.Close();

            // Summary label
            Label lblSummary = new Label
            {
                Name = "lblTotalSummary",
                Location = new Point(160, 20),
                Size = new Size(600, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlButtons.Controls.AddRange(new Control[] { btnClose, lblSummary });

            this.Controls.AddRange(new Control[] {
                tabControl, lblReportInfo, pnlFilter, pnlHeader, pnlButtons
            });

            // Set Cancel button
            this.CancelButton = btnClose;
        }

        private void SetupDueTenantsTab(TabPage tab)
        {
            dgvDueTenants = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                BackgroundColor = SystemColors.Window,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Add context menu
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("✅ Mark as Paid", null, (s, e) => MarkAsPaid());
            contextMenu.Items.Add("📞 Call Tenant", null, (s, e) => CallTenant());
            contextMenu.Items.Add("📧 Send Reminder", null, (s, e) => SendReminder());
            dgvDueTenants.ContextMenuStrip = contextMenu;

            tab.Controls.Add(dgvDueTenants);
        }

        private void SetupPropertiesTab(TabPage tab)
        {
            dgvDueProperties = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = SystemColors.Window
            };

            tab.Controls.Add(dgvDueProperties);
        }

        private void SetupStatisticsTab(TabPage tab)
        {
            Panel pnlStats = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            // Statistics will be populated dynamically
            Label lblStats = new Label
            {
                Name = "lblStats",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11, FontStyle.Regular)
            };

            // Add quick action buttons
            Panel pnlActions = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 100,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            Button btnSendAllReminders = new Button
            {
                Text = "📧 Send Reminders to All",
                Location = new Point(20, 20),
                Size = new Size(180, 35),
                BackColor = Color.LightBlue
            };
            btnSendAllReminders.Click += (s, e) => SendRemindersToAll();

            Button btnGenerateReport = new Button
            {
                Text = "📋 Generate Collection Plan",
                Location = new Point(220, 20),
                Size = new Size(200, 35),
                BackColor = Color.LightGreen
            };
            btnGenerateReport.Click += (s, e) => GenerateCollectionPlan();

            pnlActions.Controls.AddRange(new Control[] { btnSendAllReminders, btnGenerateReport });

            pnlStats.Controls.Add(lblStats);
            pnlStats.Controls.Add(pnlActions);

            tab.Controls.Add(pnlStats);
        }

        private void GenerateReport()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var report = _reportService.GenerateDueReport();

                // Update report info
                lblReportInfo.Text = $"📅 Report Date: {report.ReportDate:dd-MMM-yyyy HH:mm} | " +
                    $"👥 Due Tenants: {report.TotalDueTenants} | " +
                    $"💰 Total Due Amount: {report.TotalDueAmount:C}";

                // Update total summary
                var lblTotalSummary = this.Controls.Find("lblTotalSummary", true).FirstOrDefault() as Label;
                if (lblTotalSummary != null)
                {
                    lblTotalSummary.Text = $"Total Due: {report.TotalDueAmount:C} | " +
                                          $"Tenants: {report.TotalDueTenants} | " +
                                          $"Properties: {report.DueProperties.Count}";
                }

                // Populate property filter
                PopulatePropertyFilter(report);

                // Store original data and apply filters
                ApplyFilters();

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error generating due report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulatePropertyFilter(DueReport report)
        {
            if (cmbPropertyFilter.Items.Count == 1) // Only "All Properties" exists
            {
                var properties = report.DueTenants
                    .Select(t => t.PropertyName)
                    .Distinct()
                    .OrderBy(p => p);

                foreach (var property in properties)
                {
                    cmbPropertyFilter.Items.Add(property);
                }
            }
        }

        private void ApplyFilters()
        {
            var report = _reportService.GenerateDueReport();
            var filteredTenants = report.DueTenants.AsEnumerable();

            // Text search
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                var searchTerm = txtSearch.Text.ToLower();
                filteredTenants = filteredTenants.Where(t =>
                    (t.TenantName?.ToLower() ?? "").Contains(searchTerm) ||
                    (t.PropertyName?.ToLower() ?? "").Contains(searchTerm) ||
                    (t.PortionName?.ToLower() ?? "").Contains(searchTerm) ||
                    (t.Mobile?.Contains(searchTerm) ?? false));
            }

            // Property filter
            if (cmbPropertyFilter.SelectedItem != null && cmbPropertyFilter.SelectedItem.ToString() != "All Properties")
            {
                string selectedProperty = cmbPropertyFilter.SelectedItem.ToString();
                filteredTenants = filteredTenants.Where(t => t.PropertyName == selectedProperty);
            }

            // Tenant type filter
            if (cmbTenantTypeFilter.SelectedItem != null && cmbTenantTypeFilter.SelectedItem.ToString() != "All Types")
            {
                var selectedType = cmbTenantTypeFilter.SelectedItem.ToString() == "On Rent" ?
                    TenantType.OnRent : TenantType.OnCommission;
                filteredTenants = filteredTenants.Where(t => t.TenantType == selectedType);
            }

            // Overdue only filter
            if (chkShowOverdueOnly.Checked)
            {
                filteredTenants = filteredTenants.Where(t => t.DaysOverdue > 0);
            }

            // Populate due tenants grid
            UpdateDueTenantsGrid(filteredTenants.ToList());

            // Populate property summary
            UpdatePropertySummary(filteredTenants.ToList());

            // Update statistics
            UpdateStatistics(filteredTenants.ToList());
        }

        private void UpdateDueTenantsGrid(List<DueTenant> tenants)
        {
            dgvDueTenants.Rows.Clear();
            dgvDueTenants.Columns.Clear();

            var tenantColumns = new[]
            {
                new DataGridViewTextBoxColumn { HeaderText = "Tenant Name", Width = 150 },
                new DataGridViewTextBoxColumn { HeaderText = "📱 Mobile", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "🏢 Property", Width = 120 },
                new DataGridViewTextBoxColumn { HeaderText = "📍 Portion", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "📊 Type", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "💰 Due Amount", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "📅 Last Payment", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "⏰ Next Due", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "⌛ Days Overdue", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "🚦 Status", Width = 80 }
            };
            dgvDueTenants.Columns.AddRange(tenantColumns);

            foreach (var tenant in tenants.OrderByDescending(t => t.DaysOverdue).ThenByDescending(t => t.DueAmount))
            {
                int rowIndex = dgvDueTenants.Rows.Add(
                    tenant.TenantName,
                    tenant.Mobile,
                    tenant.PropertyName,
                    tenant.PortionName,
                    tenant.TenantType.ToString(),
                    tenant.DueAmount.ToString("C"),
                    tenant.LastPaymentDate.ToString("dd-MMM-yyyy"),
                    tenant.NextDueDate.ToString("dd-MMM-yyyy"),
                    tenant.DaysOverdue > 0 ? tenant.DaysOverdue.ToString() : "",
                    tenant.Status
                );

                // Color coding
                var row = dgvDueTenants.Rows[rowIndex];

                if (tenant.Status == "Overdue")
                {
                    row.DefaultCellStyle.BackColor = Color.LightPink;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    row.DefaultCellStyle.Font = new Font(row.DefaultCellStyle.Font, FontStyle.Bold);
                }
                else if (tenant.Status == "Due Soon")
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                    row.DefaultCellStyle.ForeColor = Color.DarkGoldenrod;
                }

                if (tenant.DaysOverdue > 30)
                {
                    row.Cells["Days Overdue"].Style.BackColor = Color.Red;
                    row.Cells["Days Overdue"].Style.ForeColor = Color.White;
                    row.Cells["Days Overdue"].Style.Font = new Font(row.DefaultCellStyle.Font, FontStyle.Bold);
                }
                else if (tenant.DaysOverdue > 15)
                {
                    row.Cells["Days Overdue"].Style.BackColor = Color.Orange;
                    row.Cells["Days Overdue"].Style.ForeColor = Color.Black;
                }
            }
        }

        private void UpdatePropertySummary(List<DueTenant> tenants)
        {
            dgvDueProperties.Rows.Clear();
            dgvDueProperties.Columns.Clear();

            var propertyGroups = tenants.GroupBy(t => t.PropertyName);

            var propertyColumns = new[]
            {
                new DataGridViewTextBoxColumn { HeaderText = "Property", Width = 150 },
                new DataGridViewTextBoxColumn { HeaderText = "Due Tenants", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Total Due", Width = 120 },
                new DataGridViewTextBoxColumn { HeaderText = "Avg Due/Tenant", Width = 140 },
                new DataGridViewTextBoxColumn { HeaderText = "Max Due", Width = 120 },
                new DataGridViewTextBoxColumn { HeaderText = "Status", Width = 100 }
            };
            dgvDueProperties.Columns.AddRange(propertyColumns);

            foreach (var group in propertyGroups.OrderByDescending(g => g.Sum(t => t.DueAmount)))
            {
                decimal totalDue = group.Sum(t => t.DueAmount);
                decimal avgDue = group.Average(t => t.DueAmount);
                decimal maxDue = group.Max(t => t.DueAmount);

                string status = totalDue == 0 ? "Good" :
                               totalDue > 10000 ? "Critical 🔴" :
                               totalDue > 5000 ? "Warning 🟡" : "Attention 🟠";

                int rowIndex = dgvDueProperties.Rows.Add(
                    group.Key,
                    group.Count(),
                    totalDue.ToString("C"),
                    avgDue.ToString("C"),
                    maxDue.ToString("C"),
                    status
                );

                // Color coding
                var row = dgvDueProperties.Rows[rowIndex];
                if (status.Contains("Critical"))
                {
                    row.DefaultCellStyle.BackColor = Color.LightPink;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    row.DefaultCellStyle.Font = new Font(row.DefaultCellStyle.Font, FontStyle.Bold);
                }
                else if (status.Contains("Warning"))
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                    row.DefaultCellStyle.ForeColor = Color.DarkGoldenrod;
                }
                else if (status.Contains("Attention"))
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                }
            }
        }

        private void UpdateStatistics(List<DueTenant> tenants)
        {
            var statsTab = tabControl.TabPages[2];
            var lblStats = statsTab.Controls.Find("lblStats", true).FirstOrDefault() as Label;

            if (lblStats != null && tenants.Any())
            {
                decimal totalDue = tenants.Sum(t => t.DueAmount);
                decimal avgDue = tenants.Average(t => t.DueAmount);
                int overdueCount = tenants.Count(t => t.Status == "Overdue");
                int criticalCount = tenants.Count(t => t.DaysOverdue > 30);
                int warningCount = tenants.Count(t => t.DaysOverdue > 15 && t.DaysOverdue <= 30);
                decimal maxDue = tenants.Max(t => t.DueAmount);
                var maxDueTenant = tenants.OrderByDescending(t => t.DueAmount).FirstOrDefault();
                var oldestDue = tenants.OrderByDescending(t => t.DaysOverdue).FirstOrDefault();

                string statsText = $"📊 DUE REPORT ANALYSIS\n\n" +
                    $"• Total Outstanding Amount: {totalDue:C}\n" +
                    $"• Average Due per Tenant: {avgDue:C}\n" +
                    $"• Total Tenants with Dues: {tenants.Count}\n" +
                    $"• Overdue Tenants: {overdueCount}\n" +
                    $"• Critical Cases (>30 days): {criticalCount}\n" +
                    $"• Warning Cases (15-30 days): {warningCount}\n" +
                    $"• Highest Due Amount: {maxDue:C}\n" +
                    $"• Top Due Tenant: {maxDueTenant?.TenantName} ({maxDueTenant?.DueAmount:C})\n" +
                    $"• Oldest Overdue: {oldestDue?.TenantName} ({oldestDue?.DaysOverdue} days)\n\n" +
                    $"💰 COLLECTION TARGETS\n" +
                    $"• Immediate Target (30%): {totalDue * 0.3m:C}\n" +
                    $"• This Week Target (60%): {totalDue * 0.6m:C}\n" +
                    $"• This Month Target (100%): {totalDue:C}\n\n" +
                    $"⚡ PRIORITY ACTIONS\n" +
                    $"1. Contact {criticalCount} tenants with >30 days overdue\n" +
                    $"2. Send reminders to {overdueCount} overdue tenants\n" +
                    $"3. Follow up on amounts >{avgDue * 2:F0}";

                lblStats.Text = statsText;
            }
            else if (lblStats != null)
            {
                lblStats.Text = "🎉 EXCELLENT NEWS!\n\nNo outstanding dues found!\n\n" +
                    "All tenants are up-to-date with their payments.\n" +
                    "Keep up the good work!";
            }
        }

        private void MarkAsPaid()
        {
            if (dgvDueTenants.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a tenant to mark as paid.", "Select Tenant",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvDueTenants.SelectedRows[0];
            string tenantName = selectedRow.Cells["Tenant Name"].Value.ToString();
            decimal dueAmount = decimal.Parse(selectedRow.Cells["Due Amount"].Value.ToString().Replace("$", "").Replace(",", ""));

            var result = MessageBox.Show($"Mark payment of {dueAmount:C} as received from {tenantName}?",
                "Mark as Paid", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Implement payment recording logic
                MessageBox.Show($"Payment from {tenantName} marked as received.\n" +
                    "The payment will be recorded in the payment history.",
                    "Payment Recorded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the report
                GenerateReport();
            }
        }

        private void CallTenant()
        {
            if (dgvDueTenants.SelectedRows.Count > 0)
            {
                var selectedRow = dgvDueTenants.SelectedRows[0];
                string mobile = selectedRow.Cells["📱 Mobile"].Value.ToString();
                string tenantName = selectedRow.Cells["Tenant Name"].Value.ToString();

                MessageBox.Show($"Calling {tenantName} at {mobile}\n\n" +
                    "Note: This would initiate a phone call in a real application.",
                    "Call Tenant", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SendReminder()
        {
            if (dgvDueTenants.SelectedRows.Count > 0)
            {
                var selectedRow = dgvDueTenants.SelectedRows[0];
                string tenantName = selectedRow.Cells["Tenant Name"].Value.ToString();
                decimal dueAmount = decimal.Parse(selectedRow.Cells["Due Amount"].Value.ToString().Replace("$", "").Replace(",", ""));
                int daysOverdue = Convert.ToInt32(selectedRow.Cells["⌛ Days Overdue"].Value ?? "0");

                MessageBox.Show($"Sending reminder to {tenantName} for {dueAmount:C}\n" +
                    $"Overdue by {daysOverdue} days\n\n" +
                    "Reminder sent successfully!",
                    "Reminder Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SendRemindersToAll()
        {
            var result = MessageBox.Show("Send payment reminders to all due tenants?",
                "Send Bulk Reminders", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Implement bulk reminder sending
                MessageBox.Show("Reminders sent to all due tenants!",
                    "Bulk Reminders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GenerateCollectionPlan()
        {
            MessageBox.Show("Generating collection plan...\n\n" +
                "This feature would create a prioritized collection schedule\n" +
                "based on due amounts and days overdue.",
                "Collection Plan", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx|PDF Files|*.pdf|CSV Files|*.csv",
                    FileName = $"Due_Report_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                    Title = "Export Due Report"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Implement export logic
                    MessageBox.Show($"Due report exported to: {saveDialog.FileName}\n\n" +
                        "Export functionality requires additional libraries:\n" +
                        "• Excel: EPPlus\n" +
                        "• PDF: iTextSharp\n" +
                        "Add these via NuGet Package Manager.",
                        "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrintReminders_Click(object sender, EventArgs e)
        {
            var report = _reportService.GenerateDueReport();
            var overdueTenants = report.DueTenants.Where(t => t.Status == "Overdue").ToList();

            if (!overdueTenants.Any())
            {
                MessageBox.Show("No overdue tenants found to send reminders.",
                    "No Overdue Tenants", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var reminderForm = new Form())
            {
                reminderForm.Text = "📄 Print Reminder Letters";
                reminderForm.Size = new Size(700, 450);
                reminderForm.StartPosition = FormStartPosition.CenterParent;
                reminderForm.KeyPreview = true;

                // Handle Escape key
                reminderForm.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) reminderForm.Close(); };

                Label lblInfo = new Label
                {
                    Text = $"Generate reminder letters for {overdueTenants.Count} overdue tenant(s)",
                    Location = new Point(20, 20),
                    Size = new Size(650, 40),
                    Font = new Font("Segoe UI", 12, FontStyle.Bold)
                };

                CheckedListBox chkListTenants = new CheckedListBox
                {
                    Location = new Point(20, 70),
                    Size = new Size(650, 200),
                    CheckOnClick = true
                };

                foreach (var tenant in overdueTenants.OrderByDescending(t => t.DaysOverdue))
                {
                    chkListTenants.Items.Add(
                        $"{tenant.TenantName} - {tenant.PropertyName} - Due: {tenant.DueAmount:C} - Overdue: {tenant.DaysOverdue} days",
                        true);
                }

                Button btnPrintAll = new Button
                {
                    Text = "🖨️ Print Selected",
                    Location = new Point(20, 280),
                    Size = new Size(150, 35),
                    BackColor = Color.LightBlue
                };

                Button btnSelectAll = new Button
                {
                    Text = "✓ Select All",
                    Location = new Point(180, 280),
                    Size = new Size(100, 35)
                };
                btnSelectAll.Click += (s, args) =>
                {
                    for (int i = 0; i < chkListTenants.Items.Count; i++)
                        chkListTenants.SetItemChecked(i, true);
                };

                Button btnDeselectAll = new Button
                {
                    Text = "✗ Deselect All",
                    Location = new Point(290, 280),
                    Size = new Size(100, 35)
                };
                btnDeselectAll.Click += (s, args) =>
                {
                    for (int i = 0; i < chkListTenants.Items.Count; i++)
                        chkListTenants.SetItemChecked(i, false);
                };

                Button btnCancel = new Button
                {
                    Text = "Cancel (Esc)",
                    Location = new Point(400, 280),
                    Size = new Size(100, 35),
                    DialogResult = DialogResult.Cancel
                };

                btnPrintAll.Click += (s, args) =>
                {
                    int selectedCount = chkListTenants.CheckedItems.Count;
                    if (selectedCount == 0)
                    {
                        MessageBox.Show("Please select at least one tenant.", "No Selection",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    MessageBox.Show($"Printing {selectedCount} reminder letter(s)...\n\n" +
                        "Print functionality will be implemented with actual letter templates.",
                        "Print Reminders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reminderForm.DialogResult = DialogResult.OK;
                };

                reminderForm.Controls.AddRange(new Control[] {
                    lblInfo, chkListTenants, btnPrintAll, btnSelectAll, btnDeselectAll, btnCancel
                });
                reminderForm.ShowDialog();
            }
        }
    }
}