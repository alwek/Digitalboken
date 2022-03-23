using Newtonsoft.Json;

namespace Digitalboken.Server.Models.Search
{
    public partial class CseImage
    {
        [JsonProperty("src")]
        public Uri Src { get; set; }
    }
}