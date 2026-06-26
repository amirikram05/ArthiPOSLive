using System;
using System.Collections.Generic;
using System.Linq;
using ShopRentManagementSystem.Models;

namespace ShopRentManagementSystem.Services
{
    public class JsonReportService
    {
        private readonly JsonDataService _dataService;

        public JsonReportService()
        {
            _dataService = new JsonDataService();
        }

        public MonthlySummary GenerateMonthlySummary(int year, int month)
        {
            var summary = new MonthlySummary
            {
                MonthYear = new DateTime(year, month, 1).ToString("MMMM yyyy"),
                MonthStart = new DateTime(year, month, 1),
                MonthEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month))
            };

            var properties = _dataService.LoadProperties();
            var portions = _dataService.LoadPortions();
            var tenants = _dataService.LoadTenants();
            var agreements = _dataService.LoadAgreements().Where(a => a.IsActive).ToList();
            var payments = _dataService.LoadPayments();

            summary.TotalProperties = properties.Count;
            summary.TotalTenants = tenants.Count;
            summary.TotalRentTenants = tenants.Count(t => t.Type == TenantType.OnRent);
            summary.TotalCommissionTenants = tenants.Count(t => t.Type == TenantType.OnCommission);

            var monthPayments = payments.Where(p =>
                p.PaymentDate.Year == year &&
                p.PaymentDate.Month == month).ToList();

            summary.TotalRentCollected = monthPayments
                .Where(p => p.PaymentType == PaymentType.Rent)
                .Sum(p => p.Amount);

            summary.TotalCommissionCollected = monthPayments
                .Where(p => p.PaymentType == PaymentType.Commission)
                .Sum(p => p.Amount);

            summary.TotalCollected = summary.TotalRentCollected + summary.TotalCommissionCollected;

            foreach (var agreement in agreements)
            {
                var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);
                if (tenant == null) continue;

