using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Domain.Interface;
using PayrollEngine.Web.Infrastructure.DataBase;
using PayrollEngine.Web.Infrastructure.Providers;
using PayrollEngine.Web.Infrastructure.Providers.Params;

namespace PayrollEngine.Web.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IScenarioProvider, ScenarioProvider>();
        services.AddScoped<IPayrollMonthsProvider, PayrollMonthProvider>();
        services.AddScoped<IResultPayrollProvider, ResultPayrollProvider>();


        services.AddScoped<MinimumWageProvider, MinimumWageProvider>();
        services.AddScoped<DisabilityDegreeProvider, DisabilityDegreeProvider>();
        services.AddScoped<IncomeTaxBracketProvider, IncomeTaxBracketProvider>();
        services.AddScoped<SSCeilingProvider, SSCeilingProvider>();
        services.AddScoped<SSParamsProvider, SSParamsProvider>();
        services.AddScoped<StampTaxProvider, StampTaxProvider>();
        

        return services;
    }
}
