using System;

namespace Axpo.Challenge.FullStack.Models.Domain
{
    /// <summary>
    /// Represents an entry in the time series for a member's energy production or consumption data.
    /// </summary>
    public class TimeseriesEntry
    {
        /// <summary>
        /// Gets or sets the unique identifier for the time series entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the member associated with this time series entry.
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Member"/> entity associated with this time series entry.
        /// </summary>
        public Member Member { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date and time of the time series entry.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the energy value (e.g., production or consumption) associated with this time series entry.
        /// </summary>
        public double Value { get; set; }
    }
}
