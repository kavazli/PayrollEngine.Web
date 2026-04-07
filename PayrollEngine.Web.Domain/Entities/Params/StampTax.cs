using System;

namespace PayrollEngine.Web.Domain.Entities.Params;

public class StampTax
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public decimal Rate { get; set; }
}
