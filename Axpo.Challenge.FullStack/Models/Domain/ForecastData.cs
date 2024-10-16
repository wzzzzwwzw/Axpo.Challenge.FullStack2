using System;

namespace Axpo.Challenge.FullStack.Models.Domain
{
    /// <summary>
    /// Represents forecast data for a member.
    /// </summary>
    public class ForecastData
    {
        /// <summary>
        /// Gets or sets the unique identifier for the forecast data.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the member ID associated with the forecast data.
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Gets or sets the member associated with the forecast data.
        /// </summary>
        public Member? Member { get; set; } // Declare as nullable

        /// <summary>
        /// Gets or sets the date of the forecast.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the forecast value.
        /// </summary>
        public double Forecast { get; set; }
    }
}