using TimelogAPI.Features.TimeLogs.Dtos;

namespace TimelogAPI.Features.Categories.Dtos
{
    public record CategoryResponse(
        int Id,
        string Name,
        List<TimeLogResponse> TimeLogs
    );
}
