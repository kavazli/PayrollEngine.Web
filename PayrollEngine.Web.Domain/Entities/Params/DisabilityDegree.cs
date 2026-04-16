using System;
using DisabilityDegreeEnum = PayrollEngine.Web.Domain.Enums.DisabilityDegree;

namespace PayrollEngine.Web.Domain.Entities.Params;

public class DisabilityDegree
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public DisabilityDegreeEnum? Degree { get; set; }
    public decimal Amount { get; set; }

}
