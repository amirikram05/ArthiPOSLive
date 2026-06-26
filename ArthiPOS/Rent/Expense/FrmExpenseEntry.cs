using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;

namespace ShopRentManagementSystem
{
    public partial class FrmExpenseEntry : Form
    {
        private readonly JsonDataService _dataService;
        private int? _expenseId;
        private Expense _expense;

        // Controls
        private ComboBox cmbCategory;
        private DateTimePicker dtpDate;
        private TextBox txtPayee;
        private TextBox txtDescription;
        private NumericUpDown numAmount;
        private ComboBox cmbPaymentMethod;
        private TextBox txtReference;
        private CheckBox chkTaxDeductible;
        private ComboBox cmbProperty;
        private TextBox txtNotes;
        private Label lblExpenseNumber;
        private Button btnSave;
        private Button btnCancel;
        private TextBox _txtExpenseNumber;

        // Helper class for property items
        private class PropertyItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        public FrmExpenseEntry(int? expenseId = null)
        {
            _dataService = new JsonDataService();
            _expenseId = expenseId;
            InitializeComponent();
            LoadCategories();
            LoadPaymentMethods();
            LoadProperties();

            if (_expenseId.HasValue)
            {
                LoadExpense();
                this.Text = "Edit Expense";
            }
            else
            {
                GenerateExpenseNumber();
                this.Text = "Add New Expense";
            }
        }

        private void InitializeComponent()
        {
            this.Size = new Size(600, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = SystemColors.Control;

            // Header Panel
            Panel pnlHeader = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.SteelBlue
            };

            Label lblTitle = new Label
            {
                Text = _expenseId.HasValue ? "✏️ Edit Expense" : "➕ Add New Expense",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                Size = new Size(400, 35),
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblExpenseNumber = new Label
            {
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.LightYellow,
                Location = new Point(400, 20),
                Size = new Size(150, 25),
                TextAlign = ContentAlignment.MiddleRight
            };

            pnlHeader.Controls.AddRange(new Control[] { lblTitle, lblExpenseNumber });

            // Main Panel
            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true
            };

            int yPos = 20;
            int labelWidth = 120;
            int controlWidth = 350;

            // Expense Number (read-only)
            Label lblExpNoLabel = new Label
            {
                Text = "Expense #:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            _txtExpenseNumber = new TextBox
            {
                Location = new Point(150, yPos),
                Size = new Size(controlWidth, 25),
                ReadOnly = true,
                BackColor = Color.LightGray,
                Name = "txtExpenseNumber"
            };
            pnlMain.Controls.AddRange(new Control[] { lblExpNoLabel, _txtExpenseNumber });
            yPos += 35;

            // Date
            Label lblDate = new Label
            {
                Text = "Expense Date:*",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25),
                Font = new Font("Segoe UI", 9)
            };
            dtpDate = new DateTimePicker
            {
                Location = new Point(150, yPos),
                Size = new Size(controlWidth, 25),
                Value = DateTime.Now,
                Format = DateTimePickerFormat.Short,
                Name = "dtpDate"
            };
            pnlMain.Controls.AddRange(new Control[] { lblDate, dtpDate });
            yPos += 35;

            // Category
            Label lblCategory = new Label
            {
                Text = "Category:*",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25),
                Font = new Font("Segoe UI", 9)
            };
            cmbCategory = new ComboBox
            {
                Location = new Point(150, yPos),
                Size = new Size(controlWidth, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "cmbCategory"
            };
            pnlMain.Controls.AddRange(new Control[] { lblCategory, cmbCategory });
            yPos += 35;

            // Payee/Vendor
            Label lblPayee = new Label
            {
                Text = "Payee/Vendor:*",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25),
                Font = new Font("Segoe UI", 9)
            };
            txtPayee = new TextBox
            {
                Location = new Point(150, yPos),
                Size = new Size(controlWidth, 25),
                Font = new Font("Segoe UI", 9),
                Name = "txtPayee"
            };
            pnlMain.Controls.AddRange(new Control[] { lblPayee, txtPayee });
            yPos += 35;

            // Description
            Label lblDescription = new Label
            {
                Text = "Description:*",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25),
                Font = new Font("Segoe UI", 9)
            };
            txtDescription = new TextBox
            {
                Location = new Point(150, yPos),
                Size = new Size(controlWidth, 60),
                Multiline = true,
                Font = new Font("Segoe UI", 9),
                Name = "txtDescription"
            };
            pnlMain.Controls.AddRange(new Control[] { lblDescription, txtDescription });
            yPos += 65;

