using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Acapulco_Bot.Bot;

namespace Acapulco_Bot.Game.Player
{
    class Shop
    {
        public async Task Sell(int itemKey)
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

                    Uri url = new Uri($"http://{AcapulcoBot.GetInstance.GetEngine().GetServer()}.margonem.pl/engine?t=shop&sell={itemKey}&mobile=1&ev={AcapulcoBot.GetInstance.GetEngine().GetEvent()}&mobile_token={AcapulcoBot.GetInstance.GetEngine().GetToken()}");
                    AcapulcoBot.GetInstance.GetCookies().Add(url, new System.Net.Cookie("mchar_id", AcapulcoBot.GetInstance.GetEngine().GetCharacterID()));

                    string response = await client.GetStringAsync(url);

                    if (response.Contains("warrior_stats"))
                    {
                       AcapulcoBot.GetInstance.GetPlayer().InitializeStatistics(response);
                    }

                    AcapulcoBot.GetInstance.GetLogger().Append("Selling item.", 1);
                }
            }
        }
    }
}
