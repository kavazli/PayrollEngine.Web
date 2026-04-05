using System;
using PayrollEngine.Web.Domain.Entities;

namespace PayrollEngine.Web.Domain.Interface;

public interface IScenarioProvider
{
    public Task<Scenario> Get();
    public Task<Scenario> Add(Scenario scenario);
    public Task<Scenario> Update(Scenario scenario);
    public Task Delete();
}
