using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;

namespace ShopRentManagementSystem.Reports
{
    public class ReportGenerator
    {
        private readonly JsonDataService _dataService;
        private readonly string _reportDirectory;

        public ReportGenerator(JsonDataService dataService)
        {
            _dataService = dataService;
            _reportDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            Directory.CreateDirectory(_reportDirectory);
        }

        #region Main Report Generation Methods

        public string GenerateMonthlySummary(DateTime month)
        {
            try
            {
                var startDate = new DateTime(month.Year, month.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                var properties = _dataService.LoadProperties();
                var portions = _dataService.LoadPortions();
                var tenants = _dataService.LoadTenants();
                var agreements = _dataService.LoadAgreements().Where(a => a.IsActive).ToList();
                var payments = _dataService.LoadPayments()
                    .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                    .ToList();

                var summary = new MonthlySummary
                {
                    MonthYear = month.ToString("MMMM yyyy"),
                    MonthStart = startDate,
                    MonthEnd = endDate,
                    TotalProperties = properties.Count,
                    TotalTenants = tenants.Count,
                    TotalRentTenants = tenants.Count(t => t.Type == TenantType.OnRent),
                    TotalCommissionTenants = tenants.Count(t => t.Type == TenantType.OnCommission),
                    PropertySummaries = new List<MonthlyPropertySummary>()
                };

                // Calculate property-wise summaries
                foreach (var property in properties)
                {
                    var propertyPortions = portions.Where(p => p.PropertyId == property.Id).ToList();
                    var propertyAgreements = agreements.Where(a => a.PropertyId == property.Id).ToList();
                    var occupiedPortions = propertyAgreements.Count;

                    var propertyPayments = payments.Where(p =>
                        propertyAgreements.Any(a => a.Id == p.AgreementId)).ToList();

                    var rentCollected = propertyPayments
                        .Where(p => p.PaymentType == PaymentType.Rent)
                        .Sum(p => p.Amount);

                    var commissionCollected = propertyPayments
                        .Where(p => p.PaymentType == PaymentType.Commission)
                        .Sum(p => p.Amount);

                    var dueAmount = CalculatePropertyDue(propertyAgreements, property.Id);

                    summary.PropertySummaries.Add(new MonthlyPropertySummary
                    {
                        PropertyName = property.Name,
                        PropertyType = property.Type,
                        TotalPortions = propertyPortions.Count,
                        OccupiedPortions = occupiedPortions,
                        TotalRentCollected = rentCollected,
                        TotalCommissionCollected = commissionCollected,
                        TotalDue = dueAmount,
                        OccupancyRate = propertyPortions.Count > 0 ?
                            (occupiedPortions * 100m / propertyPortions.Count) : 0
                    });
                }

                // Calculate totals
                summary.TotalRentCollected = summary.PropertySummaries.Sum(p => p.TotalRentCollected);
                summary.TotalCommissionCollected = summary.PropertySummaries.Sum(p => p.TotalCommissionCollected);
                summary.TotalCollected = summary.TotalRentCollected + summary.TotalCommissionCollected;
                summary.TotalRentDue = summary.PropertySummaries.Sum(p => p.TotalDue);
                summary.TotalDue = summary.TotalRentDue;
                summary.CollectionEfficiency = summary.TotalCollected > 0 ?
                    (summary.TotalCollected * 100m / (summary.TotalCollected + summary.TotalDue)) : 0;

                return GenerateMonthlySummaryHtml(summary);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Monthly Summary Report", ex.Message);
            }
        }

        public string GenerateDueReport()
        {
            try
            {
                var overviews = _dataService.GetAllRentOverviews();
                var dueTenants = overviews.Where(o => o.DueAmount > 0).ToList();

                var report = new DueReport
                {
                    ReportDate = DateTime.Now,
                    TotalDueTenants = dueTenants.Count,
                    TotalDueAmount = dueTenants.Sum(d => d.DueAmount),
                    DueTenants = new List<DueTenant>(),
                    DueProperties = new List<DueProperty>()
                };

                // Create tenant due list
                foreach (var tenant in dueTenants)
                {
                    var status = GetDueStatus(tenant.DaysOverdue);
                    report.DueTenants.Add(new DueTenant
                    {
                        TenantName = tenant.TenantName,
                        Mobile = tenant.Mobile,
                        PropertyName = tenant.PropertyName,
                        PortionName = tenant.PortionName,
                        TenantType = tenant.TenantType,
                        DueAmount = tenant.DueAmount,
                        LastPaymentDate = tenant.LastPaymentDate,
                        NextDueDate = tenant.NextDueDate,
                        DaysOverdue = tenant.DaysOverdue,
                        Status = status
                    });
                }

                // Create property-wise summary
                var propertyGroups = dueTenants.GroupBy(t => t.PropertyName);
                foreach (var group in propertyGroups)
                {
                    report.DueProperties.Add(new DueProperty
                    {
                        PropertyName = group.Key,
                        DueTenantsCount = group.Count(),
                        TotalDueAmount = group.Sum(t => t.DueAmount),
                        AverageDuePerTenant = group.Average(t => t.DueAmount)
                    });
                }

                return GenerateDueReportHtml(report);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Due Report", ex.Message);
            }
        }

        public string GenerateTenantPaymentHistory(int tenantId)
        {
            try
            {
                var tenant = _dataService.LoadTenants().FirstOrDefault(t => t.Id == tenantId);
                if (tenant == null)
                    return GenerateErrorHtml("Tenant Payment History", "Tenant not found");

                var agreements = _dataService.LoadAgreements()
                    .Where(a => a.TenantId == tenantId && a.IsActive)
                    .ToList();

                var agreementIds = agreements.Select(a => a.Id).ToList();
                var payments = _dataService.LoadPayments()
                    .Where(p => agreementIds.Contains(p.AgreementId))
                    .OrderByDescending(p => p.PaymentDate)
                    .ToList();

                var properties = _dataService.LoadProperties();
                var portions = _dataService.LoadPortions();

                return GenerateTenantPaymentHistoryHtml(tenant, agreements, payments, properties, portions);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Tenant Payment History", ex.Message);
            }
        }

        public string GenerateTenantLedgerReport(int tenantId, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                startDate ??= DateTime.Now.AddMonths(-6);
                endDate ??= DateTime.Now;

                var tenant = _dataService.LoadTenants().FirstOrDefault(t => t.Id == tenantId);
                if (tenant == null)
                    return GenerateErrorHtml("Tenant Ledger Report", "Tenant not found");

                var agreements = _dataService.LoadAgreements()
                    .Where(a => a.TenantId == tenantId && a.IsActive)
                    .ToList();

                var agreementIds = agreements.Select(a => a.Id).ToList();
                var payments = _dataService.LoadPayments()
                    .Where(p => agreementIds.Contains(p.AgreementId) &&
                                p.PaymentDate >= startDate &&
                                p.PaymentDate <= endDate)
                    .OrderBy(p => p.PaymentDate)
                    .ToList();

                var ledger = CalculateTenantLedger(tenant, agreements, payments, startDate.Value);

                return GenerateTenantLedgerHtml(tenant, ledger, startDate.Value, endDate.Value);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Tenant Ledger Report", ex.Message);
            }
        }

        public string GenerateTenantDueStatement(int tenantId)
        {
            try
            {
                var tenant = _dataService.LoadTenants().FirstOrDefault(t => t.Id == tenantId);
                if (tenant == null)
                    return GenerateErrorHtml("Tenant Due Statement", "Tenant not found");

                var overviews = _dataService.GetAllRentOverviews()
                    .Where(o => o.TenantId == tenantId)
                    .ToList();

                return GenerateTenantDueStatementHtml(tenant, overviews);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Tenant Due Statement", ex.Message);
            }
        }

        public string GeneratePropertySummaryReport(int? propertyId = null)
        {
            try
            {
                var properties = propertyId.HasValue
                    ? _dataService.LoadProperties().Where(p => p.Id == propertyId.Value).ToList()
                    : _dataService.LoadProperties();

                var portions = _dataService.LoadPortions();
                var agreements = _dataService.LoadAgreements().Where(a => a.IsActive).ToList();
                var tenants = _dataService.LoadTenants();
                var payments = _dataService.LoadPayments()
                    .Where(p => p.PaymentDate.Year == DateTime.Now.Year)
                    .ToList();

                return GeneratePropertySummaryHtml(properties, portions, agreements, tenants, payments);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Property Summary Report", ex.Message);
            }
        }

        public string GenerateOccupancyReport()
        {
            try
            {
                var properties = _dataService.LoadProperties();
                var portions = _dataService.LoadPortions();
                var agreements = _dataService.LoadAgreements().Where(a => a.IsActive).ToList();
                var tenants = _dataService.LoadTenants();

                var occupancyData = new List<OccupancyReportData>();

                foreach (var property in properties)
                {
                    var propertyPortions = portions.Where(p => p.PropertyId == property.Id).ToList();
                    var propertyAgreements = agreements.Where(a => a.PropertyId == property.Id).ToList();

                    occupancyData.Add(new OccupancyReportData
                    {
                        PropertyName = property.Name,
                        PropertyType = property.Type,
                        TotalPortions = propertyPortions.Count,
                        OccupiedPortions = propertyAgreements.Count,
                        VacantPortions = propertyPortions.Count - propertyAgreements.Count,
                        OccupancyRate = propertyPortions.Count > 0 ?
                            (propertyAgreements.Count * 100m / propertyPortions.Count) : 0,
                        Portions = propertyPortions.Select(p => new PortionOccupancy
                        {
                            PortionName = p.Name,
                            PortionSize = p.Size,
                            IsOccupied = propertyAgreements.Any(a => a.PortionId == p.Id),
                            TenantName = propertyAgreements
                                .Where(a => a.PortionId == p.Id)
                                .Select(a => tenants.FirstOrDefault(t => t.Id == a.TenantId)?.Name ?? "N/A")
                                .FirstOrDefault()
                        }).ToList()
                    });
                }

                return GenerateOccupancyReportHtml(occupancyData);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Occupancy Report", ex.Message);
            }
        }

        public string GeneratePropertyIncomeReport(int? propertyId = null)
        {
            try
            {
                var properties = propertyId.HasValue
                    ? _dataService.LoadProperties().Where(p => p.Id == propertyId.Value).ToList()
                    : _dataService.LoadProperties();

                var agreements = _dataService.LoadAgreements().Where(a => a.IsActive).ToList();
                var tenants = _dataService.LoadTenants();
                var payments = _dataService.LoadPayments()
                    .Where(p => p.PaymentDate.Year == DateTime.Now.Year)
                    .ToList();

                return GeneratePropertyIncomeHtml(properties, agreements, tenants, payments);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Property Income Report", ex.Message);
            }
        }

        public string GenerateCollectionEfficiencyReport(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                startDate ??= DateTime.Now.AddMonths(-6);
                endDate ??= DateTime.Now;

                var agreements = _dataService.LoadAgreements().Where(a => a.IsActive).ToList();
                var payments = _dataService.LoadPayments()
                    .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                    .ToList();

                return GenerateCollectionEfficiencyHtml(agreements, payments, startDate.Value, endDate.Value);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Collection Efficiency Report", ex.Message);
            }
        }

