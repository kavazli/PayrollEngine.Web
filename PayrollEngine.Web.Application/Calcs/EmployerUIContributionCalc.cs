using System;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Enums;

namespace PayrollEngine.Web.Application.Calcs;

public class EmployerUIContributionCalc
{
    private readonly SSParamsService _ssParamsService;
    private readonly MinimumWageService _minimumWageService;

    public EmployerUIContributionCalc(SSParamsService sSParamsService, MinimumWageService minimumWageService)
    {
        _ssParamsService = sSParamsService;
        _minimumWageService = minimumWageService;
    }

    public async Task<decimal> Calc(int year, Status status, decimal ssContributionBase)
    {
        var ssParams = await _ssParamsService.Get(year);
        var minimumWage = await _minimumWageService.Get(year);
        decimal result;


        if(ssContributionBase < minimumWage.GrossSalary)
        {
            ssContributionBase = minimumWage.GrossSalary;
        }

        if(Status.Active == status)
        {
            result = ssContributionBase * ssParams.ActiveEmployerUIRate;
            
        }
        else
        {
            result = 0m;
        }

        
        
        return Math.Round(result, 2);    
       
    }
}
