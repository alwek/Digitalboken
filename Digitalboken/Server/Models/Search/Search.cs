using Newtonsoft.Json;

namespace Digitalboken.Server.Models.Search
{
    public partial class Search
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("url")]
        public Url Url { get; set; }

        [JsonProperty("searchInformation")]
        public SearchInformation SearchInformation { get; set; }

        [JsonProperty("spelling")]
        public Spelling Spelling { get; set; }

        [JsonProperty("context")]
        public object Context { get; set; }

        [JsonProperty("promotions")]
        public Promotion[] Promotions { get; set; }

        [JsonProperty("items")]
        public Result[] Results { get; set; }

        [JsonProperty("queries")]
        public Queries Queries { get; set; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }
    }
}