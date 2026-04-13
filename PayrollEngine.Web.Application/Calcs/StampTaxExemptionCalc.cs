using System;
using System.Runtime.CompilerServices;
using PayrollEngine.Web.Application.Services.Params;

namespace PayrollEngine.Web.Application.Calcs;

public class StampTaxExemptionCalc
{
    private readonly MinimumWageService _minimumWageService;
    private readonly StampTaxService _stampTaxService;

    public StampTaxExemptionCalc(MinimumWageService minimumWageService, StampTaxService stampTaxService)
    {
        _minimumWageService = minimumWageService;
        _stampTaxService = stampTaxService;
    }


    public async Task<decimal> Calc(int year)
    {
        var minimumWage = await _minimumWageService.Get(year);
        var stampTax = await _stampTaxService.Get(year);

        decimal result = minimumWage.GrossSalary * stampTax.Rate;

        return Math.Round(result, 2);
    }

}
