using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Interface;
using PayrollEngine.Web.Infrastructure.DataBase;

namespace PayrollEngine.Web.Infrastructure.Providers;

public class ScenarioProvider : IScenarioProvider
{   

    private readonly AppDbContext _dbContext;

     public ScenarioProvider(AppDbContext dbContext)
     {  
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        _dbContext = dbContext;
     }
   


    public async Task<Scenario> Add(Scenario scenario)
    {
        _dbContext.Scenarios.Add(scenario);
        await _dbContext.SaveChangesAsync();
        return scenario;
    }


    public async Task Delete()
    {
        _dbContext.Scenarios.RemoveRange(_dbContext.Scenarios);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<Scenario> Get()
    {
        var scenario = await _dbContext.Scenarios.FirstOrDefaultAsync();
        if (scenario == null)
        {
            throw new InvalidOperationException("No scenario found in the database.");
        }
        return scenario;
    }


    public async Task<Scenario> Update(Scenario scenario)
    {
        var tracked = await _dbContext.Scenarios.FindAsync(scenario.Id);
        if (tracked == null)
        {
            throw new InvalidOperationException("Scenario not found.");
        }
        _dbContext.Entry(tracked).CurrentValues.SetValues(scenario);
        await _dbContext.SaveChangesAsync();
        return tracked;
    }
}
