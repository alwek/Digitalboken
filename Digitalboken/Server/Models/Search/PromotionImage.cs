using Newtonsoft.Json;

namespace Digitalboken.Server.Models.Search
{
    public partial class PromotionImage
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }
    }
}