using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using ArthiPOS.Rent.Expense;

namespace ShopRentManagementSystem
{
    public partial class FrmExpenses : Form
    {
        private readonly JsonDataService _dataService;
        private DataGridView dgvExpenses;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnView;
        private Button btnRefresh;
        private Button btnExport;
        private Button btnPrint;
        private TextBox txtSearch;
        private ComboBox cmbCategory;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Label lblTotalAmount;

        public FrmExpenses()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            LoadCategories();
            LoadExpenses();
        }

        private void InitializeComponent()
        {
            this.Text = "Expense Management";
            this.Size = new Size(1300, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = SystemColors.Control;

            // Top Filter Panel
            Panel pnlFilter = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(10)
            };

            int xPos = 10;
            int yPos = 15;

            // Search
            Label lblSearch = new Label
            {
                Text = "🔍 Search:",
                Location = new Point(xPos, yPos),
                Size = new Size(60, 25)
            };
            txtSearch = new TextBox
            {
                Location = new Point(xPos + 65, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 9)
            };
            txtSearch.TextChanged += (s, e) => LoadExpenses();

            xPos += 280;

            // Date Range
            Label lblFrom = new Label
            {
                Text = "From:",
                Location = new Point(xPos, yPos),
                Size = new Size(40, 25)
            };
            dtpFrom = new DateTimePicker
            {
                Location = new Point(xPos + 45, yPos),
                Size = new Size(120, 25),
                Value = DateTime.Now.AddMonths(-1),
                Format = DateTimePickerFormat.Short
            };
            dtpFrom.ValueChanged += (s, e) => LoadExpenses();

            xPos += 180;

            Label lblTo = new Label
            {
                Text = "To:",
                Location = new Point(xPos, yPos),
                Size = new Size(30, 25)
            };
            dtpTo = new DateTimePicker
            {
                Location = new Point(xPos + 35, yPos),
                Size = new Size(120, 25),
                Value = DateTime.Now,
                Format = DateTimePickerFormat.Short
            };
            dtpTo.ValueChanged += (s, e) => LoadExpenses();

            xPos += 170;

            // Category Filter
            Label lblCategory = new Label
            {
                Text = "Category:",
                Location = new Point(xPos, yPos),
                Size = new Size(60, 25)
            };
            cmbCategory = new ComboBox
            {
                Location = new Point(xPos + 65, yPos),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategory.SelectedIndexChanged += (s, e) => LoadExpenses();

            // Second row - Summary
            yPos += 35;
            xPos = 10;

            Panel pnlSummary = new Panel
            {
                Location = new Point(xPos, yPos),
                Size = new Size(1200, 35),
                BackColor = Color.FromArgb(220, 220, 220)
            };

            lblTotalAmount = new Label
            {
                Text = "Total: ₹0.00",
                Location = new Point(10, 5),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };

            Label lblInfo = new Label
            {
                Text = "Double-click on any expense to view details",
                Location = new Point(500, 5),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Gray
            };

            pnlSummary.Controls.AddRange(new Control[] { lblTotalAmount, lblInfo });

            pnlFilter.Controls.AddRange(new Control[] {
                lblSearch, txtSearch, lblFrom, dtpFrom, lblTo, dtpTo,
                lblCategory, cmbCategory, pnlSummary
            });

            // Data Grid View
            dgvExpenses = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                BackgroundColor = SystemColors.Window,
                BorderStyle = BorderStyle.Fixed3D
            };

            SetupDataGridView();

            // Bottom Buttons Panel
            Panel pnlButtons = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.LightGray,
                Padding = new Padding(10)
            };

