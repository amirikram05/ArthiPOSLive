using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;

namespace ShopRentManagementSystem
{
    public partial class FrmExpenseReport : Form
    {
        private readonly JsonDataService _dataService;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private ComboBox cmbCategory;
        private DataGridView dgvExpenses;
        private DataGridView dgvSummary;
        private Label lblTotal;
        private Label lblCount;
        private Label lblAverage;
        private Button btnGenerate;
        private Button btnExport;
        private Button btnPrint;
        private Panel pnlSummary;

        public FrmExpenseReport()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            LoadCategories();
        }

        private void InitializeComponent()
        {
            this.Text = "Expense Report";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Top Panel
            Panel pnlTop = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(10)
            };

            int xPos = 20;
            int yPos = 20;

            // Date Range
            Label lblFrom = new Label
            {
                Text = "From Date:",
                Location = new Point(xPos, yPos),
                Size = new Size(70, 25)
            };
            dtpFrom = new DateTimePicker
            {
                Location = new Point(xPos + 75, yPos),
                Size = new Size(120, 25),
                Value = DateTime.Now.AddMonths(-1),
                Format = DateTimePickerFormat.Short
            };

            xPos += 210;

            Label lblTo = new Label
            {
                Text = "To Date:",
                Location = new Point(xPos, yPos),
                Size = new Size(60, 25)
            };
            dtpTo = new DateTimePicker
            {
                Location = new Point(xPos + 65, yPos),
                Size = new Size(120, 25),
                Value = DateTime.Now,
                Format = DateTimePickerFormat.Short
            };

            xPos += 200;

