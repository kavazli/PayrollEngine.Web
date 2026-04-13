using System;
using System.Threading.Tasks;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Enums;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.Application.Calcs;

public class EmployerSSContributionCalc
{
    private readonly SSParamsService _ssParamsService;
    private readonly MinimumWageService _minimumWageService;
    
    
   
    
    public EmployerSSContributionCalc(SSParamsService sSParamsService, MinimumWageService minimumWageService)
    {
        _ssParamsService = sSParamsService;
        _minimumWageService = minimumWageService;

    }


    public async Task<decimal> Calc(int year, Status status, IncentiveType incentiveType, Sector sector, decimal ssContributionBase)
    {
        
        var ssParams = await _ssParamsService.Get(year);
        var minWage = await _minimumWageService.Get(year);

        decimal employerSSAmount = 0;


        if(status == Status.Retired)
        {
            employerSSAmount = Math.Round(SgdpCalc(ssContributionBase, ssParams.RetiredEmployerSSRate), 2);
            return employerSSAmount;
        }
        
        if(status == Status.Active && incentiveType == IncentiveType.None)
        {
            employerSSAmount = Math.Round(Full(ssContributionBase, ssParams.ActiveEmployerSSRate), 2);
            return employerSSAmount;
        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code5510 && sector == Sector.Manufacturing)
        {
            employerSSAmount = Math.Round(_5510ManCalc(ssContributionBase, ssParams.ActiveEmployerSSRate), 2);
            return employerSSAmount;
        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code5510 && sector == Sector.Other)
        {
            employerSSAmount = Math.Round(_5510OtherCalc(ssContributionBase, ssParams.ActiveEmployerSSRate), 2);
            return employerSSAmount;
        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code14857 && sector == Sector.Manufacturing)
        {
            
            employerSSAmount = Math.Round(_14857ManCalc(ssContributionBase, ssParams.ActiveEmployerSSRate, minWage.GrossSalary), 2);
            return employerSSAmount;


        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code14857 && sector == Sector.Other)
        {
            
            employerSSAmount = Math.Round(_14857OtherCalc(ssContributionBase, ssParams.ActiveEmployerSSRate, minWage.GrossSalary), 2);
            return employerSSAmount;


        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code6111)
        {
            employerSSAmount = Math.Round(_6111Calc(), 2);
            return employerSSAmount;

        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code16322 && sector == Sector.Manufacturing)
        {
            employerSSAmount = Math.Round(_16322ManCalc(ssContributionBase, ssParams.ActiveEmployerSSRate, minWage.GrossSalary), 2);
            return employerSSAmount;

        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code16322 && sector == Sector.Other)
        {
            employerSSAmount = Math.Round(_16322OtherCalc(ssContributionBase, ssParams.ActiveEmployerSSRate, minWage.GrossSalary), 2);
            return employerSSAmount;

        }
        else
        {
            throw new InvalidOperationException("Invalid combination of status and incentive type.");
        }
       
      



    }


    private decimal SgdpCalc(decimal ssContributionBase, decimal rate)
    {   

        return ssContributionBase * rate;
    }

    private decimal Full(decimal ssContributionBase, decimal rate)
    {
        return ssContributionBase * rate;
    }

    private decimal _5510ManCalc(decimal ssContributionBase,decimal rate)
    {
        decimal Currentrate = rate - 0.05m;
        return ssContributionBase * Currentrate;
    }

    private decimal _5510OtherCalc(decimal ssContributionBase, decimal rate)
    {
        decimal Currentrate = rate - 0.02m;
        return ssContributionBase * Currentrate;
    }

    private decimal _14857ManCalc(decimal ssContributionBase, decimal rate, decimal minWage)
    {
        decimal currentRate = rate - 0.05m;
        return (ssContributionBase * currentRate) - (minWage * currentRate);
    }

    private decimal _14857OtherCalc(decimal ssContributionBase, decimal rate, decimal minWage)
    {
        decimal currentRate = rate - 0.02m;
        return (ssContributionBase * currentRate) - (minWage * currentRate);
    }

    private decimal _6111Calc()
    {
        return 0;
    }

    private decimal _16322ManCalc(decimal ssContributionBase, decimal rate, decimal minWage)
    {
        decimal currentRate = rate - 0.05m;
        return (ssContributionBase * currentRate) - (minWage * currentRate);
    }

    private decimal _16322OtherCalc(decimal ssContributionBase, decimal rate, decimal minWage)
    {
        decimal currentRate = rate - 0.02m;
        return (ssContributionBase * currentRate) - (minWage * currentRate);
    }

        
}