            btnAdd = new Button
            {
                Text = "➕ Add New Expense",
                Location = new Point(10, 10),
                Size = new Size(150, 35),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnAdd.Click += BtnAdd_Click;

            btnEdit = new Button
            {
                Text = "✏️ Edit",
                Location = new Point(170, 10),
                Size = new Size(100, 35)
            };
            btnEdit.Click += BtnEdit_Click;

            btnView = new Button
            {
                Text = "👁️ View Details",
                Location = new Point(280, 10),
                Size = new Size(120, 35),
                BackColor = Color.LightBlue
            };
            btnView.Click += BtnView_Click;

            btnDelete = new Button
            {
                Text = "🗑️ Delete",
                Location = new Point(410, 10),
                Size = new Size(100, 35),
                BackColor = Color.LightCoral
            };
            btnDelete.Click += BtnDelete_Click;

            btnRefresh = new Button
            {
                Text = "🔄 Refresh",
                Location = new Point(520, 10),
                Size = new Size(100, 35)
            };
            btnRefresh.Click += (s, e) => LoadExpenses();

            btnExport = new Button
            {
                Text = "📊 Export",
                Location = new Point(630, 10),
                Size = new Size(100, 35)
            };
            btnExport.Click += BtnExport_Click;

            btnPrint = new Button
            {
                Text = "🖨️ Print",
                Location = new Point(740, 10),
                Size = new Size(100, 35)
            };
            btnPrint.Click += BtnPrint_Click;

            pnlButtons.Controls.AddRange(new Control[] {
                btnAdd, btnEdit, btnView, btnDelete, btnRefresh, btnExport, btnPrint
            });

            this.Controls.AddRange(new Control[] { dgvExpenses, pnlFilter, pnlButtons });

            // Double-click to view
            dgvExpenses.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    ViewSelectedExpense();
                }
            };
        }

        private void SetupDataGridView()
        {
            dgvExpenses.Columns.Clear();

            var columns = new[]
            {
                new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", Width = 50 },
                new DataGridViewTextBoxColumn { Name = "ExpenseNumber", HeaderText = "Expense #", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "ExpenseDate", HeaderText = "Date", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "Category", HeaderText = "Category", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "Payee", HeaderText = "Payee/Vendor", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "Description", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "Amount", HeaderText = "Amount", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "PaymentMethod", HeaderText = "Payment Method", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "ReferenceNumber", HeaderText = "Ref #", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "TaxDeductible", HeaderText = "Tax Deductible", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "PropertyName", HeaderText = "Property", Width = 120 }
            };

            dgvExpenses.Columns.AddRange(columns);

            // Format columns
            dgvExpenses.Columns["Amount"].DefaultCellStyle.Format = "C";
            dgvExpenses.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExpenses.Columns["ExpenseDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

            // Style headers
            dgvExpenses.EnableHeadersVisualStyles = false;
            dgvExpenses.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvExpenses.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvExpenses.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        }

        private void LoadCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All Categories");

            foreach (var category in Enum.GetValues(typeof(ExpenseCategory)))
            {
                cmbCategory.Items.Add(GetCategoryDisplayName((ExpenseCategory)category));
            }

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

        private ExpenseCategory? GetSelectedCategory()
        {
            if (cmbCategory.SelectedIndex <= 0) return null;

            string selected = cmbCategory.SelectedItem.ToString();
            foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
            {
                if (GetCategoryDisplayName(category) == selected)
                    return category;
            }
            return null;
        }

        private void LoadExpenses()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var filter = new ExpenseFilter
                {
                    FromDate = dtpFrom.Value,
                    ToDate = dtpTo.Value,
                    SearchText = txtSearch.Text,
                    Category = GetSelectedCategory()
                };

                var expenses = _dataService.FilterExpenses(filter);
                var properties = _dataService.LoadProperties();

                dgvExpenses.Rows.Clear();

                foreach (var expense in expenses)
                {
                    string propertyName = "All Properties";
                    if (expense.PropertyId.HasValue)
                    {
                        var property = properties.FirstOrDefault(p => p.Id == expense.PropertyId.Value);
                        propertyName = property?.Name ?? "Unknown";
                    }

                    dgvExpenses.Rows.Add(
                        expense.Id,
                        expense.ExpenseNumber,
                        expense.ExpenseDate,
                        GetCategoryDisplayName(expense.Category),
                        expense.Payee,
                        expense.Description,
                        expense.Amount,
                        expense.PaymentMethod,
                        expense.ReferenceNumber,
                        expense.IsTaxDeductible ? "Yes ✓" : "No ✗",
                        propertyName
                    );
                }

                // Update total
                decimal total = expenses.Sum(e => e.Amount);
                lblTotalAmount.Text = $"Total: {total:C}";

                // Update title with count
                this.Text = $"Expense Management - {expenses.Count} Expenses";

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error loading expenses: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var dialog = new FrmExpenseEntry())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadExpenses();
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an expense to edit.", "Select Expense",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvExpenses.SelectedRows[0];
            int expenseId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            using (var dialog = new FrmExpenseEntry(expenseId))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadExpenses();
                }
            }
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            ViewSelectedExpense();
        }

        private void ViewSelectedExpense()
        {
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an expense to view.", "Select Expense",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvExpenses.SelectedRows[0];
            int expenseId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            using (var dialog = new FrmExpenseViewer(expenseId))
            {
                dialog.ShowDialog();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an expense to delete.", "Select Expense",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvExpenses.SelectedRows[0];
            int expenseId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
            string expenseNumber = selectedRow.Cells["ExpenseNumber"].Value.ToString();
            decimal amount = Convert.ToDecimal(selectedRow.Cells["Amount"].Value);

            var result = MessageBox.Show(
                $"Are you sure you want to delete expense #{expenseNumber}?\n" +
                $"Amount: {amount:C}\n\n" +
                "This action can be reversed later if needed.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string reason = InputDialog.Show(
                    "Please enter reason for deletion (optional):",
                    "Delete Expense",
                    "");

                if (_dataService.DeleteExpense(expenseId, reason))
                {
                    MessageBox.Show("Expense deleted successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadExpenses();
                }
                else
                {
                    MessageBox.Show("Failed to delete expense.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx)|*.xlsx",
                FileName = $"Expenses_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToCsv(saveDialog.FileName);
            }
        }

        private void ExportToCsv(string filePath)
        {
            try
            {
                using (var writer = new System.IO.StreamWriter(filePath))
                {
                    // Write header
                    writer.WriteLine("Expense #,Date,Category,Payee,Description,Amount,Payment Method,Reference,Tax Deductible");

                    // Write data
                    foreach (DataGridViewRow row in dgvExpenses.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            writer.WriteLine($"{row.Cells["ExpenseNumber"].Value}," +
                                $"{row.Cells["ExpenseDate"].Value:dd-MMM-yyyy}," +
                                $"\"{row.Cells["Category"].Value}\"," +
                                $"\"{row.Cells["Payee"].Value}\"," +
                                $"\"{row.Cells["Description"].Value}\"," +
                                $"{row.Cells["Amount"].Value}," +
                                $"\"{row.Cells["PaymentMethod"].Value}\"," +
                                $"\"{row.Cells["ReferenceNumber"].Value}\"," +
                                $"{row.Cells["TaxDeductible"].Value}");
                        }
                    }
                }

                MessageBox.Show($"Expenses exported successfully to:\n{filePath}",
                    "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            // Simple print preview
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Print functionality will be implemented with a reporting tool.",
                    "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}