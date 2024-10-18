using System;

namespace Axpo.Challenge.FullStack.Models.Domain
{
    /// <summary>
    /// Represents the forecast data for a member in a balancing circle.
    /// </summary>
    public class ForecastData
    {
        /// <summary>
        /// Gets or sets the unique identifier for the forecast data entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the member associated with this forecast data.
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Gets or sets the member associated with this forecast data.
        /// </summary>
        public Member Member { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date for which the forecast data is recorded.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the forecast value for the member on the specified date.
        /// </summary>
        public double Forecast { get; set; }
    }
}
