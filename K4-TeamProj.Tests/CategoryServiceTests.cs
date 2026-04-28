using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using Moq;
using TimelogAPI.Features.Categories.Dtos;
using TimelogAPI.Features.TimeLogs.Dtos;
using TimelogAPI.Services;
using Xunit;
using System.Threading;

namespace K4_TeamProj.Tests.UnitTests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ILogger<CategoryService>> _mockLogger;
        private readonly Mock<HybridCache> _mockCache;
        private readonly CategoryService _service;

        public CategoryServiceTests()
        {
            _mockLogger = new Mock<ILogger<CategoryService>>();
            _mockCache = new Mock<HybridCache>();
            _service = new CategoryService(_mockLogger.Object, _mockCache.Object);
        }

        [Fact]
        public async Task CreateCategoryAsync_AddsCategoryAndReturnsResponse()
        {
            // Arrange - Fix: Använd konstruktor för record (CS7036)
            var request = new CreateCategoryRequest("New Test Category", new List<TimeLogResponse>());

            // Act
            var result = await _service.CreateCategoryAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Test Category", result.Name);
            Assert.True(result.Id >= 4);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_CategoryExists_InDataStore()
        {
            var category = CategoryService._categories.FirstOrDefault(c => c.Id == 1);
            Assert.NotNull(category);
            Assert.Equal(1, category.Id);
        }
    }
}