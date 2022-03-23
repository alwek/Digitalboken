using Digitalboken.Server.Interfaces;
using Digitalboken.Server.Models.Search;

namespace Digitalboken.Server.Services
{
    public class GoogleSearchService : IGoogleSearchService
    {
        private readonly ILogger<GoogleSearchService> _logger;
        private readonly HttpClient _httpClient;

        public GoogleSearchService(ILogger<GoogleSearchService> logger, HttpClient client)
        {
            _logger = logger;
            _httpClient = client;
        }

        public async Task<Search> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                _logger.LogError("Cannot perform search on empty query.");
                return null;
            }

            var response = await GetResponseMessage(query);
            if (response == null)
                return null;

            var search = await ReadResponseData(response);
            return search;
        }

        private async Task<HttpResponseMessage> GetResponseMessage(string query)
        {
            try
            {
                string key = _httpClient.DefaultRequestHeaders.GetValues("key").FirstOrDefault();
                string cx = _httpClient.DefaultRequestHeaders.GetValues("cx").FirstOrDefault();

                if (string.IsNullOrWhiteSpace(key))
                    throw new ArgumentNullException(nameof(key), "Key cannot be null.");
                else if (string.IsNullOrWhiteSpace(cx))
                    throw new ArgumentNullException(nameof(cx), "Cx cannot be null.");

                query = $"?key={key}&cx={cx}&q={query}";
                return await _httpClient.GetAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not send request or recieve response from Google Search Api.");
                return null;
            }
        }

        private async Task<Search> ReadResponseData(HttpResponseMessage response)
        {
            try
            {
                string json = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Search>(json);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Could not read or deserialize response data from Google Search Api.");
                return null;
            }
        }
    }
}
