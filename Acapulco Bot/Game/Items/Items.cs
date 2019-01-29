using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Acapulco_Bot.Game
{
    public class Items
    {
        [JsonProperty(PropertyName = "item")]
		public Dictionary<string, Values> item { get; set; }
    }

    public class Values
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "own")]
        public string own { get; set; }

        [JsonProperty(PropertyName = "loc")]
        public string loc { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public string icon { get; set; }

        [JsonProperty(PropertyName = "x")]
        public string x { get; set; }

        [JsonProperty(PropertyName = "y")]
        public string y { get; set; }

        [JsonProperty(PropertyName = "cl")]
        public string cl { get; set; }

        [JsonProperty(PropertyName = "pr")]
        public string pr { get; set; }

        [JsonProperty(PropertyName = "st")]
        public string st { get; set; }

        [JsonProperty(PropertyName = "stat")]
        public string stat { get; set; }
    }
}
