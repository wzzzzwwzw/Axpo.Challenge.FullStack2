using System.Collections.Generic;

namespace Axpo.Challenge.FullStack.Models.Domain
{
    public class Member
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; } // "Producer" or "Consumer"
        public string? Category { get; set; } // "Solar", "Wind", "Retailer", "Industrials"
        public ICollection<ForecastData> Forecasts { get; set; } = new List<ForecastData>();
        public bool IsProducer { get; set; }
        public int BalancingCircleId { get; set; }
        public BalancingCircle BalancingCircle { get; set; } = null!;
    }
}