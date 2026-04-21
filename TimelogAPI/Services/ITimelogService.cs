using TimelogAPI.Features.Common;
using TimelogAPI.Features.TimeLogs.Dtos;

namespace TimelogAPI.Services;

public interface ITimelogService
{
    Task<PagedResponseDto<TimeLogResponse>> GetPagedTimeLogsAsync(int page, int pageSize, DateTime? startDate, string? category);
    Task<TimeLogResponse> GetTimeLogByIdAsync(int id);
    Task<TimeLogResponse> CreateTimeLogAsync(CreateTimeLogRequest request);
    Task<bool> UpdateTimeLogAsync(UpdateTimeLogRequest request);
    Task<bool> DeleteTimeLogAsync(int id);
}