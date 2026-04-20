using System.ComponentModel.DataAnnotations;

namespace TimelogAPI.Features.TimeLog.Dtos
{
    public record CreateTimeLogRequest(
        [Required]
        DateTime StartTime,
        DateTime? EndTime,
        [Required]
        int CategoryId
    );
}
