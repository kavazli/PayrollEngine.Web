using System;
using System.Threading.Tasks;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Entities.Params;
using DisabilityDegreeEnum = PayrollEngine.Web.Domain.Enums.DisabilityDegree;

namespace PayrollEngine.Web.Application.Calcs;

public class IncomeTaxBaseCalc
{   
    private readonly DisabilityDegreeService _disabilityDegreeService;

    public IncomeTaxBaseCalc(DisabilityDegreeService disabilityDegreeService)
    {
        _disabilityDegreeService = disabilityDegreeService;
    }

    public async Task<decimal> Calc(decimal grossSalary, int year, DisabilityDegreeEnum disabilityDegree, decimal ssContribution, decimal uiContribution)
    {   
        decimal result = grossSalary - (ssContribution + uiContribution);

        if(disabilityDegree != DisabilityDegreeEnum.Normal)
        {
            var degrees = await _disabilityDegreeService.Get(year);
            var degreeParam = degrees.FirstOrDefault(x => x.Degree == disabilityDegree);
            
            if(degreeParam != null)
            {
                result -= degreeParam.Amount;
            }
        }

        return Math.Round(result, 2);

    }

}
