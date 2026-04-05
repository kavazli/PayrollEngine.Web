using System;
using PayrollEngine.Web.Domain.Entities;

namespace PayrollEngine.Web.Domain.Interface;

public interface IMinimumWageProvider
{
    public Task<MinimumWage> Get(int year);
    public Task<MinimumWage> Add(MinimumWage minimumWage);
    public Task<MinimumWage> Update(MinimumWage minimumWage);
    public Task Delete();

}
