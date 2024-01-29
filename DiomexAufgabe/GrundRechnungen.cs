using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiomexAufgabe
{
    public static class GrundRechnungen
    {
        public static double Addition(double one, double two)
        {
            return (one + two);
        }
        public static string AdditionString(string one, string two)
        {
            return ("\""+one.Replace("\"","") + two.Replace("\"","")+"\"");
        }
        public static double Subtraktion(double one, double two)
        {
            return (one - two);
        }
        public static double Multiplikation(double one, double two)
        {
            return (one * two);
        }
        public static string MultiplikationString(string one, double two)
        {
            string result = "";
            for (int i = 0; i < two; i++)
            {
                result += one;
            }
            return "\""+result.Replace("\"","")+ "\"";
        }
        public static double Division(double one, double two)
        {
            return (one / two);
        }
        
    }
}
