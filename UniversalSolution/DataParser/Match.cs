using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataParser
{
    public class Match
    {
        public int Id { get; set; }
        public string Sportname { get; set; }
        public string Champ { get; set; }
        public string Opp1Name { get; set; }
        public string Opp2Name { get; set; }
        public Event Event = new Event();
    }
    //todo не в захваті від аткого рішення хз
    public class Event
    {
        public double P1 { get; set; }
        public double X { get; set; }
        public double P2 { get; set; }
        public double X1 { get; set; }
        public double I2 { get; set; }
        public double X2 { get; set; }
        public double Fora1 { get; set; }
        public double I { get; set; }
        public double Fora2 { get; set; }
        public double II { get; set; }
        public double Total { get; set; }
        public double B { get; set; }
        public double M { get; set; }
    }
}
