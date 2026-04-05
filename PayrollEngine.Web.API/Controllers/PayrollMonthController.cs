using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollEngine.Web.Application.Normalizers;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollMonthController : ControllerBase
    {
        private readonly IPayrollMonthsService _payrollMonthsService;

        public PayrollMonthController(IPayrollMonthsService payrollMonthsService)
        {
            if (payrollMonthsService == null)
            {
                throw new ArgumentNullException(nameof(payrollMonthsService));
            }
            _payrollMonthsService = payrollMonthsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {            
            var result = await _payrollMonthsService.Get();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<PayrollTemplateMonth> payrollMonths)
        {   
            
            var Normalazer = new PayrollMonthNormalizer();    
            List<PayrollMonth> normalizedPayrollMonths = new List<PayrollMonth>();


            foreach(var payrollMonth in payrollMonths)
            {
                var Result = Normalazer.Normalize(payrollMonth);
                normalizedPayrollMonths.Add(Result);
            }


            var result = await _payrollMonthsService.AddRange(normalizedPayrollMonths);
            return Ok(payrollMonths);
            
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await _payrollMonthsService.Delete();
            return NoContent();

        }
    }



}
