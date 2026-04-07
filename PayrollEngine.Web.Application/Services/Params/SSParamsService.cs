using System;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Infrastructure.Providers.Params;

namespace PayrollEngine.Web.Application.Services.Params;

public class SSParamsService
{
    private readonly SSParamsProvider _provider;

    public SSParamsService(SSParamsProvider provider)
    {
        _provider = provider;
    }

    public async Task<SSParams> Add(SSParams ssParams)
    {
        var result = await _provider.Add(ssParams);
        return result;
    }

    public async Task<SSParams> Get(int year)
    {
        var result = await _provider.Get(year);
        return result;
    }

    public async Task<SSParams> Update(SSParams ssParams)
    {
        var result = await _provider.Update(ssParams);
        return result;
    }

    public async Task Delete()
    {
        await _provider.Delete();
    }
}
