namespace OrderAPI.Dtos
{
    public record TimeLogResponse(
        int Id,
        DateTime StartTime,
        DateTime? EndTime,
        int CategoryId    
    );

    public record CreateTimeLogRequest(
        DateTime StartTime,
        DateTime? EndTime,
        int CategoryId
    );

    public record UpdateTimeLogRequest(
        DateTime? StartTime,
        DateTime? EndTime,
        int? CategoryId
        );
}