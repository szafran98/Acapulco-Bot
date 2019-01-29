using System;

using Newtonsoft.Json;

namespace Acapulco_Bot.Game.Player
{
    public class Statistics
    {
        [JsonProperty(PropertyName = "h")]
        public Values h { get; set; }
    }

    public class Values
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "blockade")]
        public string blockade { get; set; }

        [JsonProperty(PropertyName = "uprawnienia")]
        public string uprawnienia { get; set; }

        [JsonProperty(PropertyName = "ap")]
        public string ap { get; set; }

        [JsonProperty(PropertyName = "bagi")]
        public string bagi { get; set; }

        [JsonProperty(PropertyName = "bint")]
        public string bint { get; set; }

        [JsonProperty(PropertyName = "bstr")]
        public string bstr { get; set; }

        [JsonProperty(PropertyName = "clan")]
        public string clan { get; set; }

        [JsonProperty(PropertyName = "clanrank")]
        public string clanrank { get; set; }

        [JsonProperty(PropertyName = "credits")]
        public string credits { get; set; }

        [JsonProperty(PropertyName = "runes")]
        public string runes { get; set; }

        [JsonProperty(PropertyName = "dir")]
        public string dir { get; set; }

        [JsonProperty(PropertyName = "exp")]
        public string exp { get; set; }

        [JsonProperty(PropertyName = "fgrp")]
        public string fgrp { get; set; }

        [JsonProperty(PropertyName = "gold")]
        public string gold { get; set; }

        [JsonProperty(PropertyName = "goldlim")]
        public string goldlim { get; set; }

        [JsonProperty(PropertyName = "healpower")]
        public string healpower { get; set; }

        [JsonProperty(PropertyName = "honor")]
        public string honor { get; set; }

        [JsonProperty(PropertyName = "img")]
        public string img { get; set; }

        [JsonProperty(PropertyName = "lvl")]
        public string lvl { get; set; }

        [JsonProperty(PropertyName = "mails")]
        public string mails { get; set; }

        [JsonProperty(PropertyName = "mails_all")]
        public string mails_all { get; set; }

        [JsonProperty(PropertyName = "mails_last")]
        public string mails_last { get; set; }

        [JsonProperty(PropertyName = "mpath")]
        public string mpath { get; set; }

        [JsonProperty(PropertyName = "nick")]
        public string nick { get; set; }

        [JsonProperty(PropertyName = "opt")]
        public string opt { get; set; }

        [JsonProperty(PropertyName = "prof")]
        public string prof { get; set; }

        [JsonProperty(PropertyName = "pttl")]
        public string pttl { get; set; }

        [JsonProperty(PropertyName = "pvp")]
        public string pvp { get; set; }

        [JsonProperty(PropertyName = "ttl")]
        public string ttl { get; set; }

        [JsonProperty(PropertyName = "x")]
        public string x { get; set; }

        [JsonProperty(PropertyName = "y")]
        public string y { get; set; }

        [JsonProperty(PropertyName = "bag")]
        public string bag { get; set; }

        [JsonProperty(PropertyName = "party")]
        public string party { get; set; }

        [JsonProperty(PropertyName = "trade")]
        public string trade { get; set; }

        [JsonProperty(PropertyName = "wanted")]
        public string wanted { get; set; }

        [JsonProperty(PropertyName = "stamina")]
        public string stamina { get; set; }

        [JsonProperty(PropertyName = "stamina_ts")]
        public string stamina_ts { get; set; }

        [JsonProperty(PropertyName = "stamina_renew_sec")]
        public string stamina_renew_sec { get; set; }

        [JsonProperty(PropertyName = "st")]
        public string st { get; set; }

        [JsonProperty(PropertyName = "ag")]
        public string ag { get; set; }

        [JsonProperty(PropertyName = "it")]
        public string it { get; set; }

        [JsonProperty(PropertyName = "dmg")]
        public string dmg { get; set; }

        [JsonProperty(PropertyName = "ac")]
        public string ac { get; set; }

        [JsonProperty(PropertyName = "act")]
        public string act { get; set; }

        [JsonProperty(PropertyName = "resis")]
        public string resis { get; set; }

        [JsonProperty(PropertyName = "sa")]
        public string sa { get; set; }

        [JsonProperty(PropertyName = "hp")]
        public string hp { get; set; }

        [JsonProperty(PropertyName = "heal")]
        public string heal { get; set; }

        [JsonProperty(PropertyName = "maxhp")]
        public string maxhp { get; set; }

        [JsonProperty(PropertyName = "crit")]
        public string crit { get; set; }

        [JsonProperty(PropertyName = "critval")]
        public string critval { get; set; }

        [JsonProperty(PropertyName = "critmval")]
        public string critmval { get; set; }

        [JsonProperty(PropertyName = "critmval2")]
        public string critmval2 { get; set; }

        [JsonProperty(PropertyName = "of_crit")]
        public string of_crit { get; set; }

        [JsonProperty(PropertyName = "of_critval")]
        public string of_critval { get; set; }

        [JsonProperty(PropertyName = "evade")]
        public string evade { get; set; }

        [JsonProperty(PropertyName = "absorb")]
        public string absorb { get; set; }

        [JsonProperty(PropertyName = "absorbm")]
        public string absorbm { get; set; }

        [JsonProperty(PropertyName = "block")]
        public string block { get; set; }

        [JsonProperty(PropertyName = "mana")]
        public string mana { get; set; }

        [JsonProperty(PropertyName = "energy")]
        public string energy { get; set; }

        [JsonProperty(PropertyName = "warrior_stats")]
        public WarriorStats warrior_stats { get; set; }
    }

    public class WarriorStats
    {
        [JsonProperty(PropertyName = "hp")]
        public string hp { get; set; }

        [JsonProperty(PropertyName = "maxhp")]
        public string maxhp { get; set; }
    }
}
