using Digitalboken.Server.Models;

namespace Digitalboken.Server.Interfaces
{
    public interface IDocumentRepository
    {
        public Task InsertAsync(Document document);
        public Task<Document> GetAsync(string guid);
        public Task<Document> GetByNameAsync(string name);
    }
}
