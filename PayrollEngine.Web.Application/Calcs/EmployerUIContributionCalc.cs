using System;
using PayrollEngine.Web.Application.Services.Params;

namespace PayrollEngine.Web.Application.Calcs;

public class EmployerUIContributionCalc
{
    private readonly SSParamsService _ssParamsService;

    public EmployerUIContributionCalc(SSParamsService sSParamsService)
    {
        _ssParamsService = sSParamsService;
    }

    public async Task<decimal> Calc(int year, decimal ssContributionBase)
    {
        var ssParams = await _ssParamsService.Get(year);
        decimal result = ssContributionBase * ssParams.ActiveEmployerUIRate;
        return Math.Round(result, 2);    
       
    }
}
