using TimelogAPI.Features.TimeLog.Dtos;

namespace TimelogAPI.Features.Category.Dtos
{
    public record CategoryResponse(
        int Id,
        string Name,
        List<TimeLogResponse> TimeLogs
    );
}
