using Digitalboken.Server.Models.Search;

namespace Digitalboken.Server.Interfaces
{
    public interface ISearchRepository
    {
        public Task<bool> InsertAsync(Search data);
        public Task<Search> GetAsync(string guid);
        public Task<Search> GetBySearchTermAsync(string searchTerm);
    }
}
