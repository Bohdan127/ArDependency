namespace FormulasCollection.Realizations
{
    /// <summary>
    /// Class contains fork details
    /// </summary>
    public class Fork
    {
        /// <summary>
        /// Default place for Team Names, time of match and other details
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// Details for first type of Fork 
        /// </summary>
        public string TypeFirst { get; set; }

        /// <summary>
        /// Value for first coef of Fork
        /// </summary>
        public double CoefFirst { get; set; }

        /// <summary>
        /// Details for second type of Fork 
        /// </summary>
        public string TypeSecond { get; set; }

        /// <summary>
        /// Value for second coef of Fork
        /// </summary>
        public double CoefSecond { get; set; }
    }
}
