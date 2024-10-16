using System;
using System.Collections.Generic;

namespace Axpo.Challenge.FullStack.Models.Domain
{
    /// <summary>
    /// Represents a balancing circle.
    /// </summary>
    public class BalancingCircle
    {
        /// <summary>
        /// Gets or sets the unique identifier for the balancing circle.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the balancing circle.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the members of the balancing circle.
        /// </summary>
        public ICollection<Member>? Members { get; set; }
    }
}