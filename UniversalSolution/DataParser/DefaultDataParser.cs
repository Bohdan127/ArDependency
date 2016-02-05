namespace DataParser
{
    public class DefaultDataParser : IDataParser
    {
        string IDataParser.Test
        {
            get
            {
                return "DefaultDataParser";
            }
        }
    }
}
