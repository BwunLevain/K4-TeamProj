using System.ComponentModel.DataAnnotations;

namespace TimelogAPI.Features.TimeLogs.Dtos
{
    public record CreateTimeLogRequest(
        [Required]
        DateTime StartTime,
        DateTime? EndTime,
        [Required]
        int CategoryId
    );
}
