using ProxyAPI.DTOs;

namespace ProxyAPI.Client
{
    public class TimeLogClient
    {
        private readonly HttpClient _httpClient;

        public TimeLogClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TimeLogDto>?> GetTimeLogsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TimeLogDto>>("/api/timelog");
        }
    }
}
