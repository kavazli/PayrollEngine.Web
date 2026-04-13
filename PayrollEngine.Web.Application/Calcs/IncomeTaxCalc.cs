using System;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Entities.Params;

namespace PayrollEngine.Web.Application.Calcs;

public class IncomeTaxCalc
{   

    private readonly IncomeTaxBracketService _incomeTaxBracketService;

    

    public IncomeTaxCalc(IncomeTaxBracketService incomeTaxBracketService)
    {
        _incomeTaxBracketService = incomeTaxBracketService;
        
    }



    public async Task<(decimal tax, decimal rate)> Calc(int year, decimal cumulativeIncomeTaxBase, decimal incomeTaxBase)
    {   
        decimal Result = 0;
        decimal currentCumulativeIncomeTaxBase = cumulativeIncomeTaxBase;
        
        if(currentCumulativeIncomeTaxBase <= 0)
        {
            var result = await TaxCumulative(incomeTaxBase, year);
            return (Math.Round(result.tax, 2), result.rate);
        }

        decimal LastMonthCumulativeIncomeTaxBase = currentCumulativeIncomeTaxBase - incomeTaxBase;

        var currentMonthResult = await TaxCumulative(currentCumulativeIncomeTaxBase, year);
        var lastMonthResult = await TaxCumulative(LastMonthCumulativeIncomeTaxBase, year);

        Result = currentMonthResult.tax - lastMonthResult.tax;

        return (Math.Round(Result, 2), currentMonthResult.rate);

    }



    private async Task<IncomeTaxBrackets> TaxBrackets(int year)
    {   
        var brackets = await _incomeTaxBracketService.Get(year);

        if (brackets == null || !brackets.Any())
        {
            throw new InvalidOperationException($"Vergi dilimleri {year} yılı için bulunamadı. Lütfen parametrik verileri yükleyin.");
        }

        IncomeTaxBrackets incomeTaxBrackets = new IncomeTaxBrackets(brackets);
        return incomeTaxBrackets;
    }



    private async Task<(decimal tax, decimal rate)> TaxCumulative(decimal cumulativeIncomeTaxBase, int year)
    {   
        var incomeTaxBrackets = await TaxBrackets(year);    

        
        
        if(cumulativeIncomeTaxBase <= 0)
        {
            return (0, 0);
        }
        
        decimal tax = 0;
        decimal rate = 0;

        for(int i = 0; i < incomeTaxBrackets.Brackets.Count; i++)
        {
            // Mevcut dilimin minimum ve maksimum matrahını belirle
            decimal bracketMin = (i == 0) ? 0 : incomeTaxBrackets.Brackets[i-1].MaxAmount; // İlk dilim için minimum 0, diğerleri için bir önceki dilimin maksimumu
            decimal bracketMax = incomeTaxBrackets.Brackets[i].MaxAmount; // Mevcut dilimin maksimum matrahı

            // Eğer toplam matrah, mevcut dilimin minimumundan küçük veya eşitse döngüden çık
            if(cumulativeIncomeTaxBase <= bracketMin)
            {
                break;
            }

            // Matrah bu dilimin üst sınırını aşıyorsa tam dilim, aşmıyorsa kısmi hesapla
            if (cumulativeIncomeTaxBase >= bracketMax)
            {
                // Matrah bu dilimin tamamını kapsıyor, tam dilim vergisi ekle
                tax += (bracketMax - bracketMin) * incomeTaxBrackets.Brackets[i].Rate;
                rate = incomeTaxBrackets.Brackets[i].Rate;
            }
            else
            {
                // Matrah bu dilimin ortasında kalıyor, kısmi vergi hesapla ve döngüden çık
                tax += (cumulativeIncomeTaxBase - bracketMin) * incomeTaxBrackets.Brackets[i].Rate;
                rate = incomeTaxBrackets.Brackets[i].Rate;
                break;
            }

        }

        return (tax, rate);

        
    }


}
