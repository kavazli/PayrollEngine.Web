using System;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.Application.Calcs;


public class ShoppingVoucherCalc
{   

    private readonly StampTaxService _stampTaxService;
    private readonly IScenarioService _scenarioService;

    public ShoppingVoucherCalc(StampTaxService stampTaxService, IScenarioService scenarioService)
    {
        _stampTaxService = stampTaxService;
        _scenarioService = scenarioService;
    }

    public async Task<(decimal, decimal, decimal)> Calc(decimal woucherGross, decimal rate)
    {   
        var scenario = await _scenarioService.Get();
        var stampTaxParams = await _stampTaxService.Get(scenario.Year);

        decimal taxAmount = Math.Round(woucherGross * rate, 2);
        decimal stampTaxAmount = Math.Round(woucherGross * stampTaxParams.Rate, 2);
        decimal netWoucher = Math.Round(woucherGross - taxAmount - stampTaxAmount, 2);
        
    
        return (taxAmount, stampTaxAmount, netWoucher);
    }


    public async Task<(decimal, decimal, decimal)> Iteration(decimal woucherGross, decimal rate)
    {   
        decimal targetWoucher = woucherGross;
        decimal difference;
        var temp = (0m, 0m, 0m);

        for(int i = 0; i < 100; i++)
        {
            temp = await Calc(woucherGross, rate);
            difference = Math.Abs(targetWoucher - temp.Item3);

            if(difference <= 0.001m)
            {
                return temp;
            }

            woucherGross += (targetWoucher - temp.Item3);
        }

        decimal taxAmount = temp.Item1;
        decimal stampTaxAmount = temp.Item2;    
        decimal netWoucher = temp.Item3 + 0.01m;
        return (taxAmount, stampTaxAmount, netWoucher);
    }

    
}