using System;

namespace PayrollEngine.Web.Domain.Entities.Params;

public class SSCeiling
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public decimal Ceiling { get; set; }
}
