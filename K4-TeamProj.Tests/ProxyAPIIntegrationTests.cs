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
    }
}