using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;

namespace ShopRentManagementSystem
{
    public partial class FrmCollectPayment : Form
    {
        private readonly JsonDataService _dataService;
        private readonly int _agreementId;
        private readonly int _tenantId;
        private readonly int _propertyId;
        private readonly int _portionId;
        private readonly decimal _dueAmount;

        private Label lblProperty;
        private Label lblTenant;
        private Label lblPortion;
        private Label lblDueAmount;
        private Label lblTenantType;
        private NumericUpDown nudPaidAmount;
        private DateTimePicker dtpPaymentDate;
        private TextBox txtNotes;
        private Button btnSave;
        private Button btnCancel;

        public FrmCollectPayment(int agreementId, int tenantId, int propertyId, int portionId, decimal dueAmount)
        {
            _agreementId = agreementId;
            _tenantId = tenantId;
            _propertyId = propertyId;
            _portionId = portionId;
            _dueAmount = dueAmount;
            _dataService = new JsonDataService();

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Collect Payment";
            this.Size = new Size(400, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int yPos = 20;
            int labelWidth = 120;
            int controlWidth = 200;

            // Property
            lblProperty = CreateLabel("Property:", yPos);
            this.Controls.Add(lblProperty);
            yPos += 30;

            // Tenant
            lblTenant = CreateLabel("Tenant:", yPos);
            this.Controls.Add(lblTenant);
            yPos += 30;

            // Portion
            lblPortion = CreateLabel("Portion:", yPos);
            this.Controls.Add(lblPortion);
            yPos += 30;

            // Tenant Type
            lblTenantType = CreateLabel("Tenant Type:", yPos);
            this.Controls.Add(lblTenantType);
            yPos += 30;

            // Due Amount
            lblDueAmount = CreateLabel("Due Amount:", yPos);
            lblDueAmount.ForeColor = Color.Red;
            if (_dueAmount > 0)
                lblDueAmount.Font = new Font(lblDueAmount.Font, FontStyle.Bold);
            this.Controls.Add(lblDueAmount);
            yPos += 30;

            // Paid Amount
            var lblPaidAmount = new Label
            {
                Text = "Paid Amount:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25)
            };
            this.Controls.Add(lblPaidAmount);

            nudPaidAmount = new NumericUpDown
            {
                Location = new Point(150, yPos),
                Size = new Size(controlWidth, 25),
                Minimum = 0,
                Maximum = 1000000,
                DecimalPlaces = 2
            };
            nudPaidAmount.Value = _dueAmount > 0 ? _dueAmount : 0;
            this.Controls.Add(nudPaidAmount);
            yPos += 30;

            // Payment Date
            var lblPaymentDate = new Label
            {
                Text = "Payment Date:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25)
            };
            this.Controls.Add(lblPaymentDate);

            dtpPaymentDate = new DateTimePicker
            {
                Location = new Point(150, yPos),
                Size = new Size(controlWidth, 25),
                Value = DateTime.Now
            };
            this.Controls.Add(dtpPaymentDate);
            yPos += 30;

            // Notes
            var lblNotes = new Label
            {
                Text = "Notes:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25)
            };
            this.Controls.Add(lblNotes);

            txtNotes = new TextBox
            {
                Location = new Point(150, yPos),
                Size = new Size(controlWidth, 60),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            this.Controls.Add(txtNotes);
            yPos += 70;

            // Warning label
            var lblWarning = new Label
            {
                Text = "Verify amount before saving. Use 'Payment History' to delete mistakes.",
                ForeColor = Color.DarkOrange,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                Location = new Point(20, yPos),
                Size = new Size(350, 40),
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(lblWarning);
            yPos += 40;

            // Buttons
            btnSave = new Button
            {
                Text = "Save Payment",
                Location = new Point(100, yPos),
                Size = new Size(120, 35),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(230, yPos),
                Size = new Size(100, 35)
            };
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { btnSave, btnCancel });
        }

        private Label CreateLabel(string text, int yPos)
        {
            return new Label
            {
                Text = text,
                Location = new Point(20, yPos),
                Size = new Size(350, 25),
                Font = new Font(Font.FontFamily, 9, FontStyle.Regular)
            };
        }

        private void LoadData()
        {
            var properties = _dataService.LoadProperties();
            var portions = _dataService.LoadPortions();
            var tenants = _dataService.LoadTenants();
            var agreements = _dataService.LoadAgreements();

            var property = properties.FirstOrDefault(p => p.Id == _propertyId);
            var portion = portions.FirstOrDefault(p => p.Id == _portionId);
            var tenant = tenants.FirstOrDefault(t => t.Id == _tenantId);
            var agreement = agreements.FirstOrDefault(a => a.Id == _agreementId);

            if (property != null)
                lblProperty.Text = $"Property: {property.Name}";

            if (tenant != null)
            {
                lblTenant.Text = $"Tenant: {tenant.Name}";
                lblTenantType.Text = $"Tenant Type: {tenant.Type}";
            }

            if (portion != null)
                lblPortion.Text = $"Portion: {portion.Name} ({portion.Size})";

            string dueText = _dueAmount > 0 ? $"{_dueAmount:C}" : "None";
            lblDueAmount.Text = $"Due Amount: {dueText}";

            // Adjust form for commission tenants
            if (tenant?.Type == TenantType.OnCommission)
            {
                this.Text = "Record Commission Payment";
                lblDueAmount.Text = $"Commission Due: {dueText}";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (nudPaidAmount.Value <= 0)
            {
                MessageBox.Show("Please enter a valid payment amount.", "Invalid Amount",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var payments = _dataService.LoadPayments();
                var agreements = _dataService.LoadAgreements();
                var tenants = _dataService.LoadTenants();

                var agreement = agreements.FirstOrDefault(a => a.Id == _agreementId);
                var tenant = tenants.FirstOrDefault(t => t.Id == _tenantId);

                // Create new payment
                var payment = new Payment
                {
                    Id = payments.Count > 0 ? payments.Max(p => p.Id) + 1 : 1,
                    AgreementId = _agreementId,
                    Amount = nudPaidAmount.Value,
                    PaymentDate = dtpPaymentDate.Value,
                    MonthYear = dtpPaymentDate.Value.ToString("MM-yyyy"),
                    Notes = txtNotes.Text,
                    PaymentType = tenant?.Type == TenantType.OnCommission ? PaymentType.Commission : PaymentType.Rent
                };

                payments.Add(payment);
                _dataService.SavePayments(payments);

                // Check if rent increase is needed (auto mode for rent tenants)
                if (agreement != null && tenant?.Type == TenantType.OnRent && agreement.IncreaseMode == RentIncreaseMode.Auto)
                {
                    if (dtpPaymentDate.Value > agreement.LastIncreaseDate.AddYears(1))
                    {
                        agreement.MonthlyRent *= 1.10m; // 10% increase
                        agreement.LastIncreaseDate = dtpPaymentDate.Value;
                        _dataService.SaveAgreements(agreements);

                        MessageBox.Show($"Rent automatically increased by 10%.\nNew rent: {agreement.MonthlyRent:C}",
                            "Rent Increased", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Update last commission payment date for commission tenants
                if (agreement != null && tenant?.Type == TenantType.OnCommission)
                {
                    agreement.LastCommissionPaymentDate = dtpPaymentDate.Value;
                    _dataService.SaveAgreements(agreements);
                }

                MessageBox.Show("Payment saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving payment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}