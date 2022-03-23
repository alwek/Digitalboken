using Newtonsoft.Json;

namespace Digitalboken.Server.Models.Search
{
    public partial class Label
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("label_with_op")]
        public string LabelWithOp { get; set; }
    }
}