using System;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Domain.Enums;

namespace PayrollEngine.Web.Application.Calcs;

public class EmployeeSSContributionCalc
{
    private readonly SSParamsService _ssParamsService;


    public EmployeeSSContributionCalc(SSParamsService sSParamsService)
    {
        _ssParamsService = sSParamsService;
    }


    public async Task<decimal> Calc(int year, Status status, decimal ssContributionBase)
    {
        var ssParams = await _ssParamsService.Get(year);
        decimal result;


        if(status == Status.Active)
        {
            result = ssContributionBase * ssParams.ActiveEmployeeSSRate;
        }
        else
        {
            result = ssContributionBase * ssParams.RetiredEmployeeSSRate;
        }
        
        return Math.Round(result, 2);

    }



}
