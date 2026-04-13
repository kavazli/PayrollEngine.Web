using System;

namespace PayrollEngine.Web.Application.Calcs;

public class TotalEmployerCostCalc
{

    public decimal Calc(decimal grossSalary, decimal ShoppingWoucherGross ,decimal employerSSContribution, decimal employerUIContribution)
    {
        return Math.Round(grossSalary + ShoppingWoucherGross + employerSSContribution + employerUIContribution, 2);
    }

}
