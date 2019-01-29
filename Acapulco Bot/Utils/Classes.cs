using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acapulco_Bot.Utils
{
    class Classes
    {
        public static string GetClass(string id)
        {
            switch(id)
            {
                case "w": return "Wojownik";
                case "p": return "Palladyn";
                case "h": return "Łowca";
                case "m": return "Mag";
                case "t": return "Tracer";
                case "b": return "Tancerz Ostrzy";
                default: return "Unknown";
            }
        }
    }
}
