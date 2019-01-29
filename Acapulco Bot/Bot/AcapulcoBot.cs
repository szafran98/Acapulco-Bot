using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using Acapulco_Bot.Game.Player;

using Acapulco_Bot.Game.Engine;
using Acapulco_Bot.Utils.Hash;
using Acapulco_Bot.Game;

namespace Acapulco_Bot.Bot
{
    public class Character
    {
        public string name;
        public string id;
    }

    class AcapulcoBot
    {
        static AcapulcoBot _instance;

        private const string HASH_STRING = "mleczko";

        private CookieContainer _cookies = new CookieContainer();
        private List<Character> _characters = new List<Character>();
        private string _content;

        private Engine _engine = new Engine();
        private Player _player = new Player();

        private Items _dropsToSell = new Items();
        private Items _drops = new Items();

        private Logger _logger = new Logger();

        public static AcapulcoBot GetInstance
        {
            get { if (_instance == null) _instance = new AcapulcoBot(); return _instance; }
        }

        AcapulcoBot() { }

        public async Task<bool> RequestLogin(string login, string pass)
        {
            using (HttpClientHandler handler = new HttpClientHandler { CookieContainer = _cookies })
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Dalvik/2.1.0 (Linux; U; Android 8.0.0; Samsung Galaxy S8 - 8.0 - API 26 - 1440x2960 Build/OPR6.170623.017)");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("X-Unity-Version", "5.6.2p4");

                    Dictionary<string, string> credentials = new Dictionary<string, string> { { "l", login }, {"ph", SHA1.Encode(HASH_STRING + pass) } };
                    FormUrlEncodedContent content = new FormUrlEncodedContent(credentials);
                    HttpResponseMessage httpResponseMessage = await client.PostAsync("http://www.margonem.pl/ajax/logon.php?t=login", content);

                    HttpResponseMessage response = httpResponseMessage;
                    _content = await response.Content.ReadAsStringAsync();

                    if (_content.Contains("logged"))
                        return true;
                    else
                        return false;
                }
            }
        }

        public void FindCharacters()
        {
            string pattern = "option label=\"(.*?)\" value=\"(.*?)\"";
            Regex regex = new Regex(pattern, RegexOptions.None);

            foreach (object obj in regex.Matches(_content))
            {
                Match match = (Match)obj;
                bool success = match.Success;
                if (success)
                {
                    _characters.Add(new Character
                    {
                        name = match.Groups[1].Value,
                        id = match.Groups[2].Value
                    });
                }
            }
        }

        public CookieContainer GetCookies()
        {
            return _cookies;
        }

        public List<Character> GetCharacters()
        {
            return _characters;
        }

        public Engine GetEngine()
        {
            return _engine;
        }

        public Player GetPlayer()
        {
            return _player;
        }

        public void SetDrops(Items data)
        {
            _drops = data;
        }

        public Items GetDrops()
        {
            return _drops;
        }

        public void SetDropsToSell(Items data)
        {
            _dropsToSell = data;
        }

        public Items GetDropsToSell()
        {
            return _dropsToSell;
        }

        public Logger GetLogger()
        {
            return _logger;
        }
    }
}
