using Axpo.Challenge.FullStack.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Axpo.Challenge.FullStack.Services
{
    public interface IBalancingService
    {
        Task<IEnumerable<BalancingCircle>> GetBalancingCirclesAsync();
        Task<IEnumerable<ForecastData>> GetForecastDataForMemberAsync(int memberId);
        Task<Dictionary<DateTime, double>> CalculateImbalancesAsync(int balancingCircleId);
    }
}