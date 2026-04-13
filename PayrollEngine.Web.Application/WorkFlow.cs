using System;
using PayrollEngine.Web.Application.Calcs;
using PayrollEngine.Web.Domain.Enums;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.Application;

public class WorkFlow
{
    private readonly ResultPayrollCalc _resultPayrollCalc;
    private readonly ShoppingVoucherCalc _shoppingVoucherCalc;
    private readonly TotalEmployerCostCalc _totalEmployerCostCalc;
    private readonly EmployerSSContributionCalc _employerSSContributionCalc;
    private readonly EmployerUIContributionCalc _employerUIContributionCalc;
    private readonly IPayrollMonthsService _payrollMonthsService;
    private readonly IScenarioService _scenarioService;
    private readonly IResultPayrollService _resultPayrollService;

    public WorkFlow(ResultPayrollCalc resultPayrollCalc, 
                    ShoppingVoucherCalc shoppingVoucherCalc,
                    TotalEmployerCostCalc totalEmployerCostCalc, 
                    IPayrollMonthsService payrollMonthsService,
                    IScenarioService scenarioService, 
                    IResultPayrollService resultPayrollService,
                    EmployerSSContributionCalc employerSSContributionCalc,
                    EmployerUIContributionCalc employerUIContributionCalc)
    {
        _resultPayrollCalc = resultPayrollCalc;
        _shoppingVoucherCalc = shoppingVoucherCalc;
        _totalEmployerCostCalc = totalEmployerCostCalc;
        _payrollMonthsService = payrollMonthsService;
        _scenarioService = scenarioService;
        _resultPayrollService = resultPayrollService;
        _scenarioService = scenarioService;
        _employerSSContributionCalc = employerSSContributionCalc;
        _employerUIContributionCalc = employerUIContributionCalc;
    }


    public async Task ResultPayrollExecute()
    {   

        var scenario = await _scenarioService.Get();
        var payrollmonths = await _payrollMonthsService.Get();

        foreach (var payrollmonth in payrollmonths)
        {
            if(scenario.SalaryType == SalaryType.Gross)
            {
               var result = await _resultPayrollCalc.Calc(payrollmonth);
                await _resultPayrollCalc.SaveResult(result);
            }
            else if(scenario.SalaryType == SalaryType.Net)
            {
                var result = await _resultPayrollCalc.Iteration(payrollmonth);
                await _resultPayrollCalc.SaveResult(result);
            }
        }

    }

    public async Task ShoppingVoucherExecute()
    {
        var resultPayroll = await _resultPayrollService.Get();

        foreach (var item in resultPayroll)
        {
            if(item.ShoppingVoucherNet == 0)
            {
               continue;
            }
            else
            {
               var result = await _shoppingVoucherCalc.Iteration(item.ShoppingVoucherNet, item.IncomeTaxRate);

               decimal taxAmount = result.Item1;
               decimal stampTaxAmount = result.Item2;
               decimal netWoucher = result.Item3;

               item.ShoppingVoucherIncomeTax = taxAmount;
               item.ShoppingVoucherStampTax = stampTaxAmount; 
               item.ShoppingVoucherNet = netWoucher;
               item.ShoppingVoucherGrossAmount = taxAmount + stampTaxAmount + netWoucher;


                await _resultPayrollService.Update(item);

            }
        }

    }


    public async Task EmployerContributionExecute()
    {
        var resultPayroll = await _resultPayrollService.Get();
        var scenario = await _scenarioService.Get();

        int year = scenario.Year;
        Status status = scenario.Status;
        IncentiveType incentiveType = scenario.IncentiveType;
        Sector sector = scenario.Sector;

        foreach (var item in resultPayroll)
        {
           decimal SSresult = await _employerSSContributionCalc.Calc(year, status, incentiveType, sector, item.SSContributionBase);
           decimal UIresult = await _employerUIContributionCalc.Calc(year, item.SSContributionBase);
           item.EmployerSSContributionAmount = SSresult;
           item.EmployerUIContributionAmount = UIresult;

           await _resultPayrollService.Update(item);
        }

    }


    public async Task TotalEmployerCostExecute()
    {
        var resultPayroll = await _resultPayrollService.Get();

        foreach (var item in resultPayroll)
        {
           decimal result = item.GrossSalary + item.ShoppingVoucherGrossAmount + item.EmployerSSContributionAmount + item.EmployerUIContributionAmount;
           item.TotalEmployerCost = result;

            await _resultPayrollService.Update(item);
        }
    }
}
