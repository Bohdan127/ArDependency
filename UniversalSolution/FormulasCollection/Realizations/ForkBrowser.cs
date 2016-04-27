using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulasCollection.Realizations
{
    /// <summary>
    /// Main class, where do all operation for search fork
    /// </summary>
    public static class ForkBrowser
    {
        static ForkCalculate forkcalculate;
        static ForkCalculate forkCalculate
        {
            get
            {
                if (forkcalculate == null)
                {
                    forkcalculate = new ForkCalculate();
                }
                return forkcalculate;
            }
        }

        public static List<Fork> GetFootballWork(Site[] site, bool update = false)
        {
            List<Fork> forkFootbal = new List<Fork>();
            if (site.Contains(Site.MARATHONBET))
            {
                forkFootbal.AddRange(forkCalculate.GetFootballWorkM(update));
            }
            if (site.Contains(Site.PINNACLE))
            {
                forkFootbal.AddRange(forkCalculate.GetFootballWorkP(update));
            }
            if (site.Contains(Site.MARATHONBETANDPINNACLE))
            {
                forkFootbal.AddRange(forkCalculate.GetFootballWorkPM(update));
            }
            return forkFootbal;
        }
        public static List<Fork> GetVolleybalWork(Site[] site, bool update = false)
        {
            

            List<Fork> forkFootbal = new List<Fork>();
            if (site.Contains(Site.MARATHONBET))
            {
                forkFootbal.AddRange(forkCalculate.GetVolleybalWorkM(update));
            }
            if (site.Contains(Site.PINNACLE))
            {
                forkFootbal.AddRange(forkCalculate.GetVolleybalWorkP(update));
            }
            if (site.Contains(Site.MARATHONBETANDPINNACLE))
            {
                forkFootbal.AddRange(forkCalculate.GetVolleybalWorkPM(update));
            }
            return forkFootbal;
        }
        public static List<Fork> GetTennisWork(Site[] site, bool update = false)
        {
            

            List<Fork> forkFootbal = new List<Fork>();
            if (site.Contains(Site.MARATHONBET))
            {
                forkFootbal.AddRange(forkCalculate.GetTennisWorkM(update));
            }
            if (site.Contains(Site.PINNACLE))
            {
                forkFootbal.AddRange(forkCalculate.GetTennisWorkP(update));
            }
            if (site.Contains(Site.MARATHONBETANDPINNACLE))
            {
                forkFootbal.AddRange(forkCalculate.GetTennisWorkPM(update));
            }
            return forkFootbal;
        }
        public static List<Fork> GetHockeyWork(Site[] site, bool update = false)
        {
            

            List<Fork> forkFootbal = new List<Fork>();
            if (site.Contains(Site.MARATHONBET))
            {
                forkFootbal.AddRange(forkCalculate.GetHockeyWorkM(update));
            }
            if (site.Contains(Site.PINNACLE))
            {
                forkFootbal.AddRange(forkCalculate.GetHockeyWorkP(update));
            }
            if (site.Contains(Site.MARATHONBETANDPINNACLE))
            {
                forkFootbal.AddRange(forkCalculate.GetHockeyWorkPM(update));
            }
            return forkFootbal;
        }
        public static List<Fork> GetBasketballWork(Site[] site, bool update = false)
        {
            

            List<Fork> forkFootbal = new List<Fork>();
            if (site.Contains(Site.MARATHONBET))
            {
                forkFootbal.AddRange(forkCalculate.GetBasketballWorkM(update));
            }
            if (site.Contains(Site.PINNACLE))
            {
                forkFootbal.AddRange(forkCalculate.GetBasketballWorkP(update));
            }
            if (site.Contains(Site.MARATHONBETANDPINNACLE))
            {
                forkFootbal.AddRange(forkCalculate.GetBasketballWorkPM(update));
            }
            return forkFootbal;
        }
    }

    public  enum Site
    {
        MARATHONBET = 1,
        PINNACLE = 2,
        MARATHONBETANDPINNACLE = 3,
    }
}
