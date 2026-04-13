using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollEngine.Web.Application;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Enums;
using PayrollEngine.Web.Domain.Interface;

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
    
    }
}
