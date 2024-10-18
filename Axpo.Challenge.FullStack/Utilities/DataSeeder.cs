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
        // Asynchronously seeds data into the EnergyBalanceDbContext.
        public static async Task SeedDataAsync(EnergyBalanceDbContext context, ILoggerFactory loggerFactory)
        {
            // Create a logger for the DataSeeder class.
            var logger = loggerFactory.CreateLogger("DataSeeder");

            // Check if the database has already been seeded with BalancingCircles.
            if (await context.BalancingCircles.AnyAsync())
            {
                logger.LogInformation("Database already seeded.");
                return; // Exit if data is already present.
            }

            try
            {
                // Create a list of BalancingCircle instances to seed.
                var balancingCircles = new List<BalancingCircle>
                {
                    new BalancingCircle { Name = "Circle 1" },
                    new BalancingCircle { Name = "Circle 2" },
                };

                // Add the new balancing circles to the database context.
                await context.BalancingCircles.AddRangeAsync(balancingCircles);
                await context.SaveChangesAsync(); // Persist changes to the database.
                logger.LogInformation("Balancing circles seeded.");

                // Create a list of Member instances to seed.
                var members = new List<Member>
                {
                    new Member { Name = "Member 1", Type = "Producer", Category = "Solar", IsProducer = true, BalancingCircleId = balancingCircles[0].Id },
                    new Member { Name = "Member 2", Type = "Consumer", Category = "Retailer", IsProducer = false, BalancingCircleId = balancingCircles[1].Id },
                };

                // Add the new members to the database context.
                await context.Members.AddRangeAsync(members);
                await context.SaveChangesAsync(); // Persist changes to the database.
                logger.LogInformation("Members seeded.");

                // Create a list of ForecastData instances to seed.
                var forecasts = new List<ForecastData>
                {
                    new ForecastData { MemberId = members[0].Id, Date = DateTime.UtcNow, Forecast = 100 },
                    new ForecastData { MemberId = members[1].Id, Date = DateTime.UtcNow.AddDays(1), Forecast = 200 },
                };

                // Add the new forecast data to the database context.
                await context.Forecasts.AddRangeAsync(forecasts);
                await context.SaveChangesAsync(); // Persist changes to the database.
                logger.LogInformation("Forecast data seeded.");

                // Create a list of TimeseriesEntry instances to seed.
                var timeseriesEntries = new List<TimeseriesEntry>
                {
                    new TimeseriesEntry { MemberId = members[0].Id, Date = DateTime.UtcNow, Value = 50 },
                    new TimeseriesEntry { MemberId = members[1].Id, Date = DateTime.UtcNow.AddDays(1), Value = 150 },
                };

                // Add the new timeseries entries to the database context.
                await context.TimeseriesEntries.AddRangeAsync(timeseriesEntries);
                await context.SaveChangesAsync(); // Persist changes to the database.
                logger.LogInformation("Timeseries entries seeded.");
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the seeding process.
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw; // Rethrow the exception for further handling if necessary.
            }
        }
    }
}
