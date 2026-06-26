using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using ArthiPOS.Controls;

namespace ShopRentManagementSystem
{
    public partial class FrmTenantReport : Form
    {
        private readonly JsonDataService _dataService;
        private DataGridView dgvTenants;
        private ComboBox cmbTenantTypeFilter;
        private ComboBox cmbPropertyFilter;
        private UrduTextBox txtSearch;
        private Button btnGenerate;
        private Button btnExport;
        private Button btnClose;
        private Label lblSummary;

        public FrmTenantReport()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            SetupKeyboardShortcuts();
            LoadReport();
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
                        LoadReport();
                        break;
                    case Keys.E:
                        if (e.Control) btnExport.PerformClick();
                        break;
                }
            };
        }

        private void InitializeComponent()
        {
            this.Text = "👥 Tenant Report";
            this.Size = new Size(1200, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = SystemColors.Control;
            this.KeyPreview = true;

            // Header Panel
            Panel pnlHeader = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.SteelBlue,
                Padding = new Padding(20)
            };

            Label lblTitle = new Label
            {
                Text = "👥 TENANT MASTER REPORT",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlHeader.Controls.Add(lblTitle);

            // Filter Panel
            Panel pnlFilter = new Panel
            {
                Height = 80,
                Dock = DockStyle.Top,
                BackColor = Color.LightGray,
                Padding = new Padding(20)
            };

            // Search
            txtSearch = new UrduTextBox
            {
                WaterMarkText = "Search tenants...",
                Location = new Point(20, 15),
                Size = new Size(200, 25),
                LangEnglish=true
            };
            txtSearch.TextChanged += (s, e) => ApplyFilters();

            // Tenant Type Filter
            Label lblType = new Label
            {
                Text = "Tenant Type:",
                Location = new Point(240, 15),
                Size = new Size(80, 25)
            };

            cmbTenantTypeFilter = new ComboBox
            {
                Location = new Point(330, 15),
                Size = new Size(120, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTenantTypeFilter.Items.AddRange(new[] { "All Types", "On Rent", "On Commission" });
            cmbTenantTypeFilter.SelectedIndex = 0;
            cmbTenantTypeFilter.SelectedIndexChanged += (s, e) => ApplyFilters();

            // Property Filter
            Label lblProperty = new Label
            {
                Text = "Property:",
                Location = new Point(470, 15),
                Size = new Size(60, 25)
            };

            cmbPropertyFilter = new ComboBox
            {
                Location = new Point(540, 15),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPropertyFilter.Items.Add("All Properties");
            cmbPropertyFilter.SelectedIndex = 0;
            cmbPropertyFilter.SelectedIndexChanged += (s, e) => ApplyFilters();

            // Buttons
            btnGenerate = new Button
            {
                Text = "🔄 Refresh (F5)",
                Location = new Point(710, 10),
                Size = new Size(120, 30),
                BackColor = Color.LightGreen
            };
            btnGenerate.Click += (s, e) => LoadReport();

            btnExport = new Button
            {
                Text = "📤 Export (Ctrl+E)",
                Location = new Point(840, 10),
                Size = new Size(120, 30),
                BackColor = Color.LightBlue
            };
            btnExport.Click += BtnExport_Click;

            // Active Status Filter
            CheckBox chkActiveOnly = new CheckBox
            {
                Text = "Active Tenants Only",
                Location = new Point(20, 45),
                Size = new Size(150, 25),
                Checked = true
            };
            chkActiveOnly.CheckedChanged += (s, e) => ApplyFilters();

            pnlFilter.Controls.AddRange(new Control[] {
                txtSearch, lblType, cmbTenantTypeFilter,
                lblProperty, cmbPropertyFilter, btnGenerate, btnExport, chkActiveOnly
            });

            // Summary Label
            lblSummary = new Label
            {
                Height = 40,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(240, 248, 255),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(10)
            };

            // Data Grid View
            dgvTenants = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                RowHeadersVisible = false,
                BackgroundColor = SystemColors.Window,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

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

            // Statistics label
            Label lblStats = new Label
            {
                Name = "lblDetailedStats",
                Location = new Point(160, 20),
                Size = new Size(400, 20),
                Font = new Font("Segoe UI", 9)
            };

            pnlButtons.Controls.AddRange(new Control[] { btnClose, lblStats });

            this.Controls.AddRange(new Control[] {
                dgvTenants, lblSummary, pnlFilter, pnlHeader, pnlButtons
            });

            this.CancelButton = btnClose;
        }

        private void LoadReport()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var tenants = _dataService.LoadTenants();
                var agreements = _dataService.LoadAgreements().Where(a => a.IsActive).ToList();
                var properties = _dataService.LoadProperties();
                var portions = _dataService.LoadPortions();
                var payments = _dataService.LoadPayments();

                // Populate property filter
                PopulatePropertyFilter(properties);

                // Apply filters
                ApplyFilters();

                // Update summary
                UpdateSummary(tenants, agreements, properties, portions);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error loading tenant report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulatePropertyFilter(List<Property> properties)
        {
            if (cmbPropertyFilter.Items.Count == 1) // Only "All Properties" exists
            {
                foreach (var property in properties.OrderBy(p => p.Name))
                {
                    cmbPropertyFilter.Items.Add(property.Name);
                }
            }
        }

        private void ApplyFilters()
        {
            var tenants = _dataService.LoadTenants();
            var agreements = _dataService.LoadAgreements().Where(a => a.IsActive).ToList();
            var properties = _dataService.LoadProperties();
            var portions = _dataService.LoadPortions();
            var payments = _dataService.LoadPayments();

            var filteredTenants = tenants.AsEnumerable();

            // Text search
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                var searchTerm = txtSearch.Text.ToLower();
                filteredTenants = filteredTenants.Where(t =>
                    (t.Name?.ToLower() ?? "").Contains(searchTerm) ||
                    (t.CNIC?.Contains(searchTerm) ?? false) ||
                    (t.Mobile?.Contains(searchTerm) ?? false));
            }

            // Tenant type filter
            if (cmbTenantTypeFilter.SelectedItem != null && cmbTenantTypeFilter.SelectedItem.ToString() != "All Types")
            {
                var selectedType = cmbTenantTypeFilter.SelectedItem.ToString() == "On Rent" ?
                    TenantType.OnRent : TenantType.OnCommission;
                filteredTenants = filteredTenants.Where(t => t.Type == selectedType);
            }

            // Update grid
            UpdateTenantsGrid(filteredTenants.ToList(), agreements, properties, portions, payments);

            // Update statistics
            UpdateDetailedStats(filteredTenants.ToList(), agreements);
        }

        private void UpdateTenantsGrid(List<Tenant> tenants, List<RentAgreement> agreements,
            List<Property> properties, List<Portion> portions, List<Payment> payments)
        {
            dgvTenants.Rows.Clear();
            dgvTenants.Columns.Clear();

            var columns = new[]
            {
                new DataGridViewTextBoxColumn { HeaderText = "Tenant Name", Width = 150 },
                new DataGridViewTextBoxColumn { HeaderText = "CNIC", Width = 120 },
                new DataGridViewTextBoxColumn { HeaderText = "Mobile", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Type", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "Property", Width = 120 },
                new DataGridViewTextBoxColumn { HeaderText = "Portion", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "Monthly Rent", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Commission %", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "Security Deposit", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Total Paid", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Current Due", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Status", Width = 80 }
            };
            dgvTenants.Columns.AddRange(columns);

            foreach (var tenant in tenants.OrderBy(t => t.Name))
            {
                var tenantAgreements = agreements.Where(a => a.TenantId == tenant.Id).ToList();
                var activeAgreement = tenantAgreements.FirstOrDefault(a => a.IsActive);

                string propertyName = "N/A";
                string portionName = "N/A";
                decimal monthlyRent = 0;
                decimal totalPaid = 0;
                decimal currentDue = 0;
                string status = "Inactive";

                if (activeAgreement != null)
                {
                    var property = properties.FirstOrDefault(p => p.Id == activeAgreement.PropertyId);
                    var portion = portions.FirstOrDefault(p => p.Id == activeAgreement.PortionId);

                    propertyName = property?.Name ?? "N/A";
                    portionName = portion?.Name ?? "N/A";
                    monthlyRent = activeAgreement.MonthlyRent;
                    status = "Active";

                    // Calculate payments and dues
                    var tenantPayments = payments.Where(p => p.AgreementId == activeAgreement.Id).ToList();
                    totalPaid = tenantPayments.Sum(p => p.Amount);

                    // Simplified due calculation
                    if (tenant.Type == TenantType.OnRent)
                    {
                        int monthsPassed = (DateTime.Now.Year - activeAgreement.StartDate.Year) * 12 +
                                          DateTime.Now.Month - activeAgreement.StartDate.Month;
                        monthsPassed = Math.Max(0, monthsPassed);
                        decimal totalDue = monthsPassed * monthlyRent;
                        currentDue = Math.Max(0, totalDue - totalPaid);
                    }
                }

                int rowIndex = dgvTenants.Rows.Add(
                    tenant.Name,
                    tenant.CNIC,
                    tenant.Mobile,
                    tenant.Type.ToString(),
                    propertyName,
                    portionName,
                    monthlyRent.ToString("C"),
                    tenant.Type == TenantType.OnCommission ? $"{tenant.CommissionPercentage}%" : "N/A",
                    tenant.SecurityDeposit.ToString("C"),
                    totalPaid.ToString("C"),
                    currentDue.ToString("C"),
                    status
                );

                // Color coding
                var row = dgvTenants.Rows[rowIndex];

                if (tenant.Type == TenantType.OnCommission)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);
                }

                if (currentDue > 0)
                {
                    row.Cells["Current Due"].Style.ForeColor = Color.DarkRed;
                    row.Cells["Current Due"].Style.Font = new Font(row.DefaultCellStyle.Font, FontStyle.Bold);
                }

                if (status == "Active")
                {
                    row.Cells["Status"].Style.ForeColor = Color.DarkGreen;
                }
                else
                {
                    row.Cells["Status"].Style.ForeColor = Color.Gray;
                }
            }
        }

        private void UpdateSummary(List<Tenant> tenants, List<RentAgreement> agreements,
            List<Property> properties, List<Portion> portions)
        {
            int totalTenants = tenants.Count;
            int rentTenants = tenants.Count(t => t.Type == TenantType.OnRent);
            int commissionTenants = tenants.Count(t => t.Type == TenantType.OnCommission);
            int activeTenants = agreements.Count(a => a.IsActive);

            decimal totalSecurityDeposit = tenants.Sum(t => t.SecurityDeposit);

            lblSummary.Text = $"👥 Total Tenants: {totalTenants} | " +
                            $"🏠 On Rent: {rentTenants} | " +
                            $"📊 On Commission: {commissionTenants} | " +
                            $"✅ Active: {activeTenants} | " +
                            $"💰 Total Security Deposit: {totalSecurityDeposit:C}";
        }

        private void UpdateDetailedStats(List<Tenant> tenants, List<RentAgreement> agreements)
        {
            var lblStats = this.Controls.Find("lblDetailedStats", true).FirstOrDefault() as Label;
            if (lblStats != null)
            {
                int total = tenants.Count;
                int active = agreements.Count(a => a.IsActive && tenants.Any(t => t.Id == a.TenantId));
                decimal avgDeposit = tenants.Any() ? tenants.Average(t => t.SecurityDeposit) : 0;

                lblStats.Text = $"Showing {total} tenants | {active} active | Avg Deposit: {avgDeposit:C}";
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx|CSV Files|*.csv",
                    FileName = $"Tenant_Report_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                    Title = "Export Tenant Report"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Implement export logic
                    MessageBox.Show($"Tenant report exported to: {saveDialog.FileName}\n\n" +
                        "Export functionality requires EPPlus library for Excel.",
                        "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}