// Add as a new service or add to existing JsonDataService
using System.Collections.Generic;
using System.Linq;
using System;
using ShopRentManagementSystem.Models;

namespace ShopRentManagementSystem.Services
{
    public class CashFlowService
    {
        private readonly JsonDataService _dataService;

        public CashFlowService()
        {
            _dataService = new JsonDataService();
        }

        public CashFlowSummary GetCashFlowSummary(DateRange dateRange)
        {
            var summary = new CashFlowSummary
            {
                StartDate = dateRange.StartDate,
                EndDate = dateRange.EndDate
            };

            // Get all payments (Cash In)
            var allPayments = _dataService.LoadAllPayments()
                .Where(p => !p.IsDeleted && p.PaymentDate >= dateRange.StartDate && p.PaymentDate <= dateRange.EndDate)
                .ToList();

            // Get all expenses (Cash Out)
            var allExpenses = _dataService.LoadActiveExpenses()
                .Where(e => e.ExpenseDate >= dateRange.StartDate && e.ExpenseDate <= dateRange.EndDate)
                .ToList();

            // Get tenants for reference
            var tenants = _dataService.LoadTenants();
            var properties = _dataService.LoadProperties();

            // Calculate totals
            summary.TotalCashIn = allPayments.Sum(p => p.Amount);
            summary.TotalCashOut = allExpenses.Sum(e => e.Amount);
            summary.NetCashFlow = summary.TotalCashIn - summary.TotalCashOut;
            summary.CashInTransactions = allPayments.Count;
            summary.CashOutTransactions = allExpenses.Count;

            // Group by Year
            var paymentsByYear = allPayments.GroupBy(p => p.PaymentDate.Year);
            var expensesByYear = allExpenses.GroupBy(e => e.ExpenseDate.Year);

            // Get all years in range
            var years = paymentsByYear.Select(g => g.Key)
                .Union(expensesByYear.Select(g => g.Key))
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();

            foreach (var year in years)
            {
                var yearPayments = allPayments.Where(p => p.PaymentDate.Year == year).ToList();
                var yearExpenses = allExpenses.Where(e => e.ExpenseDate.Year == year).ToList();

                var yearlyFlow = new YearlyCashFlow
                {
                    Year = year,
                    YearlyCashIn = yearPayments.Sum(p => p.Amount),
                    YearlyCashOut = yearExpenses.Sum(e => e.Amount),
                    YearlyNetCashFlow = yearPayments.Sum(p => p.Amount) - yearExpenses.Sum(e => e.Amount)
                };

                // Group by Month within the year
                for (int month = 1; month <= 12; month++)
                {
                    var monthPayments = yearPayments.Where(p => p.PaymentDate.Month == month).ToList();
                    var monthExpenses = yearExpenses.Where(e => e.ExpenseDate.Month == month).ToList();

                    if (monthPayments.Any() || monthExpenses.Any())
                    {
                        var monthlyFlow = new MonthlyCashFlow
                        {
                            Year = year,
                            Month = month,
                            MonthName = new DateTime(year, month, 1).ToString("MMMM"),
                            MonthlyCashIn = monthPayments.Sum(p => p.Amount),
                            MonthlyCashOut = monthExpenses.Sum(e => e.Amount),
                            MonthlyNetCashFlow = monthPayments.Sum(p => p.Amount) - monthExpenses.Sum(e => e.Amount),
                            CashInCount = monthPayments.Count,
                            CashOutCount = monthExpenses.Count
                        };

                        // Add payment details
                        foreach (var payment in monthPayments)
                        {
                            var tenant = tenants.FirstOrDefault(t =>
                                _dataService.LoadAgreements().Any(a => a.Id == payment.AgreementId && a.TenantId == t.Id));

                            monthlyFlow.CashInDetails.Add(new CashFlowDetail
                            {
                                Date = payment.PaymentDate,
                                TransactionType = "Cash In",
                                Category = payment.PaymentType.ToString(),
                                Description = $"Payment from {tenant?.Name ?? "Unknown"}",
                                Amount = payment.Amount,
                                Reference = $"Receipt #{payment.Id}",
                                RelatedParty = tenant?.Name ?? "Unknown Tenant"
                            });
                        }

                        // Add expense details
                        foreach (var expense in monthExpenses)
                        {
                            monthlyFlow.CashOutDetails.Add(new CashFlowDetail
                            {
                                Date = expense.ExpenseDate,
                                TransactionType = "Cash Out",
                                Category = expense.Category.ToString(),
                                Description = expense.Description,
                                Amount = expense.Amount,
                                Reference = expense.ExpenseNumber,
                                RelatedParty = expense.Payee
                            });
                        }

                        yearlyFlow.MonthlyData[month] = monthlyFlow;
                    }
                }

                summary.YearlyData[year] = yearlyFlow;
            }

            // Get recent transactions (last 10)
            var recentTransactions = new List<CashFlowDetail>();

            // Add recent payments
            recentTransactions.AddRange(allPayments.OrderByDescending(p => p.PaymentDate)
                .Take(10)
                .Select(p =>
                {
                    var tenant = tenants.FirstOrDefault(t =>
                        _dataService.LoadAgreements().Any(a => a.Id == p.AgreementId && a.TenantId == t.Id));
                    return new CashFlowDetail
                    {
                        Date = p.PaymentDate,
                        TransactionType = "Cash In",
                        Category = p.PaymentType.ToString(),
                        Description = $"Payment from {tenant?.Name ?? "Unknown"}",
                        Amount = p.Amount,
                        Reference = $"Receipt #{p.Id}",
                        RelatedParty = tenant?.Name ?? "Unknown Tenant"
                    };
                }));

            // Add recent expenses
            recentTransactions.AddRange(allExpenses.OrderByDescending(e => e.ExpenseDate)
                .Take(10)
                .Select(e => new CashFlowDetail
                {
                    Date = e.ExpenseDate,
                    TransactionType = "Cash Out",
                    Category = e.Category.ToString(),
                    Description = e.Description,
                    Amount = e.Amount,
                    Reference = e.ExpenseNumber,
                    RelatedParty = e.Payee
                }));

            summary.RecentTransactions = recentTransactions
                .OrderByDescending(t => t.Date)
                .Take(10)
                .ToList();

            return summary;
        }

        public List<DateRange> GetPredefinedDateRanges()
        {
            var now = DateTime.Now;
            return new List<DateRange>
                {
                    new DateRange(new DateTime(now.Year, 1, 1), now, "Year to Date"),
                    new DateRange(now.AddMonths(-1), now, "Last 30 Days"),
                    new DateRange(new DateTime(now.Year, now.Month, 1), now, "This Month"),
                    new DateRange(now.AddMonths(-3), now, "Last 3 Months"),
                    new DateRange(now.AddMonths(-6), now, "Last 6 Months"),
                    new DateRange(now.AddYears(-1), now, "Last 12 Months"),
                    new DateRange(new DateTime(now.Year - 1, 1, 1), new DateTime(now.Year - 1, 12, 31), "Previous Year")
                };
        }
    }

    // Extension method for DateRange
    public static class DateRangeExtensions
    {
        public static string DisplayText { get; set; }
    }
}