            // Category Filter
            Label lblCategory = new Label
            {
                Text = "Category:",
                Location = new Point(xPos, yPos),
                Size = new Size(70, 25)
            };
            cmbCategory = new ComboBox
            {
                Location = new Point(xPos + 75, yPos),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            xPos += 290;

            // Generate Button
            btnGenerate = new Button
            {
                Text = "📊 Generate Report",
                Location = new Point(xPos, yPos - 2),
                Size = new Size(150, 30),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnGenerate.Click += BtnGenerate_Click;

            // Summary Panel
            pnlSummary = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size(1100, 80),
                BackColor = Color.FromArgb(220, 240, 255),
                BorderStyle = BorderStyle.FixedSingle
            };

            int sx = 30;
            lblTotal = CreateSummaryLabel("Total Expenses:", "₹0.00", sx, 15, true);
            sx += 200;
            lblCount = CreateSummaryLabel("Transactions:", "0", sx, 15);
            sx += 200;
            lblAverage = CreateSummaryLabel("Average:", "₹0.00", sx, 15);
            sx += 200;
            Label lblLargest = CreateSummaryLabel("Largest:", "₹0.00", sx, 15);
            sx += 200;
            Label lblSmallest = CreateSummaryLabel("Smallest:", "₹0.00", sx, 15);

            pnlSummary.Controls.AddRange(new Control[] { lblTotal, lblCount, lblAverage, lblLargest, lblSmallest });

            pnlTop.Controls.AddRange(new Control[] {
                lblFrom, dtpFrom, lblTo, dtpTo,
                lblCategory, cmbCategory, btnGenerate, pnlSummary
            });

            // Tab Control
            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9)
            };

            // Tab 1: Detailed Expenses
            TabPage tabDetails = new TabPage("📋 Detailed Expenses");
            dgvExpenses = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                RowHeadersVisible = false
            };
            SetupExpensesGrid();
            tabDetails.Controls.Add(dgvExpenses);

            // Tab 2: Category Summary
            TabPage tabSummary = new TabPage("📊 Category Summary");
            dgvSummary = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            SetupSummaryGrid();
            tabSummary.Controls.Add(dgvSummary);

            tabControl.TabPages.AddRange(new TabPage[] { tabDetails, tabSummary });

            // Bottom Buttons
            Panel pnlButtons = new Panel
            {
                Height = 50,
                Dock = DockStyle.Bottom,
                BackColor = Color.LightGray
            };

            btnExport = new Button
            {
                Text = "📤 Export to Excel",
                Location = new Point(20, 10),
                Size = new Size(120, 30)
            };
            btnExport.Click += BtnExport_Click;

            btnPrint = new Button
            {
                Text = "🖨️ Print Report",
                Location = new Point(150, 10),
                Size = new Size(120, 30)
            };
            btnPrint.Click += BtnPrint_Click;

            pnlButtons.Controls.AddRange(new Control[] { btnExport, btnPrint });

            this.Controls.AddRange(new Control[] { tabControl, pnlTop, pnlButtons });
        }

        private Label CreateSummaryLabel(string caption, string value, int x, int y, bool isMain = false)
        {
            Label lblCaption = new Label
            {
                Text = caption,
                Location = new Point(x, y),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = Color.DimGray
            };

            Label lblValue = new Label
            {
                Text = value,
                Location = new Point(x, y + 20),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 12, isMain ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = isMain ? Color.DarkBlue : Color.Black
            };

            // Return the value label for later updates
            return lblValue;
        }

        private void SetupExpensesGrid()
        {
            dgvExpenses.Columns.Clear();
            dgvExpenses.Columns.Add("Date", "Date");
            dgvExpenses.Columns.Add("ExpenseNo", "Expense #");
            dgvExpenses.Columns.Add("Category", "Category");
            dgvExpenses.Columns.Add("Payee", "Payee");
            dgvExpenses.Columns.Add("Description", "Description");
            dgvExpenses.Columns.Add("Amount", "Amount");
            dgvExpenses.Columns.Add("PaymentMethod", "Payment Method");
            dgvExpenses.Columns.Add("TaxDeductible", "Tax Deductible");

            dgvExpenses.Columns["Amount"].DefaultCellStyle.Format = "C";
            dgvExpenses.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExpenses.Columns["Date"].DefaultCellStyle.Format = "dd-MMM-yyyy";
        }

        private void SetupSummaryGrid()
        {
            dgvSummary.Columns.Clear();
            dgvSummary.Columns.Add("Category", "Category");
            dgvSummary.Columns.Add("Count", "No. of Transactions");
            dgvSummary.Columns.Add("Total", "Total Amount");
            dgvSummary.Columns.Add("Average", "Average Amount");
            dgvSummary.Columns.Add("Percentage", "% of Total");

            dgvSummary.Columns["Total"].DefaultCellStyle.Format = "C";
            dgvSummary.Columns["Average"].DefaultCellStyle.Format = "C";
            dgvSummary.Columns["Percentage"].DefaultCellStyle.Format = "F1";
            dgvSummary.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSummary.Columns["Average"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSummary.Columns["Percentage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                ExpenseCategory.Utilities => "Utilities",
                ExpenseCategory.Maintenance => "Maintenance",
                ExpenseCategory.Insurance => "Insurance",
                ExpenseCategory.Taxes => "Taxes",
                ExpenseCategory.Cleaning => "Cleaning",
                ExpenseCategory.Security => "Security",
                ExpenseCategory.Marketing => "Marketing",
                ExpenseCategory.ProfessionalFees => "Professional Fees",
                ExpenseCategory.Supplies => "Supplies",
                ExpenseCategory.Salaries => "Salaries",
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

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var filter = new ExpenseFilter
                {
                    FromDate = dtpFrom.Value,
                    ToDate = dtpTo.Value,
                    Category = GetSelectedCategory()
                };

                var expenses = _dataService.FilterExpenses(filter);

                // Update summary labels
                decimal total = expenses.Sum(ex => ex.Amount);
                int count = expenses.Count;
                decimal average = count > 0 ? total / count : 0;
                decimal largest = count > 0 ? expenses.Max(ex => ex.Amount) : 0;
                decimal smallest = count > 0 ? expenses.Min(ex => ex.Amount) : 0;

                // Find the summary labels in pnlSummary
                foreach (Control ctrl in pnlSummary.Controls)
                {
                    if (ctrl is Label lbl)
                    {
                        if (lbl.Text.StartsWith("₹") || decimal.TryParse(lbl.Text.Replace("₹", "").Replace(",", ""), out _))
                        {
                            // This is a value label - we need to identify which one
                            Point loc = lbl.Location;
                            if (loc.X == 30) lbl.Text = total.ToString("C");
                            else if (loc.X == 230) lbl.Text = count.ToString();
                            else if (loc.X == 430) lbl.Text = average.ToString("C");
                            else if (loc.X == 630) lbl.Text = largest.ToString("C");
                            else if (loc.X == 830) lbl.Text = smallest.ToString("C");
                        }
                    }
                }

                // Load expenses grid
                dgvExpenses.Rows.Clear();
                foreach (var exp in expenses.OrderByDescending(ex => ex.ExpenseDate))
                {
                    dgvExpenses.Rows.Add(
                        exp.ExpenseDate,
                        exp.ExpenseNumber,
                        GetCategoryDisplayName(exp.Category),
                        exp.Payee,
                        exp.Description,
                        exp.Amount,
                        exp.PaymentMethod,
                        exp.IsTaxDeductible ? "Yes" : "No"
                    );
                }

                // Load summary grid
                var categoryGroups = expenses.GroupBy(ex => ex.Category)
                    .Select(g => new
                    {
                        Category = GetCategoryDisplayName(g.Key),
                        Count = g.Count(),
                        Total = g.Sum(ex => ex.Amount),
                        Average = g.Average(ex => ex.Amount),
                        Percentage = total > 0 ? (g.Sum(ex => ex.Amount) / total) * 100 : 0
                    })
                    .OrderByDescending(g => g.Total);

                dgvSummary.Rows.Clear();
                foreach (var group in categoryGroups)
                {
                    dgvSummary.Rows.Add(
                        group.Category,
                        group.Count,
                        group.Total,
                        group.Average,
                        $"{group.Percentage:F1}%"
                    );
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error generating report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = $"Expense_Report_{DateTime.Now:yyyyMMdd}.csv"
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
                    writer.WriteLine("Date,Expense #,Category,Payee,Description,Amount,Payment Method,Tax Deductible");

                    // Write data
                    foreach (DataGridViewRow row in dgvExpenses.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            writer.WriteLine($"{row.Cells[0].Value:dd-MMM-yyyy}," +
                                $"{row.Cells[1].Value}," +
                                $"\"{row.Cells[2].Value}\"," +
                                $"\"{row.Cells[3].Value}\"," +
                                $"\"{row.Cells[4].Value}\"," +
                                $"{row.Cells[5].Value}," +
                                $"\"{row.Cells[6].Value}\"," +
                                $"{row.Cells[7].Value}");
                        }
                    }
                }

                MessageBox.Show($"Report exported successfully to:\n{filePath}",
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
            MessageBox.Show("Print functionality will be implemented with a reporting tool.",
                "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}