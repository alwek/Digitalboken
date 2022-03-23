using Newtonsoft.Json;

namespace Digitalboken.Server.Models.Search
{
    public partial class Pagemap
    {
        [JsonProperty("cse_thumbnail")]
        public CseThumbnail[] CseThumbnail { get; set; }

        [JsonProperty("metatags")]
        public Metatag[] Metatags { get; set; }

        [JsonProperty("cse_image")]
        public CseImage[] CseImage { get; set; }
    }
}