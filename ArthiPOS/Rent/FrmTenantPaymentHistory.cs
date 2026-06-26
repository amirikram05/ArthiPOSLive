using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShopRentManagementSystem
{
    public partial class FrmTenantPaymentHistory : Form
    {
        private JsonReportService _reportService;
        private JsonDataService _dataService;
        private DataGridView dgvTenants;
        private DataGridView dgvTransactions;
        private ComboBox cmbTenants;
        private Label lblCurrentBalance;
        private Button btnExport;
        private Button btnRefresh;
        private Label lblSummary;
        private Button btnDeletePayment; // Added reference

        // Store current selection
        private int _currentTenantId = 0;
        private int _currentAgreementId = 0;
        private string _currentTenantName = "";

        public FrmTenantPaymentHistory()
        {
            InitializeComponent();
            _reportService = new JsonReportService();
            _dataService = new JsonDataService();
            SetupContextMenu();
            LoadTenants();
        }
        public FrmTenantPaymentHistory(int tenantID)
        {
            InitializeComponent();
            this._currentTenantId = tenantID;
            _reportService = new JsonReportService();
            _dataService = new JsonDataService();
            SetupContextMenu();
            LoadTenants();
        }

        private void InitializeComponent()
        {
            this.Text = "📋 Tenant Payment History";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Header
            Panel pnlHeader = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = Color.SteelBlue,
                Padding = new Padding(20)
            };

            Label lblTitle = new Label
            {
                Text = "🧾 Tenant Payment History & Ledger",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlHeader.Controls.Add(lblTitle);

            // Filter Panel
            Panel pnlFilter = new Panel
            {
                Height = 80,
                Dock = DockStyle.Top,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(20, 10, 20, 10)
            };

            Label lblSelect = new Label
            {
                Text = "Select Tenant:",
                Location = new Point(10, 13),
                Size = new Size(80, 25)
            };

            cmbTenants = new ComboBox
            {
                Location = new Point(100, 10),
                Size = new Size(250, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTenants.SelectedIndexChanged += CmbTenants_SelectedIndexChanged;

            // Date Range
            Label lblFrom = new Label
            {
                Text = "From:",
                Location = new Point(370, 13),
                Size = new Size(40, 25)
            };

            DateTimePicker dtpFrom = new DateTimePicker
            {
                Location = new Point(420, 10),
                Size = new Size(120, 30),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now.AddMonths(-3)
            };
            dtpFrom.ValueChanged += (s, e) => RefreshTransactions();

            Label lblTo = new Label
            {
                Text = "To:",
                Location = new Point(550, 13),
                Size = new Size(30, 25)
            };

            DateTimePicker dtpTo = new DateTimePicker
            {
                Location = new Point(590, 10),
                Size = new Size(120, 30),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now
            };
            dtpTo.ValueChanged += (s, e) => RefreshTransactions();

            btnRefresh = new Button
            {
                Text = "🔄 Refresh",
                Location = new Point(720, 8),
                Size = new Size(100, 30),
                BackColor = Color.LightBlue
            };
            btnRefresh.Click += (s, e) => RefreshTransactions();

            lblCurrentBalance = new Label
            {
                Location = new Point(10, 45),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkGreen
            };

            btnExport = new Button
            {
                Text = "📤 Export to Excel",
                Location = new Point(830, 8),
                Size = new Size(150, 30),
                BackColor = Color.LightGreen
            };
            btnExport.Click += BtnExport_Click;

            // Add Delete Payment Button - FIXED: Proper event handler assignment
            btnDeletePayment = new Button
            {
                Text = "🗑️ Delete Payment",
                Location = new Point(990, 8),
                Size = new Size(150, 30),
                BackColor = Color.LightCoral,
                Enabled = false,
                Name = "btnDeletePayment" // Give it a name for easy reference
            };
            // Event handler is assigned below in separate method

            pnlFilter.Controls.AddRange(new Control[] {
                lblSelect, cmbTenants, lblFrom, dtpFrom, lblTo, dtpTo,
                btnRefresh, lblCurrentBalance, btnExport, btnDeletePayment
            });

            // Main Split Container
            SplitContainer splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 150
            };

            // Tenants Grid
            dgvTenants = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };
            dgvTenants.SelectionChanged += DgvTenants_SelectionChanged;

            // Setup tenants grid columns
            dgvTenants.Columns.AddRange(
                new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 },
                new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "Name", Width = 200 },
                new DataGridViewTextBoxColumn { HeaderText = "Mobile", DataPropertyName = "Mobile", Width = 120 },
                new DataGridViewTextBoxColumn { HeaderText = "Type", DataPropertyName = "Type", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Agreement", DataPropertyName = "AgreementId", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "Property", DataPropertyName = "PropertyName", Width = 150 },
                new DataGridViewTextBoxColumn { HeaderText = "Portion", DataPropertyName = "PortionName", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Balance", DataPropertyName = "Balance", Width = 100 }
            );

            // Transactions Grid
            dgvTransactions = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvTransactions.SelectionChanged += DgvTransactions_SelectionChanged;

            splitContainer.Panel1.Controls.Add(dgvTenants);
            splitContainer.Panel2.Controls.Add(dgvTransactions);

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
                Name = "lblSummary",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlSummary.Controls.Add(lblSummary);

            Panel mainContainer = new Panel
            {
                Dock = DockStyle.Fill
            };

            mainContainer.Controls.Add(splitContainer);
            mainContainer.Controls.Add(pnlSummary);

            this.Controls.AddRange(new Control[] { mainContainer, pnlFilter, pnlHeader });

            // Setup delete button event handler - FIXED: Do this AFTER all controls are created
            SetupDeleteButtonEvent();
        }

        private void SetupDeleteButtonEvent()
        {
            // Make sure btnDeletePayment exists and assign event handler
            if (btnDeletePayment != null)
            {
                btnDeletePayment.Click += BtnDeletePayment_Click;
            }

            // Enable/disable delete button based on selection
            dgvTransactions.SelectionChanged += (s, e) =>
            {
                if (btnDeletePayment != null)
                {
                    btnDeletePayment.Enabled = dgvTransactions.SelectedRows.Count > 0;
                }
            };
        }

        private void SetupContextMenu()
        {
            ContextMenuStrip contextMenuTransactions = new ContextMenuStrip();

            ToolStripMenuItem viewDetailsItem = new ToolStripMenuItem("👁️ View Details");
            viewDetailsItem.Click += (s, e) => ViewSelectedPaymentDetails();

            ToolStripMenuItem deleteItem = new ToolStripMenuItem("🗑️ Delete Payment");
            deleteItem.Click += (s, e) => DeleteSelectedPayment();

            ToolStripMenuItem restoreItem = new ToolStripMenuItem("♻️ Restore Payment");
            restoreItem.Click += (s, e) => RestoreSelectedPayment();

            ToolStripSeparator separator = new ToolStripSeparator();

            ToolStripMenuItem printReceiptItem = new ToolStripMenuItem("🖨️ Print Receipt");
            printReceiptItem.Click += (s, e) => PrintReceipt();

            contextMenuTransactions.Items.AddRange(new ToolStripItem[] {
                viewDetailsItem, deleteItem, restoreItem, separator, printReceiptItem
            });

            if (dgvTransactions != null)
            {
                dgvTransactions.ContextMenuStrip = contextMenuTransactions;
            }
        }

        private void LoadTenants()
        {
            try
            {
                var tenants = _dataService.LoadTenants();
                var agreements = _dataService.LoadAgreements().Where(a => a.IsActive).ToList();
                var properties = _dataService.LoadProperties();
                var portions = _dataService.LoadPortions();
                var payments = _dataService.LoadPayments();

                // Create tenant display list
                var tenantList = new List<TenantDisplay>();

                foreach (var tenant in tenants)
                {
                    var tenantAgreements = agreements.Where(a => a.TenantId == tenant.Id).ToList();

                    foreach (var agreement in tenantAgreements)
                    {
                        var property = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);
                        var portion = portions.FirstOrDefault(p => p.Id == agreement.PortionId);

                        // Calculate tenant balance
                        decimal balance = 0;
                        if (tenant.Type == TenantType.OnRent)
                        {
                            var rentPayments = payments
                                .Where(p => p.AgreementId == agreement.Id && p.PaymentType == PaymentType.Rent)
                                .ToList();

                            DateTime currentDate = DateTime.Now;
                            int monthsPassed = ((currentDate.Year - agreement.StartDate.Year) * 12) +
                                               currentDate.Month - agreement.StartDate.Month;

                            if (currentDate.Day < agreement.StartDate.Day)
                                monthsPassed--;

                            monthsPassed = Math.Max(0, monthsPassed);

                            decimal totalRentDue = monthsPassed * agreement.MonthlyRent;
                            decimal totalRentPaid = rentPayments.Sum(p => p.Amount);
                            balance = totalRentDue - totalRentPaid;
                        }
                        else if (tenant.Type == TenantType.OnCommission)
                        {
                            var commissionPayments = payments
                                .Where(p => p.AgreementId == agreement.Id && p.PaymentType == PaymentType.Commission)
                                .ToList();

                            // Simplified commission balance calculation
                            balance = 0;
                        }

                        tenantList.Add(new TenantDisplay
                        {
                            Id = tenant.Id,
                            Name = tenant.Name,
                            Mobile = tenant.Mobile,
                            Type = tenant.Type,
                            AgreementId = agreement.Id,
                            PropertyName = property?.Name ?? "Unknown",
                            PortionName = portion?.Name ?? "Unknown",
                            Balance = balance
                        });
                    }
                }

                dgvTenants.DataSource = tenantList;

                cmbTenants.Items.Clear();
                cmbTenants.Items.Add("All Tenants");
                foreach (var tenant in tenantList.OrderBy(t => t.Name))
                {
                    cmbTenants.Items.Add(tenant.Name);
                }
                if (cmbTenants.Items.Count > 0)
                    cmbTenants.SelectedIndex = 0;

                UpdateSummary(tenantList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tenants: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummary(List<TenantDisplay> tenants)
        {
            int totalTenants = tenants.Select(t => t.Id).Distinct().Count();
            decimal totalBalance = tenants.Sum(t => t.Balance);
            int tenantsWithBalance = tenants.Count(t => t.Balance > 0);

            lblSummary.Text = $"📊 Total Tenants: {totalTenants} | " +
                             $"💰 Total Balance: {totalBalance:C} | " +
                             $"⚠️ Tenants with Balance: {tenantsWithBalance}";
        }

        private void CmbTenants_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbTenants.SelectedItem == null) return;

                var selectedName = cmbTenants.SelectedItem.ToString();

                if (selectedName == "All Tenants")
                {
                    _currentTenantId = 0;
                    _currentAgreementId = 0;
                    _currentTenantName = "";
                    LoadAllTransactions();
                }
                else
                {
                    // Find tenant in grid
                    foreach (DataGridViewRow row in dgvTenants.Rows)
                    {
                        if (row.Cells["Name"].Value?.ToString() == selectedName)
                        {
                            dgvTenants.ClearSelection();
                            row.Selected = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting tenant: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvTenants_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvTenants.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvTenants.SelectedRows[0];
                    try { _currentTenantId = Convert.ToInt32(selectedRow.Cells["ID"].Value); } catch (Exception ex) { }
                    _currentTenantName = selectedRow.Cells["Name"].Value?.ToString();
                    _currentAgreementId = Convert.ToInt32(selectedRow.Cells["Agreement"].Value);

                    cmbTenants.SelectedItem = _currentTenantName;
                    LoadTenantTransactions(_currentTenantId, _currentAgreementId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tenant transactions: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvTransactions_SelectionChanged(object sender, EventArgs e)
        {
            // Enable/disable delete button
            if (btnDeletePayment != null)
            {
                bool hasSelection = dgvTransactions.SelectedRows.Count > 0;
                btnDeletePayment.Enabled = hasSelection;
            }
        }

        private void LoadAllTransactions()
        {
            try
            {
                dgvTransactions.Rows.Clear();
                dgvTransactions.Columns.Clear();

                // Setup columns
                dgvTransactions.Columns.AddRange(
                    new DataGridViewTextBoxColumn { HeaderText = "Payment ID", Width = 80 },
                    new DataGridViewTextBoxColumn { HeaderText = "Date", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Tenant", Width = 150 },
                    new DataGridViewTextBoxColumn { HeaderText = "Type", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Amount", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Balance", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Notes", Width = 200 },
                    new DataGridViewTextBoxColumn { HeaderText = "Status", Width = 80 }
                );

                var allPayments = _dataService.LoadPayments().OrderBy(p => p.PaymentDate).ToList();
                var agreements = _dataService.LoadAgreements();
                var tenants = _dataService.LoadTenants();
                var properties = _dataService.LoadProperties();
                var portions = _dataService.LoadPortions();

                // Create running balances for each tenant
                var tenantBalances = new Dictionary<int, decimal>();

                foreach (var payment in allPayments)
                {
                    var agreement = agreements.FirstOrDefault(a => a.Id == payment.AgreementId);
                    if (agreement == null) continue;

                    var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);
                    if (tenant == null) continue;

                    var property = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);
                    var portion = portions.FirstOrDefault(p => p.Id == agreement.PortionId);

                    // Initialize balance for tenant if not exists
                    if (!tenantBalances.ContainsKey(tenant.Id))
                    {
                        tenantBalances[tenant.Id] = 0;
                    }

                    // Update balance (positive amount means payment received, so balance decreases)
                    decimal amountChange = payment.Amount;
                    if (payment.PaymentType == PaymentType.Rent || payment.PaymentType == PaymentType.Commission)
                    {
                        tenantBalances[tenant.Id] -= amountChange; // Payment reduces balance
                    }
                    else if (payment.PaymentType == PaymentType.SecurityDeposit)
                    {
                        // Security deposit is credit, so balance becomes more negative (credit increases)
                        tenantBalances[tenant.Id] -= amountChange;
                    }

                    string description = $"{payment.PaymentType} Payment";
                    if (payment.PaymentType == PaymentType.Commission && payment.SalesAmount.HasValue)
                    {
                        description += $" (Sales: {payment.SalesAmount:C})";
                    }

                    int rowIndex = dgvTransactions.Rows.Add(
                        payment.Id,
                        payment.PaymentDate.ToString("dd-MMM-yyyy"),
                        tenant.Name,
                        payment.PaymentType.ToString(),
                        payment.Amount.ToString("C"),
                        tenantBalances[tenant.Id].ToString("C"),
                        payment.Notes,
                        payment.IsDeleted ? "Deleted" : "Active"
                    );

                    var row = dgvTransactions.Rows[rowIndex];

                    // Store payment object in tag for easy access
                    row.Tag = payment;

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
                }

                // Update current balance label
                decimal totalBalance = tenantBalances.Values.Sum();
                lblCurrentBalance.Text = $"💰 Total System Balance: {totalBalance:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading all transactions: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTenantTransactions(int tenantId, int agreementId)
        {
            try
            {
                dgvTransactions.Rows.Clear();
                dgvTransactions.Columns.Clear();

                // Setup columns for detailed ledger
                dgvTransactions.Columns.AddRange(
                    new DataGridViewTextBoxColumn { HeaderText = "Payment ID", Width = 80 },
                    new DataGridViewTextBoxColumn { HeaderText = "Date", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Type", Width = 80 },
                    new DataGridViewTextBoxColumn { HeaderText = "Description", Width = 250 },
                    new DataGridViewTextBoxColumn { HeaderText = "Debit", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Credit", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Balance", Width = 120 },
                    new DataGridViewTextBoxColumn { HeaderText = "Notes", Width = 200 },
                    new DataGridViewTextBoxColumn { HeaderText = "Status", Width = 80 }
                );

                var tenant = _dataService.LoadTenants().FirstOrDefault(t => t.Id == tenantId);
                var agreement = _dataService.LoadAgreements().FirstOrDefault(a => a.Id == agreementId);
                var allPayments = _dataService.LoadPayments();
                var payments = allPayments
                    .Where(p => p.AgreementId == agreementId)
                    .OrderBy(p => p.PaymentDate)
                    .ToList();

                if (tenant == null || agreement == null)
                {
                    lblCurrentBalance.Text = "Tenant or agreement not found";
                    return;
                }

                // Start with security deposit as credit
                decimal runningBalance = -tenant.SecurityDeposit; // Negative because it's credit

                // Add security deposit entry
                int rowIndex = dgvTransactions.Rows.Add(
                    0,
                    tenant.StampPaperDate.ToString("dd-MMM-yyyy"),
                    "Deposit",
                    "Security Deposit",
                    "",
                    tenant.SecurityDeposit.ToString("C"),
                    runningBalance.ToString("C"),
                    "Initial security deposit",
                    "Active"
                );

                var depositRow = dgvTransactions.Rows[rowIndex];
                depositRow.DefaultCellStyle.BackColor = Color.LightYellow;
                depositRow.Tag = null; // No payment object for deposit

                if (tenant.Type == TenantType.OnRent)
                {
                    // Add rent charges
                    DateTime currentDate = agreement.StartDate;
                    DateTime endDate = DateTime.Now;

                    while (currentDate <= endDate)
                    {
                        runningBalance += agreement.MonthlyRent; // Add rent charge (debit increases balance)

                        rowIndex = dgvTransactions.Rows.Add(
                            0,
                            currentDate.ToString("dd-MMM-yyyy"),
                            "Rent",
                            $"Monthly Rent - {currentDate:MMMM yyyy}",
                            agreement.MonthlyRent.ToString("C"),
                            "",
                            runningBalance.ToString("C"),
                            "",
                            "Charge"
                        );

                        var rentRow = dgvTransactions.Rows[rowIndex];
                        rentRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230); // Light red for charges
                        rentRow.Tag = null; // No payment object for charges

                        currentDate = currentDate.AddMonths(1);
                    }
                }

                // Add actual payments
                foreach (var payment in payments)
                {
                    runningBalance -= payment.Amount; // Payment reduces balance

                    string description = $"{payment.PaymentType} Payment";
                    if (payment.PaymentType == PaymentType.Commission && payment.SalesAmount.HasValue)
                    {
                        description += $" (Sales: {payment.SalesAmount:C})";
                    }

                    rowIndex = dgvTransactions.Rows.Add(
                        payment.Id,
                        payment.PaymentDate.ToString("dd-MMM-yyyy"),
                        "Payment",
                        description,
                        "",
                        payment.Amount.ToString("C"),
                        runningBalance.ToString("C"),
                        payment.Notes,
                        payment.IsDeleted ? "Deleted" : "Active"
                    );

                    var paymentRow = dgvTransactions.Rows[rowIndex];

                    // Store payment object in tag for easy access
                    paymentRow.Tag = payment;

                    // Color coding for payments
                    if (payment.IsDeleted)
                    {
                        paymentRow.DefaultCellStyle.BackColor = Color.LightGray;
                        paymentRow.DefaultCellStyle.ForeColor = Color.Gray;
                    }
                    else
                    {
                        if (payment.PaymentType == PaymentType.Rent)
                            paymentRow.DefaultCellStyle.BackColor = Color.LightGreen;
                        else if (payment.PaymentType == PaymentType.Commission)
                            paymentRow.DefaultCellStyle.BackColor = Color.LightBlue;
                        else if (payment.PaymentType == PaymentType.SecurityDeposit)
                            paymentRow.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }

                // Update current balance label
                lblCurrentBalance.Text = $"👤 {tenant.Name} | 📱 {tenant.Mobile} | " +
                                       $"💰 Current Balance: {runningBalance:C} | " +
                                       $"{(runningBalance > 0 ? "⚠️ Overdue" : runningBalance < 0 ? "✅ In Credit" : "⚖️ Settled")}";

                // Auto-size columns
                dgvTransactions.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tenant transactions: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshTransactions()
        {
            if (_currentTenantId == 0 || _currentAgreementId == 0)
            {
                LoadAllTransactions();
            }
            else
            {
                LoadTenantTransactions(_currentTenantId, _currentAgreementId);
            }
        }

        private void ViewSelectedPaymentDetails()
        {
            if (dgvTransactions.SelectedRows.Count == 0) return;

            var selectedRow = dgvTransactions.SelectedRows[0];
            var payment = selectedRow.Tag as Payment;

            if (payment == null)
            {
                MessageBox.Show("This entry is not a payment (it's a charge or deposit).", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ShowPaymentDetails(payment);
        }

        private void ShowPaymentDetails(Payment payment)
        {
            try
            {
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

        private void DeleteSelectedPayment()
        {
            if (dgvTransactions.SelectedRows.Count == 0) return;

            var selectedRow = dgvTransactions.SelectedRows[0];
            var payment = selectedRow.Tag as Payment;

            if (payment == null)
            {
                MessageBox.Show("This entry cannot be deleted (it's a charge or deposit, not a payment).", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (payment.IsDeleted)
            {
                MessageBox.Show("This payment is already deleted.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DeletePaymentWithConfirmation(payment);
        }

        // FIXED: This is the method called by the delete button
        private void BtnDeletePayment_Click(object sender, EventArgs e)
        {
            DeleteSelectedPayment();
        }

        private void DeletePaymentWithConfirmation(Payment payment)
        {
            string date = payment.PaymentDate.ToString("dd-MMM-yyyy");
            string type = payment.PaymentType.ToString();
            string amount = payment.Amount.ToString("C");

            // Ask for deletion reason
            using (var reasonDialog = new Form())
            {
                reasonDialog.Text = "Delete Payment";
                reasonDialog.Size = new Size(400, 250);
                reasonDialog.StartPosition = FormStartPosition.CenterParent;
                reasonDialog.FormBorderStyle = FormBorderStyle.FixedDialog;

                Label lblInfo = new Label
                {
                    Text = $"Delete this payment?\n\nDate: {date}\nType: {type}\nAmount: {amount}",
                    Location = new Point(20, 20),
                    Size = new Size(350, 60)
                };

                Label lblReason = new Label
                {
                    Text = "Reason for deletion:",
                    Location = new Point(20, 90),
                    Size = new Size(350, 25)
                };

                TextBox txtReason = new TextBox
                {
                    Location = new Point(20, 120),
                    Size = new Size(350, 60),
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical
                };

                Button btnConfirm = new Button
                {
                    Text = "Confirm Delete",
                    Location = new Point(20, 190),
                    Size = new Size(120, 30),
                    BackColor = Color.LightCoral,
                    DialogResult = DialogResult.OK
                };

                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new Point(150, 190),
                    Size = new Size(120, 30),
                    DialogResult = DialogResult.Cancel
                };

                reasonDialog.Controls.AddRange(new Control[] { lblInfo, lblReason, txtReason, btnConfirm, btnCancel });
                reasonDialog.AcceptButton = btnConfirm;
                reasonDialog.CancelButton = btnCancel;
                 btnConfirm.Click += (s, e) =>
                {
                    bool success = _dataService.DeletePayment(payment.Id, txtReason.Text);
                    if (success)
                    {
                        RefreshTransactions();
                        MessageBox.Show("Payment deleted successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                };

                if (reasonDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(txtReason.Text))
                {
                    bool success = _dataService.DeletePayment(payment.Id, txtReason.Text);
                    if (success)
                    {
                        RefreshTransactions();
                        MessageBox.Show("Payment deleted successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void RestoreSelectedPayment()
        {
            if (dgvTransactions.SelectedRows.Count == 0) return;

            var selectedRow = dgvTransactions.SelectedRows[0];
            var payment = selectedRow.Tag as Payment;

            if (payment == null)
            {
                MessageBox.Show("This entry cannot be restored.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!payment.IsDeleted)
            {
                MessageBox.Show("This payment is not deleted.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string date = payment.PaymentDate.ToString("dd-MMM-yyyy");
            string type = payment.PaymentType.ToString();
            string amount = payment.Amount.ToString("C");

            DialogResult result = MessageBox.Show(
                $"Restore this payment?\n\nDate: {date}\nType: {type}\nAmount: {amount}",
                "Confirm Restoration",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool success = _dataService.RestorePayment(payment.Id);
                if (success)
                {
                    RefreshTransactions();
                    MessageBox.Show("Payment restored successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void PrintReceipt()
        {
            if (dgvTransactions.SelectedRows.Count == 0) return;

            var selectedRow = dgvTransactions.SelectedRows[0];
            var payment = selectedRow.Tag as Payment;

            if (payment == null)
            {
                MessageBox.Show("Cannot print receipt for this entry.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show($"Printing receipt for payment #{payment.Id}\n\n" +
                           $"This would open a print preview dialog.\n" +
                           $"Receipt generation requires report design implementation.",
                           "Print Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx|CSV Files|*.csv",
                    FileName = $"Tenant_Payment_History_{DateTime.Now:yyyyMMdd}.xlsx",
                    Title = "Export Tenant Payment History"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show($"Report would be exported to: {saveDialog.FileName}\n\n" +
                        "Export functionality requires EPPlus library.\n" +
                        "Add NuGet package: Install-Package EPPlus",
                        "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper class for tenant display
        private class TenantDisplay
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Mobile { get; set; }
            public TenantType Type { get; set; }
            public int AgreementId { get; set; }
            public string PropertyName { get; set; }
            public string PortionName { get; set; }
            public decimal Balance { get; set; }
        }
    }
}