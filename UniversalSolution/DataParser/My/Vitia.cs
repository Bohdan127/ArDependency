using System;
using System.Collections.Generic;

namespace DataParser.My
{
    class Vitia
    {
    }

    class wylka
    {
        public string type1;
        public double kof1;
        public string type2;
        public double kof2;
    }
    class Wylky
    {
        public Dictionary<string, Dictionary<string, double>> PodijaKofyMarafon;
        public Dictionary<string, Dictionary<string, double>> PodijaKofyPinacl;
        public Dictionary<string, wylka> WylkaPodijaKofy;

        Dictionary<string, double> marafon;
        Dictionary<string, double> pinacl;


        bool checkIsWylka(double kof1, double kof2)
        {
            return 1 > (1 / kof1 + 1 / kof2);
        }
        double getStawka(double stawka1, Double kof1, double kof2)
        {
            return (kof1 * stawka1) / kof2;
        }
        wylka GetNewWylka(string t1, string t2)
        {
            wylka nwylka = new wylka
            {
                type1 = t1,
                kof1 = marafon[t1],
                type2 = t2,
                kof2 = pinacl[t2]
            };
            return nwylka;
        }
        Dictionary<string, wylka> GetWylky()
        {
            Dictionary<string, wylka> WylkaPodijaKofy = new Dictionary<string, wylka>();
            foreach (KeyValuePair<string, Dictionary<string, double>> tmp in PodijaKofyMarafon)
            {
                if (PodijaKofyPinacl.ContainsKey(tmp.Key))
                {
                    marafon = tmp.Value;
                    pinacl = PodijaKofyPinacl[tmp.Key];

                    if (marafon.ContainsKey("1") && pinacl.ContainsKey("X2") &&
                                                checkIsWylka(marafon["1"], pinacl["X2"]))
                    {
                        WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("1", "X2"));
                    }
                    if (marafon.ContainsKey("X") && pinacl.ContainsKey("12") &&
                                                checkIsWylka(marafon["X"], pinacl["12"]))
                    {
                        WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("X", "12"));
                    }
                    if (marafon.ContainsKey("2") && pinacl.ContainsKey("1X") &&
                                                checkIsWylka(marafon["2"], pinacl["1X"]))
                    {
                        WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("2", "1X"));
                    }
                    if (marafon.ContainsKey("1X") && pinacl.ContainsKey("2") &&
                                                checkIsWylka(marafon["1X"], pinacl["2"]))
                    {
                        WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("1X", "2"));
                    }
                    if (marafon.ContainsKey("12") && pinacl.ContainsKey("X") &&
                                                checkIsWylka(marafon["12"], pinacl["X"]))
                    {
                        WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("12", "X"));
                    }
                    if (marafon.ContainsKey("12") && pinacl.ContainsKey("X") &&
                                                checkIsWylka(marafon["12"], pinacl["X"]))
                    {
                        WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("12", "X"));
                    }
                    /*
                    if (marafon.ContainsKey("ЗЛИВНЗД") && pinacl.ContainsKey("ЗЛИВНЗН") &&
                                                checkIsWylka(marafon["ЗЛИВНЗД"], pinacl["ЗЛИВНЗН"]))
                    {
                        WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("ЗЛИВНЗД", "ЗЛИВНЗН"));
                    }
                    if (marafon.ContainsKey("ЗЛИВНЗН") && pinacl.ContainsKey("ЗЛИВНЗД") &&
                                               checkIsWylka(marafon["ЗЛИВНЗН"], pinacl["ЗЛИВНЗД"]))
                    {
                        WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("ЗЛИВНЗН", "ЗЛИВНЗД"));
                    }
                    if (marafon.ContainsKey("РНД") && pinacl.ContainsKey("РНН") &&
                                               checkIsWylka(marafon["РНД"], pinacl["РНН"]))
                    {
                        WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("РНД", "РНН"));
                    }
                    if (marafon.ContainsKey("РНН") && pinacl.ContainsKey("РНД") &&
                                               checkIsWylka(marafon["РНН"], pinacl["РНД"]))
                    {
                        WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("РНН", "РНД"));
                    }
                    if (marafon.ContainsKey("ПСС1Н") && pinacl.ContainsKey("ПСС1Д") &&
                                               checkIsWylka(marafon["ПСС1Н"], pinacl["ПСС1Д"]))
                    {
                        WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("ПСС1Н", "ПСС1Д"));
                    }
                    if (marafon.ContainsKey("ПСС2Н") && pinacl.ContainsKey("ПСС2Д") &&
                                               checkIsWylka(marafon["ПСС2Н"], pinacl["ПСС2Д"]))
                    {
                        WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("ПСС2Н", "ПСС2Д"));
                    }
                    */
                    /* if (marafon.ContainsKey("ЧНКГД") && pinacl.ContainsKey("ЧНКГН") &&
                                                checkIsWylka(marafon["ЧНКГД"], pinacl["ЧНКГН"]))
                     {
                         WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("ЧНКГД", "ЧНКГН"));
                     }
                     if (marafon.ContainsKey("ЧНКГН") && pinacl.ContainsKey("ЧНКГД") &&
                                                checkIsWylka(marafon["ЧНКГН"], pinacl["ЧНКГД"]))
                     {
                         WylkaPodijaKofy.Add(tmp.Key, GetNewWylka("ЧНКГН", "ЧНКГД"));
                     }*/

                }
                marafon = null;
                pinacl = null;
            }
            return WylkaPodijaKofy;
        }
    }

}
