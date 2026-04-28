using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TimelogAPI.Features.Categories.Dtos;
using TimelogAPI.Features.TimeLogs.Dtos;
using TimelogAPI.Features.Common;
using Xunit;

namespace K4_TeamProj.Tests.IntegrationTests
{
    public class CategoriesIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CategoriesIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        }

        [Fact]
        public async Task GetCategories_ReturnsSuccessAndPagedData()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/categories");

            // Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var pagedResult = JsonSerializer.Deserialize<PagedResponseDto<CategoryResponse>>(
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            Assert.NotNull(pagedResult);
            Assert.True(pagedResult.Data.Any());
        }

        [Fact]
        public async Task GetCategoryById_ReturnsCategory_WhenExists()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/categories/1");

            // Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var category = JsonSerializer.Deserialize<CategoryResponse>(
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            Assert.NotNull(category);
            Assert.Equal(1, category.Id);
        }

        [Fact]
        public async Task CreateCategory_ReturnsCreatedCategory()
        {
            // Arrange
            var request = new CreateCategoryRequest("Integration Test Category", new List<TimeLogResponse>());
            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await _client.PostAsync("/api/v1/categories", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdCategory = JsonSerializer.Deserialize<CategoryResponse>(
                responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            Assert.NotNull(createdCategory);
            Assert.Equal("Integration Test Category", createdCategory.Name);
            Assert.True(createdCategory.Id > 0);
        }
    }
}