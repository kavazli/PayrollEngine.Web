using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Infrastructure.DataBase;

namespace PayrollEngine.Web.Infrastructure.Providers.Params;

public class DisabilityDegreeProvider
{
    private readonly AppDbContext _dbContext;

    public DisabilityDegreeProvider(AppDbContext dbContext)
    {
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        _dbContext = dbContext;
    }


    public async Task<DisabilityDegree> Add(DisabilityDegree disabilityDegree)
    {
        _dbContext.DisabilityDegrees.Add(disabilityDegree);
        await _dbContext.SaveChangesAsync();
        return disabilityDegree;
    }


    public async Task Delete()
    {
        _dbContext.DisabilityDegrees.RemoveRange(_dbContext.DisabilityDegrees);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<List<DisabilityDegree>> Get(int year)
    {
        var result = await _dbContext.DisabilityDegrees.Where(d => d.Year == year).ToListAsync();
        return result;
    }


    public async Task<DisabilityDegree> Update(DisabilityDegree disabilityDegree)
    {
        var tracked = await _dbContext.DisabilityDegrees.FindAsync(disabilityDegree.Id);
        if (tracked == null)
        {
            throw new InvalidOperationException("Disability degree not found.");
        }
        _dbContext.Entry(tracked).CurrentValues.SetValues(disabilityDegree);
        await _dbContext.SaveChangesAsync();
        return disabilityDegree;
    }

}
