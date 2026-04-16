using System;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Domain.Enums;

namespace PayrollEngine.Web.Application.Calcs;

public class EmployeeUIContributionCalc
{
    private readonly SSParamsService _ssParamsService;
    private readonly MinimumWageService _minimumWageService;


    public EmployeeUIContributionCalc(SSParamsService ssParamsService, MinimumWageService minimumWageService)
    {
        _ssParamsService = ssParamsService;
        _minimumWageService = minimumWageService;
    }


    public async Task<decimal> Calc(int year, Status status, decimal ssContributionBase)
    {
        var ssParams = await _ssParamsService.Get(year);
        var minimumWage = await _minimumWageService.Get(year);
        decimal result;

        if(status == Status.Active)
        {
            if(ssContributionBase < minimumWage.GrossSalary)
            {
                ssContributionBase = minimumWage.GrossSalary;
            }
            result = ssContributionBase * ssParams.ActiveEmployeeUIRate;
        }
        else
        {
            result = 0;
        }

        return Math.Round(result, 2);
    }


}
