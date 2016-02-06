namespace DataParser.Interfaces
{
    public interface IDataMatch
    {
        int Id { get; set; }
        string Sportname { get; set; }
        string Champ { get; set; }
        string Opp1Name { get; set; }
        string Opp2Name { get; set; }

        double P1 { get; set; }
        double X { get; set; }
        double P2 { get; set; }
        double X1 { get; set; }
        double I2 { get; set; }
        double X2 { get; set; }
        double Fora1 { get; set; }
        double I { get; set; }
        double Fora2 { get; set; }
        double II { get; set; }
        double Total { get; set; }
        double B { get; set; }
        double M { get; set; }

        IDataMatch GetInstance();
    }
}