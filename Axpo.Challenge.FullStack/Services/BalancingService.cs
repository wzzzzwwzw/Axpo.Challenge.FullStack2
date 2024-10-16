using Axpo.Challenge.FullStack.Data;
using Axpo.Challenge.FullStack.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Axpo.Challenge.FullStack.Services
{
    /// <summary>
    /// Service for managing balancing operations.
    /// </summary>
    public class BalancingService
    {
        private readonly EnergyBalanceDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalancingService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public BalancingService(EnergyBalanceDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the list of balancing circles.
        /// </summary>
        /// <returns>A list of balancing circles.</returns>
        public async Task<IEnumerable<BalancingCircle>> GetBalancingCirclesAsync()
        {
            return await _context.BalancingCircles.Include(b => b.Members).ToListAsync();
        }

        /// <summary>
        /// Gets the forecast data for a specific member.
        /// </summary>
        /// <param name="memberId">The member ID.</param>
        /// <returns>The forecast data for the specified member.</returns>
        public async Task<IEnumerable<ForecastData>> GetForecastDataForMemberAsync(int memberId)
        {
            return await _context.Forecasts.Where(f => f.MemberId == memberId).ToListAsync();
        }
    }
}