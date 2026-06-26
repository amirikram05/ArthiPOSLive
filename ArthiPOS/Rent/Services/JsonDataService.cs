using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using ArthiPOS.Utill;
using CommonUtilities;
using DataMember;
using DataMember.memberlog;
using ShopRentManagementSystem.Models;
using Expense = ShopRentManagementSystem.Models.Expense;
using Product = ShopRentManagementSystem.Models.Product;

namespace ShopRentManagementSystem.Services
{
    public class JsonDataService
    {
        private readonly string _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        AdminLog log; 
        public JsonDataService()
        {
            log = LogUtill.getAdminInputLog();
            _dataPath = log.RentMangmentFolder;
            if (!Directory.Exists(_dataPath))
                Directory.CreateDirectory(_dataPath);
        }

        private string GetFilePath(string fileName) => Path.Combine(_dataPath, fileName);

        public List<T> LoadData<T>(string fileName)
        {
            var filePath = GetFilePath(fileName);
            if (!File.Exists(filePath))
                return new List<T>();

            try
            {
                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }

        public void SaveData<T>(string fileName, List<T> data)
        {
            var filePath = GetFilePath(fileName);
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json);
        }

        // Specific entity methods
        public List<Property> LoadProperties() => LoadData<Property>("properties.json");
        public void SaveProperties(List<Property> properties) => SaveData("properties.json", properties);

        public List<Portion> LoadPortions() => LoadData<Portion>("portions.json");
        public void SavePortions(List<Portion> portions) => SaveData("portions.json", portions);

        public List<Tenant> LoadTenants() => LoadData<Tenant>("tenants.json");
        public void SaveTenants(List<Tenant> tenants) => SaveData("tenants.json", tenants);

        public List<Product> LoadProducts() => LoadData<Product>("products.json");
        public void SaveProducts(List<Product> products) => SaveData("products.json", products);

        public List<RentAgreement> LoadAgreements() => LoadData<RentAgreement>("agreements.json");
        public void SaveAgreements(List<RentAgreement> agreements) => SaveData("agreements.json", agreements);

        // Payment methods with soft delete
        public List<Payment> LoadPayments()
        {
            var payments = LoadData<Payment>("payments.json");
            return payments.Where(p => !p.IsDeleted).ToList();
        }

        public List<Payment> LoadAllPayments() => LoadData<Payment>("payments.json");
        public void SavePayments(List<Payment> payments) => SaveData("payments.json", payments);

        public List<CommissionTransaction> LoadCommissionTransactions() => LoadData<CommissionTransaction>("commission_transactions.json");
        public void SaveCommissionTransactions(List<CommissionTransaction> transactions) => SaveData("commission_transactions.json", transactions);

