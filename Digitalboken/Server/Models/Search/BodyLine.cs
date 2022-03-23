using Newtonsoft.Json;

namespace Digitalboken.Server.Models.Search
{
    public partial class BodyLine
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("htmlTitle")]
        public string HtmlTitle { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }
}