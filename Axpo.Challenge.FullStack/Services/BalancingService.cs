using Axpo.Challenge.FullStack.Data;
using Axpo.Challenge.FullStack.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axpo.Challenge.FullStack.Services
{
    public class BalancingService : IBalancingService
    {
        private readonly EnergyBalanceDbContext _context;
            private readonly ILogger<BalancingService> _logger;


        public BalancingService(EnergyBalanceDbContext context,ILogger<BalancingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<BalancingCircle>> GetBalancingCirclesAsync()
        {
            return await _context.BalancingCircles.Include(bc => bc.Members).ToListAsync();
        }

        public async Task<IEnumerable<ForecastData>> GetForecastDataForMemberAsync(int memberId)
        {
            return await _context.Forecasts.Where(f => f.MemberId == memberId).ToListAsync();
        }

   public async Task<Dictionary<DateTime, double>> CalculateImbalancesAsync(int balancingCircleId)
    {
        _logger.LogInformation("Calculating imbalances for Balancing Circle ID: {Id}", balancingCircleId);

        try
        {
            var circle = await _context.BalancingCircles
                .Include(bc => bc.Members)
                .ThenInclude(m => m.Forecasts)
                .FirstOrDefaultAsync(bc => bc.Id == balancingCircleId);

            if (circle == null)
            {
                _logger.LogWarning("Balancing circle not found for ID: {Id}", balancingCircleId);
                throw new KeyNotFoundException("Balancing circle not found");
            }

            var imbalances = new Dictionary<DateTime, double>();

            foreach (var member in circle.Members)
            {
                _logger.LogInformation("Processing member ID: {MemberId}", member.Id);
                foreach (var forecast in member.Forecasts)
                {
                    _logger.LogInformation("Processing forecast for date: {Date}, value: {Value}", forecast.Date, forecast.Forecast);
                    if (!imbalances.ContainsKey(forecast.Date))
                    {
                        imbalances[forecast.Date] = 0;
                    }

                    if (member.IsProducer)
                    {
                        imbalances[forecast.Date] += forecast.Forecast;
                    }
                    else
                    {
                        imbalances[forecast.Date] -= forecast.Forecast;
                    }
                }
            }

            _logger.LogInformation("Calculated imbalances for Balancing Circle ID: {Id}", balancingCircleId);
            return imbalances;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while calculating imbalances for Balancing Circle ID: {Id}", balancingCircleId);
            throw;
        }
    }
}
}