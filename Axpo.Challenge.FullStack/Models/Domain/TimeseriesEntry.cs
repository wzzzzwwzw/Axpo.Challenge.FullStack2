using System;

namespace Axpo.Challenge.FullStack.Models.Domain
{
    public class TimeseriesEntry
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}