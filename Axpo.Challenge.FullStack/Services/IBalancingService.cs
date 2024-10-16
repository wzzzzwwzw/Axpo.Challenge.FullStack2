using Axpo.Challenge.FullStack.Models.Domain;

namespace Axpo.Challenge.FullStack.Services
{
    public interface IBalancingService
    {
        Task<IEnumerable<BalancingCircle>> GetBalancingCirclesAsync();
        Task<IEnumerable<ForecastData>> GetForecastDataForMemberAsync(int memberId);
        Task<Dictionary<DateTime, double>> CalculateImbalancesAsync();
    }
}