using Digitalboken.Server.Interfaces;
using Digitalboken.Server.Models.Search;
using MongoDB.Driver;

namespace Digitalboken.Server.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IMongoCollection<Search> _collection;
        private readonly ILogger<SearchRepository> _logger;

        public SearchRepository(IMongoDatabase database, ILogger<SearchRepository> logger)
        {
            _collection = database.GetCollection<Search>(nameof(Search));
            _logger = logger;
        }

        public async Task InsertAsync(Search data)
        {
            try
            {
                await _collection.InsertOneAsync(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
