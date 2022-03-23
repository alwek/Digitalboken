using Newtonsoft.Json;

namespace Digitalboken.Server.Models.Search
{
    public partial class CseThumbnail
    {
        [JsonProperty("src")]
        public Uri Src { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }
    }
}