            // Amount
            Label lblAmount = new Label
            {
                Text = "Amount:*",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25),
                Font = new Font("Segoe UI", 9)
            };
            numAmount = new NumericUpDown
            {
                Location = new Point(150, yPos),
                Size = new Size(200, 25),
                Minimum = 0.01m,
                Maximum = 1000000,
                DecimalPlaces = 2,
                Font = new Font("Segoe UI", 9),
                Name = "numAmount"
            };
            pnlMain.Controls.AddRange(new Control[] { lblAmount, numAmount });
            yPos += 35;

            // Payment Method
            Label lblPaymentMethod = new Label
            {
                Text = "Payment Method:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25),
                Font = new Font("Segoe UI", 9)
            };
            cmbPaymentMethod = new ComboBox
            {
                Location = new Point(150, yPos),
                Size = new Size(controlWidth, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "cmbPaymentMethod"
            };
            pnlMain.Controls.AddRange(new Control[] { lblPaymentMethod, cmbPaymentMethod });
            yPos += 35;

            // Reference Number
            Label lblReference = new Label
            {
                Text = "Reference #:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25),
                Font = new Font("Segoe UI", 9)
            };
            txtReference = new TextBox
            {
                Location = new Point(150, yPos),
                Size = new Size(controlWidth, 25),
                Font = new Font("Segoe UI", 9),
                Name = "txtReference"
            };
            pnlMain.Controls.AddRange(new Control[] { lblReference, txtReference });
            yPos += 35;

            // Property
            Label lblProperty = new Label
            {
                Text = "Property:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25),
                Font = new Font("Segoe UI", 9)
            };
            cmbProperty = new ComboBox
            {
                Location = new Point(150, yPos),
                Size = new Size(controlWidth, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "cmbProperty"
            };
            pnlMain.Controls.AddRange(new Control[] { lblProperty, cmbProperty });
            yPos += 35;

            // Tax Deductible
            chkTaxDeductible = new CheckBox
            {
                Text = "This expense is tax deductible",
                Location = new Point(150, yPos),
                Size = new Size(300, 25),
                Checked = true,
                Font = new Font("Segoe UI", 9),
                Name = "chkTaxDeductible"
            };
            pnlMain.Controls.Add(chkTaxDeductible);
            yPos += 30;

            // Notes
            Label lblNotes = new Label
            {
                Text = "Notes:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 25),
                Font = new Font("Segoe UI", 9)
            };
            txtNotes = new TextBox
            {
                Location = new Point(150, yPos),
                Size = new Size(controlWidth, 60),
                Multiline = true,
                Font = new Font("Segoe UI", 9),
                Name = "txtNotes"
            };
            pnlMain.Controls.AddRange(new Control[] { lblNotes, txtNotes });
            yPos += 65;

            // Buttons Panel
            Panel pnlButtons = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.LightGray,
                Padding = new Padding(10)
            };

            btnSave = new Button
            {
                Text = "💾 Save Expense",
                Location = new Point(150, 10),
                Size = new Size(150, 35),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Name = "btnSave"
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(310, 10),
                Size = new Size(100, 35),
                DialogResult = DialogResult.Cancel,
                Name = "btnCancel"
            };

            pnlButtons.Controls.AddRange(new Control[] { btnSave, btnCancel });

            // Add panels to form
            this.Controls.AddRange(new Control[] { pnlMain, pnlButtons, pnlHeader });
        }

        private void LoadCategories()
        {
            cmbCategory.Items.Clear();
            foreach (var category in Enum.GetValues(typeof(ExpenseCategory)))
            {
                cmbCategory.Items.Add(GetCategoryDisplayName((ExpenseCategory)category));
            }
            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;
        }

        private string GetCategoryDisplayName(ExpenseCategory category)
        {
            return category switch
            {
                ExpenseCategory.Utilities => "Utilities (Electricity, Water, Gas)",
                ExpenseCategory.Maintenance => "Maintenance & Repairs",
                ExpenseCategory.Insurance => "Insurance Premiums",
                ExpenseCategory.Taxes => "Property Taxes",
                ExpenseCategory.Cleaning => "Cleaning Services",
                ExpenseCategory.Security => "Security Services",
                ExpenseCategory.Marketing => "Marketing & Advertising",
                ExpenseCategory.ProfessionalFees => "Professional Fees",
                ExpenseCategory.Supplies => "Office Supplies",
                ExpenseCategory.Salaries => "Salaries & Wages",
                ExpenseCategory.Equipment => "Equipment",
                ExpenseCategory.Miscellaneous => "Miscellaneous",
                _ => category.ToString()
            };
        }

        private void LoadPaymentMethods()
        {
            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.Items.AddRange(new[] {
                "Cash",
                "Check",
                "Bank Transfer",
                "Credit Card",
                "Debit Card",
                "Online Payment",
                "Other"
            });
            if (cmbPaymentMethod.Items.Count > 0)
                cmbPaymentMethod.SelectedIndex = 0;
        }

        private void LoadProperties()
        {
            var properties = _dataService.LoadProperties();
            cmbProperty.Items.Clear();
            cmbProperty.Items.Add(new PropertyItem { Id = 0, Name = "-- All Properties --" });

            foreach (var property in properties)
            {
                cmbProperty.Items.Add(new PropertyItem { Id = property.Id, Name = property.Name });
            }
            cmbProperty.SelectedIndex = 0;
            cmbProperty.DisplayMember = "Name";
        }

        private void GenerateExpenseNumber()
        {
            _txtExpenseNumber.Text = _dataService.GenerateExpenseNumber();
            lblExpenseNumber.Text = $"New: {_txtExpenseNumber.Text}";
        }

        private void LoadExpense()
        {
            _expense = _dataService.GetExpenseById(_expenseId.Value);
            if (_expense == null) return;

            _txtExpenseNumber.Text = _expense.ExpenseNumber;
            dtpDate.Value = _expense.ExpenseDate;

            // Set category
            string categoryDisplay = GetCategoryDisplayName(_expense.Category);
            for (int i = 0; i < cmbCategory.Items.Count; i++)
            {
                if (cmbCategory.Items[i].ToString() == categoryDisplay)
                {
                    cmbCategory.SelectedIndex = i;
                    break;
                }
            }

            txtPayee.Text = _expense.Payee;
            txtDescription.Text = _expense.Description;
            numAmount.Value = _expense.Amount;

            // Set payment method
            if (!string.IsNullOrEmpty(_expense.PaymentMethod))
            {
                for (int i = 0; i < cmbPaymentMethod.Items.Count; i++)
                {
                    if (cmbPaymentMethod.Items[i].ToString() == _expense.PaymentMethod)
                    {
                        cmbPaymentMethod.SelectedIndex = i;
                        break;
                    }
                }
            }

            txtReference.Text = _expense.ReferenceNumber;
            chkTaxDeductible.Checked = _expense.IsTaxDeductible;
            txtNotes.Text = _expense.Notes;

            // Set property
            if (_expense.PropertyId.HasValue)
            {
                for (int i = 0; i < cmbProperty.Items.Count; i++)
                {
                    if (cmbProperty.Items[i] is PropertyItem prop && prop.Id == _expense.PropertyId.Value)
                    {
                        cmbProperty.SelectedIndex = i;
                        break;
                    }
                }
            }

            lblExpenseNumber.Text = $"Editing: {_expense.ExpenseNumber}";
        }

        private ExpenseCategory GetSelectedCategory()
        {
            if (cmbCategory.SelectedItem == null)
                return ExpenseCategory.Miscellaneous;

            string selected = cmbCategory.SelectedItem.ToString();
            foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
            {
                if (GetCategoryDisplayName(category) == selected)
                    return category;
            }
            return ExpenseCategory.Miscellaneous;
        }

        private int? GetSelectedPropertyId()
        {
            if (cmbProperty.SelectedItem == null) return null;

            var selected = cmbProperty.SelectedItem as PropertyItem;
            if (selected == null || selected.Id == 0) return null;

            return selected.Id;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(txtPayee.Text))
                {
                    MessageBox.Show("Please enter payee/vendor name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPayee.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescription.Text))
                {
                    MessageBox.Show("Please enter description.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDescription.Focus();
                    return;
                }

                if (numAmount.Value <= 0)
                {
                    MessageBox.Show("Please enter a valid amount greater than 0.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numAmount.Focus();
                    return;
                }

                if (cmbCategory.SelectedItem == null)
                {
                    MessageBox.Show("Please select a category.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCategory.Focus();
                    return;
                }

                if (_expenseId.HasValue)
                {
                    // Update existing expense
                    if (_expense == null)
                    {
                        _expense = _dataService.GetExpenseById(_expenseId.Value);
                        if (_expense == null)
                        {
                            MessageBox.Show("Expense not found.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    _expense.ExpenseDate = dtpDate.Value;
                    _expense.Category = GetSelectedCategory();
                    _expense.Payee = txtPayee.Text.Trim();
                    _expense.Description = txtDescription.Text.Trim();
                    _expense.Amount = numAmount.Value;
                    _expense.PaymentMethod = cmbPaymentMethod.SelectedItem?.ToString();
                    _expense.ReferenceNumber = txtReference.Text.Trim();
                    _expense.PropertyId = GetSelectedPropertyId();
                    _expense.IsTaxDeductible = chkTaxDeductible.Checked;
                    _expense.Notes = txtNotes.Text.Trim();

                    if (_dataService.UpdateExpense(_expense))
                    {
                        MessageBox.Show("Expense updated successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update expense. Please check the logs.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Add new expense
                    var expense = new Expense
                    {
                        ExpenseNumber = _txtExpenseNumber.Text,
                        ExpenseDate = dtpDate.Value,
                        Category = GetSelectedCategory(),
                        Payee = txtPayee.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        Amount = numAmount.Value,
                        PaymentMethod = cmbPaymentMethod.SelectedItem?.ToString(),
                        ReferenceNumber = txtReference.Text.Trim(),
                        PropertyId = GetSelectedPropertyId(),
                        IsTaxDeductible = chkTaxDeductible.Checked,
                        Notes = txtNotes.Text.Trim(),
                        CreatedBy = Environment.UserName,
                        CreatedDate = DateTime.Now
                    };

                    if (_dataService.AddExpense(expense))
                    {
                        MessageBox.Show("Expense added successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add expense. Please check the logs.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving expense: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}