namespace ProxyAPI.DTOs
{
    public class TimeLogDto
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int CategoryId { get; set; }
    }
}