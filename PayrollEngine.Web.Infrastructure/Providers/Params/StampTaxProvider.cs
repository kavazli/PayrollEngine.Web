using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Infrastructure.DataBase;

namespace PayrollEngine.Web.Infrastructure.Providers.Params;

public class StampTaxProvider
{
    private readonly AppDbContext _dbContext;

    public StampTaxProvider(AppDbContext dbContext)
    {   
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }       

        _dbContext = dbContext;
    }


    public async Task<StampTax> Add(StampTax stampTax)
    {
        _dbContext.StampTaxes.Add(stampTax);
        await _dbContext.SaveChangesAsync();
        return stampTax;
    }


    public async Task Delete()
    {
        _dbContext.StampTaxes.RemoveRange(_dbContext.StampTaxes);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<StampTax> Get(int year)
    {
        var result = await _dbContext.StampTaxes.FirstOrDefaultAsync(s => s.Year == year);
        if (result == null)
        {
            throw new InvalidOperationException("No stamp tax found for the specified year.");
        }
        return result;
    }


    public async Task<StampTax> Update(StampTax stampTax)
    {
        var tracked = await _dbContext.StampTaxes.FindAsync(stampTax.Id);
        if (tracked == null)
        {
            throw new InvalidOperationException("Stamp tax not found.");
        }
        _dbContext.Entry(tracked).CurrentValues.SetValues(stampTax);
        await _dbContext.SaveChangesAsync();
        return stampTax;
    }

}
