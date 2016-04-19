using System.Collections.Generic;

namespace DataParser.Forks
{
    class ForkSearcher
    {
        struct EventWithKof
        {
            public string Type1 { get; set; }
            public double kof1 { get; set; }
            public string Type2 { get; set; }
            public double kof2 { get; set; }
        }
        public class Wylky
        {
            /// <summary>
            /// </summary>
            void GetEventy()
            {

            }

            /// <summary>
            /// Here sghould be already parsed data events after Parser. Where Id(can be string) to full event
            /// </summary>
            Dictionary<int, EventWithKof> eventy;

            /// <summary>
            ///  Check is any fork there are
            /// </summary>
            /// <param name="arg"></param>
            /// <returns>"<1" - fork is here</returns>    
            double CheckEvent(EventWithKof arg)
            {
                double result = 1 / arg.kof1 + 1 / arg.kof2;
                return result;
            }

            /// <summary>
            ///  Search what event should place for create fork to another event 
            /// </summary>
            /// <param name="argM"></param>
            /// <param name="kofM"></param>
            /// <param name="kof"></param>
            /// <returns></returns>
            double shearchMoney(double argM, double kofM, double kof)
            {
                double result = (kofM * argM) / kof;
                return result;
            }
        }
    }
}
