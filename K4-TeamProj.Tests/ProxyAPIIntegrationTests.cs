using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using ProxyAPI;

namespace K4_TeamProj.Tests
{
    public class ProxyApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProxyApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_TimeLogs_Returns_OK()
        {
            var response = await _client.GetAsync("/api/proxy/timelogs");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_TimeLogs_Returns_Data()
        {
            var response = await _client.GetAsync("/api/proxy/timelogs");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            Assert.False(string.IsNullOrWhiteSpace(content));
        }

        [Fact]
        public async Task Get_InvalidRoute_Returns_NotFound()
        {
            var response = await _client.GetAsync("/api/proxy/doesnotexist");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}