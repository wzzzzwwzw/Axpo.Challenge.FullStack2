using Axpo.Challenge.FullStack.Data;
using Axpo.Challenge.FullStack.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axpo.Challenge.FullStack.Services
{
    /// <summary>
    /// Service class for managing balancing circles and calculating imbalances.
    /// </summary>
    public class BalancingService : IBalancingService
    {
        private readonly EnergyBalanceDbContext _context; // Database context for accessing data
        private readonly ILogger<BalancingService> _logger; // Logger for recording events and errors

        /// <summary>
        /// Constructor for BalancingService.
        /// </summary>
        /// <param name="context">Database context to be used by the service.</param>
        /// <param name="logger">Logger to log information and errors.</param>
        public BalancingService(EnergyBalanceDbContext context, ILogger<BalancingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Retrieves all balancing circles along with their members.
        /// </summary>
        /// <returns>A list of balancing circles.</returns>
        public async Task<IEnumerable<BalancingCircle>> GetBalancingCirclesAsync()
        {
            return await _context.BalancingCircles.Include(bc => bc.Members).ToListAsync();
        }

        /// <summary>
        /// Retrieves forecast data for a specific member.
        /// </summary>
        /// <param name="memberId">The ID of the member for which to retrieve forecast data.</param>
        /// <returns>A list of forecast data for the specified member.</returns>
        public async Task<IEnumerable<ForecastData>> GetForecastDataForMemberAsync(int memberId)
        {
            return await _context.Forecasts.Where(f => f.MemberId == memberId).ToListAsync();
        }

        /// <summary>
        /// Calculates imbalances for a specific balancing circle based on its members' forecasts.
        /// </summary>
        /// <param name="balancingCircleId">The ID of the balancing circle for which to calculate imbalances.</param>
        /// <returns>A dictionary where the key is the date and the value is the calculated imbalance.</returns>
        public async Task<Dictionary<DateTime, double>> CalculateImbalancesAsync(int balancingCircleId)
        {
            _logger.LogInformation("Calculating imbalances for Balancing Circle ID: {Id}", balancingCircleId);

            try
            {
                // Retrieve the balancing circle along with its members and their forecasts
                var circle = await _context.BalancingCircles
                    .Include(bc => bc.Members) // Include members of the balancing circle
                    .ThenInclude(m => m.Forecasts) // Include forecasts for each member
                    .FirstOrDefaultAsync(bc => bc.Id == balancingCircleId); // Find by ID

                // Check if the balancing circle exists
                if (circle == null)
                {
                    _logger.LogWarning("Balancing circle not found for ID: {Id}", balancingCircleId);
                    throw new KeyNotFoundException("Balancing circle not found");
                }

                // Dictionary to hold imbalances keyed by date
                var imbalances = new Dictionary<DateTime, double>();

                // Iterate through each member of the balancing circle
                foreach (var member in circle.Members)
                {
                    _logger.LogInformation("Processing member ID: {MemberId}", member.Id);
                    // Iterate through each forecast for the member
                    foreach (var forecast in member.Forecasts)
                    {
                        _logger.LogInformation("Processing forecast for date: {Date}, value: {Value}", forecast.Date, forecast.Forecast);

                        // Initialize the date in the imbalances dictionary if it doesn't exist
                        if (!imbalances.ContainsKey(forecast.Date))
                        {
                            imbalances[forecast.Date] = 0; // Set initial imbalance to 0
                        }

                        // Update the imbalance based on whether the member is a producer or consumer
                        if (member.IsProducer)
                        {
                            imbalances[forecast.Date] += forecast.Forecast; // Add inflow for producers
                        }
                        else
                        {
                            imbalances[forecast.Date] -= forecast.Forecast; // Subtract outflow for consumers
                        }
                    }
                }

                _logger.LogInformation("Calculated imbalances for Balancing Circle ID: {Id}", balancingCircleId);
                return imbalances; // Return the final imbalances
            }
            catch (Exception ex)
            {
                // Log the error and rethrow
                _logger.LogError(ex, "An error occurred while calculating imbalances for Balancing Circle ID: {Id}", balancingCircleId);
                throw;
            }
        }
    }
}
