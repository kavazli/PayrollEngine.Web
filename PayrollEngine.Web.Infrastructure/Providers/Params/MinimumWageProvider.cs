using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Interface;
using PayrollEngine.Web.Infrastructure.DataBase;

namespace PayrollEngine.Web.Infrastructure.Providers.Params;

public class MinimumWageProvider : IMinimumWageProvider
{   
    private readonly AppDbContext _dbContext;
    
     public MinimumWageProvider(AppDbContext dbContext)
     {  
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        _dbContext = dbContext;
     }

    public async Task<MinimumWage> Add(MinimumWage minimumWage)
    {
        _dbContext.MinimumWages.Add(minimumWage);
        await _dbContext.SaveChangesAsync();
        return minimumWage;
    }

    public async Task Delete()
    {
        _dbContext.MinimumWages.RemoveRange(_dbContext.MinimumWages);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<MinimumWage> Get(int year)
    {
        var minimumWage = await _dbContext.MinimumWages.FirstOrDefaultAsync(mw => mw.Year == year);
        if (minimumWage == null)
        {
            throw new InvalidOperationException("No minimum wage found in the database.");
        }
        return minimumWage;
    }

    public async Task<MinimumWage> Update(MinimumWage minimumWage)
    {
        var tracked = await _dbContext.MinimumWages.FindAsync(minimumWage.Id);
        if (tracked == null)
        {
            throw new InvalidOperationException("Minimum wage not found.");
        }
        _dbContext.Entry(tracked).CurrentValues.SetValues(minimumWage);
        await _dbContext.SaveChangesAsync();
        return minimumWage;
    }

}
