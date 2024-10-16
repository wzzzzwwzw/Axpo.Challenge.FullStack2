using Axpo.Challenge.FullStack.Models.Domain;
using Axpo.Challenge.FullStack.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Axpo.Challenge.Balancing.Controllers
{
    /// <summary>
    /// Controller for managing balancing operations.
    /// </summary>
    [ApiController]
    [Route("api/v1/balancing")]
    public class BalancingController : ControllerBase
    {
        private readonly BalancingService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalancingController"/> class.
        /// </summary>
        /// <param name="service">The balancing service.</param>
        public BalancingController(BalancingService service)
        {
            _service = service;
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
            var forecast = await _service.GetForecastDataForMemberAsync(id); // Updated method call
            return Ok(forecast);
        }
    }
}