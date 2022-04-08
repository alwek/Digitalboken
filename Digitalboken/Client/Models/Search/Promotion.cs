using Newtonsoft.Json;

namespace Digitalboken.Client.Models.Search
{
    public partial class Promotion
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("htmlTitle")]
        public string HtmlTitle { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("displayLink")]
        public string DisplayLink { get; set; }

        [JsonProperty("bodyLines")]
        public BodyLine[] BodyLines { get; set; }

        [JsonProperty("image")]
        public PromotionImage Image { get; set; }
    }
}