using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Acapulco_Bot.Bot;

namespace Acapulco_Bot.Game.Player
{
    class Loot
    {
        public async Task Take(string lootId)
        {
            using (HttpClientHandler handler = new HttpClientHandler
            {
                CookieContainer = AcapulcoBot.GetInstance.GetCookies()
            })
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Dalvik/2.1.0 (Linux; U; Android 8.0.0; Samsung Galaxy S8 - 8.0 - API 26 - 1440x2960 Build/OPR6.170623.017)");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("X-Unity-Version", "5.6.2p4");

                    Uri url = new Uri($"http://{AcapulcoBot.GetInstance.GetEngine().GetServer()}.margonem.pl/engine?t=loot&not=&want={lootId}&must=&final=1&mobile=1&ev={AcapulcoBot.GetInstance.GetEngine().GetEvent()}&mobile_token={AcapulcoBot.GetInstance.GetEngine().GetToken()}");
                    AcapulcoBot.GetInstance.GetCookies().Add(url, new System.Net.Cookie("mchar_id", AcapulcoBot.GetInstance.GetEngine().GetCharacterID()));

                    string response = await client.GetStringAsync(url);

                    AcapulcoBot.GetInstance.GetLogger().Append($"Collecting loot. LootID: {lootId}", 1);
                }
            }
        }
    }
}
