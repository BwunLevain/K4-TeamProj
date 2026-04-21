using System.ComponentModel.DataAnnotations;
using TimelogAPI.Features.TimeLog.Dtos;

namespace TimelogAPI.Features.Category.Dtos
{
    public record CreateCategoryRequest(
        [Required,StringLength(50)]
        string Name,
        List<TimeLogResponse> TimeLogs

    );
}
