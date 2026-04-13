using System;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.Application.Calcs;

public class ShoppingVoucherStampTaxCalc
{
    private readonly StampTaxService _stampTaxService;
    private readonly IScenarioService _scenarioService;

    public ShoppingVoucherStampTaxCalc(StampTaxService stampTaxService, IScenarioService scenarioService)
    {
        _stampTaxService = stampTaxService;
        _scenarioService = scenarioService;
    }

    public async Task<decimal> Calc(int year, int month, decimal shoppingVoucherGross)
    {   
        var scenario = await _scenarioService.Get();
        var stampTaxParams = await _stampTaxService.Get(year);
        
        decimal result = shoppingVoucherGross * stampTaxParams.Rate;
        return Math.Round(result, 2);
    }
}
