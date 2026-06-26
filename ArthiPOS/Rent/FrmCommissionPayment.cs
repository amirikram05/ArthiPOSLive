using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;

namespace ShopRentManagementSystem
{
    public partial class FrmCommissionPayment : Form
    {
        private readonly JsonDataService _dataService;
        private readonly int _agreementId;
        private readonly RentAgreement _agreement;
        private readonly Tenant _tenant;
        private readonly Property _property;
        private readonly Portion _portion;

        private DataGridView dgvTransactions;
        private ComboBox cmbProduct;
        private NumericUpDown numQuantity;
        private NumericUpDown numUnitPrice;
        private NumericUpDown numLaborAmount;
        private NumericUpDown numProductTotal;
        private NumericUpDown numCommissionAmount;
        private DateTimePicker dtpPaymentDate;
        private TextBox txtNotes;
        private Label lblCommissionRate;
        private Label lblTotalSummary;
        private Button btnAddTransaction;
        private Button btnRemoveTransaction;
        private Button btnSave;
        private Button btnCancel;

        private List<CommissionTransaction> transactions = new List<CommissionTransaction>();

        public FrmCommissionPayment(int agreementId)
        {
            _agreementId = agreementId;
            _dataService = new JsonDataService();

            // Load data
            var agreements = _dataService.LoadAgreements();
            var tenants = _dataService.LoadTenants();
            var properties = _dataService.LoadProperties();
            var portions = _dataService.LoadPortions();

            _agreement = agreements.FirstOrDefault(a => a.Id == agreementId);
            _tenant = _agreement != null ? tenants.FirstOrDefault(t => t.Id == _agreement.TenantId) : null;
            _property = _agreement != null ? properties.FirstOrDefault(p => p.Id == _agreement.PropertyId) : null;
            _portion = _agreement != null ? portions.FirstOrDefault(p => p.Id == _agreement.PortionId) : null;

            if (_agreement == null || _tenant == null || _property == null || _portion == null)
            {
                MessageBox.Show("Invalid agreement data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            InitializeComponent();
            LoadProducts();
            UpdateTotals();
        }

        private void InitializeComponent()
        {
            this.Text = $"Commission Payment - {(_tenant?.Name ?? "Unknown")}";
            this.Size = new Size(1100, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = SystemColors.Control;

            // Main Container
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15)
            };

            int yPos = 10;

            // Header
            Label lblHeader = new Label
            {
                Text = $"💼 COMMISSION PAYMENT DETAILS",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(10, yPos),
                Size = new Size(1050, 35)
            };
            yPos += 30;

            // Tenant and Property Info
            Label lblInfo = new Label
            {
                Text = $"Tenant: {_tenant.Name} | Property: {_property.Name} - {_portion.Name}",
                Location = new Point(10, yPos),
                Size = new Size(1050, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            yPos += 20;

            // Commission Rate
            lblCommissionRate = new Label
            {
                Text = $"Commission Rate: {(_agreement.CommissionRate ?? 0):F1}%",
                Location = new Point(10, yPos),
                Size = new Size(1050, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkGreen
            };
            yPos += 25;

            // Product Selection Panel
            GroupBox gbProductEntry = new GroupBox
            {
                Text = "➕ Add Product Transaction",
                Location = new Point(10, yPos),
                Size = new Size(1050, 140),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            SetupProductEntryPanel(gbProductEntry);
            yPos += 140;

            // Transactions Grid
            GroupBox gbTransactions = new GroupBox
            {
                Text = "📋 Product Transactions",
                Location = new Point(10, yPos),
                Size = new Size(1050, 250),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            SetupTransactionsGrid(gbTransactions);
            yPos += 250;

            // Total Summary
            lblTotalSummary = new Label
            {
                Name = "lblTotalSummary",
                Location = new Point(10, yPos),
                Size = new Size(1050, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                TextAlign = ContentAlignment.MiddleLeft
            };
            yPos += 35;

            // Payment Details Panel
            GroupBox gbPaymentDetails = new GroupBox
            {
                Text = "💰 Payment Details",
                Location = new Point(10, yPos),
                Size = new Size(1050, 120),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            SetupPaymentDetailsPanel(gbPaymentDetails);
            yPos += 125;

            // Buttons Panel
            Panel pnlButtons = new Panel
            {
                Location = new Point(10, yPos),
                Size = new Size(1050, 60),
                BackColor = Color.LightGray
            };

            SetupButtonsPanel(pnlButtons);

            mainPanel.Controls.AddRange(new Control[] {
                lblHeader, lblInfo, lblCommissionRate,
                gbProductEntry, gbTransactions,
                lblTotalSummary, gbPaymentDetails, pnlButtons
            });

            this.Controls.Add(mainPanel);
        }

        private void SetupProductEntryPanel(GroupBox groupBox)
        {
            int yPos = 25;
            int xPos = 15;
            int labelWidth = 100;
            int controlWidth = 180;

            // Product Selection
            Label lblProduct = new Label
            {
                Text = "Product:",
                Location = new Point(xPos, yPos),
                Size = new Size(labelWidth, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };

            cmbProduct = new ComboBox
            {
                Location = new Point(xPos + labelWidth + 10, yPos),
                Size = new Size(controlWidth, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;

            // Quantity
            Label lblQuantity = new Label
            {
                Text = "Quantity:",
                Location = new Point(xPos + labelWidth + controlWidth + 30, yPos),
                Size = new Size(70, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };

            numQuantity = new NumericUpDown
            {
                Location = new Point(xPos + labelWidth + controlWidth + 110, yPos),
                Size = new Size(100, 25),
                Minimum = 0.01m,
                Maximum = 100000,
                DecimalPlaces = 2,
                Value = 1,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            numQuantity.ValueChanged += CalculateProductTotal;

            // Unit (display only)
            Label lblUnit = new Label
            {
                Name = "lblUnit",
                Text = "kg",
                Location = new Point(xPos + labelWidth + controlWidth + 220, yPos),
                Size = new Size(50, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Gray
            };
            yPos += 35;

            // Unit Price
            Label lblUnitPrice = new Label
            {
                Text = "Unit Price:",
                Location = new Point(xPos, yPos),
                Size = new Size(labelWidth, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };

            numUnitPrice = new NumericUpDown
            {
                Location = new Point(xPos + labelWidth + 10, yPos),
                Size = new Size(controlWidth, 25),
                Minimum = 0,
                Maximum = 100000,
                DecimalPlaces = 2,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            numUnitPrice.ValueChanged += CalculateProductTotal;

            // Labor Amount
            Label lblLabor = new Label
            {
                Text = "Labor Amount:",
                Location = new Point(xPos + labelWidth + controlWidth + 30, yPos),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };

            numLaborAmount = new NumericUpDown
            {
                Location = new Point(xPos + labelWidth + controlWidth + 140, yPos),
                Size = new Size(100, 25),
                Minimum = 0,
                Maximum = 100000,
                DecimalPlaces = 2,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            numLaborAmount.ValueChanged += CalculateProductTotal;

            // Product Total (read-only)
            Label lblProductTotal = new Label
            {
                Text = "Product Total:",
                Location = new Point(xPos + labelWidth + controlWidth + 250, yPos),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };

            numProductTotal = new NumericUpDown
            {
                Location = new Point(xPos + labelWidth + controlWidth + 360, yPos),
                Size = new Size(120, 25),
                Minimum = 0,
                Maximum = 1000000,
                DecimalPlaces = 2,
                ReadOnly = true,
                BackColor = Color.LightGray,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            yPos += 35;

            // Commission Amount (read-only)
            Label lblCommission = new Label
            {
                Text = "Commission Amount:",
                Location = new Point(xPos, yPos),
                Size = new Size(labelWidth + 40, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            numCommissionAmount = new NumericUpDown
            {
                Location = new Point(xPos + labelWidth + 50, yPos),
                Size = new Size(controlWidth, 25),
                Minimum = 0,
                Maximum = 100000,
                DecimalPlaces = 2,
                ReadOnly = true,
                BackColor = Color.LightYellow,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            // Add Transaction Button
            btnAddTransaction = new Button
            {
                Text = "➕ Add to List",
                Location = new Point(xPos + labelWidth + controlWidth + 250, yPos - 5),
                Size = new Size(120, 35),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnAddTransaction.Click += BtnAddTransaction_Click;

            groupBox.Controls.AddRange(new Control[] {
                lblProduct, cmbProduct, lblQuantity, numQuantity, lblUnit,
                lblUnitPrice, numUnitPrice, lblLabor, numLaborAmount,
                lblProductTotal, numProductTotal,
                lblCommission, numCommissionAmount, btnAddTransaction
            });
        }

        private void SetupTransactionsGrid(GroupBox groupBox)
        {
            dgvTransactions = new DataGridView
            {
                Location = new Point(15, 25),
                Size = new Size(1020, 190),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                BackgroundColor = SystemColors.Window
            };

            SetupTransactionsGridColumns();

            // Remove Transaction Button
            btnRemoveTransaction = new Button
            {
                Text = "➖ Remove Selected",
                Location = new Point(15, 220),
                Size = new Size(140, 25),
                BackColor = Color.LightCoral,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            btnRemoveTransaction.Click += BtnRemoveTransaction_Click;

            // Clear All Button
            Button btnClearAll = new Button
            {
                Text = "🗑️ Clear All",
                Location = new Point(165, 220),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            btnClearAll.Click += (s, e) =>
            {
                if (transactions.Any())
                {
                    var result = MessageBox.Show("Clear all transactions?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        transactions.Clear();
                        UpdateTransactionsGrid();
                        UpdateTotals();
                    }
                }
            };

            groupBox.Controls.AddRange(new Control[] { dgvTransactions, btnRemoveTransaction, btnClearAll });
        }

        private void SetupTransactionsGridColumns()
        {
            dgvTransactions.Columns.Clear();

            var columns = new[]
            {
                new DataGridViewTextBoxColumn { Name = "Product", HeaderText = "Product", Width = 180 },
                new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Qty", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "Unit", HeaderText = "Unit", Width = 60 },
                new DataGridViewTextBoxColumn { Name = "UnitPrice", HeaderText = "Unit Price", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "ProductTotal", HeaderText = "Product Total", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "Labor", HeaderText = "Labor", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "SubTotal", HeaderText = "Sub Total", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "CommissionRate", HeaderText = "Comm %", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "CommissionAmount", HeaderText = "Commission Amt", Width = 120 }
            };

            dgvTransactions.Columns.AddRange(columns);

            // Format columns
            dgvTransactions.Columns["UnitPrice"].DefaultCellStyle.Format = "C";
            dgvTransactions.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTransactions.Columns["ProductTotal"].DefaultCellStyle.Format = "C";
            dgvTransactions.Columns["ProductTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTransactions.Columns["Labor"].DefaultCellStyle.Format = "C";
            dgvTransactions.Columns["Labor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTransactions.Columns["SubTotal"].DefaultCellStyle.Format = "C";
            dgvTransactions.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTransactions.Columns["CommissionAmount"].DefaultCellStyle.Format = "C";
            dgvTransactions.Columns["CommissionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Style headers
            dgvTransactions.EnableHeadersVisualStyles = false;
            dgvTransactions.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvTransactions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTransactions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        }

        private void SetupPaymentDetailsPanel(GroupBox groupBox)
        {
            int yPos = 25;
            int xPos = 15;

            // Payment Date
            Label lblDate = new Label
            {
                Text = "Payment Date:",
                Location = new Point(xPos, yPos),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };

            dtpPaymentDate = new DateTimePicker
            {
                Location = new Point(xPos + 110, yPos),
                Size = new Size(150, 25),
                Value = DateTime.Now,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            yPos += 35;

            // Notes
            Label lblNotes = new Label
            {
                Text = "Notes:",
                Location = new Point(xPos, yPos),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };

            txtNotes = new TextBox
            {
                Location = new Point(xPos + 110, yPos),
                Size = new Size(400, 60),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };

            groupBox.Controls.AddRange(new Control[] { lblDate, dtpPaymentDate, lblNotes, txtNotes });
        }

        private void SetupButtonsPanel(Panel panel)
        {
            btnSave = new Button
            {
                Text = "💾 SAVE COMMISSION PAYMENT",
                Location = new Point(350, 15),
                Size = new Size(220, 40),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(580, 15),
                Size = new Size(100, 40),
                DialogResult = DialogResult.Cancel,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };

            panel.Controls.AddRange(new Control[] { btnSave, btnCancel });
        }

        private void LoadProducts()
        {
            var products = _dataService.GetProductsForAgreement(_agreementId);
            cmbProduct.Items.Clear();

            foreach (var product in products)
            {
                cmbProduct.Items.Add(new
                {
                    Id = product.Id,
                    Text = $"{product.Name} ({product.Unit})",
                    Product = product
                });
            }

            if (cmbProduct.Items.Count > 0)
            {
                cmbProduct.SelectedIndex = 0;
                UpdateUnitLabel();
            }
            else
            {
                MessageBox.Show("No products available for this agreement. Please add products first.",
                    "No Products", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem != null)
            {
                UpdateUnitLabel();
                var product = GetSelectedProduct();
                if (product != null)
                {
                    numUnitPrice.Value = product.UnitPrice;
                    CalculateProductTotal(sender, e);
                }
            }
        }

        private Product GetSelectedProduct()
        {
            if (cmbProduct.SelectedItem != null)
            {
                dynamic selected = cmbProduct.SelectedItem;
                return selected.Product as Product;
            }
            return null;
        }

        private void UpdateUnitLabel()
        {
            var product = GetSelectedProduct();
            if (product != null)
            {
                var lblUnit = this.Controls.Find("lblUnit", true).FirstOrDefault() as Label;
                if (lblUnit != null)
                {
                    lblUnit.Text = product.Unit;
                }
            }
        }

        private void CalculateProductTotal(object sender, EventArgs e)
        {
            decimal quantity = numQuantity.Value;
            decimal unitPrice = numUnitPrice.Value;
            decimal labor = numLaborAmount.Value;

            decimal productTotal = quantity * unitPrice;
            decimal subTotal = productTotal - labor;
            decimal commissionRate = _agreement.CommissionRate ?? 0;
            decimal commissionAmount = (subTotal * commissionRate) / 100;

            numProductTotal.Value = productTotal;
            numCommissionAmount.Value = commissionAmount;
        }

        private void BtnAddTransaction_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem == null)
            {
                MessageBox.Show("Please select a product.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numQuantity.Value <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numQuantity.Focus();
                return;
            }

            var product = GetSelectedProduct();
            if (product == null) return;

            decimal quantity = numQuantity.Value;
            decimal unitPrice = numUnitPrice.Value;
            decimal labor = numLaborAmount.Value;
            decimal productTotal = quantity * unitPrice;
            decimal subTotal = productTotal - labor;
            decimal commissionRate = _agreement.CommissionRate ?? 0;
            decimal commissionAmount = (subTotal * commissionRate) / 100;

            var transaction = new CommissionTransaction
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = quantity,
                Unit = product.Unit,
                UnitPrice = unitPrice,
                LaborAmount = labor,
                ProductTotal = subTotal,
                CommissionRate = commissionRate,
                CommissionAmount = commissionAmount,
                TransactionDate = DateTime.Now
            };

            transactions.Add(transaction);
            UpdateTransactionsGrid();
            UpdateTotals();

            // Clear input fields for next entry
            numQuantity.Value = 1;
            numLaborAmount.Value = 0;
            CalculateProductTotal(sender, e);
        }

        private void BtnRemoveTransaction_Click(object sender, EventArgs e)
        {
            if (dgvTransactions.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvTransactions.SelectedRows[0].Index;
                if (selectedIndex < transactions.Count)
                {
                    transactions.RemoveAt(selectedIndex);
                    UpdateTransactionsGrid();
                    UpdateTotals();
                }
            }
            else
            {
                MessageBox.Show("Please select a transaction to remove.", "Select Transaction",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateTransactionsGrid()
        {
            dgvTransactions.Rows.Clear();

            foreach (var transaction in transactions)
            {
                dgvTransactions.Rows.Add(
                    transaction.ProductName,
                    transaction.Quantity,
                    transaction.Unit,
                    transaction.UnitPrice,
                    transaction.ProductTotal,
                    transaction.LaborAmount,
                    transaction.ProductTotal+ transaction.LaborAmount,
                    $"{transaction.CommissionRate:F1}%",
                    transaction.CommissionAmount
                );
            }

            // Color alternating rows
            for (int i = 0; i < dgvTransactions.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    dgvTransactions.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                }
            }
        }

        private void UpdateTotals()
        {
            if (!transactions.Any())
            {
                lblTotalSummary.Text = "No transactions added yet.";
                return;
            }

            decimal totalQuantity = transactions.Sum(t => t.Quantity);
            decimal totalProductValue = transactions.Sum(t => t.ProductTotal);
            decimal totalLabor = transactions.Sum(t => t.LaborAmount);
            decimal totalSales = totalProductValue ;
            decimal totalCommission = transactions.Sum(t => t.CommissionAmount);
            decimal commissionRate = _agreement.CommissionRate ?? 0;

            lblTotalSummary.Text = $"📊 SUMMARY: {transactions.Count} Transactions | " +
                $"Total Qty: {totalQuantity:F2} | " +
                $"Product Value: {totalProductValue:C} | " +
                $"Labor: {totalLabor:C} | " +
                $"Total Sales: {totalSales:C} | " +
                $"Commission ({commissionRate:F1}%): {totalCommission:C}";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!transactions.Any())
            {
                MessageBox.Show("Please add at least one product transaction.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpPaymentDate.Value > DateTime.Now.AddDays(1))
            {
                MessageBox.Show("Payment date cannot be in the future.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpPaymentDate.Focus();
                return;
            }

            try
            {
                var payments = _dataService.LoadPayments();
                var newPaymentId = payments.Count > 0 ? payments.Max(p => p.Id) + 1 : 1;

                decimal totalCommission = transactions.Sum(t => t.CommissionAmount);
                decimal totalSales = transactions.Sum(t => t.ProductTotal);
                decimal totalQuantity = transactions.Sum(t => t.Quantity);

                // Create main payment record
                var payment = new Payment
                {
                    Id = newPaymentId,
                    AgreementId = _agreementId,
                    Amount = totalSales,
                    PaymentDate = dtpPaymentDate.Value,
                    MonthYear = $"Commission-{dtpPaymentDate.Value:MM-yyyy}",
                    Notes = $"Total Sales: {totalSales:C}\nTotal Products: {transactions.Count}\n" + txtNotes.Text,
                    PaymentType = PaymentType.Commission,
                    SalesAmount = totalSales,
                    CommissionEarned = totalCommission,
                    ProductId = transactions.First().ProductId,
                    ProductName = $"{transactions.Count} products",
                    Quantity = totalQuantity,
                    Unit = "Various",
                    UnitPrice = transactions.Average(t => t.UnitPrice),
                    LaborAmount = transactions.Sum(t => t.LaborAmount),
                    ProductTotal = transactions.Sum(t => t.ProductTotal)
                };

                payments.Add(payment);
                _dataService.SavePayments(payments);

                // Save commission transactions
                var allTransactions = _dataService.LoadCommissionTransactions();
                foreach (var transaction in transactions)
                {
                    transaction.Id = allTransactions.Count > 0 ? allTransactions.Max(t => t.Id) + 1 : 1;
                    transaction.PaymentId = newPaymentId;
                    allTransactions.Add(transaction);
                }
                _dataService.SaveCommissionTransactions(allTransactions);

                // Update last commission payment date
                var agreements = _dataService.LoadAgreements();
                var agreement = agreements.FirstOrDefault(a => a.Id == _agreementId);
                if (agreement != null)
                {
                    agreement.LastCommissionPaymentDate = dtpPaymentDate.Value;
                    _dataService.SaveAgreements(agreements);
                }

                MessageBox.Show($"✅ Commission payment saved successfully!\n\n" +
                    $"Total Commission: {totalCommission:C}\n" +
                    $"Total Sales: {totalSales:C}\n" +
                    $"Number of Transactions: {transactions.Count}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error saving commission payment:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}