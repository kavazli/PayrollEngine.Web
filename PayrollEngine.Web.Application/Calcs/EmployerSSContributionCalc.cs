using System;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.Application.Calcs;

public class EmployerSSContributionCalc
{
    private readonly SSParamsService _ssParamsService;
    private readonly IScenarioService _scenarioService;

    public EmployerSSContributionCalc(SSParamsService sSParamsService, IScenarioService scenarioService)
    {
        _ssParamsService = sSParamsService;
        _scenarioService = scenarioService;
    }

    public async Task<decimal> Calc(int year, decimal SSContributionBase)
    {
        
        
        return ;   
       
    }

    private async Task<decimal> incentiveCalc(int year, decimal ssContributionBase)
    {
        var scenario = await _scenarioService.Get();
        var ssParams = await _ssParamsService.Get(year);

    }
        
}
