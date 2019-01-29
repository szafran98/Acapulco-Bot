using System;

namespace Acapulco_Bot.Utils
{
    class Mucka
    {
        public static string GenerateMucka()
        {
            return new Random().NextDouble().ToString("R");
        }
    }
}
