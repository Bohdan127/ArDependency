namespace DataColector
{
    public class DefaultDataColector : IDataColector
    {
        string IDataColector.Test
        {
            get
            {
                return "DefaultDataColector";
            }
        }
    }
}
