using System;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Enums;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.Application.Services;

public class ResultPayrollService : IResultPayrollService
{   

    private readonly IResultPayrollProvider _provider;

    public ResultPayrollService(IResultPayrollProvider provider)
    {
        _provider = provider;
    }

    public async Task<ResultPayroll> Add(ResultPayroll resultPayroll)
    {
        await _provider.Add(resultPayroll);
        return resultPayroll;
    }

    public async Task<List<ResultPayroll>> AddRange(List<ResultPayroll> resultPayrolls)
    {
        await _provider.AddRange(resultPayrolls);
        return resultPayrolls;
    }

    public async Task Delete()
    {
        await _provider.Delete();
    }

    public async Task<List<ResultPayroll>> Get()
    {
        return await _provider.Get();
    }

    public Task<ResultPayroll> GetMonth(Months months)
    {
        return _provider.GetMonth(months);
    }

   
}
