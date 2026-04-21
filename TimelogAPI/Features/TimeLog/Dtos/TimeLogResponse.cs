namespace TimelogAPI.Features.TimeLog.Dtos
{
    public record TimeLogResponse(
        int Id,
        DateTime StartTime,
        DateTime? EndTime,
        int CategoryId
    );
}
