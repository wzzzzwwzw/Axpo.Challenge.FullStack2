namespace Axpo.Challenge.FullStack.Models.Domain
{
    /// <summary>
    /// Represents a balancing circle, which groups multiple members together.
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
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of members that belong to the balancing circle.
        /// A balancing circle can contain multiple members.
        /// </summary>
        public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}
