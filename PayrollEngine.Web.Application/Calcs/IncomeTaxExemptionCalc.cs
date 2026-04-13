using System;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Domain.Enums;

namespace PayrollEngine.Web.Application.Calcs;

public class IncomeTaxExemptionCalc
{
    private readonly MinimumWageService _minimumWageService;
    private readonly IncomeTaxBracketService _incomeTaxBracketsService;

    public IncomeTaxExemptionCalc(MinimumWageService minimumWageService, IncomeTaxBracketService incomeTaxBracketsService)
    {
        _minimumWageService = minimumWageService;
        _incomeTaxBracketsService = incomeTaxBracketsService;
    }


    private async Task<IncomeTaxBrackets> TaxBrackets(int year)
    {   
        var brackets = await _incomeTaxBracketsService.Get(year);

        IncomeTaxBrackets incomeTaxBrackets = new IncomeTaxBrackets(brackets);
        return incomeTaxBrackets;
    }

    public async Task<decimal> Calc(int year, Months month)
    {   
        var minimumWage = await _minimumWageService.Get(year);
        var incomeTaxBrackets = await TaxBrackets(year);
        decimal result = 0;

        decimal exemptionBase = minimumWage.NetSalary * (int)month;
        

        if(exemptionBase <= incomeTaxBrackets.Brackets[0].MaxAmount)
        {
            result = minimumWage.NetSalary * incomeTaxBrackets.Brackets[0].Rate;
            return Math.Round(result, 2);
        }
        else if(exemptionBase > incomeTaxBrackets.Brackets[0].MaxAmount && (exemptionBase - incomeTaxBrackets.Brackets[0].MaxAmount) <= minimumWage.NetSalary )
        {
            decimal calc1 = (exemptionBase - incomeTaxBrackets.Brackets[0].MaxAmount) * incomeTaxBrackets.Brackets[1].Rate;
            decimal calc2 = (minimumWage.NetSalary - (exemptionBase - incomeTaxBrackets.Brackets[0].MaxAmount)) * incomeTaxBrackets.Brackets[0].Rate;
            result = calc1 + calc2;
            return Math.Round(result, 2);
        }
        else if(exemptionBase > incomeTaxBrackets.Brackets[0].MaxAmount && (exemptionBase - incomeTaxBrackets.Brackets[0].MaxAmount) > minimumWage.NetSalary)
        {
            result = minimumWage.NetSalary * incomeTaxBrackets.Brackets[1].Rate;
            return Math.Round(result, 2);
        }
        
        return Math.Round(result, 2);



    
        



    }

}
