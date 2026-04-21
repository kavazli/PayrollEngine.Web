using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollEngine.Web.Application;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Enums;
using PayrollEngine.Web.Domain.Interface;
using System.Text;

namespace PayrollEngine.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultPayrollController : ControllerBase
    {
       private readonly IResultPayrollService _resultPayrollService;
       private readonly WorkFlow _workFlow;

        public ResultPayrollController(IResultPayrollService resultPayrollService, WorkFlow workFlow)
        {
            _resultPayrollService = resultPayrollService;
            _workFlow = workFlow;
        }


        [HttpPost]
        public async Task<IActionResult> Calculate()
        {
            try
            {
                await _workFlow.ResultPayrollExecute();
                await _workFlow.ShoppingVoucherExecute();
                await _workFlow.EmployerContributionExecute();
                await _workFlow.TotalEmployerCostExecute();
                return Ok(new { message = "Maaş hesaplamaları başarıyla tamamlandı" });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? "No inner exception";
                var stackTrace = ex.StackTrace ?? "No stack trace";
                
                return StatusCode(500, new 
                { 
                    error = ex.Message, 
                    details = innerMessage,
                    stackTrace = stackTrace,
                    type = ex.GetType().Name
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _resultPayrollService.Get();
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            try
            {
                await _resultPayrollService.Delete();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("csv")]
        public async Task<IActionResult> GetCsv()
        {
            try
            {
                var results = await _resultPayrollService.Get();

                if (!results.Any())
                {
                    return NotFound("İndirilecek veri bulunamadı.");
                }

                var monthNames = new Dictionary<Months, string>
                {
                    { Months.January, "Ocak" },
                    { Months.February, "Şubat" },
                    { Months.March, "Mart" },
                    { Months.April, "Nisan" },
                    { Months.May, "Mayıs" },
                    { Months.June, "Haziran" },
                    { Months.July, "Temmuz" },
                    { Months.August, "Ağustos" },
                    { Months.September, "Eylül" },
                    { Months.October, "Ekim" },
                    { Months.November, "Kasım" },
                    { Months.December, "Aralık" }
                };

                var sb = new StringBuilder();
                
                // Header - Noktalı virgül ayırıcı kullan
                sb.AppendLine("Ay;Temel Ücret;FM %50;FM %100;Prim / İkramiye;Brüt Maaş;SGK Matrahı;SGK Kesintisi;İşsizlik Kes.;GV Matrahı;KGV Matrahı;Gelir vergisi;GV İstisnası;Damga Vergisi;DV İstisnası;Net Maaş;İşveren SGK;İşveren İşsizlik;Alışveriş Net;Alışveriş Brüt;Toplam Maliyet");

                // Satırlar - Noktalı virgül ayırıcı kullan
                foreach (var r in results)
                {
                    sb.AppendLine(string.Join(";",
                        monthNames[r.Month],
                        r.BaseSalary.ToString("N2"),
                        r.Overtime_50_Amount.ToString("N2"),
                        r.Overtime_100_Amount.ToString("N2"),
                        r.Bonus.ToString("N2"),
                        r.GrossSalary.ToString("N2"),
                        r.SSContributionBase.ToString("N2"),
                        r.EmployeeSSContributionAmount.ToString("N2"),
                        r.EmployeeUIContributionAmount.ToString("N2"),
                        r.IncomeTaxBase.ToString("N2"),
                        r.CumulativeIncomeTaxBase.ToString("N2"),
                        r.IncomeTax.ToString("N2"),
                        r.IncomeTaxExemption.ToString("N2"),
                        r.StampTax.ToString("N2"),
                        r.StampTaxExemption.ToString("N2"),
                        r.NetSalary.ToString("N2"),
                        r.EmployerSSContributionAmount.ToString("N2"),
                        r.EmployerUIContributionAmount.ToString("N2"),
                        r.ShoppingVoucherNet.ToString("N2"),
                        r.ShoppingVoucherGrossAmount.ToString("N2"),
                        r.TotalEmployerCost.ToString("N2")
                    ));
                }

                // Toplam satırı - Noktalı virgül ayırıcı kullan
                sb.AppendLine(string.Join(";",
                    "TOPLAM",
                    results.Sum(r => r.BaseSalary).ToString("N2"),
                    results.Sum(r => r.Overtime_50_Amount).ToString("N2"),
                    results.Sum(r => r.Overtime_100_Amount).ToString("N2"),
                    results.Sum(r => r.Bonus).ToString("N2"),
                    results.Sum(r => r.GrossSalary).ToString("N2"),
                    results.Sum(r => r.SSContributionBase).ToString("N2"),
                    results.Sum(r => r.EmployeeSSContributionAmount).ToString("N2"),
                    results.Sum(r => r.EmployeeUIContributionAmount).ToString("N2"),
                    results.Sum(r => r.IncomeTaxBase).ToString("N2"),
                    "0",
                    results.Sum(r => r.IncomeTax).ToString("N2"),
                    results.Sum(r => r.IncomeTaxExemption).ToString("N2"),
                    results.Sum(r => r.StampTax).ToString("N2"),
                    results.Sum(r => r.StampTaxExemption).ToString("N2"),
                    results.Sum(r => r.NetSalary).ToString("N2"),
                    results.Sum(r => r.EmployerSSContributionAmount).ToString("N2"),
                    results.Sum(r => r.EmployerUIContributionAmount).ToString("N2"),
                    results.Sum(r => r.ShoppingVoucherNet).ToString("N2"),
                    results.Sum(r => r.ShoppingVoucherGrossAmount).ToString("N2"),
                    results.Sum(r => r.TotalEmployerCost).ToString("N2")
                ));

                // UTF-8 BOM ile byte array oluştur
                var bomBytes = Encoding.UTF8.GetPreamble();
                var contentBytes = Encoding.UTF8.GetBytes(sb.ToString());
                var finalBytes = bomBytes.Concat(contentBytes).ToArray();

                var fileName = $"Bordro_Sonuclari_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                
                return File(finalBytes, "text/csv; charset=utf-8", fileName);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    
    }
}
