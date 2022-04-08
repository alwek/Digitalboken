using Newtonsoft.Json;

namespace Digitalboken.Client.Models.Search
{
    public partial class SearchInformation
    {
        [JsonProperty("searchTime")]
        public long SearchTime { get; set; }

        [JsonProperty("formattedSearchTime")]
        public string FormattedSearchTime { get; set; }

        [JsonProperty("totalResults")]
        public string TotalResults { get; set; }

        [JsonProperty("formattedTotalResults")]
        public string FormattedTotalResults { get; set; }
    }
}