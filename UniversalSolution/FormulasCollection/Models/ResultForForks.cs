namespace FormulasCollection.Models
{
    public class ResultForForks
    {
        public string Event { get; set; }
        public string Type { get; set; }
        public string Coef { get; set; }
        public string EventId { get; set; }
        public string League { get; set; }
        public string Bookmaker { get; set; }

        //  X1 X2 1 2
        public ResultForForks() { }

        public ResultForForks(string eventID, string nameTeam1, string nameTeam2, string date, string nameCoff, string coef, string type, string bookmaker, string league)
        {
            this.Event = nameTeam1.Trim() + " - " + nameTeam2.Trim();
            this.MatchDateTime = date;
            this.Type = nameCoff;
            this.Coef = coef;
            SportType = type;
            Bookmaker = bookmaker;
            this.EventId = eventID;
            this.League = league;
        }

        public string SportType { get; set; }
        public string MatchDateTime { get; set; }
    }
}