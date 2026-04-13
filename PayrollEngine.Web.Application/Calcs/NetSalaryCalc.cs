using System;

namespace PayrollEngine.Web.Application.Calcs;

public class NetSalaryCalc
{

    public decimal Calc(decimal grossSalary, decimal ssContribution, decimal uıContribution, decimal incomeTax, decimal stampTax)
    {
        decimal netSalary = grossSalary - (ssContribution + uıContribution + incomeTax + stampTax);
        return Math.Round(netSalary, 2);
    }

}
