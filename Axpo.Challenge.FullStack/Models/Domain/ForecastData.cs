using System;

namespace Axpo.Challenge.FullStack.Models.Domain
{
    public class ForecastData
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public DateTime Date { get; set; }
        public double Forecast { get; set; }
    }
}