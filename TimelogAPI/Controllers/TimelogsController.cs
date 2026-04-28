using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimelogAPI.Features.Common;
using TimelogAPI.Features.TimeLogs.Dtos;
using TimelogAPI.Services;

namespace TimelogAPI.Controllers
{
    /// <summary>
    /// Hanterar tidrapporter och loggning av arbetstid.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TimelogsController : ControllerBase
    {
        private readonly ITimelogService _timelogService;

        public TimelogsController(ITimelogService timelogService)
        {
            _timelogService = timelogService;
        }

        /// <summary>
        /// Registrerar en ny tidrapport.
        /// </summary>
        /// <param name="request">Information om tidrapporten.</param>
        /// <returns>Den skapade tidrapporten.</returns>
        /// <response code="201">Tidrapporten har skapats.</response>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTimelog([FromBody] CreateTimeLogRequest request)
        {
            var result = await _timelogService.CreateTimelogAsync(request);
            return CreatedAtAction(nameof(GetTimelogById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Hämtar en specifik tidrapport via ID.
        /// </summary>
        /// <param name="id">Tidrapportens unika ID.</param>
        /// <returns>En tidrapport.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimelogById(int id)
        {
            var result = await _timelogService.GetTimelogByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Hämtar tidrapporter med filtrering och siduppdelning.
        /// </summary>
        /// <param name="page">Sidnummer (standardvärde 1).</param>
        /// <param name="pageSize">Antal resultat per sida (standardvärde 20).</param>
        /// <param name="category">Filtrera på en specifik kategori.</param>
        /// <param name="startDate">Hämta loggar från och med detta datum.</param>
        /// <returns>En lista med tidrapporter.</returns>
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

        /// <summary>
        /// Uppdaterar en befintlig tidrapport.
        /// </summary>
        /// <param name="id">ID för tidrapporten.</param>
        /// <param name="request">Ny information för tidrapporten.</param>
        /// <returns>NoContent om uppdateringen lyckades.</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimelog(int id, [FromBody] UpdateTimeLogRequest request)
        {
            await _timelogService.UpdateTimelogAsync(id, request);
            return NoContent();
        }

        /// <summary>
        /// Tar bort en tidrapport permanent.
        /// </summary>
        /// <param name="id">ID för tidrapporten som ska tas bort.</param>
        /// <returns>NoContent om borttagningen lyckades.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimelog(int id)
        {
            await _timelogService.DeleteTimelogAsync(id);
            return NoContent();
        }
    }
}