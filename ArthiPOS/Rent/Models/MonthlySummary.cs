using ShopRentManagementSystem.Models;
using System;
using System.Collections.Generic;

public class MonthlySummary
{
    public string MonthYear { get; set; } // Format: "January 2024"
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
    public decimal CollectionEfficiency { get; set; } // Percentage
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
    public decimal OccupancyRate { get; set; } // Percentage
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
    public string Status { get; set; } // "Overdue", "Due Soon", "Current"
}

public class DueProperty
{
    public string PropertyName { get; set; }
    public int DueTenantsCount { get; set; }
    public decimal TotalDueAmount { get; set; }
    public decimal AverageDuePerTenant { get; set; }
}