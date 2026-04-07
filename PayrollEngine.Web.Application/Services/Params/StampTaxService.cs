using System;
using PayrollEngine.Web.Domain.Entities.Params;
using PayrollEngine.Web.Infrastructure.Providers.Params;

namespace PayrollEngine.Web.Application.Services.Params;

public class StampTaxService
{
    private readonly StampTaxProvider _provider;

    public StampTaxService(StampTaxProvider provider)
    {
        _provider = provider;
    }

    public async Task<StampTax> Add(StampTax stampTax)
    {
        var result = await _provider.Add(stampTax);
        return result;
    }

    public async Task<StampTax> Get(int year)
    {
        var result = await _provider.Get(year);
        return result;
    }

    public async Task<StampTax> Update(StampTax stampTax)
    {
        var result = await _provider.Update(stampTax);
        return result;
    }

    public async Task Delete()
    {
        await _provider.Delete();
    }
}
