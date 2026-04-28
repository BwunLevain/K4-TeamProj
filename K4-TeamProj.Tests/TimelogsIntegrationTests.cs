using System.Net.Http.Headers;
using Xunit;

namespace K4_TeamProj.Tests.IntegrationTests
{
    public class TimelogsIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TimelogsIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        }

        [Theory]
        [InlineData("/api/v1/timelogs")]
        [InlineData("/api/v1/timelogs/1")]
        public async Task Endpoints_ReturnSuccess(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}