using TimelogAPI.Features.Common;
using TimelogAPI.Features.TimeLogs;
using TimelogAPI.Features.TimeLogs.Dtos;
using TimelogAPI.Extentions.Mappers;
using TimelogAPI.Extentions;

namespace TimelogAPI.Services
{
    public class TimelogService : ITimelogService
    {
        private readonly ILogger<TimelogService> _logger;

        // In-memory data store for Timelogs
        private static readonly List<TimeLog> _timelogs = Enumerable.Range(1, 20).Select(i =>
        {
            var randomCategoryId = new Random().Next(1, 4);
            var category = CategoryService._categories.FirstOrDefault(c => c.Id == randomCategoryId);

            return new TimeLog
            {
                Id = i,
                StartTime = DateTime.Now.AddHours(-i),
                EndTime = DateTime.Now.AddHours(-i + 1),
                CategoryId = randomCategoryId,
                Category = category
            };
        }).ToList();

        private static int _nextId = 21;

        public TimelogService(ILogger<TimelogService> logger)
        {
            _logger = logger;
        }


    public async Task<PagedResponseDto<TimeLogResponse>> GetPagedTimelogsAsync(int page, int pageSize, DateTime? startDate, string? category)
        {
            var query = _timelogs.AsQueryable();

            // Filtering
            if (startDate.HasValue)
            {
                query = query.Where(t => t.StartTime.Date >= startDate.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(t => t.Category != null &&
                    t.Category.Name.Contains(category, StringComparison.OrdinalIgnoreCase));
            }

            // Sorting
            query = query.OrderByDescending(t => t.StartTime);

            // Paging
            return query.ToPagedResponse(page, pageSize, t => t.ToResponse());
        }

    public async Task<TimeLogResponse> GetTimelogByIdAsync(int id)
        {
            var timeLog = _timelogs.FirstOrDefault(t => t.Id == id);
            return timeLog?.ToResponse()!;
        }

        public async Task<TimeLogResponse> CreateTimelogAsync(CreateTimeLogRequest request)
        {
            var entity = request.ToEntity();
            entity.Id = _nextId++;

            entity.Category = CategoryService._categories.FirstOrDefault(c => c.Id == entity.CategoryId);

            _timelogs.Add(entity);
            return entity.ToResponse();
        }

        public async Task<bool> UpdateTimelogAsync(int id, UpdateTimeLogRequest request)
        {
            var existing = _timelogs.FirstOrDefault(t => t.Id == id);
            if (existing == null) return false;

            request.UpdateEntity(existing);

            existing.Category = CategoryService._categories.FirstOrDefault(c => c.Id == existing.CategoryId);

            return true;
        }

        public async Task<bool> DeleteTimelogAsync(int id)
        {
            var existing = _timelogs.FirstOrDefault(t => t.Id == id);
            if (existing == null) return false;

            _timelogs.Remove(existing);
            return true;
        }
    }
}