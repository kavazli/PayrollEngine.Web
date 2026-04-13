using System;
using PayrollEngine.Web.Application.Services.Params;

namespace PayrollEngine.Web.Application.Calcs;

public class SSContributionBaseCalc
{
    private readonly SSCeilingService _ssCeilingService;

    public SSContributionBaseCalc(SSCeilingService sSCeilingService)
    {
        _ssCeilingService = sSCeilingService;
    }


    public async Task<decimal> Calc(int year, decimal grossSalary)
    {   
        var ssCeiling = await _ssCeilingService.Get(year);
        decimal result;

        if(grossSalary <= ssCeiling.Ceiling)
        {
            result = grossSalary;
        }
        else
        {
            result = ssCeiling.Ceiling;
        }

        return Math.Round(result, 2);
    }

}
