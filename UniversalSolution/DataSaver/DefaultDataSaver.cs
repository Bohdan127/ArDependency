namespace DataSaver
{
    public class DefaultDataSaver : IDataSaver
    {
        string IDataSaver.Test
        {
            get
            {
                return "DefaultDataSaver";
            }
        }
    }
}
