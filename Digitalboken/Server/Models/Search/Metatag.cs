using Newtonsoft.Json;

namespace Digitalboken.Server.Models.Search
{
    public partial class Metatag
    {
        [JsonProperty("application-name")]
        public string ApplicationName { get; set; }

        [JsonProperty("og:image")]
        public Uri OgImage { get; set; }

        [JsonProperty("og:type")]
        public string OgType { get; set; }

        [JsonProperty("og:image:width")]
        public long OgImageWidth { get; set; }

        [JsonProperty("twitter:card")]
        public string TwitterCard { get; set; }

        [JsonProperty("twitter:title")]
        public string TwitterTitle { get; set; }

        [JsonProperty("mod")]
        public bool Mod { get; set; }

        [JsonProperty("og:site_name")]
        public string OgSiteName { get; set; }

        [JsonProperty("apple-mobile-web-app-title")]
        public string AppleMobileWebAppTitle { get; set; }

        [JsonProperty("og:title")]
        public string OgTitle { get; set; }

        [JsonProperty("og:image:height")]
        public long OgImageHeight { get; set; }

        [JsonProperty("og:description")]
        public string OgDescription { get; set; }

        [JsonProperty("twitter:image")]
        public Uri TwitterImage { get; set; }

        [JsonProperty("referrer")]
        public string Referrer { get; set; }

        [JsonProperty("twitter:image:alt")]
        public string TwitterImageAlt { get; set; }

        [JsonProperty("apple-mobile-web-app-status-bar-style")]
        public string AppleMobileWebAppStatusBarStyle { get; set; }

        [JsonProperty("msapplication-tap-highlight")]
        public string MsapplicationTapHighlight { get; set; }

        [JsonProperty("viewport")]
        public string Viewport { get; set; }

        [JsonProperty("apple-mobile-web-app-capable")]
        public string AppleMobileWebAppCapable { get; set; }

        [JsonProperty("twitter:description")]
        public string TwitterDescription { get; set; }

        [JsonProperty("mobile-web-app-capable")]
        public string MobileWebAppCapable { get; set; }

        [JsonProperty("og:locale")]
        public string OgLocale { get; set; }

        [JsonProperty("og:url")]
        public Uri OgUrl { get; set; }
    }
}