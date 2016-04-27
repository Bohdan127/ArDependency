using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulasCollection.Realizations
{
    /// <summary>
    /// Class with all data from one site 
    /// </summary>
    class DataFromSite
    {
        public string nameSite;

        Dictionary<string, Dictionary<string, Double>> football;
        Dictionary<string, Dictionary<string, Double>> volleyball;
        Dictionary<string, Dictionary<string, Double>> hockey;
        Dictionary<string, Dictionary<string, Double>> tennis;
        Dictionary<string, Dictionary<string, Double>> basketball;

        public Dictionary<string, Dictionary<string, Double>> GetFootball(bool update = false)
        {
            if (update || football == null)
            {
                // Парсер обновлює дані
                return football;
            }
            return football;
        }
        public Dictionary<string, Dictionary<string, Double>> GetVolleyball(bool update = false)
        {
            if (update || volleyball == null)
            {
                // Парсер обновлює дані
                return volleyball;
            }
            return volleyball;
        }
        public Dictionary<string, Dictionary<string, Double>> GetHockey(bool update = false)
        {
            if (update || hockey == null)
            {
                // Парсер обновлює дані
                return hockey;
            }
            return hockey;
        }
        public Dictionary<string, Dictionary<string, Double>> GetTennis(bool update = false)
        {
            if (update || tennis == null)
            {
                // Парсер обновлює дані
                return tennis;
            }
            return tennis;
        }
        public Dictionary<string, Dictionary<string, Double>> GetBasketball(bool update = false)
        {
            if (update || basketball == null)
            {
                // Парсер обновлює дані
                return basketball;
            }
            return basketball;
        }
    }
}
