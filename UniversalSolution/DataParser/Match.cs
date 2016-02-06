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
        //П1
        public double P1 { get; set; }
        //Х
        public double X { get; set; }
        //П2
        public double P2 { get; set; }
        //1Х
        public double X1 { get; set; }
        //12
        public double I2 { get; set; }
        //Х2
        public double X2 { get; set; }
        //Фора 1
        public double Fora1 { get; set; }
        //1
        public double I { get; set; }
        //Фора 2
        public double Fora2 { get; set; }
        //2
        public double II { get; set; }
        //Тотал
        public double Total { get; set; }
        //Б
        public double B { get; set; }
        //М
        public double M { get; set; }
    }
}
