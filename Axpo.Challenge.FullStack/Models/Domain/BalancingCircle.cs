using System.Collections.Generic;

namespace Axpo.Challenge.FullStack.Models.Domain
{
    public class BalancingCircle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}