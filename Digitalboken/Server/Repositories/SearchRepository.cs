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

        public async Task<Search> GetByQueryAsync(string searchTerm)
        {
            try
            {
                var result = await _collection.FindAsync(
                    x => x.Queries.Request.FirstOrDefault().SearchTerms == searchTerm);
                return await result.FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Search> GetAsync(string guid)
        {
            try
            {
                var result = await _collection.FindAsync(x => x.Id == guid);
                return await result.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
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
