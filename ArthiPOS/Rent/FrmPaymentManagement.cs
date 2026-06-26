using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShopRentManagementSystem
{
    public partial class FrmPaymentManagement : Form
    {
        private JsonDataService _dataService;
        private DataGridView dgvPayments;
        private ComboBox cmbFilterType;
        private ComboBox cmbFilterStatus;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private TextBox txtSearch;
        private Button btnDelete;
        private Button btnRestore;
        private Button btnRefresh;
        private Button btnExport;
        private Label lblSummary;

        public FrmPaymentManagement()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            LoadPayments();
        }

        private void InitializeComponent()
        {
            this.Text = "⚙️ Payment Management";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            // Header
            Panel pnlHeader = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.SteelBlue,
                Padding = new Padding(20)
            };

            Label lblTitle = new Label
            {
                Text = "💰 Payment Management (View/Delete/Restore)",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlHeader.Controls.Add(lblTitle);

            // Filter Panel
            Panel pnlFilter = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(20, 10, 20, 10)
            };

            // Date Range
            Label lblFrom = new Label
            {
                Text = "From:",
                Location = new Point(10, 15),
                Size = new Size(40, 25)
            };

            dtpFrom = new DateTimePicker
            {
                Location = new Point(60, 12),
                Size = new Size(120, 30),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now.AddMonths(-1)
            };
            dtpFrom.ValueChanged += (s, e) => LoadPayments();

            Label lblTo = new Label
            {
                Text = "To:",
                Location = new Point(190, 15),
                Size = new Size(30, 25)
            };

            dtpTo = new DateTimePicker
            {
                Location = new Point(230, 12),
                Size = new Size(120, 30),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now
            };
            dtpTo.ValueChanged += (s, e) => LoadPayments();

            // Payment Type Filter
            Label lblType = new Label
            {
                Text = "Type:",
                Location = new Point(370, 15),
                Size = new Size(40, 25)
            };

            cmbFilterType = new ComboBox
            {
                Location = new Point(420, 12),
                Size = new Size(120, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbFilterType.Items.AddRange(new[] { "All Types", "Rent", "Commission", "Security Deposit", "Other" });
            cmbFilterType.SelectedIndex = 0;
            cmbFilterType.SelectedIndexChanged += (s, e) => LoadPayments();

            // Status Filter
            Label lblStatus = new Label
            {
                Text = "Status:",
                Location = new Point(550, 15),
                Size = new Size(50, 25)
            };

            cmbFilterStatus = new ComboBox
            {
                Location = new Point(610, 12),
                Size = new Size(120, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbFilterStatus.Items.AddRange(new[] { "All", "Active", "Deleted" });
            cmbFilterStatus.SelectedIndex = 0;
            cmbFilterStatus.SelectedIndexChanged += (s, e) => LoadPayments();

            // Search
            Label lblSearch = new Label
            {
                Text = "Search:",
                Location = new Point(10, 55),
                Size = new Size(50, 25)
            };

            txtSearch = new TextBox
            {
                Location = new Point(70, 52),
                Size = new Size(200, 30),
                Text = "Tenant, Property, Notes..."
            };
            txtSearch.TextChanged += (s, e) => LoadPayments();

            // Buttons
            btnRefresh = new Button
            {
                Text = "🔄 Refresh",
                Location = new Point(750, 12),
                Size = new Size(100, 30),
                BackColor = Color.LightBlue
            };
            btnRefresh.Click += (s, e) => LoadPayments();

            btnDelete = new Button
            {
                Text = "🗑️ Delete",
                Location = new Point(860, 12),
                Size = new Size(100, 30),
                BackColor = Color.LightCoral,
                Enabled = false
            };
            btnDelete.Click += BtnDelete_Click;

            btnRestore = new Button
            {
                Text = "♻️ Restore",
                Location = new Point(970, 12),
                Size = new Size(100, 30),
                BackColor = Color.LightGreen,
                Enabled = false
            };
            btnRestore.Click += BtnRestore_Click;

            btnExport = new Button
            {
                Text = "📤 Export",
                Location = new Point(1080, 12),
                Size = new Size(100, 30),
                BackColor = Color.LightGoldenrodYellow
            };
            btnExport.Click += BtnExport_Click;

            pnlFilter.Controls.AddRange(new Control[] {
                lblFrom, dtpFrom, lblTo, dtpTo,
                lblType, cmbFilterType, lblStatus, cmbFilterStatus,
                lblSearch, txtSearch,
                btnRefresh, btnDelete, btnRestore, btnExport
            });

            // Payments Grid
            dgvPayments = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgvPayments.SelectionChanged += DgvPayments_SelectionChanged;
            dgvPayments.CellDoubleClick += DgvPayments_CellDoubleClick;

            // Summary Panel
            Panel pnlSummary = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.FromArgb(240, 248, 255),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            lblSummary = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlSummary.Controls.Add(lblSummary);

            Panel mainContainer = new Panel
            {
                Dock = DockStyle.Fill
            };

            mainContainer.Controls.Add(dgvPayments);
            mainContainer.Controls.Add(pnlSummary);

            this.Controls.AddRange(new Control[] { mainContainer, pnlFilter, pnlHeader });
        }

        private void LoadPayments()
        {
            try
            {
                dgvPayments.Rows.Clear();
                dgvPayments.Columns.Clear();

                // Add columns
                dgvPayments.Columns.AddRange(
                    new DataGridViewTextBoxColumn { HeaderText = "ID", Width = 50 },
                    new DataGridViewTextBoxColumn { HeaderText = "Date", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Type", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Amount", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Tenant", Width = 150 },
                    new DataGridViewTextBoxColumn { HeaderText = "Property", Width = 150 },
                    new DataGridViewTextBoxColumn { HeaderText = "Agreement", Width = 80 },
                    new DataGridViewTextBoxColumn { HeaderText = "Month", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Notes", Width = 200 },
                    new DataGridViewTextBoxColumn { HeaderText = "Status", Width = 80 },
                    new DataGridViewTextBoxColumn { HeaderText = "Created", Width = 120 },
                    new DataGridViewTextBoxColumn { HeaderText = "Deleted", Width = 120 }
                );

                var allPayments = _dataService.LoadAllPayments();
                var agreements = _dataService.LoadAgreements();
                var tenants = _dataService.LoadTenants();
                var properties = _dataService.LoadProperties();
                var portions = _dataService.LoadPortions();

                // Apply filters
                var filteredPayments = allPayments.Where(p =>
                    p.PaymentDate >= dtpFrom.Value.Date &&
                    p.PaymentDate <= dtpTo.Value.Date.AddDays(1).AddSeconds(-1) &&
                    (cmbFilterType.SelectedIndex == 0 || p.PaymentType.ToString() == cmbFilterType.SelectedItem.ToString()) &&
                    (cmbFilterStatus.SelectedIndex == 0 ||
                     (cmbFilterStatus.SelectedItem.ToString() == "Active" && !p.IsDeleted) ||
                     (cmbFilterStatus.SelectedItem.ToString() == "Deleted" && p.IsDeleted))
                ).OrderByDescending(p => p.PaymentDate).ToList();

                // Apply search filter
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string searchTerm = txtSearch.Text.ToLower();
                    filteredPayments = filteredPayments.Where(p =>
                    {
                        var agreement = agreements.FirstOrDefault(a => a.Id == p.AgreementId);
                        if (agreement == null) return false;

                        var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);
                        var property = properties.FirstOrDefault(prop => prop.Id == agreement.PropertyId);

                        return (tenant?.Name?.ToLower().Contains(searchTerm) ?? false) ||
                               (property?.Name?.ToLower().Contains(searchTerm) ?? false) ||
                               (p.Notes?.ToLower().Contains(searchTerm) ?? false);
                    }).ToList();
                }

                decimal totalAmount = 0;
                int activeCount = 0;
                int deletedCount = 0;

                foreach (var payment in filteredPayments)
                {
                    var agreement = agreements.FirstOrDefault(a => a.Id == payment.AgreementId);
                    var tenant = tenants.FirstOrDefault(t => t.Id == agreement?.TenantId);
                    var property = properties.FirstOrDefault(p => p.Id == agreement?.PropertyId);
                    var portion = portions.FirstOrDefault(p => p.Id == agreement?.PortionId);

                    string tenantName = tenant?.Name ?? "Unknown";
                    string propertyName = property?.Name ?? "Unknown";
                    string portionName = portion?.Name ?? "Unknown";
                    string fullProperty = $"{propertyName} ({portionName})";

                    int rowIndex = dgvPayments.Rows.Add(
                        payment.Id,
                        payment.PaymentDate.ToString("dd-MMM-yyyy"),
                        payment.PaymentType.ToString(),
                        payment.Amount.ToString("C"),
                        tenantName,
                        fullProperty,
                        payment.AgreementId,
                        payment.MonthYear,
                        payment.Notes,
                        payment.IsDeleted ? "Deleted" : "Active",
                        payment.CreatedDate.ToString("dd-MMM-yyyy HH:mm"),
                        payment.DeletedDate?.ToString("dd-MMM-yyyy HH:mm") ?? ""
                    );

                    var row = dgvPayments.Rows[rowIndex];

                    // Color coding
                    if (payment.IsDeleted)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                    }
                    else
                    {
                        if (payment.PaymentType == PaymentType.Rent)
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        else if (payment.PaymentType == PaymentType.Commission)
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                        else if (payment.PaymentType == PaymentType.SecurityDeposit)
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }

                    // Store payment ID in tag for easy access
                    row.Tag = payment.Id;

                    if (!payment.IsDeleted)
                    {
                        totalAmount += payment.Amount;
                        activeCount++;
                    }
                    else
                    {
                        deletedCount++;
                    }
                }

                // Update summary
                lblSummary.Text = $"📊 Total: {filteredPayments.Count} payments | " +
                                $"✅ Active: {activeCount} | " +
                                $"🗑️ Deleted: {deletedCount} | " +
                                $"💰 Total Amount: {totalAmount:C}";

                // Auto-size columns
                dgvPayments.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payments: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPayments_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = dgvPayments.SelectedRows.Count > 0;

            if (hasSelection)
            {
                var selectedRow = dgvPayments.SelectedRows[0];
                bool isDeleted = selectedRow.Cells["Status"].Value?.ToString() == "Deleted";

                btnDelete.Enabled = !isDeleted;
                btnRestore.Enabled = isDeleted;
            }
            else
            {
                btnDelete.Enabled = false;
                btnRestore.Enabled = false;
            }
        }

        private void DgvPayments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvPayments.Rows[e.RowIndex];
                int paymentId = Convert.ToInt32(row.Cells["ID"].Value);

                ShowPaymentDetails(paymentId);
            }
        }

        private void ShowPaymentDetails(int paymentId)
        {
            try
            {
                var payment = _dataService.LoadAllPayments().FirstOrDefault(p => p.Id == paymentId);
                if (payment == null) return;

                var agreement = _dataService.LoadAgreements().FirstOrDefault(a => a.Id == payment.AgreementId);
                var tenant = _dataService.LoadTenants().FirstOrDefault(t => t.Id == agreement?.TenantId);
                var property = _dataService.LoadProperties().FirstOrDefault(p => p.Id == agreement?.PropertyId);
                var portion = _dataService.LoadPortions().FirstOrDefault(p => p.Id == agreement?.PortionId);

                string details = $"💰 PAYMENT DETAILS\n" +
                               $"====================\n\n" +
                               $"ID: {payment.Id}\n" +
                               $"Date: {payment.PaymentDate:dd-MMM-yyyy}\n" +
                               $"Type: {payment.PaymentType}\n" +
                               $"Amount: {payment.Amount:C}\n" +
                               $"Month/Year: {payment.MonthYear}\n" +
                               $"Status: {(payment.IsDeleted ? "Deleted" : "Active")}\n" +
                               $"Created: {payment.CreatedDate:dd-MMM-yyyy HH:mm}\n" +
                               $"Deleted: {(payment.DeletedDate.HasValue ? payment.DeletedDate.Value.ToString("dd-MMM-yyyy HH:mm") : "N/A")}\n\n" +
                               $"👤 TENANT INFO\n" +
                               $"----------------\n" +
                               $"Name: {tenant?.Name ?? "Unknown"}\n" +
                               $"Mobile: {tenant?.Mobile ?? "N/A"}\n" +
                               $"Type: {tenant?.Type.ToString() ?? "Unknown"}\n\n" +
                               $"🏢 PROPERTY INFO\n" +
                               $"----------------\n" +
                               $"Property: {property?.Name ?? "Unknown"}\n" +
                               $"Portion: {portion?.Name ?? "Unknown"} ({portion?.Size ?? "N/A"})\n\n" +
                               $"📝 NOTES\n" +
                               $"--------\n" +
                               $"{payment.Notes}\n\n" +
                               $"{(payment.PaymentType == PaymentType.Commission ? $"📈 COMMISSION DETAILS\n----------------\nSales: {payment.SalesAmount?.ToString("C") ?? "N/A"}\nCommission: {payment.CommissionEarned?.ToString("C") ?? "N/A"}\n" : "")}";

                MessageBox.Show(details, "Payment Details",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing payment details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPayments.SelectedRows.Count == 0) return;

            var selectedRow = dgvPayments.SelectedRows[0];
            int paymentId = Convert.ToInt32(selectedRow.Cells["ID"].Value);

            // Ask for deletion reason
            using (var reasonDialog = new Form())
            {
                reasonDialog.Text = "Enter Deletion Reason";
                reasonDialog.Size = new Size(400, 200);
                reasonDialog.StartPosition = FormStartPosition.CenterParent;
                reasonDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                reasonDialog.MaximizeBox = false;
                reasonDialog.MinimizeBox = false;

                Label lblReason = new Label
                {
                    Text = "Reason for deletion:",
                    Location = new Point(20, 20),
                    Size = new Size(350, 25)
                };

                TextBox txtReason = new TextBox
                {
                    Location = new Point(20, 50),
                    Size = new Size(350, 100),
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    AcceptsReturn = true
                };

                Button btnConfirm = new Button
                {
                    Text = "Confirm Delete",
                    Location = new Point(20, 160),
                    Size = new Size(120, 30),
                    BackColor = Color.LightCoral,
                    DialogResult = DialogResult.OK
                };

                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new Point(150, 160),
                    Size = new Size(120, 30),
                    DialogResult = DialogResult.Cancel
                };

                reasonDialog.Controls.AddRange(new Control[] { lblReason, txtReason, btnConfirm, btnCancel });
                reasonDialog.AcceptButton = btnConfirm;
                reasonDialog.CancelButton = btnCancel;

                if (reasonDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(txtReason.Text))
                {
                    bool success = _dataService.DeletePayment(paymentId, txtReason.Text);
                    if (success)
                    {
                        LoadPayments();
                    }
                }
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            if (dgvPayments.SelectedRows.Count == 0) return;

            var selectedRow = dgvPayments.SelectedRows[0];
            int paymentId = Convert.ToInt32(selectedRow.Cells["ID"].Value);

            bool success = _dataService.RestorePayment(paymentId);
            if (success)
            {
                LoadPayments();
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx|CSV Files|*.csv",
                    FileName = $"Payments_Export_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                    Title = "Export Payments"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Simple CSV export implementation
                    if (saveDialog.FilterIndex == 2) // CSV
                    {
                        ExportToCSV(saveDialog.FileName);
                    }
                    else // Excel
                    {
                        ExportToExcel(saveDialog.FileName);
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
            using (var writer = new System.IO.StreamWriter(filePath))
            {
                // Write header
                writer.WriteLine("ID,Date,Type,Amount,Tenant,Property,Agreement ID,Month,Notes,Status,Created Date,Deleted Date");

                // Write data
                foreach (DataGridViewRow row in dgvPayments.Rows)
                {
                    if (row.IsNewRow) continue;

                    var cells = row.Cells;
                    writer.WriteLine(
                        $"\"{cells["ID"].Value}\"," +
                        $"\"{cells["Date"].Value}\"," +
                        $"\"{cells["Type"].Value}\"," +
                        $"\"{cells["Amount"].Value}\"," +
                        $"\"{cells["Tenant"].Value}\"," +
                        $"\"{cells["Property"].Value}\"," +
                        $"\"{cells["Agreement"].Value}\"," +
                        $"\"{cells["Month"].Value}\"," +
                        $"\"{EscapeCsv(cells["Notes"].Value?.ToString() ?? "")}\"," +
                        $"\"{cells["Status"].Value}\"," +
                        $"\"{cells["Created"].Value}\"," +
                        $"\"{cells["Deleted"].Value}\""
                    );
                }
            }

            MessageBox.Show($"Exported {dgvPayments.Rows.Count - 1} payments to CSV file.", "Export Successful",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string EscapeCsv(string text)
        {
            return text.Replace("\"", "\"\"");
        }

        private void ExportToExcel(string filePath)
        {
            MessageBox.Show($"Excel export would save to: {filePath}\n\n" +
                "Excel export requires EPPlus library.\n" +
                "Add NuGet package: Install-Package EPPlus",
                "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
