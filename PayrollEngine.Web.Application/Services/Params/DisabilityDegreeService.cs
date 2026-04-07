using System;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Infrastructure.Providers.Params;

namespace PayrollEngine.Web.Application.Services.Params;

public class DisabilityDegreeService
{
    private readonly DisabilityDegreeProvider _provider;

    public DisabilityDegreeService(DisabilityDegreeProvider provider)
    {
        _provider = provider;
    }

    public async Task<DisabilityDegree> Add(DisabilityDegree disabilityDegree)
    {
        var result = await _provider.Add(disabilityDegree);
        return result;
    }

    public async Task<List<DisabilityDegree>> Get(int year)
    {
        var result = await _provider.Get(year);
        return result;
    }

    public async Task<DisabilityDegree> Update(DisabilityDegree disabilityDegree)
    {
        var result = await _provider.Update(disabilityDegree);
        return result;
    }

    public async Task Delete()
    {
        await _provider.Delete();
    }
}
