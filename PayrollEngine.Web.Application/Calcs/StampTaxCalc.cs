using System;
using PayrollEngine.Web.Application.Services.Params;

namespace PayrollEngine.Web.Application.Calcs;

public class StampTaxCalc
{
    private readonly StampTaxService _stampTaxService;

    public StampTaxCalc(StampTaxService stampTaxService)
    {
        _stampTaxService = stampTaxService;
    }

    public async Task<decimal> Calc(int year, decimal grossSalary)
    {
        var stampTax = await _stampTaxService.Get(year);

        decimal result = grossSalary * stampTax.Rate;

        return Math.Round(result, 2);
    }


}
