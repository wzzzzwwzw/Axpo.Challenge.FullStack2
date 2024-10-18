using Axpo.Challenge.FullStack.Data;
using Axpo.Challenge.FullStack.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Axpo.Challenge.FullStack.SeedData
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(EnergyBalanceDbContext context, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("DataSeeder");

            if (await context.BalancingCircles.AnyAsync())
            {
                logger.LogInformation("Database already seeded.");
                return; // DB has been seeded
            }

            try
            {
                var balancingCircles = new List<BalancingCircle>
        {
            new BalancingCircle { Name = "Circle 1" },
            new BalancingCircle { Name = "Circle 2" },
        };

                await context.BalancingCircles.AddRangeAsync(balancingCircles);
                await context.SaveChangesAsync();
                logger.LogInformation("Balancing circles seeded.");

                var members = new List<Member>
        {
            new Member { Name = "Member 1", Type = "Producer", Category = "Solar", IsProducer = true, BalancingCircleId = balancingCircles[0].Id },
            new Member { Name = "Member 2", Type = "Consumer", Category = "Retailer", IsProducer = false, BalancingCircleId = balancingCircles[1].Id },
        };

                await context.Members.AddRangeAsync(members);
                await context.SaveChangesAsync();
                logger.LogInformation("Members seeded.");

                var forecasts = new List<ForecastData>
        {
            new ForecastData { MemberId = members[0].Id, Date = DateTime.UtcNow, Forecast = 100 },
            new ForecastData { MemberId = members[1].Id, Date = DateTime.UtcNow.AddDays(1), Forecast = 200 },
        };

                await context.Forecasts.AddRangeAsync(forecasts);
                logger.LogInformation("Forecast data seeded.");

                var timeseriesEntries = new List<TimeseriesEntry>
        {
            new TimeseriesEntry { MemberId = members[0].Id, Date = DateTime.UtcNow, Value = 50 },
            new TimeseriesEntry { MemberId = members[1].Id, Date = DateTime.UtcNow.AddDays(1), Value = 150 },
        };

                await context.TimeseriesEntries.AddRangeAsync(timeseriesEntries);
                await context.SaveChangesAsync();
                logger.LogInformation("Timeseries entries seeded.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
    }
}