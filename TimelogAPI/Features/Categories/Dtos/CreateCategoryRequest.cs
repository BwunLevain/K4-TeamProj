using System.ComponentModel.DataAnnotations;
using TimelogAPI.Features.TimeLogs.Dtos;

namespace TimelogAPI.Features.Categories.Dtos
{
    public record CreateCategoryRequest(
        [Required,StringLength(50)]
        string Name,
        List<TimeLogResponse> TimeLogs

    );
}
