namespace TimelogAPI.Features.TimeLogs.Dtos
{
    public record TimeLogResponse(
        int Id,
        DateTime StartTime,
        DateTime? EndTime,
        int CategoryId
    );
}
