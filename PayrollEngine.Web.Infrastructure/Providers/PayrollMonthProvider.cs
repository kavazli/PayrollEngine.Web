using System;
using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Interface;
using PayrollEngine.Web.Infrastructure.DataBase;

namespace PayrollEngine.Web.Infrastructure.Providers;

public class PayrollMonthProvider : IPayrollMonthsProvider
{
    private readonly AppDbContext _dbContext;


    public PayrollMonthProvider(AppDbContext dbContext)
     {  
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }
        _dbContext = dbContext;
    }


    public async Task<PayrollMonth> Add(PayrollMonth payrollMonth)
    {
        _dbContext.PayrollMonths.Add(payrollMonth);
        await _dbContext.SaveChangesAsync();
        return payrollMonth;
    }

    public async Task<List<PayrollMonth>> AddRange(List<PayrollMonth> payrollMonths)
    {
        _dbContext.PayrollMonths.AddRange(payrollMonths);
        await _dbContext.SaveChangesAsync();
        return payrollMonths;
    }

    public async Task Delete()
    {
        _dbContext.PayrollMonths.RemoveRange(_dbContext.PayrollMonths);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<PayrollMonth>> Get()
    {
        var result = await _dbContext.PayrollMonths.ToListAsync();
        
        return result;
    }

   
}
