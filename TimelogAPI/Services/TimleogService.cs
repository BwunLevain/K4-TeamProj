using TimelogAPI.Features.TimeLog.Dtos;

namespace TimelogAPI.Services;

// implement all tasks from the ITimelogService
public class TimleogService : ITimelogService
{
    public async Task<PagedResponseDto<TimeLogResponse>> GetAllTimeLogsAsync(int page, int pageSize,
        DateTime? startDate, string? category)
    {
        // remove this and implement the metod 
        throw new NotImplementedException();
    }
}