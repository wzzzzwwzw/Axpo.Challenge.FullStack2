using System;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Axpo.Challenge.FullStack.Models.Domain;
using Microsoft.Extensions.Logging;

namespace Axpo.Challenge.FullStack.Services
{
    /// <summary>
    /// Service for managing balancing operations.
    /// </summary>
    public class BalancingService : IBalancingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BalancingService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalancingService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="logger">The logger.</param>
        public BalancingService(HttpClient httpClient, ILogger<BalancingService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient.BaseAddress = new Uri("http://localhost:5295/"); // Set the base address
        }

        /// <summary>
        /// Gets the list of balancing circles.
        /// </summary>
        /// <returns>A list of balancing circles.</returns>
        public async Task<IEnumerable<BalancingCircle>> GetBalancingCirclesAsync()
{
    try
    {
        var response = await _httpClient.GetAsync("api/v1/balancing");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var circles = JsonSerializer.Deserialize<IEnumerable<BalancingCircle>>(content);
        return circles ?? new List<BalancingCircle>();
    }
    catch (HttpRequestException ex)
    {
        _logger.LogError(ex, "Error fetching balancing circles");
        // Fallback or custom handling
        return new List<BalancingCircle>(); // Returning an empty list to prevent crashing
    }
}
        /// <summary>
        /// Gets the forecast data for a specific member.
        /// </summary>
        /// <param name="memberId">The member ID.</param>
        /// <returns>The forecast data for the specified member.</returns>
        public async Task<IEnumerable<ForecastData>> GetForecastDataForMemberAsync(int memberId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"balancing/member/{memberId}/forecast");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<IEnumerable<ForecastData>>(content) ?? new List<ForecastData>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Error fetching forecast data for member {memberId}");
                throw new Exception($"Could not fetch forecast data for member {memberId}", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"Error deserializing forecast data for member {memberId}");
                throw new Exception($"Could not deserialize forecast data for member {memberId}", ex);
            }
        }

        /// <summary>
        /// Calculates the imbalances for each balancing circle.
        /// </summary>
        /// <returns>A dictionary with the date and corresponding imbalance value.</returns>
        public async Task<Dictionary<DateTime, double>> CalculateImbalancesAsync()
        {
            var imbalances = new Dictionary<DateTime, double>();

            try
            {
                var circles = await GetBalancingCirclesAsync();

                foreach (var circle in circles)
                {
                    foreach (var member in circle.Members)
                    {
                        var forecastData = await GetForecastDataForMemberAsync(member.Id);
                        foreach (var data in forecastData)
                        {
                            if (!imbalances.ContainsKey(data.Date))
                            {
                                imbalances[data.Date] = 0;
                            }

                            if (member.IsProducer)
                            {
                                imbalances[data.Date] += data.Forecast; // assuming data.Forecast is the inflow
                            }
                            else
                            {
                                imbalances[data.Date] -= data.Forecast; // assuming data.Forecast is the outflow
                            }
                        }
                    }
                }

                return imbalances;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating imbalances");
                throw new Exception("Could not calculate imbalances", ex);
            }
        }
    }
}
