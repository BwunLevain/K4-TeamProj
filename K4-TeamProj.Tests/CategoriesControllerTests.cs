using Microsoft.AspNetCore.Mvc;
using Moq;
using TimelogAPI.Controllers;
using TimelogAPI.Features.Categories.Dtos;
using TimelogAPI.Features.TimeLogs.Dtos;
using TimelogAPI.Services;
using Xunit;

namespace K4_TeamProj.Tests.UnitTests
{
    public class CategoriesControllerTests
    {
        private readonly Mock<ICategoryService> _mockService;
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            _mockService = new Mock<ICategoryService>();
            _controller = new CategoriesController(_mockService.Object);
        }

        [Fact]
        public async Task GetCategoryById_ReturnsCategory_WhenCategoryExists()
        {
            // Arrange
            var expectedCategory = new CategoryResponse(1, "Development", new List<TimeLogResponse>());

            _mockService
                .Setup(s => s.GetCategoryByIdAsync(1))
                .ReturnsAsync(expectedCategory);

            // Act
            var result = await _controller.GetCategoryById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var category = Assert.IsType<CategoryResponse>(okResult.Value);
            Assert.Equal(1, category.Id);
            Assert.Equal("Development", category.Name);
        }

        [Fact]
        public async Task CreateCategory_ReturnsCreatedCategory()
        {
            // Arrange
            var request = new CreateCategoryRequest("Design", new List<TimeLogResponse>());
            var expectedCategory = new CategoryResponse(2, "Design", new List<TimeLogResponse>());

            _mockService
                .Setup(s => s.CreateCategoryAsync(request))
                .ReturnsAsync(expectedCategory);

            // Act
            var result = await _controller.CreateCategory(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var category = Assert.IsType<CategoryResponse>(createdResult.Value);
            Assert.Equal(2, category.Id);
            Assert.Equal("Design", category.Name);
        }
    }
}