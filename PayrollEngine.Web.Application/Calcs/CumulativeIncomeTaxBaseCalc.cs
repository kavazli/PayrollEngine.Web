using System;
using PayrollEngine.Web.Domain.Enums;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.Application.Calcs;

public class CumulativeIncomeTaxBaseCalc
{
    private IResultPayrollService _resultPayrollService;

    public CumulativeIncomeTaxBaseCalc(IResultPayrollService resultPayrollService)
    {
        _resultPayrollService = resultPayrollService;
    }


    public async Task<decimal> Calc(Months month, decimal incomeTaxBase)
    {   
        int monthValue = (int)month;
        decimal result;

        if (monthValue == 1)
        {
            result = incomeTaxBase;
        }
        else
        {
            Months previousMonth = (Months)(monthValue - 1);
            var previousMonths = await _resultPayrollService.GetMonth(previousMonth);
            result = previousMonths.CumulativeIncomeTaxBase + incomeTaxBase;
        }


        return Math.Round(result, 2);
    }



}
