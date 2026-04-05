using System;
using PayrollEngine.Web.Domain.Enums;

namespace PayrollEngine.Web.Domain.Entities;

public class PayrollTemplateMonth
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Months Month { get; set; }
    public int WorkDays { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal SalaryIncreaseRate { get; set; }
    public decimal Overtime_50 { get; set; }
    public decimal Overtime_100 { get; set; }
    public decimal Bonus { get; set; }
    public decimal ShoppingVoucher { get; set; }
}
