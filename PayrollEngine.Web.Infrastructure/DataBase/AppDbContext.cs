using Microsoft.EntityFrameworkCore;
using PayrollEngine.Web.Domain.Entities;

namespace PayrollEngine.Web.Infrastructure.DataBase;

public class AppDbContext : DbContext
{   

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Scenario> Scenarios => Set<Scenario>();
    public DbSet<PayrollMonth> PayrollMonths => Set<PayrollMonth>();
    public DbSet<MinimumWage> MinimumWages => Set<MinimumWage>();
   
}
