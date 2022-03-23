using Digitalboken.Server.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Digitalboken.Server.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly ILogger<RedisCacheService> _logger;
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        public RedisCacheService(ILogger<RedisCacheService> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
            _options = new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromSeconds(300) };
        }

        public async Task InsertAsync(string key, string value)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException(nameof(key), "Key cannot be null or empty.");
                else if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value), "Value cannot be null or empty.");

                await _cache.SetStringAsync(key, value, _options);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not cache value.");
            }
        }

        public async Task<string> GetAsync(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException(nameof(key), "Key cannot be null or empty.");

                byte[] encoded = await _cache.GetAsync(key);

                if(encoded == null)
                    throw new ArgumentNullException(nameof(encoded), "Encoded value is null or empty.");

                return Encoding.UTF8.GetString(encoded);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not get cache value.");
                return null;
            }
        }

        public async Task RefreshAsync(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException(nameof(key), "Key cannot be null or empty.");

                await _cache.RefreshAsync(key);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Could not refresh cache key.");
            }
        }
    }
}