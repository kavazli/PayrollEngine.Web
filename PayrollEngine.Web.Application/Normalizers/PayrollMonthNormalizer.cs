using System;
using PayrollEngine.Web.Domain.Entities;

namespace PayrollEngine.Web.Application.Normalizers;

public class PayrollMonthNormalizer
{


    public PayrollMonth Normalize(PayrollTemplateMonth templateMonth)
    {    

          decimal _BaseSalary = BaseSalaryCalc(templateMonth.BaseSalary, templateMonth.SalaryIncreaseRate, templateMonth.WorkDays);
          decimal _Overtime_50_Amount = Overtime_50_Calc(_BaseSalary, templateMonth.Overtime_50, templateMonth.WorkDays);
          decimal _Overtime_100_Amount = Overtime_100_Calc(_BaseSalary, templateMonth.Overtime_100, templateMonth.WorkDays);     


        return new PayrollMonth
        {
            Month = templateMonth.Month,
            WorkDays = templateMonth.WorkDays,
            BaseSalary = _BaseSalary,
            Overtime_50_Amount = _Overtime_50_Amount,
            Overtime_100_Amount = _Overtime_100_Amount,
            Bonus = templateMonth.Bonus,
            ShoppingVoucher = templateMonth.ShoppingVoucher
        };
    }

    private decimal BaseSalaryCalc(Decimal baseSalary, decimal salaryIncreaseRate, int WorkDays)
    {   
        decimal _baseSalary = (baseSalary / 30) * WorkDays;

        decimal Result = _baseSalary + (_baseSalary * salaryIncreaseRate / 100);

        return Math.Round(Result, 2);
    }


    private decimal Overtime_50_Calc(decimal baseSalary, decimal overtime_50, int WorkDays)
    {   
        decimal _baseSalary = (baseSalary / WorkDays) * 30  ;

        decimal Result = ((_baseSalary / 225) * 1.5m ) * overtime_50;

        return Math.Round(Result, 2);
    }

    private decimal Overtime_100_Calc(decimal baseSalary, decimal overtime_100, int WorkDays)
    {   
        decimal _baseSalary = (baseSalary / WorkDays) * 30  ;

        decimal Result = ((_baseSalary / 225) * 2m ) * overtime_100;

        return Math.Round(Result, 2);
    }

}