using System;
using System.Collections.Generic;

namespace Axpo.Challenge.FullStack.Models.Domain
{
    /// <summary>
    /// Represents a member in the balancing circle.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Gets or sets the unique identifier for the member.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the member.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the member, e.g., "Producer" or "Consumer".
        /// </summary>
        public string? Type { get; set; } // "Producer" or "Consumer"

        /// <summary>
        /// Gets or sets the category of the member, e.g., "Solar", "Wind", "Retailer", "Industrials".
        /// </summary>
        public string? Category { get; set; } // "Solar", "Wind", "Retailer", "Industrials"

        /// <summary>
        /// Gets or sets the collection of forecast data associated with the member.
        /// </summary>
        public ICollection<ForecastData> Forecasts { get; set; }

          /// <summary>
        /// Gets or sets a value indicating whether the member is a producer.
        /// </summary>
        public bool IsProducer { get; set; } // Add this property
    }
}