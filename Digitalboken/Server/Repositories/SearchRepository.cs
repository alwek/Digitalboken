using Digitalboken.Server.Interfaces;
using Digitalboken.Server.Models.Search;
using Microsoft.Azure.Cosmos;

namespace Digitalboken.Server.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ILogger<SearchRepository> _logger;
        private readonly Container _container;

        public SearchRepository(ILogger<SearchRepository> logger, CosmosClient client)
        {
            _container = client.GetDatabase("Digitalboken").GetContainer("Search");
            _logger = logger;
        }

        public async Task<Search> GetBySearchTermAsync(string searchTerm)
        {
            try
            {
                return _container
                    .GetItemLinqQueryable<Search>(allowSynchronousQueryExecution: true)
                    .Where(x => x.Queries.Request[0].SearchTerms == searchTerm)
                    .ToList()
                    .FirstOrDefault();
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
                return await _container.ReadItemAsync<Search>(guid, PartitionKey.None);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> InsertAsync(Search data)
        {
            try
            {
                await _container.CreateItemAsync(data);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}