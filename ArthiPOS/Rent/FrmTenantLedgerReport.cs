using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShopRentManagementSystem
{
    public partial class FrmTenantLedgerReport : Form
    {
        private JsonDataService _dataService;
        private DataGridView dgvLedger;
        private ComboBox cmbTenant;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Button btnGenerate;
        private Button btnPrint;
        private Button btnExport;
        private Label lblSummary;
        private Label lblTenantInfo;
        private Panel pnlHeader;

        public FrmTenantLedgerReport()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            LoadTenants();
        }

        private void InitializeComponent()
        {
            this.Text = "📒 Tenant Ledger Report";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            // Header Panel
            pnlHeader = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = Color.SteelBlue,
                Padding = new Padding(20)
            };

            Label lblTitle = new Label
            {
                Text = "📒 TENANT LEDGER REPORT",
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
                Text = "Select a tenant to view ledger"
            };

            pnlHeader.Controls.Add(lblTenantInfo);
            pnlHeader.Controls.Add(lblTitle);

            // Filter Panel
            Panel pnlFilter = new Panel
            {
                Height = 80,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(240, 240, 240),
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

            Label lblFrom = new Label
            {
                Text = "From:",
                Location = new Point(390, 15),
                Size = new Size(50, 25),
                Font = new Font("Segoe UI", 10)
            };

            dtpFrom = new DateTimePicker
            {
                Location = new Point(450, 12),
                Size = new Size(120, 30),
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10),
                Value = DateTime.Now.AddMonths(-6)
            };

            Label lblTo = new Label
            {
                Text = "To:",
                Location = new Point(580, 15),
                Size = new Size(30, 25),
                Font = new Font("Segoe UI", 10)
            };

            dtpTo = new DateTimePicker
            {
                Location = new Point(620, 12),
                Size = new Size(120, 30),
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10),
                Value = DateTime.Now
            };

            btnGenerate = new Button
            {
                Text = "📊 Generate Report",
                Location = new Point(760, 10),
                Size = new Size(150, 35),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnGenerate.Click += BtnGenerate_Click;

            btnPrint = new Button
            {
                Text = "🖨️ Print",
                Location = new Point(920, 10),
                Size = new Size(100, 35),
                BackColor = Color.LightBlue
            };
            btnPrint.Click += BtnPrint_Click;

            btnExport = new Button
            {
                Text = "📤 Export",
                Location = new Point(1030, 10),
                Size = new Size(100, 35),
                BackColor = Color.LightGoldenrodYellow
            };
            btnExport.Click += BtnExport_Click;

            pnlFilter.Controls.AddRange(new Control[] {
                lblTenant, cmbTenant,
                lblFrom, dtpFrom,
                lblTo, dtpTo,
                btnGenerate, btnPrint, btnExport
            });

            // Ledger Grid
            dgvLedger = new DataGridView
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
                Height = 60,
                BackColor = Color.FromArgb(240, 248, 255),
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

            mainContainer.Controls.Add(dgvLedger);
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
                    // Check if tenant has active agreement
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

                GenerateLedgerReport(tenantItem.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateLedgerReport(int tenantId)
        {
            try
            {
                dgvLedger.Rows.Clear();
                dgvLedger.Columns.Clear();

                // Setup columns for ledger
                dgvLedger.Columns.AddRange(
                    new DataGridViewTextBoxColumn { HeaderText = "Date", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Particulars", Width = 250 },
                    new DataGridViewTextBoxColumn { HeaderText = "Voucher Type", Width = 120 },
                    new DataGridViewTextBoxColumn { HeaderText = "Voucher No.", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Debit", Width = 120 },
                    new DataGridViewTextBoxColumn { HeaderText = "Credit", Width = 120 },
                    new DataGridViewTextBoxColumn { HeaderText = "Balance", Width = 120 },
                    new DataGridViewTextBoxColumn { HeaderText = "Remarks", Width = 200 }
                );

                var tenant = _dataService.LoadTenants().FirstOrDefault(t => t.Id == tenantId);
                if (tenant == null) return;

                var agreements = _dataService.LoadAgreements().Where(a => a.TenantId == tenantId && a.IsActive).ToList();
                var payments = _dataService.LoadPayments()
                    .Where(p => agreements.Any(a => a.Id == p.AgreementId))
                    .OrderBy(p => p.PaymentDate)
                    .ToList();

                // Update header with tenant info
                lblTenantInfo.Text = $"👤 {tenant.Name} | 📱 {tenant.Mobile} | Type: {tenant.Type} | Security Deposit: {tenant.SecurityDeposit:C}";
                pnlHeader.BackColor = tenant.Type == TenantType.OnRent ? Color.SteelBlue : Color.DarkOrange;

                decimal runningBalance = 0;

                // Add security deposit entry
                if (tenant.SecurityDeposit > 0)
                {
                    runningBalance -= tenant.SecurityDeposit; // Negative balance = credit

                    int rowIndex = dgvLedger.Rows.Add(
                        tenant.StampPaperDate.ToString("dd-MMM-yyyy"),
                        "Security Deposit Received",
                        "Receipt",
                        "SD-001",
                        "",
                        tenant.SecurityDeposit.ToString("C"),
                        runningBalance.ToString("C"),
                        $"Stamp Paper: {tenant.StampPaperDetails} dated {tenant.StampPaperDate:dd-MMM-yyyy}"
                    );

                    ColorRow(dgvLedger.Rows[rowIndex], "Deposit");
                }

                foreach (var agreement in agreements)
                {
                    var property = _dataService.LoadProperties().FirstOrDefault(p => p.Id == agreement.PropertyId);
                    var portion = _dataService.LoadPortions().FirstOrDefault(p => p.Id == agreement.PortionId);

                    string propertyInfo = $"{property?.Name ?? "Unknown"} ({portion?.Name ?? "Unknown"})";

                    // Add agreement start entry
                    int startRowIndex = dgvLedger.Rows.Add(
                        agreement.StartDate.ToString("dd-MMM-yyyy"),
                        $"Rent Agreement Started - {propertyInfo}",
                        "Agreement",
                        $"AGR-{agreement.Id}",
                        "",
                        "",
                        runningBalance.ToString("C"),
                        $"Monthly Rent: {agreement.MonthlyRent:C} | Size: {portion?.Size}"
                    );

                    ColorRow(dgvLedger.Rows[startRowIndex], "Agreement");

                    if (tenant.Type == TenantType.OnRent)
                    {
                        // Generate rent charges
                        DateTime currentDate = agreement.StartDate;
                        DateTime endDate = dtpTo.Value;

                        while (currentDate <= endDate)
                        {
                            runningBalance += agreement.MonthlyRent; // Debit increases balance

                            int rentRowIndex = dgvLedger.Rows.Add(
                                currentDate.ToString("dd-MMM-yyyy"),
                                $"Monthly Rent - {currentDate:MMMM yyyy}",
                                "Invoice",
                                $"RENT-{currentDate:yyyyMM}",
                                agreement.MonthlyRent.ToString("C"),
                                "",
                                runningBalance.ToString("C"),
                                $"{propertyInfo}"
                            );

                            ColorRow(dgvLedger.Rows[rentRowIndex], "Rent");

                            currentDate = currentDate.AddMonths(1);
                        }
                    }
                    else if (tenant.Type == TenantType.OnCommission)
                    {
                        // Add commission agreement details
                        int commRowIndex = dgvLedger.Rows.Add(
                            agreement.StartDate.ToString("dd-MMM-yyyy"),
                            $"Commission Agreement - {propertyInfo}",
                            "Agreement",
                            $"COM-AGR-{agreement.Id}",
                            "",
                            "",
                            runningBalance.ToString("C"),
                            $"Commission Rate: {agreement.CommissionRate}% | Frequency: {agreement.PaymentFrequency}"
                        );

                        ColorRow(dgvLedger.Rows[commRowIndex], "Commission");

                        // Add commission charges if any
                        if (agreement.LastCommissionPaymentDate.HasValue)
                        {
                            DateTime lastCommDate = agreement.LastCommissionPaymentDate.Value;
                            DateTime currentDate = lastCommDate;

                            while (currentDate <= dtpTo.Value)
                            {
                                // This is simplified - in real scenario, you'd calculate actual commission
                                decimal estimatedCommission = agreement.CommissionRate.HasValue ?
                                    agreement.CommissionRate.Value * 1000 : 1000;

                                runningBalance += estimatedCommission;

                                int commChargeRowIndex = dgvLedger.Rows.Add(
                                    currentDate.ToString("dd-MMM-yyyy"),
                                    $"Commission Charge - {currentDate:MMMM yyyy}",
                                    "Invoice",
                                    $"COMM-{currentDate:yyyyMM}",
                                    estimatedCommission.ToString("C"),
                                    "",
                                    runningBalance.ToString("C"),
                                    $"Estimated commission for {propertyInfo}"
                                );

                                ColorRow(dgvLedger.Rows[commChargeRowIndex], "Commission");

                                currentDate = currentDate.AddMonths(1);
                            }
                        }
                    }

                    // Add payments for this agreement
                    var agreementPayments = payments.Where(p => p.AgreementId == agreement.Id)
                        .OrderBy(p => p.PaymentDate)
                        .ToList();

                    foreach (var payment in agreementPayments)
                    {
                        // Only include payments within date range
                        if (payment.PaymentDate >= dtpFrom.Value && payment.PaymentDate <= dtpTo.Value)
                        {
                            runningBalance -= payment.Amount; // Credit reduces balance

                            string paymentType = payment.PaymentType switch
                            {
                                PaymentType.Rent => "Rent Payment",
                                PaymentType.Commission => "Commission Payment",
                                PaymentType.SecurityDeposit => "Security Deposit",
                                _ => "Payment"
                            };

                            int paymentRowIndex = dgvLedger.Rows.Add(
                                payment.PaymentDate.ToString("dd-MMM-yyyy"),
                                $"{paymentType} Received",
                                "Receipt",
                                $"RCV-{payment.Id}",
                                "",
                                payment.Amount.ToString("C"),
                                runningBalance.ToString("C"),
                                payment.Notes
                            );

                            var paymentRow = dgvLedger.Rows[paymentRowIndex];

                            if (payment.IsDeleted)
                            {
                                paymentRow.DefaultCellStyle.BackColor = Color.LightGray;
                                paymentRow.DefaultCellStyle.ForeColor = Color.Gray;
                                paymentRow.Cells["Particulars"].Value = $"[DELETED] {paymentRow.Cells["Particulars"].Value}";
                            }
                            else
                            {
                                ColorRow(paymentRow, payment.PaymentType.ToString());
                            }
                        }
                    }
                }

                // Update summary
                UpdateLedgerSummary(tenant, runningBalance);

                // Auto-size columns
                dgvLedger.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating ledger: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorRow(DataGridViewRow row, string type)
        {
            switch (type)
            {
                case "Deposit":
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                    break;
                case "Rent":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230); // Light red
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    break;
                case "Commission":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(230, 240, 255); // Light blue
                    row.DefaultCellStyle.ForeColor = Color.DarkBlue;
                    break;
                case "Agreement":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230); // Light green
                    break;
                case "PaymentType.Rent":
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    break;
                case "PaymentType.Commission":
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                    break;
                case "PaymentType.SecurityDeposit":
                    row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    break;
            }
        }

        private void UpdateLedgerSummary(Tenant tenant, decimal finalBalance)
        {
            var agreements = _dataService.LoadAgreements().Where(a => a.TenantId == tenant.Id && a.IsActive).ToList();
            var payments = _dataService.LoadPayments()
                .Where(p => agreements.Any(a => a.Id == p.AgreementId))
                .ToList();

            decimal totalRentCharges = 0;
            decimal totalCommissionCharges = 0;
            decimal totalPayments = payments.Where(p => !p.IsDeleted).Sum(p => p.Amount);
            decimal securityDeposit = tenant.SecurityDeposit;

            if (tenant.Type == TenantType.OnRent)
            {
                foreach (var agreement in agreements)
                {
                    DateTime currentDate = agreement.StartDate;
                    DateTime endDate = dtpTo.Value;

                    while (currentDate <= endDate)
                    {
                        totalRentCharges += agreement.MonthlyRent;
                        currentDate = currentDate.AddMonths(1);
                    }
                }
            }
            else if (tenant.Type == TenantType.OnCommission)
            {
                // Simplified commission calculation
                totalCommissionCharges = agreements
                    .Where(a => a.CommissionRate.HasValue)
                    .Sum(a => a.CommissionRate.Value * 1000);
            }

            string balanceStatus = finalBalance > 0 ? "⚠️ OVERDUE" : finalBalance < 0 ? "✅ IN CREDIT" : "⚖️ SETTLED";
            Color statusColor = finalBalance > 0 ? Color.DarkRed : finalBalance < 0 ? Color.DarkGreen : Color.DarkBlue;

            lblSummary.Text = $"💰 Final Balance: {finalBalance:C} | {balanceStatus} | " +
                            $"📊 Total Charges: {(totalRentCharges + totalCommissionCharges):C} | " +
                            $"💳 Total Payments: {totalPayments:C} | " +
                            $"🏦 Security Deposit: {securityDeposit:C}";
            lblSummary.ForeColor = statusColor;
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLedger.Rows.Count == 0)
            {
                MessageBox.Show("No data to print. Please generate a report first.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Print preview dialog
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Printing ledger report...\n" +
                    "Print functionality requires proper report formatting.",
                    "Print Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (dgvLedger.Rows.Count == 0)
            {
                MessageBox.Show("No data to export. Please generate a report first.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx|CSV Files|*.csv|PDF Files|*.pdf",
                    FileName = $"Tenant_Ledger_{cmbTenant.Text}_{DateTime.Now:yyyyMMdd}.xlsx",
                    Title = "Export Ledger Report"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveDialog.FilterIndex == 2) // CSV
                    {
                        ExportToCSV(saveDialog.FileName);
                    }
                    else
                    {
                        MessageBox.Show($"Report would be exported to: {saveDialog.FileName}\n\n" +
                            "Excel/PDF export requires additional libraries.\n" +
                            "For CSV: File has been exported successfully.",
                            "Export Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToCSV(string filePath)
        {
            try
            {
                using (var writer = new System.IO.StreamWriter(filePath))
                {
                    // Write header
                    writer.WriteLine("Tenant Ledger Report");
                    writer.WriteLine($"Tenant: {cmbTenant.Text}");
                    writer.WriteLine($"Period: {dtpFrom.Value:dd-MMM-yyyy} to {dtpTo.Value:dd-MMM-yyyy}");
                    writer.WriteLine($"Generated: {DateTime.Now:dd-MMM-yyyy HH:mm}");
                    writer.WriteLine();

                    // Write column headers
                    writer.WriteLine("Date,Particulars,Voucher Type,Voucher No.,Debit,Credit,Balance,Remarks");

                    // Write data
                    foreach (DataGridViewRow row in dgvLedger.Rows)
                    {
                        if (row.IsNewRow) continue;

                        writer.WriteLine(
                            $"\"{row.Cells["Date"].Value}\"," +
                            $"\"{row.Cells["Particulars"].Value}\"," +
                            $"\"{row.Cells["Voucher Type"].Value}\"," +
                            $"\"{row.Cells["Voucher No."].Value}\"," +
                            $"\"{row.Cells["Debit"].Value}\"," +
                            $"\"{row.Cells["Credit"].Value}\"," +
                            $"\"{row.Cells["Balance"].Value}\"," +
                            $"\"{EscapeCsv(row.Cells["Remarks"].Value?.ToString() ?? "")}\""
                        );
                    }
                }

                MessageBox.Show($"Ledger exported successfully to:\n{filePath}", "Export Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to CSV: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string EscapeCsv(string text)
        {
            return text.Replace("\"", "\"\"");
        }

        // Helper class for tenant combo box
        private class TenantItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public TenantType Type { get; set; }

            public override string ToString()
            {
                string typeIcon = Type == TenantType.OnRent ? "💰" : "📈";
                return $"{typeIcon} {Name}";
            }
        }
    }
}