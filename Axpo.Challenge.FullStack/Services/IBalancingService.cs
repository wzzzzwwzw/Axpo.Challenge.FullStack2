using Axpo.Challenge.FullStack.Models.Domain;

namespace Axpo.Challenge.FullStack.Services
{
    /// <summary>
    /// Provides methods to manage balancing services.
    /// </summary>
    public interface IBalancingService
    {
        /// <summary>
        /// Gets the balancing circles asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of balancing circles.</returns>
        Task<IEnumerable<BalancingCircle>> GetBalancingCirclesAsync();

        /// <summary>
        /// Gets the forecast data for a specific member asynchronously.
        /// </summary>
        /// <param name="memberId">The ID of the member.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of forecast data.</returns>
        Task<IEnumerable<ForecastData>> GetForecastDataForMemberAsync(int memberId);

        /// <summary>
        /// Calculates the imbalances asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a dictionary with date and imbalance value.</returns>
        Task<Dictionary<DateTime, double>> CalculateImbalancesAsync();
    }
}