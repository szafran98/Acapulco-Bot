using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Acapulco_Bot.Game.Player
{
    public class Maps
    {
        [JsonProperty(PropertyName = "mobile_maps")]
        public Dictionary<string, Default> mobile_maps { get; set; }

        [JsonProperty(PropertyName = "mobile_elites")]
        public Dictionary<string, Elites> mobile_elites { get; set; }
    }

    public class Elites
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public string icon { get; set; }

        [JsonProperty(PropertyName = "lvl")]
        public string lvl { get; set; }

        [JsonProperty(PropertyName = "wt")]
        public string wt { get; set; }

        [JsonProperty(PropertyName = "bg")]
        public string bg { get; set; }
    }

    public class Default
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "bg")]
        public string bg { get; set; }

        [JsonProperty(PropertyName = "done")]
        public string done { get; set; }

        [JsonProperty(PropertyName = "next_map")]
        public string next_map { get; set; }

        [JsonProperty(PropertyName = "req_map")]
        public string req_map { get; set; }

        [JsonProperty(PropertyName = "stamina_cost_fight")]
        public string stamina_cost_fight { get; set; }

        [JsonProperty(PropertyName = "stamina_cost_boss")]
        public string stamina_cost_boss { get; set; }
    }
}
