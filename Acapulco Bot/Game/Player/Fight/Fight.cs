using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Acapulco_Bot.Utils;
using Acapulco_Bot.Utils.Hash;
using Acapulco_Bot.Bot;

using Newtonsoft.Json;

namespace Acapulco_Bot.Game.Player
{
    class Fight
    {
        public async Task Attack(string town)
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

                    Uri url = new Uri($"http://{AcapulcoBot.GetInstance.GetEngine().GetServer()}.margonem.pl/engine?t=fight&a=attack&town_id={town}&mobile=1&ev={AcapulcoBot.GetInstance.GetEngine().GetEvent()}&mobile_token={AcapulcoBot.GetInstance.GetEngine().GetToken()}");
                    AcapulcoBot.GetInstance.GetCookies().Add(url, new System.Net.Cookie("mchar_id", AcapulcoBot.GetInstance.GetEngine().GetCharacterID()));

                    string response = await client.GetStringAsync(url);

                    AcapulcoBot.GetInstance.GetLogger().Append($"Sending attack request. TownID {town}", 4);
                }
            }
        }

        public async Task AutoAttack()
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

                    Uri url = new Uri($"http://{AcapulcoBot.GetInstance.GetEngine().GetServer()}.margonem.pl/engine?t=fight&a=f&mobile=1&ev={AcapulcoBot.GetInstance.GetEngine().GetEvent()}&mobile_token={AcapulcoBot.GetInstance.GetEngine().GetToken()}");

                    AcapulcoBot.GetInstance.GetCookies().Add(url, new System.Net.Cookie("mchar_id", AcapulcoBot.GetInstance.GetEngine().GetCharacterID()));

                    string response = await client.GetStringAsync(url);
                    AcapulcoBot.GetInstance.GetLogger().Append($"Sending auto attack request.", 4);
                }
            }
        }

        public async Task Close()
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

                    Uri url = new Uri($"http://{AcapulcoBot.GetInstance.GetEngine().GetServer()}.margonem.pl/engine?t=fight&a=quit&mobile=1&ev={AcapulcoBot.GetInstance.GetEngine().GetEvent()}&mobile_token={AcapulcoBot.GetInstance.GetEngine().GetToken()}");
                    AcapulcoBot.GetInstance.GetCookies().Add(url, new System.Net.Cookie("mchar_id", AcapulcoBot.GetInstance.GetEngine().GetCharacterID()));

                    string response = await client.GetStringAsync(url);

                    if (response.Contains("warrior_stats"))
                    {
                        AcapulcoBot.GetInstance.GetPlayer().InitializeStatistics(response);
                        await Task.Delay(500);

                        AcapulcoBot.GetInstance.SetDrops(JsonConvert.DeserializeObject<Items>(response));
                        if (Settings.AutoSell)
                        {
                            AcapulcoBot.GetInstance.SetDropsToSell(JsonConvert.DeserializeObject<Items>(response));
                        }

                        AcapulcoBot.GetInstance.GetLogger().Append($"Finished fight. No loot found.", 5);

                        if (AcapulcoBot.GetInstance.GetDrops().item != null)
                        {
                            foreach (string itema in AcapulcoBot.GetInstance.GetDrops().item.Keys.ToList<string>())
                            {
                                Game.Values value = AcapulcoBot.GetInstance.GetDrops().item[itema];
                                if (value.loc.Contains("g"))
                                {
                                    AcapulcoBot.GetInstance.GetDrops().item.Remove(itema);
                                    if (Settings.AutoSell)
                                    {
                                        AcapulcoBot.GetInstance.GetDropsToSell().item.Remove(itema);
                                    }
                                }
                                
                                if (Settings.MinimalPrice && Settings.MinimalPriceG < int.Parse(value.pr))
                                {
                                    AcapulcoBot.GetInstance.GetDrops().item.Remove(itema);
                                    if (Settings.AutoSell)
                                    {
                                        AcapulcoBot.GetInstance.GetDropsToSell().item.Remove(itema);
                                    }

                                    AcapulcoBot.GetInstance.GetLogger().Append($"Removing {value.name} form item pool due to item price, price: {value.pr}, minimum: {Settings.MinimalPriceG}", 5);
                                }
                                AcapulcoBot.GetInstance.GetLogger().Append($"Finished fight. Found {value.name}, price: {value.pr}", 1);
                            }
                        }
                    }
                    else
                    {
                        AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.warrior_stats.hp = "1";
                        if (Settings.AutoHealOnDie)
                        {
                            await AcapulcoBot.GetInstance.GetPlayer().GetHeal().Request();
                        }
                        AcapulcoBot.GetInstance.GetLogger().Append($"Finished fight. Player died.", 2);
                    }
                }
            }
        }
    }
}
