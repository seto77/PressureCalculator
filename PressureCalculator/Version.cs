using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureCalculator
{
    class Version
    {

        static public string Software =
            "Pressure Calculator"
            ;
        static public string VersionAndDate { get => History.Remove(0, 10).Remove(20); }

        static public string History =
           "History" +
           "\r\n ver1.003(2021/09/01) Fixed a minor bug in Celsius degree mode. The lower limit of temperature was set." +
           "\r\n ver1.002(2021/07/03) Changed a framework to .Net 5.0; Added two diamond Raman equations (Fratanduono et al., 2021)." +
           "\r\n ver1.001(2021/05/15) Distribution site is changed to GitHub." +
           "\r\n ver0.000(???/??/??)  "
           ;
    }
}
