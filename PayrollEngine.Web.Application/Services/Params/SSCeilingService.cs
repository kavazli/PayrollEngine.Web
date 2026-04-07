using System;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Infrastructure.Providers.Params;

namespace PayrollEngine.Web.Application.Services.Params;

public class SSCeilingService
{
    private readonly SSCeilingProvider _provider;

    public SSCeilingService(SSCeilingProvider provider)
    {
        _provider = provider;
    }

    public async Task<SSCeiling> Add(SSCeiling ssCeiling)
    {
        var result = await _provider.Add(ssCeiling);
        return result;
    }

    public async Task<SSCeiling> Get(int year)
    {
        var result = await _provider.Get(year);
        return result;
    }

    public async Task<SSCeiling> Update(SSCeiling ssCeiling)
    {
        var result = await _provider.Update(ssCeiling);
        return result;
    }

    public async Task Delete()
    {
        await _provider.Delete();
    }
}
