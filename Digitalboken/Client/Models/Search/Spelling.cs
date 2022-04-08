using Newtonsoft.Json;

namespace Digitalboken.Client.Models.Search
{
    public partial class Spelling
    {
        [JsonProperty("correctedQuery")]
        public string CorrectedQuery { get; set; }

        [JsonProperty("htmlCorrectedQuery")]
        public string HtmlCorrectedQuery { get; set; }
    }
}