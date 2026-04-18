using System;
using System.Threading.Tasks;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Entities.Params;
using DisabilityDegreeEnum = PayrollEngine.Web.Domain.Enums.DisabilityDegree;

namespace PayrollEngine.Web.Application.Calcs;

public class IncomeTaxBaseCalc
{   
    private readonly DisabilityDegreeService _disabilityDegreeService;
    private readonly MinimumWageService _minimumWageService;

    public IncomeTaxBaseCalc(DisabilityDegreeService disabilityDegreeService, MinimumWageService minimumWageService)
    {
        _disabilityDegreeService = disabilityDegreeService;
        _minimumWageService = minimumWageService;
    }

    public async Task<decimal> Calc(decimal grossSalary, int year, DisabilityDegreeEnum disabilityDegree, decimal ssContribution, decimal uiContribution)
    {   
        decimal result = grossSalary - (ssContribution + uiContribution);

        if(disabilityDegree != DisabilityDegreeEnum.Normal)
        {
            var degrees = await _disabilityDegreeService.Get(year);
            var degreeParam = degrees.FirstOrDefault(x => x.Degree == disabilityDegree);
            var minimumWage = await _minimumWageService.Get(year);

            if(result <= minimumWage.NetSalary)
            {
                return Math.Round(result, 2);
            }
            else if(result >= minimumWage.NetSalary + degreeParam.Amount)
            {   
                result -= degreeParam.Amount;
            }
            else if(result > minimumWage.NetSalary && result < minimumWage.NetSalary + degreeParam.Amount)
            {   
                decimal tempResult = result - minimumWage.NetSalary;
                result -= tempResult;
                
            }
             
        }

        return Math.Round(result, 2);

    }           

    public decimal CalcNormal(decimal grossSalary, decimal ssContribution, decimal uiContribution)
    {
        return Math.Round(grossSalary - (ssContribution + uiContribution), 2);
    }

}
