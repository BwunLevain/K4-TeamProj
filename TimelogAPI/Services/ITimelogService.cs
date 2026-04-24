using TimelogAPI.Features.Common;
using TimelogAPI.Features.TimeLogs.Dtos;

namespace TimelogAPI.Services { 

    public interface ITimelogService
    {
        Task<PagedResponseDto<TimeLogResponse>> GetPagedTimelogsAsync(int page, int pageSize, DateTime? startDate, string? category);
        Task<TimeLogResponse> GetTimelogByIdAsync(int id);
        Task<TimeLogResponse> CreateTimelogAsync(CreateTimeLogRequest request);
        Task<bool> UpdateTimelogAsync(int id, UpdateTimeLogRequest request);
        Task<bool> DeleteTimelogAsync(int id);
    }
}