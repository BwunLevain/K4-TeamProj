using TimelogAPI.Features.TimeLog.Dtos;

namespace TimelogAPI.Services;

public interface ITimelogService
{
    // get all timelogs, user can filter with start date and/or category both are optional 
    Task<PagedResponseDto<TimeLogResponse>> GetAllTimeLogsAsync(int page, int pageSize, DateTime? startDate, string? category);
    
    // add the other tasks below (create,delete, update)
}