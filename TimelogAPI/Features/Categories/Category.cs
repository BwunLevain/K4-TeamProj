using TimelogAPI.Features.TimeLogs;

namespace TimelogAPI.Features.Categories
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
    }
}