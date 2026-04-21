using TimelogAPI.Features.TimeLogs.Dtos;
using TimelogAPI.Features.TimeLogs;

namespace TimelogAPI.Extentions.Mappers
{
    public static class TimeLogMapper
    {
        public static TimeLogResponse ToResponse(this TimeLog timeLog)
        {
            if (timeLog == null) return null!;

            return new TimeLogResponse(
                timeLog.Id,
                timeLog.StartTime,
                timeLog.EndTime,
                timeLog.CategoryId
            );
        }

        public static TimeLog ToEntity(this CreateTimeLogRequest request)
        {
            if (request == null) return null!;

            return new TimeLog
            {
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                CategoryId = request.CategoryId
            };
        }

        public static void UpdateEntity(this UpdateTimeLogRequest request, TimeLog existingLog)
        {
            if (request == null || existingLog == null) return;

            existingLog.StartTime = request.StartTime ?? existingLog.StartTime;
            existingLog.EndTime = request.EndTime;
            existingLog.CategoryId = request.CategoryId ?? existingLog.CategoryId;
        }
    }
}