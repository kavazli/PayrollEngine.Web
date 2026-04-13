using System;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Domain.Enums;

namespace PayrollEngine.Web.Application.Calcs;

public class EmployeeUIContributionCalc
{
    private readonly SSParamsService _ssParamsService;


    public EmployeeUIContributionCalc(SSParamsService ssParamsService)
    {
        _ssParamsService = ssParamsService;     
    }


    public async Task<decimal> Calc(int year, Status status, decimal ssContributionBase)
    {
        var ssParams = await _ssParamsService.Get(year);
        decimal result;

        if(status == Status.Active)
        {
            result = ssContributionBase * ssParams.ActiveEmployeeUIRate;
        }
        else
        {
            result = 0;
        }

        return Math.Round(result, 2);
    }


}
