using System;

namespace PayrollEngine.Web.Application.Calcs;

public class IncomeTaxBaseCalc
{

    public decimal Calc(decimal grossSalary, decimal ssContribution, decimal uiContribution)
    {
        decimal result = grossSalary - (ssContribution + uiContribution);

        return Math.Round(result, 2);

    }

}
