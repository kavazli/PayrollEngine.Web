using System;
using PayrollEngine.Web.Domain.Entities;

namespace PayrollEngine.Web.Domain.Interface;

public interface IPayrollMonthsProvider
{
    public Task<List<PayrollMonth>> Get();
    public Task<PayrollMonth> Add(PayrollMonth payrollMonth);
    public Task<List<PayrollMonth>> AddRange(List<PayrollMonth> payrollMonths);
    public Task Delete();
}
