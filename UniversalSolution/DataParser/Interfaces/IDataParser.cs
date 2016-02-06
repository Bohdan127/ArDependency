using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DataParser.Interfaces
{
    public interface IDataParser
    {
        List<IDataMatch> ParseData(JArray input);
    }
}