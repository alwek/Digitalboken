using Digitalboken.Server.Models;

namespace Digitalboken.Server.Interfaces
{
    public interface IDocumentRepository
    {
        public Task<bool> InsertAsync(Document document);
        public Task<Document> GetAsync(string guid);
        public Task<Document> GetByNameAsync(string name);
    }
}
