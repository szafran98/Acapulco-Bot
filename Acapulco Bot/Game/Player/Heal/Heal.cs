using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Acapulco_Bot.Utils;
using Acapulco_Bot.Bot;

namespace Acapulco_Bot.Game.Player
{
    internal class Potion
    {
        public int keyid { get; set; }
        public int amount { get; set; }
        public int healvalue { get; set; }
        public string name { get; set; }
    }

    class Heal
    {
        private async Task _checkHealth(int hp, int maxHp, int percentage, Potion potion)
        {   
            await _heal(potion.keyid.ToString());
            AcapulcoBot.GetInstance.GetLogger().Append($"Player hp below {Settings.AutoHealHP}%, healing with {potion.name} for {potion.healvalue} HP", 1);
        }

        private List<Potion> _getItems()
        {
            List<Potion> list = new List<Potion>();
            foreach (KeyValuePair<string, Game.Values> key in AcapulcoBot.GetInstance.GetEngine().GetItems().item)
            {
                Match match1 = Regex.Match(key.Value.stat, "amount=(.*?);");
                Match match = Regex.Match(key.Value.stat, "leczy=(.*?);");
                Match match2 = Regex.Match(key.Value.stat, "leczy=(.*)");

                if (match1.Success && match.Success)
                {
                    string amountp = match1.Groups[1].Value;
                    string healp = match.Groups[1].Value;

                    list.Add(new Potion
                    {
                        keyid = int.Parse(key.Key),
                        amount = int.Parse(amountp),
                        healvalue = int.Parse(healp),
                        name = key.Value.name
                    });
                }
                else if (match1.Success && match2.Success)
                {
                    string amountp = match1.Groups[1].Value;
                    string healp = match2.Groups[1].Value;

                    list.Add(new Potion
                    {
                        keyid = int.Parse(key.Key),
                        amount = int.Parse(amountp),
                        healvalue = int.Parse(healp),
                        name = key.Value.name
                    });
                }
            }
            return list;
        }

        private async Task _heal(string item)
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

                    Uri url = new Uri($"http://{AcapulcoBot.GetInstance.GetEngine().GetServer()}.margonem.pl/engine?t=moveitem&id={item}&st=1&mobile=1&ev={AcapulcoBot.GetInstance.GetEngine().GetEvent()}&mobile_token={AcapulcoBot.GetInstance.GetEngine().GetToken()}");
                    AcapulcoBot.GetInstance.GetCookies().Add(url, new System.Net.Cookie("mchar_id", AcapulcoBot.GetInstance.GetEngine().GetCharacterID()));

                    string response = await client.GetStringAsync(url);

                    Items items = JsonConvert.DeserializeObject<Items>(response);

                    if (response.Contains("warrior_stats"))
                    {
                        AcapulcoBot.GetInstance.GetPlayer().InitializeStatistics(response);
                    }

                    Match match = Regex.Match(response, "\"stat\": \"(.*?)\"");

                    string authKey = match.Groups[1].Value;
                    foreach (KeyValuePair<string, Game.Values> item2 in AcapulcoBot.GetInstance.GetEngine().GetItems().item)
                    {
                        if (item2.Key == item)
                        {
                            item2.Value.stat = authKey;
                        }
                    }
                }
            }
        }

        public async Task Request()
        {
            if (AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.warrior_stats.hp != AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.warrior_stats.maxhp)
            {
                int p = ((int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.warrior_stats.hp) - int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.warrior_stats.maxhp)) / Math.Abs(int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.warrior_stats.maxhp))) * 100;
                if (p < Settings.AutoHealHP)
                {
                    IOrderedEnumerable<Potion> potions = from heals in _getItems() orderby heals.healvalue descending select heals;

                    foreach (Potion potion in potions)
                    {
                        await _checkHealth(int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.warrior_stats.hp), int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.warrior_stats.maxhp), Settings.AutoHealHP, potion);
                        break;
                    }
                }
            }
        }
    }
}