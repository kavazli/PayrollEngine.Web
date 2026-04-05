using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Interface;

namespace PayrollEngine.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenarioController : ControllerBase
    {
        private readonly IScenarioService _scenarioService;

        public ScenarioController(IScenarioService scenarioService)
         {
            if (scenarioService == null)
            {
                throw new ArgumentNullException(nameof(scenarioService));
            }

            _scenarioService = scenarioService;
         }
        

        [HttpPost]
        public async Task<ActionResult<Scenario>> Add([FromBody] Scenario scenario)
        {
            var Result = await _scenarioService.Add(scenario);
            return Ok(Result);
        }
        

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await _scenarioService.Delete();
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Scenario>> Get()
        {
            var Result = await _scenarioService.Get();
            return Ok(Result);    

        }

        [HttpPut]
        public async Task<ActionResult<Scenario>> Update([FromBody] Scenario scenario)
        {
            var Result = await _scenarioService.Update(scenario);
            return NoContent();
        }


    }
}
