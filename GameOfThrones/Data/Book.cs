using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GameOfThrones.Data
{
    public partial class Book
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isbn")]
        public string Isbn { get; set; }

        [JsonProperty("authors")]
        public List<string> Authors { get; set; }

        [JsonProperty("numberOfPages")]
        public long NumberOfPages { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("mediaType")]
        public string MediaType { get; set; }

        [JsonProperty("released")]
        public DateTimeOffset Released { get; set; }

        [JsonProperty("characters")]
        public List<Uri> Characters { get; set; }

        [JsonProperty("povCharacters")]
        public List<Uri> PovCharacters { get; set; }
    }

    public partial class Book
    {
        public static List<Book> FromJson(string json) => JsonConvert.DeserializeObject<List<Book>>(json, Converter.Settings);
    }

    public static partial class Serialize
    {
        public static string ToJson(this List<Book> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
