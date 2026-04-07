using System;

namespace PayrollEngine.Web.Domain.Entities;

public class MinimumWage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Year { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal NetSalary { get; set; }
    public decimal RetiredNetSalary { get; set; }

}
