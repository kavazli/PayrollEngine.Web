using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollEngine.Web.Application.Services.Params;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinimumWageController : ControllerBase
    {
        private readonly MinimumWageService _minimumWageService;


        public MinimumWageController( MinimumWageService minimumWageService)
        {
            _minimumWageService = minimumWageService ?? throw new ArgumentNullException(nameof(minimumWageService));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int year)
        {
            try
            {
                var result = await _minimumWageService.Get(year);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MinimumWage minimumWage)
        {
            var result = await _minimumWageService.Add(minimumWage);
            return Ok(result);  
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MinimumWage minimumWage)
        {
            var result = await _minimumWageService.Update(minimumWage);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await _minimumWageService.Delete();
            return NoContent();
        }

    }
}
