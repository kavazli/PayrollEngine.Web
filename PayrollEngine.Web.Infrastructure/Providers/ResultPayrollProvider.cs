using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Enums;
using PayrollEngine.Web.Domain.Interface;
using PayrollEngine.Web.Infrastructure.DataBase;

namespace PayrollEngine.Web.Infrastructure.Providers;

public class ResultPayrollProvider : IResultPayrollProvider
{   

    private readonly AppDbContext _dbContext;

    public ResultPayrollProvider(AppDbContext appDbContext)
    {   
        if (appDbContext == null)
        {
            throw new ArgumentNullException(nameof(appDbContext));
        }
        _dbContext = appDbContext;
    }


    public async Task<ResultPayroll> Add(ResultPayroll resultPayroll)
    {
       _dbContext.PayrollResults.Add(resultPayroll);
        await _dbContext.SaveChangesAsync();
        return resultPayroll;
    }

    
    public async Task<List<ResultPayroll>> AddRange(List<ResultPayroll> resultPayrolls)
    {
        _dbContext.PayrollResults.AddRange(resultPayrolls);
        await _dbContext.SaveChangesAsync();
        return resultPayrolls;
    }

    public Task Delete()
    {
        _dbContext.PayrollResults.RemoveRange(_dbContext.PayrollResults);
        return _dbContext.SaveChangesAsync();
    }

    public async Task<List<ResultPayroll>> Get()
    {
        var result = await _dbContext.PayrollResults.ToListAsync();
        return result;
    }

    public async Task<ResultPayroll> GetMonth(Months months)
    {
        var result = await _dbContext.PayrollResults.Where(r => r.Month == months).FirstOrDefaultAsync();
        return result;
    }

    public async Task<ResultPayroll> Update(ResultPayroll resultPayroll)
    {
        _dbContext.PayrollResults.Update(resultPayroll);
        await _dbContext.SaveChangesAsync();
        return resultPayroll;
    }
}
