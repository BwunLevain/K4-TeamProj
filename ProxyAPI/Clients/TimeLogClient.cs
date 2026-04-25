using ProxyAPI.DTOs;

namespace ProxyAPI.Client
{
    public class TimeLogClient // to prevent socketexhaustion we use typed client in IHttpClientFactory
    {
        private readonly HttpClient _httpClient; // Typed client is used to try specific code and not everything at the same time

        public TimeLogClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TimeLogDto>?> GetTimeLogsAsync()
        {
            var response = await _httpClient.GetAsync("/api/timelog");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<TimeLogDto>>();
        }
    }
}
