using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopRentManagementSystem.Models
{
    // Property Types
    public enum PropertyType
    {
        Commercial,
        NonCommercial
    }

    // Tenant Types
    public enum TenantType
    {
        OnRent,
        OnCommission
    }

    // Commission Payment Frequencies
    public enum CommissionFrequency
    {
        Daily,
        Every5Days,
        Every10Days,
        Weekly,
        Monthly,
        Custom
    }

    // Rent Increase Mode
    public enum RentIncreaseMode
    {
        Auto,
        Manual
    }

    // Payment Types
    public enum PaymentType
    {
        Rent,
        Commission,
        SecurityDeposit,
        Other
    }

    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public PropertyType Type { get; set; } = PropertyType.Commercial;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    public class Portion
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; } // e.g., "5x10"
        public bool IsActive { get; set; } = true;
    }

    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string Mobile { get; set; }
        public decimal SecurityDeposit { get; set; }
        public string StampPaperDetails { get; set; }
        public DateTime StampPaperDate { get; set; }
        public TenantType Type { get; set; } = TenantType.OnRent;
        public decimal? CommissionPercentage { get; set; } // For commission tenants
        public CommissionFrequency? CommissionFrequency { get; set; }
        public int? CustomCommissionDays { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; } // kg, piece, box, etc.
        public decimal UnitPrice { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }

    public class RentAgreement
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int PortionId { get; set; }
        public int TenantId { get; set; }
        public decimal MonthlyRent { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime? NextDueDate { get; set; }
        public RentIncreaseMode IncreaseMode { get; set; } = RentIncreaseMode.Auto;
        public DateTime LastIncreaseDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Commission-specific fields
        public decimal? DailyMinimumTarget { get; set; }
        public decimal? CommissionRate { get; set; } // Percentage
        public CommissionFrequency? PaymentFrequency { get; set; }
        public int? CustomPaymentDays { get; set; }
        public DateTime? LastCommissionPaymentDate { get; set; }
        public List<int> ProductIds { get; set; } = new List<int>(); // Products for this agreement
    }

    public class Payment
    {
        public int Id { get; set; }
        public int AgreementId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string MonthYear { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }
        public PaymentType PaymentType { get; set; } = PaymentType.Rent;

        // Commission-specific fields
        public decimal? SalesAmount { get; set; }
        public decimal? CommissionEarned { get; set; }

        // New fields for product details
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Quantity { get; set; }
        public string Unit { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? LaborAmount { get; set; }
        public decimal? ProductTotal { get; set; }
    }

    public class CommissionTransaction
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LaborAmount { get; set; }
        public decimal ProductTotal { get; set; }
        public decimal CommissionRate { get; set; }
        public decimal CommissionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class RentOverview
    {
        public string PropertyName { get; set; }
        public string PortionName { get; set; }
        public string PortionSize { get; set; }
        public string TenantName { get; set; }
        public string Mobile { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal LastPaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public DateTime NextDueDate { get; set; }
        public int AgreementId { get; set; }
        public int TenantId { get; set; }
        public int PropertyId { get; set; }
        public int PortionId { get; set; }
        public string StatusColor { get; set; } = "Green";
        public TenantType TenantType { get; set; }
        public decimal? CommissionDue { get; set; }
        public DateTime? NextCommissionDueDate { get; set; }
        public string PaymentInfo { get; set; }
        public int DaysOverdue { get; set; }
    }

    // Report Models
    public class MonthlySummary
    {
        public string MonthYear { get; set; }
        public DateTime MonthStart { get; set; }
        public DateTime MonthEnd { get; set; }
        public int TotalProperties { get; set; }
        public int TotalTenants { get; set; }
        public int TotalRentTenants { get; set; }
        public int TotalCommissionTenants { get; set; }
        public decimal TotalRentCollected { get; set; }
        public decimal TotalCommissionCollected { get; set; }
        public decimal TotalCollected { get; set; }
        public decimal TotalRentDue { get; set; }
        public decimal TotalCommissionDue { get; set; }
        public decimal TotalDue { get; set; }
        public decimal CollectionEfficiency { get; set; }
        public List<MonthlyPropertySummary> PropertySummaries { get; set; } = new List<MonthlyPropertySummary>();
    }

    public class MonthlyPropertySummary
    {
        public string PropertyName { get; set; }
        public PropertyType PropertyType { get; set; }
        public int TotalPortions { get; set; }
        public int OccupiedPortions { get; set; }
        public decimal TotalRentCollected { get; set; }
        public decimal TotalCommissionCollected { get; set; }
        public decimal TotalDue { get; set; }
        public decimal OccupancyRate { get; set; }
    }

    public class DueReport
    {
        public DateTime ReportDate { get; set; }
        public int TotalDueTenants { get; set; }
        public decimal TotalDueAmount { get; set; }
        public List<DueTenant> DueTenants { get; set; } = new List<DueTenant>();
        public List<DueProperty> DueProperties { get; set; } = new List<DueProperty>();
    }

    public class DueTenant
    {
        public string TenantName { get; set; }
        public string Mobile { get; set; }
        public string PropertyName { get; set; }
        public string PortionName { get; set; }
        public TenantType TenantType { get; set; }
        public decimal DueAmount { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public DateTime NextDueDate { get; set; }
        public int DaysOverdue { get; set; }
        public string Status { get; set; }
    }

    public class DueProperty
    {
        public string PropertyName { get; set; }
        public int DueTenantsCount { get; set; }
        public decimal TotalDueAmount { get; set; }
        public decimal AverageDuePerTenant { get; set; }
    }

    // Helper class for commission summary
    public class CommissionSummary
    {
        public decimal TotalQuantity { get; set; }
        public decimal TotalProductValue { get; set; }
        public decimal TotalLabor { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalCommission { get; set; }
    }

    // Helper class for product selection
    public class ProductSelection
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsSelected { get; set; }
    }
    public enum ExpenseCategory
    {
        [Display(Name = "Utilities")]
        Utilities,
        [Display(Name = "Maintenance")]
        Maintenance,
        [Display(Name = "Repairs")]
        Repairs,
        [Display(Name = "Insurance")]
        Insurance,
        [Display(Name = "Taxes")]
        Taxes,
        [Display(Name = "Cleaning")]
        Cleaning,
        [Display(Name = "Security")]
        Security,
        [Display(Name = "Marketing")]
        Marketing,
        [Display(Name = "Professional Fees")]
        ProfessionalFees,
        [Display(Name = "Supplies")]
        Supplies,
        [Display(Name = "Salaries")]
        Salaries,
        [Display(Name = "Equipment")]
        Equipment,
        [Display(Name = "Miscellaneous")]
        Miscellaneous
    }

    public class Expense
    {
        public int Id { get; set; }
        public string ExpenseNumber { get; set; }  // Auto-generated like EXP-2024-0001
        public DateTime ExpenseDate { get; set; }
        public ExpenseCategory Category { get; set; }
        public decimal Amount { get; set; }
        public string Payee { get; set; }  // Who was paid
        public string Description { get; set; }
        public string PaymentMethod { get; set; }  // Cash, Check, Bank Transfer, Credit Card
        public string ReferenceNumber { get; set; }  // Check number, transaction ID, etc.
        public int? PropertyId { get; set; }  // Optional - if expense is for specific property
        public string ReceiptPath { get; set; }  // Path to receipt image/file
        public bool IsTaxDeductible { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }

    public class ExpenseCategorySummary
    {
        public ExpenseCategory Category { get; set; }
        public string CategoryName { get; set; }
        public decimal TotalAmount { get; set; }
        public int TransactionCount { get; set; }
        public decimal AverageAmount { get; set; }
        public decimal PercentageOfTotal { get; set; }
    }

    public class ExpenseReport
    {
        public DateTime ReportDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Expense> Expenses { get; set; } = new List<Expense>();
        public List<ExpenseCategorySummary> CategorySummaries { get; set; } = new List<ExpenseCategorySummary>();
        public decimal TotalExpenses { get; set; }
        public int TotalTransactions { get; set; }
        public decimal AverageExpense { get; set; }
        public decimal LargestExpense { get; set; }
        public decimal SmallestExpense { get; set; }
        public Dictionary<string, decimal> MonthlyTotals { get; set; } = new Dictionary<string, decimal>();
        public Dictionary<ExpenseCategory, decimal> CategoryTotals { get; set; } = new Dictionary<ExpenseCategory, decimal>();
    }

    public class ExpenseDashboardSummary
    {
        public decimal TotalExpensesThisMonth { get; set; }
        public decimal TotalExpensesThisYear { get; set; }
        public decimal AverageMonthlyExpense { get; set; }
        public int ExpenseCountThisMonth { get; set; }
        public decimal LargestExpenseThisMonth { get; set; }
        public ExpenseCategory MostFrequentCategory { get; set; }
        public decimal MonthOverMonthChange { get; set; }  // Percentage change
        public List<Expense> RecentExpenses { get; set; } = new List<Expense>();
        public Dictionary<ExpenseCategory, decimal> CategoryBreakdown { get; set; } = new Dictionary<ExpenseCategory, decimal>();
        public Dictionary<string, decimal> Last6MonthsTotals { get; set; } = new Dictionary<string, decimal>();
    }

    public class ExpenseFilter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public ExpenseCategory? Category { get; set; }
        public int? PropertyId { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public string SearchText { get; set; }
        public bool? IsTaxDeductible { get; set; }
        public string PaymentMethod { get; set; }
    }

    // For attaching receipts
    public class ExpenseReceipt
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadedDate { get; set; }
    }
    public class CashFlowSummary
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCashIn { get; set; }
        public decimal TotalCashOut { get; set; }
        public decimal NetCashFlow { get; set; }
        public int CashInTransactions { get; set; }
        public int CashOutTransactions { get; set; }
        public Dictionary<int, YearlyCashFlow> YearlyData { get; set; } = new Dictionary<int, YearlyCashFlow>();
        public List<CashFlowDetail> RecentTransactions { get; set; } = new List<CashFlowDetail>();
    }

    public class YearlyCashFlow
    {
        public int Year { get; set; }
        public decimal YearlyCashIn { get; set; }
        public decimal YearlyCashOut { get; set; }
        public decimal YearlyNetCashFlow { get; set; }
        public Dictionary<int, MonthlyCashFlow> MonthlyData { get; set; } = new Dictionary<int, MonthlyCashFlow>();
    }

    public class MonthlyCashFlow
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public decimal MonthlyCashIn { get; set; }
        public decimal MonthlyCashOut { get; set; }
        public decimal MonthlyNetCashFlow { get; set; }
        public int CashInCount { get; set; }
        public int CashOutCount { get; set; }
        public List<CashFlowDetail> CashInDetails { get; set; } = new List<CashFlowDetail>();
        public List<CashFlowDetail> CashOutDetails { get; set; } = new List<CashFlowDetail>();
    }

    public class CashFlowDetail
    {
        public DateTime Date { get; set; }
        public string TransactionType { get; set; } // "Cash In" or "Cash Out"
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public string RelatedParty { get; set; } // Tenant name for payments, Payee for expenses
    }

    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DisplayText { get; set; }

        public DateRange()
        {
            StartDate = new DateTime(DateTime.Now.Year, 1, 1); // Jan 1 of current year
            EndDate = DateTime.Now;
            DisplayText = "Custom Range";
        }

        public DateRange(DateTime start, DateTime end, string displayText = null)
        {
            StartDate = start;
            EndDate = end;
            DisplayText = displayText ?? $"{start:dd-MMM-yyyy} to {end:dd-MMM-yyyy}";
        }

        public override string ToString()
        {
            return DisplayText;
        }
    }

}