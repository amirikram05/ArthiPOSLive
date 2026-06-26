using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;

namespace ShopRentManagementSystem
{
    public partial class FrmExpenseDashboard : Form
    {
        private readonly JsonDataService _dataService;
        private Label lblTotalMonth;
        private Label lblTotalYear;
        private Label lblAverageMonth;
        private Label lblCount;
        private Label lblChange;
        private DataGridView dgvRecent;
        private Panel pnlCategories;
        private Timer refreshTimer;

        public FrmExpenseDashboard()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            LoadDashboard();

            // Auto-refresh every 5 minutes
            refreshTimer = new Timer { Interval = 300000 };
            refreshTimer.Tick += (s, e) => LoadDashboard();
            refreshTimer.Start();
        }

        private void InitializeComponent()
        {
            this.Text = "Expense Dashboard";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Title
            Label lblTitle = new Label
            {
                Text = "📊 Expense Management Dashboard",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.SteelBlue,
                Location = new Point(20, 20),
                Size = new Size(600, 40),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Refresh Button
            Button btnRefresh = new Button
            {
                Text = "🔄 Refresh",
                Location = new Point(800, 25),
                Size = new Size(120, 30),
                BackColor = Color.LightGray
            };
            btnRefresh.Click += (s, e) => LoadDashboard();

            // Summary Cards Panel
            Panel pnlCards = new Panel
            {
                Location = new Point(20, 70),
                Size = new Size(950, 120),
                BackColor = Color.Transparent
            };

            // Create summary cards
            int cardWidth = 180;
            int cardHeight = 100;
            int spacing = 10;

            pnlCards.Controls.Add(CreateSummaryCard("This Month", "₹0", 0, 0, Color.FromArgb(70, 130, 180), ref lblTotalMonth));
            pnlCards.Controls.Add(CreateSummaryCard("This Year", "₹0", cardWidth + spacing, 0, Color.FromArgb(60, 179, 113), ref lblTotalYear));
            pnlCards.Controls.Add(CreateSummaryCard("Monthly Avg", "₹0", 2 * (cardWidth + spacing), 0, Color.FromArgb(255, 140, 0), ref lblAverageMonth));
            pnlCards.Controls.Add(CreateSummaryCard("Transactions", "0", 3 * (cardWidth + spacing), 0, Color.FromArgb(128, 0, 128), ref lblCount));
            pnlCards.Controls.Add(CreateSummaryCard("vs Last Month", "0%", 4 * (cardWidth + spacing), 0, Color.FromArgb(220, 20, 60), ref lblChange));

            // Recent Expenses Panel
            GroupBox gbRecent = new GroupBox
            {
                Text = "📋 Recent Expenses",
                Location = new Point(20, 210),
                Size = new Size(580, 400),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            dgvRecent = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = Color.White
            };

            dgvRecent.Columns.Add("Date", "Date");
            dgvRecent.Columns.Add("ExpenseNo", "Expense #");
            dgvRecent.Columns.Add("Category", "Category");
            dgvRecent.Columns.Add("Payee", "Payee");
            dgvRecent.Columns.Add("Amount", "Amount");

            dgvRecent.Columns["Date"].Width = 80;
            dgvRecent.Columns["ExpenseNo"].Width = 100;
            dgvRecent.Columns["Category"].Width = 120;
            dgvRecent.Columns["Payee"].Width = 150;
            dgvRecent.Columns["Amount"].Width = 100;
            dgvRecent.Columns["Amount"].DefaultCellStyle.Format = "C";
            dgvRecent.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvRecent.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    int expenseId = Convert.ToInt32(dgvRecent.Rows[e.RowIndex].Cells["Id"].Value);
                    using (var viewer = new FrmExpenseViewer(expenseId))
                    {
                        viewer.ShowDialog();
                    }
                }
            };

            // Add hidden ID column
            dgvRecent.Columns.Add("Id", "ID");
            dgvRecent.Columns["Id"].Visible = false;

            gbRecent.Controls.Add(dgvRecent);

            // Category Breakdown Panel
            GroupBox gbCategories = new GroupBox
            {
                Text = "📊 Category Breakdown (This Year)",
                Location = new Point(620, 210),
                Size = new Size(350, 400),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            pnlCategories = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White
            };
            gbCategories.Controls.Add(pnlCategories);

            // Quick Actions
            GroupBox gbActions = new GroupBox
            {
                Text = "⚡ Quick Actions",
                Location = new Point(20, 620),
                Size = new Size(950, 60),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            Button btnAddExpense = new Button
            {
                Text = "➕ Add New Expense",
                Location = new Point(20, 20),
                Size = new Size(150, 30),
                BackColor = Color.LightGreen
            };
            btnAddExpense.Click += (s, e) =>
            {
                using (var entry = new FrmExpenseEntry())
                {
                    if (entry.ShowDialog() == DialogResult.OK)
                        LoadDashboard();
                }
            };

            Button btnViewAll = new Button
            {
                Text = "📋 View All Expenses",
                Location = new Point(180, 20),
                Size = new Size(150, 30)
            };
            btnViewAll.Click += (s, e) =>
            {
                var expensesForm = new FrmExpenses();
                expensesForm.ShowDialog();
            };

            Button btnGenerateReport = new Button
            {
                Text = "📊 Generate Report",
                Location = new Point(340, 20),
                Size = new Size(150, 30)
            };
            btnGenerateReport.Click += (s, e) =>
            {
                var reportForm = new FrmExpenseReport();
                reportForm.ShowDialog();
            };

            gbActions.Controls.AddRange(new Control[] { btnAddExpense, btnViewAll, btnGenerateReport });

            this.Controls.AddRange(new Control[] {
                lblTitle, btnRefresh, pnlCards, gbRecent, gbCategories, gbActions
            });
        }

