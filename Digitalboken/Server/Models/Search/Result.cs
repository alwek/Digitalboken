using Newtonsoft.Json;

namespace Digitalboken.Server.Models.Search
{
    public partial class Result
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("htmlTitle")]
        public string HtmlTitle { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("displayLink")]
        public string DisplayLink { get; set; }

        [JsonProperty("snippet")]
        public string Snippet { get; set; }

        [JsonProperty("htmlSnippet")]
        public string HtmlSnippet { get; set; }

        [JsonProperty("cacheId")]
        public string CacheId { get; set; }

        [JsonProperty("formattedUrl")]
        public string FormattedUrl { get; set; }

        [JsonProperty("htmlFormattedUrl")]
        public string HtmlFormattedUrl { get; set; }

        [JsonProperty("pagemap")]
        public Pagemap Pagemap { get; set; }

        [JsonProperty("mime")]
        public string Mime { get; set; }

        [JsonProperty("fileFormat")]
        public string FileFormat { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("labels")]
        public Label[] Labels { get; set; }
    }
}