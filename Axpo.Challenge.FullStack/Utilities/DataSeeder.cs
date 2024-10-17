using Axpo.Challenge.FullStack.Data;
using Axpo.Challenge.FullStack.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace Axpo.Challenge.FullStack.SeedData
{
    /// <summary>
    /// Provides methods to seed the database with initial data.
    /// </summary>
    public static class DataSeeder
    {
        /// <summary>
        /// Seeds the database with initial data if it is empty.
        /// </summary>
        /// <param name="context">The database context to be seeded.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
       public static async Task SeedDataAsync(EnergyBalanceDbContext context, ILoggerFactory loggerFactory)
{
    if (await context.BalancingCircles.AnyAsync())
    {
        return; // DB has been seeded
    }

    var logger = loggerFactory.CreateLogger("DataSeeder");

    try
    {
        // Seed Balancing Circles first
        var balancingCircles = new List<BalancingCircle>
        {
            new BalancingCircle { Name = "Circle 1" },
            new BalancingCircle { Name = "Circle 2" },
            // Add more mock data as needed
        };

        await context.BalancingCircles.AddRangeAsync(balancingCircles);
        await context.SaveChangesAsync(); // Save to get IDs assigned

        // Now that the circles have been saved, create members
        var members = new List<Member>
        {
            new Member { Name = "Member 1", Type = "Producer", Category = "Solar", IsProducer = true, BalancingCircleId = balancingCircles[0].Id },
            new Member { Name = "Member 2", Type = "Consumer", Category = "Retailer", IsProducer = false, BalancingCircleId = balancingCircles[1].Id },
            // Add more mock data as needed
        };

        await context.Members.AddRangeAsync(members);
        await context.SaveChangesAsync(); // Save members

        // Seed forecasts with unique dates
        var forecasts = new List<ForecastData>
        {
            new ForecastData { MemberId = members[0].Id, Date = DateTime.UtcNow, Forecast = 100 },
            new ForecastData { MemberId = members[1].Id, Date = DateTime.UtcNow.AddDays(1), Forecast = 200 },
            // Add more mock data as needed
        };

        await context.Forecasts.AddRangeAsync(forecasts);
        
        // Seed timeseries entries with unique dates
        var timeseriesEntries = new List<TimeseriesEntry>
        {
            new TimeseriesEntry { MemberId = members[0].Id, Date = DateTime.UtcNow, Value = 50 },
            new TimeseriesEntry { MemberId = members[1].Id, Date = DateTime.UtcNow.AddDays(1), Value = 150 },
            // Add more mock data as needed
        };

        await context.TimeseriesEntries.AddRangeAsync(timeseriesEntries);

        await context.SaveChangesAsync(); // Save forecasts and timeseries
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding the database.");
        throw;
    }
}
    }
}