using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DataMember;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using Expense = ShopRentManagementSystem.Models.Expense;

namespace ShopRentManagementSystem
{
    public partial class FrmExpenseViewer : Form
    {
        private readonly JsonDataService _dataService;
        private Expense _expense;

        public FrmExpenseViewer(int expenseId)
        {
            _dataService = new JsonDataService();
            _expense = _dataService.GetExpenseById(expenseId);
            InitializeComponent();
            LoadExpenseDetails();
        }

        private void InitializeComponent()
        {
            this.Text = "Expense Details";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true
            };

            if (_expense == null)
            {
                Label lblError = new Label
                {
                    Text = "❌ Expense not found or has been deleted.",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.Red,
                    Location = new Point(50, 50),
                    Size = new Size(400, 50),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pnlMain.Controls.Add(lblError);
            }

            Button btnClose = new Button
            {
                Text = "Close",
                Location = new Point(250, 400),
                Size = new Size(100, 35),
                DialogResult = DialogResult.OK
            };

            this.Controls.AddRange(new Control[] { pnlMain, btnClose });
        }

        private void LoadExpenseDetails()
        {
            if (_expense == null) return;

            Panel pnlMain = this.Controls[0] as Panel;
            pnlMain.Controls.Clear();

            int yPos = 20;
            int labelWidth = 150;
            int valueWidth = 350;

            // Header
            Label lblHeader = new Label
            {
                Text = $"🧾 Expense Details - {_expense.ExpenseNumber}",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.SteelBlue,
                Location = new Point(20, yPos),
                Size = new Size(500, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlMain.Controls.Add(lblHeader);
            yPos += 40;

            // Create a panel for details
            Panel pnlDetails = new Panel
            {
                Location = new Point(20, yPos),
                Size = new Size(520, 300),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(250, 250, 250)
            };

            int dy = 15;

            AddDetailRow(pnlDetails, "Expense #:", _expense.ExpenseNumber, ref dy);
            AddDetailRow(pnlDetails, "Date:", _expense.ExpenseDate.ToString("dddd, dd MMMM yyyy"), ref dy);
            AddDetailRow(pnlDetails, "Category:", GetCategoryDisplayName(_expense.Category), ref dy);
            AddDetailRow(pnlDetails, "Payee/Vendor:", _expense.Payee, ref dy);
            AddDetailRow(pnlDetails, "Description:", _expense.Description, ref dy);
            AddDetailRow(pnlDetails, "Amount:", _expense.Amount.ToString("C"), ref dy, true);
            AddDetailRow(pnlDetails, "Payment Method:", _expense.PaymentMethod, ref dy);
            AddDetailRow(pnlDetails, "Reference #:", _expense.ReferenceNumber ?? "N/A", ref dy);
            AddDetailRow(pnlDetails, "Tax Deductible:", _expense.IsTaxDeductible ? "Yes ✓" : "No ✗", ref dy);

            // Property info
            if (_expense.PropertyId.HasValue)
            {
                var properties = _dataService.LoadProperties();
                var property = properties.FirstOrDefault(p => p.Id == _expense.PropertyId.Value);
                AddDetailRow(pnlDetails, "Property:", property?.Name ?? "Unknown", ref dy);
            }

            // Notes
            if (!string.IsNullOrEmpty(_expense.Notes))
            {
                AddDetailRow(pnlDetails, "Notes:", _expense.Notes, ref dy);
            }

            // Audit info
            AddDetailRow(pnlDetails, "Created By:", _expense.CreatedBy ?? "System", ref dy);
            AddDetailRow(pnlDetails, "Created Date:", _expense.CreatedDate.ToString("dd-MMM-yyyy HH:mm"), ref dy);

            if (_expense.LastModifiedDate.HasValue)
            {
                AddDetailRow(pnlDetails, "Last Modified:", _expense.LastModifiedDate.Value.ToString("dd-MMM-yyyy HH:mm"), ref dy);
            }

            pnlMain.Controls.Add(pnlDetails);
            yPos += 320;

            // Button
            Button btnClose = new Button
            {
                Text = "Close",
                Location = new Point(250, yPos),
                Size = new Size(100, 35),
                DialogResult = DialogResult.OK,
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            pnlMain.Controls.Add(btnClose);
            btnClose.BringToFront();

            // Adjust form height based on content
            this.Height = Math.Min(650, yPos + 100);
        }

        private void AddDetailRow(Panel panel, string label, string value, ref int yPos, bool isBold = false)
        {
            Label lblLabel = new Label
            {
                Text = label,
                Location = new Point(20, yPos),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray,
                TextAlign = ContentAlignment.MiddleRight
            };

            Label lblValue = new Label
            {
                Text = value,
                Location = new Point(180, yPos),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 9, isBold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = isBold ? Color.DarkGreen : Color.Black,
                TextAlign = ContentAlignment.MiddleLeft
            };

            panel.Controls.AddRange(new Control[] { lblLabel, lblValue });
            yPos += 28;
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
    }
}