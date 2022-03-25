using Digitalboken.Server.Interfaces;
using Digitalboken.Server.Models;
using MongoDB.Driver;

namespace Digitalboken.Server.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IMongoCollection<Document> _collection;
        private readonly ILogger<DocumentRepository> _logger;

        public DocumentRepository(IMongoDatabase database, ILogger<DocumentRepository> logger)
        {
            _collection = database.GetCollection<Document>(nameof(Document));
            _logger = logger;
        }

        public async Task InsertAsync(Document entity)
        {
            try
            {
                await _collection.InsertOneAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task<Document> GetAsync(Guid guid)
        {
            return await _collection.Find(x => x.Id == guid).FirstOrDefaultAsync();
        }
    }
}
