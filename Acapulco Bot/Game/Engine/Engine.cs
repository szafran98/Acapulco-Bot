using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Acapulco_Bot.Bot;
using Acapulco_Bot.Utils;
using Acapulco_Bot.Utils.Hash;

using Newtonsoft.Json;

namespace Acapulco_Bot.Game.Engine
{
    class Engine
    {
        private string _characterToken;
        private string _characterEvent;

        private string _server;
        private string _id;

        private Items _items = new Items();

        public async Task<string> Initialize(string server, string characterId, int level, bool useToken = false)
        {
            using (HttpClientHandler handler = new HttpClientHandler { CookieContainer = AcapulcoBot.GetInstance.GetCookies() })
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Dalvik/2.1.0 (Linux; U; Android 8.0.0; Samsung Galaxy S8 - 8.0 - API 26 - 1440x2960 Build/OPR6.170623.017)");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("X-Unity-Version", "5.6.2p4");

                    Uri url = new Uri(string.Concat(new string[]
                    {
                        $"http://{server}",
                        ".margonem.pl/engine?t=init&initlvl=",
                        $"{level}",
                        $"&mucka={Mucka.GenerateMucka()}",
                        "&mobile=1",
                        useToken ? $"&mobile_token={_characterToken}" : ""
                    }));

                    AcapulcoBot.GetInstance.GetCookies().Add(url, new Cookie("mchar_id", characterId));
                    string response = await client.GetStringAsync(url);

                    if (level == 1)
                    {
                        Match m = Regex.Match(response, "\"mobile_token\": \"(.*?)\",");
                        if (m.Success)
                        {
                            _characterToken = MD5.Hash(m.Groups[1].Value);
                        }
                        else
                        {
                            MessageBox.Show("Ta postać jest zarejestrowana na innym świecie.", "Acapulco Bot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(0);
                        }
                    }
                    else if (level == 4)
                    {
                        Match m = Regex.Match(response, "\"ev\": (.*?),");
                        if (m.Success)
                        {
                            _characterEvent = m.Groups[1].Value;
                        }

                        _server = server;
                        _id = characterId;
                    }

                    return response;
                }
            }
        }

        public async Task<bool> RefreshEvent()
        {
            using (HttpClientHandler handler = new HttpClientHandler { CookieContainer = AcapulcoBot.GetInstance.GetCookies() })
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Dalvik/2.1.0 (Linux; U; Android 8.0.0; Samsung Galaxy S8 - 8.0 - API 26 - 1440x2960 Build/OPR6.170623.017)");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("X-Unity-Version", "5.6.2p4");

                    Uri url = new Uri($"http://{_server}.margonem.pl/engine?t=_&aid={_id}&mobile=1&ev={_characterEvent}&mobile_token={_characterToken}");

                    string response = await client.GetStringAsync(url);

                    Match m = Regex.Match(response, "\"ev\": (.*?),");
                    if (m.Success)
                    {
                        _characterEvent = m.Groups[1].Value;
                        return true;
                    }

                    return false;
                }
            }
        }

        public void InitializeItems(string data)
        {
            _items = JsonConvert.DeserializeObject<Items>(data);
        }

        public Items GetItems()
        {
            return _items;
        }

        public string GetToken()
        {
            return _characterToken;
        }

        public string GetEvent()
        {
            return _characterEvent;
        }

        public string GetCharacterID()
        {
            return _id;
        }

        public string GetServer()
        {
            return _server;
        }
    }
}
