namespace TimelogAPI.Features.TimeLogs.Dtos
{
    public record UpdateTimeLogRequest(
        DateTime? StartTime,
        DateTime? EndTime,
        int? CategoryId
    );
}
