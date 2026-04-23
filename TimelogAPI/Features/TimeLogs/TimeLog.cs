using TimelogAPI.Features.Categories;

namespace TimelogAPI.Features.TimeLogs
{
    public class TimeLog
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; } = DateTime.Now;

        public DateTime? EndTime { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}