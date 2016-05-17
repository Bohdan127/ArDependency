using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulasCollection.Realizations
{
  public class ResultForForksDictionary
    {
      public string TeamNames { get; set; }

      public DateTime MatchDateTime { get; set; }

      public Dictionary<string,double> TypeCoefDictionary { get; set; }
    }
}
