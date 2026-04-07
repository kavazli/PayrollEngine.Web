using System;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Infrastructure.Providers.Params;

namespace PayrollEngine.Web.Application.Services.Params;

public class IncomeTaxBracketService
{
    private readonly IncomeTaxBracketProvider _provider;

    public IncomeTaxBracketService(IncomeTaxBracketProvider provider)
    {
        _provider = provider;
    }

    public async Task<IncomeTaxBracket> Add(IncomeTaxBracket incomeTaxBracket)
    {
        var result = await _provider.Add(incomeTaxBracket);
        return result;
    }

    public async Task<List<IncomeTaxBracket>> Get(int year)
    {
        var result = await _provider.Get(year);
        return result;
    }

    public async Task<IncomeTaxBracket> Update(IncomeTaxBracket incomeTaxBracket)
    {
        var result = await _provider.Update(incomeTaxBracket);
        return result;
    }

    public async Task Delete()
    {
        await _provider.Delete();
    }
}
