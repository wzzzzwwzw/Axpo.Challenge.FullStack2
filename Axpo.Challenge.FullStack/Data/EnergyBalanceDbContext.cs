using Microsoft.EntityFrameworkCore;
using Axpo.Challenge.FullStack.Models.Domain;

namespace Axpo.Challenge.FullStack.Data
{
    /// <summary>
    /// Represents the database context for energy balance operations.
    /// </summary>
    public class EnergyBalanceDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnergyBalanceDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public EnergyBalanceDbContext(DbContextOptions<EnergyBalanceDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for balancing circles.
        /// </summary>
        public DbSet<BalancingCircle> BalancingCircles { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for forecast data.
        /// </summary>
        public DbSet<ForecastData> Forecasts { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for members.
        /// </summary>
        public DbSet<Member> Members { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for timeseries entries.
        /// </summary>
        public DbSet<TimeseriesEntry> TimeseriesEntries { get; set; }

        /// <summary>
        /// Configures the entity relationships and other configurations.
        /// </summary>
        /// <param name="modelBuilder">The model builder to be used for configuration.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entity relationships and other configurations here
        }
    }
}