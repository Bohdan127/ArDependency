namespace FormulasCollection.Models
{
    public class ResultForForks
    {
        public string Event { get; set; }
        public string Type { get; set; }
        public string Coef { get; set; }
        public string EventID { get; set; }
        public string Liga { get; set; }
        public string Bookmaker { get; set; }

        //  X1 X2 1 2
        public ResultForForks() { }

        public ResultForForks(string nameTeam1, string nameTeam2, string date, string nameCoff, string coef, string type, string bookmaker)
        {
            this.Event = nameTeam1.Trim() + " - " + nameTeam2.Trim();
            this.MatchDateTime = date;
            this.Type = nameCoff;
            this.Coef = coef;
            SportType = type;
            Bookmaker = bookmaker;
        }
        public ResultForForks(string eventID, string nameTeam1, string nameTeam2, string date, string nameCoff, string coef, string type, string bookmaker,string liga)
        {
            this.Event = nameTeam1.Trim() + " - " + nameTeam2.Trim();
            this.MatchDateTime = date;
            this.Type = nameCoff;
            this.Coef = coef;
            SportType = type;
            Bookmaker = bookmaker;
            this.EventID = eventID;
            this.Liga = liga;
        }

        public string SportType { get; set; }
        public string MatchDateTime { get; set; }
    }
}