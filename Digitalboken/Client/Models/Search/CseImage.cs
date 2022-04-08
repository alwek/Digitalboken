using Newtonsoft.Json;

namespace Digitalboken.Client.Models.Search
{
    public partial class CseImage
    {
        [JsonProperty("src")]
        public Uri Src { get; set; }
    }
}