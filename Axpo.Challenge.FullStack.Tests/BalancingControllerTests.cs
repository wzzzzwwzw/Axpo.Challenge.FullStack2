using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Axpo.Challenge.FullStack.Controllers;
using Axpo.Challenge.FullStack.Services;
using Axpo.Challenge.FullStack.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Axpo.Challenge.FullStack.Tests
{
    public class BalancingControllerTests
    {
        private readonly Mock<IBalancingService> _mockService;
        private readonly BalancingController _controller;

        public BalancingControllerTests()
        {
            _mockService = new Mock<IBalancingService>();
            _controller = new BalancingController(_mockService.Object);
        }

        [Fact]
        public async Task GetBalancingCircles_ReturnsOkResult_WithListOfBalancingCircles()
        {
            // Arrange
            var testCircles = new List<BalancingCircle> { new BalancingCircle { Id = 1, Name = "Circle1" } };
            _mockService.Setup(service => service.GetBalancingCirclesAsync()).ReturnsAsync(testCircles);

            // Act
            var result = await _controller.GetBalancingCircles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<BalancingCircle>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetMemberForecast_ReturnsOkResult_WithForecastData()
        {
            // Arrange
            var testForecast = new List<ForecastData> { new ForecastData { MemberId = 1, Forecast = 100 } };
            _mockService.Setup(service => service.GetForecastDataForMemberAsync(1)).ReturnsAsync(testForecast);

            // Act
            var result = await _controller.GetMemberForecast(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ForecastData>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetImbalances_ReturnsOkResult_WithImbalanceData()
        {
            // Arrange
            var testImbalances = new Dictionary<DateTime, double> { { DateTime.Today, 10.5 } };
            _mockService.Setup(service => service.CalculateImbalancesAsync()).ReturnsAsync(testImbalances);

            // Act
            var result = await _controller.GetImbalances();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Dictionary<DateTime, double>>(okResult.Value);
            Assert.Single(returnValue);
        }
    }
}