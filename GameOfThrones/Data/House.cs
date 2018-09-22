using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameOfThrones.Data
{
    public partial class House
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("coatOfArms")]
        public string CoatOfArms { get; set; }

        [JsonProperty("words")]
        public string Words { get; set; }

        [JsonProperty("titles")]
        public List<string> Titles { get; set; }

        [JsonProperty("seats")]
        public List<string> Seats { get; set; }

        [JsonProperty("currentLord")]
        public string CurrentLord { get; set; }

        [JsonProperty("heir")]
        public string Heir { get; set; }

        [JsonProperty("overlord")]
        public string Overlord { get; set; }

        [JsonProperty("founded")]
        public string Founded { get; set; }

        [JsonProperty("founder")]
        public string Founder { get; set; }

        [JsonProperty("diedOut")]
        public string DiedOut { get; set; }

        [JsonProperty("ancestralWeapons")]
        public List<string> AncestralWeapons { get; set; }

        [JsonProperty("cadetBranches")]
        public List<Uri> CadetBranches { get; set; }

        [JsonProperty("swornMembers")]
        public List<Uri> SwornMembers { get; set; }
    }

    public partial class House
    {
        public static List<House> FromJson(string json) => JsonConvert.DeserializeObject<List<House>>(json, Converter.Settings);
    }

    public static partial class Serialize
    {
        public static string ToJson(this List<House> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
