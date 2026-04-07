using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Infrastructure.DataBase;

namespace PayrollEngine.Web.Infrastructure.Providers.Params;

public class SSCeilingProvider
{
    private readonly AppDbContext _dbContext;

    public SSCeilingProvider(AppDbContext dbContext)
    {
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        _dbContext = dbContext;
    }


    public async Task<SSCeiling> Add(SSCeiling ssCeiling)
    {
        _dbContext.SSCeilings.Add(ssCeiling);
        await _dbContext.SaveChangesAsync();
        return ssCeiling;
    }


    public async Task Delete()
    {
        _dbContext.SSCeilings.RemoveRange(_dbContext.SSCeilings);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<SSCeiling> Get(int year)
    {
        var result = await _dbContext.SSCeilings.FirstOrDefaultAsync(s => s.Year == year);
        if (result == null)
        {
            throw new InvalidOperationException("No SS ceiling found for the specified year.");
        }
        return result;
    }


    public async Task<SSCeiling> Update(SSCeiling ssCeiling)
    {
        var tracked = await _dbContext.SSCeilings.FindAsync(ssCeiling.Id);
        if (tracked == null)
        {
            throw new InvalidOperationException("SS ceiling not found.");
        }
        _dbContext.Entry(tracked).CurrentValues.SetValues(ssCeiling);
        await _dbContext.SaveChangesAsync();
        return ssCeiling;
    }

}
