using System;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.Application.Services;

public class ScenarioService : IScenarioService
{
    private readonly IScenarioProvider _scenarioProvider;


    public ScenarioService(IScenarioProvider scenarioProvider)
    {
        if (scenarioProvider == null)
        {
            throw new ArgumentNullException(nameof(scenarioProvider));
        }

        _scenarioProvider = scenarioProvider;
    }


    public async Task<Scenario> Add(Scenario scenario)
    {
        return await _scenarioProvider.Add(scenario);
    }


    public async Task Delete()
    {
        await _scenarioProvider.Delete();
    }


    public async Task<Scenario> Get()
    {
        return await _scenarioProvider.Get();
    }


    public async Task<Scenario> Update(Scenario scenario)
    {
        return await _scenarioProvider.Update(scenario);
    }


}
