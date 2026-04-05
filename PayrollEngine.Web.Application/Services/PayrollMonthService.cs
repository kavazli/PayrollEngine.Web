using System;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.Application.Services;

public class PayrollMonthService : IPayrollMonthsService
{

    private readonly IPayrollMonthsProvider _payrollMonthsProvider;

    public PayrollMonthService(IPayrollMonthsProvider payrollMonthsProvider)
    {
        if (payrollMonthsProvider == null)
        {
            throw new ArgumentNullException(nameof(payrollMonthsProvider));
        }
        _payrollMonthsProvider = payrollMonthsProvider;
    }

    public async Task<PayrollMonth> Add(PayrollMonth payrollMonth)
    {
        var result = await _payrollMonthsProvider.Add(payrollMonth);
        return result;
    }

    public async Task<List<PayrollMonth>> AddRange(List<PayrollMonth> payrollMonths)
    {
        var result = await _payrollMonthsProvider.AddRange(payrollMonths);
        return result;
    }

    public async Task Delete()
    {
        await _payrollMonthsProvider.Delete();
    }

    public async Task<List<PayrollMonth>> Get()
    {
        var result = await _payrollMonthsProvider.Get();
        return result;
    }
}
