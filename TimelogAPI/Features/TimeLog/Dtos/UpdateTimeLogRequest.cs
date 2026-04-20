namespace TimelogAPI.Features.TimeLog.Dtos
{
    public record UpdateTimeLogRequest(
        DateTime? StartTime,
        DateTime? EndTime,
        int? CategoryId
    );
}
