using Axpo.Challenge.FullStack.Models.Domain;
using Axpo.Challenge.FullStack.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace Axpo.Challenge.FullStack.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BalancingController : ControllerBase
    {
        private readonly IBalancingService _balancingService;
        private readonly ILogger<BalancingController> _logger; // Add this line


        public BalancingController(IBalancingService balancingService, ILogger<BalancingController> logger)
        {
            _balancingService = balancingService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BalancingCircle>>> GetBalancingCircles()
        {
            var circles = await _balancingService.GetBalancingCirclesAsync();
            return Ok(circles);
        }

        [HttpGet("member/{memberId}/forecast")]
        public async Task<IActionResult> GetMemberForecast(int memberId)
        {
            var forecasts = await _balancingService.GetForecastDataForMemberAsync(memberId);
            if (forecasts == null)
            {
                return NotFound();
            }
            return Ok(forecasts);
        }

        [HttpGet("{id}/imbalances")]
        public async Task<IActionResult> GetImbalances(int id)
        {
            try
            {
                _logger.LogInformation("Getting imbalances for Balancing Circle ID: {Id}", id); // Add logging
                var imbalances = await _balancingService.CalculateImbalancesAsync(id);
                return Ok(imbalances);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Balancing Circle not found for ID: {Id}", id); // Add logging
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting imbalances for Balancing Circle ID: {Id}", id); // Add logging
                return StatusCode(500, "Internal server error");
            }
        }
    }
}