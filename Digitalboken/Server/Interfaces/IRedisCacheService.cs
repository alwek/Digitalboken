namespace Digitalboken.Server.Interfaces
{
    public interface IRedisCacheService
    {
        public Task InsertAsync(string key, string value);
        public Task<string> GetAsync(string key);
        public Task RefreshAsync(string key);
    }
}
