using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulasCollection.Models
{
    public static class CoefsWhichMustBeRevert
    {
        private static Dictionary<string, string> coefs;

        public static Dictionary<string, string> revertCoefs => coefs;

        static CoefsWhichMustBeRevert()
        {
            FillCoefsToRevert();
        }

        private static void FillCoefsToRevert()
        {
            coefs = new Dictionary <string,string>
                {
                { "12", "X"},
                { "1X", "1"},
                { "X2", "2"},
                { "1", "1X"},
                { "2", "X2"},
                { "X", "12"}
            };
        }
    }
}
