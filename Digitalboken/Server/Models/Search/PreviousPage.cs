using Newtonsoft.Json;

namespace Digitalboken.Server.Models.Search
{
    public partial class PreviousPage
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("totalResults")]
        public string TotalResults { get; set; }

        [JsonProperty("searchTerms")]
        public string SearchTerms { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("startIndex")]
        public long StartIndex { get; set; }

        [JsonProperty("startPage")]
        public long StartPage { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("inputEncoding")]
        public string InputEncoding { get; set; }

        [JsonProperty("outputEncoding")]
        public string OutputEncoding { get; set; }

        [JsonProperty("safe")]
        public string Safe { get; set; }

        [JsonProperty("cx")]
        public string Cx { get; set; }

        [JsonProperty("sort")]
        public string Sort { get; set; }

        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("gl")]
        public string Gl { get; set; }

        [JsonProperty("cr")]
        public string Cr { get; set; }

        [JsonProperty("googleHost")]
        public string GoogleHost { get; set; }

        [JsonProperty("disableCnTwTranslation")]
        public string DisableCnTwTranslation { get; set; }

        [JsonProperty("hq")]
        public string Hq { get; set; }

        [JsonProperty("hl")]
        public string Hl { get; set; }

        [JsonProperty("siteSearch")]
        public string SiteSearch { get; set; }

        [JsonProperty("siteSearchFilter")]
        public string SiteSearchFilter { get; set; }

        [JsonProperty("exactTerms")]
        public string ExactTerms { get; set; }

        [JsonProperty("excludeTerms")]
        public string ExcludeTerms { get; set; }

        [JsonProperty("linkSite")]
        public string LinkSite { get; set; }

        [JsonProperty("orTerms")]
        public string OrTerms { get; set; }

        [JsonProperty("relatedSite")]
        public string RelatedSite { get; set; }

        [JsonProperty("dateRestrict")]
        public string DateRestrict { get; set; }

        [JsonProperty("lowRange")]
        public string LowRange { get; set; }

        [JsonProperty("highRange")]
        public string HighRange { get; set; }

        [JsonProperty("fileType")]
        public string FileType { get; set; }

        [JsonProperty("rights")]
        public string Rights { get; set; }

        [JsonProperty("searchType")]
        public string SearchType { get; set; }

        [JsonProperty("imgSize")]
        public string ImgSize { get; set; }

        [JsonProperty("imgType")]
        public string ImgType { get; set; }

        [JsonProperty("imgColorType")]
        public string ImgColorType { get; set; }

        [JsonProperty("imgDominantColor")]
        public string ImgDominantColor { get; set; }
    }
}