using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Acapulco_Bot.Game.Player
{
    class Player
    {
        private Statistics _statistics = new Statistics();
        private Fight _fight = new Fight();
        private Maps _maps = new Maps();
        private Loot _loot = new Loot();
        private Heal _heal = new Heal();
        private Shop _shop = new Shop();

        public bool InitializeStatistics(string data)
        {
            _statistics = JsonConvert.DeserializeObject<Statistics>(data);
            return true;
        }

        public void InitializeMaps(string data)
        {
            _maps = JsonConvert.DeserializeObject<Maps>(data);
        }

        public Statistics GetStatistics()
        {
            return _statistics;
        }

        public Fight GetFight()
        {
            return _fight;
        }

        public Maps GetMaps()
        {
            return _maps;
        }

        public Loot GetLoot()
        {
            return _loot;
        }

        public Heal GetHeal()
        {
            return _heal;
        }

        public Shop GetShop()
        {
            return _shop;
        }
    }
}