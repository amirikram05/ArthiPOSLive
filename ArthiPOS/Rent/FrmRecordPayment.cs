using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShopRentManagementSystem
{
    public partial class FrmRecordPayment : Form
    {
        private JsonDataService _dataService;
        private ComboBox cmbAgreement;
        private ComboBox cmbPaymentType;
        private TextBox txtAmount;
        private DateTimePicker dtpPaymentDate;
        private TextBox txtNotes;
        private Button btnSave;
        private Button btnCancel;
        private Label lblTenantInfo;
        private Label lblPropertyInfo;
        private Label lblCurrentDue;
        private Panel pnlCommissionDetails;
        private DataGridView dgvProducts;
        private TextBox txtSalesAmount;
        private TextBox txtCommissionRate;
        private TextBox txtCommissionAmount;
        private CheckBox chkAdvanced;
        private TabControl tabControl;
        private DataGridView dgvPaymentHistory;
        private Button btnRefreshHistory;
        private ComboBox cmbFilterAgreement;

        // For commission products
        private List<CommissionProduct> _selectedProducts = new List<CommissionProduct>();

        public FrmRecordPayment()
        {
            try
            {
                InitializeComponent();
                _dataService = new JsonDataService();
                _selectedProducts = new List<CommissionProduct>();

                LoadAgreements();
                SetupPaymentTypeChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public FrmRecordPayment(int agreementId) : this()
        {
            // Select specific agreement if provided
            for (int i = 0; i < cmbAgreement.Items.Count; i++)
            {
                if (cmbAgreement.Items[i] is AgreementDisplayItem item && item.Id == agreementId)
                {
                    cmbAgreement.SelectedIndex = i;
                    break;
                }
            }
        }

        private class AgreementDisplayItem
        {
            public int Id { get; set; }
            public string DisplayText { get; set; }
            public int TenantId { get; set; }
            public int PropertyId { get; set; }
            public int PortionId { get; set; }
            public TenantType TenantType { get; set; }
            public RentAgreement Agreement { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }

        private class CommissionProduct
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Unit { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Quantity { get; set; }
            public decimal LaborAmount { get; set; }
            public decimal ProductTotal => Quantity * UnitPrice;
            public decimal TotalAmount => ProductTotal + LaborAmount;
        }

        private void InitializeComponent()
        {
            this.Text = "💳 Record Payment";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = SystemColors.Control;
            this.Padding = new Padding(10);

            // Tab Control
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Appearance = TabAppearance.FlatButtons,
                ItemSize = new Size(100, 30),
                SizeMode = TabSizeMode.Fixed
            };

            // Tab 1: Record New Payment
            TabPage tabRecord = new TabPage("➕ Record Payment");
            SetupRecordTab(tabRecord);

            // Tab 2: View Payment History
            TabPage tabHistory = new TabPage("📋 Payment History");
            SetupHistoryTab(tabHistory);

            tabControl.TabPages.Add(tabRecord);
            tabControl.TabPages.Add(tabHistory);

            this.Controls.Add(tabControl);

            // Set Accept and Cancel buttons
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void SetupRecordTab(TabPage tab)
        {
            // Main Panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Header
            Label lblHeader = new Label
            {
                Text = "📝 Record New Payment",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.SteelBlue,
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Agreement Selection
            Panel pnlAgreement = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                Padding = new Padding(0, 10, 0, 10)
            };

            Label lblAgreement = new Label
            {
                Text = "Select Agreement:",
                Location = new Point(0, 10),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            cmbAgreement = new ComboBox
            {
                Location = new Point(160, 10),
                Size = new Size(400, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cmbAgreement.SelectedIndexChanged += CmbAgreement_SelectedIndexChanged;

            // Tenant Info Label
            lblTenantInfo = new Label
            {
                Location = new Point(0, 45),
                Size = new Size(560, 25),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.DarkSlateGray
            };

            // Property Info Label
            lblPropertyInfo = new Label
            {
                Location = new Point(0, 70),
                Size = new Size(560, 25),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.DarkSlateGray
            };

            pnlAgreement.Controls.AddRange(new Control[] { lblAgreement, cmbAgreement, lblTenantInfo, lblPropertyInfo });

            // Current Due Panel
            Panel pnlDueInfo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(255, 248, 225),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            lblCurrentDue = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlDueInfo.Controls.Add(lblCurrentDue);

            // Payment Details Panel
            Panel pnlPaymentDetails = new Panel
            {
                Dock = DockStyle.Top,
                Height = 200,
                Padding = new Padding(0, 20, 0, 10)
            };

            // Payment Type
            Label lblPaymentType = new Label
            {
                Text = "Payment Type:",
                Location = new Point(0, 10),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10)
            };

            cmbPaymentType = new ComboBox
            {
                Location = new Point(130, 10),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cmbPaymentType.Items.AddRange(new[] { "Rent", "Commission", "Security Deposit", "Other" });
            cmbPaymentType.SelectedIndex = 0;

            // Amount
            Label lblAmount = new Label
            {
                Text = "Amount:",
                Location = new Point(0, 50),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10)
            };

            txtAmount = new TextBox
            {
                Location = new Point(130, 50),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 10)
            };
            txtAmount.KeyPress += TxtAmount_KeyPress;

            // Payment Date
            Label lblPaymentDate = new Label
            {
                Text = "Payment Date:",
                Location = new Point(0, 90),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10)
            };

            dtpPaymentDate = new DateTimePicker
            {
                Location = new Point(130, 90),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 10),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now
            };

            // Notes
            Label lblNotes = new Label
            {
                Text = "Notes:",
                Location = new Point(0, 130),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10)
            };

            txtNotes = new TextBox
            {
                Location = new Point(130, 130),
                Size = new Size(430, 30),
                Font = new Font("Segoe UI", 10)
            };

            pnlPaymentDetails.Controls.AddRange(new Control[] {
                lblPaymentType, cmbPaymentType,
                lblAmount, txtAmount,
                lblPaymentDate, dtpPaymentDate,
                lblNotes, txtNotes
            });

            // Commission Details Panel (initially hidden)
            pnlCommissionDetails = new Panel
            {
                Dock = DockStyle.Top,
                Height = 300,
                Visible = false,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(240, 248, 255),
                Padding = new Padding(10)
            };

            SetupCommissionPanel();

            // Buttons Panel
            Panel pnlButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                Padding = new Padding(0, 10, 0, 0)
            };

            btnSave = new Button
            {
                Text = "💾 Save Payment",
                Location = new Point(200, 10),
                Size = new Size(150, 40),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "✖ Cancel",
                Location = new Point(360, 10),
                Size = new Size(150, 40),
                BackColor = Color.LightCoral,
                Font = new Font("Segoe UI", 10)
            };
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            pnlButtons.Controls.AddRange(new Control[] { btnSave, btnCancel });

            // Add panels to main panel
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill
            };

            contentPanel.Controls.Add(pnlButtons);
            contentPanel.Controls.Add(pnlCommissionDetails);
            contentPanel.Controls.Add(pnlPaymentDetails);
            contentPanel.Controls.Add(pnlDueInfo);
            contentPanel.Controls.Add(pnlAgreement);

            mainPanel.Controls.Add(contentPanel);
            mainPanel.Controls.Add(lblHeader);

            tab.Controls.Add(mainPanel);
        }

        private void SetupHistoryTab(TabPage tab)
        {
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            // Header
            Label lblHeader = new Label
            {
                Text = "📋 Payment History",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.SteelBlue,
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Filter Panel
            Panel pnlFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(245, 245, 245),
                Padding = new Padding(10)
            };

            Label lblFilterAgreement = new Label
            {
                Text = "Agreement:",
                Location = new Point(10, 13),
                Size = new Size(80, 25)
            };

            cmbFilterAgreement = new ComboBox
            {
                Location = new Point(100, 10),
                Size = new Size(300, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cmbFilterAgreement.SelectedIndexChanged += (s, e) => LoadPaymentHistory();

            // Load agreements into filter
            LoadFilterAgreements();

            btnRefreshHistory = new Button
            {
                Text = "🔄 Refresh",
                Location = new Point(420, 10),
                Size = new Size(100, 30),
                BackColor = Color.LightBlue
            };
            btnRefreshHistory.Click += (s, e) => LoadPaymentHistory();

            pnlFilter.Controls.AddRange(new Control[] { lblFilterAgreement, cmbFilterAgreement, btnRefreshHistory });

            // Payment History Grid
            dgvPaymentHistory = new DataGridView
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
                Height = 40,
                BackColor = Color.FromArgb(240, 248, 255),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            Label lblSummary = new Label
            {
                Name = "lblHistorySummary",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlSummary.Controls.Add(lblSummary);

            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill
            };

            contentPanel.Controls.Add(dgvPaymentHistory);
            contentPanel.Controls.Add(pnlSummary);
            contentPanel.Controls.Add(pnlFilter);

            mainPanel.Controls.Add(contentPanel);
            mainPanel.Controls.Add(lblHeader);

            tab.Controls.Add(mainPanel);

            // Load initial history
            LoadPaymentHistory();
        }

        private void LoadFilterAgreements()
        {
            try
            {
                cmbFilterAgreement.Items.Clear();
                cmbFilterAgreement.Items.Add("All Agreements");

                if (_dataService == null) return;

                var agreements = _dataService.LoadAgreements()?.Where(a => a.IsActive).ToList();
                var tenants = _dataService.LoadTenants();
                var properties = _dataService.LoadProperties();
                var portions = _dataService.LoadPortions();

                if (agreements == null || tenants == null || properties == null || portions == null)
                    return;

                foreach (var agreement in agreements)
                {
                    var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);
                    var property = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);
                    var portion = portions.FirstOrDefault(p => p.Id == agreement.PortionId);

                    if (tenant != null && property != null && portion != null)
                    {
                        cmbFilterAgreement.Items.Add(new AgreementDisplayItem
                        {
                            Id = agreement.Id,
                            DisplayText = $"[{agreement.Id}] {tenant.Name} - {property.Name}"
                        });
                    }
                }
                cmbFilterAgreement.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading filter agreements: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupCommissionPanel()
        {
            pnlCommissionDetails.Controls.Clear();

            Label lblTitle = new Label
            {
                Text = "📦 Commission Details",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 30,
                ForeColor = Color.DarkSlateBlue
            };

            // Products Grid
            dgvProducts = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 150,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White
            };

            dgvProducts.Columns.AddRange(
                new DataGridViewTextBoxColumn { HeaderText = "Product", DataPropertyName = "ProductName", Width = 150 },
                new DataGridViewTextBoxColumn { HeaderText = "Quantity", DataPropertyName = "Quantity", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "Unit", DataPropertyName = "Unit", Width = 60 },
                new DataGridViewTextBoxColumn { HeaderText = "Unit Price", DataPropertyName = "UnitPrice", Width = 90 },
                new DataGridViewTextBoxColumn { HeaderText = "Product Total", DataPropertyName = "ProductTotal", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Labor", DataPropertyName = "LaborAmount", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "Total", DataPropertyName = "TotalAmount", Width = 100 }
            );

            // Add Product Button
            Button btnAddProduct = new Button
            {
                Text = "➕ Add Product",
                Dock = DockStyle.Top,
                Height = 35,
                BackColor = Color.LightBlue,
                Margin = new Padding(0, 10, 0, 0)
            };
            btnAddProduct.Click += BtnAddProduct_Click;

            // Remove Product Button
            Button btnRemoveProduct = new Button
            {
                Text = "➖ Remove Selected",
                Dock = DockStyle.Top,
                Height = 35,
                BackColor = Color.LightPink,
                Margin = new Padding(0, 5, 0, 0)
            };
            btnRemoveProduct.Click += BtnRemoveProduct_Click;

            // Commission Calculation Panel
            Panel pnlCalc = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                Padding = new Padding(10)
            };

            Label lblSalesAmount = new Label
            {
                Text = "Total Sales Amount:",
                Location = new Point(0, 10),
                Size = new Size(150, 25)
            };

            txtSalesAmount = new TextBox
            {
                Location = new Point(160, 10),
                Size = new Size(150, 25),
                ReadOnly = true,
                BackColor = Color.WhiteSmoke
            };

            Label lblCommissionRate = new Label
            {
                Text = "Commission Rate (%):",
                Location = new Point(0, 40),
                Size = new Size(150, 25)
            };

            txtCommissionRate = new TextBox
            {
                Location = new Point(160, 40),
                Size = new Size(150, 25)
            };
            txtCommissionRate.TextChanged += TxtCommissionRate_TextChanged;
            txtCommissionRate.KeyPress += TxtCommissionRate_KeyPress;

            Label lblCommissionAmount = new Label
            {
                Text = "Commission Amount:",
                Location = new Point(320, 10),
                Size = new Size(150, 25)
            };

            txtCommissionAmount = new TextBox
            {
                Location = new Point(480, 10),
                Size = new Size(150, 25),
                ReadOnly = true,
                BackColor = Color.WhiteSmoke,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkGreen
            };

            Button btnCalculate = new Button
            {
                Text = "🧮 Calculate",
                Location = new Point(320, 40),
                Size = new Size(120, 25),
                BackColor = Color.LightYellow
            };
            btnCalculate.Click += BtnCalculate_Click;

            pnlCalc.Controls.AddRange(new Control[] {
                lblSalesAmount, txtSalesAmount,
                lblCommissionRate, txtCommissionRate,
                lblCommissionAmount, txtCommissionAmount,
                btnCalculate
            });

            pnlCommissionDetails.Controls.Add(pnlCalc);
            pnlCommissionDetails.Controls.Add(btnRemoveProduct);
            pnlCommissionDetails.Controls.Add(btnAddProduct);
            pnlCommissionDetails.Controls.Add(dgvProducts);
            pnlCommissionDetails.Controls.Add(lblTitle);
        }

        private void LoadAgreements()
        {
            try
            {
                if (_dataService == null)
                    _dataService = new JsonDataService();

                cmbAgreement.Items.Clear();

                var agreements = _dataService.LoadAgreements();
                var tenants = _dataService.LoadTenants();
                var properties = _dataService.LoadProperties();
                var portions = _dataService.LoadPortions();

                if (agreements == null || tenants == null || properties == null || portions == null)
                {
                    MessageBox.Show("Unable to load data. Please check data files.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var activeAgreements = agreements.Where(a => a.IsActive).ToList();

                if (!activeAgreements.Any())
                {
                    cmbAgreement.Items.Add("No active agreements found");
                    cmbAgreement.Enabled = false;
                    return;
                }

                foreach (var agreement in activeAgreements.OrderBy(a => a.Id))
                {
                    var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);
                    var property = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);
                    var portion = portions.FirstOrDefault(p => p.Id == agreement.PortionId);

                    if (tenant == null || property == null || portion == null)
                        continue;

                    string tenantType = tenant.Type == TenantType.OnRent ? "Rent" : "Commission";
                    string displayText = $"[{tenantType}] {tenant.Name} - {property.Name} ({portion.Name})";

                    var displayItem = new AgreementDisplayItem
                    {
                        Id = agreement.Id,
                        DisplayText = displayText,
                        TenantId = tenant.Id,
                        PropertyId = property.Id,
                        PortionId = portion.Id,
                        TenantType = tenant.Type,
                        Agreement = agreement
                    };

                    cmbAgreement.Items.Add(displayItem);
                }

                if (cmbAgreement.Items.Count > 0)
                {
                    cmbAgreement.SelectedIndex = 0;
                    cmbAgreement.Enabled = true;
                }
                else
                {
                    cmbAgreement.Items.Add("No valid agreements found");
                    cmbAgreement.SelectedIndex = 0;
                    cmbAgreement.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading agreements: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                cmbAgreement.Items.Clear();
                cmbAgreement.Items.Add("Error loading agreements");
                cmbAgreement.SelectedIndex = 0;
                cmbAgreement.Enabled = false;
            }
        }

        private void CmbAgreement_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbAgreement.SelectedItem == null) return;

                if (cmbAgreement.SelectedItem is string)
                {
                    lblTenantInfo.Text = "Please add agreements first";
                    lblPropertyInfo.Text = "";
                    lblCurrentDue.Text = "";
                    return;
                }

                var agreementItem = cmbAgreement.SelectedItem as AgreementDisplayItem;
                if (agreementItem == null)
                {
                    lblTenantInfo.Text = "Invalid selection";
                    lblPropertyInfo.Text = "";
                    lblCurrentDue.Text = "";
                    return;
                }

                UpdateTenantInfo(agreementItem);
                UpdateCurrentDue(agreementItem);
                UpdateCommissionDetails(agreementItem);

                // Refresh filter in history tab
                LoadFilterAgreements();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in agreement selection: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTenantInfo(AgreementDisplayItem agreementItem)
        {
            try
            {
                if (_dataService == null || agreementItem == null)
                {
                    lblTenantInfo.Text = "Data not available";
                    lblPropertyInfo.Text = "";
                    return;
                }

                var tenants = _dataService.LoadTenants();
                var properties = _dataService.LoadProperties();
                var portions = _dataService.LoadPortions();

                var tenant = tenants?.FirstOrDefault(t => t.Id == agreementItem.TenantId);
                var property = properties?.FirstOrDefault(p => p.Id == agreementItem.PropertyId);
                var portion = portions?.FirstOrDefault(p => p.Id == agreementItem.PortionId);

                if (tenant != null)
                {
                    lblTenantInfo.Text = $"👤 Tenant: {tenant.Name} | 📱 Mobile: {tenant.Mobile} | Type: {tenant.Type}";
                }
                else
                {
                    lblTenantInfo.Text = "Tenant not found";
                }

                if (property != null && portion != null)
                {
                    lblPropertyInfo.Text = $"🏢 Property: {property.Name} | 📦 Portion: {portion.Name} ({portion.Size})";
                }
                else
                {
                    lblPropertyInfo.Text = "Property/Portion not found";
                }
            }
            catch (Exception ex)
            {
                lblTenantInfo.Text = $"Error: {ex.Message}";
                lblPropertyInfo.Text = "";
            }
        }

        private void UpdateCurrentDue(AgreementDisplayItem agreementItem)
        {
            try
            {
                if (agreementItem == null || agreementItem.Agreement == null || _dataService == null)
                {
                    lblCurrentDue.Text = "Unable to calculate due amount";
                    txtAmount.Text = "";
                    return;
                }

                var agreement = agreementItem.Agreement;
                var tenant = _dataService.LoadTenants()?.FirstOrDefault(t => t.Id == agreementItem.TenantId);

                if (tenant == null)
                {
                    lblCurrentDue.Text = "Tenant information not found";
                    txtAmount.Text = "";
                    return;
                }

                decimal currentDue = 0;
                string dueText = "";

                if (tenant.Type == TenantType.OnRent)
                {
                    var payments = _dataService.LoadPayments()
                        ?.Where(p => p.AgreementId == agreement.Id && p.PaymentType == PaymentType.Rent)
                        ?.ToList() ?? new List<Payment>();

                    DateTime currentDate = DateTime.Now;
                    int monthsPassed = ((currentDate.Year - agreement.StartDate.Year) * 12) +
                                       currentDate.Month - agreement.StartDate.Month;

                    if (currentDate.Day < agreement.StartDate.Day)
                        monthsPassed--;

                    monthsPassed = Math.Max(0, monthsPassed);

                    decimal totalRentDue = monthsPassed * agreement.MonthlyRent;
                    decimal totalRentPaid = payments.Sum(p => p.Amount);
                    currentDue = Math.Max(0, totalRentDue - totalRentPaid);

                    dueText = $"💰 Monthly Rent: {agreement.MonthlyRent:C} | " +
                              $"📅 Next Due: {agreement.StartDate.AddMonths(monthsPassed + 1):dd-MMM-yyyy}";
                }
                else if (tenant.Type == TenantType.OnCommission)
                {
                    dueText = $"📊 Commission Tenant";

                    if (agreement.CommissionRate.HasValue)
                    {
                        dueText += $" | Rate: {agreement.CommissionRate}%";
                    }

                    if (agreement.LastCommissionPaymentDate.HasValue)
                    {
                        dueText += $" | Last Payment: {agreement.LastCommissionPaymentDate.Value:dd-MMM-yyyy}";
                    }
                }

                lblCurrentDue.Text = $"⚠️ Current Due: {currentDue:C} | {dueText}";

                // Auto-fill amount with current due if there's a due amount
                if (currentDue > 0)
                {
                    txtAmount.Text = currentDue.ToString("F2");
                }
                else
                {
                    txtAmount.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblCurrentDue.Text = $"Error calculating due: {ex.Message}";
                txtAmount.Text = "";
            }
        }

        private void UpdateCommissionDetails(AgreementDisplayItem agreementItem)
        {
            try
            {
                if (agreementItem == null || _dataService == null)
                {
                    pnlCommissionDetails.Visible = false;
                    return;
                }

                var tenant = _dataService.LoadTenants()?.FirstOrDefault(t => t.Id == agreementItem.TenantId);

                // Show/Hide commission panel based on tenant type and payment type
                bool isCommissionTenant = tenant?.Type == TenantType.OnCommission;
                bool isCommissionPayment = cmbPaymentType?.SelectedItem?.ToString() == "Commission";

                pnlCommissionDetails.Visible = isCommissionTenant && isCommissionPayment;

                if (isCommissionTenant && isCommissionPayment)
                {
                    var agreement = agreementItem.Agreement;
                    if (agreement != null && agreement.CommissionRate.HasValue)
                    {
                        txtCommissionRate.Text = agreement.CommissionRate.Value.ToString("F2");
                    }

                    // Load products for this agreement
                    LoadProductsForAgreement(agreementItem.Id);
                    UpdateSalesTotal();
                }
                else
                {
                    _selectedProducts.Clear();
                    if (dgvProducts != null) dgvProducts.Rows.Clear();
                    if (txtSalesAmount != null) txtSalesAmount.Text = "";
                    if (txtCommissionAmount != null) txtCommissionAmount.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating commission details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductsForAgreement(int agreementId)
        {
            try
            {
                _selectedProducts.Clear();
                if (dgvProducts != null) dgvProducts.Rows.Clear();

                if (_dataService == null) return;

                var products = _dataService.GetProductsForAgreement(agreementId);
                if (products == null) return;

                foreach (var product in products)
                {
                    _selectedProducts.Add(new CommissionProduct
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Unit = product.Unit,
                        UnitPrice = product.UnitPrice,
                        Quantity = 0,
                        LaborAmount = 0
                    });
                }

                RefreshProductsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshProductsGrid()
        {
            try
            {
                if (dgvProducts == null) return;

                dgvProducts.Rows.Clear();
                foreach (var product in _selectedProducts.Where(p => p.Quantity > 0))
                {
                    dgvProducts.Rows.Add(
                        product.ProductName,
                        product.Quantity,
                        product.Unit,
                        product.UnitPrice.ToString("C"),
                        product.ProductTotal.ToString("C"),
                        product.LaborAmount.ToString("C"),
                        product.TotalAmount.ToString("C")
                    );
                }
                UpdateSalesTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing products grid: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSalesTotal()
        {
            try
            {
                if (txtSalesAmount == null || txtCommissionAmount == null || txtAmount == null) return;

                decimal totalSales = _selectedProducts.Sum(p => p.TotalAmount);
                txtSalesAmount.Text = totalSales.ToString("F2");

                if (decimal.TryParse(txtCommissionRate.Text, out decimal commissionRate) && commissionRate > 0)
                {
                    decimal commissionAmount = (totalSales * commissionRate) / 100;
                    txtCommissionAmount.Text = commissionAmount.ToString("F2");
                    txtAmount.Text = commissionAmount.ToString("F2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating sales total: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupPaymentTypeChange()
        {
            if (cmbPaymentType == null) return;

            cmbPaymentType.SelectedIndexChanged += (s, e) =>
            {
                if (cmbAgreement.SelectedItem != null)
                {
                    UpdateCommissionDetails(cmbAgreement.SelectedItem as AgreementDisplayItem);
                }
            };
        }

        // ========== EVENT HANDLERS ==========

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new Form())
                {
                    dialog.Text = "Add Product Sale";
                    dialog.Size = new Size(400, 300);
                    dialog.StartPosition = FormStartPosition.CenterParent;

                    if (_dataService == null) return;

                    var products = _dataService.GetAllActiveProducts();
                    var agreementItem = cmbAgreement.SelectedItem as AgreementDisplayItem;
                    var agreementProducts = agreementItem != null ?
                        _dataService.GetProductsForAgreement(agreementItem.Id) : products;

                    Label lblProduct = new Label
                    {
                        Text = "Product:",
                        Location = new Point(20, 20),
                        Size = new Size(80, 25)
                    };

                    ComboBox cmbProduct = new ComboBox
                    {
                        Location = new Point(110, 20),
                        Size = new Size(250, 25),
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };

                    foreach (var product in agreementProducts)
                    {
                        cmbProduct.Items.Add(new { Id = product.Id, Text = $"{product.Name} ({product.Unit}) - {product.UnitPrice:C}" });
                    }

                    if (cmbProduct.Items.Count > 0)
                        cmbProduct.SelectedIndex = 0;

                    Label lblQuantity = new Label
                    {
                        Text = "Quantity:",
                        Location = new Point(20, 60),
                        Size = new Size(80, 25)
                    };

                    TextBox txtQuantity = new TextBox
                    {
                        Location = new Point(110, 60),
                        Size = new Size(100, 25),
                        Text = "1"
                    };

                    Label lblLabor = new Label
                    {
                        Text = "Labor Amount:",
                        Location = new Point(20, 100),
                        Size = new Size(100, 25)
                    };

                    TextBox txtLabor = new TextBox
                    {
                        Location = new Point(130, 100),
                        Size = new Size(100, 25),
                        Text = "0"
                    };

                    Button btnAdd = new Button
                    {
                        Text = "Add",
                        Location = new Point(20, 150),
                        Size = new Size(100, 35),
                        BackColor = Color.LightGreen
                    };

                    btnAdd.Click += (s2, e2) =>
                    {
                        if (cmbProduct.SelectedItem != null &&
                            decimal.TryParse(txtQuantity.Text, out decimal quantity) &&
                            decimal.TryParse(txtLabor.Text, out decimal labor))
                        {
                            dynamic selectedProduct = cmbProduct.SelectedItem;
                            int productId = selectedProduct.Id;
                            var product = products.FirstOrDefault(p => p.Id == productId);

                            if (product != null)
                            {
                                var existingProduct = _selectedProducts.FirstOrDefault(p => p.ProductId == productId);
                                if (existingProduct != null)
                                {
                                    existingProduct.Quantity += quantity;
                                    existingProduct.LaborAmount += labor;
                                }
                                else
                                {
                                    _selectedProducts.Add(new CommissionProduct
                                    {
                                        ProductId = productId,
                                        ProductName = product.Name,
                                        Unit = product.Unit,
                                        UnitPrice = product.UnitPrice,
                                        Quantity = quantity,
                                        LaborAmount = labor
                                    });
                                }

                                RefreshProductsGrid();
                                dialog.DialogResult = DialogResult.OK;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter valid quantity and labor amount.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };

                    dialog.Controls.AddRange(new Control[] {
                        lblProduct, cmbProduct,
                        lblQuantity, txtQuantity,
                        lblLabor, txtLabor,
                        btnAdd
                    });

                    dialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRemoveProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProducts == null || dgvProducts.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a product to remove.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var selectedProductName = dgvProducts.SelectedRows[0].Cells[0].Value?.ToString();
                if (string.IsNullOrEmpty(selectedProductName)) return;

                var product = _selectedProducts.FirstOrDefault(p => p.ProductName == selectedProductName);
                if (product != null)
                {
                    _selectedProducts.Remove(product);
                    RefreshProductsGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing product: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtCommissionRate_TextChanged(object sender, EventArgs e)
        {
            UpdateSalesTotal();
        }

        private void TxtCommissionRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void TxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            UpdateSalesTotal();
        }

        private void ChkAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            // Reserved for future use
        }

        private int GetDaysFromFrequency(CommissionFrequency frequency, int? customDays)
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

        // ========== PAYMENT HISTORY METHODS ==========

        private void LoadPaymentHistory()
        {
            try
            {
                if (dgvPaymentHistory == null || _dataService == null) return;

                dgvPaymentHistory.Rows.Clear();
                dgvPaymentHistory.Columns.Clear();

                // Add columns
                dgvPaymentHistory.Columns.AddRange(
                    new DataGridViewTextBoxColumn { HeaderText = "Date", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Type", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Amount", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Notes", Width = 200 },
                    new DataGridViewTextBoxColumn { HeaderText = "Month/Year", Width = 120 },
                    new DataGridViewTextBoxColumn { HeaderText = "Sales Amount", Width = 120 },
                    new DataGridViewTextBoxColumn { HeaderText = "Commission", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Created", Width = 120 }
                );

                var allPayments = _dataService.LoadPayments();
                var agreements = _dataService.LoadAgreements();
                var tenants = _dataService.LoadTenants();
                var properties = _dataService.LoadProperties();

                if (allPayments == null || agreements == null || tenants == null || properties == null)
                {
                    UpdateHistorySummary(0, 0);
                    return;
                }

                // Filter by selected agreement if any
                int? selectedAgreementId = null;
                if (cmbFilterAgreement != null && cmbFilterAgreement.SelectedIndex > 0)
                {
                    if (cmbFilterAgreement.SelectedItem is AgreementDisplayItem selectedItem)
                    {
                        selectedAgreementId = selectedItem.Id;
                    }
                }

                var filteredPayments = allPayments.Where(p =>
                    !selectedAgreementId.HasValue || p.AgreementId == selectedAgreementId.Value)
                    .OrderByDescending(p => p.PaymentDate)
                    .ToList();

                decimal totalAmount = 0;

                foreach (var payment in filteredPayments)
                {
                    var agreement = agreements.FirstOrDefault(a => a.Id == payment.AgreementId);
                    var tenant = tenants.FirstOrDefault(t => t.Id == agreement?.TenantId);
                    var property = properties.FirstOrDefault(p => p.Id == agreement?.PropertyId);

                    int rowIndex = dgvPaymentHistory.Rows.Add(
                        payment.PaymentDate.ToString("dd-MMM-yyyy"),
                        payment.PaymentType.ToString(),
                        payment.Amount.ToString("C"),
                        payment.Notes,
                        payment.MonthYear,
                        payment.SalesAmount?.ToString("C") ?? "",
                        payment.CommissionEarned?.ToString("C") ?? "",
                        payment.CreatedDate.ToString("dd-MMM-yyyy HH:mm")
                    );

                    var row = dgvPaymentHistory.Rows[rowIndex];

                    // Color coding
                    if (payment.PaymentType == PaymentType.Rent)
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    else if (payment.PaymentType == PaymentType.Commission)
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                    else if (payment.PaymentType == PaymentType.SecurityDeposit)
                        row.DefaultCellStyle.BackColor = Color.LightYellow;

                    if (payment.IsDeleted)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                        row.Cells["Notes"].Value = $"[DELETED] {payment.Notes}";
                    }

                    totalAmount += payment.Amount;
                }

                UpdateHistorySummary(filteredPayments.Count, totalAmount);
                dgvPaymentHistory.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payment history: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateHistorySummary(0, 0);
            }
        }

        private void UpdateHistorySummary(int count, decimal totalAmount)
        {
            var lblSummary = tabControl?.TabPages[1]?.Controls?.Find("lblHistorySummary", true)?.FirstOrDefault() as Label;
            if (lblSummary != null)
            {
                lblSummary.Text = $"📊 Total Payments: {count} | 💰 Total Amount: {totalAmount:C}";
            }
        }

        // ========== SAVE PAYMENT METHODS ==========

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbAgreement == null || cmbPaymentType == null || txtAmount == null)
                {
                    MessageBox.Show("Form not properly initialized. Please restart the application.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cmbAgreement.SelectedItem == null)
                {
                    MessageBox.Show("Please select an agreement.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!(cmbAgreement.SelectedItem is AgreementDisplayItem agreementItem))
                {
                    MessageBox.Show("Invalid agreement selected.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtAmount.Text) ||
                    !decimal.TryParse(txtAmount.Text, out decimal amount) ||
                    amount <= 0)
                {
                    MessageBox.Show("Please enter a valid payment amount.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAmount.Focus();
                    return;
                }

                bool success = SavePayment(agreementItem, amount);

                if (success)
                {
                    // Clear form for next entry
                    txtAmount.Text = "";
                    txtNotes.Text = "";
                    _selectedProducts.Clear();
                    if (dgvProducts != null) dgvProducts.Rows.Clear();
                    if (txtSalesAmount != null) txtSalesAmount.Text = "";
                    if (txtCommissionAmount != null) txtCommissionAmount.Text = "";

                    // Refresh history and switch to history tab
                    LoadPaymentHistory();

                    if (tabControl != null)
                    {
                        tabControl.SelectedIndex = 1;
                    }

                    MessageBox.Show("✅ Payment saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving payment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SavePayment(AgreementDisplayItem agreementItem, decimal amount)
        {
            try
            {
                if (_dataService == null) return false;

                var paymentType = (PaymentType)Enum.Parse(typeof(PaymentType), cmbPaymentType.SelectedItem.ToString());

                var allPayments = _dataService.LoadAllPayments();
                if (allPayments == null) allPayments = new List<Payment>();

                int newId = allPayments.Count > 0 ? allPayments.Max(p => p.Id) + 1 : 1;

                var payment = new Payment
                {
                    Id = newId,
                    AgreementId = agreementItem.Id,
                    Amount = amount,
                    PaymentDate = dtpPaymentDate.Value,
                    MonthYear = dtpPaymentDate.Value.ToString("MMMM yyyy"),
                    Notes = txtNotes.Text,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    DeletedDate = null,
                    PaymentType = paymentType
                };

                // Add commission details if applicable
                if (paymentType == PaymentType.Commission && agreementItem.TenantType == TenantType.OnCommission)
                {
                    decimal totalSales = _selectedProducts.Sum(p => p.TotalAmount);
                    decimal commissionRate = decimal.TryParse(txtCommissionRate.Text, out decimal rate) ? rate : 0;

                    payment.SalesAmount = totalSales;
                    payment.CommissionEarned = (totalSales * commissionRate) / 100;

                    if (_selectedProducts.Any())
                    {
                        var firstProduct = _selectedProducts.First();
                        payment.ProductId = firstProduct.ProductId;
                        payment.ProductName = firstProduct.ProductName;
                        payment.Quantity = firstProduct.Quantity;
                        payment.Unit = firstProduct.Unit;
                        payment.UnitPrice = firstProduct.UnitPrice;
                        payment.LaborAmount = firstProduct.LaborAmount;
                        payment.ProductTotal = firstProduct.ProductTotal;
                    }
                }

                allPayments.Add(payment);
                _dataService.SavePayments(allPayments);

                // Update agreement last payment date for commission payments
                if (paymentType == PaymentType.Commission)
                {
                    var agreements = _dataService.LoadAgreements();
                    var agreement = agreements.FirstOrDefault(a => a.Id == agreementItem.Id);
                    if (agreement != null)
                    {
                        agreement.LastCommissionPaymentDate = dtpPaymentDate.Value;
                        _dataService.SaveAgreements(agreements);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in SavePayment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}