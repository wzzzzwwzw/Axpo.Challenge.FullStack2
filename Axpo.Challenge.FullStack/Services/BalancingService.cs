using Microsoft.AspNetCore.Mvc;
using Axpo.Challenge.FullStack.Models.Domain;
using Axpo.Challenge.FullStack.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Axpo.Challenge.FullStack.Controllers
{
    /// <summary>
    /// Controller for managing balancing operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BalanceService : ControllerBase
    {
        private readonly IBalancingService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalanceService"/> class.
        /// </summary>
        /// <param name="service">The balancing service.</param>
        public BalanceService(IBalancingService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Gets the list of balancing circles.
        /// </summary>
        /// <returns>A list of balancing circles.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BalancingCircle>>> GetBalancingCircles()
        {
            var circles = await _service.GetBalancingCirclesAsync();
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
            return Ok(imbalances);
        }
    }
}