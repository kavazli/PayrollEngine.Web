using System;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Domain.Enums;

namespace PayrollEngine.Web.Application.Calcs;

public class EmployeeSSContributionCalc
{
    private readonly SSParamsService _ssParamsService;
    private readonly MinimumWageService _minimumWageService;


    public EmployeeSSContributionCalc(SSParamsService sSParamsService, MinimumWageService minimumWageService)
    {
        _ssParamsService = sSParamsService;
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
            result = ssContributionBase * ssParams.ActiveEmployeeSSRate;
        }
        else
        {   
            if(ssContributionBase < minimumWage.GrossSalary)
            {
                ssContributionBase = minimumWage.GrossSalary;
            }
            result = ssContributionBase * ssParams.RetiredEmployeeSSRate;
        }
        
        return Math.Round(result, 2);

    }



}
