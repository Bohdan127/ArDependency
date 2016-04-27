using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulasCollection.Realizations
{
    class ForkCalculate
    {

        DataFromSite data;

        public bool CheckIsFork(double coef1, double coef2) => 1 > (1 / coef1 + 1 / coef2);


        public List<Fork> GetFootballWorkM(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> marathon = data.GetFootball(update);

            return forkFootbal;
        }
        public List<Fork> GetFootballWorkP(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> pinnacle = data.GetFootball(update);

            return forkFootbal;
        }

        public List<Fork> GetFootballWorkPM(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> marathon = data.GetFootball(update);
            Dictionary<string, Dictionary<string, Double>> pinnacle = data.GetFootball(update);

            return forkFootbal;
        }
        public List<Fork> GetBasketballWorkM(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> marathon = data.GetBasketball(update);

            return forkFootbal;
        }
        public List<Fork> GetBasketballWorkP(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> pinnacle = data.GetBasketball(update);

            return forkFootbal;
        }

        public List<Fork> GetBasketballWorkPM(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> marathon = data.GetBasketball(update);
            Dictionary<string, Dictionary<string, Double>> pinnacle = data.GetBasketball(update);

            return forkFootbal;
        }

        public List<Fork> GetTennisWorkM(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> marathon = data.GetTennis(update);

            return forkFootbal;
        }
        public List<Fork> GetTennisWorkP(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> pinnacle = data.GetTennis(update);

            return forkFootbal;
        }

        public List<Fork> GetTennisWorkPM(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> marathon = data.GetTennis(update);
            Dictionary<string, Dictionary<string, Double>> pinnacle = data.GetTennis(update);

            return forkFootbal;
        }

        public List<Fork> GetHockeyWorkM(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> marathon = data.GetHockey(update);

            return forkFootbal;
        }
        public List<Fork> GetHockeyWorkP(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> pinnacle = data.GetHockey(update);

            return forkFootbal;
        }

        public List<Fork> GetHockeyWorkPM(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> marathon = data.GetHockey(update);
            Dictionary<string, Dictionary<string, Double>> pinnacle = data.GetHockey(update);

            return forkFootbal;
        }

        public List<Fork> GetVolleyballWorkM(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> marathon = data.GetVolleyball(update);

            return forkFootbal;
        }
        public List<Fork> GetVolleyballWorkP(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> pinnacle = data.GetVolleyball(update);

            return forkFootbal;
        }

        public List<Fork> GetVolleyballWorkPM(bool update)
        {
            List<Fork> forkFootbal = new List<Fork>();
            Dictionary<string, Dictionary<string, Double>> marathon = data.GetVolleyball(update);
            Dictionary<string, Dictionary<string, Double>> pinnacle = data.GetVolleyball(update);

            return forkFootbal;
        }
    }
}
