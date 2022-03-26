using Digitalboken.Server.Models.Search;

namespace Digitalboken.Server.Interfaces
{
    public interface ISearchRepository
    {
        public Task InsertAsync(Search data);
        public Task<Search> GetAsync(string guid);
        public Task<Search> GetByQueryAsync(string searchTerm);
    }
}
