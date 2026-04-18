using System;
using PayrollEngine.Web.Application.Calcs;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.Application.Calcs;

public class ResultPayrollCalc
{

    private readonly IScenarioService _scenarioService;
    private readonly IResultPayrollService _resultPayrollService;
  

    private readonly SSContributionBaseCalc _ssContributionBaseCalc;
    private readonly EmployeeSSContributionCalc _employeeSSContributionCalc;
    private readonly EmployeeUIContributionCalc _employeeUIContributionCalc;
    private readonly IncomeTaxBaseCalc _incomeTaxBaseCalc;
    private readonly CumulativeIncomeTaxBaseCalc _cumulativeIncomeTaxBaseCalc;
    private readonly IncomeTaxCalc _incomeTaxCalc;
    private readonly StampTaxCalc _stampTaxCalc;
    private readonly NetSalaryCalc _netSalaryCalc;
    private readonly IncomeTaxExemptionCalc _incomeTaxExemptionCalc;
    private readonly StampTaxExemptionCalc _stampTaxExemptionCalc;
    


    public ResultPayrollCalc(IScenarioService scenarioService, 
                             IResultPayrollService resultPayrollService, 
                             SSContributionBaseCalc ssContributionBaseCalc,
                             EmployeeSSContributionCalc employeeSSContributionCalc,
                             EmployeeUIContributionCalc employeeUIContributionCalc,
                             IncomeTaxBaseCalc incomeTaxBaseCalc,
                             CumulativeIncomeTaxBaseCalc cumulativeIncomeTaxBaseCalc,
                             IncomeTaxCalc incomeTaxCalc,
                             StampTaxCalc stampTaxCalc,
                             NetSalaryCalc netSalaryCalc,
                             IncomeTaxExemptionCalc incomeTaxExemptionCalc,
                             StampTaxExemptionCalc stampTaxExemptionCalc)
    {
        _scenarioService = scenarioService;
        _resultPayrollService = resultPayrollService;

        _ssContributionBaseCalc = ssContributionBaseCalc;
        _employeeSSContributionCalc = employeeSSContributionCalc;
        _employeeUIContributionCalc = employeeUIContributionCalc;
        _incomeTaxBaseCalc = incomeTaxBaseCalc;
        _cumulativeIncomeTaxBaseCalc = cumulativeIncomeTaxBaseCalc;
        _incomeTaxCalc = incomeTaxCalc;
        _stampTaxCalc = stampTaxCalc;
        _netSalaryCalc = netSalaryCalc;
        _incomeTaxExemptionCalc = incomeTaxExemptionCalc;
        _stampTaxExemptionCalc = stampTaxExemptionCalc;
    }