        public string GenerateRevenueAnalysisReport(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                startDate ??= DateTime.Now.AddMonths(-12);
                endDate ??= DateTime.Now;

                var payments = _dataService.LoadPayments()
                    .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                    .ToList();

                var agreements = _dataService.LoadAgreements();
                var tenants = _dataService.LoadTenants();
                var properties = _dataService.LoadProperties();

                return GenerateRevenueAnalysisHtml(payments, agreements, tenants, properties, startDate.Value, endDate.Value);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Revenue Analysis Report", ex.Message);
            }
        }

        public string GenerateOutstandingDuesReport()
        {
            try
            {
                var overviews = _dataService.GetAllRentOverviews();
                var outstandingDues = overviews.Where(o => o.DueAmount > 0).ToList();

                return GenerateOutstandingDuesHtml(outstandingDues);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Outstanding Dues Report", ex.Message);
            }
        }

        public string GenerateCommissionSummaryReport(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                startDate ??= DateTime.Now.AddMonths(-1);
                endDate ??= DateTime.Now;

                var commissionPayments = _dataService.LoadPayments()
                    .Where(p => p.PaymentType == PaymentType.Commission &&
                               p.PaymentDate >= startDate &&
                               p.PaymentDate <= endDate)
                    .ToList();

                var commissionTransactions = _dataService.LoadCommissionTransactions()
                    .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                    .ToList();

                var agreements = _dataService.LoadAgreements();
                var tenants = _dataService.LoadTenants();
                var products = _dataService.LoadProducts();

                return GenerateCommissionSummaryHtml(commissionPayments, commissionTransactions,
                    agreements, tenants, products, startDate.Value, endDate.Value);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Commission Summary Report", ex.Message);
            }
        }

        public string GenerateProductSalesReport(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                startDate ??= DateTime.Now.AddMonths(-1);
                endDate ??= DateTime.Now;

                var commissionTransactions = _dataService.LoadCommissionTransactions()
                    .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                    .ToList();

                var products = _dataService.LoadProducts();
                var agreements = _dataService.LoadAgreements();
                var tenants = _dataService.LoadTenants();

                return GenerateProductSalesHtml(commissionTransactions, products, agreements, tenants,
                    startDate.Value, endDate.Value);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Product Sales Report", ex.Message);
            }
        }

        public string GenerateCommissionDueReport()
        {
            try
            {
                var overviews = _dataService.GetAllRentOverviews()
                    .Where(o => o.TenantType == TenantType.OnCommission && o.CommissionDue > 0)
                    .ToList();

                return GenerateCommissionDueHtml(overviews);
            }
            catch (Exception ex)
            {
                return GenerateErrorHtml("Commission Due Report", ex.Message);
            }
        }

        #endregion

        #region Helper Methods

        private decimal CalculatePropertyDue(List<RentAgreement> propertyAgreements, int propertyId)
        {
            var payments = _dataService.LoadPayments()
                .Where(p => propertyAgreements.Any(a => a.Id == p.AgreementId))
                .ToList();

            decimal totalDue = 0;

            foreach (var agreement in propertyAgreements)
            {
                var overview = _dataService.GetRentOverview(agreement);
                if (overview != null && overview.DueAmount > 0)
                {
                    totalDue += overview.DueAmount;
                }
            }

            return totalDue;
        }

        private string GetDueStatus(int daysOverdue)
        {
            if (daysOverdue <= 0) return "Current";
            if (daysOverdue <= 7) return "Due Soon";
            if (daysOverdue <= 30) return "Overdue";
            return "Severely Overdue";
        }

        private string GetStatusClass(string status)
        {
            return status.ToLower() switch
            {
                "current" => "success",
                "due soon" => "warning",
                "overdue" => "critical",
                "severely overdue" => "danger",
                _ => "info"
            };
        }

        private List<TenantLedgerEntry> CalculateTenantLedger(Tenant tenant, List<RentAgreement> agreements,
            List<Payment> payments, DateTime startDate)
        {
            var ledger = new List<TenantLedgerEntry>();
            decimal runningBalance = 0;

            // Add opening balance
            ledger.Add(new TenantLedgerEntry
            {
                Date = startDate,
                Description = "Opening Balance",
                CreditAmount = 0,
                DebitAmount = 0,
                Balance = 0,
                Type = "Opening"
            });

            // Add rent charges
            foreach (var agreement in agreements)
            {
                var month = startDate;
                while (month <= DateTime.Now)
                {
                    var overview = _dataService.GetRentOverview(agreement);
                    if (overview != null)
                    {
                        ledger.Add(new TenantLedgerEntry
                        {
                            Date = new DateTime(month.Year, month.Month, 1),
                            Description = $"Rent - {month:MMMM yyyy}",
                            CreditAmount = overview.MonthlyRent,
                            DebitAmount = 0,
                            Balance = runningBalance + overview.MonthlyRent,
                            Type = "Rent Charge"
                        });
                        runningBalance += overview.MonthlyRent;
                    }
                    month = month.AddMonths(1);
                }
            }

            // Add payments
            foreach (var payment in payments.OrderBy(p => p.PaymentDate))
            {
                runningBalance -= payment.Amount;
                ledger.Add(new TenantLedgerEntry
                {
                    Date = payment.PaymentDate,
                    Description = $"Payment - {payment.MonthYear}",
                    CreditAmount = 0,
                    DebitAmount = payment.Amount,
                    Balance = runningBalance,
                    Type = payment.PaymentType.ToString()
                });
            }

            return ledger.OrderBy(l => l.Date).ToList();
        }

        #endregion

        #region HTML Generation Methods

        private string GenerateMonthlySummaryHtml(MonthlySummary summary)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Monthly Summary Report - " + summary.MonthYear));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-chart-bar'></i> Monthly Summary Report</h1>");
            html.AppendLine($"<p class='period'>Period: {summary.MonthStart:dd MMM yyyy} to {summary.MonthEnd:dd MMM yyyy}</p>");
            html.AppendLine("</div>");

            // Summary Cards
            html.AppendLine("<div class='summary-cards'>");

            html.AppendLine("<div class='card'>");
            html.AppendLine("<div class='card-icon'><i class='fas fa-building'></i></div>");
            html.AppendLine("<div class='card-content'>");
            html.AppendLine($"<div class='card-value'>{summary.TotalProperties}</div>");
            html.AppendLine("<div class='card-label'>Properties</div>");
            html.AppendLine("</div></div>");

            html.AppendLine("<div class='card'>");
            html.AppendLine("<div class='card-icon'><i class='fas fa-users'></i></div>");
            html.AppendLine("<div class='card-content'>");
            html.AppendLine($"<div class='card-value'>{summary.TotalTenants}</div>");
            html.AppendLine("<div class='card-label'>Total Tenants</div>");
            html.AppendLine("</div></div>");

            html.AppendLine("<div class='card'>");
            html.AppendLine("<div class='card-icon'><i class='fas fa-money-bill-wave'></i></div>");
            html.AppendLine("<div class='card-content'>");
            html.AppendLine($"<div class='card-value'>{summary.TotalCollected:C}</div>");
            html.AppendLine("<div class='card-label'>Total Collected</div>");
            html.AppendLine("</div></div>");

            html.AppendLine("<div class='card'>");
            html.AppendLine("<div class='card-icon'><i class='fas fa-exclamation-triangle'></i></div>");
            html.AppendLine("<div class='card-content'>");
            html.AppendLine($"<div class='card-value'>{summary.TotalDue:C}</div>");
            html.AppendLine("<div class='card-label'>Total Due</div>");
            html.AppendLine("</div></div>");

            html.AppendLine("<div class='card'>");
            html.AppendLine("<div class='card-icon'><i class='fas fa-percentage'></i></div>");
            html.AppendLine("<div class='card-content'>");
            html.AppendLine($"<div class='card-value'>{summary.CollectionEfficiency:F1}%</div>");
            html.AppendLine("<div class='card-label'>Collection Efficiency</div>");
            html.AppendLine("</div></div>");

            html.AppendLine("</div>");

            // Tenant Type Breakdown
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-user-tag'></i> Tenant Type Breakdown</h2>");
            html.AppendLine("<div class='breakdown'>");

            html.AppendLine("<div class='breakdown-item'>");
            html.AppendLine("<div class='breakdown-label'>Rent Tenants</div>");
            html.AppendLine($"<div class='breakdown-value'>{summary.TotalRentTenants}</div>");
            html.AppendLine($"<div class='breakdown-amount'>{summary.TotalRentCollected:C}</div>");
            html.AppendLine("</div>");

            html.AppendLine("<div class='breakdown-item'>");
            html.AppendLine("<div class='breakdown-label'>Commission Tenants</div>");
            html.AppendLine($"<div class='breakdown-value'>{summary.TotalCommissionTenants}</div>");
            html.AppendLine($"<div class='breakdown-amount'>{summary.TotalCommissionCollected:C}</div>");
            html.AppendLine("</div>");

            html.AppendLine("</div></div>");

            // Property-wise Summary Table
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-th-list'></i> Property-wise Summary</h2>");
            html.AppendLine("<div class='table-container'>");
            html.AppendLine("<table class='data-table'>");
            html.AppendLine("<thead>");
            html.AppendLine("<tr>");
            html.AppendLine("<th>Property</th>");
            html.AppendLine("<th>Type</th>");
            html.AppendLine("<th>Portions</th>");
            html.AppendLine("<th>Occupied</th>");
            html.AppendLine("<th>Occupancy</th>");
            html.AppendLine("<th>Rent Collected</th>");
            html.AppendLine("<th>Commission</th>");
            html.AppendLine("<th>Due</th>");
            html.AppendLine("</tr>");
            html.AppendLine("</thead>");
            html.AppendLine("<tbody>");

            foreach (var property in summary.PropertySummaries.OrderByDescending(p => p.TotalRentCollected + p.TotalCommissionCollected))
            {
                html.AppendLine("<tr>");
                html.AppendLine($"<td>{property.PropertyName}</td>");
                html.AppendLine($"<td><span class='badge badge-{property.PropertyType.ToString().ToLower()}'>{property.PropertyType}</span></td>");
                html.AppendLine($"<td>{property.TotalPortions}</td>");
                html.AppendLine($"<td>{property.OccupiedPortions}</td>");
                html.AppendLine($"<td><span class='occupancy-badge'>{property.OccupancyRate:F1}%</span></td>");
                html.AppendLine($"<td class='amount positive'>{property.TotalRentCollected:C}</td>");
                html.AppendLine($"<td class='amount positive'>{property.TotalCommissionCollected:C}</td>");
                html.AppendLine($"<td class='amount negative'>{property.TotalDue:C}</td>");
                html.AppendLine("</tr>");
            }

            html.AppendLine("</tbody>");
            html.AppendLine("</table>");
            html.AppendLine("</div></div>");

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooter());

            return html.ToString();
        }

        private string GenerateDueReportHtml(DueReport report)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Due Report - " + report.ReportDate.ToString("dd MMM yyyy")));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-exclamation-triangle'></i> Due Report</h1>");
            html.AppendLine($"<p class='period'>As of: {report.ReportDate:dd MMM yyyy hh:mm tt}</p>");
            html.AppendLine("</div>");

            // Summary Cards
            html.AppendLine("<div class='summary-cards'>");

            html.AppendLine("<div class='card critical'>");
            html.AppendLine("<div class='card-icon'><i class='fas fa-users'></i></div>");
            html.AppendLine("<div class='card-content'>");
            html.AppendLine($"<div class='card-value'>{report.TotalDueTenants}</div>");
            html.AppendLine("<div class='card-label'>Tenants with Dues</div>");
            html.AppendLine("</div></div>");

            html.AppendLine("<div class='card critical'>");
            html.AppendLine("<div class='card-icon'><i class='fas fa-money-bill-wave'></i></div>");
            html.AppendLine("<div class='card-content'>");
            html.AppendLine($"<div class='card-value'>{report.TotalDueAmount:C}</div>");
            html.AppendLine("<div class='card-label'>Total Due Amount</div>");
            html.AppendLine("</div></div>");

            html.AppendLine("<div class='card'>");
            html.AppendLine("<div class='card-icon'><i class='fas fa-building'></i></div>");
            html.AppendLine("<div class='card-content'>");
            html.AppendLine($"<div class='card-value'>{report.DueProperties.Count}</div>");
            html.AppendLine("<div class='card-label'>Properties with Dues</div>");
            html.AppendLine("</div></div>");

            html.AppendLine("</div>");

            // Property-wise Due Summary
            if (report.DueProperties.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-building'></i> Property-wise Due Summary</h2>");
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Tenants with Dues</th>");
                html.AppendLine("<th>Total Due Amount</th>");
                html.AppendLine("<th>Average per Tenant</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var property in report.DueProperties.OrderByDescending(p => p.TotalDueAmount))
                {
                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{property.PropertyName}</td>");
                    html.AppendLine($"<td>{property.DueTenantsCount}</td>");
                    html.AppendLine($"<td class='amount negative'>{property.TotalDueAmount:C}</td>");
                    html.AppendLine($"<td class='amount negative'>{property.AverageDuePerTenant:C}</td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div></div>");
            }

            // Tenant Due Details
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-list'></i> Tenant Due Details</h2>");
            html.AppendLine("<div class='table-container'>");
            html.AppendLine("<table class='data-table'>");
            html.AppendLine("<thead>");
            html.AppendLine("<tr>");
            html.AppendLine("<th>Tenant</th>");
            html.AppendLine("<th>Contact</th>");
            html.AppendLine("<th>Property</th>");
            html.AppendLine("<th>Portion</th>");
            html.AppendLine("<th>Tenant Type</th>");
            html.AppendLine("<th>Due Amount</th>");
            html.AppendLine("<th>Last Payment</th>");
            html.AppendLine("<th>Next Due</th>");
            html.AppendLine("<th>Days Overdue</th>");
            html.AppendLine("<th>Status</th>");
            html.AppendLine("</tr>");
            html.AppendLine("</thead>");
            html.AppendLine("<tbody>");

            foreach (var tenant in report.DueTenants.OrderByDescending(t => t.DaysOverdue))
            {
                var statusClass = GetStatusClass(tenant.Status);
                html.AppendLine("<tr>");
                html.AppendLine($"<td>{tenant.TenantName}</td>");
                html.AppendLine($"<td>{tenant.Mobile}</td>");
                html.AppendLine($"<td>{tenant.PropertyName}</td>");
                html.AppendLine($"<td>{tenant.PortionName}</td>");
                html.AppendLine($"<td><span class='badge badge-{tenant.TenantType.ToString().ToLower()}'>{tenant.TenantType}</span></td>");
                html.AppendLine($"<td class='amount negative'>{tenant.DueAmount:C}</td>");
                html.AppendLine($"<td>{tenant.LastPaymentDate:dd MMM yyyy}</td>");
                html.AppendLine($"<td>{tenant.NextDueDate:dd MMM yyyy}</td>");
                html.AppendLine($"<td><span class='days-overdue'>{tenant.DaysOverdue} days</span></td>");
                html.AppendLine($"<td><span class='status-badge {statusClass}'>{tenant.Status}</span></td>");
                html.AppendLine("</tr>");
            }

            html.AppendLine("</tbody>");
            html.AppendLine("</table>");
            html.AppendLine("</div></div>");

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooter());

            return html.ToString();
        }

        // Other HTML generation methods follow similar patterns...

        #endregion

        #region HTML Template Methods

        private string CreateHtmlHeader(string title)
        {
            return $@"<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>{title} - ShopRent System</title>
    <link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css'>
    <style>
        * {{ box-sizing: border-box; margin: 0; padding: 0; }}
        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background: #f5f7fa; color: #333; line-height: 1.6; padding: 20px; }}
        
        .report-container {{ max-width: 1400px; margin: 0 auto; background: white; border-radius: 15px; box-shadow: 0 10px 30px rgba(0,0,0,0.1); overflow: hidden; }}
        
        .report-header {{ background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px; }}
        .report-header h1 {{ font-size: 2.2em; margin-bottom: 10px; display: flex; align-items: center; gap: 15px; }}
        .report-header .period {{ opacity: 0.9; font-size: 1.1em; }}
        
        .tenant-info {{ background: rgba(255,255,255,0.1); padding: 20px; border-radius: 10px; margin-top: 20px; display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 15px; }}
        .tenant-info div {{ display: flex; flex-direction: column; }}
        .tenant-info strong {{ margin-bottom: 5px; opacity: 0.9; }}
        
        .summary-cards {{ display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 20px; padding: 30px; }}
        .summary-cards.mini {{ grid-template-columns: repeat(auto-fit, minmax(150px, 1fr)); padding: 20px; }}
        .card {{ background: white; border-radius: 12px; padding: 25px; box-shadow: 0 5px 15px rgba(0,0,0,0.08); text-align: center; transition: transform 0.3s; border: 2px solid transparent; }}
        .card:hover {{ transform: translateY(-5px); }}
        .card.critical {{ border-color: #ff4757; }}
        .card.critical .card-icon {{ background: #ff4757; }}
        .card .card-icon {{ width: 60px; height: 60px; border-radius: 50%; background: #667eea; color: white; display: flex; align-items: center; justify-content: center; font-size: 1.8em; margin: 0 auto 15px; }}
        .card .card-value {{ font-size: 2.5em; font-weight: bold; color: #2c3e50; margin-bottom: 5px; }}
        .card .card-label {{ color: #7f8c8d; font-size: 0.95em; }}
        
        .section {{ padding: 20px 30px; border-bottom: 1px solid #eee; }}
        .section:last-child {{ border-bottom: none; }}
        .section h2 {{ color: #2c3e50; margin-bottom: 20px; display: flex; align-items: center; gap: 10px; font-size: 1.4em; }}
        
        .table-container {{ overflow-x: auto; }}
        .data-table {{ width: 100%; border-collapse: collapse; }}
        .data-table th {{ background: #f8f9fa; padding: 15px; text-align: left; font-weight: 600; color: #495057; border-bottom: 2px solid #dee2e6; }}
        .data-table td {{ padding: 12px 15px; border-bottom: 1px solid #eee; }}
        .data-table tr:hover {{ background: #f8f9fa; }}
        .data-table .amount {{ font-weight: 600; text-align: right; }}
        .data-table .amount.positive {{ color: #27ae60; }}
        .data-table .amount.negative {{ color: #e74c3c; }}
        
        .badge {{ padding: 5px 12px; border-radius: 20px; font-size: 0.85em; font-weight: bold; display: inline-block; }}
        .badge-onrent {{ background: #e3f2fd; color: #1565c0; }}
        .badge-oncommission {{ background: #f3e5f5; color: #7b1fa2; }}
        .badge-commercial {{ background: #e8f5e9; color: #2e7d32; }}
        .badge-noncommercial {{ background: #fff3e0; color: #f57c00; }}
        .badge-rent {{ background: #bbdefb; color: #0d47a1; }}
        .badge-commission {{ background: #e1bee7; color: #4a148c; }}
        
        .status-badge {{ padding: 5px 15px; border-radius: 20px; font-size: 0.85em; font-weight: bold; }}
        .status-badge.success {{ background: #d4edda; color: #155724; }}
        .status-badge.warning {{ background: #fff3cd; color: #856404; }}
        .status-badge.critical {{ background: #f8d7da; color: #721c24; }}
        .status-badge.danger {{ background: #f5c6cb; color: #491217; }}
        
        .days-overdue {{ color: #e74c3c; font-weight: bold; }}
        
        .breakdown {{ display: grid; grid-template-columns: repeat(auto-fit, minmax(250px, 1fr)); gap: 20px; }}
        .breakdown-item {{ background: #f8f9fa; padding: 20px; border-radius: 10px; }}
        .breakdown-label {{ color: #6c757d; font-size: 0.9em; margin-bottom: 5px; }}
        .breakdown-value {{ font-size: 2em; font-weight: bold; color: #2c3e50; }}
        .breakdown-amount {{ color: #27ae60; font-weight: 600; }}
        
        .occupancy-badge {{ padding: 3px 10px; border-radius: 15px; background: #e3f2fd; color: #1565c0; font-weight: 600; }}
        
        .no-data {{ text-align: center; padding: 40px; color: #6c757d; font-style: italic; }}
        
        .report-footer {{ text-align: center; padding: 20px; color: #6c757d; font-size: 0.9em; border-top: 1px solid #eee; }}
        
        .chart-container {{ height: 300px; margin: 20px 0; }}
        
        @media print {{
            body {{ padding: 0; }}
            .report-container {{ box-shadow: none; border-radius: 0; }}
            .summary-cards {{ break-inside: avoid; }}
            .section {{ break-inside: avoid; }}
        }}
    </style>
    <script src='https://cdn.jsdelivr.net/npm/chart.js'></script>
</head>
<body>";
        }

        private string CreateHtmlFooter()
        {
            return @"<div class='report-footer'>
    <p>Generated by ShopRent Management System on " + DateTime.Now.ToString("dd MMM yyyy hh:mm tt") + @"</p>
    <div class='print-controls' style='margin-top: 10px;'>
        <button onclick='window.print()' style='padding: 10px 20px; background: #667eea; color: white; border: none; border-radius: 5px; cursor: pointer; margin: 0 5px;'>
            <i class='fas fa-print'></i> Print Report
        </button>
        <button onclick='exportToPDF()' style='padding: 10px 20px; background: #e74c3c; color: white; border: none; border-radius: 5px; cursor: pointer; margin: 0 5px;'>
            <i class='fas fa-file-pdf'></i> Export PDF
        </button>
        <button onclick='exportToExcel()' style='padding: 10px 20px; background: #2ecc71; color: white; border: none; border-radius: 5px; cursor: pointer; margin: 0 5px;'>
            <i class='fas fa-file-excel'></i> Export Excel
        </button>
    </div>
</div>
<script>
    function exportToPDF() {{
        alert('PDF export feature would be implemented here.\\nFor now, use Print and save as PDF.');
    }}
    
    function exportToExcel() {{
        alert('Excel export feature would be implemented here.');
    }}
</script>
</body>
</html>";
        }

        private string GenerateErrorHtml(string reportName, string errorMessage)
        {
            return $@"<!DOCTYPE html>
<html>
<head>
    <title>Error - {reportName}</title>
    <style>
        body {{ font-family: Arial, sans-serif; padding: 40px; text-align: center; background: #f5f7fa; }}
        .error-container {{ max-width: 600px; margin: 50px auto; padding: 40px; background: #f8d7da; border-radius: 10px; color: #721c24; box-shadow: 0 5px 15px rgba(0,0,0,0.1); }}
        h1 {{ color: #721c24; margin-bottom: 20px; display: flex; align-items: center; justify-content: center; gap: 10px; }}
        h3 {{ margin-bottom: 20px; color: #491217; }}
        .btn {{ padding: 10px 30px; background: #667eea; color: white; border: none; border-radius: 5px; cursor: pointer; margin-top: 20px; font-size: 16px; }}
        .btn:hover {{ background: #764ba2; }}
    </style>
    <link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css'>
</head>
<body>
    <div class='error-container'>
        <h1><i class='fas fa-exclamation-circle'></i> Report Generation Failed</h1>
        <h3>{reportName}</h3>
        <p style='font-size: 16px; margin-bottom: 20px;'>Error: {errorMessage}</p>
        <button class='btn' onclick='window.history.back()'><i class='fas fa-arrow-left'></i> Go Back</button>
    </div>
</body>
</html>";
        }
        #region HTML Generation Methods

        private string GenerateTenantPaymentHistoryHtml(Tenant tenant, List<RentAgreement> agreements,
            List<Payment> payments, List<Property> properties, List<Portion> portions)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Tenant Payment History - " + tenant.Name));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-history'></i> Tenant Payment History</h1>");

            // Tenant Info
            html.AppendLine("<div class='tenant-info'>");
            html.AppendLine($"<div><strong>Name:</strong> {tenant.Name}</div>");
            html.AppendLine($"<div><strong>CNIC:</strong> {tenant.CNIC}</div>");
            html.AppendLine($"<div><strong>Mobile:</strong> {tenant.Mobile}</div>");
            html.AppendLine($"<div><strong>Type:</strong> <span class='badge badge-{tenant.Type.ToString().ToLower()}'>{tenant.Type}</span></div>");
            if (tenant.Type == TenantType.OnCommission)
            {
                html.AppendLine($"<div><strong>Commission %:</strong> {tenant.CommissionPercentage}%</div>");
            }
            html.AppendLine("</div>");
            html.AppendLine("</div>");

            // Agreements Summary
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-file-contract'></i> Current Agreements</h2>");

            if (agreements.Any())
            {
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Portion</th>");
                html.AppendLine("<th>Monthly Rent</th>");
                html.AppendLine("<th>Start Date</th>");
                html.AppendLine("<th>Next Due</th>");
                html.AppendLine("<th>Status</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var agreement in agreements)
                {
                    var property = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);
                    var portion = portions.FirstOrDefault(p => p.Id == agreement.PortionId);
                    var overview = _dataService.GetRentOverview(agreement);
                    var status = overview?.DueAmount > 0 ? "Overdue" : "Active";

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{property?.Name ?? "N/A"}</td>");
                    html.AppendLine($"<td>{portion?.Name ?? "N/A"}</td>");
                    html.AppendLine($"<td class='amount'>{agreement.MonthlyRent:C}</td>");
                    html.AppendLine($"<td>{agreement.StartDate:dd MMM yyyy}</td>");
                    html.AppendLine($"<td>{overview?.NextDueDate:dd MMM yyyy}</td>");
                    html.AppendLine($"<td><span class='status-badge {(status == "Overdue" ? "critical" : "success")}'>{status}</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div>");
            }
            else
            {
                html.AppendLine("<p class='no-data'>No active agreements found.</p>");
            }
            html.AppendLine("</div>");

            // Payment History
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-money-check-alt'></i> Payment History</h2>");

            if (payments.Any())
            {
                var totalPaid = payments.Sum(p => p.Amount);

                html.AppendLine("<div class='summary-cards mini'>");
                html.AppendLine($"<div class='card'><div class='card-value'>{payments.Count}</div><div class='card-label'>Total Payments</div></div>");
                html.AppendLine($"<div class='card'><div class='card-value'>{totalPaid:C}</div><div class='card-label'>Total Paid</div></div>");
                html.AppendLine("</div>");

                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Date</th>");
                html.AppendLine("<th>Amount</th>");
                html.AppendLine("<th>Month/Year</th>");
                html.AppendLine("<th>Payment Type</th>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Notes</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var payment in payments)
                {
                    var agreement = agreements.FirstOrDefault(a => a.Id == payment.AgreementId);
                    var property = agreement != null ? properties.FirstOrDefault(p => p.Id == agreement.PropertyId) : null;

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{payment.PaymentDate:dd MMM yyyy}</td>");
                    html.AppendLine($"<td class='amount positive'>{payment.Amount:C}</td>");
                    html.AppendLine($"<td>{payment.MonthYear}</td>");
                    html.AppendLine($"<td><span class='badge badge-{payment.PaymentType.ToString().ToLower()}'>{payment.PaymentType}</span></td>");
                    html.AppendLine($"<td>{property?.Name ?? "N/A"}</td>");
                    html.AppendLine($"<td>{payment.Notes}</td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div>");
            }
            else
            {
                html.AppendLine("<p class='no-data'>No payment history found.</p>");
            }
            html.AppendLine("</div>");

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooter());

            return html.ToString();
        }

        private string GenerateTenantLedgerHtml(Tenant tenant, List<TenantLedgerEntry> ledger, DateTime startDate, DateTime endDate)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Tenant Ledger - " + tenant.Name));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-book'></i> Tenant Ledger</h1>");
            html.AppendLine($"<p class='period'>Period: {startDate:dd MMM yyyy} to {endDate:dd MMM yyyy}</p>");

            // Tenant Info
            html.AppendLine("<div class='tenant-info'>");
            html.AppendLine($"<div><strong>Name:</strong> {tenant.Name}</div>");
            html.AppendLine($"<div><strong>Mobile:</strong> {tenant.Mobile}</div>");
            html.AppendLine($"<div><strong>Type:</strong> <span class='badge badge-{tenant.Type.ToString().ToLower()}'>{tenant.Type}</span></div>");
            html.AppendLine($"<div><strong>Opening Balance:</strong> {ledger.FirstOrDefault()?.Balance:C}</div>");
            html.AppendLine($"<div><strong>Closing Balance:</strong> {ledger.LastOrDefault()?.Balance:C}</div>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");

            // Ledger Table
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-list'></i> Ledger Entries</h2>");

            if (ledger.Any())
            {
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Date</th>");
                html.AppendLine("<th>Description</th>");
                html.AppendLine("<th>Type</th>");
                html.AppendLine("<th>Credit</th>");
                html.AppendLine("<th>Debit</th>");
                html.AppendLine("<th>Balance</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var entry in ledger)
                {
                    var balanceClass = entry.Balance >= 0 ? "positive" : "negative";

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{entry.Date:dd MMM yyyy}</td>");
                    html.AppendLine($"<td>{entry.Description}</td>");
                    html.AppendLine($"<td><span class='badge'>{entry.Type}</span></td>");
                    html.AppendLine($"<td class='amount negative'>{entry.CreditAmount:C}</td>");
                    html.AppendLine($"<td class='amount positive'>{entry.DebitAmount:C}</td>");
                    html.AppendLine($"<td class='amount {balanceClass}'>{entry.Balance:C}</td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div>");
            }
            else
            {
                html.AppendLine("<p class='no-data'>No ledger entries found for this period.</p>");
            }
            html.AppendLine("</div>");

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooter());

            return html.ToString();
        }

        private string GenerateTenantDueStatementHtml(Tenant tenant, List<RentOverview> overviews)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Tenant Due Statement - " + tenant.Name));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-file-invoice-dollar'></i> Tenant Due Statement</h1>");
            html.AppendLine($"<p class='period'>As of: {DateTime.Now:dd MMM yyyy}</p>");

            // Tenant Info
            html.AppendLine("<div class='tenant-info'>");
            html.AppendLine($"<div><strong>Name:</strong> {tenant.Name}</div>");
            html.AppendLine($"<div><strong>CNIC:</strong> {tenant.CNIC}</div>");
            html.AppendLine($"<div><strong>Mobile:</strong> {tenant.Mobile}</div>");
            html.AppendLine($"<div><strong>Type:</strong> <span class='badge badge-{tenant.Type.ToString().ToLower()}'>{tenant.Type}</span></div>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");

            // Due Summary
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-exclamation-circle'></i> Due Summary</h2>");

            if (overviews.Any())
            {
                var totalDue = overviews.Sum(o => o.DueAmount);
                var overdueCount = overviews.Count(o => o.DaysOverdue > 0);

                html.AppendLine("<div class='summary-cards'>");
                html.AppendLine($"<div class='card critical'><div class='card-value'>{overviews.Count}</div><div class='card-label'>Agreements</div></div>");
                html.AppendLine($"<div class='card critical'><div class='card-value'>{totalDue:C}</div><div class='card-label'>Total Due</div></div>");
                html.AppendLine($"<div class='card warning'><div class='card-value'>{overdueCount}</div><div class='card-label'>Overdue</div></div>");
                html.AppendLine("</div>");

                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Portion</th>");
                html.AppendLine("<th>Monthly Rent</th>");
                html.AppendLine("<th>Due Amount</th>");
                html.AppendLine("<th>Last Payment</th>");
                html.AppendLine("<th>Next Due</th>");
                html.AppendLine("<th>Days Overdue</th>");
                html.AppendLine("<th>Status</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var overview in overviews.OrderByDescending(o => o.DaysOverdue))
                {
                    var statusClass = overview.DaysOverdue > 30 ? "danger" :
                                    overview.DaysOverdue > 7 ? "critical" :
                                    overview.DaysOverdue > 0 ? "warning" : "success";
                    var statusText = overview.DaysOverdue > 30 ? "Severely Overdue" :
                                   overview.DaysOverdue > 7 ? "Overdue" :
                                   overview.DaysOverdue > 0 ? "Due Soon" : "Current";

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{overview.PropertyName}</td>");
                    html.AppendLine($"<td>{overview.PortionName}</td>");
                    html.AppendLine($"<td class='amount'>{overview.MonthlyRent:C}</td>");
                    html.AppendLine($"<td class='amount negative'>{overview.DueAmount:C}</td>");
                    html.AppendLine($"<td>{overview.LastPaymentDate:dd MMM yyyy}</td>");
                    html.AppendLine($"<td>{overview.NextDueDate:dd MMM yyyy}</td>");
                    html.AppendLine($"<td><span class='days-overdue'>{overview.DaysOverdue} days</span></td>");
                    html.AppendLine($"<td><span class='status-badge {statusClass}'>{statusText}</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div>");
            }
            else
            {
                html.AppendLine("<p class='no-data'>No dues found for this tenant.</p>");
            }
            html.AppendLine("</div>");

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooter());

            return html.ToString();
        }

        private string GeneratePropertySummaryHtml(List<Property> properties, List<Portion> portions,
            List<RentAgreement> agreements, List<Tenant> tenants, List<Payment> payments)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Property Summary Report"));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-building'></i> Property Summary Report</h1>");
            html.AppendLine($"<p class='period'>As of: {DateTime.Now:dd MMM yyyy}</p>");
            html.AppendLine("</div>");

            // Summary Cards
            html.AppendLine("<div class='summary-cards'>");

            html.AppendLine($"<div class='card'><div class='card-value'>{properties.Count}</div><div class='card-label'>Total Properties</div></div>");

            var totalPortions = portions.Count;
            var occupiedPortions = agreements.Count;
            var occupancyRate = totalPortions > 0 ? (occupiedPortions * 100m / totalPortions) : 0;

            html.AppendLine($"<div class='card'><div class='card-value'>{totalPortions}</div><div class='card-label'>Total Portions</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{occupiedPortions}</div><div class='card-label'>Occupied Portions</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{occupancyRate:F1}%</div><div class='card-label'>Occupancy Rate</div></div>");

            var currentYearRevenue = payments.Sum(p => p.Amount);
            html.AppendLine($"<div class='card'><div class='card-value'>{currentYearRevenue:C}</div><div class='card-label'>YTD Revenue</div></div>");

            html.AppendLine("</div>");

            // Property Details Table
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-th-list'></i> Property Details</h2>");

            if (properties.Any())
            {
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Type</th>");
                html.AppendLine("<th>Address</th>");
                html.AppendLine("<th>Portions</th>");
                html.AppendLine("<th>Occupied</th>");
                html.AppendLine("<th>Vacant</th>");
                html.AppendLine("<th>Occupancy</th>");
                html.AppendLine("<th>YTD Revenue</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var property in properties)
                {
                    var propertyPortions = portions.Where(p => p.PropertyId == property.Id).ToList();
                    var propertyAgreements = agreements.Where(a => a.PropertyId == property.Id).ToList();
                    var propertyPayments = payments.Where(p =>
                        propertyAgreements.Any(a => a.Id == p.AgreementId)).ToList();

                    var occupied = propertyAgreements.Count;
                    var vacant = propertyPortions.Count - occupied;
                    var occupancy = propertyPortions.Count > 0 ? (occupied * 100m / propertyPortions.Count) : 0;
                    var revenue = propertyPayments.Sum(p => p.Amount);

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{property.Name}</td>");
                    html.AppendLine($"<td><span class='badge badge-{property.Type.ToString().ToLower()}'>{property.Type}</span></td>");
                    html.AppendLine($"<td>{property.Address}</td>");
                    html.AppendLine($"<td>{propertyPortions.Count}</td>");
                    html.AppendLine($"<td>{occupied}</td>");
                    html.AppendLine($"<td>{vacant}</td>");
                    html.AppendLine($"<td><span class='occupancy-badge'>{occupancy:F1}%</span></td>");
                    html.AppendLine($"<td class='amount positive'>{revenue:C}</td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div>");
            }
            else
            {
                html.AppendLine("<p class='no-data'>No properties found.</p>");
            }
            html.AppendLine("</div>");

            // Property Type Breakdown
            var propertyTypes = properties.GroupBy(p => p.Type);
            if (propertyTypes.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-chart-pie'></i> Property Type Breakdown</h2>");
                html.AppendLine("<div class='breakdown'>");

                foreach (var typeGroup in propertyTypes)
                {
                    var typeProperties = typeGroup.ToList();
                    var typePortions = portions.Where(p => typeProperties.Any(tp => tp.Id == p.PropertyId)).Count();
                    var typeOccupied = agreements.Where(a => typeProperties.Any(tp => tp.Id == a.PropertyId)).Count();

                    html.AppendLine("<div class='breakdown-item'>");
                    html.AppendLine($"<div class='breakdown-label'>{typeGroup.Key} Properties</div>");
                    html.AppendLine($"<div class='breakdown-value'>{typeGroup.Count()}</div>");
                    html.AppendLine($"<div class='breakdown-amount'>{typeOccupied}/{typePortions} occupied</div>");
                    html.AppendLine("</div>");
                }

                html.AppendLine("</div></div>");
            }

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooter());

            return html.ToString();
        }

        private string GenerateOccupancyReportHtml(List<OccupancyReportData> occupancyData)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Occupancy Report"));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-bed'></i> Occupancy Report</h1>");
            html.AppendLine($"<p class='period'>As of: {DateTime.Now:dd MMM yyyy}</p>");
            html.AppendLine("</div>");

            // Summary Cards
            var totalPortions = occupancyData.Sum(d => d.TotalPortions);
            var occupiedPortions = occupancyData.Sum(d => d.OccupiedPortions);
            var overallOccupancy = totalPortions > 0 ? (occupiedPortions * 100m / totalPortions) : 0;

            html.AppendLine("<div class='summary-cards'>");
            html.AppendLine($"<div class='card'><div class='card-value'>{occupancyData.Count}</div><div class='card-label'>Properties</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{totalPortions}</div><div class='card-label'>Total Portions</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{occupiedPortions}</div><div class='card-label'>Occupied</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{totalPortions - occupiedPortions}</div><div class='card-label'>Vacant</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{overallOccupancy:F1}%</div><div class='card-label'>Overall Occupancy</div></div>");
            html.AppendLine("</div>");

            // Property Occupancy Table
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-building'></i> Property Occupancy Details</h2>");

            if (occupancyData.Any())
            {
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Type</th>");
                html.AppendLine("<th>Total Portions</th>");
                html.AppendLine("<th>Occupied</th>");
                html.AppendLine("<th>Vacant</th>");
                html.AppendLine("<th>Occupancy Rate</th>");
                html.AppendLine("<th>Status</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var data in occupancyData.OrderByDescending(d => d.OccupancyRate))
                {
                    var statusClass = data.OccupancyRate >= 90 ? "success" :
                                    data.OccupancyRate >= 70 ? "warning" : "critical";
                    var statusText = data.OccupancyRate >= 90 ? "High" :
                                   data.OccupancyRate >= 70 ? "Moderate" : "Low";

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{data.PropertyName}</td>");
                    html.AppendLine($"<td><span class='badge badge-{data.PropertyType.ToString().ToLower()}'>{data.PropertyType}</span></td>");
                    html.AppendLine($"<td>{data.TotalPortions}</td>");
                    html.AppendLine($"<td>{data.OccupiedPortions}</td>");
                    html.AppendLine($"<td>{data.VacantPortions}</td>");
                    html.AppendLine($"<td><span class='occupancy-badge'>{data.OccupancyRate:F1}%</span></td>");
                    html.AppendLine($"<td><span class='status-badge {statusClass}'>{statusText}</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div>");
            }
            else
            {
                html.AppendLine("<p class='no-data'>No occupancy data found.</p>");
            }
            html.AppendLine("</div>");

            // Vacant Portions
            var vacantProperties = occupancyData.Where(d => d.VacantPortions > 0).ToList();
            if (vacantProperties.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-door-open'></i> Vacant Portions</h2>");

                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Portion Name</th>");
                html.AppendLine("<th>Size</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var property in vacantProperties)
                {
                    foreach (var portion in property.Portions.Where(p => !p.IsOccupied))
                    {
                        html.AppendLine("<tr>");
                        html.AppendLine($"<td>{property.PropertyName}</td>");
                        html.AppendLine($"<td>{portion.PortionName}</td>");
                        html.AppendLine($"<td>{portion.PortionSize}</td>");
                        html.AppendLine("</tr>");
                    }
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div></div>");
            }

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooter());

            return html.ToString();
        }

        private string GeneratePropertyIncomeHtml(List<Property> properties, List<RentAgreement> agreements,
            List<Tenant> tenants, List<Payment> payments)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Property Income Report - " + DateTime.Now.Year));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-money-bill-wave'></i> Property Income Report</h1>");
            html.AppendLine($"<p class='period'>Year: {DateTime.Now.Year}</p>");
            html.AppendLine("</div>");

            // Summary Cards
            var totalRevenue = payments.Sum(p => p.Amount);
            var rentRevenue = payments.Where(p => p.PaymentType == PaymentType.Rent).Sum(p => p.Amount);
            var commissionRevenue = payments.Where(p => p.PaymentType == PaymentType.Commission).Sum(p => p.Amount);

            html.AppendLine("<div class='summary-cards'>");
            html.AppendLine($"<div class='card'><div class='card-value'>{totalRevenue:C}</div><div class='card-label'>Total Revenue</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{rentRevenue:C}</div><div class='card-label'>Rent Revenue</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{commissionRevenue:C}</div><div class='card-label'>Commission Revenue</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{payments.Count}</div><div class='card-label'>Total Transactions</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{properties.Count}</div><div class='card-label'>Properties</div></div>");
            html.AppendLine("</div>");

            // Monthly Revenue Chart
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-chart-line'></i> Monthly Revenue Trend</h2>");
            html.AppendLine("<div class='chart-container'>");
            html.AppendLine("<canvas id='revenueChart'></canvas>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");

            // Property-wise Income
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-building'></i> Property-wise Income</h2>");

            if (properties.Any())
            {
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Type</th>");
                html.AppendLine("<th>Rent Revenue</th>");
                html.AppendLine("<th>Commission Revenue</th>");
                html.AppendLine("<th>Total Revenue</th>");
                html.AppendLine("<th>% of Total</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var property in properties)
                {
                    var propertyAgreements = agreements.Where(a => a.PropertyId == property.Id).ToList();
                    var propertyPayments = payments.Where(p =>
                        propertyAgreements.Any(a => a.Id == p.AgreementId)).ToList();

                    var rentIncome = propertyPayments.Where(p => p.PaymentType == PaymentType.Rent).Sum(p => p.Amount);
                    var commissionIncome = propertyPayments.Where(p => p.PaymentType == PaymentType.Commission).Sum(p => p.Amount);
                    var totalIncome = rentIncome + commissionIncome;
                    var percentage = totalRevenue > 0 ? (totalIncome * 100m / totalRevenue) : 0;

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{property.Name}</td>");
                    html.AppendLine($"<td><span class='badge badge-{property.Type.ToString().ToLower()}'>{property.Type}</span></td>");
                    html.AppendLine($"<td class='amount positive'>{rentIncome:C}</td>");
                    html.AppendLine($"<td class='amount positive'>{commissionIncome:C}</td>");
                    html.AppendLine($"<td class='amount positive'>{totalIncome:C}</td>");
                    html.AppendLine($"<td><span class='percentage'>{percentage:F1}%</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div>");
            }
            else
            {
                html.AppendLine("<p class='no-data'>No properties found.</p>");
            }
            html.AppendLine("</div>");

            // Payment Type Breakdown
            var paymentTypes = payments.GroupBy(p => p.PaymentType);
            if (paymentTypes.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-chart-pie'></i> Payment Type Breakdown</h2>");
                html.AppendLine("<div class='breakdown'>");

                foreach (var typeGroup in paymentTypes)
                {
                    var typeAmount = typeGroup.Sum(p => p.Amount);
                    var percentage = totalRevenue > 0 ? (typeAmount * 100m / totalRevenue) : 0;

                    html.AppendLine("<div class='breakdown-item'>");
                    html.AppendLine($"<div class='breakdown-label'>{typeGroup.Key}</div>");
                    html.AppendLine($"<div class='breakdown-value'>{typeGroup.Count()}</div>");
                    html.AppendLine($"<div class='breakdown-amount'>{typeAmount:C}</div>");
                    html.AppendLine($"<div class='breakdown-percentage'>{percentage:F1}%</div>");
                    html.AppendLine("</div>");
                }

                html.AppendLine("</div></div>");
            }

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooterWithChart(payments));

            return html.ToString();
        }

        private string GenerateCollectionEfficiencyHtml(List<RentAgreement> agreements, List<Payment> payments, DateTime startDate, DateTime endDate)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Collection Efficiency Report"));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-percentage'></i> Collection Efficiency Report</h1>");
            html.AppendLine($"<p class='period'>Period: {startDate:dd MMM yyyy} to {endDate:dd MMM yyyy}</p>");
            html.AppendLine("</div>");

            // Calculate collection efficiency
            var expectedRent = agreements.Sum(a => a.MonthlyRent * GetMonthsInPeriod(startDate, endDate));
            var actualCollection = payments.Where(p => p.PaymentType == PaymentType.Rent).Sum(p => p.Amount);
            var collectionEfficiency = expectedRent > 0 ? (actualCollection * 100m / expectedRent) : 0;
            var pendingAmount = expectedRent - actualCollection;

            // Summary Cards
            html.AppendLine("<div class='summary-cards'>");
            html.AppendLine($"<div class='card'><div class='card-value'>{agreements.Count}</div><div class='card-label'>Active Agreements</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{expectedRent:C}</div><div class='card-label'>Expected Rent</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{actualCollection:C}</div><div class='card-label'>Collected</div></div>");
            html.AppendLine($"<div class='card critical'><div class='card-value'>{pendingAmount:C}</div><div class='card-label'>Pending</div></div>");

            var efficiencyClass = collectionEfficiency >= 90 ? "success" :
                                 collectionEfficiency >= 70 ? "warning" : "critical";
            html.AppendLine($"<div class='card {efficiencyClass}'><div class='card-value'>{collectionEfficiency:F1}%</div><div class='card-label'>Collection Efficiency</div></div>");

            html.AppendLine("</div>");

            // Monthly Collection Chart
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-chart-bar'></i> Monthly Collection Trend</h2>");
            html.AppendLine("<div class='chart-container'>");
            html.AppendLine("<canvas id='collectionChart'></canvas>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");

            // Agreement-wise Collection
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-list'></i> Agreement-wise Collection Status</h2>");

            if (agreements.Any())
            {
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Tenant</th>");
                html.AppendLine("<th>Monthly Rent</th>");
                html.AppendLine("<th>Expected</th>");
                html.AppendLine("<th>Collected</th>");
                html.AppendLine("<th>Pending</th>");
                html.AppendLine("<th>Collection %</th>");
                html.AppendLine("<th>Status</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                var dataService = new JsonDataService();
                var tenants = dataService.LoadTenants();

                foreach (var agreement in agreements)
                {
                    var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);
                    var agreementPayments = payments.Where(p => p.AgreementId == agreement.Id).ToList();
                    var expected = agreement.MonthlyRent * GetMonthsInPeriod(startDate, endDate);
                    var collected = agreementPayments.Sum(p => p.Amount);
                    var pending = expected - collected;
                    var efficiency = expected > 0 ? (collected * 100m / expected) : 0;

                    var statusClass = efficiency >= 100 ? "success" :
                                    efficiency >= 80 ? "warning" : "critical";
                    var statusText = efficiency >= 100 ? "Fully Paid" :
                                   efficiency >= 80 ? "Partially Paid" : "Behind Schedule";

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{tenant?.Name ?? "Unknown"}</td>");
                    html.AppendLine($"<td class='amount'>{agreement.MonthlyRent:C}</td>");
                    html.AppendLine($"<td class='amount'>{expected:C}</td>");
                    html.AppendLine($"<td class='amount positive'>{collected:C}</td>");
                    html.AppendLine($"<td class='amount negative'>{pending:C}</td>");
                    html.AppendLine($"<td><span class='percentage'>{efficiency:F1}%</span></td>");
                    html.AppendLine($"<td><span class='status-badge {statusClass}'>{statusText}</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div>");
            }
            else
            {
                html.AppendLine("<p class='no-data'>No active agreements found.</p>");
            }
            html.AppendLine("</div>");

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooterWithCollectionChart(payments, startDate, endDate));

            return html.ToString();
        }

        private string GenerateRevenueAnalysisHtml(List<Payment> payments, List<RentAgreement> agreements,
            List<Tenant> tenants, List<Property> properties, DateTime startDate, DateTime endDate)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Revenue Analysis Report"));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-chart-line'></i> Revenue Analysis Report</h1>");
            html.AppendLine($"<p class='period'>Period: {startDate:dd MMM yyyy} to {endDate:dd MMM yyyy}</p>");
            html.AppendLine("</div>");

            // Summary Cards
            var totalRevenue = payments.Sum(p => p.Amount);
            var rentRevenue = payments.Where(p => p.PaymentType == PaymentType.Rent).Sum(p => p.Amount);
            var commissionRevenue = payments.Where(p => p.PaymentType == PaymentType.Commission).Sum(p => p.Amount);
            var avgTransaction = payments.Any() ? payments.Average(p => p.Amount) : 0;
            var maxTransaction = payments.Any() ? payments.Max(p => p.Amount) : 0;

            html.AppendLine("<div class='summary-cards'>");
            html.AppendLine($"<div class='card'><div class='card-value'>{totalRevenue:C}</div><div class='card-label'>Total Revenue</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{rentRevenue:C}</div><div class='card-label'>Rent Revenue</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{commissionRevenue:C}</div><div class='card-label'>Commission</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{payments.Count}</div><div class='card-label'>Transactions</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{avgTransaction:C}</div><div class='card-label'>Avg Transaction</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{maxTransaction:C}</div><div class='card-label'>Max Transaction</div></div>");
            html.AppendLine("</div>");

            // Revenue Trend Chart
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-chart-line'></i> Revenue Trend</h2>");
            html.AppendLine("<div class='chart-container'>");
            html.AppendLine("<canvas id='revenueTrendChart'></canvas>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");

            // Monthly Revenue Breakdown
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-calendar'></i> Monthly Revenue Breakdown</h2>");

            var monthlyRevenue = payments
                .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                    RentRevenue = g.Where(p => p.PaymentType == PaymentType.Rent).Sum(p => p.Amount),
                    CommissionRevenue = g.Where(p => p.PaymentType == PaymentType.Commission).Sum(p => p.Amount),
                    TotalRevenue = g.Sum(p => p.Amount),
                    TransactionCount = g.Count()
                })
                .OrderBy(r => r.Month)
                .ToList();

            if (monthlyRevenue.Any())
            {
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Month</th>");
                html.AppendLine("<th>Rent Revenue</th>");
                html.AppendLine("<th>Commission Revenue</th>");
                html.AppendLine("<th>Total Revenue</th>");
                html.AppendLine("<th>Transactions</th>");
                html.AppendLine("<th>Avg/Transaction</th>");
                html.AppendLine("<th>Growth %</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                decimal? previousRevenue = null;
                foreach (var month in monthlyRevenue)
                {
                    var growth = previousRevenue.HasValue && previousRevenue.Value > 0
                        ? ((month.TotalRevenue - previousRevenue.Value) * 100m / previousRevenue.Value)
                        : 0;
                    previousRevenue = month.TotalRevenue;

                    var growthClass = growth >= 0 ? "positive" : "negative";
                    var growthText = growth >= 0 ? $"+{growth:F1}%" : $"{growth:F1}%";

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{month.Month:MMM yyyy}</td>");
                    html.AppendLine($"<td class='amount positive'>{month.RentRevenue:C}</td>");
                    html.AppendLine($"<td class='amount positive'>{month.CommissionRevenue:C}</td>");
                    html.AppendLine($"<td class='amount positive'>{month.TotalRevenue:C}</td>");
                    html.AppendLine($"<td>{month.TransactionCount}</td>");
                    html.AppendLine($"<td class='amount'>{(month.TransactionCount > 0 ? month.TotalRevenue / month.TransactionCount : 0):C}</td>");
                    html.AppendLine($"<td class='amount {growthClass}'>{growthText}</td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div>");
            }
            else
            {
                html.AppendLine("<p class='no-data'>No revenue data found for this period.</p>");
            }
            html.AppendLine("</div>");

            // Top Performing Properties
            var propertyRevenue = payments
                .Join(agreements, p => p.AgreementId, a => a.Id, (p, a) => new { Payment = p, Agreement = a })
                .Join(properties, x => x.Agreement.PropertyId, pr => pr.Id, (x, pr) => new { x.Payment, Property = pr })
                .GroupBy(x => x.Property.Name)
                .Select(g => new
                {
                    PropertyName = g.Key,
                    Revenue = g.Sum(x => x.Payment.Amount),
                    TransactionCount = g.Count()
                })
                .OrderByDescending(r => r.Revenue)
                .Take(10)
                .ToList();

            if (propertyRevenue.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-trophy'></i> Top Performing Properties</h2>");
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Revenue</th>");
                html.AppendLine("<th>Transactions</th>");
                html.AppendLine("<th>% of Total</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var property in propertyRevenue)
                {
                    var percentage = totalRevenue > 0 ? (property.Revenue * 100m / totalRevenue) : 0;

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{property.PropertyName}</td>");
                    html.AppendLine($"<td class='amount positive'>{property.Revenue:C}</td>");
                    html.AppendLine($"<td>{property.TransactionCount}</td>");
                    html.AppendLine($"<td><span class='percentage'>{percentage:F1}%</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div></div>");
            }

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooterWithRevenueChart(payments, startDate, endDate));

            return html.ToString();
        }

        private string GenerateOutstandingDuesHtml(List<RentOverview> outstandingDues)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Outstanding Dues Report"));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-exclamation-triangle'></i> Outstanding Dues Report</h1>");
            html.AppendLine($"<p class='period'>As of: {DateTime.Now:dd MMM yyyy}</p>");
            html.AppendLine("</div>");

            // Summary Cards
            var totalDue = outstandingDues.Sum(d => d.DueAmount);
            var rentDue = outstandingDues.Where(d => d.TenantType == TenantType.OnRent).Sum(d => d.DueAmount);
            var commissionDue = outstandingDues.Where(d => d.TenantType == TenantType.OnCommission).Sum(d => d.DueAmount);
            var severelyOverdue = outstandingDues.Count(d => d.DaysOverdue > 30);
            var averageOverdueDays = outstandingDues.Any() ? outstandingDues.Average(d => d.DaysOverdue) : 0;

            html.AppendLine("<div class='summary-cards'>");
            html.AppendLine($"<div class='card critical'><div class='card-value'>{outstandingDues.Count}</div><div class='card-label'>Tenants with Dues</div></div>");
            html.AppendLine($"<div class='card critical'><div class='card-value'>{totalDue:C}</div><div class='card-label'>Total Dues</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{rentDue:C}</div><div class='card-label'>Rent Dues</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{commissionDue:C}</div><div class='card-label'>Commission Dues</div></div>");
            html.AppendLine($"<div class='card danger'><div class='card-value'>{severelyOverdue}</div><div class='card-label'>>30 Days Overdue</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{averageOverdueDays:F0} days</div><div class='card-label'>Avg Overdue Days</div></div>");
            html.AppendLine("</div>");

            // Dues by Property
            var propertyGroups = outstandingDues.GroupBy(d => d.PropertyName);
            if (propertyGroups.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-building'></i> Dues by Property</h2>");
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Tenants with Dues</th>");
                html.AppendLine("<th>Total Due Amount</th>");
                html.AppendLine("<th>Avg Due per Tenant</th>");
                html.AppendLine("<th>Max Overdue Days</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var group in propertyGroups.OrderByDescending(g => g.Sum(d => d.DueAmount)))
                {
                    var maxOverdueDays = group.Max(d => d.DaysOverdue);

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{group.Key}</td>");
                    html.AppendLine($"<td>{group.Count()}</td>");
                    html.AppendLine($"<td class='amount negative'>{group.Sum(d => d.DueAmount):C}</td>");
                    html.AppendLine($"<td class='amount negative'>{group.Average(d => d.DueAmount):C}</td>");
                    html.AppendLine($"<td><span class='days-overdue'>{maxOverdueDays} days</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div></div>");
            }

            // Detailed Dues List
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-list'></i> Detailed Outstanding Dues</h2>");

            if (outstandingDues.Any())
            {
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Tenant</th>");
                html.AppendLine("<th>Mobile</th>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Portion</th>");
                html.AppendLine("<th>Tenant Type</th>");
                html.AppendLine("<th>Due Amount</th>");
                html.AppendLine("<th>Last Payment</th>");
                html.AppendLine("<th>Next Due</th>");
                html.AppendLine("<th>Days Overdue</th>");
                html.AppendLine("<th>Status</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var due in outstandingDues.OrderByDescending(d => d.DaysOverdue))
                {
                    var statusClass = due.DaysOverdue > 30 ? "danger" :
                                    due.DaysOverdue > 7 ? "critical" : "warning";
                    var statusText = due.DaysOverdue > 30 ? "Severely Overdue" :
                                   due.DaysOverdue > 7 ? "Overdue" : "Due Soon";

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{due.TenantName}</td>");
                    html.AppendLine($"<td>{due.Mobile}</td>");
                    html.AppendLine($"<td>{due.PropertyName}</td>");
                    html.AppendLine($"<td>{due.PortionName}</td>");
                    html.AppendLine($"<td><span class='badge badge-{due.TenantType.ToString().ToLower()}'>{due.TenantType}</span></td>");
                    html.AppendLine($"<td class='amount negative'>{due.DueAmount:C}</td>");
                    html.AppendLine($"<td>{due.LastPaymentDate:dd MMM yyyy}</td>");
                    html.AppendLine($"<td>{due.NextDueDate:dd MMM yyyy}</td>");
                    html.AppendLine($"<td><span class='days-overdue'>{due.DaysOverdue} days</span></td>");
                    html.AppendLine($"<td><span class='status-badge {statusClass}'>{statusText}</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div>");
            }
            else
            {
                html.AppendLine("<p class='no-data'>No outstanding dues found.</p>");
            }
            html.AppendLine("</div>");

            // Overdue Analysis
            var overdueAnalysis = new[]
            {
        new { Range = "1-7 days", Count = outstandingDues.Count(d => d.DaysOverdue > 0 && d.DaysOverdue <= 7), Amount = outstandingDues.Where(d => d.DaysOverdue > 0 && d.DaysOverdue <= 7).Sum(d => d.DueAmount) },
        new { Range = "8-30 days", Count = outstandingDues.Count(d => d.DaysOverdue > 7 && d.DaysOverdue <= 30), Amount = outstandingDues.Where(d => d.DaysOverdue > 7 && d.DaysOverdue <= 30).Sum(d => d.DueAmount) },
        new { Range = "31-90 days", Count = outstandingDues.Count(d => d.DaysOverdue > 30 && d.DaysOverdue <= 90), Amount = outstandingDues.Where(d => d.DaysOverdue > 30 && d.DaysOverdue <= 90).Sum(d => d.DueAmount) },
        new { Range = ">90 days", Count = outstandingDues.Count(d => d.DaysOverdue > 90), Amount = outstandingDues.Where(d => d.DaysOverdue > 90).Sum(d => d.DueAmount) }
    };

            if (overdueAnalysis.Any(a => a.Count > 0))
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-chart-bar'></i> Overdue Analysis</h2>");
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Overdue Period</th>");
                html.AppendLine("<th>Tenants</th>");
                html.AppendLine("<th>Due Amount</th>");
                html.AppendLine("<th>% of Total Dues</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var analysis in overdueAnalysis.Where(a => a.Count > 0))
                {
                    var percentage = totalDue > 0 ? (analysis.Amount * 100m / totalDue) : 0;

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{analysis.Range}</td>");
                    html.AppendLine($"<td>{analysis.Count}</td>");
                    html.AppendLine($"<td class='amount negative'>{analysis.Amount:C}</td>");
                    html.AppendLine($"<td><span class='percentage'>{percentage:F1}%</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div></div>");
            }

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooter());

            return html.ToString();
        }

        private string GenerateCommissionSummaryHtml(List<Payment> commissionPayments,
            List<CommissionTransaction> commissionTransactions, List<RentAgreement> agreements,
            List<Tenant> tenants, List<Product> products, DateTime startDate, DateTime endDate)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Commission Summary Report"));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-chart-line'></i> Commission Summary Report</h1>");
            html.AppendLine($"<p class='period'>Period: {startDate:dd MMM yyyy} to {endDate:dd MMM yyyy}</p>");
            html.AppendLine("</div>");

            // Summary Cards
            var totalCommission = commissionPayments.Sum(p => p.Amount);
            var totalSales = commissionTransactions.Sum(t => t.ProductTotal + t.LaborAmount);
            var totalProductsValue = commissionTransactions.Sum(t => t.ProductTotal);
            var totalLabor = commissionTransactions.Sum(t => t.LaborAmount);
            var avgCommissionRate = commissionTransactions.Any() ? commissionTransactions.Average(t => t.CommissionRate) : 0;

            html.AppendLine("<div class='summary-cards'>");
            html.AppendLine($"<div class='card'><div class='card-value'>{totalCommission:C}</div><div class='card-label'>Total Commission</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{totalSales:C}</div><div class='card-label'>Total Sales</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{totalProductsValue:C}</div><div class='card-label'>Product Sales</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{totalLabor:C}</div><div class='card-label'>Labor Charges</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{commissionPayments.Count}</div><div class='card-label'>Commission Payments</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{avgCommissionRate:F1}%</div><div class='card-label'>Avg Commission Rate</div></div>");
            html.AppendLine("</div>");

            // Commission Trend Chart
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-chart-line'></i> Commission Trend</h2>");
            html.AppendLine("<div class='chart-container'>");
            html.AppendLine("<canvas id='commissionChart'></canvas>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");

            // Commission by Tenant
            var commissionByTenant = commissionPayments
                .Join(agreements, p => p.AgreementId, a => a.Id, (p, a) => new { Payment = p, Agreement = a })
                .Join(tenants, x => x.Agreement.TenantId, t => t.Id, (x, t) => new { x.Payment, Tenant = t })
                .GroupBy(x => x.Tenant.Name)
                .Select(g => new
                {
                    TenantName = g.Key,
                    Commission = g.Sum(x => x.Payment.Amount),
                    PaymentCount = g.Count(),
                    AvgCommission = g.Average(x => x.Payment.Amount)
                })
                .OrderByDescending(t => t.Commission)
                .ToList();

            if (commissionByTenant.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-user-tie'></i> Commission by Tenant</h2>");
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Tenant</th>");
                html.AppendLine("<th>Commission Amount</th>");
                html.AppendLine("<th>Payments</th>");
                html.AppendLine("<th>Average Commission</th>");
                html.AppendLine("<th>% of Total</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var tenant in commissionByTenant)
                {
                    var percentage = totalCommission > 0 ? (tenant.Commission * 100m / totalCommission) : 0;

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{tenant.TenantName}</td>");
                    html.AppendLine($"<td class='amount positive'>{tenant.Commission:C}</td>");
                    html.AppendLine($"<td>{tenant.PaymentCount}</td>");
                    html.AppendLine($"<td class='amount'>{tenant.AvgCommission:C}</td>");
                    html.AppendLine($"<td><span class='percentage'>{percentage:F1}%</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div></div>");
            }

            // Product-wise Commission
            var productCommission = commissionTransactions
                .GroupBy(t => t.ProductName)
                .Select(g => new
                {
                    ProductName = g.Key,
                    Quantity = g.Sum(t => t.Quantity),
                    SalesValue = g.Sum(t => t.ProductTotal),
                    Commission = g.Sum(t => t.CommissionAmount),
                    AvgCommissionRate = g.Average(t => t.CommissionRate)
                })
                .OrderByDescending(p => p.Commission)
                .ToList();

            if (productCommission.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-box'></i> Product-wise Commission</h2>");
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Product</th>");
                html.AppendLine("<th>Quantity</th>");
                html.AppendLine("<th>Sales Value</th>");
                html.AppendLine("<th>Commission</th>");
                html.AppendLine("<th>Avg Commission Rate</th>");
                html.AppendLine("<th>Commission % of Sales</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var product in productCommission)
                {
                    var commissionPercentage = product.SalesValue > 0 ? (product.Commission * 100m / product.SalesValue) : 0;

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{product.ProductName}</td>");
                    html.AppendLine($"<td>{product.Quantity:F2}</td>");
                    html.AppendLine($"<td class='amount positive'>{product.SalesValue:C}</td>");
                    html.AppendLine($"<td class='amount positive'>{product.Commission:C}</td>");
                    html.AppendLine($"<td><span class='percentage'>{product.AvgCommissionRate:F1}%</span></td>");
                    html.AppendLine($"<td><span class='percentage'>{commissionPercentage:F1}%</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div></div>");
            }

            // Monthly Commission Breakdown
            var monthlyCommission = commissionPayments
                .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                    Commission = g.Sum(p => p.Amount),
                    PaymentCount = g.Count(),
                    AvgCommission = g.Average(p => p.Amount)
                })
                .OrderBy(m => m.Month)
                .ToList();

            if (monthlyCommission.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-calendar'></i> Monthly Commission Breakdown</h2>");
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Month</th>");
                html.AppendLine("<th>Commission</th>");
                html.AppendLine("<th>Payments</th>");
                html.AppendLine("<th>Average Commission</th>");
                html.AppendLine("<th>Growth %</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                decimal? previousCommission = null;
                foreach (var month in monthlyCommission)
                {
                    var growth = previousCommission.HasValue && previousCommission.Value > 0
                        ? ((month.Commission - previousCommission.Value) * 100m / previousCommission.Value)
                        : 0;
                    previousCommission = month.Commission;

                    var growthClass = growth >= 0 ? "positive" : "negative";
                    var growthText = growth >= 0 ? $"+{growth:F1}%" : $"{growth:F1}%";

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{month.Month:MMM yyyy}</td>");
                    html.AppendLine($"<td class='amount positive'>{month.Commission:C}</td>");
                    html.AppendLine($"<td>{month.PaymentCount}</td>");
                    html.AppendLine($"<td class='amount'>{month.AvgCommission:C}</td>");
                    html.AppendLine($"<td class='amount {growthClass}'>{growthText}</td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div></div>");
            }

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooterWithCommissionChart(commissionPayments, startDate, endDate));

            return html.ToString();
        }

        private string GenerateProductSalesHtml(List<CommissionTransaction> commissionTransactions,
            List<Product> products, List<RentAgreement> agreements, List<Tenant> tenants,
            DateTime startDate, DateTime endDate)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Product Sales Report"));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-shopping-cart'></i> Product Sales Report</h1>");
            html.AppendLine($"<p class='period'>Period: {startDate:dd MMM yyyy} to {endDate:dd MMM yyyy}</p>");
            html.AppendLine("</div>");

            // Summary Cards
            var totalSales = commissionTransactions.Sum(t => t.ProductTotal + t.LaborAmount);
            var productSales = commissionTransactions.Sum(t => t.ProductTotal);
            var laborCharges = commissionTransactions.Sum(t => t.LaborAmount);
            var totalQuantity = commissionTransactions.Sum(t => t.Quantity);
            var avgSaleValue = commissionTransactions.Any() ? commissionTransactions.Average(t => t.ProductTotal + t.LaborAmount) : 0;
            var transactionCount = commissionTransactions.GroupBy(t => t.PaymentId).Count();

            html.AppendLine("<div class='summary-cards'>");
            html.AppendLine($"<div class='card'><div class='card-value'>{totalSales:C}</div><div class='card-label'>Total Sales</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{productSales:C}</div><div class='card-label'>Product Sales</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{laborCharges:C}</div><div class='card-label'>Labor Charges</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{totalQuantity:F2}</div><div class='card-label'>Total Quantity</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{transactionCount}</div><div class='card-label'>Transactions</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{avgSaleValue:C}</div><div class='card-label'>Avg Sale Value</div></div>");
            html.AppendLine("</div>");

            // Sales Trend Chart
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-chart-line'></i> Sales Trend</h2>");
            html.AppendLine("<div class='chart-container'>");
            html.AppendLine("<canvas id='salesChart'></canvas>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");

            // Product Sales Summary
            var productSummary = commissionTransactions
                .GroupBy(t => t.ProductName)
                .Select(g => new
                {
                    ProductName = g.Key,
                    Quantity = g.Sum(t => t.Quantity),
                    Unit = g.First().Unit,
                    UnitPrice = g.Average(t => t.UnitPrice),
                    ProductValue = g.Sum(t => t.ProductTotal),
                    LaborValue = g.Sum(t => t.LaborAmount),
                    TotalValue = g.Sum(t => t.ProductTotal + t.LaborAmount),
                    TransactionCount = g.Select(t => t.PaymentId).Distinct().Count(),
                    AvgQuantityPerSale = g.Average(t => t.Quantity),
                    AvgSaleValue = g.Average(t => t.ProductTotal + t.LaborAmount)
                })
                .OrderByDescending(p => p.TotalValue)
                .ToList();

            if (productSummary.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-box'></i> Product Sales Summary</h2>");
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Product</th>");
                html.AppendLine("<th>Quantity</th>");
                html.AppendLine("<th>Unit</th>");
                html.AppendLine("<th>Avg Unit Price</th>");
                html.AppendLine("<th>Product Value</th>");
                html.AppendLine("<th>Labor Charges</th>");
                html.AppendLine("<th>Total Value</th>");
                html.AppendLine("<th>Transactions</th>");
                html.AppendLine("<th>Avg Qty/Sale</th>");
                html.AppendLine("<th>Avg Sale Value</th>");
                html.AppendLine("<th>% of Total</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var product in productSummary)
                {
                    var percentage = totalSales > 0 ? (product.TotalValue * 100m / totalSales) : 0;

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{product.ProductName}</td>");
                    html.AppendLine($"<td>{product.Quantity:F2}</td>");
                    html.AppendLine($"<td>{product.Unit}</td>");
                    html.AppendLine($"<td class='amount'>{product.UnitPrice:C}</td>");
                    html.AppendLine($"<td class='amount positive'>{product.ProductValue:C}</td>");
                    html.AppendLine($"<td class='amount positive'>{product.LaborValue:C}</td>");
                    html.AppendLine($"<td class='amount positive'>{product.TotalValue:C}</td>");
                    html.AppendLine($"<td>{product.TransactionCount}</td>");
                    html.AppendLine($"<td>{product.AvgQuantityPerSale:F2}</td>");
                    html.AppendLine($"<td class='amount'>{product.AvgSaleValue:C}</td>");
                    html.AppendLine($"<td><span class='percentage'>{percentage:F1}%</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div></div>");
            }

            // Top Customers
            var customerSales = commissionTransactions
                .Join(agreements, ct => ct.PaymentId, a => a.Id, (ct, a) => new { Transaction = ct, Agreement = a })
                .Join(tenants, x => x.Agreement.TenantId, t => t.Id, (x, t) => new { x.Transaction, Tenant = t })
                .GroupBy(x => x.Tenant.Name)
                .Select(g => new
                {
                    CustomerName = g.Key,
                    TotalValue = g.Sum(x => x.Transaction.ProductTotal + x.Transaction.LaborAmount),
                    TransactionCount = g.Select(x => x.Transaction.PaymentId).Distinct().Count(),
                    AvgSaleValue = g.Average(x => x.Transaction.ProductTotal + x.Transaction.LaborAmount),
                    TotalQuantity = g.Sum(x => x.Transaction.Quantity)
                })
                .OrderByDescending(c => c.TotalValue)
                .Take(10)
                .ToList();

            if (customerSales.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-user-tie'></i> Top Customers</h2>");
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Customer</th>");
                html.AppendLine("<th>Total Purchases</th>");
                html.AppendLine("<th>Transactions</th>");
                html.AppendLine("<th>Average Purchase</th>");
                html.AppendLine("<th>Total Quantity</th>");
                html.AppendLine("<th>% of Total Sales</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var customer in customerSales)
                {
                    var percentage = totalSales > 0 ? (customer.TotalValue * 100m / totalSales) : 0;

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{customer.CustomerName}</td>");
                    html.AppendLine($"<td class='amount positive'>{customer.TotalValue:C}</td>");
                    html.AppendLine($"<td>{customer.TransactionCount}</td>");
                    html.AppendLine($"<td class='amount'>{customer.AvgSaleValue:C}</td>");
                    html.AppendLine($"<td>{customer.TotalQuantity:F2}</td>");
                    html.AppendLine($"<td><span class='percentage'>{percentage:F1}%</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div></div>");
            }

            // Monthly Sales Breakdown
            var monthlySales = commissionTransactions
                .GroupBy(t => new { t.TransactionDate.Year, t.TransactionDate.Month })
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                    ProductValue = g.Sum(t => t.ProductTotal),
                    LaborValue = g.Sum(t => t.LaborAmount),
                    TotalValue = g.Sum(t => t.ProductTotal + t.LaborAmount),
                    Quantity = g.Sum(t => t.Quantity),
                    TransactionCount = g.Select(t => t.PaymentId).Distinct().Count()
                })
                .OrderBy(m => m.Month)
                .ToList();

            if (monthlySales.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-calendar'></i> Monthly Sales Breakdown</h2>");
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Month</th>");
                html.AppendLine("<th>Product Sales</th>");
                html.AppendLine("<th>Labor Charges</th>");
                html.AppendLine("<th>Total Sales</th>");
                html.AppendLine("<th>Quantity</th>");
                html.AppendLine("<th>Transactions</th>");
                html.AppendLine("<th>Avg Sale Value</th>");
                html.AppendLine("<th>Growth %</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                decimal? previousSales = null;
                foreach (var month in monthlySales)
                {
                    var growth = previousSales.HasValue && previousSales.Value > 0
                        ? ((month.TotalValue - previousSales.Value) * 100m / previousSales.Value)
                        : 0;
                    previousSales = month.TotalValue;

                    var growthClass = growth >= 0 ? "positive" : "negative";
                    var growthText = growth >= 0 ? $"+{growth:F1}%" : $"{growth:F1}%";
                    var avgSaleValue1 = month.TransactionCount > 0 ? month.TotalValue / month.TransactionCount : 0;

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{month.Month:MMM yyyy}</td>");
                    html.AppendLine($"<td class='amount positive'>{month.ProductValue:C}</td>");
                    html.AppendLine($"<td class='amount positive'>{month.LaborValue:C}</td>");
                    html.AppendLine($"<td class='amount positive'>{month.TotalValue:C}</td>");
                    html.AppendLine($"<td>{month.Quantity:F2}</td>");
                    html.AppendLine($"<td>{month.TransactionCount}</td>");
                    html.AppendLine($"<td class='amount'>{avgSaleValue1:C}</td>");
                    html.AppendLine($"<td class='amount {growthClass}'>{growthText}</td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div></div>");
            }

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooterWithSalesChart(commissionTransactions, startDate, endDate));

            return html.ToString();
        }

        private string GenerateCommissionDueHtml(List<RentOverview> commissionDues)
        {
            var html = new StringBuilder();
            html.AppendLine(CreateHtmlHeader("Commission Due Report"));

            html.AppendLine("<div class='report-container'>");
            html.AppendLine("<div class='report-header'>");
            html.AppendLine("<h1><i class='fas fa-money-check-alt'></i> Commission Due Report</h1>");
            html.AppendLine($"<p class='period'>As of: {DateTime.Now:dd MMM yyyy}</p>");
            html.AppendLine("</div>");

            // Summary Cards
            var totalDue = commissionDues.Sum(d => d.CommissionDue ?? 0);
            var tenantsWithDue = commissionDues.Count;
            var avgDuePerTenant = tenantsWithDue > 0 ? totalDue / tenantsWithDue : 0;
            var maxDue = commissionDues.Any() ? commissionDues.Max(d => d.CommissionDue ?? 0) : 0;
            var overdueCount = commissionDues.Count(d => d.NextCommissionDueDate.HasValue && d.NextCommissionDueDate < DateTime.Now);

            html.AppendLine("<div class='summary-cards'>");
            html.AppendLine($"<div class='card critical'><div class='card-value'>{tenantsWithDue}</div><div class='card-label'>Tenants with Due</div></div>");
            html.AppendLine($"<div class='card critical'><div class='card-value'>{totalDue:C}</div><div class='card-label'>Total Commission Due</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{avgDuePerTenant:C}</div><div class='card-label'>Avg Due per Tenant</div></div>");
            html.AppendLine($"<div class='card'><div class='card-value'>{maxDue:C}</div><div class='card-label'>Max Commission Due</div></div>");
            html.AppendLine($"<div class='card warning'><div class='card-value'>{overdueCount}</div><div class='card-label'>Overdue Commissions</div></div>");
            html.AppendLine("</div>");

            // Commission Due Details
            html.AppendLine("<div class='section'>");
            html.AppendLine("<h2><i class='fas fa-list'></i> Commission Due Details</h2>");

            if (commissionDues.Any())
            {
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Tenant</th>");
                html.AppendLine("<th>Mobile</th>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Portion</th>");
                html.AppendLine("<th>Commission Due</th>");
                html.AppendLine("<th>Last Commission Paid</th>");
                html.AppendLine("<th>Next Commission Due</th>");
                html.AppendLine("<th>Days Overdue</th>");
                html.AppendLine("<th>Payment Info</th>");
                html.AppendLine("<th>Status</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var due in commissionDues.OrderByDescending(d => d.CommissionDue))
                {
                    var daysOverdue = due.NextCommissionDueDate.HasValue
                        ? (DateTime.Now - due.NextCommissionDueDate.Value).Days
                        : 0;

                    var statusClass = daysOverdue > 30 ? "danger" :
                                    daysOverdue > 7 ? "critical" :
                                    daysOverdue > 0 ? "warning" : "success";
                    var statusText = daysOverdue > 30 ? "Severely Overdue" :
                                   daysOverdue > 7 ? "Overdue" :
                                   daysOverdue > 0 ? "Due Soon" : "Current";

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{due.TenantName}</td>");
                    html.AppendLine($"<td>{due.Mobile}</td>");
                    html.AppendLine($"<td>{due.PropertyName}</td>");
                    html.AppendLine($"<td>{due.PortionName}</td>");
                    html.AppendLine($"<td class='amount negative'>{due.CommissionDue:C}</td>");
                    html.AppendLine($"<td>{due.LastPaymentDate:dd MMM yyyy}</td>");
                    html.AppendLine($"<td>{due.NextCommissionDueDate:dd MMM yyyy}</td>");
                    html.AppendLine($"<td><span class='days-overdue'>{daysOverdue} days</span></td>");
                    html.AppendLine($"<td>{due.PaymentInfo}</td>");
                    html.AppendLine($"<td><span class='status-badge {statusClass}'>{statusText}</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div>");
            }
            else
            {
                html.AppendLine("<p class='no-data'>No commission dues found.</p>");
            }
            html.AppendLine("</div>");

            // Commission Due Analysis
            var dueAnalysis = commissionDues
                .Where(d => d.CommissionDue > 0)
                .GroupBy(d => d.PropertyName)
                .Select(g => new
                {
                    PropertyName = g.Key,
                    TenantCount = g.Count(),
                    TotalDue = g.Sum(d => d.CommissionDue ?? 0),
                    AvgDue = g.Average(d => d.CommissionDue ?? 0)
                })
                .OrderByDescending(a => a.TotalDue)
                .ToList();

            if (dueAnalysis.Any())
            {
                html.AppendLine("<div class='section'>");
                html.AppendLine("<h2><i class='fas fa-building'></i> Commission Due by Property</h2>");
                html.AppendLine("<div class='table-container'>");
                html.AppendLine("<table class='data-table'>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Property</th>");
                html.AppendLine("<th>Tenants with Due</th>");
                html.AppendLine("<th>Total Commission Due</th>");
                html.AppendLine("<th>Average Due</th>");
                html.AppendLine("<th>% of Total Due</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var analysis in dueAnalysis)
                {
                    var percentage = totalDue > 0 ? (analysis.TotalDue * 100m / totalDue) : 0;

                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{analysis.PropertyName}</td>");
                    html.AppendLine($"<td>{analysis.TenantCount}</td>");
                    html.AppendLine($"<td class='amount negative'>{analysis.TotalDue:C}</td>");
                    html.AppendLine($"<td class='amount negative'>{analysis.AvgDue:C}</td>");
                    html.AppendLine($"<td><span class='percentage'>{percentage:F1}%</span></td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("</div></div>");
            }

            html.AppendLine("</div>");
            html.AppendLine(CreateHtmlFooter());

            return html.ToString();
        }

        #endregion

        #region Helper Methods for Charts

        private string CreateHtmlFooterWithChart(List<Payment> payments)
        {
            var monthlyData = payments
                .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy"),
                    Rent = g.Where(p => p.PaymentType == PaymentType.Rent).Sum(p => p.Amount),
                    Commission = g.Where(p => p.PaymentType == PaymentType.Commission).Sum(p => p.Amount)
                })
                .OrderBy(d => d.Month)
                .ToList();

            var labels = string.Join(", ", monthlyData.Select(d => $"'{d.Month}'"));
            var rentData = string.Join(", ", monthlyData.Select(d => d.Rent));
            var commissionData = string.Join(", ", monthlyData.Select(d => d.Commission));

            return $@"{CreateHtmlFooter()}
<script>
    const ctx = document.getElementById('revenueChart').getContext('2d');
    new Chart(ctx, {{
        type: 'line',
        data: {{
            labels: [{labels}],
            datasets: [
                {{
                    label: 'Rent Revenue',
                    data: [{rentData}],
                    borderColor: '#27ae60',
                    backgroundColor: 'rgba(39, 174, 96, 0.1)',
                    fill: true
                }},
                {{
                    label: 'Commission Revenue',
                    data: [{commissionData}],
                    borderColor: '#9b59b6',
                    backgroundColor: 'rgba(155, 89, 182, 0.1)',
                    fill: true
                }}
            ]
        }},
        options: {{
            responsive: true,
            plugins: {{
                title: {{
                    display: true,
                    text: 'Monthly Revenue Trend'
                }}
            }},
            scales: {{
                y: {{
                    beginAtZero: true,
                    ticks: {{
                        callback: function(value) {{
                            return '₹' + value.toLocaleString('en-IN');
                        }}
                    }}
                }}
            }}
        }}
    }});
</script>";
        }

        private string CreateHtmlFooterWithCollectionChart(List<Payment> payments, DateTime startDate, DateTime endDate)
        {
            var monthlyData = payments
                .Where(p => p.PaymentType == PaymentType.Rent)
                .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy"),
                    Collected = g.Sum(p => p.Amount)
                })
                .OrderBy(d => d.Month)
                .ToList();

            var labels = string.Join(", ", monthlyData.Select(d => $"'{d.Month}'"));
            var data = string.Join(", ", monthlyData.Select(d => d.Collected));

            return $@"{CreateHtmlFooter()}
<script>
    const ctx = document.getElementById('collectionChart').getContext('2d');
    new Chart(ctx, {{
        type: 'bar',
        data: {{
            labels: [{labels}],
            datasets: [{{
                label: 'Rent Collection',
                data: [{data}],
                backgroundColor: '#3498db',
                borderColor: '#2980b9',
                borderWidth: 1
            }}]
        }},
        options: {{
            responsive: true,
            plugins: {{
                title: {{
                    display: true,
                    text: 'Monthly Rent Collection'
                }}
            }},
            scales: {{
                y: {{
                    beginAtZero: true,
                    ticks: {{
                        callback: function(value) {{
                            return '₹' + value.toLocaleString('en-IN');
                        }}
                    }}
                }}
            }}
        }}
    }});
</script>";
        }

        private string CreateHtmlFooterWithRevenueChart(List<Payment> payments, DateTime startDate, DateTime endDate)
        {
            var monthlyData = payments
                .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy"),
                    Total = g.Sum(p => p.Amount)
                })
                .OrderBy(d => d.Month)
                .ToList();

            var labels = string.Join(", ", monthlyData.Select(d => $"'{d.Month}'"));
            var data = string.Join(", ", monthlyData.Select(d => d.Total));

            return $@"{CreateHtmlFooter()}
<script>
    const ctx = document.getElementById('revenueTrendChart').getContext('2d');
    new Chart(ctx, {{
        type: 'line',
        data: {{
            labels: [{labels}],
            datasets: [{{
                label: 'Total Revenue',
                data: [{data}],
                borderColor: '#e74c3c',
                backgroundColor: 'rgba(231, 76, 60, 0.1)',
                fill: true,
                tension: 0.4
            }}]
        }},
        options: {{
            responsive: true,
            plugins: {{
                title: {{
                    display: true,
                    text: 'Revenue Trend'
                }}
            }},
            scales: {{
                y: {{
                    beginAtZero: true,
                    ticks: {{
                        callback: function(value) {{
                            return '₹' + value.toLocaleString('en-IN');
                        }}
                    }}
                }}
            }}
        }}
    }});
</script>";
        }

        private string CreateHtmlFooterWithCommissionChart(List<Payment> commissionPayments, DateTime startDate, DateTime endDate)
        {
            var monthlyData = commissionPayments
                .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy"),
                    Commission = g.Sum(p => p.Amount)
                })
                .OrderBy(d => d.Month)
                .ToList();

            var labels = string.Join(", ", monthlyData.Select(d => $"'{d.Month}'"));
            var data = string.Join(", ", monthlyData.Select(d => d.Commission));

            return $@"{CreateHtmlFooter()}
<script>
    const ctx = document.getElementById('commissionChart').getContext('2d');
    new Chart(ctx, {{
        type: 'line',
        data: {{
            labels: [{labels}],
            datasets: [{{
                label: 'Commission Paid',
                data: [{data}],
                borderColor: '#f39c12',
                backgroundColor: 'rgba(243, 156, 18, 0.1)',
                fill: true
            }}]
        }},
        options: {{
            responsive: true,
            plugins: {{
                title: {{
                    display: true,
                    text: 'Commission Trend'
                }}
            }},
            scales: {{
                y: {{
                    beginAtZero: true,
                    ticks: {{
                        callback: function(value) {{
                            return '₹' + value.toLocaleString('en-IN');
                        }}
                    }}
                }}
            }}
        }}
    }});
</script>";
        }

        private string CreateHtmlFooterWithSalesChart(List<CommissionTransaction> transactions, DateTime startDate, DateTime endDate)
        {
            var monthlyData = transactions
                .GroupBy(t => new { t.TransactionDate.Year, t.TransactionDate.Month })
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy"),
                    Sales = g.Sum(t => t.ProductTotal + t.LaborAmount)
                })
                .OrderBy(d => d.Month)
                .ToList();

            var labels = string.Join(", ", monthlyData.Select(d => $"'{d.Month}'"));
            var data = string.Join(", ", monthlyData.Select(d => d.Sales));

            return $@"{CreateHtmlFooter()}
<script>
    const ctx = document.getElementById('salesChart').getContext('2d');
    new Chart(ctx, {{
        type: 'bar',
        data: {{
            labels: [{labels}],
            datasets: [{{
                label: 'Sales Value',
                data: [{data}],
                backgroundColor: '#2ecc71',
                borderColor: '#27ae60',
                borderWidth: 1
            }}]
        }},
        options: {{
            responsive: true,
            plugins: {{
                title: {{
                    display: true,
                    text: 'Monthly Sales Trend'
                }}
            }},
            scales: {{
                y: {{
                    beginAtZero: true,
                    ticks: {{
                        callback: function(value) {{
                            return '₹' + value.toLocaleString('en-IN');
                        }}
                    }}
                }}
            }}
        }}
    }});
</script>";
        }

        private int GetMonthsInPeriod(DateTime startDate, DateTime endDate)
        {
            return (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month + 1;
        }

        #endregion
        #endregion

        #region Helper Classes

        private class OccupancyReportData
        {
            public string PropertyName { get; set; }
            public PropertyType PropertyType { get; set; }
            public int TotalPortions { get; set; }
            public int OccupiedPortions { get; set; }
            public int VacantPortions { get; set; }
            public decimal OccupancyRate { get; set; }
            public List<PortionOccupancy> Portions { get; set; }
        }

        private class PortionOccupancy
        {
            public string PortionName { get; set; }
            public string PortionSize { get; set; }
            public bool IsOccupied { get; set; }
            public string TenantName { get; set; }
        }

        private class TenantLedgerEntry
        {
            public DateTime Date { get; set; }
            public string Description { get; set; }
            public decimal CreditAmount { get; set; }
            public decimal DebitAmount { get; set; }
            public decimal Balance { get; set; }
            public string Type { get; set; }
        }

        #endregion

        #region Report Opening Methods

        public void OpenReportInBrowser(string htmlContent, string reportName)
        {
            try
            {
                var fileName = $"{reportName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.html";
                var filePath = Path.Combine(_reportDirectory, fileName);
                File.WriteAllText(filePath, htmlContent, Encoding.UTF8);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GenerateAndOpenMonthlySummary()
        {
            var html = GenerateMonthlySummary(DateTime.Now);
            OpenReportInBrowser(html, "Monthly_Summary");
        }

        public void GenerateAndOpenDueReport()
        {
            var html = GenerateDueReport();
            OpenReportInBrowser(html, "Due_Report");
        }

        public void GenerateAndOpenTenantPaymentHistory(int tenantId)
        {
            var html = GenerateTenantPaymentHistory(tenantId);
            OpenReportInBrowser(html, $"Tenant_Payment_History_{tenantId}");
        }

        public void GenerateAndOpenTenantLedger(int tenantId)
        {
            var html = GenerateTenantLedgerReport(tenantId);
            OpenReportInBrowser(html, $"Tenant_Ledger_{tenantId}");
        }

        public void GenerateAndOpenTenantDueStatement(int tenantId)
        {
            var html = GenerateTenantDueStatement(tenantId);
            OpenReportInBrowser(html, $"Tenant_Due_Statement_{tenantId}");
        }

        public void GenerateAndOpenPropertySummary()
        {
            var html = GeneratePropertySummaryReport();
            OpenReportInBrowser(html, "Property_Summary");
        }

        public void GenerateAndOpenOccupancyReport()
        {
            var html = GenerateOccupancyReport();
            OpenReportInBrowser(html, "Occupancy_Report");
        }

        public void GenerateAndOpenPropertyIncomeReport()
        {
            var html = GeneratePropertyIncomeReport();
            OpenReportInBrowser(html, "Property_Income_Report");
        }

        public void GenerateAndOpenCollectionEfficiencyReport()
        {
            var html = GenerateCollectionEfficiencyReport();
            OpenReportInBrowser(html, "Collection_Efficiency_Report");
        }

        public void GenerateAndOpenRevenueAnalysisReport()
        {
            var html = GenerateRevenueAnalysisReport();
            OpenReportInBrowser(html, "Revenue_Analysis_Report");
        }

        public void GenerateAndOpenOutstandingDuesReport()
        {
            var html = GenerateOutstandingDuesReport();
            OpenReportInBrowser(html, "Outstanding_Dues_Report");
        }

        public void GenerateAndOpenCommissionSummaryReport()
        {
            var html = GenerateCommissionSummaryReport();
            OpenReportInBrowser(html, "Commission_Summary_Report");
        }

        public void GenerateAndOpenProductSalesReport()
        {
            var html = GenerateProductSalesReport();
            OpenReportInBrowser(html, "Product_Sales_Report");
        }

        public void GenerateAndOpenCommissionDueReport()
        {
            var html = GenerateCommissionDueReport();
            OpenReportInBrowser(html, "Commission_Due_Report");
        }

        #endregion
    }
}