                if (tenant.Type == TenantType.OnRent)
                {
                    var rentPaid = monthPayments
                        .Any(p => p.AgreementId == agreement.Id && p.PaymentType == PaymentType.Rent);

                    if (!rentPaid)
                    {
                        summary.TotalRentDue += agreement.MonthlyRent;
                    }
                }
                else if (tenant.Type == TenantType.OnCommission &&
                         agreement.CommissionRate.HasValue &&
                         agreement.LastCommissionPaymentDate.HasValue)
                {
                    int daysSinceLastPayment = (summary.MonthEnd - agreement.LastCommissionPaymentDate.Value).Days;
                    int paymentFrequencyDays = GetDaysFromFrequency(agreement.PaymentFrequency ?? CommissionFrequency.Every10Days,
                                                                   agreement.CustomPaymentDays);

                    if (daysSinceLastPayment >= paymentFrequencyDays)
                    {
                        summary.TotalCommissionDue += agreement.CommissionRate.Value * 1000;
                    }
                }
            }

            summary.TotalDue = summary.TotalRentDue + summary.TotalCommissionDue;

            decimal totalExpected = summary.TotalCollected + summary.TotalDue;
            summary.CollectionEfficiency = totalExpected > 0 ?
                (summary.TotalCollected / totalExpected) * 100 : 100;

            foreach (var property in properties)
            {
                var propertySummary = new MonthlyPropertySummary
                {
                    PropertyName = property.Name,
                    PropertyType = property.Type
                };

                var propertyPortions = portions.Where(p => p.PropertyId == property.Id).ToList();
                propertySummary.TotalPortions = propertyPortions.Count;

                var occupiedPortionIds = agreements
                    .Where(a => a.PropertyId == property.Id)
                    .Select(a => a.PortionId)
                    .Distinct()
                    .ToList();

                propertySummary.OccupiedPortions = occupiedPortionIds.Count;
                propertySummary.OccupancyRate = propertySummary.TotalPortions > 0 ?
                    ((decimal)propertySummary.OccupiedPortions / propertySummary.TotalPortions) * 100 : 0;

                foreach (var agreement in agreements.Where(a => a.PropertyId == property.Id))
                {
                    var monthPropertyPayments = monthPayments
                        .Where(p => p.AgreementId == agreement.Id)
                        .ToList();

                    var rentPayment = monthPropertyPayments
                        .FirstOrDefault(p => p.PaymentType == PaymentType.Rent);

                    if (rentPayment != null)
                    {
                        propertySummary.TotalRentCollected += rentPayment.Amount;
                    }
                    else
                    {
                        propertySummary.TotalDue += agreement.MonthlyRent;
                    }

                    var commissionPayment = monthPropertyPayments
                        .FirstOrDefault(p => p.PaymentType == PaymentType.Commission);

                    if (commissionPayment != null)
                    {
                        propertySummary.TotalCommissionCollected += commissionPayment.Amount;
                    }
                }

                summary.PropertySummaries.Add(propertySummary);
            }

            return summary;
        }

        // In JsonReportService.cs, update the GenerateDueReport method
        public DueReport GenerateDueReport()
        {
            var report = new DueReport
            {
                ReportDate = DateTime.Now
            };

            var agreements = _dataService.LoadAgreements().Where(a => a.IsActive).ToList();
            var tenants = _dataService.LoadTenants();
            var properties = _dataService.LoadProperties();
            var portions = _dataService.LoadPortions();
            var payments = _dataService.LoadPayments();

            var dueTenants = new List<DueTenant>();

            foreach (var agreement in agreements)
            {
                var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);
                var property = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);
                var portion = portions.FirstOrDefault(p => p.Id == agreement.PortionId);

                if (tenant == null || property == null || portion == null) continue;

                decimal dueAmount = 0;
                DateTime nextDueDate = DateTime.MinValue;
                DateTime lastPaymentDate = DateTime.MinValue;
                int daysOverdue = 0;
                string status = "Current";

                if (tenant.Type == TenantType.OnRent)
                {
                    var rentPayments = payments
                        .Where(p => p.AgreementId == agreement.Id && p.PaymentType == PaymentType.Rent)
                        .OrderByDescending(p => p.PaymentDate)
                        .ToList();

                    lastPaymentDate = rentPayments.FirstOrDefault()?.PaymentDate ?? agreement.StartDate;

                    // Calculate months since start date
                    DateTime currentDate = DateTime.Now;
                    int monthsPassed = ((currentDate.Year - agreement.StartDate.Year) * 12) +
                                      currentDate.Month - agreement.StartDate.Month;

                    // Adjust if we haven't reached the same day of month
                    if (currentDate.Day < agreement.StartDate.Day)
                    {
                        monthsPassed--;
                    }

                    monthsPassed = Math.Max(0, monthsPassed);

                    decimal totalRentDue = monthsPassed * agreement.MonthlyRent;
                    decimal totalRentPaid = rentPayments.Sum(p => p.Amount);
                    dueAmount = Math.Max(0, totalRentDue - totalRentPaid);

                    nextDueDate = agreement.StartDate.AddMonths(monthsPassed + 1);

                    if (dueAmount > 0 && nextDueDate < DateTime.Now)
                    {
                        daysOverdue = (DateTime.Now - nextDueDate).Days;
                        status = "Overdue";
                    }
                    else if (dueAmount > 0)
                    {
                        daysOverdue = 0;
                        status = "Due Soon";
                    }
                }
                else if (tenant.Type == TenantType.OnCommission)
                {
                    var commissionPayments = payments
                        .Where(p => p.AgreementId == agreement.Id && p.PaymentType == PaymentType.Commission)
                        .OrderByDescending(p => p.PaymentDate)
                        .ToList();

                    lastPaymentDate = commissionPayments.FirstOrDefault()?.PaymentDate ??
                                     agreement.LastCommissionPaymentDate ?? agreement.StartDate;

                    // Check if commission calculation is possible
                    if (agreement.PaymentFrequency.HasValue &&
                        agreement.CommissionRate.HasValue &&
                        agreement.LastCommissionPaymentDate.HasValue)
                    {
                        int daysSinceLastPayment = (DateTime.Now - lastPaymentDate).Days;
                        int paymentFrequencyDays = GetDaysFromFrequency(agreement.PaymentFrequency.Value,
                                                                       agreement.CustomPaymentDays);

                        if (daysSinceLastPayment >= paymentFrequencyDays)
                        {
                            // Calculate estimated commission due
                            int periodsOverdue = daysSinceLastPayment / paymentFrequencyDays;
                            dueAmount = agreement.CommissionRate.Value * 1000 * periodsOverdue;
                            nextDueDate = lastPaymentDate.AddDays(paymentFrequencyDays);
                            daysOverdue = daysSinceLastPayment - paymentFrequencyDays;
                            status = daysOverdue > 0 ? "Overdue" : "Due";
                        }
                        else
                        {
                            nextDueDate = lastPaymentDate.AddDays(paymentFrequencyDays);
                            status = "Current";
                        }
                    }
                }

                if (dueAmount > 0 || status != "Current")
                {
                    var dueTenant = new DueTenant
                    {
                        TenantName = tenant.Name,
                        Mobile = tenant.Mobile,
                        PropertyName = property.Name,
                        PortionName = portion.Name,
                        TenantType = tenant.Type,
                        DueAmount = dueAmount,
                        LastPaymentDate = lastPaymentDate,
                        NextDueDate = nextDueDate,
                        DaysOverdue = daysOverdue,
                        Status = status
                    };

                    dueTenants.Add(dueTenant);
                }
            }

            report.DueTenants = dueTenants.OrderByDescending(d => d.DueAmount).ToList();
            report.TotalDueTenants = dueTenants.Count(d => d.DueAmount > 0);
            report.TotalDueAmount = dueTenants.Sum(d => d.DueAmount);

            var propertyGroups = dueTenants.Where(d => d.DueAmount > 0)
                                           .GroupBy(d => d.PropertyName);

            foreach (var group in propertyGroups)
            {
                var dueProperty = new DueProperty
                {
                    PropertyName = group.Key,
                    DueTenantsCount = group.Count(),
                    TotalDueAmount = group.Sum(d => d.DueAmount),
                    AverageDuePerTenant = group.Average(d => d.DueAmount)
                };
                report.DueProperties.Add(dueProperty);
            }

            return report;
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
        public class TenantPaymentHistory
        {
            public int TenantId { get; set; }
            public string TenantName { get; set; }
            public string PropertyName { get; set; }
            public string PortionName { get; set; }
            public List<PaymentTransaction> Transactions { get; set; } = new List<PaymentTransaction>();
            public decimal CurrentBalance { get; set; }
        }

        public class PaymentTransaction
        {
            public DateTime Date { get; set; }
            public string Type { get; set; } // "Rent", "Commission", "Payment"
            public string Description { get; set; }
            public decimal ChargeAmount { get; set; }
            public decimal PaymentAmount { get; set; }
            public decimal Balance { get; set; }
        }

        public TenantPaymentHistory GetTenantPaymentHistory(int tenantId)
        {
            var history = new TenantPaymentHistory();

            var agreements = _dataService.LoadAgreements()
                .Where(a => a.TenantId == tenantId && a.IsActive)
                .ToList();

            var tenant = _dataService.LoadTenants().FirstOrDefault(t => t.Id == tenantId);
            if (tenant == null || !agreements.Any()) return history;

            history.TenantId = tenantId;
            history.TenantName = tenant.Name;

            // Get the first active agreement for property info
            var firstAgreement = agreements.First();
            var property = _dataService.LoadProperties().FirstOrDefault(p => p.Id == firstAgreement.PropertyId);
            var portion = _dataService.LoadPortions().FirstOrDefault(p => p.Id == firstAgreement.PortionId);

            if (property != null) history.PropertyName = property.Name;
            if (portion != null) history.PortionName = portion.Name;

            // Get all payments for this tenant
            var allPayments = _dataService.LoadPayments()
                .Where(p => agreements.Select(a => a.Id).Contains(p.AgreementId))
                .OrderBy(p => p.PaymentDate)
                .ToList();

            decimal runningBalance = 0;
            List<PaymentTransaction> transactions = new List<PaymentTransaction>();

            // Add initial balance (security deposit or starting point)
            transactions.Add(new PaymentTransaction
            {
                Date = DateTime.Now.AddYears(-1), // Placeholder for initial state
                Type = "Initial",
                Description = "Starting Balance",
                ChargeAmount = 0,
                PaymentAmount = tenant.SecurityDeposit,
                Balance = tenant.SecurityDeposit
            });

            runningBalance = -tenant.SecurityDeposit; // Negative because it's credit

            if (tenant.Type == TenantType.OnRent)
            {
                foreach (var agreement in agreements)
                {
                    DateTime currentDate = agreement.StartDate;
                    DateTime endDate = DateTime.Now;

                    // Generate rent charges month by month
                    while (currentDate <= endDate)
                    {
                        runningBalance += agreement.MonthlyRent;

                        transactions.Add(new PaymentTransaction
                        {
                            Date = currentDate,
                            Type = "Rent",
                            Description = $"Monthly Rent - {currentDate:MMMM yyyy}",
                            ChargeAmount = agreement.MonthlyRent,
                            PaymentAmount = 0,
                            Balance = runningBalance
                        });

                        currentDate = currentDate.AddMonths(1);
                    }
                }
            }

            // Add actual payments
            foreach (var payment in allPayments)
            {
                runningBalance -= payment.Amount;

                string paymentType = payment.PaymentType switch
                {
                    PaymentType.Rent => "Rent Payment",
                    PaymentType.Commission => "Commission Payment",
                    PaymentType.SecurityDeposit => "Security Deposit",
                    _ => "Other Payment"
                };

                string description = paymentType;
                if (!string.IsNullOrEmpty(payment.Notes))
                    description += $" - {payment.Notes}";

                if (payment.PaymentType == PaymentType.Commission && payment.SalesAmount.HasValue)
                    description += $" (Sales: {payment.SalesAmount:C})";

                transactions.Add(new PaymentTransaction
                {
                    Date = payment.PaymentDate,
                    Type = "Payment",
                    Description = description,
                    ChargeAmount = 0,
                    PaymentAmount = payment.Amount,
                    Balance = runningBalance
                });
            }

            // Sort all transactions by date
            history.Transactions = transactions.OrderBy(t => t.Date).ToList();
            history.CurrentBalance = runningBalance;

            return history;
        }

        public List<TenantPaymentHistory> GetAllTenantsPaymentHistory()
        {
            var histories = new List<TenantPaymentHistory>();
            var tenants = _dataService.LoadTenants();

            foreach (var tenant in tenants)
            {
                var history = GetTenantPaymentHistory(tenant.Id);
                if (history.Transactions.Any())
                {
                    histories.Add(history);
                }
            }

            return histories.OrderBy(h => h.TenantName).ToList();
        }
    }
}