    public async Task<ResultPayroll> Calc(PayrollMonth month)
    {
        var scenario = await _scenarioService.Get();

       

        decimal baseSalary = month.BaseSalary;
        decimal overtime_50_amount = month.Overtime_50_Amount;
        decimal overtime_100_amount = month.Overtime_100_Amount;
        decimal bonus = month.Bonus;
        decimal totalSalary = baseSalary + overtime_50_amount + overtime_100_amount + bonus;
       
        ResultPayroll result = new ResultPayroll();
        result.Month = month.Month;
        result.WorkDays = month.WorkDays;
        result.BaseSalary = month.BaseSalary;
        result.Overtime_50_Amount = overtime_50_amount;
        result.Overtime_100_Amount = overtime_100_amount;
        result.Bonus = bonus;
        result.TotalSalary = totalSalary;
        
        result.GrossSalary = result.TotalSalary;

        decimal grossSalary = result.GrossSalary;
        decimal SSContributionBase = await _ssContributionBaseCalc.Calc(scenario.Year, grossSalary);
        decimal employeeSSContributionAmount = await _employeeSSContributionCalc.Calc(scenario.Year, scenario.Status, SSContributionBase);
        decimal employeeUIContributionAmount = await _employeeUIContributionCalc.Calc(scenario.Year, scenario.Status, SSContributionBase);
        decimal incomeTaxBase = _incomeTaxBaseCalc.CalcNormal(grossSalary, employeeSSContributionAmount, employeeUIContributionAmount);
        decimal reducedIncomeTaxBase = await _incomeTaxBaseCalc.Calc(grossSalary, scenario.Year, scenario.DisabilityDegree, employeeSSContributionAmount, employeeUIContributionAmount);
        decimal cumulativeIncomeTaxBase = await _cumulativeIncomeTaxBaseCalc.Calc(month.Month, reducedIncomeTaxBase);
        var taxRate = await _incomeTaxCalc.Calc(scenario.Year, cumulativeIncomeTaxBase, reducedIncomeTaxBase);
        decimal incometaxExemption = await _incomeTaxExemptionCalc.Calc(scenario.Year, month.Month);
        
        decimal incomeTax = Math.Max(0, taxRate.tax - incometaxExemption);
        decimal incomeTaxRate = taxRate.rate;

        decimal stampTaxExemption = await _stampTaxExemptionCalc.Calc(scenario.Year);
        decimal stampTax = Math.Max(0, await _stampTaxCalc.Calc(scenario.Year, grossSalary) - stampTaxExemption);
        decimal netSalary = _netSalaryCalc.Calc(grossSalary, employeeSSContributionAmount, employeeUIContributionAmount, incomeTax, stampTax);

        result.SSContributionBase = SSContributionBase;
        result.EmployeeSSContributionAmount = employeeSSContributionAmount;
        result.EmployeeUIContributionAmount = employeeUIContributionAmount;
        result.IncomeTaxBase = incomeTaxBase;
        result.ReducedIncomeTaxBase = reducedIncomeTaxBase;
        result.CumulativeIncomeTaxBase = cumulativeIncomeTaxBase;
        result.IncomeTax = incomeTax;
        result.IncomeTaxExemption = incometaxExemption;
        result.IncomeTaxRate = incomeTaxRate;
        result.StampTax = stampTax;
        result.StampTaxExemption = stampTaxExemption;
        result.NetSalary = netSalary;

        result.ShoppingVoucherNet = month.ShoppingVoucher;
        result.ShoppingVoucherIncomeTax = 0;
        result.ShoppingVoucherStampTax = 0;
        result.ShoppingVoucherGrossAmount = 0;

        result.EmployerSSContributionAmount = 0;
        result.EmployerUIContributionAmount = 0;
        result.TotalEmployerCost = 0;
        return result;

    }


    public async Task SaveResult(ResultPayroll result)
    {
        await _resultPayrollService.Add(result);
    }


    public async Task<ResultPayroll> Iteration(PayrollMonth month)
    {   
        decimal targetNetSalary = month.BaseSalary + month.Overtime_50_Amount + month.Overtime_100_Amount + month.Bonus;
        decimal difference = 0;
        decimal extraNet = 0;
        
        PayrollMonth TempResult = new PayrollMonth
        {
            Month = month.Month,
            BaseSalary = month.BaseSalary,
            Overtime_50_Amount = month.Overtime_50_Amount,
            Overtime_100_Amount = month.Overtime_100_Amount,
            Bonus = month.Bonus,
           
        };

        ResultPayroll calculatedResult = new ResultPayroll();

        for (int i = 0; i < 200; i++)
        {
            calculatedResult = await Calc(TempResult);


            if(calculatedResult.IncomeTaxBase - calculatedResult.ReducedIncomeTaxBase > 0)
            {
                extraNet = (calculatedResult.IncomeTaxBase - calculatedResult.ReducedIncomeTaxBase) * calculatedResult.IncomeTaxRate;
            }
            else
            {
                extraNet = 0;
            }

            decimal calculatedNetSalary = calculatedResult.NetSalary - extraNet;
            difference = targetNetSalary - calculatedNetSalary;


            if (Math.Abs(difference) < 0.01m)
            {   
                
                break;
            }
            else
            {
                TempResult.BaseSalary += difference;
            }

           
        }

        
        
        return calculatedResult;
    }


}