        private Panel CreateSummaryCard(string title, string value, int x, int y, Color color, ref Label valueLabel)
        {
            Panel card = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(180, 100),
                BackColor = color,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblTitle = new Label
            {
                Text = title,
                Location = new Point(10, 10),
                Size = new Size(160, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };

            valueLabel = new Label
            {
                Text = value,
                Location = new Point(10, 40),
                Size = new Size(160, 40),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };

            card.Controls.AddRange(new Control[] { lblTitle, valueLabel });
            return card;
        }

        private void LoadDashboard()
        {
            try
            {
                var summary = _dataService.GetExpenseDashboardSummary();

                // Update summary cards
                lblTotalMonth.Text = summary.TotalExpensesThisMonth.ToString("C");
                lblTotalYear.Text = summary.TotalExpensesThisYear.ToString("C");
                lblAverageMonth.Text = summary.AverageMonthlyExpense.ToString("C");
                lblCount.Text = summary.ExpenseCountThisMonth.ToString();

                string changeText = summary.MonthOverMonthChange >= 0 ?
                    $"+{summary.MonthOverMonthChange:F1}%" :
                    $"{summary.MonthOverMonthChange:F1}%";
                lblChange.Text = changeText;
                lblChange.ForeColor = summary.MonthOverMonthChange <= 0 ? Color.LightGreen : Color.White;

                // Update recent expenses
                dgvRecent.Rows.Clear();
                foreach (var expense in summary.RecentExpenses)
                {
                    dgvRecent.Rows.Add(
                        expense.ExpenseDate.ToString("dd-MMM"),
                        expense.ExpenseNumber,
                        GetCategoryShortName(expense.Category),
                        expense.Payee,
                        expense.Amount,
                        expense.Id
                    );
                }

                // Update category breakdown
                pnlCategories.Controls.Clear();
                int yPos = 10;

                foreach (var kvp in summary.CategoryBreakdown.OrderByDescending(k => k.Value))
                {
                    string categoryName = GetCategoryShortName(kvp.Key);
                    decimal amount = kvp.Value;
                    decimal percentage = summary.TotalExpensesThisYear > 0 ?
                        (amount / summary.TotalExpensesThisYear) * 100 : 0;

                    Panel item = new Panel
                    {
                        Location = new Point(10, yPos),
                        Size = new Size(320, 40),
                        BackColor = yPos % 80 == 10 ? Color.FromArgb(250, 250, 250) : Color.White
                    };

                    Label lblCat = new Label
                    {
                        Text = categoryName,
                        Location = new Point(10, 10),
                        Size = new Size(150, 20),
                        Font = new Font("Segoe UI", 9, FontStyle.Regular)
                    };

                    Label lblAmt = new Label
                    {
                        Text = amount.ToString("C"),
                        Location = new Point(170, 10),
                        Size = new Size(100, 20),
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleRight,
                        ForeColor = Color.DarkBlue
                    };

                    Label lblPct = new Label
                    {
                        Text = $"{percentage:F1}%",
                        Location = new Point(270, 10),
                        Size = new Size(50, 20),
                        Font = new Font("Segoe UI", 8, FontStyle.Regular),
                        TextAlign = ContentAlignment.MiddleRight,
                        ForeColor = Color.Gray
                    };

                    item.Controls.AddRange(new Control[] { lblCat, lblAmt, lblPct });
                    pnlCategories.Controls.Add(item);

                    yPos += 35;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCategoryShortName(ExpenseCategory category)
        {
            return category switch
            {
                ExpenseCategory.Utilities => "⚡ Utilities",
                ExpenseCategory.Maintenance => "🔧 Maintenance",
                ExpenseCategory.Insurance => "🛡️ Insurance",
                ExpenseCategory.Taxes => "📄 Taxes",
                ExpenseCategory.Cleaning => "🧹 Cleaning",
                ExpenseCategory.Security => "🔒 Security",
                ExpenseCategory.Marketing => "📢 Marketing",
                ExpenseCategory.ProfessionalFees => "👔 Professional",
                ExpenseCategory.Supplies => "📦 Supplies",
                ExpenseCategory.Salaries => "👥 Salaries",
                ExpenseCategory.Equipment => "💻 Equipment",
                ExpenseCategory.Miscellaneous => "📌 Misc",
                _ => category.ToString()
            };
        }
    }
}