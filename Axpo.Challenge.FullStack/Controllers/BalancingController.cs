using Axpo.Challenge.FullStack.Models.Domain;
using Axpo.Challenge.FullStack.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Axpo.Challenge.FullStack.Controllers
{
    /// <summary>
    /// Handles requests related to balancing circles and forecasts.
    /// </summary>
    [ApiController] // Indicates that this class is an API controller
    [Route("api/v1/[controller]")] // Sets the route for the controller
    public class BalancingController : ControllerBase
    {
        private readonly IBalancingService _balancingService; // Service to handle balancing operations
        private readonly ILogger<BalancingController> _logger; // Logger for this controller

        /// <summary>
        /// Initializes a new instance of the <see cref="BalancingController"/> class.
        /// </summary>
        /// <param name="balancingService">The service to handle balancing operations.</param>
        /// <param name="logger">The logger for this controller.</param>
        public BalancingController(IBalancingService balancingService, ILogger<BalancingController> logger)
        {
            _balancingService = balancingService; // Dependency injection of balancing service
            _logger = logger; // Dependency injection of logger
        }

        /// <summary>
        /// Retrieves a list of all balancing circles.
        /// </summary>
        /// <returns>A list of <see cref="BalancingCircle"/> objects.</returns>
        // GET: api/v1/balancing
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BalancingCircle>>> GetBalancingCircles()
        {
            // Fetches all balancing circles from the service
            var circles = await _balancingService.GetBalancingCirclesAsync();
            return Ok(circles); // Returns a 200 OK response with the circles
        }

        /// <summary>
        /// Retrieves forecast data for a specific member.
        /// </summary>
        /// <param name="memberId">The ID of the member for whom to retrieve forecast data.</param>
        /// <returns>A list of forecast data for the specified member, or a 404 Not Found result if none exist.</returns>
        // GET: api/v1/balancing/member/{memberId}/forecast
        [HttpGet("member/{memberId}/forecast")]
        public async Task<IActionResult> GetMemberForecast(int memberId)
        {
            // Fetches forecast data for a specific member
            var forecasts = await _balancingService.GetForecastDataForMemberAsync(memberId);
            if (forecasts == null)
            {
                return NotFound(); // Returns 404 if no forecasts found
            }
            return Ok(forecasts); // Returns 200 OK with the forecast data
        }

        /// <summary>
        /// Retrieves imbalances for a specific balancing circle.
        /// </summary>
        /// <param name="id">The ID of the balancing circle for which to calculate imbalances.</param>
        /// <returns>A dictionary of imbalances for the specified balancing circle, or an appropriate error result.</returns>
        // GET: api/v1/balancing/{id}/imbalances
        [HttpGet("{id}/imbalances")]
        public async Task<IActionResult> GetImbalances(int id)
        {
            try
            {
                // Log the request for getting imbalances
                _logger.LogInformation("Getting imbalances for Balancing Circle ID: {Id}", id);
                
                // Calculates imbalances for the specified balancing circle
                var imbalances = await _balancingService.CalculateImbalancesAsync(id);
                return Ok(imbalances); // Returns 200 OK with the imbalances data
            }
            catch (KeyNotFoundException ex)
            {
                // Log a warning if the balancing circle ID was not found
                _logger.LogWarning(ex, "Balancing Circle not found for ID: {Id}", id);
                return NotFound(ex.Message); // Returns 404 Not Found with the error message
            }
            catch (Exception ex)
            {
                // Log an error for any unexpected exceptions
                _logger.LogError(ex, "An error occurred while getting imbalances for Balancing Circle ID: {Id}", id);
                return StatusCode(500, "Internal server error"); // Returns 500 Internal Server Error
            }
        }
    }
}
