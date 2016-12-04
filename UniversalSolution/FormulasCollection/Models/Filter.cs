using System;

namespace FormulasCollection.Models
{
    public class Filter
    {
        public string Id { get; set; }

        public decimal? Min { get; set; }

        public decimal? Max { get; set; }

        public bool Football { get; set; }

        public bool Basketball { get; set; }

        public bool Volleyball { get; set; }

        public bool Hockey { get; set; }

        public bool Tennis { get; set; }

        public DateTime? FaterThen { get; set; }

        public DateTime? LongerThen { get; set; }

        public string LicenseKey { get; set; }

        public int? AutoUpdateTime { get; set; }

        public int? RecommendedRate1 { get; set; }

        public int? RecommendedRate2 { get; set; }

        public decimal? MinPinBet { get; set; }

        public decimal? MinMarBet { get; set; }

    }
}