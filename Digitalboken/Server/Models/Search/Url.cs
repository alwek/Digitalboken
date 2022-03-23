using Newtonsoft.Json;

namespace Digitalboken.Server.Models.Search
{
    public partial class Url
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("template")]
        public string Template { get; set; }
    }
}