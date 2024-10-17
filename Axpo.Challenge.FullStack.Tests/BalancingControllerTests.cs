using Axpo.Challenge.FullStack.Controllers;
using Axpo.Challenge.FullStack.Models.Domain;
using Axpo.Challenge.FullStack.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Axpo.Challenge.FullStack.Tests.Controllers
{
    public class BalancingControllerTests
    {
        private readonly Mock<IBalancingService> _mockBalancingService;
        private readonly Mock<ILogger<BalancingController>> _mockLogger;
        private readonly BalancingController _controller;

        public BalancingControllerTests()
        {
            _mockBalancingService = new Mock<IBalancingService>();
            _mockLogger = new Mock<ILogger<BalancingController>>();
            _controller = new BalancingController(_mockBalancingService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetBalancingCircles_ReturnsOkResult_WithListOfBalancingCircles()
        {
            // Arrange
            var circles = new List<BalancingCircle>
            {
                new BalancingCircle { Id = 1, Name = "Circle 1" },
                new BalancingCircle { Id = 2, Name = "Circle 2" }
            };
            _mockBalancingService.Setup(service => service.GetBalancingCirclesAsync())
                .ReturnsAsync(circles);

            // Act
            var result = await _controller.GetBalancingCircles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<BalancingCircle>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetMemberForecast_ReturnsOkResult_WithListOfForecastData()
        {
            // Arrange
            var forecasts = new List<ForecastData>
            {
                new ForecastData { Id = 1, MemberId = 1, Date = DateTime.UtcNow, Forecast = 100 },
                new ForecastData { Id = 2, MemberId = 1, Date = DateTime.UtcNow.AddDays(1), Forecast = 200 }
            };
            _mockBalancingService.Setup(service => service.GetForecastDataForMemberAsync(1))
                .ReturnsAsync(forecasts);

            // Act
            var result = await _controller.GetMemberForecast(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ForecastData>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetMemberForecast_ReturnsNotFound_WhenForecastsNotFound()
        {
            // Arrange
            _mockBalancingService.Setup(service => service.GetForecastDataForMemberAsync(1))
                .ReturnsAsync((List<ForecastData>)null);

            // Act
            var result = await _controller.GetMemberForecast(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetImbalances_ReturnsOkResult_WithDictionaryOfImbalances()
        {
            // Arrange
            var imbalances = new Dictionary<DateTime, double>
            {
                { DateTime.UtcNow, 100 },
                { DateTime.UtcNow.AddDays(1), -200 }
            };
            _mockBalancingService.Setup(service => service.CalculateImbalancesAsync(1))
                .ReturnsAsync(imbalances);

            // Act
            var result = await _controller.GetImbalances(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Dictionary<DateTime, double>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetImbalances_ReturnsNotFound_WhenBalancingCircleNotFound()
        {
            // Arrange
            _mockBalancingService.Setup(service => service.CalculateImbalancesAsync(1))
                .ThrowsAsync(new KeyNotFoundException("Balancing circle not found"));

            // Act
            var result = await _controller.GetImbalances(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Balancing circle not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetImbalances_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockBalancingService.Setup(service => service.CalculateImbalancesAsync(1))
                .ThrowsAsync(new Exception("Internal server error"));

            // Act
            var result = await _controller.GetImbalances(1);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Internal server error", statusCodeResult.Value);
        }
    }
}