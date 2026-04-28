using Microsoft.AspNetCore.Mvc;
using Moq;
using TimelogAPI.Controllers;
using TimelogAPI.Features.TimeLogs.Dtos;
using TimelogAPI.Services;
using Xunit;

namespace K4_TeamProj.Tests.UnitTests
{
    public class TimelogsControllerTests
    {
        private readonly Mock<ITimelogService> _mockService;
        private readonly TimelogsController _controller;

        public TimelogsControllerTests()
        {
            _mockService = new Mock<ITimelogService>();
            _controller = new TimelogsController(_mockService.Object);
        }

        [Fact]
        public async Task GetTimelogById_ReturnsTimelog_WhenExists()
        {
            // Arrange
            var expectedLog = new TimeLogResponse(1, DateTime.Now, null, 1);

            _mockService
                .Setup(s => s.GetTimelogByIdAsync(1))
                .ReturnsAsync(expectedLog);

            // Act
            var result = await _controller.GetTimelogById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var timelog = Assert.IsType<TimeLogResponse>(okResult.Value);
            Assert.Equal(1, timelog.Id);
        }

        [Fact]
        public async Task DeleteTimelog_ReturnsNoContent_WhenDeleted()
        {
            // Arrange
            _mockService
                .Setup(s => s.DeleteTimelogAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteTimelog(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}