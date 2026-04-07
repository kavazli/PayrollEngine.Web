using System;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Enums;

namespace PayrollEngine.Web.Domain.Interface;

public interface IResultPayrollService
{
    public Task<List<ResultPayroll>> Get();
    public Task<ResultPayroll> GetMonth(Months months);
    public Task<ResultPayroll> Add(ResultPayroll resultPayroll);
    public Task<List<ResultPayroll>> AddRange(List<ResultPayroll> resultPayrolls);
    public Task Delete();

}
