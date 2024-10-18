using Axpo.Challenge.FullStack.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Axpo.Challenge.FullStack.Services
{
    /// <summary>
    /// Interface for the balancing service, defining methods to manage balancing circles and calculate imbalances.
    /// </summary>
    public interface IBalancingService
    {
        /// <summary>
        /// Asynchronously retrieves all balancing circles from the data source.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of balancing circles.</returns>
        Task<IEnumerable<BalancingCircle>> GetBalancingCirclesAsync();

        /// <summary>
        /// Asynchronously retrieves forecast data for a specific member based on their ID.
        /// </summary>
        /// <param name="memberId">The ID of the member whose forecast data is to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of forecast data for the specified member.</returns>
        Task<IEnumerable<ForecastData>> GetForecastDataForMemberAsync(int memberId);

        /// <summary>
        /// Asynchronously calculates the imbalances for a specific balancing circle based on its members' forecasts.
        /// </summary>
        /// <param name="balancingCircleId">The ID of the balancing circle for which to calculate imbalances.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a dictionary where the key is the date and the value is the calculated imbalance.</returns>
        Task<Dictionary<DateTime, double>> CalculateImbalancesAsync(int balancingCircleId);
    }
}
