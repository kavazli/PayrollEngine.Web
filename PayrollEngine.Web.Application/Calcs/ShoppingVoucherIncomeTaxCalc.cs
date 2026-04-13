using System;
using System.ComponentModel.Design;
using PayrollEngine.Web.Domain.Enums;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.Application.Calcs;

public class ShoppingVoucherIncomeTaxCalc
{
    
    public async Task<decimal> Calc(decimal shoppingVoucherGross, decimal rate)
    {
        decimal result = shoppingVoucherGross * rate;

        return Math.Round(result, 2);

    }

}
