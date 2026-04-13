using System;
using PayrollEngine.Web.Application.Services.Params;

namespace PayrollEngine.Web.Application.Calcs;

public class EmployerUIContributionAmountCalc
{
    private readonly SSParamsService _ssParamsService;

    public EmployerUIContributionAmountCalc(SSParamsService sSParamsService)
    {
        _ssParamsService = sSParamsService;
    }

    public async Task<decimal> Calc(int year, decimal GrossSalary)
    {
        var ssParams = await _ssParamsService.Get(year);
        decimal result = GrossSalary * ssParams.ActiveEmployerUIRate;
        return Math.Round(result, 2);    
       
    }
}
