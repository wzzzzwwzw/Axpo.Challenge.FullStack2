using System;

namespace Axpo.Challenge.FullStack.Models.Domain
{
    /// <summary>
    /// Represents an entry in a timeseries.
    /// </summary>
    public class TimeseriesEntry
    {
        /// <summary>
        /// Gets or sets the unique identifier for the timeseries entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the timeseries entry.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the value of the timeseries entry.
        /// </summary>
        public double Value { get; set; }
    }
}