using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ResultPayrollController(IResultPayrollService resultPayrollService)
        {
            _resultPayrollService = resultPayrollService;
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

    
    }
}
