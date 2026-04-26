using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimelogAPI.Features.Common;
using TimelogAPI.Features.TimeLogs.Dtos;
using TimelogAPI.Services;

namespace TimelogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimelogsController : ControllerBase
    {
        private readonly ITimelogService _timelogService;
        public TimelogsController(ITimelogService timelogService)
        {
            _timelogService = timelogService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTimelog([FromBody] CreateTimeLogRequest request)
        {
            var result = await _timelogService.CreateTimelogAsync(request);
            return CreatedAtAction(nameof(GetTimelogById), new { id = result.Id }, result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimelogById(int id)
        {
            var result = await _timelogService.GetTimelogByIdAsync(id);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTimelogs(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? category = null,
            [FromQuery] DateTime? startDate = null)
        {
            var result = await _timelogService.GetPagedTimelogsAsync(page, pageSize, startDate, category);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimelog(int id, [FromBody] UpdateTimeLogRequest request)
        {
            await _timelogService.UpdateTimelogAsync(id, request);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimelog(int id)
        {
            await _timelogService.DeleteTimelogAsync(id);
            return NoContent();
        }
    }
}