        public bool DeletePayment(int paymentId, string reason = "")
        {
            try
            {
                var payments = LoadAllPayments();
                var payment = payments.FirstOrDefault(p => p.Id == paymentId);

                if (payment != null)
                {
                    if (payment.IsDeleted)
                        return false;

                    payment.IsDeleted = true;
                    payment.DeletedDate = DateTime.Now;
                    payment.Notes += $"\n[DELETED: {DateTime.Now:yyyy-MM-dd HH:mm:ss}] Reason: {reason}";

                    SaveData("payments.json", payments);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public List<Payment> GetPaymentsByAgreement(int agreementId)
        {
            var payments = LoadPayments();
            return payments.Where(p => p.AgreementId == agreementId)
                          .OrderByDescending(p => p.PaymentDate)
                          .ToList();
        }

        // Get products for an agreement
        public List<Product> GetProductsForAgreement(int agreementId)
        {
            var agreements = LoadAgreements();
            var products = LoadProducts();
            var agreement = agreements.FirstOrDefault(a => a.Id == agreementId);

            if (agreement != null && agreement.ProductIds != null && agreement.ProductIds.Any())
            {
                return products.Where(p => agreement.ProductIds.Contains(p.Id) && p.IsActive).ToList();
            }

            return products.Where(p => p.IsActive).ToList(); // Return all active products if none specified
        }

        // Get product by ID
        public Product GetProductById(int productId)
        {
            var products = LoadProducts();
            return products.FirstOrDefault(p => p.Id == productId);
        }

        // Calculate commission for a product transaction
        public CommissionTransaction CalculateCommission(int productId, decimal quantity, decimal laborAmount, decimal commissionRate)
        {
            var product = GetProductById(productId);

            if (product == null)
                return null;

            decimal productTotal = quantity * product.UnitPrice;
            decimal totalAmount = productTotal + laborAmount;
            decimal commissionAmount = (totalAmount * commissionRate) / 100;

            return new CommissionTransaction
            {
                ProductId = productId,
                ProductName = product.Name,
                Quantity = quantity,
                Unit = product.Unit,
                UnitPrice = product.UnitPrice,
                LaborAmount = laborAmount,
                ProductTotal = productTotal,
                CommissionRate = commissionRate,
                CommissionAmount = commissionAmount,
                TransactionDate = DateTime.Now
            };
        }

        // Get commission summary for a payment
        public CommissionSummary GetCommissionSummary(int paymentId)
        {
            var transactions = LoadCommissionTransactions()
                .Where(t => t.PaymentId == paymentId)
                .ToList();

            if (!transactions.Any())
                return null;

            return new CommissionSummary
            {
                TotalQuantity = transactions.Sum(t => t.Quantity),
                TotalProductValue = transactions.Sum(t => t.ProductTotal),
                TotalLabor = transactions.Sum(t => t.LaborAmount),
                TotalSales = transactions.Sum(t => t.ProductTotal + t.LaborAmount),
                TotalCommission = transactions.Sum(t => t.CommissionAmount)
            };
        }

        // Get commission transactions for a payment
        public List<CommissionTransaction> GetCommissionTransactionsForPayment(int paymentId)
        {
            return LoadCommissionTransactions()
                .Where(t => t.PaymentId == paymentId)
                .ToList();
        }

        // Rent calculation methods
        public RentOverview GetRentOverview(RentAgreement agreement)
        {
            var properties = LoadProperties();
            var portions = LoadPortions();
            var tenants = LoadTenants();
            var payments = LoadPayments();

            var property = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);
            var portion = portions.FirstOrDefault(p => p.Id == agreement.PortionId);
            var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);

            if (property == null || portion == null || tenant == null)
                return null;

            var agreementPayments = payments
                .Where(p => p.AgreementId == agreement.Id)
                .OrderByDescending(p => p.PaymentDate)
                .ToList();

            var lastPayment = agreementPayments.FirstOrDefault();

            decimal totalPaid = agreementPayments.Sum(p => p.Amount);
            if (tenant.Type == TenantType.OnRent)
            {
                int monthsPassed = (DateTime.Now.Year - agreement.StartDate.Year) * 12 +
                                  DateTime.Now.Month - agreement.StartDate.Month;
                monthsPassed = Math.Max(0, monthsPassed);

                decimal totalRentDue = 0;
                DateTime currentDate = agreement.StartDate;
                decimal currentRent = agreement.MonthlyRent;
                DateTime lastIncreaseDate = agreement.LastIncreaseDate;

                for (int i = 0; i <= monthsPassed; i++)
                {
                    if (agreement.IncreaseMode == RentIncreaseMode.Auto)
                    {
                        if (currentDate > lastIncreaseDate.AddYears(1))
                        {
                            currentRent *= 1.10m;
                            lastIncreaseDate = currentDate;
                        }
                    }

                    if (i > 0)
                    {
                        totalRentDue += currentRent;
                    }

                    currentDate = currentDate.AddMonths(1);
                }

                decimal dueAmount = totalRentDue - totalPaid;
                DateTime nextDueDate = agreement.StartDate.AddMonths(monthsPassed + 1);
                string statusColor = dueAmount <= 0 ? "Green" : "Red";

                return new RentOverview
                {
                    PropertyName = property.Name,
                    PortionName = portion.Name,
                    PortionSize = portion.Size,
                    TenantName = tenant.Name,
                    Mobile = tenant.Mobile,
                    MonthlyRent = currentRent,
                    LastPaidAmount = lastPayment?.Amount ?? 0,
                    DueAmount = dueAmount,
                    LastPaymentDate = lastPayment?.PaymentDate ?? DateTime.MinValue,
                    NextDueDate = nextDueDate,
                    AgreementId = agreement.Id,
                    TenantId = tenant.Id,
                    PropertyId = property.Id,
                    PortionId = portion.Id,
                    StatusColor = statusColor,
                    TenantType = tenant.Type,
                    PaymentInfo = $"Rent: {currentRent:C}/month"
                };
            }
            else if (tenant.Type == TenantType.OnCommission)
            {
                decimal commissionDue = 0;
                DateTime nextCommissionDate = DateTime.Now;
                string paymentInfo = "On Commission";

                if (agreement.CommissionRate.HasValue)
                {
                    paymentInfo = $"{agreement.CommissionRate.Value}% Commission";

                    DateTime lastCommDate = agreement.LastCommissionPaymentDate ?? agreement.StartDate;
                    int daysSinceLastPayment = (DateTime.Now - lastCommDate).Days;

                    if (agreement.PaymentFrequency.HasValue)
                    {
                        int daysPerPeriod = GetDaysFromFrequency(agreement.PaymentFrequency.Value, agreement.CustomPaymentDays);

                        if (daysSinceLastPayment >= daysPerPeriod)
                        {
                            // Estimate commission based on average sales
                            commissionDue = (agreement.CommissionRate.Value * 1000) / 30 * daysPerPeriod;
                            nextCommissionDate = lastCommDate.AddDays(daysPerPeriod);
                        }
                        else
                        {
                            nextCommissionDate = lastCommDate.AddDays(daysPerPeriod);
                        }
                    }
                }

                string statusColor = commissionDue > 0 ? "Orange" : "Green";

                return new RentOverview
                {
                    PropertyName = property.Name,
                    PortionName = portion.Name,
                    PortionSize = portion.Size,
                    TenantName = tenant.Name,
                    Mobile = tenant.Mobile,
                    MonthlyRent = 0,
                    LastPaidAmount = lastPayment?.Amount ?? 0,
                    DueAmount = commissionDue,
                    LastPaymentDate = lastPayment?.PaymentDate ?? DateTime.MinValue,
                    NextDueDate = nextCommissionDate,
                    AgreementId = agreement.Id,
                    TenantId = tenant.Id,
                    PropertyId = property.Id,
                    PortionId = portion.Id,
                    StatusColor = statusColor,
                    TenantType = tenant.Type,
                    CommissionDue = commissionDue,
                    NextCommissionDueDate = nextCommissionDate,
                    PaymentInfo = paymentInfo
                };
            }

            return null;
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

        public List<RentOverview> GetAllRentOverviews()
        {
            var agreements = LoadAgreements().Where(a => a.IsActive).ToList();
            var overviews = new List<RentOverview>();

            foreach (var agreement in agreements)
            {
                var overview = GetRentOverview(agreement);
                if (overview != null)
                {
                    overview.AgreementId = agreement.Id;
                    overview.TenantId = agreement.TenantId;
                    overview.PropertyId = agreement.PropertyId;
                    overview.PortionId = agreement.PortionId;
                    overviews.Add(overview);
                }
            }

            return overviews;
        }

        // Get all active products
        public List<Product> GetAllActiveProducts()
        {
            return LoadProducts().Where(p => p.IsActive).ToList();
        }

        // Get product selections for an agreement
        public List<ProductSelection> GetProductSelectionsForAgreement(int agreementId)
        {
            var agreement = LoadAgreements().FirstOrDefault(a => a.Id == agreementId);
            var allProducts = GetAllActiveProducts();
            var selectedIds = agreement?.ProductIds ?? new List<int>();

            return allProducts.Select(p => new ProductSelection
            {
                ProductId = p.Id,
                ProductName = p.Name,
                Unit = p.Unit,
                UnitPrice = p.UnitPrice,
                IsSelected = selectedIds.Contains(p.Id)
            }).ToList();
        }
        // Add this method to your existing JsonDataService class
        //public bool DeletePayment(int paymentId, string reason = "")
        //{
        //    try
        //    {
        //        var allPayments = LoadAllPayments();
        //        var payment = allPayments.FirstOrDefault(p => p.Id == paymentId);

        //        if (payment == null)
        //        {
        //            MessageBox.Show("Payment not found.", "Error",
        //                MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }

        //        if (payment.IsDeleted)
        //        {
        //            MessageBox.Show("Payment is already deleted.", "Information",
        //                MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return false;
        //        }

        //        // Ask for confirmation with payment details
        //        DialogResult result = MessageBox.Show(
        //            $"Are you sure you want to delete this payment?\n\n" +
        //            $"Date: {payment.PaymentDate:dd-MMM-yyyy}\n" +
        //            $"Type: {payment.PaymentType}\n" +
        //            $"Amount: {payment.Amount:C}\n" +
        //            $"Agreement ID: {payment.AgreementId}\n" +
        //            $"Notes: {payment.Notes}\n\n" +
        //            $"Reason for deletion:",
        //            "Confirm Payment Deletion",
        //            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        //        if (result != DialogResult.Yes) return false;

        //        // Soft delete the payment
        //        payment.IsDeleted = true;
        //        payment.DeletedDate = DateTime.Now;
        //        payment.Notes += $"\n[DELETED on {DateTime.Now:dd-MMM-yyyy HH:mm}] Reason: {reason}";

        //        SavePayments(allPayments);

        //        // Log the deletion
        //        LogDeletion(paymentId, reason);

        //        MessageBox.Show("Payment has been deleted successfully.", "Success",
        //            MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error deleting payment: {ex.Message}", "Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //}

        public bool RestorePayment(int paymentId)
        {
            try
            {
                var allPayments = LoadAllPayments();
                var payment = allPayments.FirstOrDefault(p => p.Id == paymentId);

                if (payment == null)
                {
                    MessageBox.Show("Payment not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (!payment.IsDeleted)
                {
                    MessageBox.Show("Payment is not deleted.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                // Ask for confirmation
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to restore this payment?\n\n" +
                    $"Date: {payment.PaymentDate:dd-MMM-yyyy}\n" +
                    $"Type: {payment.PaymentType}\n" +
                    $"Amount: {payment.Amount:C}",
                    "Confirm Payment Restoration",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return false;

                // Restore the payment
                payment.IsDeleted = false;
                payment.DeletedDate = null;

                // Remove the deletion note
                if (payment.Notes.Contains("[DELETED"))
                {
                    int deleteIndex = payment.Notes.IndexOf("[DELETED");
                    if (deleteIndex >= 0)
                    {
                        payment.Notes = payment.Notes.Substring(0, deleteIndex).Trim();
                    }
                }

                SavePayments(allPayments);

                MessageBox.Show("Payment has been restored successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error restoring payment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void LogDeletion(int paymentId, string reason)
        {
            try
            {
                string logPath = Path.Combine(_dataPath, "deletion_log.json");
                List<DeletionLog> logs = new List<DeletionLog>();

                if (File.Exists(logPath))
                {
                    var json = File.ReadAllText(logPath);
                    logs = JsonSerializer.Deserialize<List<DeletionLog>>(json) ?? new List<DeletionLog>();
                }

                logs.Add(new DeletionLog
                {
                    PaymentId = paymentId,
                    DeletedDate = DateTime.Now,
                    Reason = reason,
                    DeletedBy = Environment.UserName
                });

                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonOutput = JsonSerializer.Serialize(logs, options);
                File.WriteAllText(logPath, jsonOutput);
            }
            catch
            {
                // Silently fail if logging fails
            }
        }
        // Add to JsonDataService.cs

        // Expense Management Methods
        public List<Expense> LoadExpenses() => LoadData<Expense>("expenses.json");
        public void SaveExpenses(List<Expense> expenses) => SaveData("expenses.json", expenses);

        public List<Expense> LoadActiveExpenses()
        {
            var expenses = LoadData<Expense>("expenses.json");
            return expenses.Where(e => !e.IsDeleted).OrderByDescending(e => e.ExpenseDate).ToList();
        }

        public Expense GetExpenseById(int expenseId)
        {
            var expenses = LoadExpenses();
            return expenses.FirstOrDefault(e => e.Id == expenseId && !e.IsDeleted);
        }

        public int GetNextExpenseId()
        {
            var expenses = LoadExpenses();
            return expenses.Count > 0 ? expenses.Max(e => e.Id) + 1 : 1;
        }

        public string GenerateExpenseNumber()
        {
            var expenses = LoadExpenses();
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            // Count expenses for this month
            int countThisMonth = expenses.Count(e =>
                e.ExpenseDate.Year == year &&
                e.ExpenseDate.Month == month) + 1;

            return $"EXP-{year}-{month:D2}-{countThisMonth:D4}";
        }

        public bool AddExpense(Expense expense)
        {
            try
            {
                var expenses = LoadExpenses();
                expense.Id = GetNextExpenseId();
                expense.ExpenseNumber = GenerateExpenseNumber();
                expense.CreatedDate = DateTime.Now;
                expense.IsDeleted = false;

                expenses.Add(expense);
                SaveExpenses(expenses);

                // Log the expense addition
                LogExpenseAction("ADD", expense.Id, expense.Amount, expense.Category.ToString());

                return true;
            }
            catch (Exception ex)
            {
                log?.Error($"Error adding expense: {ex.Message}");
                return false;
            }
        }

        public bool UpdateExpense(Expense expense)
        {
            try
            {
                var expenses = LoadExpenses();
                var existing = expenses.FirstOrDefault(e => e.Id == expense.Id);

                if (existing == null || existing.IsDeleted)
                    return false;

                // Update fields
                existing.ExpenseDate = expense.ExpenseDate;
                existing.Category = expense.Category;
                existing.Amount = expense.Amount;
                existing.Payee = expense.Payee;
                existing.Description = expense.Description;
                existing.PaymentMethod = expense.PaymentMethod;
                existing.ReferenceNumber = expense.ReferenceNumber;
                existing.PropertyId = expense.PropertyId;
                existing.IsTaxDeductible = expense.IsTaxDeductible;
                existing.Notes = expense.Notes;
                existing.LastModifiedDate = DateTime.Now;

                SaveExpenses(expenses);
                LogExpenseAction("UPDATE", expense.Id, expense.Amount, expense.Category.ToString());

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteExpense(int expenseId, string reason = "")
        {
            try
            {
                var expenses = LoadExpenses();
                var expense = expenses.FirstOrDefault(e => e.Id == expenseId);

                if (expense == null || expense.IsDeleted)
                    return false;

                expense.IsDeleted = true;
                expense.DeletedDate = DateTime.Now;
                expense.Notes += $"\n[DELETED: {DateTime.Now:yyyy-MM-dd HH:mm:ss}] Reason: {reason}";

                SaveExpenses(expenses);
                LogExpenseAction("DELETE", expenseId, expense.Amount, reason);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void LogExpenseAction(string action, int expenseId, decimal amount, string details)
        {
            try
            {
                string logPath = Path.Combine(_dataPath, "expense_audit_log.json");
                List<ExpenseAuditLog> logs = new List<ExpenseAuditLog>();

                if (File.Exists(logPath))
                {
                    var json = File.ReadAllText(logPath);
                    logs = JsonSerializer.Deserialize<List<ExpenseAuditLog>>(json) ?? new List<ExpenseAuditLog>();
                }

                logs.Add(new ExpenseAuditLog
                {
                    ExpenseId = expenseId,
                    Action = action,
                    Amount = amount,
                    Details = details,
                    ActionDate = DateTime.Now,
                    ActionBy = Environment.UserName
                });

                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(logPath, JsonSerializer.Serialize(logs, options));
            }
            catch
            {
                // Silently fail if logging fails
            }
        }

        public List<Expense> FilterExpenses(ExpenseFilter filter)
        {
            var expenses = LoadActiveExpenses();

            if (filter.FromDate.HasValue)
                expenses = expenses.Where(e => e.ExpenseDate >= filter.FromDate.Value).ToList();

            if (filter.ToDate.HasValue)
                expenses = expenses.Where(e => e.ExpenseDate <= filter.ToDate.Value).ToList();

            if (filter.Category.HasValue)
                expenses = expenses.Where(e => e.Category == filter.Category.Value).ToList();

            if (filter.PropertyId.HasValue)
                expenses = expenses.Where(e => e.PropertyId == filter.PropertyId.Value).ToList();

            if (filter.MinAmount.HasValue)
                expenses = expenses.Where(e => e.Amount >= filter.MinAmount.Value).ToList();

            if (filter.MaxAmount.HasValue)
                expenses = expenses.Where(e => e.Amount <= filter.MaxAmount.Value).ToList();

            if (filter.IsTaxDeductible.HasValue)
                expenses = expenses.Where(e => e.IsTaxDeductible == filter.IsTaxDeductible.Value).ToList();

            if (!string.IsNullOrEmpty(filter.PaymentMethod))
                expenses = expenses.Where(e => e.PaymentMethod.Equals(filter.PaymentMethod, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                var search = filter.SearchText.ToLower();
                expenses = expenses.Where(e =>
                    (e.Payee?.ToLower() ?? "").Contains(search) ||
                    (e.Description?.ToLower() ?? "").Contains(search) ||
                    (e.Notes?.ToLower() ?? "").Contains(search) ||
                    (e.ReferenceNumber?.ToLower() ?? "").Contains(search) ||
                    e.ExpenseNumber.ToLower().Contains(search)).ToList();
            }

            return expenses.OrderByDescending(e => e.ExpenseDate).ToList();
        }

        public ExpenseReport GenerateExpenseReport(DateTime startDate, DateTime endDate)
        {
            var expenses = LoadActiveExpenses()
                .Where(e => e.ExpenseDate >= startDate && e.ExpenseDate <= endDate)
                .OrderBy(e => e.ExpenseDate)
                .ToList();

            var report = new ExpenseReport
            {
                ReportDate = DateTime.Now,
                StartDate = startDate,
                EndDate = endDate,
                Expenses = expenses,
                TotalExpenses = expenses.Sum(e => e.Amount),
                TotalTransactions = expenses.Count,
                AverageExpense = expenses.Count > 0 ? expenses.Average(e => e.Amount) : 0,
                LargestExpense = expenses.Count > 0 ? expenses.Max(e => e.Amount) : 0,
                SmallestExpense = expenses.Count > 0 ? expenses.Min(e => e.Amount) : 0
            };

            // Category summaries
            var categoryGroups = expenses.GroupBy(e => e.Category);
            foreach (var group in categoryGroups)
            {
                var total = group.Sum(e => e.Amount);
                report.CategorySummaries.Add(new ExpenseCategorySummary
                {
                    Category = group.Key,
                    CategoryName = GetCategoryDisplayName(group.Key),
                    TotalAmount = total,
                    TransactionCount = group.Count(),
                    AverageAmount = group.Average(e => e.Amount),
                    PercentageOfTotal = report.TotalExpenses > 0 ? (total / report.TotalExpenses) * 100 : 0
                });

                report.CategoryTotals[group.Key] = total;
            }

            // Monthly totals
            var monthGroups = expenses.GroupBy(e => e.ExpenseDate.ToString("yyyy-MM"));
            foreach (var group in monthGroups)
            {
                report.MonthlyTotals[group.Key] = group.Sum(e => e.Amount);
            }

            return report;
        }

        public ExpenseDashboardSummary GetExpenseDashboardSummary()
        {
            var allExpenses = LoadActiveExpenses();
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfYear = new DateTime(now.Year, 1, 1);
            var lastMonth = now.AddMonths(-1);
            var startOfLastMonth = new DateTime(lastMonth.Year, lastMonth.Month, 1);
            var endOfLastMonth = startOfLastMonth.AddMonths(1).AddDays(-1);

            var thisMonthExpenses = allExpenses.Where(e => e.ExpenseDate >= startOfMonth).ToList();
            var lastMonthExpenses = allExpenses.Where(e =>
                e.ExpenseDate >= startOfLastMonth &&
                e.ExpenseDate <= endOfLastMonth).ToList();
            var thisYearExpenses = allExpenses.Where(e => e.ExpenseDate >= startOfYear).ToList();

            var summary = new ExpenseDashboardSummary
            {
                TotalExpensesThisMonth = thisMonthExpenses.Sum(e => e.Amount),
                TotalExpensesThisYear = thisYearExpenses.Sum(e => e.Amount),
                ExpenseCountThisMonth = thisMonthExpenses.Count,
                LargestExpenseThisMonth = thisMonthExpenses.Count > 0 ? thisMonthExpenses.Max(e => e.Amount) : 0,
                RecentExpenses = allExpenses.OrderByDescending(e => e.ExpenseDate).Take(5).ToList()
            };

            // Average monthly expense (over last 6 months or available data)
            var last6Months = new List<DateTime>();
            for (int i = 5; i >= 0; i--)
            {
                last6Months.Add(now.AddMonths(-i));
            }

            var monthlyTotals = new List<decimal>();
            foreach (var month in last6Months)
            {
                var monthStart = new DateTime(month.Year, month.Month, 1);
                var monthEnd = monthStart.AddMonths(1).AddDays(-1);
                var total = allExpenses.Where(e => e.ExpenseDate >= monthStart && e.ExpenseDate <= monthEnd).Sum(e => e.Amount);
                summary.Last6MonthsTotals[month.ToString("MMM yyyy")] = total;
                monthlyTotals.Add(total);
            }

            summary.AverageMonthlyExpense = monthlyTotals.Count > 0 ? monthlyTotals.Average() : 0;

            // Month over month change
            decimal lastMonthTotal = lastMonthExpenses.Sum(e => e.Amount);
            decimal thisMonthTotal = thisMonthExpenses.Sum(e => e.Amount);

            if (lastMonthTotal > 0)
            {
                summary.MonthOverMonthChange = ((thisMonthTotal - lastMonthTotal) / lastMonthTotal) * 100;
            }

            // Category breakdown
            var categoryGroups = thisYearExpenses.GroupBy(e => e.Category);
            foreach (var group in categoryGroups)
            {
                summary.CategoryBreakdown[group.Key] = group.Sum(e => e.Amount);
            }

            // Most frequent category
            if (categoryGroups.Any())
            {
                summary.MostFrequentCategory = categoryGroups
                    .OrderByDescending(g => g.Count())
                    .First().Key;
            }

            return summary;
        }

        private string GetCategoryDisplayName(ExpenseCategory category)
        {
            // Return user-friendly category names
            return category switch
            {
                ExpenseCategory.Utilities => "Utilities (Electricity, Water, Gas)",
                ExpenseCategory.Maintenance => "Maintenance & Repairs",
                ExpenseCategory.Insurance => "Insurance Premiums",
                ExpenseCategory.Taxes => "Property Taxes",
                ExpenseCategory.Cleaning => "Cleaning Services",
                ExpenseCategory.Security => "Security Services",
                ExpenseCategory.Marketing => "Marketing & Advertising",
                ExpenseCategory.ProfessionalFees => "Professional Fees (Legal, Accounting)",
                ExpenseCategory.Supplies => "Office Supplies",
                ExpenseCategory.Salaries => "Salaries & Wages",
                ExpenseCategory.Equipment => "Equipment Purchase/Lease",
                _ => category.ToString()
            };
        }

        // Audit log class
        public class ExpenseAuditLog
        {
            public int ExpenseId { get; set; }
            public string Action { get; set; }
            public decimal Amount { get; set; }
            public string Details { get; set; }
            public DateTime ActionDate { get; set; }
            public string ActionBy { get; set; }
        }
        // Add this class inside JsonDataService namespace
        public class DeletionLog
        {
            public int PaymentId { get; set; }
            public DateTime DeletedDate { get; set; }
            public string Reason { get; set; }
            public string DeletedBy { get; set; }
        }

    }
}