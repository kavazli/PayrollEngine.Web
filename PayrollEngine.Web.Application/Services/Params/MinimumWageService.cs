using System;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Interface;
using PayrollEngine.Web.Infrastructure.Providers.Params;

namespace PayrollEngine.Web.Application.Services.Params;

public class MinimumWageService 
{   
    private readonly MinimumWageProvider _provider;

    public MinimumWageService(MinimumWageProvider provider)
    {  
        _provider = provider;
    }

    public async Task<MinimumWage> Add(MinimumWage minimumWage)
    {
        var result = await _provider.Add(minimumWage);
        return result;
    }

     public async Task<MinimumWage> Get(int year)
     {
        var result = await _provider.Get(year);
        return result;
     }

     public async Task<MinimumWage> Update(MinimumWage minimumWage)
    {
        var result = await _provider.Update(minimumWage);
        return result;
    }

    public async Task Delete()
    {
        await _provider.Delete();
        
    }

   
}
