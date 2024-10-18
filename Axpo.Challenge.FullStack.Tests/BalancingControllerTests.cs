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
        // Mocks for dependencies
        private readonly Mock<IBalancingService> _mockBalancingService;
        private readonly Mock<ILogger<BalancingController>> _mockLogger;
        private readonly BalancingController _controller;

        // Constructor to initialize mocks and the controller
        public BalancingControllerTests()
        {
            _mockBalancingService = new Mock<IBalancingService>();
            _mockLogger = new Mock<ILogger<BalancingController>>();
            _controller = new BalancingController(_mockBalancingService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetBalancingCircles_ReturnsOkResult_WithListOfBalancingCircles()
        {
            // Arrange: Set up expected data
            var circles = new List<BalancingCircle>
            {
                new BalancingCircle { Id = 1, Name = "Circle 1" },
                new BalancingCircle { Id = 2, Name = "Circle 2" }
            };
            // Setup the mock service to return the expected list
            _mockBalancingService.Setup(service => service.GetBalancingCirclesAsync())
                .ReturnsAsync(circles);

            // Act: Call the method under test
            var result = await _controller.GetBalancingCircles();

            // Assert: Verify the result is as expected
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<BalancingCircle>>(okResult.Value);
            Assert.Equal(2, returnValue.Count); // Check the count of the circles
        }

        [Fact]
        public async Task GetMemberForecast_ReturnsOkResult_WithListOfForecastData()
        {
            // Arrange: Set up expected forecast data for a member
            var forecasts = new List<ForecastData>
            {
                new ForecastData { Id = 1, MemberId = 1, Date = DateTime.UtcNow, Forecast = 100 },
                new ForecastData { Id = 2, MemberId = 1, Date = DateTime.UtcNow.AddDays(1), Forecast = 200 }
            };
            // Setup the mock service to return the expected forecasts for member ID 1
            _mockBalancingService.Setup(service => service.GetForecastDataForMemberAsync(1))
                .ReturnsAsync(forecasts);

            // Act: Call the method under test
            var result = await _controller.GetMemberForecast(1);

            // Assert: Verify the result is as expected
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ForecastData>>(okResult.Value);
            Assert.Equal(2, returnValue.Count); // Check the count of forecasts
        }

        [Fact]
        public async Task GetMemberForecast_ReturnsNotFound_WhenForecastsNotFound()
        {
            // Arrange: Setup the mock to return null when no forecasts are found
            _mockBalancingService.Setup(service => service.GetForecastDataForMemberAsync(1))
                .ReturnsAsync((List<ForecastData>)null);

            // Act: Call the method under test
            var result = await _controller.GetMemberForecast(1);

            // Assert: Verify that a NotFound result is returned
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetImbalances_ReturnsOkResult_WithDictionaryOfImbalances()
        {
            // Arrange: Set up expected imbalances data
            var imbalances = new Dictionary<DateTime, double>
            {
                { DateTime.UtcNow, 100 },
                { DateTime.UtcNow.AddDays(1), -200 }
            };
            // Setup the mock service to return the expected imbalances for balancing circle ID 1
            _mockBalancingService.Setup(service => service.CalculateImbalancesAsync(1))
                .ReturnsAsync(imbalances);

            // Act: Call the method under test
            var result = await _controller.GetImbalances(1);

            // Assert: Verify the result is as expected
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Dictionary<DateTime, double>>(okResult.Value);
            Assert.Equal(2, returnValue.Count); // Check the count of imbalance entries
        }

        [Fact]
        public async Task GetImbalances_ReturnsNotFound_WhenBalancingCircleNotFound()
        {
            // Arrange: Setup the mock to throw an exception when the balancing circle is not found
            _mockBalancingService.Setup(service => service.CalculateImbalancesAsync(1))
                .ThrowsAsync(new KeyNotFoundException("Balancing circle not found"));

            // Act: Call the method under test
            var result = await _controller.GetImbalances(1);

            // Assert: Verify that a NotFoundObjectResult is returned with the correct message
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Balancing circle not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetImbalances_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange: Setup the mock to throw a general exception
            _mockBalancingService.Setup(service => service.CalculateImbalancesAsync(1))
                .ThrowsAsync(new Exception("Internal server error"));

            // Act: Call the method under test
            var result = await _controller.GetImbalances(1);

            // Assert: Verify that an Internal Server Error result is returned
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode); // Check for 500 status code
            Assert.Equal("Internal server error", statusCodeResult.Value); // Check for error message
        }
    }
}
