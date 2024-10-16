using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Axpo.Challenge.FullStack.Models.Domain;

namespace Axpo.Challenge.FullStack.Services
{
    /// <summary>
    /// Service for managing balancing operations.
    /// </summary>
    public class BalancingService : IBalancingService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalancingService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public BalancingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Gets the list of balancing circles.
        /// </summary>
        /// <returns>A list of balancing circles.</returns>
        public async Task<IEnumerable<BalancingCircle>> GetBalancingCirclesAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/balancing");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var circles = JsonSerializer.Deserialize<IEnumerable<BalancingCircle>>(content);
            return circles ?? Enumerable.Empty<BalancingCircle>();
        }

        /// <summary>
        /// Gets the forecast data for a specific member.
        /// </summary>
        /// <param name="memberId">The member ID.</param>
        /// <returns>The forecast data for the specified member.</returns>
        public async Task<IEnumerable<ForecastData>> GetForecastDataForMemberAsync(int memberId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/balancing/member/{memberId}/forecast");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var forecastData = JsonSerializer.Deserialize<IEnumerable<ForecastData>>(content);
            return forecastData ?? Enumerable.Empty<ForecastData>();
        }

        /// <summary>
        /// Calculates the imbalances for each balancing circle.
        /// </summary>
        /// <returns>A dictionary with the date and corresponding imbalance value.</returns>
        public async Task<Dictionary<DateTime, double>> CalculateImbalancesAsync()
        {
            var circles = await GetBalancingCirclesAsync();
            var imbalances = new Dictionary<DateTime, double>();

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
                            imbalances[data.Date] += data.Forecast;
                        }
                        else
                        {
                            imbalances[data.Date] -= data.Forecast;
                        }
                    }
                }
            }

            return imbalances;
        }
    }
}