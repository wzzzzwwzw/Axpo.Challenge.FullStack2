using Microsoft.AspNetCore.Mvc;
using Axpo.Challenge.FullStack.Models.Domain;
using Axpo.Challenge.FullStack.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; // Ensure to include this
using Microsoft.Extensions.Logging;

namespace Axpo.Challenge.FullStack.Controllers
{
    /// <summary>
    /// Controller for managing balancing operations.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BalancingController : ControllerBase
    {
        private readonly IBalancingService _service;
        private readonly ILogger<BalancingController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalancingController"/> class.
        /// </summary>
        /// <param name="service">The balancing service.</param>
        /// <param name="logger">The logger.</param>
        public BalancingController(IBalancingService service, ILogger<BalancingController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Gets the list of balancing circles.
        /// </summary>
        /// <returns>A list of balancing circles.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BalancingCircle>>> GetBalancingCircles()
        {
            var circles = await _service.GetBalancingCirclesAsync();
            
            if (circles == null || !circles.Any())
            {
                _logger.LogWarning("No balancing circles were found."); // Log a warning
                return NotFound(); // Handle the case where no data is found
            }

            return Ok(circles);
        }

        /// <summary>
        /// Gets the forecast data for a specific member.
        /// </summary>
        /// <param name="id">The member ID.</param>
        /// <returns>The forecast data for the specified member.</returns>
        [HttpGet("member/{id}/forecast")]
        public async Task<ActionResult<IEnumerable<ForecastData>>> GetMemberForecast(int id)
        {
            var forecast = await _service.GetForecastDataForMemberAsync(id);

            if (forecast == null || !forecast.Any())
            {
                _logger.LogWarning($"No forecast data found for member {id}."); // Log a warning
                return NotFound(); // Handle no forecast data
            }

            return Ok(forecast);
        }

        /// <summary>
        /// Gets the imbalances for each balancing circle.
        /// </summary>
        /// <returns>A dictionary with the date and corresponding imbalance value.</returns>
        [HttpGet("imbalances")]
        public async Task<ActionResult<Dictionary<DateTime, double>>> GetImbalances()
        {
            var imbalances = await _service.CalculateImbalancesAsync();

            if (imbalances == null || !imbalances.Any())
            {
                _logger.LogWarning("No imbalances data found."); // Log a warning
                return NotFound(); // Handle no imbalances data
            }

            return Ok(imbalances);
        }
    }
}
