using Digitalboken.Server.Models.Search;

namespace Digitalboken.Server.Interfaces
{
    public interface IGoogleSearchService
    {
        public Task<Search> Search(string query);
    }
}
