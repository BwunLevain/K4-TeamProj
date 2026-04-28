using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimelogAPI.Features.AiComments;
using TimelogAPI.Services;

namespace TimelogAPI.Controllers
{
    /// <summary>
    /// Hanterar AI-genererat innehåll och feedback baserat på användarens data.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SavedContentController : ControllerBase
    {
        private readonly IAiMessageService _messageService;
        private readonly ITimelogService _timelogService;

        public SavedContentController(IAiMessageService messageService, ITimelogService timelogService)
        {
            _messageService = messageService;
            _timelogService = timelogService;
        }

        [HttpPost("generic")]
        [Authorize]
        public async Task<ActionResult<AiMessageResponse>> GenericAiFeedback([FromBody] GenericAiRequest request)
        {
            var message = await _messageService.GetSimpleAiResponseAsync(request.Prompt);
            return Ok(new AiMessageResponse(message));
        }

        /// <summary>
        /// Analyserar tidsloggar inom ett specifikt tidsspann och genererar feedback.
        /// </summary>
        /// <param name="request">Tidsspannet (FromDate och ToDate) som ska analyseras.</param>
        /// <returns>AI-baserad feedback (affirmation eller förolämpning).</returns>
        /// <response code="200">Returnerar feedback-meddelandet baserat på loggad tid.</response>
        [HttpPost("feedback")]
        [Authorize]
        public async Task<ActionResult<AiMessageResponse>> GenerateAiFeedback([FromBody] AiFeedbackRequest request)
        {
            var pagedResult = await _timelogService.GetPagedTimelogsAsync(
                page: 1,
                pageSize: 100,
                startDate: request.FromDate,
                category: null);

            var logs = pagedResult.Data
                .Where(x => x.StartTime <= request.ToDate)
                .Select(dto => new TimelogAPI.Features.TimeLogs.TimeLog
                {
                    Id = dto.Id,
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                    CategoryId = dto.CategoryId
                })
                .ToList();

            if (!logs.Any())
            {
                return Ok(new AiMessageResponse("Inga loggar hittades för det valda tidsspannet. Du har ju inte gjort någonting!"));
            }

            var message = await _messageService.GenerateTimelogFeedbackMessageAsync(logs);

            return Ok(new AiMessageResponse(message));
        }
    }
}