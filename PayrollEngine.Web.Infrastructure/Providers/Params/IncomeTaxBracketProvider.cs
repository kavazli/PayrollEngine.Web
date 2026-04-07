using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Infrastructure.DataBase;

namespace PayrollEngine.Web.Infrastructure.Providers.Params;

public class IncomeTaxBracketProvider
{
    private readonly AppDbContext _dbContext;

    public IncomeTaxBracketProvider(AppDbContext dbContext)
    {
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        _dbContext = dbContext;
    }


    public async Task<IncomeTaxBracket> Add(IncomeTaxBracket incomeTaxBracket)
    {
        _dbContext.IncomeTaxBrackets.Add(incomeTaxBracket);
        await _dbContext.SaveChangesAsync();
        return incomeTaxBracket;
    }


    public async Task Delete()
    {
        _dbContext.IncomeTaxBrackets.RemoveRange(_dbContext.IncomeTaxBrackets);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<List<IncomeTaxBracket>> Get(int year)
    {
        var result = await _dbContext.IncomeTaxBrackets.Where(b => b.Year == year).ToListAsync();
        return result;
    }


    public async Task<IncomeTaxBracket> Update(IncomeTaxBracket incomeTaxBracket)
    {
        var tracked = await _dbContext.IncomeTaxBrackets.FindAsync(incomeTaxBracket.Id);
        if (tracked == null)
        {
            throw new InvalidOperationException("Income tax bracket not found.");
        }
        _dbContext.Entry(tracked).CurrentValues.SetValues(incomeTaxBracket);
        await _dbContext.SaveChangesAsync();
        return incomeTaxBracket;
    }

}
