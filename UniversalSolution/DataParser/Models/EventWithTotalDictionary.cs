namespace DataParser.Models
{
    public class EventWithTotalDictionary
    {
        public string TotalType { get; set; }

        public string TotalValue { get; set; }

        public string MatchDateTime { get; set; }

        public string LineId { get; set; }

        public long? LeagueId { get; set; }
    }
}