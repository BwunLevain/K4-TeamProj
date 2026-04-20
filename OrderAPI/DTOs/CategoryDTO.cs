using System.ComponentModel.DataAnnotations;

namespace OrderAPI.DTOs
{
    public record CategoryResponse(
        int Id,
        string Name,
        List<TimeLogResponse> TimeLogs
    );

    public record CreateCategoryRequest(
        [Required,StringLength(50)]
        string Name,
        List<TimeLogResponse> TimeLogs

    );

    public record UpdateCategoryRequest(
        string Name
        );
}
