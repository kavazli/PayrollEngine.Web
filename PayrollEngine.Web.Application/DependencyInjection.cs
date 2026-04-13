using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayrollEngine.Web.Application.Calcs;
using PayrollEngine.Web.Application.Normalizers;
using PayrollEngine.Web.Application.Services;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Interface;
using PayrollEngine.Web.Infrastructure;

namespace PayrollEngine.Web.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IScenarioService, ScenarioService>();
        services.AddScoped<IPayrollMonthsService, PayrollMonthService>();
        services.AddScoped<IResultPayrollService, ResultPayrollService>();
        services.AddScoped<MinimumWageService, MinimumWageService>();
        services.AddScoped<DisabilityDegreeService, DisabilityDegreeService>();
        services.AddScoped<IncomeTaxBracketService, IncomeTaxBracketService>();
        services.AddScoped<SSCeilingService, SSCeilingService>();
        services.AddScoped<SSParamsService, SSParamsService>();
        services.AddScoped<StampTaxService, StampTaxService>();

        services.AddScoped<PayrollMonthNormalizer, PayrollMonthNormalizer>();

        services.AddScoped<CumulativeIncomeTaxBaseCalc>();
        services.AddScoped<EmployeeSSContributionCalc>();
        services.AddScoped<EmployeeUIContributionCalc>();
        services.AddScoped<EmployerSSContributionCalc>();
        services.AddScoped<EmployerUIContributionAmountCalc>();
        services.AddScoped<IncomeTaxBaseCalc>();
        services.AddScoped<IncomeTaxCalc>();
        services.AddScoped<IncomeTaxExemptionCalc>();
        services.AddScoped<NetSalaryCalc>();
        services.AddScoped<ResultPayrollCalc>();
        services.AddScoped<SSContributionBaseCalc>();
        services.AddScoped<ShoppingVoucherGrossCalc>();
        services.AddScoped<ShoppingVoucherIncomeTaxCalc>();
        services.AddScoped<ShoppingVoucherStampTaxCalc>();
        services.AddScoped<StampTaxCalc>();
        services.AddScoped<StampTaxExemptionCalc>();
        services.AddScoped<TotalEmployerCostCalc>();

        services.AddInfrastructure(configuration);

        return services;
    }
}
