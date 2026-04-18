using System;
using PayrollEngine.Web.Domain.Enums;

namespace PayrollEngine.Web.Domain.Entities;

public class ResultPayroll
{   

    public Guid Id { get; set; }
    public Months Month { get; set; }
    public int WorkDays { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal Overtime_50_Amount { get; set; }
    public decimal Overtime_100_Amount { get; set; }
    public decimal Bonus { get; set; }
    public decimal TotalSalary { get; set; }
   
    public decimal GrossSalary { get; set; }
    public decimal SSContributionBase { get; set; }
    public decimal EmployeeSSContributionAmount { get; set; }
    public decimal EmployeeUIContributionAmount { get; set; }
    public decimal IncomeTaxBase { get; set; }
    public decimal ReducedIncomeTaxBase { get; set; }
    public decimal CumulativeIncomeTaxBase { get; set; }
    public decimal IncomeTax { get; set; }
    public decimal IncomeTaxRate { get; set; }
    public decimal IncomeTaxExemption { get; set; }
    public decimal StampTax { get; set; }
    public decimal StampTaxExemption { get; set; }
    public decimal NetSalary { get; set; }

    public decimal ShoppingVoucherNet { get; set; }
    public decimal ShoppingVoucherIncomeTax { get; set; }
    public decimal ShoppingVoucherStampTax { get; set; }
    public decimal ShoppingVoucherGrossAmount { get; set; }

    public decimal EmployerSSContributionAmount { get; set; }
    public decimal EmployerUIContributionAmount { get; set; }
    public decimal TotalEmployerCost { get; set; }
    

}
