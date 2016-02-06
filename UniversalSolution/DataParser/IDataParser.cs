using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DataParser
{
    public interface IDataParser
    {
        List<Match> ParseData(JArray input);
    }
}
