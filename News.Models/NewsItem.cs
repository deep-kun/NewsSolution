using Newtonsoft.Json;
using System;

namespace News.Models
{
    public class NewsItem
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonProperty("category")]
        public string[] Category { get; set; }

        [JsonProperty("published")]
        public string Published { get; set; }
    }
}
