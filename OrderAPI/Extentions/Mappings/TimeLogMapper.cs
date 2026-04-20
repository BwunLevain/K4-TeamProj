using OrderAPI.Features.Categories;
using OrderAPI.Features.TimeLogs;

namespace OrderAPI.Extentions.Mappings
{
    public static class TimeLogMapper
    {
        public static TimeLogResponse ToResponse(this TimeLog timeLog)
        {
            return new TimeLogResponse(
                timeLog.Id,
                timeLog.StartTime,
                timeLog.EndTime,
                timeLog.CategoryId,
                timeLog.Category?.Name
            );
        }

        public static TimeLog ToEntity(this CreateTimeLogRequest request)
        {
            return new TimeLog
            {
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                CategoryId = request.CategoryId
            };
        }
    }
}