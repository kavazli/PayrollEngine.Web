using System;
using PayrollEngine.Web.Domain.Enums;

namespace PayrollEngine.Web.Domain.Entities;

public class PayrollMonth
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Months Month { get; set; }
    public int WorkDays { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal Overtime_50_Amount { get; set; }
    public decimal Overtime_100_Amount { get; set; }
    public decimal Bonus { get; set; }
    public decimal ShoppingVoucher { get; set; }
}
