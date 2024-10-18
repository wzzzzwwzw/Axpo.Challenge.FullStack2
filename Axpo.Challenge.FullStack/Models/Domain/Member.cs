namespace Axpo.Challenge.FullStack.Models.Domain
{
    public class Member
    {
        /// <summary>
        /// Gets or sets the unique identifier for the member.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the member.
        /// This is optional and can be null.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the member, either "Producer" or "Consumer".
        /// </summary>
        public string? Type { get; set; } // Example: "Producer", "Consumer"

        /// <summary>
        /// Gets or sets the category the member belongs to.
        /// This describes the type of energy or business, such as "Solar", "Wind", "Retailer", or "Industrials".
        /// </summary>
        public string? Category { get; set; } // Example: "Solar", "Wind", "Retailer", etc.

        /// <summary>
        /// Gets or sets the collection of forecast data related to this member.
        /// A member can have multiple forecast entries.
        /// </summary>
        public ICollection<ForecastData> Forecasts { get; set; } = new List<ForecastData>();

        /// <summary>
        /// Indicates whether the member is a producer of energy.
        /// If false, the member is considered a consumer.
        /// </summary>
        public bool IsProducer { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the balancing circle this member belongs to.
        /// </summary>
        public int BalancingCircleId { get; set; }

        /// <summary>
        /// Gets or sets the balancing circle associated with the member.
        /// This represents the relationship to the <see cref="BalancingCircle"/> entity.
        /// </summary>
        public BalancingCircle BalancingCircle { get; set; } = null!;
    }
}
