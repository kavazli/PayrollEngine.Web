using Microsoft.EntityFrameworkCore;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Entities.Params;

namespace PayrollEngine.Web.Infrastructure.DataBase;

public class AppDbContext : DbContext
{   

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Scenario> Scenarios => Set<Scenario>();
    public DbSet<PayrollMonth> PayrollMonths => Set<PayrollMonth>();
    public DbSet<MinimumWage> MinimumWages => Set<MinimumWage>();
    public DbSet<ResultPayroll> PayrollResults => Set<ResultPayroll>();
    public DbSet<SSCeiling> SSCeilings => Set<SSCeiling>();
    public DbSet<DisabilityDegree> DisabilityDegrees => Set<DisabilityDegree>();
    public DbSet<IncomeTaxBracket> IncomeTaxBrackets => Set<IncomeTaxBracket>();
    public DbSet<StampTax> StampTaxes => Set<StampTax>();
    public DbSet<SSParams> SSParams => Set<SSParams>();
   
}
