using Newtonsoft.Json.Linq;

namespace DataColector.Interfaces
{
    public interface IDataColector
    {
        JArray GetJsonArray(string url);
        string t { get; }
    }
}
