using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;

namespace ShopRentManagementSystem
{
    public partial class FrmRentAgreement : Form
    {
        private readonly JsonDataService _dataService;
        private DataGridView dgvAgreements;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;

        public FrmRentAgreement()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            LoadAgreements();
        }

        private void InitializeComponent()
        {
            this.Text = "Rent Agreement Management";
            this.Size = new Size(1200, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Data Grid View
            dgvAgreements = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false
            };

            // Setup columns
            dgvAgreements.Columns.Add("Id", "ID");
            dgvAgreements.Columns.Add("PropertyName", "Property");
            dgvAgreements.Columns.Add("PortionName", "Portion");
            dgvAgreements.Columns.Add("TenantName", "Tenant");
            dgvAgreements.Columns.Add("TenantType", "Type");
            dgvAgreements.Columns.Add("MonthlyRent", "Monthly Rent");
            dgvAgreements.Columns.Add("StartDate", "Start Date");
            dgvAgreements.Columns.Add("IncreaseMode", "Increase Mode");
            dgvAgreements.Columns.Add("CommissionRate", "Commission %");
            dgvAgreements.Columns.Add("PaymentFrequency", "Pay Frequency");
            dgvAgreements.Columns.Add("DailyMinTarget", "Daily Target");
            dgvAgreements.Columns.Add("IsActive", "Active");

            dgvAgreements.Columns["Id"].Width = 50;
            dgvAgreements.Columns["MonthlyRent"].DefaultCellStyle.Format = "C";
            dgvAgreements.Columns["StartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dgvAgreements.Columns["DailyMinTarget"].DefaultCellStyle.Format = "C";
            dgvAgreements.Columns["IsActive"].Width = 60;

            // Cell formatting for better readability
            dgvAgreements.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    var row = dgvAgreements.Rows[e.RowIndex];

                    // Color code based on tenant type
                    if (e.ColumnIndex == dgvAgreements.Columns["TenantType"].Index && e.Value != null)
                    {
                        if (e.Value.ToString() == "OnCommission")
                        {
                            e.CellStyle.BackColor = Color.LightGoldenrodYellow;
                            e.CellStyle.ForeColor = Color.DarkGoldenrod;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                        else
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkGreen;
                        }
                    }

                    // Highlight zero rent for commission tenants
                    if (e.ColumnIndex == dgvAgreements.Columns["MonthlyRent"].Index && e.Value != null)
                    {
                        if (decimal.TryParse(e.Value.ToString().Replace("$", "").Replace(",", ""), out decimal rent))
                        {
                            if (rent == 0)
                            {
                                e.CellStyle.BackColor = Color.LightGray;
                                e.CellStyle.ForeColor = Color.DarkGray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Italic);
                            }
                        }
                    }

                    // Highlight zero commission
                    if (e.ColumnIndex == dgvAgreements.Columns["CommissionRate"].Index && e.Value != null)
                    {
                        if (e.Value.ToString() == "0%" || e.Value.ToString() == "0.0%")
                        {
                            e.CellStyle.BackColor = Color.LightGray;
                            e.CellStyle.ForeColor = Color.DarkGray;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Italic);
                        }
                    }
                }
            };

            // Buttons Panel
            Panel pnlButtons = new Panel
            {
                Height = 50,
                Dock = DockStyle.Bottom,
                BackColor = Color.LightGray
            };

            btnAdd = new Button
            {
                Text = "➕ Add New Agreement",
                Location = new Point(10, 10),
                Size = new Size(150, 30),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnAdd.Click += BtnAdd_Click;

            btnEdit = new Button
            {
                Text = "✏️ Edit",
                Location = new Point(170, 10),
                Size = new Size(80, 30)
            };
            btnEdit.Click += BtnEdit_Click;

            btnDelete = new Button
            {
                Text = "🗑️ Delete",
                Location = new Point(260, 10),
                Size = new Size(80, 30),
                BackColor = Color.LightPink
            };
            btnDelete.Click += BtnDelete_Click;

            pnlButtons.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete });

            this.Controls.AddRange(new Control[] { dgvAgreements, pnlButtons });
        }

        private void LoadAgreements()
        {
            var agreements = _dataService.LoadAgreements();
            var properties = _dataService.LoadProperties();
            var portions = _dataService.LoadPortions();
            var tenants = _dataService.LoadTenants();

            dgvAgreements.Rows.Clear();

            foreach (var agreement in agreements)
            {
                var property = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);
                var portion = portions.FirstOrDefault(p => p.Id == agreement.PortionId);
                var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);

                dgvAgreements.Rows.Add(
                    agreement.Id,
                    property?.Name ?? "N/A",
                    portion?.Name ?? "N/A",
                    tenant?.Name ?? "N/A",
                    tenant?.Type.ToString() ?? "N/A",
                    agreement.MonthlyRent,
                    agreement.StartDate,
                    agreement.IncreaseMode.ToString(),
                    agreement.CommissionRate?.ToString("F1") + "%" ?? "N/A",
                    agreement.PaymentFrequency?.ToString() ?? "N/A",
                    agreement.DailyMinimumTarget?.ToString("C") ?? "N/A",
                    agreement.IsActive ? "✅ Yes" : "❌ No"
                );
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var dialog = new Form())
            {
                dialog.Text = "Add New Rent Agreement";
                dialog.Size = new Size(600, 700); // Increased height for product selection
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.BackColor = SystemColors.Control;

                var properties = _dataService.LoadProperties();
                var portions = _dataService.LoadPortions();
                var tenants = _dataService.LoadTenants();

                int yPos = 20;
                int labelWidth = 160;
                int controlWidth = 250;

                // Property
                var lblProperty = new Label
                {
                    Text = "🏢 Property:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var cmbProperty = new ComboBox
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9)
                };

                foreach (var property in properties)
                {
                    cmbProperty.Items.Add(new { Id = property.Id, Name = property.Name });
                }

                if (cmbProperty.Items.Count > 0)
                    cmbProperty.SelectedIndex = 0;
                yPos += 35;

                // Portion
                var lblPortion = new Label
                {
                    Text = "📍 Portion:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var cmbPortion = new ComboBox
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9)
                };

                cmbProperty.SelectedIndexChanged += (s, args) =>
                {
                    cmbPortion.Items.Clear();
                    if (cmbProperty.SelectedItem != null)
                    {
                        var selectedProperty = (dynamic)cmbProperty.SelectedItem;
                        var filteredPortions = portions.Where(p => p.PropertyId == selectedProperty.Id && p.IsActive).ToList();

                        foreach (var portion in filteredPortions)
                        {
                            cmbPortion.Items.Add(new { Id = portion.Id, Name = $"{portion.Name} ({portion.Size})" });
                        }

                        if (cmbPortion.Items.Count > 0)
                            cmbPortion.SelectedIndex = 0;
                    }
                };

                // Trigger initial load
                if (cmbProperty.Items.Count > 0)
                {
                    var tempArgs = EventArgs.Empty;
                    cmbProperty_SelectedIndexChanged(cmbProperty, tempArgs);
                }
                yPos += 35;

                void cmbProperty_SelectedIndexChanged(object sender, EventArgs e)
                {
                    cmbPortion.Items.Clear();
                    if (cmbProperty.SelectedItem != null)
                    {
                        var selectedProperty = (dynamic)cmbProperty.SelectedItem;
                        var filteredPortions = portions.Where(p => p.PropertyId == selectedProperty.Id && p.IsActive).ToList();

                        foreach (var portion in filteredPortions)
                        {
                            cmbPortion.Items.Add(new { Id = portion.Id, Name = $"{portion.Name} ({portion.Size})" });
                        }

                        if (cmbPortion.Items.Count > 0)
                            cmbPortion.SelectedIndex = 0;
                    }
                }

                cmbProperty.SelectedIndexChanged += cmbProperty_SelectedIndexChanged;

                // Tenant
                var lblTenant = new Label
                {
                    Text = "👤 Tenant:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var cmbTenant = new ComboBox
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9)
                };

                foreach (var tenant in tenants)
                {
                    cmbTenant.Items.Add(new { Id = tenant.Id, Name = $"{tenant.Name} ({tenant.Type})" });
                }

                if (cmbTenant.Items.Count > 0)
                    cmbTenant.SelectedIndex = 0;
                yPos += 35;

                // Monthly Rent
                var lblRent = new Label
                {
                    Text = "💰 Monthly Rent:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var numRent = new NumericUpDown
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    Minimum = 0,  // Allow zero for commission tenants
                    Maximum = 1000000,
                    DecimalPlaces = 2,
                    Font = new Font("Segoe UI", 9)
                };
                var lblRentHint = new Label
                {
                    Text = "Can be 0 for commission tenants",
                    Location = new Point(450, yPos),
                    Size = new Size(150, 25),
                    Font = new Font("Segoe UI", 8, FontStyle.Italic),
                    ForeColor = Color.Gray
                };
                yPos += 35;

                // Start Date
                var lblStartDate = new Label
                {
                    Text = "📅 Start Date:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var dtpStartDate = new DateTimePicker
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    Value = DateTime.Now,
                    Font = new Font("Segoe UI", 9)
                };
                yPos += 35;

                // Increase Mode
                var lblIncreaseMode = new Label
                {
                    Text = "📈 Increase Mode:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var cmbIncreaseMode = new ComboBox
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9)
                };
                cmbIncreaseMode.Items.AddRange(new[] { "Auto", "Manual" });
                cmbIncreaseMode.SelectedIndex = 0;
                yPos += 35;

                // Commission Fields Panel (initially hidden)
                Panel pnlCommission = new Panel
                {
                    Location = new Point(20, yPos),
                    Size = new Size(550, 160),
                    Visible = false,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.FromArgb(255, 255, 240)
                };

                var lblCommissionHeader = new Label
                {
                    Text = "📊 COMMISSION SETTINGS",
                    Location = new Point(10, 10),
                    Size = new Size(250, 25),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.DarkGoldenrod
                };

                var lblCommissionRate = new Label
                {
                    Text = "Commission Rate %:",
                    Location = new Point(10, 45),
                    Size = new Size(150, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var numCommissionRate = new NumericUpDown
                {
                    Location = new Point(170, 45),
                    Size = new Size(100, 25),
                    Minimum = 0,  // Allow zero commission
                    Maximum = 100,
                    DecimalPlaces = 2,
                    Value = 15,
                    Font = new Font("Segoe UI", 9)
                };
                var lblCommissionHint = new Label
                {
                    Text = "Can be 0 for flat fee arrangements",
                    Location = new Point(280, 45),
                    Size = new Size(200, 25),
                    Font = new Font("Segoe UI", 8, FontStyle.Italic),
                    ForeColor = Color.Gray
                };

                var lblDailyTarget = new Label
                {
                    Text = "Daily Minimum Target:",
                    Location = new Point(10, 80),
                    Size = new Size(150, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var numDailyTarget = new NumericUpDown
                {
                    Location = new Point(170, 80),
                    Size = new Size(100, 25),
                    Minimum = 0,
                    Maximum = 100000,
                    DecimalPlaces = 2,
                    Font = new Font("Segoe UI", 9)
                };

                var lblPaymentFreq = new Label
                {
                    Text = "Payment Frequency:",
                    Location = new Point(10, 115),
                    Size = new Size(150, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var cmbPaymentFreq = new ComboBox
                {
                    Location = new Point(170, 115),
                    Size = new Size(150, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9)
                };
                cmbPaymentFreq.Items.AddRange(new[] { "Daily", "Every5Days", "Every10Days", "Weekly", "Monthly", "Custom" });
                cmbPaymentFreq.SelectedIndex = 2; // Every10Days

                pnlCommission.Controls.AddRange(new Control[] {
                    lblCommissionHeader, lblCommissionRate, numCommissionRate, lblCommissionHint,
                    lblDailyTarget, numDailyTarget, lblPaymentFreq, cmbPaymentFreq
                });
                yPos += 170;

                // Product Selection Panel (for commission tenants)
                Panel pnlProducts = new Panel
                {
                    Location = new Point(20, yPos),
                    Size = new Size(550, 120),
                    Visible = false,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.FromArgb(240, 248, 255)
                };

                var lblProductsHeader = new Label
                {
                    Text = "📦 PRODUCTS FOR COMMISSION",
                    Location = new Point(10, 10),
                    Size = new Size(250, 25),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.DarkSlateBlue
                };

                CheckedListBox chkProducts = new CheckedListBox
                {
                    Location = new Point(10, 40),
                    Size = new Size(530, 70),
                    CheckOnClick = true,
                    Font = new Font("Segoe UI", 9)
                };

                pnlProducts.Controls.AddRange(new Control[] { lblProductsHeader, chkProducts });
                yPos += 130;

                // Show/hide commission and product fields based on tenant type
                cmbTenant.SelectedIndexChanged += (s, args) =>
                {
                    if (cmbTenant.SelectedItem != null)
                    {
                        var selectedTenant = (dynamic)cmbTenant.SelectedItem;
                        var tenant = tenants.FirstOrDefault(t => t.Id == selectedTenant.Id);
                        bool isCommission = tenant?.Type == TenantType.OnCommission;
                        pnlCommission.Visible = isCommission;
                        pnlProducts.Visible = isCommission;

                        // Load products for commission tenants
                        if (isCommission)
                        {
                            chkProducts.Items.Clear();
                            var products = _dataService.LoadProducts().Where(p => p.IsActive).ToList();
                            foreach (var product in products)
                            {
                                chkProducts.Items.Add(new
                                {
                                    Id = product.Id,
                                    Text = $"{product.Name} ({product.UnitPrice:C}/{product.Unit})"
                                });
                            }
                        }

                        // Adjust dialog height
                        dialog.Height = isCommission ? 700 : 700;

                        // Update validation message
                        if (isCommission)
                        {
                            lblRentHint.Text = "Rent can be 0 for commission tenants";
                        }
                        else
                        {
                            lblRentHint.Text = "Rent required for rent tenants";
                        }
                    }
                };

                // Buttons
                var btnSave = new Button
                {
                    Text = "💾 Save Agreement",
                    Location = new Point(190, yPos),
                    Size = new Size(150, 35),
                    BackColor = Color.LightGreen,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                var btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new Point(350, yPos),
                    Size = new Size(100, 35),
                    DialogResult = DialogResult.Cancel,
                    Font = new Font("Segoe UI", 9)
                };

                btnSave.Click += (s, args) =>
                {
                    if (cmbProperty.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a property.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (cmbPortion.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a portion.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (cmbTenant.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a tenant.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var selectedTenant = (dynamic)cmbTenant.SelectedItem;
                    var tenant = tenants.FirstOrDefault(t => t.Id == selectedTenant.Id);
                    bool isCommission = tenant?.Type == TenantType.OnCommission;

                    // Different validation for rent vs commission tenants
                    if (!isCommission && numRent.Value <= 0)
                    {
                        MessageBox.Show("Monthly rent is required for rent tenants.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Check if portion is already occupied
                    var selectedPortion = (dynamic)cmbPortion.SelectedItem;
                    var existingAgreements = _dataService.LoadAgreements();
                    var existingForPortion = existingAgreements.FirstOrDefault(a =>
                        a.PortionId == selectedPortion.Id && a.IsActive);

                    if (existingForPortion != null)
                    {
                        MessageBox.Show($"This portion is already occupied by another tenant.\n" +
                            $"Please select a different portion.",
                            "Portion Occupied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var agreements = _dataService.LoadAgreements();
                    var newId = agreements.Count > 0 ? agreements.Max(a => a.Id) + 1 : 1;

                    var selectedProperty = (dynamic)cmbProperty.SelectedItem;

                    var agreement = new RentAgreement
                    {
                        Id = newId,
                        PropertyId = selectedProperty.Id,
                        PortionId = selectedPortion.Id,
                        TenantId = selectedTenant.Id,
                        MonthlyRent = numRent.Value,
                        StartDate = dtpStartDate.Value,
                        IncreaseMode = cmbIncreaseMode.SelectedItem.ToString() == "Auto" ?
                            RentIncreaseMode.Auto : RentIncreaseMode.Manual,
                        LastIncreaseDate = dtpStartDate.Value,
                        IsActive = true
                    };

                    if (isCommission)
                    {
                        agreement.CommissionRate = numCommissionRate.Value;
                        agreement.DailyMinimumTarget = numDailyTarget.Value > 0 ? numDailyTarget.Value : (decimal?)null;
                        agreement.PaymentFrequency = (CommissionFrequency)Enum.Parse(
                            typeof(CommissionFrequency), cmbPaymentFreq.SelectedItem.ToString());
                        agreement.LastCommissionPaymentDate = dtpStartDate.Value;

                        // Get selected products
                        var selectedProducts = new List<int>();
                        foreach (var item in chkProducts.CheckedItems)
                        {
                            dynamic productItem = item;
                            selectedProducts.Add(productItem.Id);
                        }
                        agreement.ProductIds = selectedProducts;
                    }

                    agreements.Add(agreement);
                    _dataService.SaveAgreements(agreements);
                    LoadAgreements();
                    dialog.DialogResult = DialogResult.OK;
                };

                btnCancel.Click += (s, args) => dialog.DialogResult = DialogResult.Cancel;

                dialog.Controls.AddRange(new Control[] {
                    lblProperty, cmbProperty, lblPortion, cmbPortion, lblTenant, cmbTenant,
                    lblRent, numRent, lblRentHint, lblStartDate, dtpStartDate,
                    lblIncreaseMode, cmbIncreaseMode, pnlCommission, pnlProducts,
                    btnSave, btnCancel
                });

                // Set focus to first control
                dialog.Shown += (s, args) => cmbProperty.Focus();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("✅ Rent agreement added successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAgreements.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an agreement to edit.", "Select Agreement",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvAgreements.SelectedRows[0];
            var agreementId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            var agreements = _dataService.LoadAgreements();
            var properties = _dataService.LoadProperties();
            var portions = _dataService.LoadPortions();
            var agreement = agreements.FirstOrDefault(a => a.Id == agreementId);
            var tenants = _dataService.LoadTenants();
            var tenant = tenants.FirstOrDefault(t => t.Id == agreement?.TenantId);

            if (agreement == null) return;

            // Declare commission controls at the class scope
            NumericUpDown numCommissionRate = null;
            NumericUpDown numDailyTarget = null;
            ComboBox cmbPortion = null; // Add this

            using (var dialog = new Form())
            {
                dialog.Text = "Edit Rent Agreement";
                dialog.Size = new Size(500, 500); // Increased size for new controls
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.BackColor = SystemColors.Control;

                int yPos = 20;
                int labelWidth = 160;
                int controlWidth = 200;

                // Portion Selection
                var lblPortion = new Label
                {
                    Text = "📍 Portion:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                cmbPortion = new ComboBox // Assign to outer variable
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9)
                };

                // Load portions for the current property
                var currentProperty = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);
                var currentPortion = portions.FirstOrDefault(p => p.Id == agreement.PortionId);
                var availablePortions = portions.Where(p =>
                    p.PropertyId == agreement.PropertyId &&
                    (p.Id == agreement.PortionId || !agreements.Any(a => a.PortionId == p.Id && a.IsActive && a.Id != agreement.Id))
                ).ToList();

                foreach (var portion in availablePortions)
                {
                    cmbPortion.Items.Add(new { Id = portion.Id, Name = $"{portion.Name} ({portion.Size})" });
                    if (portion.Id == agreement.PortionId)
                    {
                        // Set as selected
                        var itemToSelect = cmbPortion.Items.Cast<dynamic>()
                            .FirstOrDefault(item => item.Id == portion.Id);
                        if (itemToSelect != null)
                            cmbPortion.SelectedItem = itemToSelect;
                    }
                }

                if (cmbPortion.Items.Count > 0 && cmbPortion.SelectedItem == null)
                    cmbPortion.SelectedIndex = 0;

                yPos += 35;

                // Monthly Rent
                var lblRent = new Label
                {
                    Text = "💰 Monthly Rent:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var numRent = new NumericUpDown
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    Minimum = 0,  // Allow zero for commission tenants
                    Maximum = 1000000,
                    DecimalPlaces = 2,
                    Value = agreement.MonthlyRent,
                    Font = new Font("Segoe UI", 9)
                };
                yPos += 35;

                // Show hint if commission tenant
                if (tenant?.Type == TenantType.OnCommission)
                {
                    var lblHint = new Label
                    {
                        Text = "Rent can be 0 for commission tenants",
                        Location = new Point(400, yPos - 30),
                        Size = new Size(150, 25),
                        Font = new Font("Segoe UI", 8, FontStyle.Italic),
                        ForeColor = Color.Gray
                    };
                    dialog.Controls.Add(lblHint);
                }

                // Start Date
                var lblStartDate = new Label
                {
                    Text = "📅 Start Date:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var dtpStartDate = new DateTimePicker
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    Value = agreement.StartDate,
                    Font = new Font("Segoe UI", 9)
                };
                yPos += 35;

                // Last Increase Date
                var lblLastIncreaseDate = new Label
                {
                    Text = "📈 Last Increase Date:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var dtpLastIncreaseDate = new DateTimePicker
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    Value = agreement.LastIncreaseDate,
                    Font = new Font("Segoe UI", 9)
                };
                yPos += 35;

                // Next Due Date
                var lblNextDueDate = new Label
                {
                    Text = "⏳ Next Due Date:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var dtpNextDueDate = new DateTimePicker
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    Value = agreement.NextDueDate ?? DateTime.Now.AddMonths(1),
                    Font = new Font("Segoe UI", 9)
                };
                yPos += 35;

                // Increase Mode
                var lblIncreaseMode = new Label
                {
                    Text = "📊 Increase Mode:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var cmbIncreaseMode = new ComboBox
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9)
                };
                cmbIncreaseMode.Items.AddRange(new[] { "Auto", "Manual" });
                cmbIncreaseMode.SelectedItem = agreement.IncreaseMode.ToString();
                yPos += 35;

                // Commission Fields if commission tenant
                if (tenant?.Type == TenantType.OnCommission)
                {
                    Panel pnlCommission = new Panel
                    {
                        Location = new Point(20, yPos),
                        Size = new Size(450, 100),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.FromArgb(255, 255, 240)
                    };

                    var lblCommissionRate = new Label
                    {
                        Text = "Commission Rate %:",
                        Location = new Point(10, 10),
                        Size = new Size(150, 25),
                        Font = new Font("Segoe UI", 9)
                    };
                    numCommissionRate = new NumericUpDown  // Assign to outer variable
                    {
                        Location = new Point(170, 10),
                        Size = new Size(100, 25),
                        Minimum = 0,  // Allow zero
                        Maximum = 100,
                        DecimalPlaces = 2,
                        Value = agreement.CommissionRate ?? 0,
                        Font = new Font("Segoe UI", 9)
                    };

                    var lblDailyTarget = new Label
                    {
                        Text = "Daily Minimum Target:",
                        Location = new Point(10, 45),
                        Size = new Size(150, 25),
                        Font = new Font("Segoe UI", 9)
                    };
                    numDailyTarget = new NumericUpDown  // Assign to outer variable
                    {
                        Location = new Point(170, 45),
                        Size = new Size(100, 25),
                        Minimum = 0,
                        Maximum = 100000,
                        DecimalPlaces = 2,
                        Value = agreement.DailyMinimumTarget ?? 0,
                        Font = new Font("Segoe UI", 9)
                    };

                    pnlCommission.Controls.AddRange(new Control[] {
                lblCommissionRate, numCommissionRate, lblDailyTarget, numDailyTarget
            });
                    dialog.Controls.Add(pnlCommission);
                    yPos += 110;
                }

                // Active Status
                var chkActive = new CheckBox
                {
                    Text = "✅ Active Agreement",
                    Location = new Point(20, yPos),
                    Size = new Size(200, 25),
                    Checked = agreement.IsActive,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };
                yPos += 40;

                // Buttons
                var btnSave = new Button
                {
                    Text = "💾 Update Agreement",
                    Location = new Point(190, yPos),
                    Size = new Size(150, 35),
                    BackColor = Color.LightBlue,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                var btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new Point(350, yPos),
                    Size = new Size(100, 35),
                    DialogResult = DialogResult.Cancel,
                    Font = new Font("Segoe UI", 9)
                };

                btnSave.Click += (s, args) =>
                {
                    // Validation based on tenant type
                    if (tenant?.Type != TenantType.OnCommission && numRent.Value <= 0)
                    {
                        MessageBox.Show("Monthly rent is required for rent tenants.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Check if changing portion to an occupied one
                    if (cmbPortion.SelectedItem != null)
                    {
                        var selectedPortion = (dynamic)cmbPortion.SelectedItem;
                        if (selectedPortion.Id != agreement.PortionId)
                        {
                            var isPortionOccupied = agreements.Any(a =>
                                a.PortionId == selectedPortion.Id &&
                                a.IsActive &&
                                a.Id != agreement.Id);

                            if (isPortionOccupied)
                            {
                                MessageBox.Show("The selected portion is already occupied by another tenant.",
                                    "Portion Occupied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    // Update agreement
                    agreement.MonthlyRent = numRent.Value;
                    agreement.StartDate = dtpStartDate.Value;
                    agreement.LastIncreaseDate = dtpLastIncreaseDate.Value;
                    //agreement.NextDueDate = dtpNextDueDate.Value;
                    agreement.IncreaseMode = cmbIncreaseMode.SelectedItem.ToString() == "Auto" ?
                        RentIncreaseMode.Auto : RentIncreaseMode.Manual;
                    agreement.IsActive = chkActive.Checked;

                    // Update portion if changed
                    if (cmbPortion.SelectedItem != null)
                    {
                        var selectedPortion = (dynamic)cmbPortion.SelectedItem;
                        agreement.PortionId = selectedPortion.Id;
                    }

                    // Update commission fields if commission tenant
                    if (tenant?.Type == TenantType.OnCommission)
                    {
                        agreement.CommissionRate = numCommissionRate?.Value ?? 0;
                        agreement.DailyMinimumTarget = numDailyTarget?.Value > 0 ? numDailyTarget.Value : (decimal?)null;
                    }

                    _dataService.SaveAgreements(agreements);
                    LoadAgreements();
                    dialog.DialogResult = DialogResult.OK;
                };

                btnCancel.Click += (s, args) => dialog.DialogResult = DialogResult.Cancel;

                dialog.Controls.AddRange(new Control[] {
            lblPortion, cmbPortion,
            lblRent, numRent, lblStartDate, dtpStartDate,
            lblLastIncreaseDate, dtpLastIncreaseDate, lblNextDueDate, dtpNextDueDate,
            lblIncreaseMode, cmbIncreaseMode, chkActive, btnSave, btnCancel
        });

                // Set focus to portion field
                dialog.Shown += (s, args) => cmbPortion.Focus();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("✅ Rent agreement updated successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        /*
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAgreements.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an agreement to edit.", "Select Agreement",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvAgreements.SelectedRows[0];
            var agreementId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            var agreements = _dataService.LoadAgreements();
            var agreement = agreements.FirstOrDefault(a => a.Id == agreementId);
            var tenants = _dataService.LoadTenants();
            var tenant = tenants.FirstOrDefault(t => t.Id == agreement?.TenantId);

            if (agreement == null) return;

            // Declare commission controls at the class scope
            NumericUpDown numCommissionRate = null;
            NumericUpDown numDailyTarget = null;

            using (var dialog = new Form())
            {
                dialog.Text = "Edit Rent Agreement";
                dialog.Size = new Size(500, 450);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.BackColor = SystemColors.Control;

                int yPos = 20;
                int labelWidth = 160;
                int controlWidth = 200;

                // Monthly Rent
                var lblRent = new Label
                {
                    Text = "💰 Monthly Rent:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var numRent = new NumericUpDown
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    Minimum = 0,  // Allow zero for commission tenants
                    Maximum = 1000000,
                    DecimalPlaces = 2,
                    Value = agreement.MonthlyRent,
                    Font = new Font("Segoe UI", 9)
                };
                yPos += 35;

                // Show hint if commission tenant
                if (tenant?.Type == TenantType.OnCommission)
                {
                    var lblHint = new Label
                    {
                        Text = "Rent can be 0 for commission tenants",
                        Location = new Point(400, yPos - 30),
                        Size = new Size(150, 25),
                        Font = new Font("Segoe UI", 8, FontStyle.Italic),
                        ForeColor = Color.Gray
                    };
                    dialog.Controls.Add(lblHint);
                }

                // Start Date
                var lblStartDate = new Label
                {
                    Text = "📅 Start Date:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var dtpStartDate = new DateTimePicker
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    Value = agreement.StartDate,
                    Font = new Font("Segoe UI", 9)
                };
                yPos += 35;

                // Last Increase Date
                var lblLastIncreaseDate = new Label
                {
                    Text = "📈 Last Increase Date:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var dtpLastIncreaseDate = new DateTimePicker
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    Value = agreement.LastIncreaseDate,
                    Font = new Font("Segoe UI", 9)
                };
                yPos += 35;

                // Increase Mode
                var lblIncreaseMode = new Label
                {
                    Text = "📊 Increase Mode:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9)
                };
                var cmbIncreaseMode = new ComboBox
                {
                    Location = new Point(190, yPos),
                    Size = new Size(controlWidth, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9)
                };
                cmbIncreaseMode.Items.AddRange(new[] { "Auto", "Manual" });
                cmbIncreaseMode.SelectedItem = agreement.IncreaseMode.ToString();
                yPos += 35;

                // Commission Fields if commission tenant
                if (tenant?.Type == TenantType.OnCommission)
                {
                    Panel pnlCommission = new Panel
                    {
                        Location = new Point(20, yPos),
                        Size = new Size(450, 100),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.FromArgb(255, 255, 240)
                    };

                    var lblCommissionRate = new Label
                    {
                        Text = "Commission Rate %:",
                        Location = new Point(10, 10),
                        Size = new Size(150, 25),
                        Font = new Font("Segoe UI", 9)
                    };
                    numCommissionRate = new NumericUpDown  // Assign to outer variable
                    {
                        Location = new Point(170, 10),
                        Size = new Size(100, 25),
                        Minimum = 0,  // Allow zero
                        Maximum = 100,
                        DecimalPlaces = 2,
                        Value = agreement.CommissionRate ?? 0,
                        Font = new Font("Segoe UI", 9)
                    };

                    var lblDailyTarget = new Label
                    {
                        Text = "Daily Minimum Target:",
                        Location = new Point(10, 45),
                        Size = new Size(150, 25),
                        Font = new Font("Segoe UI", 9)
                    };
                    numDailyTarget = new NumericUpDown  // Assign to outer variable
                    {
                        Location = new Point(170, 45),
                        Size = new Size(100, 25),
                        Minimum = 0,
                        Maximum = 100000,
                        DecimalPlaces = 2,
                        Value = agreement.DailyMinimumTarget ?? 0,
                        Font = new Font("Segoe UI", 9)
                    };

                    pnlCommission.Controls.AddRange(new Control[] {
                lblCommissionRate, numCommissionRate, lblDailyTarget, numDailyTarget
            });
                    dialog.Controls.Add(pnlCommission);
                    yPos += 110;
                }

                // Active Status
                var chkActive = new CheckBox
                {
                    Text = "✅ Active Agreement",
                    Location = new Point(20, yPos),
                    Size = new Size(200, 25),
                    Checked = agreement.IsActive,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };
                yPos += 40;

                // Buttons
                var btnSave = new Button
                {
                    Text = "💾 Update Agreement",
                    Location = new Point(190, yPos),
                    Size = new Size(150, 35),
                    BackColor = Color.LightBlue,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                var btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new Point(350, yPos),
                    Size = new Size(100, 35),
                    DialogResult = DialogResult.Cancel,
                    Font = new Font("Segoe UI", 9)
                };

                btnSave.Click += (s, args) =>
                {
                    // Validation based on tenant type
                    if (tenant?.Type != TenantType.OnCommission && numRent.Value <= 0)
                    {
                        MessageBox.Show("Monthly rent is required for rent tenants.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    agreement.MonthlyRent = numRent.Value;
                    agreement.StartDate = dtpStartDate.Value;
                    agreement.LastIncreaseDate = dtpLastIncreaseDate.Value;
                    agreement.IncreaseMode = cmbIncreaseMode.SelectedItem.ToString() == "Auto" ?
                        RentIncreaseMode.Auto : RentIncreaseMode.Manual;
                    agreement.IsActive = chkActive.Checked;

                    // Update commission fields if commission tenant
                    if (tenant?.Type == TenantType.OnCommission)
                    {
                        // Now we can access numCommissionRate and numDailyTarget
                        agreement.CommissionRate = numCommissionRate?.Value ?? 0;
                        agreement.DailyMinimumTarget = numDailyTarget?.Value > 0 ? numDailyTarget.Value : (decimal?)null;
                    }

                    _dataService.SaveAgreements(agreements);
                    LoadAgreements();
                    dialog.DialogResult = DialogResult.OK;
                };

                btnCancel.Click += (s, args) => dialog.DialogResult = DialogResult.Cancel;

                dialog.Controls.AddRange(new Control[] {
            lblRent, numRent, lblStartDate, dtpStartDate, lblLastIncreaseDate, dtpLastIncreaseDate,
            lblIncreaseMode, cmbIncreaseMode, chkActive, btnSave, btnCancel
        });

                // Set focus to rent field
                dialog.Shown += (s, args) => numRent.Focus();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("✅ Rent agreement updated successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
       */
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAgreements.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an agreement to delete.", "Select Agreement",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvAgreements.SelectedRows[0];
            var agreementId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
            var propertyName = selectedRow.Cells["PropertyName"].Value.ToString();
            var tenantName = selectedRow.Cells["TenantName"].Value.ToString();
            var tenantType = selectedRow.Cells["TenantType"].Value.ToString();

            string message = tenantType == "OnCommission"
                ? $"Are you sure you want to delete commission agreement for '{tenantName}' at '{propertyName}'?"
                : $"Are you sure you want to delete rent agreement for '{tenantName}' at '{propertyName}'?";

            var result = MessageBox.Show(message,
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                var agreements = _dataService.LoadAgreements();
                agreements.RemoveAll(a => a.Id == agreementId);
                _dataService.SaveAgreements(agreements);
                LoadAgreements();

                MessageBox.Show("✅ Rent agreement deleted successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}