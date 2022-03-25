using Digitalboken.Server.Models;

namespace Digitalboken.Server.Interfaces
{
    public interface IDocumentRepository
    {
        public Task InsertAsync(Document document);
        public Task<Document> GetAsync(Guid guid);
    }
}
