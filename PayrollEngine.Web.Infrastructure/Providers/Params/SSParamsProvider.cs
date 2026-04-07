using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Infrastructure.DataBase;

namespace PayrollEngine.Web.Infrastructure.Providers.Params;

public class SSParamsProvider
{
    private readonly AppDbContext _dbContext;

    public SSParamsProvider(AppDbContext dbContext)
    {
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        _dbContext = dbContext;
    }


    public async Task<SSParams> Add(SSParams ssParams)
    {
        _dbContext.SSParams.Add(ssParams);
        await _dbContext.SaveChangesAsync();
        return ssParams;
    }


    public async Task Delete()
    {
        _dbContext.SSParams.RemoveRange(_dbContext.SSParams);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<SSParams> Get(int year)
    {
        var result = await _dbContext.SSParams.FirstOrDefaultAsync(s => s.Year == year);
        if (result == null)
        {
            throw new InvalidOperationException("No SS params found for the specified year.");
        }
        return result;
    }


    public async Task<SSParams> Update(SSParams ssParams)
    {
        var tracked = await _dbContext.SSParams.FindAsync(ssParams.Id);
        if (tracked == null)
        {
            throw new InvalidOperationException("SS params not found.");
        }
        _dbContext.Entry(tracked).CurrentValues.SetValues(ssParams);
        await _dbContext.SaveChangesAsync();
        return ssParams;
    }

}
