namespace OrderAPI.Features.TimeLog
{
    public class TimeLog
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; } = DateTime.Now;

        public DateTime? EndTime { get; set; }

        public int CategoryId { get; set; }

        public virtual Category.Category? Category { get; set; }
    }
}