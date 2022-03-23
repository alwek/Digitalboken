using Newtonsoft.Json;

namespace Digitalboken.Server.Models.Search
{
    public partial class Queries
    {
        [JsonProperty("previousPage")]
        public PreviousPage[] PreviousPage { get; set; }

        [JsonProperty("request")]
        public PreviousPage[] Request { get; set; }

        [JsonProperty("nextPage")]
        public NextPage[] NextPage { get; set; }
    }
}