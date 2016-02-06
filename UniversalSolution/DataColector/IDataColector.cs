using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataColector
{
    public interface IDataColector
    {
        JArray GetJsonArray(string url);
    }
}
