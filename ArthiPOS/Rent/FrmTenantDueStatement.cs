using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShopRentManagementSystem
{
    public partial class FrmTenantDueStatement : Form
    {
        private JsonDataService _dataService;
        private DataGridView dgvDueStatement;
        private ComboBox cmbTenant;
        private DateTimePicker dtpAsOfDate;
        private Button btnGenerate;
        private Button btnPrint;
        private Button btnExport;
        private Label lblSummary;
        private Label lblTenantInfo;
        private Panel pnlHeader;

        public FrmTenantDueStatement()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            LoadTenants();
        }

        private void InitializeComponent()
        {
            this.Text = "📝 Tenant Due Statement";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            // Header Panel
            pnlHeader = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = Color.DarkRed,
                Padding = new Padding(20)
            };

            Label lblTitle = new Label
            {
                Text = "📝 TENANT DUE STATEMENT",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblTenantInfo = new Label
            {
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Select a tenant to view due statement"
            };

            pnlHeader.Controls.Add(lblTenantInfo);
            pnlHeader.Controls.Add(lblTitle);

            // Filter Panel
            Panel pnlFilter = new Panel
            {
                Height = 80,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(255, 240, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(20, 10, 20, 10)
            };

            Label lblTenant = new Label
            {
                Text = "Select Tenant:",
                Location = new Point(10, 15),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10)
            };

            cmbTenant = new ComboBox
            {
                Location = new Point(120, 12),
                Size = new Size(250, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };

            Label lblAsOf = new Label
            {
                Text = "As of Date:",
                Location = new Point(390, 15),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10)
            };

            dtpAsOfDate = new DateTimePicker
            {
                Location = new Point(480, 12),
                Size = new Size(120, 30),
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10),
                Value = DateTime.Now
            };

            btnGenerate = new Button
            {
                Text = "📊 Generate Statement",
                Location = new Point(620, 10),
                Size = new Size(180, 35),
                BackColor = Color.LightCoral,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnGenerate.Click += BtnGenerate_Click;

            btnPrint = new Button
            {
                Text = "🖨️ Print",
                Location = new Point(810, 10),
                Size = new Size(100, 35),
                BackColor = Color.LightBlue
            };
            btnPrint.Click += BtnPrint_Click;

            btnExport = new Button
            {
                Text = "📤 Export",
                Location = new Point(920, 10),
                Size = new Size(100, 35),
                BackColor = Color.LightGoldenrodYellow
            };
            btnExport.Click += BtnExport_Click;

            pnlFilter.Controls.AddRange(new Control[] {
                lblTenant, cmbTenant,
                lblAsOf, dtpAsOfDate,
                btnGenerate, btnPrint, btnExport
            });

            // Due Statement Grid
            dgvDueStatement = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Summary Panel
            Panel pnlSummary = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(255, 245, 245),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(20)
            };

            lblSummary = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlSummary.Controls.Add(lblSummary);

            Panel mainContainer = new Panel
            {
                Dock = DockStyle.Fill
            };

            mainContainer.Controls.Add(dgvDueStatement);
            mainContainer.Controls.Add(pnlSummary);

            this.Controls.AddRange(new Control[] { mainContainer, pnlFilter, pnlHeader });
        }

        private void LoadTenants()
        {
            try
            {
                var tenants = _dataService.LoadTenants();
                var agreements = _dataService.LoadAgreements().Where(a => a.IsActive).ToList();

                cmbTenant.Items.Clear();
                cmbTenant.Items.Add("-- Select Tenant --");

                foreach (var tenant in tenants.OrderBy(t => t.Name))
                {
                    bool hasActiveAgreement = agreements.Any(a => a.TenantId == tenant.Id);
                    if (hasActiveAgreement)
                    {
                        cmbTenant.Items.Add(new TenantItem
                        {
                            Id = tenant.Id,
                            Name = tenant.Name,
                            Type = tenant.Type
                        });
                    }
                }

                if (cmbTenant.Items.Count > 0)
                    cmbTenant.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tenants: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTenant.SelectedItem == null || cmbTenant.SelectedIndex == 0)
                {
                    MessageBox.Show("Please select a tenant.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var tenantItem = cmbTenant.SelectedItem as TenantItem;
                if (tenantItem == null) return;

                GenerateDueStatement(tenantItem.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating statement: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateDueStatement(int tenantId)
        {
            try
            {
                dgvDueStatement.Rows.Clear();
                dgvDueStatement.Columns.Clear();

                // Setup columns for due statement
                dgvDueStatement.Columns.AddRange(
                    new DataGridViewTextBoxColumn { HeaderText = "Description", Width = 300 },
                    new DataGridViewTextBoxColumn { HeaderText = "Due Date", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Days Overdue", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Amount Due", Width = 150 },
                    new DataGridViewTextBoxColumn { HeaderText = "Status", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Remarks", Width = 200 }
                );

                var tenant = _dataService.LoadTenants().FirstOrDefault(t => t.Id == tenantId);
                if (tenant == null) return;

                var agreements = _dataService.LoadAgreements().Where(a => a.TenantId == tenantId && a.IsActive).ToList();
                var payments = _dataService.LoadPayments()
                    .Where(p => agreements.Any(a => a.Id == p.AgreementId))
                    .ToList();

                // Update header with tenant info
                lblTenantInfo.Text = $"👤 {tenant.Name} | 📱 {tenant.Mobile} | Type: {tenant.Type} | Statement Date: {dtpAsOfDate.Value:dd-MMM-yyyy}";

                decimal totalDue = 0;
                int totalOverdueDays = 0;
                int dueItems = 0;

                // Add header row
                int headerRow = dgvDueStatement.Rows.Add(
                    $"📋 DUE STATEMENT FOR: {tenant.Name.ToUpper()}",
                    "",
                    "",
                    "",
                    "",
                    $"Generated on: {DateTime.Now:dd-MMM-yyyy HH:mm}"
                );
                dgvDueStatement.Rows[headerRow].DefaultCellStyle.BackColor = Color.LightGray;
                dgvDueStatement.Rows[headerRow].DefaultCellStyle.Font = new Font(dgvDueStatement.Font, FontStyle.Bold);

                foreach (var agreement in agreements)
                {
                    var property = _dataService.LoadProperties().FirstOrDefault(p => p.Id == agreement.PropertyId);
                    var portion = _dataService.LoadPortions().FirstOrDefault(p => p.Id == agreement.PortionId);

                    string propertyInfo = $"{property?.Name ?? "Unknown"} ({portion?.Name ?? "Unknown"})";

                    // Add agreement header
                    int agreementHeader = dgvDueStatement.Rows.Add(
                        $"🏢 Property: {propertyInfo}",
                        "",
                        "",
                        "",
                        "",
                        $"Agreement #: {agreement.Id} | Start: {agreement.StartDate:dd-MMM-yyyy}"
                    );
                    dgvDueStatement.Rows[agreementHeader].DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

                    if (tenant.Type == TenantType.OnRent)
                    {
                        // Calculate rent dues
                        DateTime currentDate = agreement.StartDate;
                        DateTime asOfDate = dtpAsOfDate.Value;

                        while (currentDate <= asOfDate)
                        {
                            DateTime dueDate = currentDate;
                            decimal monthlyRent = agreement.MonthlyRent;

                            // Check if rent for this month is paid
                            bool isPaid = payments.Any(p =>
                                p.AgreementId == agreement.Id &&
                                p.PaymentType == PaymentType.Rent &&
                                p.PaymentDate.Year == dueDate.Year &&
                                p.PaymentDate.Month == dueDate.Month &&
                                !p.IsDeleted);

                            if (!isPaid)
                            {
                                int daysOverdue = (asOfDate - dueDate).Days;
                                string status = daysOverdue > 0 ? "⚠️ OVERDUE" : "📅 DUE";

                                int dueRow = dgvDueStatement.Rows.Add(
                                    $"Monthly Rent - {dueDate:MMMM yyyy}",
                                    dueDate.ToString("dd-MMM-yyyy"),
                                    daysOverdue > 0 ? daysOverdue.ToString() : "",
                                    monthlyRent.ToString("C"),
                                    status,
                                    $"Rent for {propertyInfo}"
                                );

                                // Color code based on overdue status
                                var row = dgvDueStatement.Rows[dueRow];
                                if (daysOverdue > 30)
                                    row.DefaultCellStyle.BackColor = Color.LightPink;
                                else if (daysOverdue > 0)
                                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                                else
                                    row.DefaultCellStyle.BackColor = Color.LightGreen;

                                totalDue += monthlyRent;
                                totalOverdueDays = Math.Max(totalOverdueDays, daysOverdue);
                                dueItems++;
                            }

                            currentDate = currentDate.AddMonths(1);
                        }
                    }
                    else if (tenant.Type == TenantType.OnCommission)
                    {
                        // Calculate commission dues
                        if (agreement.LastCommissionPaymentDate.HasValue)
                        {
                            DateTime lastPaymentDate = agreement.LastCommissionPaymentDate.Value;
                            DateTime nextDueDate = lastPaymentDate;

                            if (agreement.PaymentFrequency.HasValue)
                            {
                                int frequencyDays = GetFrequencyDays(agreement.PaymentFrequency.Value, agreement.CustomPaymentDays);

                                while (nextDueDate <= dtpAsOfDate.Value)
                                {
                                    nextDueDate = nextDueDate.AddDays(frequencyDays);

                                    if (nextDueDate <= dtpAsOfDate.Value)
                                    {
                                        int daysOverdue = (dtpAsOfDate.Value - nextDueDate).Days;
                                        decimal commissionAmount = agreement.CommissionRate.HasValue ?
                                            agreement.CommissionRate.Value * 1000 : 1000;

                                        string status = daysOverdue > 0 ? "⚠️ OVERDUE" : "📅 DUE";

                                        int dueRow = dgvDueStatement.Rows.Add(
                                            $"Commission Payment - {nextDueDate:dd-MMM-yyyy}",
                                            nextDueDate.ToString("dd-MMM-yyyy"),
                                            daysOverdue > 0 ? daysOverdue.ToString() : "",
                                            commissionAmount.ToString("C"),
                                            status,
                                            $"Commission for {propertyInfo} | Rate: {agreement.CommissionRate}%"
                                        );

                                        var row = dgvDueStatement.Rows[dueRow];
                                        if (daysOverdue > 0)
                                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                                        else
                                            row.DefaultCellStyle.BackColor = Color.LightBlue;

                                        totalDue += commissionAmount;
                                        totalOverdueDays = Math.Max(totalOverdueDays, daysOverdue);
                                        dueItems++;
                                    }
                                }
                            }
                        }
                    }

                    // Add separator
                    dgvDueStatement.Rows.Add("", "", "", "", "", "");
                }

                // Add total row
                int totalRow = dgvDueStatement.Rows.Add(
                    "💰 TOTAL AMOUNT DUE",
                    "",
                    $"Max Overdue: {totalOverdueDays} days",
                    totalDue.ToString("C"),
                    dueItems > 0 ? "⚠️ ACTION REQUIRED" : "✅ ALL CLEAR",
                    $"Total Due Items: {dueItems}"
                );

                dgvDueStatement.Rows[totalRow].DefaultCellStyle.BackColor =
                    totalDue > 0 ? Color.LightCoral : Color.LightGreen;
                dgvDueStatement.Rows[totalRow].DefaultCellStyle.Font =
                    new Font(dgvDueStatement.Font, FontStyle.Bold);

                // Update summary
                UpdateDueSummary(tenant, totalDue, totalOverdueDays, dueItems);

                // Auto-size columns
                dgvDueStatement.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating due statement: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetFrequencyDays(CommissionFrequency frequency, int? customDays)
        {
            return frequency switch
            {
                CommissionFrequency.Daily => 1,
                CommissionFrequency.Every5Days => 5,
                CommissionFrequency.Every10Days => 10,
                CommissionFrequency.Weekly => 7,
                CommissionFrequency.Monthly => 30,
                CommissionFrequency.Custom => customDays ?? 7,
                _ => 7
            };
        }

        private void UpdateDueSummary(Tenant tenant, decimal totalDue, int maxOverdueDays, int dueItems)
        {
            var agreements = _dataService.LoadAgreements().Where(a => a.TenantId == tenant.Id && a.IsActive).ToList();
            int activeProperties = agreements.Count;

            string status;
            Color statusColor;

            if (totalDue == 0)
            {
                status = "✅ NO DUES";
                statusColor = Color.DarkGreen;
                pnlHeader.BackColor = Color.DarkGreen;
            }
            else if (maxOverdueDays > 30)
            {
                status = "🚨 SERIOUSLY OVERDUE";
                statusColor = Color.DarkRed;
                pnlHeader.BackColor = Color.DarkRed;
            }
            else if (maxOverdueDays > 0)
            {
                status = "⚠️ OVERDUE";
                statusColor = Color.DarkOrange;
                pnlHeader.BackColor = Color.DarkOrange;
            }
            else
            {
                status = "📅 UPCOMING DUE";
                statusColor = Color.DarkBlue;
                pnlHeader.BackColor = Color.DarkBlue;
            }

            lblSummary.Text = $"{status} | " +
                            $"💰 Total Due: {totalDue:C} | " +
                            $"📋 Due Items: {dueItems} | " +
                            $"📅 Max Overdue: {maxOverdueDays} days | " +
                            $"🏢 Active Properties: {activeProperties}";
            lblSummary.ForeColor = statusColor;
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (dgvDueStatement.Rows.Count == 0)
            {
                MessageBox.Show("No data to print. Please generate a statement first.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Printing due statement...\n" +
                    "This would generate a formal due statement document.",
                    "Print Statement", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (dgvDueStatement.Rows.Count == 0)
            {
                MessageBox.Show("No data to export. Please generate a statement first.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf|Excel Files|*.xlsx|CSV Files|*.csv",
                    FileName = $"Due_Statement_{cmbTenant.Text}_{DateTime.Now:yyyyMMdd}.pdf",
                    Title = "Export Due Statement"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show($"Due statement would be exported to: {saveDialog.FileName}\n\n" +
                        "Export functionality requires report generation libraries.",
                        "Export Statement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper class for tenant combo box
        private class TenantItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public TenantType Type { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}