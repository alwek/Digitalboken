using Digitalboken.Server.Interfaces;
using Digitalboken.Server.Models;
using Microsoft.Azure.Cosmos;

namespace Digitalboken.Server.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly Container _container;
        private readonly ILogger<DocumentRepository> _logger;

        public DocumentRepository(ILogger<DocumentRepository> logger, CosmosClient client)
        {
            _container = client.GetDatabase("Digitalboken").GetContainer("Document");
            _logger = logger;
        }

        public async Task<bool> InsertAsync(Document entity)
        {
            try
            {
                await _container.CreateItemAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<Document> GetAsync(string guid)
        {
            try
            {
                return await _container.ReadItemAsync<Document>(guid, PartitionKey.None);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Document> GetByNameAsync(string name)
        {
            try
            {
                return _container
                    .GetItemLinqQueryable<Document>(allowSynchronousQueryExecution: true)
                    .Where(x => x.Name == name)
                    .ToList()
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
