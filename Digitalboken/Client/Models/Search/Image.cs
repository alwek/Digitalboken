using Newtonsoft.Json;

namespace Digitalboken.Client.Models.Search
{
    public partial class Image
    {
        [JsonProperty("contextLink")]
        public string ContextLink { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("byteSize")]
        public long ByteSize { get; set; }

        [JsonProperty("thumbnailLink")]
        public string ThumbnailLink { get; set; }

        [JsonProperty("thumbnailHeight")]
        public long ThumbnailHeight { get; set; }

        [JsonProperty("thumbnailWidth")]
        public long ThumbnailWidth { get; set; }
    }
}