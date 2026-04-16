using System;
using PayrollEngine.Web.Domain.Enums;

namespace PayrollEngine.Web.Domain.Entities;

public class Scenario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Year { get; set; }
    public SalaryType SalaryType { get; set; }
    public Status Status { get; set; }
    public DisabilityDegree DisabilityDegree { get; set; }
    public PayType PayType { get; set; }
    public Sector Sector { get; set; }
    public IncentiveType IncentiveType { get; set; }
    
}
