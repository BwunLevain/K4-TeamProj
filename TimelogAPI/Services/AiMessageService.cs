using System.Text.Json;
using TimelogAPI.Features.AiComments;
using TimelogAPI.Features.TimeLogs;

namespace TimelogAPI.Services
{
    public class AiMessageService : IAiMessageService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AiMessageService> _logger;

        public AiMessageService(IHttpClientFactory httpClientFactory, ILogger<AiMessageService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<string> GenerateMessageAsync(string prompt)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProxyApiClient");
                var response = await client.PostAsJsonAsync("api/ai/ask", prompt);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                _logger.LogWarning("AI Service returnerade status: {StatusCode}", response.StatusCode);
                return "Error: Kunde inte generera innehåll.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tekniskt fel vid kontakt med AI-tjänsten.");
                return "Error: AI-tjänsten är otillgänglig.";
            }
        }

        public async Task<string> GenerateTimelogFeedbackMessageAsync(List<TimeLog> timeLogs)
        {
            try
            {
                var logsJson = JsonSerializer.Serialize(timeLogs);
                var formattedPrompt = $"Kolla på mina loggade timer från en tidsloggningsapp och ge mig antingen en positiv affrimation om jag gjorde bra eller en förolämpning om jag gjorde dåligt. Här är datan: {logsJson}";

                var client = _httpClientFactory.CreateClient("ProxyApiClient");
                var response = await client.PostAsJsonAsync("api/ai/ask", formattedPrompt);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                _logger.LogWarning("AI Service returnerade status: {StatusCode}", response.StatusCode);
                return "Error: Kunde inte generera innehåll.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tekniskt fel vid kontakt med AI-tjänsten.");
                return "Error: AI-tjänsten är otillgänglig.";
            }
        }

        public async Task<string> GetSimpleAiResponseAsync(string prompt)
        {
            var client = _httpClientFactory.CreateClient("ProxyApiClient");

            var payload = new { Prompt = prompt };

            var response = await client.PostAsJsonAsync("api/ai/ask", payload);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AiMessageResponse>();
                return result?.GeneratedMessage ?? "Tomt svar från AI.";
            }

            var error = await response.Content.ReadAsStringAsync();
            return $"Error: ProxyAPI svarade {response.StatusCode} - {error}";
        }
    }
}