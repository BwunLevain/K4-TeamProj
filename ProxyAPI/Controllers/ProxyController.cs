using Microsoft.AspNetCore.Mvc;
using ProxyAPI.Client;

namespace ProxyAPI.Controller
{
    [ApiController]
    [Route("api/proxy")]
    public class ProxyController : ControllerBase
    {
        private readonly TimeLogClient _timeLogClient;

        public ProxyController(TimeLogClient timeLogClient)
        {
            _timeLogClient = timeLogClient;
        }

        [HttpGet("timelogs")]
        public async Task<IActionResult> GetTimeLogs()
        {
            var logs = await _timeLogClient.GetTimeLogsAsync();
            return Ok(logs);
        }
    }
}
