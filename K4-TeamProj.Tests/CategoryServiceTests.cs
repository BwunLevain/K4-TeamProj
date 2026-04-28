using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using Moq;
using TimelogAPI.Features.Categories.Dtos;
using TimelogAPI.Features.TimeLogs.Dtos;
using TimelogAPI.Services;
using Xunit;

namespace K4_TeamProj.Tests.UnitTests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ILogger<CategoryService>> _mockLogger;
        private readonly CategoryService _service;

        public CategoryServiceTests()
        {
            var logger = new Mock<ILogger<CategoryService>>();
            var cache = new Mock<HybridCache>();
            _service = new CategoryService(logger.Object, cache.Object);
        }

        [Fact]
        public async Task CreateCategoryAsync_AddsCategoryAndReturnsResponse()
        {
            // Arrange
            var request = new CreateCategoryRequest("New Test Category", new List<TimeLogResponse>());

            // Act
            var result = await _service.CreateCategoryAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Test Category", result.Name);
            Assert.True(result.Id >= 4);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ReturnsCategory_WhenItExists()
        {
            // Arrange

            // Act
            var result = await _service.GetCategoryByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}