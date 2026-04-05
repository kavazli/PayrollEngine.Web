using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        services.AddScoped<IMinimumWageService, MinimumWageService>();

        services.AddScoped<PayrollMonthNormalizer, PayrollMonthNormalizer>();



        services.AddInfrastructure(configuration);

        return services;
    }
}
