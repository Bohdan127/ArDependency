namespace FormulasCollection.Realizations
{
    /// <summary>
    /// Class contains fork details
    /// </summary>
    public class Fork
    {
        /// <summary>
        /// Sport Type
        /// </summary>
        public string Sport { get; set; }

        /// <summary>
        /// Default place for Team Names, time of match and other details
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// Site 1
        /// </summary>
        public string SiteFirst { get; set; }

        /// <summary>
        /// Details for first type of Fork 
        /// </summary>
        public string TypeFirst { get; set; }

        /// <summary>
        /// Value for first coef of Fork
        /// </summary>
        public string CoefFirst { get; set; }

        /// <summary>
        /// Site 2
        /// </summary>
        public string SecondFirst { get; set; }
        /// <summary>
        /// Details for second type of Fork 
        /// </summary>
        public string TypeSecond { get; set; }

        /// <summary>
        /// Value for second coef of Fork
        /// </summary>
        public string CoefSecond { get; set; }

        public string MatchDateTime { get; set; }

        public string BookmakerFirst { get; set; }
        public string BookmakerSecond { get; set; }
    }
}
