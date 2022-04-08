using Newtonsoft.Json;

namespace Digitalboken.Server.Models
{
    public class Document
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; } = "/document";
    }
}
