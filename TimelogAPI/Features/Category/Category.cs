namespace TimelogAPI.Features.Category
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual List<TimeLog.TimeLog> TimeLogs { get; set; } = new List<TimeLog.TimeLog>();
    }
}