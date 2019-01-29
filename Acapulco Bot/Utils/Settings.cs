using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acapulco_Bot.Utils
{
    class Settings
    {
        public static bool AutoHeal = false;
        public static bool AutoHealOnDie = false;
        public static int AutoHealHP = 90;

        public static bool RandomAttack = false;
        public static int RandomAttackDelay = 3000;

        public static bool MinimalPrice = false;
        public static int MinimalPriceG = 100;
        public static bool AutoSell = false;
    }
}
